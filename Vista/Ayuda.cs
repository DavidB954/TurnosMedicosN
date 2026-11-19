using System;
using System.IO;
using System.Windows.Forms;

namespace Vista
{
    // D02 Ayuda en linea: cada pantalla tiene su pagina de ayuda en la carpeta Ayuda del programa
    // (Ayuda\<NombreDeLaPantalla>.html). F1 en cualquier pantalla, o el boton Ayuda del menu, la muestra en la
    // ventana de ayuda. Si la pantalla no tiene pagina propia se muestra el indice (Ayuda\index.html).
    public static class Ayuda
    {
        private static frmAyuda ventana;

        public static string Carpeta
        {
            get { return Path.Combine(Path.GetDirectoryName(typeof(Ayuda).Assembly.Location), "Ayuda"); }
        }

        // Pagina de ayuda de la pantalla, o el indice si no tiene una propia (null si no hay ayuda instalada).
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
