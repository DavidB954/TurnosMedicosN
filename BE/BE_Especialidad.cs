namespace BE
{
    // Rama de la medicina en la que se organiza la atencion (tabla Especialidades).
    public class BE_Especialidad
    {
        public int IdEspecialidad { get; set; }

        public string Nombre { get; set; }

        // Honorario por turno atendido en la especialidad (CU-02.1, RN2.4).
        public decimal ValorTurno { get; set; }

        // Porcentaje del valor del turno que se paga cuando el paciente no se presenta (RN2.2).
        public decimal PorcentajeAusente { get; set; }

        public string ValorTurnoTexto { get { return Formato.Moneda(ValorTurno); } }

        public string PorcentajeAusenteTexto { get { return Formato.Porcentaje(PorcentajeAusente); } }
    }
}
