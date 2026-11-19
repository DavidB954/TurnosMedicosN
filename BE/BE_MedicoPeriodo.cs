using System.Collections.Generic;
using System.Linq;

namespace BE
{
    // Renglon de "Medicos del periodo" (CU-02.2): lo trabajado por un medico en el periodo, con el detalle por
    // especialidad, y si ya tiene liquidacion. No tiene tabla propia: se arma con la consulta de los turnos
    // trabajados. El contador revisa un medico a la vez y genera su liquidacion.
    public class BE_MedicoPeriodo
    {
        public int IdMedico { get; set; }

        public string NombreMedico { get; set; }

        public string Matricula { get; set; }

        public List<BE_TurnosTrabajados> TurnosTrabajados { get; set; } = new List<BE_TurnosTrabajados>();

        // RN2.3: el medico ya tiene liquidacion del periodo.
        public bool Liquidado { get; set; }

        public int TurnosAtendidos { get { return TurnosTrabajados.Sum(t => t.TurnosAtendidos); } }

        public int TurnosAusentes { get { return TurnosTrabajados.Sum(t => t.TurnosAusentes); } }

        public int TurnosCancelados { get { return TurnosTrabajados.Sum(t => t.TurnosCancelados); } }

        public decimal ImporteEstimado { get { return TurnosTrabajados.Sum(t => t.ImporteEstimado); } }

        public string ImporteEstimadoTexto { get { return Formato.Moneda(ImporteEstimado); } }

        public string EstadoTexto { get { return Liquidado ? "Liquidado" : "Pendiente"; } }
    }
}
