/*
    Script de actualización — Permisos por formulario (Composite Familia / Patente)
    Base: GestionTurnosMedicos

    Modelo:
      - Patente  = un botón (o el acceso "Ver") de un formulario. Tabla Permisos.
                   Nombre = clave usada por el código: "<ClaseFormulario>.<NombreControl>"
                   Formulario = clase del formulario, Descripcion = texto para mostrar.
      - Familia de formulario = fila de Rol con Formulario NO NULL. Agrupa (Rol_Permiso)
                   todas las patentes de ese formulario. Asignarla = acceso a la página completa.
      - Rol      = fila de Rol con Formulario NULL. Contiene familias de formulario (Rol_Rol)
                   y/o patentes sueltas (Rol_Permiso).
      - Usuario  = roles o familias de formulario (Usuario_Rol) + patentes sueltas
                   directas (Usuario_Permiso, tabla nueva).

    Es idempotente: se puede ejecutar más de una vez sin error.
*/

USE GestionTurnosMedicos;
GO

-- 1) Ampliar los nombres: las claves "frmUsuarioABM.btnAgregar" no entran en VARCHAR(20).
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UQ_Permisos_Nombre' AND object_id = OBJECT_ID('dbo.Permisos'))
    ALTER TABLE dbo.Permisos DROP CONSTRAINT UQ_Permisos_Nombre;
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UQ_Rol_Nombre' AND object_id = OBJECT_ID('dbo.Rol'))
    ALTER TABLE dbo.Rol DROP CONSTRAINT UQ_Rol_Nombre;
GO

ALTER TABLE dbo.Permisos ALTER COLUMN Nombre VARCHAR(100) NOT NULL;
ALTER TABLE dbo.Rol      ALTER COLUMN Nombre VARCHAR(50)  NOT NULL;
GO

ALTER TABLE dbo.Permisos ADD CONSTRAINT UQ_Permisos_Nombre UNIQUE (Nombre);
ALTER TABLE dbo.Rol      ADD CONSTRAINT UQ_Rol_Nombre UNIQUE (Nombre);
GO

-- 2) Columna Formulario en Permisos y en Rol (NULL en Rol = rol "real").
IF COL_LENGTH('dbo.Permisos', 'Formulario') IS NULL
    ALTER TABLE dbo.Permisos ADD Formulario VARCHAR(50) NULL;
IF COL_LENGTH('dbo.Rol', 'Formulario') IS NULL
    ALTER TABLE dbo.Rol ADD Formulario VARCHAR(50) NULL;
GO

-- 3) Patentes asignadas directamente a un usuario (un solo botón para un solo usuario).
IF OBJECT_ID('dbo.Usuario_Permiso') IS NULL
BEGIN
    CREATE TABLE dbo.Usuario_Permiso
    (
        IdUsuario  INT NOT NULL,
        IdPermisos INT NOT NULL,
        CONSTRAINT PK_Usuario_Permiso PRIMARY KEY (IdUsuario, IdPermisos),
        CONSTRAINT FK_Usuario_Permiso_Usuario  FOREIGN KEY (IdUsuario)  REFERENCES dbo.Usuario (IdUsuario),
        CONSTRAINT FK_Usuario_Permiso_Permisos FOREIGN KEY (IdPermisos) REFERENCES dbo.Permisos (IdPermisos)
    );
END
GO

-- 4) Catálogo de formularios y sus patentes.
--    Para agregar un botón nuevo al sistema de permisos alcanza con sumar una fila acá:
--    frmBase oculta automáticamente cualquier botón cuyo "<Formulario>.<Control>" esté en el catálogo.
IF OBJECT_ID('tempdb..#Catalogo') IS NOT NULL DROP TABLE #Catalogo;
CREATE TABLE #Catalogo
(
    Formulario  VARCHAR(50)  COLLATE DATABASE_DEFAULT,
    Familia     VARCHAR(50)  COLLATE DATABASE_DEFAULT,
    Control     VARCHAR(50)  COLLATE DATABASE_DEFAULT,
    Descripcion VARCHAR(100) COLLATE DATABASE_DEFAULT
);

INSERT INTO #Catalogo (Formulario, Familia, Control, Descripcion) VALUES
('frmUsuarioABM',       'Usuarios - ABM',                 'Ver',                        'Ingresar a la pantalla'),
('frmUsuarioABM',       'Usuarios - ABM',                 'btnAgregar',                 'Agregar usuario'),
('frmUsuarioABM',       'Usuarios - ABM',                 'btnModificar',               'Modificar usuario'),
('frmUsuarioABM',       'Usuarios - ABM',                 'btnEliminar',                'Eliminar usuario'),

('frmUsuarioRoles',     'Usuarios - Asignacion de Roles', 'Ver',                        'Ingresar a la pantalla'),
('frmUsuarioRoles',     'Usuarios - Asignacion de Roles', 'btnGuardarRoles',            'Asignar / quitar roles'),
('frmUsuarioRoles',     'Usuarios - Asignacion de Roles', 'btnGuardarPermisos',         'Asignar / quitar permisos directos'),

('frmUsuarioHistorial', 'Usuarios - Historial',           'Ver',                        'Ingresar a la pantalla'),
('frmUsuarioHistorial', 'Usuarios - Historial',           'btnRestaurar',               'Restaurar estado anterior'),

('frmRolesPermisos',    'Gestion de Roles',               'Ver',                        'Ingresar a la pantalla'),
('frmRolesPermisos',    'Gestion de Roles',               'btnNuevoRol',                'Crear rol'),
('frmRolesPermisos',    'Gestion de Roles',               'btnModificarRol',            'Modificar rol'),
('frmRolesPermisos',    'Gestion de Roles',               'btnEliminarRol',             'Eliminar rol'),
('frmRolesPermisos',    'Gestion de Roles',               'btnGuardarPermisos',         'Asignar formularios y permisos al rol'),

('frmSeguridad',        'Seguridad',                      'Ver',                        'Ingresar a la pantalla'),
('frmSeguridad',        'Seguridad',                      'btnBackUp',                  'Generar backup'),
('frmSeguridad',        'Seguridad',                      'btnRestaurarBD',             'Restaurar base de datos'),
('frmSeguridad',        'Seguridad',                      'btnBloquearUsuYRecalcular',  'Bloquear usuario y recalcular DV'),

('frmBitacora',         'Bitacora',                       'Ver',                        'Ingresar a la pantalla'),

('frm_BitacoraTurno',   'Bitacora de Turnos',             'Ver',                        'Ingresar a la pantalla'),

('frmABM',              'Gestion de Idiomas',             'Ver',                        'Ingresar a la pantalla'),
('frmABM',              'Gestion de Idiomas',             'btnAgregarIdioma',           'Agregar idioma'),
('frmABM',              'Gestion de Idiomas',             'btnGuardar',                 'Guardar traducciones'),
('frmABM',              'Gestion de Idiomas',             'btnCargarControles',         'Cargar controles'),
('frmABM',              'Gestion de Idiomas',             'btnEliminar',                'Eliminar idioma'),

('frm_AccesoMedico',    'Acceso Medico',                  'Ver',                        'Ingresar a la pantalla'),
('frm_AccesoMedico',    'Acceso Medico',                  'btnAtencionMedica',          'Emitir atencion'),
('frm_AccesoMedico',    'Acceso Medico',                  'btnCancelarAtencionMedica',  'Cancelar atencion'),

('frm_AccesoPaciente',  'Acceso Paciente',                'Ver',                        'Ingresar a la pantalla'),
('frm_AccesoPaciente',  'Acceso Paciente',                'btnReservarTurno',           'Reservar turno'),
('frm_AccesoPaciente',  'Acceso Paciente',                'btnModificarTurno',          'Modificar turno'),
('frm_AccesoPaciente',  'Acceso Paciente',                'btnCancelarTurnos',          'Cancelar turno'),

('frmGestionMedicos',   'Gestion de Medicos',             'Ver',                        'Ingresar a la pantalla'),
('frmGestionMedicos',   'Gestion de Medicos',             'btnEspecialidadCrear',       'Crear especialidad'),
('frmGestionMedicos',   'Gestion de Medicos',             'btnEspecialidadEliminar',    'Eliminar especialidad'),
('frmGestionMedicos',   'Gestion de Medicos',             'btnAsignarEspecialidad',     'Asignar especialidad'),
('frmGestionMedicos',   'Gestion de Medicos',             'btnQuitarEspecialidad',      'Quitar especialidad'),
('frmGestionMedicos',   'Gestion de Medicos',             'btnAsignarDiaYHorario',      'Asignar dias y horarios'),
('frmGestionMedicos',   'Gestion de Medicos',             'btnModificarDiasYHorarios',  'Modificar dias y horarios'),
('frmGestionMedicos',   'Gestion de Medicos',             'btnEditarFranjas',           'Editar / inactivar franjas'),

('frmGestionTurnos',    'Gestion de Turnos',              'Ver',                        'Ingresar a la pantalla'),
('frmGestionTurnos',    'Gestion de Turnos',              'btnCancelarTurno',           'Cancelar turno');

-- 4.a) Familias de formulario (filas de Rol con Formulario)
INSERT INTO dbo.Rol (Nombre, Descripcion, Formulario)
SELECT DISTINCT c.Familia, 'Formulario ' + c.Formulario, c.Formulario
FROM #Catalogo c
WHERE NOT EXISTS (SELECT 1 FROM dbo.Rol r WHERE r.Formulario = c.Formulario);

-- 4.b) Patentes
INSERT INTO dbo.Permisos (Nombre, Descripcion, Formulario)
SELECT c.Formulario + '.' + c.Control, c.Descripcion, c.Formulario
FROM #Catalogo c
WHERE NOT EXISTS (SELECT 1 FROM dbo.Permisos p WHERE p.Nombre = c.Formulario + '.' + c.Control);

UPDATE p SET p.Descripcion = c.Descripcion, p.Formulario = c.Formulario
FROM dbo.Permisos p
JOIN #Catalogo c ON p.Nombre = c.Formulario + '.' + c.Control;

-- 4.c) Cada familia contiene todas las patentes de su formulario
INSERT INTO dbo.Rol_Permiso (IdRol, IdPermisos)
SELECT r.IdRol, p.IdPermisos
FROM dbo.Rol r
JOIN dbo.Permisos p ON p.Formulario = r.Formulario
WHERE NOT EXISTS (SELECT 1 FROM dbo.Rol_Permiso rp WHERE rp.IdRol = r.IdRol AND rp.IdPermisos = p.IdPermisos);
GO

-- 5) Los permisos viejos escritos a mano (sin formulario) ya no los verifica ningún código.
DELETE rp FROM dbo.Rol_Permiso rp
JOIN dbo.Permisos p ON p.IdPermisos = rp.IdPermisos
WHERE p.Formulario IS NULL;

DELETE up FROM dbo.Usuario_Permiso up
JOIN dbo.Permisos p ON p.IdPermisos = up.IdPermisos
WHERE p.Formulario IS NULL;

DELETE FROM dbo.Permisos WHERE Formulario IS NULL;
GO

-- 6) Permisos iniciales de los roles existentes (equivalen a lo que antes estaba fijo en frmPrincipal).
--    El Administrador recibe TODAS las familias de formulario.
INSERT INTO dbo.Rol_Rol (IdRolPadre, IdRolHijo)
SELECT padre.IdRol, fam.IdRol
FROM dbo.Rol padre
JOIN dbo.Rol fam ON fam.Formulario IS NOT NULL
WHERE padre.Nombre = 'Administrador'
  AND NOT EXISTS (SELECT 1 FROM dbo.Rol_Rol rr WHERE rr.IdRolPadre = padre.IdRol AND rr.IdRolHijo = fam.IdRol);

DECLARE @Defaults TABLE (Rol VARCHAR(50) COLLATE DATABASE_DEFAULT, Formulario VARCHAR(50) COLLATE DATABASE_DEFAULT);
INSERT INTO @Defaults VALUES
('Administrativo', 'frmABM'),
('Administrativo', 'frmGestionTurnos'),
('Medico',         'frm_AccesoMedico'),
('Paciente',       'frm_AccesoPaciente');

INSERT INTO dbo.Rol_Rol (IdRolPadre, IdRolHijo)
SELECT padre.IdRol, fam.IdRol
FROM @Defaults d
JOIN dbo.Rol padre ON padre.Nombre = d.Rol AND padre.Formulario IS NULL
JOIN dbo.Rol fam   ON fam.Formulario = d.Formulario
WHERE NOT EXISTS (SELECT 1 FROM dbo.Rol_Rol rr WHERE rr.IdRolPadre = padre.IdRol AND rr.IdRolHijo = fam.IdRol);
GO

-- 7) Traducciones de las pantallas nuevas.
--    Las de frmUsuarioABM / frmUsuarioHistorial se copian de frmUsuario (mismos controles).
INSERT INTO dbo.Traducciones (Clave, IdIdioma, Traduccion)
SELECT REPLACE(t.Clave, 'frmUsuario.', 'frmUsuarioABM.'), t.IdIdioma, t.Traduccion
FROM dbo.Traducciones t
WHERE t.Clave IN ('frmUsuario.groupBox_DatosUsuario', 'frmUsuario.groupBoxUsuarios', 'frmUsuario.lbl_Nombre',
                  'frmUsuario.lbl_Apellido', 'frmUsuario.lbl_Email', 'frmUsuario.lbl_Contrasena',
                  'frmUsuario.lbl_Estado', 'frmUsuario.btnAgregar', 'frmUsuario.btnModificar', 'frmUsuario.btnEliminar')
  AND NOT EXISTS (SELECT 1 FROM dbo.Traducciones x
                  WHERE x.Clave = REPLACE(t.Clave, 'frmUsuario.', 'frmUsuarioABM.') AND x.IdIdioma = t.IdIdioma);

INSERT INTO dbo.Traducciones (Clave, IdIdioma, Traduccion)
SELECT REPLACE(t.Clave, 'frmUsuario.', 'frmUsuarioHistorial.'), t.IdIdioma, t.Traduccion
FROM dbo.Traducciones t
WHERE (t.Clave IN ('frmUsuario.GroupBoxUsuarioHistorial', 'frmUsuario.btnRestaurar') OR t.Clave LIKE 'frmUsuario.dgvHistorial.%')
  AND NOT EXISTS (SELECT 1 FROM dbo.Traducciones x
                  WHERE x.Clave = REPLACE(t.Clave, 'frmUsuario.', 'frmUsuarioHistorial.') AND x.IdIdioma = t.IdIdioma);

DECLARE @Trad TABLE (Clave VARCHAR(150) COLLATE DATABASE_DEFAULT, Es NVARCHAR(255), En NVARCHAR(255));
INSERT INTO @Trad VALUES
('frmPrincipal.btn_SubUsuariosABM',              N'ABM de Usuarios',                      N'Users (CRUD)'),
('frmPrincipal.btn_SubUsuariosRoles',            N'Asignación de Roles',                  N'Role Assignment'),
('frmPrincipal.btn_SubUsuariosHistorial',        N'Historial',                            N'History'),
('frmPrincipal.btn_GestionMedicos',              N'Gestion Medicos',                      N'Doctor Management'),
('frmPrincipal.btn_GestionTurnos',               N'Gestion de Turnos',                    N'Appointment Management'),

('frmUsuarioHistorial.lbl_Usuario',              N'Usuario:',                             N'User:'),

('frmUsuarioRoles.lbl_Usuario',                  N'Usuario:',                             N'User:'),
('frmUsuarioRoles.groupBoxRoles',                N'Roles',                                N'Roles'),
('frmUsuarioRoles.groupBoxPermisosDirectos',     N'Permisos directos (página o botón)',   N'Direct permissions (page or button)'),
('frmUsuarioRoles.groupBoxPermisosEfectivos',    N'Permisos efectivos del usuario',       N'User effective permissions'),
('frmUsuarioRoles.btnGuardarRoles',              N'Guardar Roles',                        N'Save Roles'),
('frmUsuarioRoles.btnGuardarPermisos',           N'Guardar Permisos',                     N'Save Permissions'),

('frmRolesPermisos.groupBoxRoles',               N'Roles',                                N'Roles'),
('frmRolesPermisos.lblNombre',                   N'Nombre',                               N'Name'),
('frmRolesPermisos.lblDescripcion',              N'Descripción',                          N'Description'),
('frmRolesPermisos.btnNuevoRol',                 N'Nuevo',                                N'New'),
('frmRolesPermisos.btnModificarRol',             N'Modificar',                            N'Modify'),
('frmRolesPermisos.btnEliminarRol',              N'Eliminar',                             N'Delete'),
('frmRolesPermisos.groupBoxPermisos',            N'Formularios y permisos del rol',       N'Role forms and permissions'),
('frmRolesPermisos.lblAyuda',                    N'Tildar un formulario = toda la página. Tildar solo botones = únicamente esos botones.',
                                                 N'Check a form = whole page. Check only buttons = just those buttons.'),
('frmRolesPermisos.btnGuardarPermisos',          N'Guardar Permisos',                     N'Save Permissions');

INSERT INTO dbo.Traducciones (Clave, IdIdioma, Traduccion)
SELECT t.Clave, i.Id, CASE i.Codigo WHEN 'EN' THEN t.En ELSE t.Es END
FROM @Trad t
CROSS JOIN dbo.Idiomas i
WHERE i.Codigo IN ('ES', 'EN')
  AND NOT EXISTS (SELECT 1 FROM dbo.Traducciones x WHERE x.Clave = t.Clave AND x.IdIdioma = i.Id);
GO
