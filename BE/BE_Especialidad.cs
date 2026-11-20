namespace BE
{
    public class BE_Especialidad
    {
        public int IdEspecialidad { get; set; }

        public string Nombre { get; set; }

        public decimal ValorTurno { get; set; }

        public decimal PorcentajeAusente { get; set; }

        public string ValorTurnoTexto { get { return Formato.Moneda(ValorTurno); } }

        public string PorcentajeAusenteTexto { get { return Formato.Porcentaje(PorcentajeAusente); } }
    }
}
