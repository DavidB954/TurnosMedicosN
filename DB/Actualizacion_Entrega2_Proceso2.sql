/*
    Script de actualizacion - Entrega 2 / Proceso 2: Atencion y Agenda Medica (RF 02.1 a RF 02.6)
    Base: GestionTurnosMedicos

    1) Estados de Turno: habilita 'Atendido' y 'Ausente' (si no se habia corrido
       Actualizacion_ProcesoVencimientoTurnos.sql).
    2) Secuencias para el N° de Historia Clinica y el N° de Receta (los genera el sistema).
    3) Tabla Paciente (1:1 con Usuario): DNI, N° HC, fecha de nacimiento, telefono.
       A los usuarios ya existentes se les crea un registro con datos provisorios.
    4) Tablas AtencionMedica (con DVH), Receta y RecetaItem.
    5) Fila de DVV para AtencionMedica.
    6) Patentes del formulario frm_AccesoMedico (Mi Agenda).

    La "tabla copia" de Turno (T06.B) ya existe: BitacoraEventosTurno, que se completa con
    los triggers de Turno dentro de la misma transaccion del UPDATE.

    Es idempotente: se puede ejecutar mas de una vez sin error.
*/

USE GestionTurnosMedicos;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- 1) Estados de Turno -------------------------------------------------------------------
IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Turno_Estado')
    ALTER TABLE dbo.Turno DROP CONSTRAINT CK_Turno_Estado;
GO
ALTER TABLE dbo.Turno
    ADD CONSTRAINT CK_Turno_Estado CHECK (Estado IN ('Confirmado', 'Cancelado', 'Atendido', 'Ausente'));
GO

-- 2) Secuencias ----------------------------------------------------------------------------
IF OBJECT_ID('dbo.SEQ_NroHistoriaClinica', 'SO') IS NULL
    CREATE SEQUENCE dbo.SEQ_NroHistoriaClinica AS INT START WITH 1000 INCREMENT BY 1;
GO
IF OBJECT_ID('dbo.SEQ_NroReceta', 'SO') IS NULL
    CREATE SEQUENCE dbo.SEQ_NroReceta AS INT START WITH 1 INCREMENT BY 1;
GO

-- 3) Paciente -----------------------------------------------------------------------------
IF OBJECT_ID('dbo.Paciente') IS NULL
BEGIN
    CREATE TABLE dbo.Paciente (
        IdUsuario          INT          NOT NULL,
        DNI                VARCHAR(10)  NOT NULL,
        NroHistoriaClinica INT          NOT NULL CONSTRAINT DF_Paciente_NroHC DEFAULT (NEXT VALUE FOR dbo.SEQ_NroHistoriaClinica),
        FechaNacimiento    DATE         NULL,
        Telefono           VARCHAR(30)  NULL,
        FechaAperturaHC    DATE         NOT NULL CONSTRAINT DF_Paciente_FechaAperturaHC DEFAULT (CAST(GETDATE() AS DATE)),
        CONSTRAINT PK_Paciente PRIMARY KEY (IdUsuario),
        CONSTRAINT FK_Paciente_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario (IdUsuario),
        CONSTRAINT UQ_Paciente_DNI UNIQUE (DNI),
        CONSTRAINT UQ_Paciente_NroHC UNIQUE (NroHistoriaClinica)
    );
END
GO

-- Usuarios existentes: DNI provisorio (20.000.000 + IdUsuario) para que todos tengan HC.
-- Se corrige despues desde ABM de Usuarios.
INSERT INTO dbo.Paciente (IdUsuario, DNI, FechaNacimiento, Telefono)
SELECT u.IdUsuario, CAST(20000000 + u.IdUsuario AS VARCHAR(10)), '1990-01-01', '11-0000-0000'
FROM dbo.Usuario u
WHERE NOT EXISTS (SELECT 1 FROM dbo.Paciente p WHERE p.IdUsuario = u.IdUsuario);
GO

-- 4) AtencionMedica / Receta / RecetaItem -------------------------------------------------
IF OBJECT_ID('dbo.AtencionMedica') IS NULL
BEGIN
    CREATE TABLE dbo.AtencionMedica (
        IdAtencion     INT IDENTITY(1,1) NOT NULL,
        IdTurno        INT            NOT NULL,
        FechaHora      DATETIME       NOT NULL,
        MotivoConsulta VARCHAR(500)   NOT NULL,
        Indicaciones   VARCHAR(1000)  NULL,
        DVH            VARCHAR(64)    NOT NULL,
        CONSTRAINT PK_AtencionMedica PRIMARY KEY (IdAtencion),
        CONSTRAINT FK_AtencionMedica_Turno FOREIGN KEY (IdTurno) REFERENCES dbo.Turno (IdTurno),
        -- RN-2: un turno tiene como maximo una atencion
        CONSTRAINT UQ_AtencionMedica_IdTurno UNIQUE (IdTurno)
    );
END
GO

IF OBJECT_ID('dbo.Receta') IS NULL
BEGIN
    CREATE TABLE dbo.Receta (
        IdReceta     INT IDENTITY(1,1) NOT NULL,
        IdAtencion   INT          NOT NULL,
        NroReceta    VARCHAR(20)  NOT NULL,
        FechaEmision DATETIME     NOT NULL,
        CONSTRAINT PK_Receta PRIMARY KEY (IdReceta),
        CONSTRAINT FK_Receta_AtencionMedica FOREIGN KEY (IdAtencion) REFERENCES dbo.AtencionMedica (IdAtencion),
        CONSTRAINT UQ_Receta_IdAtencion UNIQUE (IdAtencion),
        CONSTRAINT UQ_Receta_NroReceta UNIQUE (NroReceta)
    );
END
GO

IF OBJECT_ID('dbo.RecetaItem') IS NULL
BEGIN
    CREATE TABLE dbo.RecetaItem (
        IdRecetaItem INT IDENTITY(1,1) NOT NULL,
        IdReceta     INT           NOT NULL,
        Medicamento  VARCHAR(150)  NOT NULL,
        Presentacion VARCHAR(100)  NULL,
        Posologia    VARCHAR(200)  NOT NULL,
        Cantidad     INT           NOT NULL,
        CONSTRAINT PK_RecetaItem PRIMARY KEY (IdRecetaItem),
        CONSTRAINT FK_RecetaItem_Receta FOREIGN KEY (IdReceta) REFERENCES dbo.Receta (IdReceta),
        CONSTRAINT CK_RecetaItem_Cantidad CHECK (Cantidad > 0)
    );
END
GO

-- 5) DVV de AtencionMedica (tabla vacia = hash SHA-256 de cadena vacia, igual que HashHelper) --
IF NOT EXISTS (SELECT 1 FROM dbo.DVV WHERE NombreTabla = 'AtencionMedica')
    INSERT INTO dbo.DVV (NombreTabla, DVV, FechaDeActualizacion)
    VALUES ('AtencionMedica', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', N''), 2), GETDATE());
GO

-- 6) Patentes de frm_AccesoMedico (Mi Agenda) ----------------------------------------------
--    Se quitan las patentes de la pantalla de ejemplo anterior y se agregan las nuevas.
DELETE rp FROM dbo.Rol_Permiso rp JOIN dbo.Permisos p ON p.IdPermisos = rp.IdPermisos
WHERE p.Nombre IN ('frm_AccesoMedico.btnAtencionMedica', 'frm_AccesoMedico.btnCancelarAtencionMedica');
DELETE up FROM dbo.Usuario_Permiso up JOIN dbo.Permisos p ON p.IdPermisos = up.IdPermisos
WHERE p.Nombre IN ('frm_AccesoMedico.btnAtencionMedica', 'frm_AccesoMedico.btnCancelarAtencionMedica');
DELETE FROM dbo.Permisos
WHERE Nombre IN ('frm_AccesoMedico.btnAtencionMedica', 'frm_AccesoMedico.btnCancelarAtencionMedica');
DELETE FROM dbo.Traducciones
WHERE Clave IN ('Permisos.frm_AccesoMedico.btnAtencionMedica', 'Permisos.frm_AccesoMedico.btnCancelarAtencionMedica');
GO

DECLARE @Patentes TABLE (Control VARCHAR(50), Descripcion VARCHAR(100), Es NVARCHAR(255), En NVARCHAR(255));
INSERT INTO @Patentes VALUES
('btnAtender',           'Atender consulta',              N'Atender consulta',              N'Attend consultation'),
('btnAusente',           'Marcar paciente ausente',       N'Marcar paciente ausente',       N'Mark patient as absent'),
('btnFinalizarAtencion', 'Finalizar atencion del dia',    N'Finalizar atención del día',    N'Finish the day''s consultations'),
('btnBuscarHC',          'Consultar historia clinica',    N'Consultar historia clínica',    N'View medical record');

INSERT INTO dbo.Permisos (Nombre, Descripcion, Formulario)
SELECT 'frm_AccesoMedico.' + x.Control, x.Descripcion, 'frm_AccesoMedico'
FROM @Patentes x
WHERE NOT EXISTS (SELECT 1 FROM dbo.Permisos p WHERE p.Nombre = 'frm_AccesoMedico.' + x.Control);

-- La familia del formulario contiene todas sus patentes
INSERT INTO dbo.Rol_Permiso (IdRol, IdPermisos)
SELECT r.IdRol, p.IdPermisos
FROM dbo.Rol r
JOIN dbo.Permisos p ON p.Formulario = r.Formulario
WHERE r.Formulario = 'frm_AccesoMedico'
  AND NOT EXISTS (SELECT 1 FROM dbo.Rol_Permiso rp WHERE rp.IdRol = r.IdRol AND rp.IdPermisos = p.IdPermisos);

-- Traduccion de las patentes ("Permisos.frm_AccesoMedico.btnX")
UPDATE t SET t.Traduccion = CASE i.Codigo WHEN 'EN' THEN x.En ELSE x.Es END
FROM dbo.Traducciones t
JOIN dbo.Idiomas i ON i.Id = t.IdIdioma
JOIN @Patentes x ON t.Clave = 'Permisos.frm_AccesoMedico.' + x.Control;

INSERT INTO dbo.Traducciones (Clave, IdIdioma, Traduccion)
SELECT 'Permisos.frm_AccesoMedico.' + x.Control, i.Id, CASE i.Codigo WHEN 'EN' THEN x.En ELSE x.Es END
FROM @Patentes x
CROSS JOIN dbo.Idiomas i
WHERE NOT EXISTS (SELECT 1 FROM dbo.Traducciones t WHERE t.Clave = 'Permisos.frm_AccesoMedico.' + x.Control AND t.IdIdioma = i.Id);
GO

-- 6.b) Patente de frm_AccesoPaciente: el paciente ve y descarga en PDF la receta de sus turnos
--      Atendidos (solo la receta: el motivo y las indicaciones son de la Historia Clinica).
--      Si existia la version anterior del boton (btnVerAtencion), se renombra.
UPDATE dbo.Permisos SET Nombre = 'frm_AccesoPaciente.btnVerReceta', Descripcion = 'Ver y descargar receta'
WHERE Nombre = 'frm_AccesoPaciente.btnVerAtencion'
  AND NOT EXISTS (SELECT 1 FROM dbo.Permisos WHERE Nombre = 'frm_AccesoPaciente.btnVerReceta');

INSERT INTO dbo.Permisos (Nombre, Descripcion, Formulario)
SELECT 'frm_AccesoPaciente.btnVerReceta', 'Ver y descargar receta', 'frm_AccesoPaciente'
WHERE NOT EXISTS (SELECT 1 FROM dbo.Permisos WHERE Nombre = 'frm_AccesoPaciente.btnVerReceta');

INSERT INTO dbo.Rol_Permiso (IdRol, IdPermisos)
SELECT r.IdRol, p.IdPermisos
FROM dbo.Rol r
JOIN dbo.Permisos p ON p.Nombre = 'frm_AccesoPaciente.btnVerReceta'
WHERE r.Formulario = 'frm_AccesoPaciente'
  AND NOT EXISTS (SELECT 1 FROM dbo.Rol_Permiso rp WHERE rp.IdRol = r.IdRol AND rp.IdPermisos = p.IdPermisos);
GO

-- 6.c) Medico (1:1 con Usuario): matricula profesional, se muestra en el PDF de la receta.
--      Se carga en Gestion de Medicos (frmGestionMedicos.btnGuardarMatricula).
IF OBJECT_ID('dbo.Medico') IS NULL
BEGIN
    CREATE TABLE dbo.Medico (
        IdUsuario INT         NOT NULL,
        Matricula VARCHAR(20) NOT NULL,
        CONSTRAINT PK_Medico PRIMARY KEY (IdUsuario),
        CONSTRAINT FK_Medico_Usuario FOREIGN KEY (IdUsuario) REFERENCES dbo.Usuario (IdUsuario),
        CONSTRAINT UQ_Medico_Matricula UNIQUE (Matricula)
    );
END
GO

INSERT INTO dbo.Permisos (Nombre, Descripcion, Formulario)
SELECT 'frmGestionMedicos.btnGuardarMatricula', 'Guardar matricula del medico', 'frmGestionMedicos'
WHERE NOT EXISTS (SELECT 1 FROM dbo.Permisos WHERE Nombre = 'frmGestionMedicos.btnGuardarMatricula');

INSERT INTO dbo.Rol_Permiso (IdRol, IdPermisos)
SELECT r.IdRol, p.IdPermisos
FROM dbo.Rol r
JOIN dbo.Permisos p ON p.Nombre = 'frmGestionMedicos.btnGuardarMatricula'
WHERE r.Formulario = 'frmGestionMedicos'
  AND NOT EXISTS (SELECT 1 FROM dbo.Rol_Permiso rp WHERE rp.IdRol = r.IdRol AND rp.IdPermisos = p.IdPermisos);
GO

-- 6.d) Formulario "Mis recetas" (frmMisRecetas): el paciente ve todas sus recetas y descarga la elegida.
--      Familia nueva con sus patentes; la reciben el rol Paciente y el Administrador (que tiene todas).
IF NOT EXISTS (SELECT 1 FROM dbo.Rol WHERE Formulario = 'frmMisRecetas')
    INSERT INTO dbo.Rol (Nombre, Descripcion, Formulario) VALUES ('Mis Recetas', 'Formulario frmMisRecetas', 'frmMisRecetas');

INSERT INTO dbo.Permisos (Nombre, Descripcion, Formulario)
SELECT x.Nombre, x.Descripcion, 'frmMisRecetas'
FROM (VALUES ('frmMisRecetas.Ver', 'Ingresar a la pantalla'),
             ('frmMisRecetas.btnDescargarPdf', 'Descargar receta en PDF')) AS x (Nombre, Descripcion)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Permisos p WHERE p.Nombre = x.Nombre);

INSERT INTO dbo.Rol_Permiso (IdRol, IdPermisos)
SELECT r.IdRol, p.IdPermisos
FROM dbo.Rol r
JOIN dbo.Permisos p ON p.Formulario = r.Formulario
WHERE r.Formulario = 'frmMisRecetas'
  AND NOT EXISTS (SELECT 1 FROM dbo.Rol_Permiso rp WHERE rp.IdRol = r.IdRol AND rp.IdPermisos = p.IdPermisos);

INSERT INTO dbo.Rol_Rol (IdRolPadre, IdRolHijo)
SELECT padre.IdRol, fam.IdRol
FROM dbo.Rol padre
JOIN dbo.Rol fam ON fam.Formulario = 'frmMisRecetas'
WHERE padre.Nombre IN ('Paciente', 'Administrador') AND padre.Formulario IS NULL
  AND NOT EXISTS (SELECT 1 FROM dbo.Rol_Rol rr WHERE rr.IdRolPadre = padre.IdRol AND rr.IdRolHijo = fam.IdRol);
GO

-- 7) Traducciones (RNF-06) de Mi Agenda, frmAtencion y los datos de paciente de ABM de Usuarios ---
DECLARE @Trad TABLE (Clave VARCHAR(150) PRIMARY KEY, Es NVARCHAR(255), En NVARCHAR(255));
INSERT INTO @Trad (Clave, Es, En) VALUES
-- frm_AccesoMedico (Mi Agenda)
('frm_AccesoMedico.Titulo',                      N'Mi Agenda',                                  N'My Schedule'),
('frm_AccesoMedico.lblTitulo',                   N'Mi Agenda',                                  N'My Schedule'),
('frm_AccesoMedico.lblFecha',                    N'Fecha',                                      N'Date'),
('frm_AccesoMedico.btnHoy',                      N'Hoy',                                        N'Today'),
('frm_AccesoMedico.lblAyudaAusente',             N'«Ausente» se habilita solo cuando la hora de inicio del turno ya pasó.', N'"Absent" is enabled only after the appointment start time.'),
('frm_AccesoMedico.btnFinalizarAtencion',        N'Finalizar atención',                         N'Finish consultations'),
('frm_AccesoMedico.groupBoxHC',                  N'Consultar Historia Clínica',                 N'Medical Record Lookup'),
('frm_AccesoMedico.lblBuscarHC',                 N'Nombre y apellido, N° HC o DNI',             N'Full name, record # or ID'),
('frm_AccesoMedico.btnBuscarHC',                 N'Buscar',                                     N'Search'),
('frm_AccesoMedico.lblNotaHC',                   N'Solo lectura. Cada consulta de Historia Clínica queda registrada en la Bitácora.', N'Read only. Every medical record lookup is recorded in the Log.'),
('frm_AccesoMedico.dgvAgenda.colFecha',          N'Fecha',                                      N'Date'),
('frm_AccesoMedico.dgvAgenda.colHora',           N'Hora',                                       N'Time'),
('frm_AccesoMedico.dgvAgenda.colPaciente',       N'Paciente',                                   N'Patient'),
('frm_AccesoMedico.dgvAgenda.colHistoriaClinica',N'Historia Clínica',                           N'Medical Record'),
('frm_AccesoMedico.dgvAgenda.colEstado',         N'Estado',                                     N'Status'),
('frm_AccesoMedico.dgvAgenda.colAtender',        N'Opciones',                                   N'Options'),
('frm_AccesoMedico.dgvPacientes.colPaciente',    N'Paciente',                                   N'Patient'),
('frm_AccesoMedico.dgvPacientes.colDNI',         N'DNI',                                        N'ID'),
('frm_AccesoMedico.dgvPacientes.colHC',          N'N° HC',                                      N'Record #'),
('frm_AccesoMedico.Estado.Confirmado',           N'Confirmado',                                 N'Confirmed'),
('frm_AccesoMedico.Estado.Atendido',             N'Atendido',                                   N'Attended'),
('frm_AccesoMedico.Estado.Ausente',              N'Ausente',                                    N'Absent'),
('frm_AccesoMedico.Msg.Atender',                 N'Atender',                                    N'Attend'),
('frm_AccesoMedico.Msg.Ausente',                 N'Ausente',                                    N'Absent'),
('frm_AccesoMedico.Msg.AccesoDenegado',          N'Acceso denegado: Mi Agenda es exclusiva del rol Médico.', N'Access denied: My Schedule is only available to the Doctor role.'),
('frm_AccesoMedico.Msg.ErrorCargar',             N'Error al cargar la agenda',                  N'Error loading the schedule'),
('frm_AccesoMedico.Msg.Error',                   N'Error',                                      N'Error'),
('frm_AccesoMedico.Msg.Medico',                  N'Médico',                                     N'Doctor'),
('frm_AccesoMedico.Msg.ModoSoloLectura',         N'Fecha distinta de hoy: agenda en modo solo lectura.', N'Date other than today: schedule is read only.'),
('frm_AccesoMedico.Msg.SinTurnosHoy',            N'No tiene turnos para hoy.',                  N'You have no appointments today.'),
('frm_AccesoMedico.Msg.SinTurnosFecha',          N'No tiene turnos para el {0:dd/MM/yyyy}. Puede elegir otra fecha.', N'You have no appointments on {0:dd/MM/yyyy}. You can choose another date.'),
('frm_AccesoMedico.Msg.TurnoNoAtendible',        N'El turno no puede atenderse',                N'The appointment cannot be attended'),
('frm_AccesoMedico.Msg.ConfirmarAusente',        N'¿Confirma que {0} no se presentó?',          N'Do you confirm that {0} did not show up?'),
('frm_AccesoMedico.Msg.MarcarAusente',           N'Marcar ausente',                             N'Mark absent'),
('frm_AccesoMedico.Msg.FinalizarAtencion',       N'Finalizar atención',                         N'Finish consultations'),
('frm_AccesoMedico.Msg.FinalizadaOk',            N'Todos los turnos del día están Atendidos o Ausentes. Atención finalizada.', N'All of today''s appointments are Attended or Absent. Consultations finished.'),
('frm_AccesoMedico.Msg.PreguntaAusentes',        N'Hay {0} turno(s) sin atender. ¿Desea marcar como ausente los turnos no atendidos?', N'There are {0} unattended appointment(s). Do you want to mark them as absent?'),
('frm_AccesoMedico.Msg.AusentesMarcados',        N'Se marcaron {0} turno(s) como Ausente. Atención finalizada.', N'{0} appointment(s) marked as Absent. Consultations finished.'),
('frm_AccesoMedico.Msg.BuscarHC',                N'Consultar Historia Clínica',                 N'Medical Record Lookup'),
('frm_AccesoMedico.Msg.SinPacientes',            N'No se encontraron pacientes.',               N'No patients found.'),
('frm_AccesoMedico.Msg.ElegirPaciente',          N'Seleccione un paciente de la lista.',        N'Select a patient from the list.'),
('frm_AccesoMedico.Msg.HCSinAtenciones',         N'La Historia Clínica no tiene atenciones registradas.', N'The medical record has no consultations.'),
('frm_AccesoMedico.Msg.Motivo',                  N'Motivo',                                     N'Reason'),
('frm_AccesoMedico.Msg.Indicaciones',            N'Indicaciones',                               N'Instructions'),
('frm_AccesoMedico.Msg.SinReceta',               N'Sin receta',                                 N'No prescription'),
('frm_AccesoMedico.Msg.Receta',                  N'Receta',                                     N'Prescription'),
-- frmAtencion (Registrar Atencion + Receta)
('frmAtencion.Titulo',                           N'Registrar Atención',                         N'Record Consultation'),
('frmAtencion.lblPaciente',                      N'Paciente',                                   N'Patient'),
('frmAtencion.lblHistoriaClinica',               N'Historia Clínica',                           N'Medical Record'),
('frmAtencion.lblDNI',                           N'DNI',                                        N'ID'),
('frmAtencion.lblMotivo',                        N'Motivo de la consulta *',                    N'Reason for consultation *'),
('frmAtencion.lblIndicaciones',                  N'Indicaciones',                               N'Instructions'),
('frmAtencion.lblReceta',                        N'¿Receta?',                                   N'Prescription?'),
('frmAtencion.rbRecetaSi',                       N'SÍ',                                         N'YES'),
('frmAtencion.rbRecetaNo',                       N'NO',                                         N'NO'),
('frmAtencion.groupBoxReceta',                   N'Receta',                                     N'Prescription'),
('frmAtencion.lblMedicamento',                   N'Medicamento *',                              N'Medication *'),
('frmAtencion.lblPresentacion',                  N'Presentación (ej.: 500 mg comp.)',           N'Form / strength (e.g. 500 mg tab.)'),
('frmAtencion.lblPosologia',                     N'Posología *',                                N'Dosage *'),
('frmAtencion.lblCantidad',                      N'Cant.',                                      N'Qty.'),
('frmAtencion.btnAgregar',                       N'Agregar',                                    N'Add'),
('frmAtencion.lblAyudaReceta',                   N'Al menos 1 medicamento si Receta = SÍ.',     N'At least 1 medication if Prescription = YES.'),
('frmAtencion.btnCancelar',                      N'Cancelar',                                   N'Cancel'),
('frmAtencion.btnRegistrar',                     N'Registrar Atención',                         N'Record Consultation'),
('frmAtencion.dgvMedicamentos.colMedicamento',   N'Medicamento',                                N'Medication'),
('frmAtencion.dgvMedicamentos.colPresentacion',  N'Presentación',                               N'Form / strength'),
('frmAtencion.dgvMedicamentos.colPosologia',     N'Posología',                                  N'Dosage'),
('frmAtencion.dgvMedicamentos.colCantidad',      N'Cant.',                                      N'Qty.'),
('frmAtencion.dgvMedicamentos.colQuitar',        N'Quitar',                                     N'Remove'),
('frmAtencion.Msg.Error',                        N'Error',                                      N'Error'),
('frmAtencion.Msg.Receta',                       N'Receta',                                     N'Prescription'),
('frmAtencion.Msg.DescartarReceta',              N'Se descartarán los medicamentos cargados. ¿Continuar?', N'The medications entered will be discarded. Continue?'),
('frmAtencion.Msg.AtencionRegistrada',           N'Atención registrada en la Historia Clínica. El turno quedó Atendido.', N'Consultation recorded in the medical record. The appointment is now Attended.'),
('frmAtencion.Msg.RecetaEmitida',                N'Receta N° {0} emitida.',                     N'Prescription # {0} issued.'),
('frmAtencion.Msg.DatosIncompletos',             N'Revise los datos',                           N'Check the data'),
('frmAtencion.Msg.ConfirmarCancelar',            N'¿Cerrar sin registrar la atención? No se guardará nada y el turno seguirá Confirmado.', N'Close without recording? Nothing will be saved and the appointment will remain Confirmed.'),
-- frm_BitacoraTurno (T06.B: usuario de la aplicacion que hizo el cambio)
('frm_BitacoraTurno.dgvEventos.colUsuario',      N'Usuario',                                    N'User'),
-- frm_AccesoPaciente / frmMiReceta (el paciente ve y descarga su receta)
('frm_AccesoPaciente.btnVerReceta',              N'Ver receta',                                 N'View prescription'),
('Permisos.frm_AccesoPaciente.btnVerReceta',     N'Ver y descargar receta',                     N'View and download prescription'),
('frmMiReceta.Titulo',                           N'Mi receta',                                  N'My prescription'),
('frmMiReceta.lblNota',                          N'Documento informativo. La receta oficial la emite el médico en una plataforma de receta electrónica registrada.', N'Informative document. The official prescription is issued by the doctor on a registered electronic prescription platform.'),
('frmMiReceta.btnDescargarPdf',                  N'Descargar PDF',                              N'Download PDF'),
('frmMiReceta.btnCerrar',                        N'Cerrar',                                     N'Close'),
('frmMiReceta.dgvMedicamentos.colMedicamento',   N'Medicamento',                                N'Medication'),
('frmMiReceta.dgvMedicamentos.colPresentacion',  N'Presentación',                               N'Form / strength'),
('frmMiReceta.dgvMedicamentos.colPosologia',     N'Posología',                                  N'Dosage'),
('frmMiReceta.dgvMedicamentos.colCantidad',      N'Cant.',                                      N'Qty.'),
('frmMiReceta.Msg.RecetaNro',                    N'Receta N° {0}',                              N'Prescription # {0}'),
('frmMiReceta.Msg.EmitidaEl',                    N'Emitida el {0:dd/MM/yyyy}',                  N'Issued on {0:dd/MM/yyyy}'),
('frmMiReceta.Msg.Medico',                       N'Médico',                                     N'Doctor'),
('frmMiReceta.Msg.GuardarComo',                  N'Guardar receta',                             N'Save prescription'),
('frmMiReceta.Msg.Descargada',                   N'Receta guardada en: {0}. ¿Desea abrirla?',   N'Prescription saved to: {0}. Do you want to open it?'),
('frmMiReceta.Msg.ArchivoEnUso',                 N'No se pudo guardar el archivo. Si está abierto, ciérrelo e intente de nuevo.', N'The file could not be saved. If it is open, close it and try again.'),
('frmMiReceta.Msg.Error',                        N'Error',                                      N'Error'),
-- frmMisRecetas (menu "Mis recetas" del paciente)
('frmPrincipal.btn_MisRecetas',                  N'Mis recetas',                                N'My prescriptions'),
('Formularios.frmMisRecetas',                    N'Mis Recetas',                                N'My Prescriptions'),
('Permisos.frmMisRecetas.Ver',                   N'Ingresar a la pantalla',                     N'Open the screen'),
('Permisos.frmMisRecetas.btnDescargarPdf',       N'Descargar receta en PDF',                    N'Download prescription as PDF'),
('frmMisRecetas.Titulo',                         N'Mis recetas',                                N'My prescriptions'),
('frmMisRecetas.lblTitulo',                      N'Mis recetas',                                N'My prescriptions'),
('frmMisRecetas.lblAyuda',                       N'Seleccione una receta para ver sus medicamentos y descargarla en PDF.', N'Select a prescription to see its medications and download it as PDF.'),
('frmMisRecetas.btnDescargarPdf',                N'Descargar PDF',                              N'Download PDF'),
('frmMisRecetas.lblNota',                        N'Documento informativo. La receta oficial la emite el médico en una plataforma de receta electrónica registrada.', N'Informative document. The official prescription is issued by the doctor on a registered electronic prescription platform.'),
('frmMisRecetas.dgvRecetas.colFecha',            N'Fecha',                                      N'Date'),
('frmMisRecetas.dgvRecetas.colNroReceta',        N'N° de receta',                               N'Prescription #'),
('frmMisRecetas.dgvRecetas.colMedico',           N'Médico',                                     N'Doctor'),
('frmMisRecetas.dgvRecetas.colMatricula',        N'Matrícula',                                  N'License #'),
('frmMisRecetas.dgvRecetas.colEspecialidad',     N'Especialidad',                               N'Specialty'),
('frmMisRecetas.dgvRecetas.colMedicamentos',     N'Medicamentos',                               N'Medications'),
('frmMisRecetas.dgvMedicamentos.colMedicamento', N'Medicamento',                                N'Medication'),
('frmMisRecetas.dgvMedicamentos.colPresentacion',N'Presentación',                               N'Form / strength'),
('frmMisRecetas.dgvMedicamentos.colPosologia',   N'Posología',                                  N'Dosage'),
('frmMisRecetas.dgvMedicamentos.colCantidad',    N'Cant.',                                      N'Qty.'),
('frmMisRecetas.Msg.Medicamentos',               N'Medicamentos de la receta {0}',              N'Medications of prescription {0}'),
('frmMisRecetas.Msg.SinRecetas',                 N'Todavía no tiene recetas emitidas.',         N'You have no prescriptions yet.'),
('frmMisRecetas.Msg.ElegirReceta',               N'Seleccione una receta de la lista.',         N'Select a prescription from the list.'),
('frmMisRecetas.Msg.GuardarComo',                N'Guardar receta',                             N'Save prescription'),
('frmMisRecetas.Msg.Descargada',                 N'Receta guardada en: {0}. ¿Desea abrirla?',   N'Prescription saved to: {0}. Do you want to open it?'),
('frmMisRecetas.Msg.ArchivoEnUso',               N'No se pudo guardar el archivo. Si está abierto, ciérrelo e intente de nuevo.', N'The file could not be saved. If it is open, close it and try again.'),
('frmMisRecetas.Msg.Error',                      N'Error',                                      N'Error'),
-- frmGestionMedicos (matricula)
('frmGestionMedicos.lblMatricula',               N'Matrícula:',                                 N'License #:'),
('frmGestionMedicos.btnGuardarMatricula',        N'Guardar matrícula',                          N'Save license'),
('Permisos.frmGestionMedicos.btnGuardarMatricula', N'Guardar matrícula del médico',             N'Save doctor license'),
-- frmMiReceta (matricula)
('frmMiReceta.Msg.Matricula',                    N'Matrícula',                                  N'License #'),
-- frmUsuarioABM (datos de paciente)
('frmUsuarioABM.groupBoxDatosPaciente',          N'Datos de Paciente',                          N'Patient Data'),
('frmUsuarioABM.lblDNI',                         N'DNI',                                        N'ID'),
('frmUsuarioABM.lblFechaNacimiento',             N'Fecha Nac.',                                 N'Birth date'),
('frmUsuarioABM.lblTelefono',                    N'Teléfono',                                   N'Phone'),
('frmUsuarioABM.lblAyudaPaciente',               N'Completar solo para pacientes. Al guardar el DNI se abre la Historia Clínica (el N° de HC lo genera el sistema).', N'Only for patients. Saving the ID opens the medical record (the record # is generated by the system).');

DELETE FROM dbo.Traducciones WHERE Clave = 'frmAtencion.lblAyudaRegistrar';
-- Version anterior de la pantalla del paciente (mostraba tambien motivo e indicaciones)
DELETE FROM dbo.Traducciones
WHERE Clave LIKE 'frmDetalleAtencion.%'
   OR Clave IN ('frm_AccesoPaciente.btnVerAtencion', 'Permisos.frm_AccesoPaciente.btnVerAtencion');

-- Se borran las claves de la pantalla de ejemplo anterior de frm_AccesoMedico que ya no existen.
DELETE t FROM dbo.Traducciones t
WHERE t.Clave LIKE 'frm[_]AccesoMedico.%'
  AND NOT EXISTS (SELECT 1 FROM @Trad x WHERE x.Clave = t.Clave);

-- Upsert: ES y EN con su texto; cualquier otro idioma ya registrado recibe el texto en Español.
UPDATE t SET t.Traduccion = CASE i.Codigo WHEN 'EN' THEN x.En ELSE x.Es END
FROM dbo.Traducciones t
JOIN dbo.Idiomas i ON i.Id = t.IdIdioma
JOIN @Trad x ON x.Clave = t.Clave
WHERE i.Codigo IN ('ES', 'EN');

INSERT INTO dbo.Traducciones (Clave, IdIdioma, Traduccion)
SELECT x.Clave, i.Id, CASE i.Codigo WHEN 'EN' THEN x.En ELSE x.Es END
FROM @Trad x
CROSS JOIN dbo.Idiomas i
WHERE NOT EXISTS (SELECT 1 FROM dbo.Traducciones t WHERE t.Clave = x.Clave AND t.IdIdioma = i.Id);
GO
