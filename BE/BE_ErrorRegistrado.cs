using System;

namespace BE
{
    // A03 Serializacion: un error inesperado del sistema, tal como se guarda serializado en XML en el registro de
    // errores (RegistroErrores.xml). Necesita constructor sin parametros y propiedades publicas para XmlSerializer.
    public class BE_ErrorRegistrado
    {
        public DateTime FechaHora { get; set; }

        // Usuario de la sesion ("SISTEMA" si todavia no inicio sesion).
        public string Usuario { get; set; }

        public string Equipo { get; set; }

        // Pantalla o lugar del sistema donde ocurrio el error.
        public string Origen { get; set; }

        // Tipo de la excepcion (por ejemplo System.Data.SqlClient.SqlException).
        public string Tipo { get; set; }

        public string Mensaje { get; set; }

        // Pila de llamadas y excepciones internas, para el mantenimiento.
        public string Detalle { get; set; }
    }
}
