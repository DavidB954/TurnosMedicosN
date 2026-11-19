using System;

namespace BE
{
    // Persona que recibe atencion en el sanatorio. NO es usuario del sistema: lo registra el
    // recepcionista (CU-01.1) y se identifica por su DNI. Tabla Paciente.
    public class BE_Paciente
    {
        public int IdPaciente { get; set; }

        public string DNI { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        // N° de Historia Clinica: lo genera la base (SEQ_NroHistoriaClinica) al registrar al paciente.
        public int NroHistoriaClinica { get; set; }

        public DateTime FechaAperturaHC { get; set; }

        public string NombreApellido { get { return Nombre + " " + Apellido; } }

        public string HistoriaClinicaTexto { get { return NroHistoriaClinica > 0 ? "H.C " + NroHistoriaClinica : ""; } }
    }
}
