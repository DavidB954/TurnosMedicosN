using System;
using System.Collections.Generic;

namespace BE
{
    public class BE_Receta
    {
        public int IdReceta { get; set; }

        public string NroReceta { get; set; }

        public DateTime FechaEmision { get; set; }

        public List<BE_RecetaItem> Items { get; set; } = new List<BE_RecetaItem>();
    }
}
