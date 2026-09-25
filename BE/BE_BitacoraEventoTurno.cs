using System;

namespace BE
{
    // Fila de BitacoraEventosTurno: bitacora adicional, orientada a eventos, que se completa
    // sola via SQL Triggers (TR_Turno_Insert_BitacoraEventos / TR_Turno_Update_BitacoraEventos),
    // sin depender de que la capa BLL la invoque explicitamente.
    public class BE_BitacoraEventoTurno
    {
        public int IdBitacoraEvento { get; set; }

        public int IdTurno { get; set; }

        // "ALTA" o "CAMBIO_ESTADO"
        public string TipoEvento { get; set; }

        public string EstadoAnterior { get; set; }

        public string EstadoNuevo { get; set; }

        public DateTime FechaHora { get; set; }

        // Login de SQL Server que ejecuto la instruccion que disparo el trigger (SUSER_SNAME()).
        public string UsuarioBD { get; set; }

        // Equipo desde el que se abrio la conexion que disparo el trigger (HOST_NAME()).
        public string HostBD { get; set; }

        // Datos resueltos por JOIN, no se persisten en BitacoraEventosTurno
        public string NombreApellidoPaciente { get; set; }

        public string NombreApellidoMedico { get; set; }

        // Fecha y horario del turno auditado (no de la bitacora), resueltos por JOIN
        // con Turno/Medico_Horario, igual que BE_Turno.
        public DateTime FechaTurno { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public string FechaTurnoTexto { get { return FechaTurno.ToString("dd/MM/yyyy"); } }

        public string RangoHorario { get { return string.Format("{0:hh\\:mm} - {1:hh\\:mm}", HoraInicio, HoraFin); } }
    }
}
