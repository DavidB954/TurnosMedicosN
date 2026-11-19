/*
    Migracion 02 - RF1: traducciones de las pantallas de turnos (2026-11-17)

    1) Borra las traducciones de las pantallas eliminadas (acceso del paciente, mis recetas,
       bitacora de eventos de turno) y las de la version anterior de frmGestionTurnos.
    2) Carga en espanol e ingles los textos de frmGestionTurnos (CU-01.1 a CU-01.3),
       frmHistorialTurnos (T06b), el menu y los nombres de formularios y permisos.
    3) El menu del medico pasa a llamarse "Mi agenda" (seccion 17.7).

    Ejecutar con: sqlcmd -S localhost\SQLEXPRESS -E -C -b -I -f 65001 -d GestionTurnosMedicos -i Migracion_02_RF1_Traducciones.sql
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

-- 1) Traducciones que ya no corresponden ------------------------------------------------------
DELETE FROM dbo.Traducciones
WHERE Clave LIKE 'frm[_]AccesoPaciente.%'
   OR Clave LIKE 'frm[_]BitacoraTurno.%'
   OR Clave LIKE 'frmMisRecetas.%'
   OR Clave LIKE 'frmMiReceta.%'
   OR Clave LIKE 'frmGestionTurnos.%'
   OR Clave LIKE 'Permisos.frm[_]AccesoPaciente.%'
   OR Clave IN ('frmPrincipal.btn_AccesoPaciente', 'frmPrincipal.btn_MisRecetas', 'frmPrincipal.btn_BitacoraTurno',
                'Formularios.frm_AccesoPaciente', 'Formularios.frm_BitacoraTurno', 'Formularios.frmMisRecetas');

-- 2) Textos vigentes ---------------------------------------------------------------------------
DECLARE @textos TABLE (Clave varchar(150) PRIMARY KEY, ES nvarchar(400), EN nvarchar(400));
INSERT INTO @textos (Clave, ES, EN) VALUES
-- Menu
('frmPrincipal.btn_AccesoMedico',                   N'Mi agenda',                N'My schedule'),
('frmPrincipal.btn_HistorialTurnos',                N'Historial de Turnos',      N'Appointment History'),
('Formularios.frm_AccesoMedico',                    N'Mi agenda',                N'My schedule'),
('Formularios.frmGestionTurnos',                    N'Gestión de Turnos',        N'Appointment Management'),
('Formularios.frmHistorialTurnos',                  N'Historial de cambios de turnos', N'Appointment change history'),
-- Permisos
('Permisos.frmGestionTurnos.Ver',                   N'Ingresar a la pantalla',   N'Open the screen'),
('Permisos.frmGestionTurnos.btnRegistrarPaciente',  N'Registrar paciente',       N'Register patient'),
('Permisos.frmGestionTurnos.btnRegistrarTurno',     N'Registrar turno',          N'Book appointment'),
('Permisos.frmGestionTurnos.btnModificarTurno',     N'Modificar turno',          N'Change appointment'),
('Permisos.frmGestionTurnos.btnCancelarTurno',      N'Cancelar turno',           N'Cancel appointment'),
('Permisos.frmHistorialTurnos.Ver',                 N'Ingresar a la pantalla',   N'Open the screen'),
('Permisos.frmHistorialTurnos.btnRestaurar',        N'Restaurar versión',        N'Restore version'),
-- frmGestionTurnos
('frmGestionTurnos.groupBox_Paciente',              N'Paciente',                 N'Patient'),
('frmGestionTurnos.lblDNI',                         N'DNI:',                     N'ID number:'),
('frmGestionTurnos.btnBuscarPaciente',              N'Buscar',                   N'Search'),
('frmGestionTurnos.lblHC',                          N'Historia clínica N°:',     N'Medical record #:'),
('frmGestionTurnos.lblNombre',                      N'Nombre:',                  N'First name:'),
('frmGestionTurnos.lblApellido',                    N'Apellido:',                N'Last name:'),
('frmGestionTurnos.lblFechaNacimiento',             N'Fecha de nac.:',           N'Date of birth:'),
('frmGestionTurnos.lblTelefono',                    N'Teléfono:',                N'Phone:'),
('frmGestionTurnos.lblEmail',                       N'Email:',                   N'Email:'),
('frmGestionTurnos.btnRegistrarPaciente',           N'Registrar paciente',       N'Register patient'),
('frmGestionTurnos.groupBox_NuevoTurno',            N'Nuevo turno',              N'New appointment'),
('frmGestionTurnos.lblFecha',                       N'Fecha:',                   N'Date:'),
('frmGestionTurnos.lblHorarios',                    N'Horarios disponibles:',    N'Available time slots:'),
('frmGestionTurnos.btnRegistrarTurno',              N'Registrar turno',          N'Book appointment'),
('frmGestionTurnos.groupBox_TurnosPaciente',        N'Turnos del paciente',      N'Patient appointments'),
('frmGestionTurnos.btnModificarTurno',              N'Modificar turno',          N'Change appointment'),
('frmGestionTurnos.btnCancelarTurno',               N'Cancelar turno',           N'Cancel appointment'),
('frmGestionTurnos.lblAyudaModificar',              N'Para modificar: seleccione el turno, elija el nuevo horario en Nuevo turno y presione Modificar turno.',
                                                    N'To change: select the appointment, choose the new time slot under New appointment and press Change appointment.'),
('frmGestionTurnos.dgvTurnosPaciente.colIdTurno',   N'Turno',                    N'Appointment'),
('frmGestionTurnos.dgvTurnosPaciente.colFecha',     N'Fecha',                    N'Date'),
('frmGestionTurnos.dgvTurnosPaciente.colHorario',   N'Horario',                  N'Time'),
('frmGestionTurnos.dgvTurnosPaciente.colEspecialidad', N'Especialidad',          N'Specialty'),
('frmGestionTurnos.dgvTurnosPaciente.colMedico',    N'Médico',                   N'Doctor'),
('frmGestionTurnos.dgvTurnosPaciente.colEstado',    N'Estado',                   N'Status'),
-- frmHistorialTurnos
('frmHistorialTurnos.groupBoxFiltros',              N'Buscar turnos',            N'Search appointments'),
('frmHistorialTurnos.lblIdTurno',                   N'N° turno:',                N'Appointment #:'),
('frmHistorialTurnos.lblPaciente',                  N'Paciente (nombre o DNI):', N'Patient (name or ID):'),
('frmHistorialTurnos.lblDesde',                     N'Desde:',                   N'From:'),
('frmHistorialTurnos.lblHasta',                     N'Hasta:',                   N'To:'),
('frmHistorialTurnos.lblEstado',                    N'Estado:',                  N'Status:'),
('frmHistorialTurnos.btnBuscar',                    N'Buscar',                   N'Search'),
('frmHistorialTurnos.btnLimpiarFiltros',            N'Limpiar filtros',          N'Clear filters'),
('frmHistorialTurnos.lblTurnos',                    N'Turnos',                   N'Appointments'),
('frmHistorialTurnos.lblVersiones',                 N'Versiones del turno',      N'Appointment versions'),
('frmHistorialTurnos.btnRestaurar',                 N'Restaurar versión',        N'Restore version'),
('frmHistorialTurnos.lblAyudaRestaurar',            N'Cada fila es una versión del turno, de la más reciente a la más antigua. Seleccione una versión anterior y presione Restaurar versión para volver el turno a esos datos.',
                                                    N'Each row is a version of the appointment, newest first. Select an earlier version and press Restore version to return the appointment to that data.'),
('frmHistorialTurnos.Estado.Todos',                 N'Todos',                    N'All'),
('frmHistorialTurnos.Estado.Confirmado',            N'Confirmado',               N'Confirmed'),
('frmHistorialTurnos.Estado.Cancelado',             N'Cancelado',                N'Cancelled'),
('frmHistorialTurnos.Estado.Atendido',              N'Atendido',                 N'Attended'),
('frmHistorialTurnos.Estado.Ausente',               N'Ausente',                  N'Absent'),
('frmHistorialTurnos.dgvTurnos.colIdTurno',         N'Turno',                    N'Appointment'),
('frmHistorialTurnos.dgvTurnos.colPaciente',        N'Paciente',                 N'Patient'),
('frmHistorialTurnos.dgvTurnos.colDNI',             N'DNI',                      N'ID number'),
('frmHistorialTurnos.dgvTurnos.colFecha',           N'Fecha',                    N'Date'),
('frmHistorialTurnos.dgvTurnos.colHorario',         N'Horario',                  N'Time'),
('frmHistorialTurnos.dgvTurnos.colMedico',          N'Médico',                   N'Doctor'),
('frmHistorialTurnos.dgvTurnos.colEstado',          N'Estado',                   N'Status'),
('frmHistorialTurnos.dgvVersiones.colFechaHoraCambio', N'Fecha del cambio',      N'Changed on'),
('frmHistorialTurnos.dgvVersiones.colOperacion',    N'Operación',                N'Operation'),
('frmHistorialTurnos.dgvVersiones.colUsuario',      N'Usuario',                  N'User'),
('frmHistorialTurnos.dgvVersiones.colFecha',        N'Fecha del turno',          N'Appointment date'),
('frmHistorialTurnos.dgvVersiones.colHorario',      N'Horario',                  N'Time'),
('frmHistorialTurnos.dgvVersiones.colEstado',       N'Estado',                   N'Status');

DECLARE @idiomas TABLE (IdIdioma int, Texto nvarchar(400), Clave varchar(150));
INSERT INTO @idiomas (IdIdioma, Texto, Clave)
SELECT i.Id, CASE i.Codigo WHEN 'ES' THEN t.ES ELSE t.EN END, t.Clave
FROM @textos t CROSS JOIN dbo.Idiomas i
WHERE i.Codigo IN ('ES', 'EN');

UPDATE tr SET Traduccion = x.Texto
FROM dbo.Traducciones tr
INNER JOIN @idiomas x ON x.Clave = tr.Clave AND x.IdIdioma = tr.IdIdioma;

INSERT INTO dbo.Traducciones (Clave, IdIdioma, Traduccion)
SELECT x.Clave, x.IdIdioma, x.Texto
FROM @idiomas x
WHERE NOT EXISTS (SELECT 1 FROM dbo.Traducciones tr WHERE tr.Clave = x.Clave AND tr.IdIdioma = x.IdIdioma);

COMMIT TRANSACTION;

SELECT COUNT(*) AS TraduccionesDeTurnos
FROM dbo.Traducciones
WHERE Clave LIKE 'frmGestionTurnos.%' OR Clave LIKE 'frmHistorialTurnos.%';
