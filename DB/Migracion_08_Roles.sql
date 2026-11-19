/*
    Migracion 08 - Roles (revision de David, 2026-11-18)

      1) "Consulta de Historial de Turnos" (migracion 07) no era un perfil: aparecia en la lista de roles de Gestion de
         Roles. Se elimina y el Recepcionista recibe directamente el permiso frmHistorialTurnos.Ver (patente suelta del
         Composite): ve el historial pero no restaura.
      2) Descripcion de cada rol (en Gestion de Roles se muestra de solo lectura). La columna es varchar(100).

    Repetible. Ejecutar con: sqlcmd -S localhost\SQLEXPRESS -E -C -b -I -f 65001 -d GestionTurnosMedicos -i Migracion_08_Roles.sql
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;

DECLARE @recep int = (SELECT IdRol FROM dbo.Rol WHERE Nombre = 'Recepcionista');
DECLARE @ver int = (SELECT IdPermisos FROM dbo.Permisos WHERE Nombre = 'frmHistorialTurnos.Ver');
DECLARE @consulta int = (SELECT IdRol FROM dbo.Rol WHERE Nombre = 'Consulta de Historial de Turnos');

IF @consulta IS NOT NULL
BEGIN
    DELETE FROM dbo.Usuario_Rol WHERE IdRol = @consulta;
    DELETE FROM dbo.Rol_Rol WHERE IdRolPadre = @consulta OR IdRolHijo = @consulta;
    DELETE FROM dbo.Rol_Permiso WHERE IdRol = @consulta;
    DELETE FROM dbo.Rol WHERE IdRol = @consulta;
END
IF NOT EXISTS (SELECT 1 FROM dbo.Rol_Permiso WHERE IdRol = @recep AND IdPermisos = @ver)
    INSERT INTO dbo.Rol_Permiso (IdRol, IdPermisos) VALUES (@recep, @ver);

UPDATE dbo.Rol SET Descripcion = N'Recepción: pacientes, turnos, agenda médica y consulta del historial de turnos.'
WHERE Nombre = 'Recepcionista';
UPDATE dbo.Rol SET Descripcion = N'Médico: su agenda, historia clínica, atención, recetas y ausencias.'
WHERE Nombre = 'Medico';
UPDATE dbo.Rol SET Descripcion = N'Administrador del sistema: usuarios, roles, seguridad, idiomas, bitácora y restauración de turnos.'
WHERE Nombre = 'Administrador';
UPDATE dbo.Rol SET Descripcion = N'Contador: valor del turno por especialidad, liquidaciones de honorarios y recibos.'
WHERE Nombre = 'Contador';

COMMIT TRANSACTION;
GO

PRINT 'Migracion 08 aplicada.';
