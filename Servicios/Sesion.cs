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

        // Todo lo que puede hacer el usuario logueado (roles + permisos directos) como un único Composite
        public RolComposite Permisos { get; set; } = new RolComposite { Nombre = "Permisos del usuario" };

        // Nombres de todas las patentes del catálogo ("frmX.btnY"). Un botón que esté en el
        // catálogo se oculta si el usuario no tiene esa patente; uno que no esté no se toca.
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

        // Devuelve true si alguno de los roles del usuario logueado tiene el nombre indicado.
        // Comparación case-insensitive para evitar falsos negativos por mayúsculas/minúsculas.
        public bool TieneRol(string nombreRol)
        {
            if (RolesActuales == null || RolesActuales.Count == 0)
                return false;

            return RolesActuales.Any(r =>
                string.Equals(r.Nombre, nombreRol, StringComparison.OrdinalIgnoreCase));
        }

        // Devuelve true si el usuario tiene la patente, ya sea por un rol, por una familia
        // de formulario completa o asignada directamente.
        public bool TienePermiso(string permiso)
        {
            return Permisos != null && Permisos.TienePermiso(permiso);
        }

        public bool EstaEnCatalogo(string permiso)
        {
            return CatalogoPermisos != null && CatalogoPermisos.Contains(permiso);
        }

        // Puede abrir el formulario si tiene al menos una patente de ese formulario
        // (la página completa, "Ver" o aunque sea un solo botón).
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
