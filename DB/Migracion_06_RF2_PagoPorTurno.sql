/*
    Migracion 06 - RF2: honorarios por turno (2026-11-18, decision D15 de David)

    El medico cobra por turno: el atendido paga el valor del turno de la especialidad, el ausente un
    porcentaje de ese valor (por especialidad) y el cancelado nada. Las horas dejan de usarse.
      1) Especialidades: ValorHora pasa a ValorTurno y se agrega PorcentajeAusente (inicial 50).
      2) LiquidacionDetalle: se quita HorasTrabajadas; ValorHoraAplicado pasa a ValorTurnoAplicado y se
         agrega PorcentajeAusenteAplicado. Se borran las liquidaciones de prueba calculadas por hora.
      3) Rol, permisos y traducciones: frmValorHora pasa a frmValorTurno.

    Ejecutar con: sqlcmd -S localhost\SQLEXPRESS -E -C -b -I -f 65001 -d GestionTurnosMedicos -i Migracion_06_RF2_PagoPorTurno.sql
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

BEGIN TRANSACTION;

-- 1) Especialidades ------------------------------------------------------------------------------
IF COL_LENGTH('dbo.Especialidades', 'ValorHora') IS NOT NULL
BEGIN
    ALTER TABLE dbo.Especialidades DROP CONSTRAINT CK_Especialidades_ValorHora;
    EXEC sp_rename 'dbo.Especialidades.ValorHora', 'ValorTurno', 'COLUMN';
    EXEC sp_rename 'dbo.DF_Especialidades_ValorHora', 'DF_Especialidades_ValorTurno', 'OBJECT';
END
IF COL_LENGTH('dbo.Especialidades', 'PorcentajeAusente') IS NULL
    ALTER TABLE dbo.Especialidades ADD PorcentajeAusente decimal(5,2) NOT NULL
        CONSTRAINT DF_Especialidades_PorcentajeAusente DEFAULT 50
        CONSTRAINT CK_Especialidades_PorcentajeAusente CHECK (PorcentajeAusente BETWEEN 0 AND 100);

-- 2) Liquidaciones -------------------------------------------------------------------------------
DELETE FROM dbo.LiquidacionDetalle;
DELETE FROM dbo.Liquidacion;

IF COL_LENGTH('dbo.LiquidacionDetalle', 'HorasTrabajadas') IS NOT NULL
BEGIN
    ALTER TABLE dbo.LiquidacionDetalle DROP CONSTRAINT CK_LiquidacionDetalle_Horas;
    ALTER TABLE dbo.LiquidacionDetalle DROP COLUMN HorasTrabajadas;
END
IF COL_LENGTH('dbo.LiquidacionDetalle', 'ValorHoraAplicado') IS NOT NULL
BEGIN
    -- La restriccion CHECK depende de la columna: se quita, se renombra la columna y se vuelve a crear.
    ALTER TABLE dbo.LiquidacionDetalle DROP CONSTRAINT CK_LiquidacionDetalle_Valor;
    EXEC sp_rename 'dbo.LiquidacionDetalle.ValorHoraAplicado', 'ValorTurnoAplicado', 'COLUMN';
END
IF COL_LENGTH('dbo.LiquidacionDetalle', 'PorcentajeAusenteAplicado') IS NULL
    ALTER TABLE dbo.LiquidacionDetalle ADD PorcentajeAusenteAplicado decimal(5,2) NOT NULL
        CONSTRAINT DF_LiquidacionDetalle_Porcentaje DEFAULT 0
        CONSTRAINT CK_LiquidacionDetalle_Porcentaje CHECK (PorcentajeAusenteAplicado BETWEEN 0 AND 100);

COMMIT TRANSACTION;
GO

BEGIN TRANSACTION;

IF OBJECT_ID('dbo.CK_Especialidades_ValorTurno', 'C') IS NULL
    ALTER TABLE dbo.Especialidades ADD CONSTRAINT CK_Especialidades_ValorTurno CHECK (ValorTurno >= 0);
IF OBJECT_ID('dbo.CK_LiquidacionDetalle_Valor', 'C') IS NULL
    ALTER TABLE dbo.LiquidacionDetalle ADD CONSTRAINT CK_LiquidacionDetalle_Valor CHECK (ValorTurnoAplicado > 0);   -- RN2.4 y RN2.5

-- RN2.6: no se liquida un renglon sin turnos atendidos ni ausentes (lote aparte: columnas recien creadas).
IF OBJECT_ID('dbo.CK_LiquidacionDetalle_Turnos', 'C') IS NULL
    ALTER TABLE dbo.LiquidacionDetalle ADD CONSTRAINT CK_LiquidacionDetalle_Turnos
        CHECK (TurnosAtendidos + TurnosAusentes > 0);

-- 3) Rol, permisos y traducciones ------------------------------------------------------------------
UPDATE dbo.Rol SET Nombre = 'Valor del Turno', Descripcion = 'Formulario frmValorTurno', Formulario = 'frmValorTurno'
WHERE Formulario = 'frmValorHora';
UPDATE dbo.Permisos SET Formulario = 'frmValorTurno', Nombre = 'frmValorTurno.Ver' WHERE Nombre = 'frmValorHora.Ver';
UPDATE dbo.Permisos SET Formulario = 'frmValorTurno', Nombre = 'frmValorTurno.btnGuardarValorTurno', Descripcion = 'Guardar valor del turno'
WHERE Nombre = 'frmValorHora.btnGuardarValorHora';
UPDATE dbo.Permisos SET Nombre = 'frmLiquidaciones.btnConsultarTurnos', Descripcion = 'Consultar turnos'
WHERE Nombre = 'frmLiquidaciones.btnConsultarHoras';

DELETE FROM dbo.Traducciones
WHERE Clave LIKE 'frmValorHora.%' OR Clave LIKE 'Permisos.frmValorHora.%'
   OR Clave IN ('Formularios.frmValorHora', 'frmPrincipal.btn_ValorHora')
   OR Clave LIKE 'frmLiquidaciones.%';

DECLARE @textos TABLE (Clave varchar(150) PRIMARY KEY, ES nvarchar(400), EN nvarchar(400));
INSERT INTO @textos (Clave, ES, EN) VALUES
('frmPrincipal.btn_ValorTurno',                   N'Valor del turno',            N'Appointment fee'),
('Formularios.frmValorTurno',                     N'Valor del turno por especialidad', N'Appointment fee by specialty'),
('Permisos.frmValorTurno.Ver',                    N'Ingresar a la pantalla',     N'Open the screen'),
('Permisos.frmValorTurno.btnGuardarValorTurno',   N'Guardar valor del turno',    N'Save appointment fee');

UPDATE tr SET Traduccion = CASE i.Codigo WHEN 'ES' THEN t.ES ELSE t.EN END
FROM dbo.Traducciones tr
INNER JOIN dbo.Idiomas i ON i.Id = tr.IdIdioma
INNER JOIN @textos t ON t.Clave = tr.Clave
WHERE i.Codigo IN ('ES', 'EN');

INSERT INTO dbo.Traducciones (Clave, IdIdioma, Traduccion)
SELECT t.Clave, i.Id, CASE i.Codigo WHEN 'ES' THEN t.ES ELSE t.EN END
FROM @textos t CROSS JOIN dbo.Idiomas i
WHERE i.Codigo IN ('ES', 'EN')
  AND NOT EXISTS (SELECT 1 FROM dbo.Traducciones tr WHERE tr.Clave = t.Clave AND tr.IdIdioma = i.Id);

COMMIT TRANSACTION;
GO

PRINT 'Migracion 06 aplicada.';
