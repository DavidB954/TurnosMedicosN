/*
    Script de actualizacion - Traducciones de la pantalla frm_BitacoraTurno y del boton
    nuevo en frmPrincipal (btn_BitacoraTurno).
    Base: GestionTurnosMedicos

    Sigue el mismo mecanismo ya usado por frmABM (Gestion de Idiomas -> "Cargar Controles"):
    la clave de cada control es "NombreForm.NombreControl" (o "NombreForm.NombreGrilla.NombreColumna"
    para columnas de DataGridView), y cada idioma existente en Idiomas necesita su propia fila
    en Traducciones para ese control.

    Paso 1: crea, para CUALQUIER idioma ya registrado (no solo ES/EN), una fila base con el
    mismo texto en Español que ya tiene el control (idéntico a lo que hace
    frmABM.btnCargarControles_Click), por si en el futuro se agrega un tercer idioma antes
    de traducirlo.
    Paso 2: sobrescribe puntualmente la fila del idioma EN con la traduccion real al ingles
    (siguiendo el vocabulario ya usado en el resto de la app: Turno=Appointment,
    Medico=Doctor, Bitacora=Log).

    Es idempotente: se puede ejecutar mas de una vez sin error (usa MERGE).
*/

USE GestionTurnosMedicos;
GO

DECLARE @Claves TABLE (Clave VARCHAR(200) PRIMARY KEY, TextoBase NVARCHAR(255), TextoEn NVARCHAR(255));

INSERT INTO @Claves (Clave, TextoBase, TextoEn) VALUES
    ('frm_BitacoraTurno.lblTitulo',                    N'BITACORA DE EVENTOS DE TURNO (SQL TRIGGERS)', N'APPOINTMENT EVENT LOG (SQL TRIGGERS)'),
    ('frm_BitacoraTurno.groupBoxFiltros',               N'Filtros de Busqueda',      N'Search Filters'),
    ('frm_BitacoraTurno.btnLimpiarFiltros',             N'Limpiar Filtros',          N'Clear Filters'),
    ('frm_BitacoraTurno.btnBuscar',                     N'Buscar',                   N'Search'),
    ('frm_BitacoraTurno.lblDesde',                      N'Fecha desde:',             N'From date:'),
    ('frm_BitacoraTurno.lblHasta',                      N'Fecha hasta:',             N'To date:'),
    ('frm_BitacoraTurno.lblIdTurno',                    N'N° Turno:',                N'Appointment #:'),
    ('frm_BitacoraTurno.btnActualizar',                 N'Actualizar',               N'Refresh'),
    ('frm_BitacoraTurno.lblTotalDeRegistros',           N'Total registros:',         N'Total records:'),
    ('frm_BitacoraTurno.dgvEventos.colFechaHora',       N'Fecha y Hora',             N'Date and Time'),
    ('frm_BitacoraTurno.dgvEventos.colTipoEvento',      N'Evento',                   N'Event'),
    ('frm_BitacoraTurno.dgvEventos.colEstadoAnterior',  N'Estado Anterior',          N'Previous Status'),
    ('frm_BitacoraTurno.dgvEventos.colEstadoNuevo',     N'Estado Nuevo',             N'New Status'),
    ('frm_BitacoraTurno.dgvEventos.colPaciente',        N'Paciente',                 N'Patient'),
    ('frm_BitacoraTurno.dgvEventos.colMedico',          N'Medico',                   N'Doctor'),
    ('frm_BitacoraTurno.dgvEventos.colFechaTurno',      N'Fecha Turno',              N'Appointment Date'),
    ('frm_BitacoraTurno.dgvEventos.colHorario',         N'Horario',                  N'Time Slot'),
    ('frm_BitacoraTurno.dgvEventos.colIdTurno',         N'Turno',                    N'Appointment'),
    ('frm_BitacoraTurno.dgvEventos.colUsuarioBD',       N'Usuario SQL',              N'SQL User'),
    ('frm_BitacoraTurno.dgvEventos.colHostBD',          N'Equipo',                   N'Host'),
    ('frmPrincipal.btn_BitacoraTurno',                  N'Bitacora de Turnos',       N'Appointment Log');

-- Paso 1: fila base (texto en Español) para cada idioma ya registrado que todavia no tenga
-- esa clave (cubre ES y cualquier idioma futuro agregado antes de traducirlo).
MERGE dbo.Traducciones AS destino
USING (
    SELECT c.Clave, i.Id AS IdIdioma, c.TextoBase
    FROM @Claves c
    CROSS JOIN dbo.Idiomas i
) AS origen
ON destino.Clave = origen.Clave AND destino.IdIdioma = origen.IdIdioma
WHEN NOT MATCHED THEN
    INSERT (Clave, IdIdioma, Traduccion) VALUES (origen.Clave, origen.IdIdioma, origen.TextoBase);
GO

DECLARE @Claves2 TABLE (Clave VARCHAR(200) PRIMARY KEY, TextoBase NVARCHAR(255), TextoEn NVARCHAR(255));

INSERT INTO @Claves2 (Clave, TextoBase, TextoEn) VALUES
    ('frm_BitacoraTurno.lblTitulo',                    N'BITACORA DE EVENTOS DE TURNO (SQL TRIGGERS)', N'APPOINTMENT EVENT LOG (SQL TRIGGERS)'),
    ('frm_BitacoraTurno.groupBoxFiltros',               N'Filtros de Busqueda',      N'Search Filters'),
    ('frm_BitacoraTurno.btnLimpiarFiltros',             N'Limpiar Filtros',          N'Clear Filters'),
    ('frm_BitacoraTurno.btnBuscar',                     N'Buscar',                   N'Search'),
    ('frm_BitacoraTurno.lblDesde',                      N'Fecha desde:',             N'From date:'),
    ('frm_BitacoraTurno.lblHasta',                      N'Fecha hasta:',             N'To date:'),
    ('frm_BitacoraTurno.lblIdTurno',                    N'N° Turno:',                N'Appointment #:'),
    ('frm_BitacoraTurno.btnActualizar',                 N'Actualizar',               N'Refresh'),
    ('frm_BitacoraTurno.lblTotalDeRegistros',           N'Total registros:',         N'Total records:'),
    ('frm_BitacoraTurno.dgvEventos.colFechaHora',       N'Fecha y Hora',             N'Date and Time'),
    ('frm_BitacoraTurno.dgvEventos.colTipoEvento',      N'Evento',                   N'Event'),
    ('frm_BitacoraTurno.dgvEventos.colEstadoAnterior',  N'Estado Anterior',          N'Previous Status'),
    ('frm_BitacoraTurno.dgvEventos.colEstadoNuevo',     N'Estado Nuevo',             N'New Status'),
    ('frm_BitacoraTurno.dgvEventos.colPaciente',        N'Paciente',                 N'Patient'),
    ('frm_BitacoraTurno.dgvEventos.colMedico',          N'Medico',                   N'Doctor'),
    ('frm_BitacoraTurno.dgvEventos.colFechaTurno',      N'Fecha Turno',              N'Appointment Date'),
    ('frm_BitacoraTurno.dgvEventos.colHorario',         N'Horario',                  N'Time Slot'),
    ('frm_BitacoraTurno.dgvEventos.colIdTurno',         N'Turno',                    N'Appointment'),
    ('frm_BitacoraTurno.dgvEventos.colUsuarioBD',       N'Usuario SQL',              N'SQL User'),
    ('frm_BitacoraTurno.dgvEventos.colHostBD',          N'Equipo',                   N'Host'),
    ('frmPrincipal.btn_BitacoraTurno',                  N'Bitacora de Turnos',       N'Appointment Log');

-- Paso 2: traduccion real al ingles, sobrescribiendo la fila EN (o insertandola si por algun
-- motivo el Paso 1 no corrio, ej. no existia el idioma EN al momento de ejecutar este script).
MERGE dbo.Traducciones AS destino
USING (
    SELECT c.Clave, i.Id AS IdIdioma, c.TextoEn
    FROM @Claves2 c
    INNER JOIN dbo.Idiomas i ON i.Codigo = 'EN'
) AS origen
ON destino.Clave = origen.Clave AND destino.IdIdioma = origen.IdIdioma
WHEN MATCHED THEN
    UPDATE SET Traduccion = origen.TextoEn
WHEN NOT MATCHED THEN
    INSERT (Clave, IdIdioma, Traduccion) VALUES (origen.Clave, origen.IdIdioma, origen.TextoEn);
GO
