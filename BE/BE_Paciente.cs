using System;

namespace BE
{
    // Un Paciente ES un Usuario (herencia): reutiliza IdUsuario, Nombre, Apellido, etc. y agrega
    // los datos que solo tienen los pacientes. En la base es la tabla Paciente (1:1 con Usuario).
    public class BE_Paciente : BE_Usuario
    {
        public string DNI { get; set; }

        // N° de Historia Clinica: lo genera la base (SEQ_NroHistoriaClinica) al crear el paciente.
        public int NroHistoriaClinica { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaAperturaHC { get; set; }

        public string HistoriaClinicaTexto { get { return NroHistoriaClinica > 0 ? "H.C " + NroHistoriaClinica : ""; } }
    }
}
