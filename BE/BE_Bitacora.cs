using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BE_Bitacora
    {
        public int IdBitacora {get; set;}

        public int? IdUsuario {get; set;}

        public DateTime FechaHora {get; set;}

        public AccionBitacora Accion { get; set;}

        public string Modulo { get; set;}

        public string IP { get; set;}

        public string Descripcion { get; set;}

        public string NombreMaquina { get; set;}

        // Nombre y apellido del usuario (o "SISTEMA" si el evento ocurrió sin sesión, ej. arranque de la app).
        // Se resuelve en la consulta (JOIN con Usuario), no se persiste en la tabla Bitacora.
        public string NombreUsuario { get; set;}
    }
}
