using System;

namespace BE
{
    // T06b Control de cambios: una version de un turno (fila de la tabla copia Turno_Historial).
    // La registra el trigger TR_Turno_Historial despues de cada alta o modificacion del turno.
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

        // "Alta" o "Modificacion"
        public string Operacion { get; set; }

        public DateTime FechaHoraCambio { get; set; }

        // null: el cambio no lo hizo un usuario identificado (por ejemplo, la migracion inicial)
        public int? IdUsuarioCambio { get; set; }

        public string NombreUsuarioCambio { get; set; }

        public string FechaTexto { get { return Fecha.ToString("dd/MM/yyyy"); } }

        public string FechaHoraCambioTexto { get { return FechaHoraCambio.ToString("dd/MM/yyyy HH:mm:ss"); } }

        public string OperacionTexto { get { return Operacion == "Modificacion" ? "Modificación" : Operacion; } }
    }
}
