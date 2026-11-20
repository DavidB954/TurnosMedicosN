namespace BE
{
    public class BE_LiquidacionDetalle
    {
        public int IdDetalle { get; set; }

        public int IdEspecialidad { get; set; }

        public string NombreEspecialidad { get; set; }

        public int DiasTrabajados { get; set; }

        public int TurnosAtendidos { get; set; }

        public int TurnosAusentes { get; set; }

        public decimal ValorTurnoAplicado { get; set; }

        public decimal PorcentajeAusenteAplicado { get; set; }

        public decimal Subtotal { get { return CalcularSubtotal(); } }

        public decimal CalcularSubtotal()
        {
            return Formato.Importe(TurnosAtendidos, TurnosAusentes, ValorTurnoAplicado, PorcentajeAusenteAplicado);
        }
    }
}
