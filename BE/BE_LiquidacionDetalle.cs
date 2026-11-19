namespace BE
{
    // Renglon de una liquidacion: actividad de una especialidad y los valores aplicados (RN2.5).
    public class BE_LiquidacionDetalle
    {
        public int IdDetalle { get; set; }

        public int IdEspecialidad { get; set; }

        public string NombreEspecialidad { get; set; }

        // Se guardan al generar la liquidacion para que el recibo no cambie despues (RN2.7).
        public int DiasTrabajados { get; set; }

        public int TurnosAtendidos { get; set; }

        public int TurnosAusentes { get; set; }

        // Valor del turno y porcentaje por ausencia vigentes al generar la liquidacion: si despues
        // cambian, la liquidacion no cambia (RN2.5).
        public decimal ValorTurnoAplicado { get; set; }

        public decimal PorcentajeAusenteAplicado { get; set; }

        // Atributo calculado: no se guarda en la base (3FN).
        public decimal Subtotal { get { return CalcularSubtotal(); } }

        // RN2.2: atendidos x valor del turno + ausentes x valor del turno x porcentaje por ausencia.
        public decimal CalcularSubtotal()
        {
            return Formato.Importe(TurnosAtendidos, TurnosAusentes, ValorTurnoAplicado, PorcentajeAusenteAplicado);
        }
    }
}
