using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Composite
{
    public abstract class Componente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public abstract bool TienePermiso(string Permiso);
    }
}
