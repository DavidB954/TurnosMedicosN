using System;
using System.IO;
using System.Windows.Forms;

namespace Vista
{
    public static class Ayuda
    {
        private static frmAyuda ventana;

        public static string Carpeta
        {
            get { return Path.Combine(Path.GetDirectoryName(typeof(Ayuda).Assembly.Location), "Ayuda"); }
        }

        public static string RutaPagina(string pantalla)
        {
            string propia = Path.Combine(Carpeta, (pantalla ?? string.Empty) + ".html");
            if (!string.IsNullOrEmpty(pantalla) && File.Exists(propia))
                return propia;
            string indice = Path.Combine(Carpeta, "index.html");
            return File.Exists(indice) ? indice : null;
        }

        public static void Mostrar(string pantalla, IWin32Window duenio)
        {
            string ruta = RutaPagina(pantalla);
            if (ruta == null)
            {
                MessageBox.Show("No se encontró la ayuda del sistema (carpeta Ayuda).", "Ayuda",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ventana == null || ventana.IsDisposed)
                ventana = new frmAyuda();
            ventana.MostrarPagina(ruta);
            if (!ventana.Visible)
                ventana.Show(duenio);
            ventana.BringToFront();
        }
    }
}
