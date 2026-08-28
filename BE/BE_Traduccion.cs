using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BE_Traduccion
    {

        public class Idioma
        {
            public int Id { get; set; }
            public string Codigo { get; set; }
            public string Nombre { get; set; }
        }

        public class Traduccion
        {
            public int Id { get; set; }
            public string Clave { get; set; }
            public int IdIdioma { get; set; }
            public string Texto { get; set; }
        }
    }
}
