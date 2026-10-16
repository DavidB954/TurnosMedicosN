using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Composite
{
    // Patente: un botón (o el acceso "Ver") de un formulario.
    // Nombre es la clave que verifica el código: "<ClaseFormulario>.<NombreControl>".
    public class Permiso : Componente
    {
        public int IdPermiso { get; set; }
        public string Descripcion { get; set; }
        public string Formulario { get; set; }

        public override string ToString() => string.IsNullOrEmpty(Descripcion) ? Nombre : Descripcion;

        public override bool TienePermiso(string permiso)
        {
            return Nombre == permiso;
        }

    }
}
