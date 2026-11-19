/* =====================================================================================
   Migracion 01 - RF1 Gestion de turnos (2026-11-17)

   1. Paciente deja de ser un Usuario: tabla propia con IdPaciente, nombre, apellido y email.
      Los turnos pasan a referenciar al paciente por IdPaciente.
   2. T06b Control de cambios: tabla copia Turno_Historial + trigger TR_Turno_Historial.
      Reemplaza a BitacoraEventosTurno y a sus dos triggers.
   3. Horarios de atencion: la especialidad pasa a ser obligatoria.
   4. Roles: Administrativo pasa a Recepcionista; se agrega Contador; se eliminan los roles
      y permisos del paciente; permisos nuevos de la pantalla Gestion de Turnos y del
      Historial de cambios de turnos.

   Se ejecuta UNA vez sobre la base GestionTurnosMedicos (hacer backup antes):
     sqlcmd -S localhost\SQLEXPRESS -E -C -b -I -d GestionTurnosMedicos -i Migracion_01_RF1_Turnos.sql
   (-b corta el script ante el primer error; -I activa QUOTED_IDENTIFIER, que exige el indice
    filtrado UQ_Turno_HorarioFecha_Confirmado)
   ===================================================================================== */
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET XACT_ABORT ON;
SET NOCOUNT ON;

IF COL_LENGTH('dbo.Paciente', 'IdPaciente') IS NOT NULL
BEGIN
    RAISERROR('La migracion 01 ya fue aplicada (Paciente.IdPaciente existe).', 16, 1);
    RETURN;
END
GO

BEGIN TRANSACTION;

/* ---------------------------------------------------------------- 1. Paciente */
-- Triggers y tabla de la bitacora de eventos anterior (se reemplazan por Turno_Historial)
IF OBJECT_ID('dbo.TR_Turno_Insert_BitacoraEventos', 'TR') IS NOT NULL DROP TRIGGER dbo.TR_Turno_Insert_BitacoraEventos;
IF OBJECT_ID('dbo.TR_Turno_Update_BitacoraEventos', 'TR') IS NOT NULL DROP TRIGGER dbo.TR_Turno_Update_BitacoraEventos;
IF OBJECT_ID('dbo.BitacoraEventosTurno', 'U') IS NOT NULL DROP TABLE dbo.BitacoraEventosTurno;

CREATE TABLE dbo.Paciente_Nuevo (
    IdPaciente         int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Paciente_Nuevo PRIMARY KEY,
    DNI                varchar(10)  NOT NULL,
    Nombre             varchar(50)  NOT NULL,
    Apellido           varchar(50)  NOT NULL,
    FechaNacimiento    date         NULL,
    Telefono           varchar(30)  NULL,
    Email              varchar(100) NULL,
    NroHistoriaClinica int          NOT NULL,
    FechaAperturaHC    date         NOT NULL,
    IdUsuarioAnterior  int          NULL   -- solo para mapear los turnos; se borra al final
);

INSERT INTO dbo.Paciente_Nuevo (DNI, Nombre, Apellido, FechaNacimiento, Telefono, Email, NroHistoriaClinica, FechaAperturaHC, IdUsuarioAnterior)
SELECT p.DNI, u.Nombre, u.Apellido, p.FechaNacimiento, p.Telefono, u.Email, p.NroHistoriaClinica, p.FechaAperturaHC, p.IdUsuario
FROM dbo.Paciente p
INNER JOIN dbo.Usuario u ON u.IdUsuario = p.IdUsuario
ORDER BY p.NroHistoriaClinica;

-- Los turnos apuntan al nuevo IdPaciente
DECLARE @fk sysname;
SELECT @fk = name FROM sys.foreign_keys WHERE name = 'FK_Turno_Paciente';
IF @fk IS NOT NULL ALTER TABLE dbo.Turno DROP CONSTRAINT FK_Turno_Paciente;

UPDATE t SET t.IdPaciente = pn.IdPaciente
FROM dbo.Turno t
INNER JOIN dbo.Paciente_Nuevo pn ON pn.IdUsuarioAnterior = t.IdPaciente;

DROP TABLE dbo.Paciente;
EXEC sp_rename 'dbo.Paciente_Nuevo', 'Paciente';
EXEC sp_rename 'dbo.PK_Paciente_Nuevo', 'PK_Paciente', 'OBJECT';
ALTER TABLE dbo.Paciente DROP COLUMN IdUsuarioAnterior;
ALTER TABLE dbo.Paciente ADD CONSTRAINT UQ_Paciente_DNI UNIQUE (DNI);
ALTER TABLE dbo.Paciente ADD CONSTRAINT UQ_Paciente_NroHC UNIQUE (NroHistoriaClinica);
ALTER TABLE dbo.Paciente ADD CONSTRAINT DF_Paciente_NroHC DEFAULT (NEXT VALUE FOR dbo.SEQ_NroHistoriaClinica) FOR NroHistoriaClinica;
ALTER TABLE dbo.Paciente ADD CONSTRAINT DF_Paciente_FechaAperturaHC DEFAULT (CONVERT(date, GETDATE())) FOR FechaAperturaHC;
ALTER TABLE dbo.Turno ADD CONSTRAINT FK_Turno_Paciente FOREIGN KEY (IdPaciente) REFERENCES dbo.Paciente (IdPaciente);

/* ---------------------------------------------------------------- 2. T06b Turno_Historial */
CREATE TABLE dbo.Turno_Historial (
    IdHistorial     int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Turno_Historial PRIMARY KEY,
    IdTurno         int          NOT NULL CONSTRAINT FK_TurnoHistorial_Turno REFERENCES dbo.Turno (IdTurno),
    IdPaciente      int          NOT NULL,   -- copia: sin clave foranea
    IdHorario       int          NOT NULL,   -- copia: sin clave foranea
    Fecha           date         NOT NULL,
    Estado          varchar(20)  NOT NULL,
    FechaCreacion   datetime     NOT NULL,
    Operacion       varchar(15)  NOT NULL CONSTRAINT CK_TurnoHistorial_Operacion CHECK (Operacion IN ('Alta', 'Modificacion')),
    FechaHoraCambio datetime     NOT NULL CONSTRAINT DF_TurnoHistorial_FechaHoraCambio DEFAULT (GETDATE()),
    IdUsuarioCambio int          NULL CONSTRAINT FK_TurnoHistorial_Usuario REFERENCES dbo.Usuario (IdUsuario)
);
CREATE INDEX IX_TurnoHistorial_IdTurno ON dbo.Turno_Historial (IdTurno, FechaHoraCambio);

-- Version inicial de los turnos que ya existian
INSERT INTO dbo.Turno_Historial (IdTurno, IdPaciente, IdHorario, Fecha, Estado, FechaCreacion, Operacion, FechaHoraCambio, IdUsuarioCambio)
SELECT IdTurno, IdPaciente, IdHorario, Fecha, Estado, FechaCreacion, 'Alta', FechaCreacion, NULL
FROM dbo.Turno;

COMMIT TRANSACTION;
GO

-- El trigger copia la fila completa despues de cada alta o modificacion del turno. El usuario lo
-- toma del contexto de la sesion, que la capa de datos fija con DAL_Conexion.EstablecerUsuarioAuditoria.
CREATE TRIGGER dbo.TR_Turno_Historial
ON dbo.Turno
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @operacion varchar(15) = CASE WHEN EXISTS (SELECT 1 FROM deleted) THEN 'Modificacion' ELSE 'Alta' END;
    DECLARE @idUsuario int = TRY_CAST(SESSION_CONTEXT(N'IdUsuario') AS int);

    INSERT INTO dbo.Turno_Historial (IdTurno, IdPaciente, IdHorario, Fecha, Estado, FechaCreacion, Operacion, FechaHoraCambio, IdUsuarioCambio)
    SELECT i.IdTurno, i.IdPaciente, i.IdHorario, i.Fecha, i.Estado, i.FechaCreacion, @operacion, GETDATE(), @idUsuario
    FROM inserted i;
END
GO

BEGIN TRANSACTION;

/* ---------------------------------------------------------------- 3. Especialidad obligatoria en los horarios */
-- Datos de prueba: los horarios sin especialidad toman la primera especialidad del medico.
UPDATE mh SET mh.IdEspecialidad = (SELECT MIN(me.IdEsp) FROM dbo.Medico_Especialidad me WHERE me.IdMedico = mh.IdMedico)
FROM dbo.Medico_Horario mh
WHERE mh.IdEspecialidad IS NULL;

IF NOT EXISTS (SELECT 1 FROM dbo.Medico_Horario WHERE IdEspecialidad IS NULL)
    ALTER TABLE dbo.Medico_Horario ALTER COLUMN IdEspecialidad int NOT NULL;

UPDATE dbo.Especialidades SET Nombre = 'Sin nombre' WHERE Nombre IS NULL;
ALTER TABLE dbo.Especialidades ALTER COLUMN Nombre nvarchar(30) NOT NULL;

/* ---------------------------------------------------------------- 4. Roles y permisos */
-- Roles del paciente: se eliminan con sus permisos
DECLARE @rolesBaja TABLE (IdRol int);
INSERT INTO @rolesBaja SELECT IdRol FROM dbo.Rol WHERE Nombre IN ('Paciente', 'Acceso Paciente', 'Mis Recetas', 'Bitacora de Turnos');
DECLARE @permisosBaja TABLE (IdPermisos int);
INSERT INTO @permisosBaja SELECT IdPermisos FROM dbo.Permisos
WHERE Formulario IN ('frm_AccesoPaciente', 'frmMisRecetas', 'frm_BitacoraTurno');

DELETE FROM dbo.Usuario_Rol WHERE IdRol IN (SELECT IdRol FROM @rolesBaja);
DELETE FROM dbo.Rol_Rol WHERE IdRolPadre IN (SELECT IdRol FROM @rolesBaja) OR IdRolHijo IN (SELECT IdRol FROM @rolesBaja);
DELETE FROM dbo.Rol_Permiso WHERE IdRol IN (SELECT IdRol FROM @rolesBaja) OR IdPermisos IN (SELECT IdPermisos FROM @permisosBaja);
DELETE FROM dbo.Usuario_Permiso WHERE IdPermisos IN (SELECT IdPermisos FROM @permisosBaja);
DELETE FROM dbo.Rol WHERE IdRol IN (SELECT IdRol FROM @rolesBaja);
DELETE FROM dbo.Permisos WHERE IdPermisos IN (SELECT IdPermisos FROM @permisosBaja);

-- Administrativo pasa a Recepcionista: turnos y agenda medica (no idiomas)
UPDATE dbo.Rol SET Nombre = 'Recepcionista', Descripcion = 'Recepcion: turnos, pacientes y agenda medica' WHERE Nombre = 'Administrativo';
DECLARE @recep int = (SELECT IdRol FROM dbo.Rol WHERE Nombre = 'Recepcionista');
DELETE FROM dbo.Rol_Rol WHERE IdRolPadre = @recep AND IdRolHijo = (SELECT IdRol FROM dbo.Rol WHERE Nombre = 'Gestion de Idiomas');
IF NOT EXISTS (SELECT 1 FROM dbo.Rol_Rol WHERE IdRolPadre = @recep AND IdRolHijo = (SELECT IdRol FROM dbo.Rol WHERE Nombre = 'Gestion de Medicos'))
    INSERT INTO dbo.Rol_Rol (IdRolPadre, IdRolHijo) SELECT @recep, IdRol FROM dbo.Rol WHERE Nombre = 'Gestion de Medicos';

-- Contador (sus pantallas se agregan con el RF2)
IF NOT EXISTS (SELECT 1 FROM dbo.Rol WHERE Nombre = 'Contador')
    INSERT INTO dbo.Rol (Nombre, Descripcion, Formulario) VALUES ('Contador', 'Area contable: liquidacion de honorarios', NULL);

-- Usuarios de prueba (G04): Facundo Perez es el contador; Juan Baccigaluppi ya no es usuario de un rol
DELETE FROM dbo.Usuario_Rol WHERE IdUsuario IN (8, 3012);
INSERT INTO dbo.Usuario_Rol (IdUsuario, IdRol) SELECT 8, IdRol FROM dbo.Rol WHERE Nombre = 'Contador';

-- Permisos de la pantalla Gestion de Turnos del recepcionista (CU-01.1, CU-01.2, CU-01.3)
DECLARE @rolTurnos int = (SELECT IdRol FROM dbo.Rol WHERE Formulario = 'frmGestionTurnos');
DECLARE @nuevos TABLE (Nombre varchar(50), Descripcion varchar(100));
INSERT INTO @nuevos VALUES
    ('frmGestionTurnos.btnRegistrarPaciente', 'Registrar paciente'),
    ('frmGestionTurnos.btnRegistrarTurno', 'Registrar turno'),
    ('frmGestionTurnos.btnModificarTurno', 'Modificar turno');
INSERT INTO dbo.Permisos (Nombre, Descripcion, Formulario)
SELECT n.Nombre, n.Descripcion, 'frmGestionTurnos' FROM @nuevos n
WHERE NOT EXISTS (SELECT 1 FROM dbo.Permisos p WHERE p.Nombre = n.Nombre);
INSERT INTO dbo.Rol_Permiso (IdRol, IdPermisos)
SELECT @rolTurnos, p.IdPermisos FROM dbo.Permisos p
WHERE p.Formulario = 'frmGestionTurnos'
  AND NOT EXISTS (SELECT 1 FROM dbo.Rol_Permiso rp WHERE rp.IdRol = @rolTurnos AND rp.IdPermisos = p.IdPermisos);

-- Historial de cambios de turnos (T06b), para el Administrador
INSERT INTO dbo.Rol (Nombre, Descripcion, Formulario) VALUES ('Historial de Turnos', 'Formulario frmHistorialTurnos', 'frmHistorialTurnos');
DECLARE @rolHist int = SCOPE_IDENTITY();
INSERT INTO dbo.Permisos (Nombre, Descripcion, Formulario) VALUES
    ('frmHistorialTurnos.Ver', 'Ingresar a la pantalla', 'frmHistorialTurnos'),
    ('frmHistorialTurnos.btnRestaurar', 'Restaurar version de un turno', 'frmHistorialTurnos');
INSERT INTO dbo.Rol_Permiso (IdRol, IdPermisos)
SELECT @rolHist, IdPermisos FROM dbo.Permisos WHERE Formulario = 'frmHistorialTurnos';
INSERT INTO dbo.Rol_Rol (IdRolPadre, IdRolHijo) SELECT IdRol, @rolHist FROM dbo.Rol WHERE Nombre = 'Administrador';

COMMIT TRANSACTION;
GO

PRINT 'Migracion 01 aplicada.';
