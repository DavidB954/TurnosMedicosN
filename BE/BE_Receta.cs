using System;
using System.Collections.Generic;

namespace BE
{
    // Prescripcion asociada a una Atencion Medica (RF 02.5). Tiene al menos un item.
    public class BE_Receta
    {
        public int IdReceta { get; set; }

        // Lo genera el sistema al guardar (formato R-AAAA-NNNNNN)
        public string NroReceta { get; set; }

        public DateTime FechaEmision { get; set; }

        public List<BE_RecetaItem> Items { get; set; } = new List<BE_RecetaItem>();
    }
}
