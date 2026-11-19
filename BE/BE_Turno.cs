using System;

namespace BE
{
    // Reserva de un horario de atencion de un medico, en una fecha, para un paciente.
    // En la base el turno guarda el paciente y el horario; el medico, la especialidad y las horas
    // salen del horario (Medico_Horario) y se completan aca para mostrarlos.
    public class BE_Turno
    {
        public int IdTurno { get; set; }

        public int IdPaciente { get; set; }

        public int IdHorario { get; set; }

        public DateTime Fecha { get; set; }

        // "Confirmado", "Cancelado", "Atendido" o "Ausente"
        public string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int IdMedico { get; set; }

        public int? IdEspecialidad { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public string NombreApellidoMedico { get; set; }

        // Matricula profesional del medico (tabla Medico).
        public string MatriculaMedico { get; set; }

        public string NombreApellidoPaciente { get; set; }

        public string DNIPaciente { get; set; }

        public string Especialidades { get; set; }

        // Asociacion con el Paciente: un Turno TIENE un Paciente. Se completa solo en las
        // consultas que lo necesitan (agenda del medico, atencion).
        public BE_Paciente Paciente { get; set; }

        public string HoraTexto { get { return HoraInicio.ToString(@"hh\:mm"); } }

        public string HistoriaClinicaTexto { get { return Paciente != null ? Paciente.HistoriaClinicaTexto : ""; } }

        public string FechaTexto { get { return Fecha.ToString("dd/MM/yyyy"); } }

        public string RangoHorario { get { return string.Format("{0:hh\\:mm} - {1:hh\\:mm}", HoraInicio, HoraFin); } }
    }
}
