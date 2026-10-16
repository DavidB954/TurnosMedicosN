using BE;
using BE.Composite;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BLL
{
    public class BLL_Roles
    {
        // El rol Administrador no se puede borrar, renombrar ni recortar desde la UI,
        // y siempre tiene que quedar al menos un usuario con ese rol (evita quedar afuera del sistema).
        public const string NombreRolAdministrador = "Administrador";

        DAL_Roles dal_roles = new DAL_Roles();
        DAL_Usuario dal_usuario = new DAL_Usuario();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();

        private string ActorActual()
        {
            return Sesion.Instancia().UsuarioActual?.Nombre ?? "SISTEMA";
        }

        private void Registrar(AccionBitacora accion, string mensaje)
        {
            bll_bitacora.RegistrarEvento(Sesion.Instancia().UsuarioActual?.IdUsuario, accion, "ROLES", mensaje);
        }

        private static string Texto(DataRow row, string columna)
        {
            return row.Table.Columns.Contains(columna) && row[columna] != DBNull.Value
                ? row[columna].ToString()
                : null;
        }

        private string NombreUsuario(int idUsuario)
        {
            BE_Usuario usuario = dal_usuario.ObtenerUsuarioPorId(idUsuario);
            return usuario != null ? usuario.NombreApellido : $"ID {idUsuario}";
        }

        public static bool EsAdministrador(RolComposite rol)
        {
            return rol != null && string.Equals(rol.Nombre, NombreRolAdministrador, StringComparison.OrdinalIgnoreCase);
        }

        //// ARBOL COMPLETO (Composite)

        public RolComposite CargarArbol()
        {
            try
            {
                var (dtRoles, dtPermisos, dtRolRol, dtRolPermiso) = dal_roles.CargarArbol();

                var roles = new Dictionary<int, RolComposite>();
                var permisos = new Dictionary<int, Permiso>();

                // Crear todos los roles (comunes y familias de formulario)
                foreach (DataRow row in dtRoles.Rows)
                {
                    int id = Convert.ToInt32(row["IdRol"]);
                    roles[id] = new RolComposite
                    {
                        Id = id,
                        Nombre = row["Nombre"].ToString(),
                        Descripcion = Texto(row, "Descripcion"),
                        Formulario = Texto(row, "Formulario")
                    };
                }

                // Crear todas las patentes
                foreach (DataRow row in dtPermisos.Rows)
                {
                    int id = Convert.ToInt32(row["IdPermisos"]);
                    permisos[id] = new Permiso
                    {
                        Id = id,
                        IdPermiso = id,
                        Nombre = row["Nombre"].ToString(),
                        Descripcion = Texto(row, "Descripcion"),
                        Formulario = Texto(row, "Formulario")
                    };
                }

                // Asignar patentes a sus roles / familias
                foreach (DataRow row in dtRolPermiso.Rows)
                {
                    int idRol = Convert.ToInt32(row["IdRol"]);
                    int idPerm = Convert.ToInt32(row["IdPermisos"]);
                    if (roles.ContainsKey(idRol) && permisos.ContainsKey(idPerm))
                        roles[idRol].Agregar(permisos[idPerm]);
                }

                // Construir jerarquía rol-rol (un rol contiene familias de formulario u otros roles)
                foreach (DataRow row in dtRolRol.Rows)
                {
                    int idPadre = Convert.ToInt32(row["IdRolPadre"]);
                    int idHijo = Convert.ToInt32(row["IdRolHijo"]);
                    if (roles.ContainsKey(idPadre) && roles.ContainsKey(idHijo))
                        roles[idPadre].Agregar(ClonarRol(roles[idHijo]));
                }

                // Raíz virtual con todos los roles
                var raiz = new RolComposite { Id = 0, Nombre = "Roles" };
                foreach (var rol in roles.Values)
                    raiz.Agregar(rol);

                return raiz;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private RolComposite ClonarRol(RolComposite original)
        {
            var clon = new RolComposite
            {
                Id = original.Id,
                Nombre = original.Nombre,
                Descripcion = original.Descripcion,
                Formulario = original.Formulario,
                EsReferencia = true
            };

            foreach (var hijo in original.Hijos())
            {
                if (hijo is RolComposite subRol)
                    clon.Agregar(ClonarRol(subRol));
                else if (hijo is Permiso p)
                    clon.Agregar(new Permiso { Id = p.Id, IdPermiso = p.IdPermiso, Nombre = p.Nombre, Descripcion = p.Descripcion, Formulario = p.Formulario });
            }

            return clon;
        }

        // Catálogo: una familia por formulario, con sus patentes (botones) como hijos.
        public List<RolComposite> ObtenerCatalogo()
        {
            try
            {
                return CargarArbol().Hijos()
                    .OfType<RolComposite>()
                    .Where(r => r.EsFormulario)
                    .OrderBy(r => r.Nombre)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Solo roles comunes (sin las familias de formulario)
        public List<RolComposite> ObtenerRoles()
        {
            try
            {
                return CargarArbol().Hijos()
                    .OfType<RolComposite>()
                    .Where(r => !r.EsFormulario)
                    .OrderBy(r => r.Nombre)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Permiso> ObtenerPermisos()
        {
            try
            {
                var permisos = new List<Permiso>();

                foreach (DataRow row in dal_roles.ObtenerTodosLosPermisos().Rows)
                {
                    int id = Convert.ToInt32(row["IdPermisos"]);
                    permisos.Add(new Permiso
                    {
                        Id = id,
                        IdPermiso = id,
                        Nombre = row["Nombre"].ToString(),
                        Descripcion = Texto(row, "Descripcion"),
                        Formulario = Texto(row, "Formulario")
                    });
                }
                return permisos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //// ABM DE ROLES

        private void ValidarNombreRol(string nombre, int? idExcluir)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Ingrese el nombre del rol.");

            if (nombre.Trim().Length > 50)
                throw new Exception("El nombre del rol no puede superar los 50 caracteres.");

            if (dal_roles.ExisteNombreRol(nombre.Trim(), idExcluir))
                throw new Exception("Ya existe un rol (o formulario) con ese nombre.");
        }

        private RolComposite ObtenerRolComun(int idRol)
        {
            RolComposite rol = ObtenerRoles().FirstOrDefault(r => r.Id == idRol);
            if (rol == null)
                throw new Exception("El rol seleccionado no existe.");
            return rol;
        }

        public int CrearRol(string nombre, string descripcion)
        {
            try
            {
                ValidarNombreRol(nombre, null);

                int idRol = dal_roles.InsertarRol(nombre.Trim(), string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim());

                Registrar(AccionBitacora.ROL_ALTA, $"Usuario {ActorActual()} crea el rol {nombre.Trim()}");

                return idRol;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModificarRol(int idRol, string nombre, string descripcion)
        {
            try
            {
                RolComposite rol = ObtenerRolComun(idRol);

                if (EsAdministrador(rol) && !string.Equals(rol.Nombre, nombre?.Trim(), StringComparison.OrdinalIgnoreCase))
                    throw new Exception("El rol Administrador no se puede renombrar.");

                ValidarNombreRol(nombre, idRol);

                dal_roles.ModificarRol(idRol, nombre.Trim(), string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim());

                Registrar(AccionBitacora.ROL_MODIFICACION, $"Usuario {ActorActual()} modifica el rol {rol.Nombre} -> {nombre.Trim()}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarRol(int idRol)
        {
            try
            {
                RolComposite rol = ObtenerRolComun(idRol);

                if (EsAdministrador(rol))
                    throw new Exception("El rol Administrador no se puede eliminar.");

                int cantidad = dal_roles.CantidadUsuariosConRol(idRol);
                if (cantidad > 0)
                    throw new Exception($"El rol está asignado a {cantidad} usuario(s). Quíteselo antes de eliminarlo.");

                dal_roles.EliminarRol(idRol);

                Registrar(AccionBitacora.ROL_BAJA, $"Usuario {ActorActual()} elimina el rol {rol.Nombre}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Deja al rol con exactamente esas familias de formulario (página completa)
        // y esas patentes sueltas (botones). Calcula la diferencia contra lo que ya tiene.
        public void GuardarPermisosRol(int idRol, List<int> idsFormularios, List<int> idsPermisos)
        {
            try
            {
                RolComposite rol = ObtenerRolComun(idRol);

                if (EsAdministrador(rol))
                    throw new Exception("El rol Administrador siempre tiene acceso a todos los formularios y no se puede modificar.");

                var formulariosActuales = rol.Hijos().OfType<RolComposite>().Where(r => r.EsFormulario).Select(r => r.Id).ToList();
                var permisosActuales = rol.Hijos().OfType<Permiso>().Select(p => p.Id).ToList();

                foreach (int id in idsFormularios.Except(formulariosActuales))
                    dal_roles.AsignarRolHijo(idRol, id);
                foreach (int id in formulariosActuales.Except(idsFormularios))
                    dal_roles.DesasignarRolHijo(idRol, id);

                foreach (int id in idsPermisos.Except(permisosActuales))
                    dal_roles.AsignarPermisoARol(idRol, id);
                foreach (int id in permisosActuales.Except(idsPermisos))
                    dal_roles.DesasignarPermisoDeRol(idRol, id);

                Registrar(AccionBitacora.ROL_PERMISOS_MODIFICADOS,
                    $"Usuario {ActorActual()} actualiza los permisos del rol {rol.Nombre}: {idsFormularios.Count} formulario(s) completo(s), {idsPermisos.Count} boton(es) sueltos");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //// USUARIOS

        // Roles y familias de formulario asignados al usuario (Usuario_Rol), ya armados con sus hijos.
        public List<RolComposite> ObtenerRolesDeUsuario(int idUsuario)
        {
            try
            {
                RolComposite arbolCompleto = CargarArbol();

                var idsRolesUsuario = new HashSet<int>();
                foreach (DataRow row in dal_roles.ObtenerRolesPorUsuario(idUsuario).Rows)
                    idsRolesUsuario.Add(Convert.ToInt32(row["IdRol"]));

                return arbolCompleto.Hijos()
                    .OfType<RolComposite>()
                    .Where(r => idsRolesUsuario.Contains(r.Id))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<int> ObtenerIdsUsuariosConRol(int idRol)
        {
            try
            {
                return dal_roles.ObtenerIdsUsuariosConRol(idRol);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Permiso> ObtenerPermisosDirectosUsuario(int idUsuario)
        {
            try
            {
                var lista = new List<Permiso>();

                foreach (DataRow row in dal_roles.ObtenerPermisosDirectosUsuario(idUsuario).Rows)
                {
                    int id = Convert.ToInt32(row["IdPermisos"]);
                    lista.Add(new Permiso
                    {
                        Id = id,
                        IdPermiso = id,
                        Nombre = row["Nombre"].ToString(),
                        Descripcion = Texto(row, "Descripcion"),
                        Formulario = Texto(row, "Formulario")
                    });
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Todo lo que puede hacer el usuario, como un único Composite:
        // sus roles + familias de formulario directas + patentes directas.
        public RolComposite ObtenerPermisosUsuario(int idUsuario)
        {
            try
            {
                var raiz = new RolComposite { Id = 0, Nombre = "Permisos del usuario" };

                foreach (var rol in ObtenerRolesDeUsuario(idUsuario))
                    raiz.Agregar(rol);

                foreach (var permiso in ObtenerPermisosDirectosUsuario(idUsuario))
                    raiz.Agregar(permiso);

                return raiz;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Se llama una vez al iniciar sesión.
        public void CargarSeguridadEnSesion(int idUsuario)
        {
            try
            {
                Sesion sesion = Sesion.Instancia();
                RolComposite permisos = ObtenerPermisosUsuario(idUsuario);

                sesion.Permisos = permisos;
                sesion.RolesActuales = permisos.Hijos().OfType<RolComposite>().ToList();
                sesion.CatalogoPermisos = new HashSet<string>(ObtenerPermisos().Select(p => p.Nombre));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AgregarRolUsuario(int idUsuario, int idRol)
        {
            try
            {
                dal_roles.AgregarRolUsuario(idUsuario, idRol);

                string nombreRol = dal_roles.ObtenerNombreRol(idRol) ?? $"ID {idRol}";
                Registrar(AccionBitacora.ROL_ASIGNADO_USUARIO,
                    $"Usuario {ActorActual()} asigna el rol {nombreRol} al usuario {NombreUsuario(idUsuario)}, ID: {idUsuario}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void QuitarRolUsuario(int idUsuario, int idRol)
        {
            try
            {
                string nombreRol = dal_roles.ObtenerNombreRol(idRol) ?? $"ID {idRol}";

                if (string.Equals(nombreRol, NombreRolAdministrador, StringComparison.OrdinalIgnoreCase)
                    && dal_roles.CantidadUsuariosConRol(idRol) <= 1)
                    throw new Exception("No se puede quitar el rol Administrador al último usuario que lo tiene.");

                dal_roles.QuitarRolUsuario(idUsuario, idRol);

                Registrar(AccionBitacora.ROL_QUITADO_USUARIO,
                    $"Usuario {ActorActual()} quita el rol {nombreRol} al usuario {NombreUsuario(idUsuario)}, ID: {idUsuario}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Deja al usuario con exactamente esos roles comunes (no toca las familias de formulario directas).
        public void GuardarRolesUsuario(int idUsuario, List<int> idsRoles)
        {
            try
            {
                var actuales = ObtenerRolesDeUsuario(idUsuario).Where(r => !r.EsFormulario).Select(r => r.Id).ToList();

                foreach (int id in idsRoles.Except(actuales))
                    AgregarRolUsuario(idUsuario, id);
                foreach (int id in actuales.Except(idsRoles))
                    QuitarRolUsuario(idUsuario, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Permisos directos del usuario: familias de formulario (página completa, van a Usuario_Rol)
        // y patentes sueltas (un botón, van a Usuario_Permiso).
        public void GuardarPermisosDirectosUsuario(int idUsuario, List<int> idsFormularios, List<int> idsPermisos)
        {
            try
            {
                var formulariosActuales = ObtenerRolesDeUsuario(idUsuario).Where(r => r.EsFormulario).Select(r => r.Id).ToList();
                var permisosActuales = ObtenerPermisosDirectosUsuario(idUsuario).Select(p => p.Id).ToList();

                foreach (int id in idsFormularios.Except(formulariosActuales))
                    AgregarRolUsuario(idUsuario, id);
                foreach (int id in formulariosActuales.Except(idsFormularios))
                    QuitarRolUsuario(idUsuario, id);

                string nombreUsuario = NombreUsuario(idUsuario);

                foreach (int id in idsPermisos.Except(permisosActuales))
                {
                    dal_roles.AsignarPermisoUsuario(idUsuario, id);
                    Registrar(AccionBitacora.PERMISO_ASIGNADO_USUARIO,
                        $"Usuario {ActorActual()} asigna el permiso {dal_roles.ObtenerNombrePermiso(id)} al usuario {nombreUsuario}, ID: {idUsuario}");
                }
                foreach (int id in permisosActuales.Except(idsPermisos))
                {
                    dal_roles.QuitarPermisoUsuario(idUsuario, id);
                    Registrar(AccionBitacora.PERMISO_QUITADO_USUARIO,
                        $"Usuario {ActorActual()} quita el permiso {dal_roles.ObtenerNombrePermiso(id)} al usuario {nombreUsuario}, ID: {idUsuario}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
