using System;

namespace BE
{
    // Registro de lo actuado en un turno (RF 02.1). Forma parte de la Historia Clinica del
    // paciente: el paciente y el medico se obtienen por el Turno (3FN), no se repiten aca.
    public class BE_AtencionMedica
    {
        public int IdAtencion { get; set; }

        public BE_Turno Turno { get; set; }

        public DateTime FechaHora { get; set; }

        public string MotivoConsulta { get; set; }

        public string Indicaciones { get; set; }

        // null si en la atencion se eligio Receta = NO
        public BE_Receta Receta { get; set; }

        public string DVH { get; set; }
    }
}
