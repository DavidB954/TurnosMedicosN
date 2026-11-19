namespace BE
{
    // Renglon de la planilla de turnos trabajados (CU-02.2): actividad de un medico en una especialidad
    // durante un periodo. No tiene tabla propia: se arma con la consulta de DAL_Liquidacion.
    public class BE_TurnosTrabajados
    {
        public int IdMedico { get; set; }

        public string NombreMedico { get; set; }

        public string Matricula { get; set; }

        public int IdEspecialidad { get; set; }

        public string NombreEspecialidad { get; set; }

        // Dias distintos con al menos un turno Atendido o Ausente.
        public int DiasTrabajados { get; set; }

        public int TurnosAtendidos { get; set; }

        public int TurnosAusentes { get; set; }

        // Informativo: no se pagan (RN2.2).
        public int TurnosCancelados { get; set; }

        // Valor del turno y porcentaje por ausencia vigentes de la especialidad.
        public decimal ValorTurno { get; set; }

        public decimal PorcentajeAusente { get; set; }

        // RN2.2: atendidos x valor del turno + ausentes x valor del turno x porcentaje por ausencia.
        public decimal ImporteEstimado
        {
            get { return Formato.Importe(TurnosAtendidos, TurnosAusentes, ValorTurno, PorcentajeAusente); }
        }

        public string ValorTurnoTexto { get { return Formato.Moneda(ValorTurno); } }

        public string PorcentajeAusenteTexto { get { return Formato.Porcentaje(PorcentajeAusente); } }

        public string ImporteEstimadoTexto { get { return Formato.Moneda(ImporteEstimado); } }
    }
}
