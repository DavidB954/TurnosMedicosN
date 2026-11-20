using System;

namespace BE
{
    public class BE_Paciente
    {
        public int IdPaciente { get; set; }

        public string DNI { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public int NroHistoriaClinica { get; set; }

        public DateTime FechaAperturaHC { get; set; }

        public string NombreApellido { get { return Nombre + " " + Apellido; } }

        public string HistoriaClinicaTexto { get { return NroHistoriaClinica > 0 ? "H.C " + NroHistoriaClinica : ""; } }
    }
}
