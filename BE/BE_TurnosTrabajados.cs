namespace BE
{
    public class BE_TurnosTrabajados
    {
        public int IdMedico { get; set; }

        public string NombreMedico { get; set; }

        public string Matricula { get; set; }

        public int IdEspecialidad { get; set; }

        public string NombreEspecialidad { get; set; }

        public int DiasTrabajados { get; set; }

        public int TurnosAtendidos { get; set; }

        public int TurnosAusentes { get; set; }

        public int TurnosCancelados { get; set; }

        public decimal ValorTurno { get; set; }

        public decimal PorcentajeAusente { get; set; }

        public decimal ImporteEstimado
        {
            get { return Formato.Importe(TurnosAtendidos, TurnosAusentes, ValorTurno, PorcentajeAusente); }
        }

        public string ValorTurnoTexto { get { return Formato.Moneda(ValorTurno); } }

        public string PorcentajeAusenteTexto { get { return Formato.Porcentaje(PorcentajeAusente); } }

        public string ImporteEstimadoTexto { get { return Formato.Moneda(ImporteEstimado); } }
    }
}
