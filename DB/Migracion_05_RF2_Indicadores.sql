/*
    Migracion 05 - RF2: indicadores de actividad en la liquidacion (2026-11-18)

    Cada renglon de la liquidacion (LiquidacionDetalle) guarda tambien los dias trabajados y la cantidad
    de turnos atendidos y ausentes de esa especialidad, para que el recibo (CU-02.4) los muestre y no
    cambie despues (RN2.7). Las liquidaciones que ya existen se completan con los turnos del periodo.

    Ejecutar con: sqlcmd -S localhost\SQLEXPRESS -E -C -b -I -f 65001 -d GestionTurnosMedicos -i Migracion_05_RF2_Indicadores.sql
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

BEGIN TRANSACTION;

IF COL_LENGTH('dbo.LiquidacionDetalle', 'DiasTrabajados') IS NULL
    ALTER TABLE dbo.LiquidacionDetalle ADD
        DiasTrabajados  int NOT NULL CONSTRAINT DF_LiquidacionDetalle_Dias DEFAULT 0
                        CONSTRAINT CK_LiquidacionDetalle_Dias CHECK (DiasTrabajados >= 0),
        TurnosAtendidos int NOT NULL CONSTRAINT DF_LiquidacionDetalle_Atendidos DEFAULT 0
                        CONSTRAINT CK_LiquidacionDetalle_Atendidos CHECK (TurnosAtendidos >= 0),
        TurnosAusentes  int NOT NULL CONSTRAINT DF_LiquidacionDetalle_Ausentes DEFAULT 0
                        CONSTRAINT CK_LiquidacionDetalle_Ausentes CHECK (TurnosAusentes >= 0);

COMMIT TRANSACTION;
GO

-- Completa las liquidaciones generadas antes de esta migracion (lote aparte: las columnas ya existen).
UPDATE d SET
    d.DiasTrabajados  = x.Dias,
    d.TurnosAtendidos = x.Atendidos,
    d.TurnosAusentes  = x.Ausentes
FROM dbo.LiquidacionDetalle d
INNER JOIN dbo.Liquidacion l ON l.IdLiquidacion = d.IdLiquidacion
CROSS APPLY (
    SELECT COUNT(DISTINCT t.Fecha) AS Dias,
           SUM(CASE WHEN t.Estado = 'Atendido' THEN 1 ELSE 0 END) AS Atendidos,
           SUM(CASE WHEN t.Estado = 'Ausente' THEN 1 ELSE 0 END) AS Ausentes
    FROM dbo.Turno t
    INNER JOIN dbo.Medico_Horario mh ON mh.IdHorario = t.IdHorario
    WHERE mh.IdMedico = l.IdMedico AND mh.IdEspecialidad = d.IdEspecialidad
      AND t.Estado IN ('Atendido', 'Ausente')
      AND MONTH(t.Fecha) = l.Mes AND YEAR(t.Fecha) = l.Anio
) x
WHERE d.DiasTrabajados = 0 AND d.TurnosAtendidos = 0 AND d.TurnosAusentes = 0;

PRINT 'Migracion 05 aplicada.';
