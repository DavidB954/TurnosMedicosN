using System;

namespace BE
{
    public class BE_ErrorRegistrado
    {
        public DateTime FechaHora { get; set; }

        public string Usuario { get; set; }

        public string Equipo { get; set; }

        public string Origen { get; set; }

        public string Tipo { get; set; }

        public string Mensaje { get; set; }

        public string Detalle { get; set; }
    }
}
