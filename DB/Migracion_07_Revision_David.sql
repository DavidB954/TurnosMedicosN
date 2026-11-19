/*
    Migracion 07 - Revision del programa por David (2026-11-18)

      1) RF1, CU-01.4: un horario de atencion se inactiva en una fecha determinada (por ejemplo, solo el
         lunes 20 de 10 a 12), no todas las semanas. Tabla Medico_HorarioInactivo: una fila por horario y
         fecha en que no se atiende.
      2) T06b: el recepcionista puede ver el historial de cambios de los turnos, pero solo el administrador
         restaura una version. Rol nuevo "Consulta de Historial de Turnos" (solo frmHistorialTurnos.Ver) para
         el recepcionista; el rol "Historial de Turnos" (ver y restaurar) queda solo para el administrador.
      (La liquidacion de un medico a la vez, CU-02.2, no cambia la base.)

    Repetible. Ejecutar con: sqlcmd -S localhost\SQLEXPRESS -E -C -b -I -f 65001 -d GestionTurnosMedicos -i Migracion_07_Revision_David.sql
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

BEGIN TRANSACTION;

-- 1) Horarios inactivos por fecha -------------------------------------------------------------------
IF OBJECT_ID('dbo.Medico_HorarioInactivo', 'U') IS NULL
    CREATE TABLE dbo.Medico_HorarioInactivo (
        IdHorario     int      NOT NULL CONSTRAINT FK_HorarioInactivo_Horario REFERENCES dbo.Medico_Horario (IdHorario),
        Fecha         date     NOT NULL,
        IdUsuario     int      NOT NULL CONSTRAINT FK_HorarioInactivo_Usuario REFERENCES dbo.Usuario (IdUsuario),
        FechaRegistro datetime NOT NULL CONSTRAINT DF_HorarioInactivo_FechaRegistro DEFAULT GETDATE(),
        CONSTRAINT PK_Medico_HorarioInactivo PRIMARY KEY (IdHorario, Fecha)
    );

-- 2) Historial de turnos: el recepcionista solo consulta ------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM dbo.Rol WHERE Nombre = 'Consulta de Historial de Turnos')
    INSERT INTO dbo.Rol (Nombre, Descripcion, Formulario)
    VALUES ('Consulta de Historial de Turnos', 'Ver el historial de cambios de los turnos (sin restaurar)', NULL);

DECLARE @consulta int = (SELECT IdRol FROM dbo.Rol WHERE Nombre = 'Consulta de Historial de Turnos');
DECLARE @historial int = (SELECT IdRol FROM dbo.Rol WHERE Nombre = 'Historial de Turnos');
DECLARE @recep int = (SELECT IdRol FROM dbo.Rol WHERE Nombre = 'Recepcionista');
DECLARE @ver int = (SELECT IdPermisos FROM dbo.Permisos WHERE Nombre = 'frmHistorialTurnos.Ver');

IF NOT EXISTS (SELECT 1 FROM dbo.Rol_Permiso WHERE IdRol = @consulta AND IdPermisos = @ver)
    INSERT INTO dbo.Rol_Permiso (IdRol, IdPermisos) VALUES (@consulta, @ver);

DELETE FROM dbo.Rol_Rol WHERE IdRolPadre = @recep AND IdRolHijo = @historial;
IF NOT EXISTS (SELECT 1 FROM dbo.Rol_Rol WHERE IdRolPadre = @recep AND IdRolHijo = @consulta)
    INSERT INTO dbo.Rol_Rol (IdRolPadre, IdRolHijo) VALUES (@recep, @consulta);

COMMIT TRANSACTION;
GO

PRINT 'Migracion 07 aplicada.';
