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

        public List<RolComposite> RolesActuales { get; set; } = new List<RolComposite>();

        public RolComposite Permisos { get; set; } = new RolComposite { Nombre = "Permisos del usuario" };

        public HashSet<string> CatalogoPermisos { get; set; } = new HashSet<string>();

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

        public bool TieneRol(string nombreRol)
        {
            if (RolesActuales == null || RolesActuales.Count == 0)
                return false;

            return RolesActuales.Any(r =>
                string.Equals(r.Nombre, nombreRol, StringComparison.OrdinalIgnoreCase));
        }

        public bool TienePermiso(string permiso)
        {
            return Permisos != null && Permisos.TienePermiso(permiso);
        }

        public bool EstaEnCatalogo(string permiso)
        {
            return CatalogoPermisos != null && CatalogoPermisos.Contains(permiso);
        }

        public bool TieneAccesoFormulario(string formulario)
        {
            return TienePatenteDeFormulario(Permisos, formulario);
        }

        private bool TienePatenteDeFormulario(Componente componente, string formulario)
        {
            if (componente is Permiso permiso)
                return string.Equals(permiso.Formulario, formulario, StringComparison.OrdinalIgnoreCase);

            if (componente is RolComposite rol)
                return rol.Hijos().Any(h => TienePatenteDeFormulario(h, formulario));

            return false;
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
            RolesActuales = new List<RolComposite>();
            Permisos = new RolComposite { Nombre = "Permisos del usuario" };
            CatalogoPermisos = new HashSet<string>();
            instancia = null;
        }
    }

}
