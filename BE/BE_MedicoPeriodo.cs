using System.Collections.Generic;
using System.Linq;

namespace BE
{
    public class BE_MedicoPeriodo
    {
        public int IdMedico { get; set; }

        public string NombreMedico { get; set; }

        public string Matricula { get; set; }

        public List<BE_TurnosTrabajados> TurnosTrabajados { get; set; } = new List<BE_TurnosTrabajados>();

        public bool Liquidado { get; set; }

        public int TurnosAtendidos { get { return TurnosTrabajados.Sum(t => t.TurnosAtendidos); } }

        public int TurnosAusentes { get { return TurnosTrabajados.Sum(t => t.TurnosAusentes); } }

        public int TurnosCancelados { get { return TurnosTrabajados.Sum(t => t.TurnosCancelados); } }

        public decimal ImporteEstimado { get { return TurnosTrabajados.Sum(t => t.ImporteEstimado); } }

        public string ImporteEstimadoTexto { get { return Formato.Moneda(ImporteEstimado); } }

        public string EstadoTexto { get { return Liquidado ? "Liquidado" : "Pendiente"; } }
    }
}
