using System;

namespace BE
{
    public class BE_TurnoHistorial
    {
        public int IdHistorial { get; set; }

        public int IdTurno { get; set; }

        public int IdPaciente { get; set; }

        public string NombrePaciente { get; set; }

        public int IdHorario { get; set; }

        public string RangoHorario { get; set; }

        public DateTime Fecha { get; set; }

        public string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string Operacion { get; set; }

        public DateTime FechaHoraCambio { get; set; }

        public int? IdUsuarioCambio { get; set; }

        public string NombreUsuarioCambio { get; set; }

        public string FechaTexto { get { return Fecha.ToString("dd/MM/yyyy"); } }

        public string FechaHoraCambioTexto { get { return FechaHoraCambio.ToString("dd/MM/yyyy HH:mm:ss"); } }

        public string OperacionTexto { get { return Operacion == "Modificacion" ? "Modificación" : Operacion; } }
    }
}
