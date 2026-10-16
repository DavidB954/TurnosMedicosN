/*
    Script de actualización — Traducciones de Seguridad, Gestión de Turnos y Gestión de Médicos
    Base: GestionTurnosMedicos

    Claves:
      <Formulario>.<Control>                 labels, botones, groupbox, checkbox y selectores (uc)
      <Formulario>.<Grilla>.<Columna>        encabezados de grillas
      frmGestionTurnos.Estado.<Valor>        textos del combo de estado

    No se traduce frmGestionTurnos.lblTotalRegistros: muestra el número de registros.
    Es idempotente: inserta las claves faltantes y actualiza las existentes.
*/

USE GestionTurnosMedicos;
GO

DECLARE @Trad TABLE (Clave VARCHAR(150) COLLATE DATABASE_DEFAULT, Es NVARCHAR(255), En NVARCHAR(255));

INSERT INTO @Trad VALUES
-- Seguridad
('frmSeguridad.groupBox_Opciones',                         N'Opciones',                               N'Options'),
('frmSeguridad.groupBox_FilasCorruptas',                   N'Filas Corrompidas',                      N'Corrupted Rows'),
('frmSeguridad.btnBackUp',                                 N'Generar Back Up',                        N'Generate Backup'),
('frmSeguridad.btnRestaurarBD',                            N'Restaurar BD',                           N'Restore DB'),
('frmSeguridad.btnBloquearUsuYRecalcular',                 N'Bloquear Usuario y Recalcular DV',       N'Lock User and Recalculate CD'),
('frmSeguridad.dgvSeguridad.IdUsuario',                    N'ID Usuario',                             N'User ID'),
('frmSeguridad.dgvSeguridad.DVH_Almacenado',               N'DVH Guardado',                           N'Stored HCD'),
('frmSeguridad.dgvSeguridad.DVH_Recalculado',              N'DVH Recalculado',                        N'Recalculated HCD'),

-- Gestión de Turnos
('frmGestionTurnos.lblTitulo',                             N'GESTIÓN DE TURNOS',                      N'APPOINTMENT MANAGEMENT'),
('frmGestionTurnos.groupBoxFiltros',                       N'Filtros de Búsqueda',                    N'Search Filters'),
('frmGestionTurnos.lblIdTurno',                            N'N° Turno:',                              N'Appointment No.:'),
('frmGestionTurnos.lblDesde',                              N'Fecha desde:',                           N'Date from:'),
('frmGestionTurnos.lblHasta',                              N'Fecha hasta:',                           N'Date to:'),
('frmGestionTurnos.lblEstado',                             N'Estado:',                                N'Status:'),
('frmGestionTurnos.btnBuscar',                             N'Buscar',                                 N'Search'),
('frmGestionTurnos.btnLimpiarFiltros',                     N'Limpiar Filtros',                        N'Clear Filters'),
('frmGestionTurnos.btnCancelarTurno',                      N'Cancelar Turno Seleccionado',            N'Cancel Selected Appointment'),
('frmGestionTurnos.lblTotalDeRegistros',                   N'Total registros:',                       N'Total records:'),
('frmGestionTurnos.Estado.Todos',                          N'Todos',                                  N'All'),
('frmGestionTurnos.Estado.Confirmado',                     N'Confirmado',                             N'Confirmed'),
('frmGestionTurnos.Estado.Cancelado',                      N'Cancelado',                              N'Cancelled'),
('frmGestionTurnos.Estado.Atendido',                       N'Atendido',                               N'Attended'),
('frmGestionTurnos.Estado.Ausente',                        N'Ausente',                                N'Absent'),
('frmGestionTurnos.dgvTurnos.colIdTurno',                  N'Turno',                                  N'Appointment'),
('frmGestionTurnos.dgvTurnos.colFecha',                    N'Fecha',                                  N'Date'),
('frmGestionTurnos.dgvTurnos.colHorario',                  N'Horario',                                N'Time'),
('frmGestionTurnos.dgvTurnos.colPaciente',                 N'Paciente',                               N'Patient'),
('frmGestionTurnos.dgvTurnos.colMedico',                   N'Médico',                                 N'Doctor'),
('frmGestionTurnos.dgvTurnos.colEspecialidad',             N'Especialidad',                           N'Specialty'),
('frmGestionTurnos.dgvTurnos.colEstado',                   N'Estado',                                 N'Status'),

-- Gestión de Médicos
('frmGestionMedicos.groupBoxEspecialidades',               N'Especialidades',                         N'Specialties'),
('frmGestionMedicos.lblNombreEspecialidad',                N'Nombre de Especialidad:',                N'Specialty Name:'),
('frmGestionMedicos.btnEspecialidadCrear',                 N'Crear',                                  N'Create'),
('frmGestionMedicos.btnEspecialidadEliminar',              N'Eliminar',                               N'Delete'),
('frmGestionMedicos.ucEspecialidadCrud',                   N'Especialidades:',                        N'Specialties:'),
('frmGestionMedicos.groupBoxAsignacionEspecialidades',     N'Asignación de Especialidades',           N'Specialty Assignment'),
('frmGestionMedicos.ucEspecialidadAsignar',                N'Seleccione una Especialidad:',           N'Select a Specialty:'),
('frmGestionMedicos.ucMedicoAsignar',                      N'Seleccione un Médico:',                  N'Select a Doctor:'),
('frmGestionMedicos.btnAsignarEspecialidad',               N'Asignar',                                N'Assign'),
('frmGestionMedicos.btnQuitarEspecialidad',                N'Quitar',                                 N'Remove'),
('frmGestionMedicos.groupBoxHorariosMedicos',              N'Asignación de Horarios',                 N'Schedule Assignment'),
('frmGestionMedicos.ucMedicoHorario',                      N'Seleccione un Médico:',                  N'Select a Doctor:'),
('frmGestionMedicos.ucEspecialidadHorario',                N'Especialidad:',                          N'Specialty:'),
('frmGestionMedicos.lblFranjaHoraria',                     N'Ingrese la franja horaria:',             N'Enter the time slot:'),
('frmGestionMedicos.lblDesde',                             N'Desde',                                  N'From'),
('frmGestionMedicos.lblHasta',                             N'Hasta',                                  N'To'),
('frmGestionMedicos.lblTurnosCadaXMin',                    N'Turnos cada:',                           N'Appointments every:'),
('frmGestionMedicos.lblDiasTrabajados',                    N'Días Trabajados:',                       N'Working Days:'),
('frmGestionMedicos.checkBoxLunes',                        N'L',                                      N'Mo'),
('frmGestionMedicos.checkBoxMartes',                       N'MA',                                     N'Tu'),
('frmGestionMedicos.checkBoxMiercoles',                    N'MI',                                     N'We'),
('frmGestionMedicos.checkBoxJueves',                       N'J',                                      N'Th'),
('frmGestionMedicos.checkBoxViernes',                      N'V',                                      N'Fr'),
('frmGestionMedicos.checkBoxSabado',                       N'S',                                      N'Sa'),
('frmGestionMedicos.btnAsignarDiaYHorario',                N'Asignar Días y Horarios',                N'Assign Days and Hours'),
('frmGestionMedicos.btnModificarDiasYHorarios',            N'Modificar',                              N'Modify'),
('frmGestionMedicos.btnEditarFranjas',                     N'Editar / inactivar franjas de un médico', N'Edit / deactivate a doctor''s time slots'),
('frmGestionMedicos.checkBoxMostrarInactivos',             N'Mostrar horarios inactivos',             N'Show inactive schedules'),
('frmGestionMedicos.dataGridViewMedicosDiasHorarios.colMedico',       N'Médico',              N'Doctor'),
('frmGestionMedicos.dataGridViewMedicosDiasHorarios.colEspecialidad', N'Especialidad',        N'Specialty'),
('frmGestionMedicos.dataGridViewMedicosDiasHorarios.colDia',          N'Día',                 N'Day'),
('frmGestionMedicos.dataGridViewMedicosDiasHorarios.colHorario',      N'Horario de atención', N'Office hours'),
('frmGestionMedicos.dataGridViewMedicosDiasHorarios.colTurnosCada',   N'Turnos cada',         N'Appointments every'),
('frmGestionMedicos.dataGridViewMedicosDiasHorarios.colEstado',       N'Estado',              N'Status');

-- Upsert en Traducciones para ES y EN
UPDATE t SET t.Traduccion = CASE i.Codigo WHEN 'EN' THEN x.En ELSE x.Es END
FROM dbo.Traducciones t
JOIN dbo.Idiomas i ON i.Id = t.IdIdioma AND i.Codigo IN ('ES', 'EN')
JOIN @Trad x ON x.Clave = t.Clave;

INSERT INTO dbo.Traducciones (Clave, IdIdioma, Traduccion)
SELECT x.Clave, i.Id, CASE i.Codigo WHEN 'EN' THEN x.En ELSE x.Es END
FROM @Trad x
CROSS JOIN dbo.Idiomas i
WHERE i.Codigo IN ('ES', 'EN')
  AND NOT EXISTS (SELECT 1 FROM dbo.Traducciones t WHERE t.Clave = x.Clave AND t.IdIdioma = i.Id);
GO
