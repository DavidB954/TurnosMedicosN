using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Composite
{
    public class Permiso : Componente
    {
        public int IdPermiso { get; set; }
        public override bool TienePermiso(string permiso)
        {
            return Nombre == permiso;
        }

    }
}
