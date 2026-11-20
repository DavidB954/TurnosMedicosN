using System;

namespace BE
{
    public class BE_AtencionMedica
    {
        public int IdAtencion { get; set; }

        public BE_Turno Turno { get; set; }

        public DateTime FechaHora { get; set; }

        public string MotivoConsulta { get; set; }

        public string Indicaciones { get; set; }

        public BE_Receta Receta { get; set; }

        public string DVH { get; set; }
    }
}
