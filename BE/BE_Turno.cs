using System;

namespace BE
{
    public class BE_Turno
    {
        public int IdTurno { get; set; }

        public int IdPaciente { get; set; }

        public int IdHorario { get; set; }

        public DateTime Fecha { get; set; }

        // "Confirmado", "Cancelado", "Atendido" o "Ausente"
        public string Estado { get; set; }

        public int IdMedico { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public string NombreApellidoMedico { get; set; }

        public string NombreApellidoPaciente { get; set; }

        public string Especialidades { get; set; }

        public string FechaTexto { get { return Fecha.ToString("dd/MM/yyyy"); } }

        public string RangoHorario { get { return string.Format("{0:hh\\:mm} - {1:hh\\:mm}", HoraInicio, HoraFin); } }
    }
}
