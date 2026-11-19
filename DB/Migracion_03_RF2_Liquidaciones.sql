/*
    Migracion 03 - RF2 Liquidacion de honorarios medicos (2026-11-18)

    1) Especialidades.ValorHora (CU-02.1, RN2.4).
    2) Tablas Liquidacion y LiquidacionDetalle (CU-02.3, RN2.3 y RN2.5). TotalHoras, ImporteTotal y
       Subtotal no se guardan: se calculan en BE (3FN).
    3) Roles "Valor Hora" y "Liquidacion de Honorarios" (hijos de Contador y de Administrador) con sus permisos.
    4) Traducciones ES/EN de las pantallas nuevas, del menu y de los permisos.

    Ejecutar con: sqlcmd -S localhost\SQLEXPRESS -E -C -b -I -f 65001 -d GestionTurnosMedicos -i Migracion_03_RF2_Liquidaciones.sql
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

-- Numero de liquidacion (lo genera la base, como el N° de Historia Clinica)
IF OBJECT_ID('dbo.SEQ_NroLiquidacion', 'SO') IS NULL
    CREATE SEQUENCE dbo.SEQ_NroLiquidacion AS int START WITH 1 INCREMENT BY 1;
GO

BEGIN TRANSACTION;

-- 1) Valor hora por especialidad ----------------------------------------------------------------
IF COL_LENGTH('dbo.Especialidades', 'ValorHora') IS NULL
    ALTER TABLE dbo.Especialidades ADD ValorHora decimal(12,2) NOT NULL
        CONSTRAINT DF_Especialidades_ValorHora DEFAULT 0
        CONSTRAINT CK_Especialidades_ValorHora CHECK (ValorHora >= 0);

-- 2) Liquidaciones ------------------------------------------------------------------------------
IF OBJECT_ID('dbo.Liquidacion', 'U') IS NULL
    CREATE TABLE dbo.Liquidacion (
        IdLiquidacion  int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Liquidacion PRIMARY KEY,
        NroLiquidacion int NOT NULL CONSTRAINT DF_Liquidacion_Nro DEFAULT (NEXT VALUE FOR dbo.SEQ_NroLiquidacion)
                       CONSTRAINT UQ_Liquidacion_Nro UNIQUE,
        IdMedico       int NOT NULL CONSTRAINT FK_Liquidacion_Medico REFERENCES dbo.Medico (IdUsuario),
        Mes            tinyint NOT NULL CONSTRAINT CK_Liquidacion_Mes CHECK (Mes BETWEEN 1 AND 12),
        Anio           smallint NOT NULL CONSTRAINT CK_Liquidacion_Anio CHECK (Anio BETWEEN 2000 AND 2100),
        FechaEmision   datetime NOT NULL CONSTRAINT DF_Liquidacion_FechaEmision DEFAULT (GETDATE()),
        CONSTRAINT UQ_Liquidacion_MedicoPeriodo UNIQUE (IdMedico, Mes, Anio)          -- RN2.3
    );

IF OBJECT_ID('dbo.LiquidacionDetalle', 'U') IS NULL
    CREATE TABLE dbo.LiquidacionDetalle (
        IdDetalle         int IDENTITY(1,1) NOT NULL CONSTRAINT PK_LiquidacionDetalle PRIMARY KEY,
        IdLiquidacion     int NOT NULL CONSTRAINT FK_LiquidacionDetalle_Liquidacion REFERENCES dbo.Liquidacion (IdLiquidacion),
        IdEspecialidad    int NOT NULL CONSTRAINT FK_LiquidacionDetalle_Especialidad REFERENCES dbo.Especialidades (IdEspecialidad),
        HorasTrabajadas   decimal(8,2) NOT NULL CONSTRAINT CK_LiquidacionDetalle_Horas CHECK (HorasTrabajadas > 0),
        ValorHoraAplicado decimal(12,2) NOT NULL CONSTRAINT CK_LiquidacionDetalle_Valor CHECK (ValorHoraAplicado > 0),  -- RN2.4 y RN2.5
        CONSTRAINT UQ_LiquidacionDetalle_Especialidad UNIQUE (IdLiquidacion, IdEspecialidad)
    );

-- 3) Roles y permisos del Contador -------------------------------------------------------------
DECLARE @formularios TABLE (Rol varchar(50), Formulario varchar(50));
INSERT INTO @formularios VALUES ('Valor Hora', 'frmValorHora'), ('Liquidacion de Honorarios', 'frmLiquidaciones');

INSERT INTO dbo.Rol (Nombre, Descripcion, Formulario)
SELECT f.Rol, 'Formulario ' + f.Formulario, f.Formulario FROM @formularios f
WHERE NOT EXISTS (SELECT 1 FROM dbo.Rol r WHERE r.Formulario = f.Formulario);

DECLARE @permisos TABLE (Nombre varchar(50), Descripcion varchar(100), Formulario varchar(50));
INSERT INTO @permisos VALUES
    ('frmValorHora.Ver', 'Ingresar a la pantalla', 'frmValorHora'),
    ('frmValorHora.btnGuardarValorHora', 'Guardar valor hora', 'frmValorHora'),
    ('frmLiquidaciones.Ver', 'Ingresar a la pantalla', 'frmLiquidaciones'),
    ('frmLiquidaciones.btnGenerarLiquidacion', 'Generar liquidaciones', 'frmLiquidaciones'),
    ('frmLiquidaciones.btnEmitirRecibo', 'Emitir recibo', 'frmLiquidaciones');

INSERT INTO dbo.Permisos (Nombre, Descripcion, Formulario)
SELECT p.Nombre, p.Descripcion, p.Formulario FROM @permisos p
WHERE NOT EXISTS (SELECT 1 FROM dbo.Permisos x WHERE x.Nombre = p.Nombre);

INSERT INTO dbo.Rol_Permiso (IdRol, IdPermisos)
SELECT r.IdRol, p.IdPermisos
FROM dbo.Rol r INNER JOIN dbo.Permisos p ON p.Formulario = r.Formulario
WHERE r.Formulario IN ('frmValorHora', 'frmLiquidaciones')
  AND NOT EXISTS (SELECT 1 FROM dbo.Rol_Permiso rp WHERE rp.IdRol = r.IdRol AND rp.IdPermisos = p.IdPermisos);

INSERT INTO dbo.Rol_Rol (IdRolPadre, IdRolHijo)
SELECT padre.IdRol, hijo.IdRol
FROM dbo.Rol padre CROSS JOIN dbo.Rol hijo
WHERE padre.Nombre IN ('Contador', 'Administrador')
  AND hijo.Formulario IN ('frmValorHora', 'frmLiquidaciones')
  AND NOT EXISTS (SELECT 1 FROM dbo.Rol_Rol rr WHERE rr.IdRolPadre = padre.IdRol AND rr.IdRolHijo = hijo.IdRol);

-- 4) Traducciones ------------------------------------------------------------------------------
DECLARE @textos TABLE (Clave varchar(150) PRIMARY KEY, ES nvarchar(400), EN nvarchar(400));
INSERT INTO @textos (Clave, ES, EN) VALUES
('frmPrincipal.btn_ValorHora',                      N'Valor hora',               N'Hourly rate'),
('frmPrincipal.btn_Liquidaciones',                  N'Liquidación de honorarios', N'Fee settlement'),
('Formularios.frmValorHora',                        N'Valor hora por especialidad', N'Hourly rate by specialty'),
('Formularios.frmLiquidaciones',                    N'Liquidación de honorarios', N'Fee settlement'),
('Permisos.frmValorHora.Ver',                       N'Ingresar a la pantalla',   N'Open the screen'),
('Permisos.frmValorHora.btnGuardarValorHora',       N'Guardar valor hora',       N'Save hourly rate'),
('Permisos.frmLiquidaciones.Ver',                   N'Ingresar a la pantalla',   N'Open the screen'),
('Permisos.frmLiquidaciones.btnGenerarLiquidacion', N'Generar liquidaciones',    N'Generate settlements'),
('Permisos.frmLiquidaciones.btnEmitirRecibo',       N'Emitir recibo',            N'Issue receipt'),
-- frmValorHora
('frmValorHora.lblEspecialidades',                  N'Especialidades',           N'Specialties'),
('frmValorHora.groupBox_ValorHora',                 N'Actualizar valor hora',    N'Update hourly rate'),
('frmValorHora.lblEspecialidad',                    N'Especialidad:',            N'Specialty:'),
('frmValorHora.lblValorHora',                       N'Valor hora ($):',          N'Hourly rate ($):'),
('frmValorHora.btnGuardarValorHora',                N'Guardar valor hora',       N'Save hourly rate'),
('frmValorHora.dgvEspecialidades.colNombre',        N'Especialidad',             N'Specialty'),
('frmValorHora.dgvEspecialidades.colValorHora',     N'Valor hora',               N'Hourly rate'),
-- frmLiquidaciones
('frmLiquidaciones.groupBox_Periodo',               N'Período',                  N'Period'),
('frmLiquidaciones.lblMes',                         N'Mes:',                     N'Month:'),
('frmLiquidaciones.lblAnio',                        N'Año:',                     N'Year:'),
('frmLiquidaciones.btnConsultarHoras',              N'Consultar horas',          N'Check hours'),
('frmLiquidaciones.lblHoras',                       N'Horas trabajadas del período', N'Hours worked in the period'),
('frmLiquidaciones.btnGenerarLiquidacion',          N'Generar liquidaciones',    N'Generate settlements'),
('frmLiquidaciones.lblLiquidaciones',               N'Liquidaciones del período', N'Settlements of the period'),
('frmLiquidaciones.btnEmitirRecibo',                N'Emitir recibo',            N'Issue receipt'),
('frmLiquidaciones.dgvHoras.colMedico',             N'Médico',                   N'Doctor'),
('frmLiquidaciones.dgvHoras.colMatricula',          N'Matrícula',                N'License #'),
('frmLiquidaciones.dgvHoras.colEspecialidad',       N'Especialidad',             N'Specialty'),
('frmLiquidaciones.dgvHoras.colHoras',              N'Horas',                    N'Hours'),
('frmLiquidaciones.dgvHoras.colValorHora',          N'Valor hora',               N'Hourly rate'),
('frmLiquidaciones.dgvHoras.colImporteEstimado',    N'Importe estimado',         N'Estimated amount'),
('frmLiquidaciones.dgvLiquidaciones.colNro',        N'Nro.',                     N'No.'),
('frmLiquidaciones.dgvLiquidaciones.colMedico',     N'Médico',                   N'Doctor'),
('frmLiquidaciones.dgvLiquidaciones.colPeriodo',    N'Período',                  N'Period'),
('frmLiquidaciones.dgvLiquidaciones.colTotalHoras', N'Total de horas',           N'Total hours'),
('frmLiquidaciones.dgvLiquidaciones.colImporteTotal', N'Importe total',          N'Total amount'),
('frmLiquidaciones.dgvLiquidaciones.colFechaEmision', N'Fecha de emisión',       N'Issue date');

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

PRINT 'Migracion 03 aplicada.';
