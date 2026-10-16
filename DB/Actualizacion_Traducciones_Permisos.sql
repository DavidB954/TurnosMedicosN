/*
    Script de actualización — Traducciones de Gestión de Usuarios / Roles / Permisos
    Base: GestionTurnosMedicos
    Requiere haber ejecutado antes Actualizacion_PermisosPorFormulario.sql.

    Claves:
      <Formulario>.<Control>                 textos de labels, botones y groupbox
      <Formulario>.<Grilla>.<Columna>        encabezados de grillas
      Formularios.<ClaseFormulario>          nombre del formulario en el árbol de permisos
      Permisos.<NombrePatente>               nombre del botón en el árbol de permisos

    Es idempotente: inserta las claves faltantes y actualiza las existentes.
*/

USE GestionTurnosMedicos;
GO

DECLARE @Trad TABLE (Clave VARCHAR(150) COLLATE DATABASE_DEFAULT, Es NVARCHAR(255), En NVARCHAR(255));

INSERT INTO @Trad VALUES
-- Menú principal
('frmPrincipal.btn_GestionUsuarios',              N'Gestion de Usuarios',               N'User Management'),
('frmPrincipal.btn_SubUsuariosABM',               N'ABM de Usuarios',                   N'Users (CRUD)'),
('frmPrincipal.btn_SubUsuariosRoles',             N'Asignación de Roles',               N'Role Assignment'),
('frmPrincipal.btn_SubUsuariosHistorial',         N'Historial',                         N'History'),

-- ABM de Usuarios
('frmUsuarioABM.groupBox_DatosUsuario',           N'Datos del Usuario',                 N'User Data'),
('frmUsuarioABM.groupBoxUsuarios',                N'Usuarios',                          N'Users'),
('frmUsuarioABM.lbl_Nombre',                      N'Nombre',                            N'Name'),
('frmUsuarioABM.lbl_Apellido',                    N'Apellido',                          N'Last Name'),
('frmUsuarioABM.lbl_Email',                       N'Email',                             N'Email'),
('frmUsuarioABM.lbl_Contrasena',                  N'Contraseña',                        N'Password'),
('frmUsuarioABM.lbl_Estado',                      N'Estado',                            N'Status'),
('frmUsuarioABM.btnAgregar',                      N'Agregar',                           N'Add'),
('frmUsuarioABM.btnModificar',                    N'Modificar',                         N'Modify'),
('frmUsuarioABM.btnEliminar',                     N'Eliminar',                          N'Delete'),
('frmUsuarioABM.Activo',                          N'Activo',                            N'Active'),
('frmUsuarioABM.Inactivo',                        N'Inactivo',                          N'Inactive'),
('frmUsuarioABM.dgvUsuarios.IdUsuario',           N'ID',                                N'ID'),
('frmUsuarioABM.dgvUsuarios.Nombre',              N'Nombre',                            N'Name'),
('frmUsuarioABM.dgvUsuarios.Apellido',            N'Apellido',                          N'Last Name'),
('frmUsuarioABM.dgvUsuarios.Email',               N'Email',                             N'Email'),
('frmUsuarioABM.dgvUsuarios.IntentosFallidos',    N'Intentos fallidos',                 N'Failed attempts'),
('frmUsuarioABM.dgvUsuarios.Activo',              N'Activo',                            N'Active'),

-- Asignación de Roles
('frmUsuarioRoles.lbl_Rol',                       N'Rol:',                              N'Role:'),
('frmUsuarioRoles.Todos',                         N'(Todos)',                           N'(All)'),
('frmUsuarioRoles.lbl_Usuario',                   N'Usuario:',                          N'User:'),
('frmUsuarioRoles.groupBoxRoles',                 N'Roles',                             N'Roles'),
('frmUsuarioRoles.groupBoxPermisosDirectos',      N'Permisos directos (página o botón)', N'Direct permissions (page or button)'),
('frmUsuarioRoles.groupBoxPermisosEfectivos',     N'Permisos efectivos del usuario',    N'User effective permissions'),
('frmUsuarioRoles.btnGuardarRoles',               N'Guardar Roles',                     N'Save Roles'),
('frmUsuarioRoles.btnGuardarPermisos',            N'Guardar Permisos',                  N'Save Permissions'),

-- Historial
('frmUsuarioHistorial.GroupBoxUsuarioHistorial',  N'Historial',                         N'History'),
('frmUsuarioHistorial.lbl_Usuario',               N'Usuario:',                          N'User:'),
('frmUsuarioHistorial.btnRestaurar',              N'Restaurar',                         N'Restore'),
('frmUsuarioHistorial.dgvHistorial.FechaCambio',  N'Fecha',                             N'Date'),
('frmUsuarioHistorial.dgvHistorial.Usuario',      N'Modificado por',                    N'Modified by'),
('frmUsuarioHistorial.dgvHistorial.Accion',       N'Acción',                            N'Action'),
('frmUsuarioHistorial.dgvHistorial.Campo',        N'Campo',                             N'Field'),
('frmUsuarioHistorial.dgvHistorial.ValorAnterior', N'Valor anterior',                   N'Previous value'),
('frmUsuarioHistorial.dgvHistorial.ValorNuevo',   N'Valor nuevo',                       N'New value'),

-- Gestión de Roles
('frmRolesPermisos.groupBoxRoles',                N'Roles',                             N'Roles'),
('frmRolesPermisos.lblNombre',                    N'Nombre',                            N'Name'),
('frmRolesPermisos.lblDescripcion',               N'Descripción',                       N'Description'),
('frmRolesPermisos.btnNuevoRol',                  N'Nuevo',                             N'New'),
('frmRolesPermisos.btnModificarRol',              N'Modificar',                         N'Modify'),
('frmRolesPermisos.btnEliminarRol',               N'Eliminar',                          N'Delete'),
('frmRolesPermisos.groupBoxPermisos',             N'Formularios y permisos del rol',    N'Role forms and permissions'),
('frmRolesPermisos.lblAyuda',                     N'Tildar un formulario = toda la página. Tildar solo botones = únicamente esos botones.',
                                                  N'Check a form = whole page. Check only buttons = just those buttons.'),
('frmRolesPermisos.btnGuardarPermisos',           N'Guardar Permisos',                  N'Save Permissions'),

-- Nombres de formularios en el árbol de permisos
('Formularios.frmUsuarioABM',                     N'Usuarios - ABM',                    N'Users - CRUD'),
('Formularios.frmUsuarioRoles',                   N'Usuarios - Asignación de Roles',    N'Users - Role Assignment'),
('Formularios.frmUsuarioHistorial',               N'Usuarios - Historial',              N'Users - History'),
('Formularios.frmRolesPermisos',                  N'Gestión de Roles',                  N'Role Management'),
('Formularios.frmSeguridad',                      N'Seguridad',                         N'Security'),
('Formularios.frmBitacora',                       N'Bitácora',                          N'Log'),
('Formularios.frm_BitacoraTurno',                 N'Bitácora de Turnos',                N'Appointment Log'),
('Formularios.frmABM',                            N'Gestión de Idiomas',                N'Language Management'),
('Formularios.frm_AccesoMedico',                  N'Acceso Médico',                     N'Doctor Access'),
('Formularios.frm_AccesoPaciente',                N'Acceso Paciente',                   N'Patient Access'),
('Formularios.frmGestionMedicos',                 N'Gestión de Médicos',                N'Doctor Management'),
('Formularios.frmGestionTurnos',                  N'Gestión de Turnos',                 N'Appointment Management');

-- Nombres de los botones en el árbol de permisos (se traducen por el control, sin importar el formulario)
DECLARE @Controles TABLE (Formulario VARCHAR(50) COLLATE DATABASE_DEFAULT, Control VARCHAR(50) COLLATE DATABASE_DEFAULT, Es NVARCHAR(255), En NVARCHAR(255));
INSERT INTO @Controles VALUES
(NULL,                  'Ver',                       N'Ingresar a la pantalla',              N'Open the screen'),
('frmUsuarioABM',       'btnAgregar',                N'Agregar usuario',                     N'Add user'),
('frmUsuarioABM',       'btnModificar',              N'Modificar usuario',                   N'Modify user'),
('frmUsuarioABM',       'btnEliminar',               N'Eliminar usuario',                    N'Delete user'),
('frmUsuarioRoles',     'btnGuardarRoles',           N'Asignar / quitar roles',              N'Assign / remove roles'),
('frmUsuarioRoles',     'btnGuardarPermisos',        N'Asignar / quitar permisos directos',  N'Assign / remove direct permissions'),
('frmUsuarioHistorial', 'btnRestaurar',              N'Restaurar estado anterior',           N'Restore previous state'),
('frmRolesPermisos',    'btnNuevoRol',               N'Crear rol',                           N'Create role'),
('frmRolesPermisos',    'btnModificarRol',           N'Modificar rol',                       N'Modify role'),
('frmRolesPermisos',    'btnEliminarRol',            N'Eliminar rol',                        N'Delete role'),
('frmRolesPermisos',    'btnGuardarPermisos',        N'Asignar formularios y permisos al rol', N'Assign forms and permissions to role'),
('frmSeguridad',        'btnBackUp',                 N'Generar backup',                      N'Generate backup'),
('frmSeguridad',        'btnRestaurarBD',            N'Restaurar base de datos',             N'Restore database'),
('frmSeguridad',        'btnBloquearUsuYRecalcular', N'Bloquear usuario y recalcular DV',    N'Lock user and recalculate CD'),
('frmABM',              'btnAgregarIdioma',          N'Agregar idioma',                      N'Add language'),
('frmABM',              'btnGuardar',                N'Guardar traducciones',                N'Save translations'),
('frmABM',              'btnCargarControles',        N'Cargar controles',                    N'Load controls'),
('frmABM',              'btnEliminar',               N'Eliminar idioma',                     N'Delete language'),
('frm_AccesoMedico',    'btnAtencionMedica',         N'Emitir atención',                     N'Issue medical care'),
('frm_AccesoMedico',    'btnCancelarAtencionMedica', N'Cancelar atención',                   N'Cancel medical care'),
('frm_AccesoPaciente',  'btnReservarTurno',          N'Reservar turno',                      N'Book appointment'),
('frm_AccesoPaciente',  'btnModificarTurno',         N'Modificar turno',                     N'Modify appointment'),
('frm_AccesoPaciente',  'btnCancelarTurnos',         N'Cancelar turno',                      N'Cancel appointment'),
('frmGestionMedicos',   'btnEspecialidadCrear',      N'Crear especialidad',                  N'Create specialty'),
('frmGestionMedicos',   'btnEspecialidadEliminar',   N'Eliminar especialidad',               N'Delete specialty'),
('frmGestionMedicos',   'btnAsignarEspecialidad',    N'Asignar especialidad',                N'Assign specialty'),
('frmGestionMedicos',   'btnQuitarEspecialidad',     N'Quitar especialidad',                 N'Remove specialty'),
('frmGestionMedicos',   'btnAsignarDiaYHorario',     N'Asignar días y horarios',             N'Assign days and hours'),
('frmGestionMedicos',   'btnModificarDiasYHorarios', N'Modificar días y horarios',           N'Modify days and hours'),
('frmGestionMedicos',   'btnEditarFranjas',          N'Editar / inactivar franjas',          N'Edit / deactivate time slots'),
('frmGestionTurnos',    'btnCancelarTurno',          N'Cancelar turno',                      N'Cancel appointment');

-- Una clave por cada patente del catálogo ("Permisos.frmX.btnY")
INSERT INTO @Trad (Clave, Es, En)
SELECT 'Permisos.' + p.Nombre, c.Es, c.En
FROM dbo.Permisos p
JOIN @Controles c
  ON p.Nombre = ISNULL(c.Formulario, p.Formulario) + '.' + c.Control;

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

-- Control: patentes del catálogo que quedaron sin traducción (debería devolver 0 filas)
SELECT p.Nombre AS PatenteSinTraduccion
FROM dbo.Permisos p
WHERE NOT EXISTS (SELECT 1 FROM @Trad x WHERE x.Clave = 'Permisos.' + p.Nombre);
GO
