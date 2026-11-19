using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Servicios
{
    // A03 Serializacion: registro de errores del sistema. Cada error inesperado se convierte en un BE_ErrorRegistrado y
    // se serializa en XML (XmlSerializer) en el archivo RegistroErrores.xml de la carpeta de datos del usuario. El
    // archivo se deserializa para agregar el error nuevo y para consultarlo (guia de mantenimiento, D03).
    public class RegistroErrores
    {
        private static readonly object bloqueo = new object();

        private static readonly XmlSerializer serializador = new XmlSerializer(typeof(List<BE_ErrorRegistrado>));

        public static string RutaArchivo
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                    "GestionTurnosMedicos", "RegistroErrores.xml");
            }
        }

        // Registra el error. Nunca lanza otra excepcion: si no se puede escribir el archivo, el error se pierde pero
        // el sistema sigue funcionando.
        public static void Registrar(Exception ex, string origen)
        {
            if (ex == null)
                return;
            try
            {
                lock (bloqueo)
                {
                    List<BE_ErrorRegistrado> errores = ObtenerErrores();
                    errores.Add(Crear(ex, origen));
                    Directory.CreateDirectory(Path.GetDirectoryName(RutaArchivo));
                    using (StreamWriter escritor = new StreamWriter(RutaArchivo, false, Encoding.UTF8))
                        serializador.Serialize(escritor, errores);
                }
            }
            catch (Exception)
            {
                // El registro de errores no debe provocar otro error.
            }
        }

        // Deserializa el registro de errores (lista vacia si todavia no hay errores o el archivo no se puede leer).
        public static List<BE_ErrorRegistrado> ObtenerErrores()
        {
            try
            {
                if (!File.Exists(RutaArchivo))
                    return new List<BE_ErrorRegistrado>();
                using (StreamReader lector = new StreamReader(RutaArchivo, Encoding.UTF8))
                    return (List<BE_ErrorRegistrado>)serializador.Deserialize(lector);
            }
            catch (Exception)
            {
                return new List<BE_ErrorRegistrado>();
            }
        }

        private static BE_ErrorRegistrado Crear(Exception ex, string origen)
        {
            StringBuilder detalle = new StringBuilder(ex.StackTrace ?? string.Empty);
            for (Exception interna = ex.InnerException; interna != null; interna = interna.InnerException)
                detalle.AppendLine().Append("Causa: ").Append(interna.GetType().FullName).Append(": ").Append(interna.Message);

            BE_Usuario usuario = Sesion.Instancia().UsuarioActual;
            return new BE_ErrorRegistrado
            {
                FechaHora = DateTime.Now,
                Usuario = usuario == null ? "SISTEMA" : usuario.Nombre + " " + usuario.Apellido,
                Equipo = Environment.MachineName,
                Origen = origen,
                Tipo = ex.GetType().FullName,
                Mensaje = ex.Message,
                Detalle = detalle.ToString()
            };
        }
    }
}
