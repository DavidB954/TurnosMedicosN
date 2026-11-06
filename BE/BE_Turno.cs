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

        // Especialidad con la que se reservo (la de la franja horaria); null = el horario no
        // tenia una especialidad puntual asignada (franjas creadas antes de esa funcionalidad).
        public int? IdEspecialidad { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public string NombreApellidoMedico { get; set; }

        // Matricula profesional del medico (tabla Medico). Se muestra en la receta.
        public string MatriculaMedico { get; set; }

        public string NombreApellidoPaciente { get; set; }

        public string Especialidades { get; set; }

        // Asociacion con el Paciente (Proceso 2): un Turno TIENE un Paciente, no hereda de el.
        // Se completa solo en las consultas que lo necesitan (agenda del medico, atencion).
        public BE_Paciente Paciente { get; set; }

        public string HoraTexto { get { return HoraInicio.ToString(@"hh\:mm"); } }

        public string HistoriaClinicaTexto { get { return Paciente != null ? Paciente.HistoriaClinicaTexto : ""; } }

        public string FechaTexto { get { return Fecha.ToString("dd/MM/yyyy"); } }

        public string RangoHorario { get { return string.Format("{0:hh\\:mm} - {1:hh\\:mm}", HoraInicio, HoraFin); } }
    }
}
