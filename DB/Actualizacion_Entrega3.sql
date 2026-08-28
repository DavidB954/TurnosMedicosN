/*
    Script de actualización — Entrega 3
    Base: GestionTurnosMedicos

    IMPORTANTE: Los nuevos valores del enum AccionBitacora (LOGOUT, USUARIO_RESTAURACION,
    INTEGRIDAD_ERROR, BACKUP_GENERADO, BACKUP_RESTAURADO, ROL_ALTA, ROL_BAJA,
    ROL_ASIGNADO_USUARIO, ROL_QUITADO_USUARIO, PERMISO_ALTA, PERMISO_BAJA, IDIOMA_ALTA,
    IDIOMA_BAJA) NO requieren cambios de esquema: Bitacora.Accion es VARCHAR(50) sin
    CHECK constraint, y Bitacora.Modulo es VARCHAR(30) sin CHECK constraint. Este script
    solo agrega integridad referencial/unicidad que faltaba, detectada al revisar el
    esquema en vivo. Es idempotente: se puede ejecutar más de una vez sin error.
*/

USE GestionTurnosMedicos;
GO

-- 1) Un solo DVV por tabla auditada (evita filas duplicadas para el mismo NombreTabla,
--    que romperían ObtenerDVV/ActualizarDVV si algún día llegaran a existir dos filas
--    "Usuario"). Verificado: hoy no hay duplicados.
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'UQ_DVV_NombreTabla' AND object_id = OBJECT_ID('dbo.DVV')
)
BEGIN
    ALTER TABLE dbo.DVV
        ADD CONSTRAINT UQ_DVV_NombreTabla UNIQUE (NombreTabla);
END
GO

-- 2) No permitir roles con el mismo nombre. BLL_Roles ya tenía la validación
--    ExisteNombreRol escrita en DAL pero nunca invocada; esto la refuerza también
--    a nivel de base, que es donde realmente debe vivir la garantía de unicidad.
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'UQ_Rol_Nombre' AND object_id = OBJECT_ID('dbo.Rol')
)
BEGIN
    ALTER TABLE dbo.Rol
        ADD CONSTRAINT UQ_Rol_Nombre UNIQUE (Nombre);
END
GO

-- 3) Ídem para permisos.
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'UQ_Permisos_Nombre' AND object_id = OBJECT_ID('dbo.Permisos')
)
BEGIN
    ALTER TABLE dbo.Permisos
        ADD CONSTRAINT UQ_Permisos_Nombre UNIQUE (Nombre);
END
GO

-- 4) FiltrarBitacora ahora también devuelve el nombre y apellido del usuario
--    (o 'SISTEMA' si el evento no tiene usuario asociado, ej. falla de integridad
--    detectada antes del login), para que la grilla de Bitácora no muestre el IdUsuario
--    crudo ni columnas vacías en los eventos de sistema.
GO
ALTER PROCEDURE [dbo].[FiltrarBitacora]
    @Desde DATETIME = NULL,
    @Hasta DATETIME = NULL,
    @IdUsuario INT = NULL,
    @Modulo VARCHAR(50) = NULL,
    @IP VARCHAR(100) = NULL
AS
BEGIN
    SELECT
        b.IdBitacora,
        b.IdUsuario,
        b.FechaHora,
        b.Accion,
        b.Modulo,
        b.DireccionIP AS IP,
        b.NombreMaquina,
        b.Descripcion,
        ISNULL(u.Nombre + ' ' + u.Apellido, 'SISTEMA') AS NombreUsuario
    FROM Bitacora b
    LEFT JOIN Usuario u ON b.IdUsuario = u.IdUsuario
    WHERE (@Desde IS NULL OR b.FechaHora >= @Desde)
      AND (@Hasta IS NULL OR b.FechaHora <= @Hasta)
      AND (@IdUsuario IS NULL OR b.IdUsuario = @IdUsuario)
      AND (@Modulo IS NULL OR b.Modulo = @Modulo)
      AND (@IP IS NULL OR b.DireccionIP LIKE '%' + @IP + '%')
    ORDER BY b.FechaHora DESC
END
GO

/*
    NOTA (no aplicado por este script, a propósito):
    HistorialCambios.IdUsuario no tiene FK hacia Usuario. BLL_HistorialCambios.RegistrarCambio
    usa "idUsuarioEditor ?? 0" como valor centinela cuando no hay usuario en sesión, y hoy no
    existe una fila de Usuario con IdUsuario = 0. Agregar la FK rompería esos inserts. Si en
    la Entrega Final se decide modelar un "usuario sistema", crear esa fila primero y recién
    ahí agregar la FK.
*/

-- Verificación rápida de que las constraints quedaron aplicadas:
SELECT name AS constraint_agregado, OBJECT_NAME(parent_object_id) AS tabla
FROM sys.indexes
WHERE name IN ('UQ_DVV_NombreTabla', 'UQ_Rol_Nombre', 'UQ_Permisos_Nombre');
GO
