/*
    Script de actualización — Historial de cambios por transacción
    Base: GestionTurnosMedicos

    Antes, cada campo modificado de un usuario generaba una fila independiente en
    HistorialCambios (modificar Nombre y Apellido = 2 filas sueltas). Ahora todos
    los campos de una misma edición se insertan como UNA transacción: comparten
    un IdTransaccion (GUID) y la misma FechaCambio, y la grilla de historial
    muestra una sola fila por edición, restaurable completa.

    Este script agrega la columna IdTransaccion. Es idempotente: se puede
    ejecutar más de una vez sin error.
*/

USE GestionTurnosMedicos;
GO

-- 1) Columna de agrupación de transacción.
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.HistorialCambios') AND name = 'IdTransaccion'
)
BEGIN
    ALTER TABLE dbo.HistorialCambios
        ADD IdTransaccion UNIQUEIDENTIFIER NULL;
END
GO

-- 2) Backfill: las filas históricas previas a este cambio no tienen grupo;
--    cada una pasa a ser su propia transacción (no se puede reconstruir qué
--    filas viejas pertenecían a una misma edición).
UPDATE dbo.HistorialCambios
SET IdTransaccion = NEWID()
WHERE IdTransaccion IS NULL;
GO

-- Verificación rápida:
SELECT COUNT(*) AS filas_sin_transaccion
FROM dbo.HistorialCambios
WHERE IdTransaccion IS NULL;  -- debe dar 0
GO
