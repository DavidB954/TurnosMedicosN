using BE;
using BE.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Sesion
    {
        private static Sesion instancia;

        private static readonly object _lock = new object();
        public BE_Usuario UsuarioActual
        {
            get; set;
        }

        // Roles del usuario logueado, cargados una vez al iniciar sesión
        public List<RolComposite> RolesActuales { get; set; } = new List<RolComposite>();

        private Sesion()
        {
        }

        public static Sesion Instancia()
        {
            if (instancia == null)
            {
                lock (_lock)
                {
                    if (instancia == null)
                    {
                        instancia = new Sesion();
                    }
                        
                }
            }
            return instancia;
        }

        // Devuelve true si alguno de los roles del usuario logueado tiene el nombre indicado.
        // Comparación case-insensitive para evitar falsos negativos por mayúsculas/minúsculas.
        public bool TieneRol(string nombreRol)
        {
            if (RolesActuales == null || RolesActuales.Count == 0)
                return false;

            return RolesActuales.Any(r =>
                string.Equals(r.Nombre, nombreRol, StringComparison.OrdinalIgnoreCase));
        }

        // Devuelve true si alguno de los roles del usuario logueado tiene el nombre indicado
        // o si alguno de sus roles/permisos hijos coincide con el permiso buscado.
        // El rol "Administrador" siempre tiene acceso (bypass), para no quedar bloqueado
        // mientras se van creando y asignando permisos granulares nuevos.
        public bool TienePermiso(string permiso)
        {
            if (TieneRol("Administrador"))
                return true;

            if (RolesActuales == null || RolesActuales.Count == 0)
                return false;

            return RolesActuales.Any(r => r.TienePermiso(permiso));
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
            RolesActuales = new List<RolComposite>();
            instancia = null;
        }
    }

}
