using System;
using System.Windows.Forms;

namespace Vista
{
    // D02 Ayuda en linea: ventana que muestra las paginas de ayuda (HTML) y permite navegar entre ellas.
    public partial class frmAyuda : Form
    {
        public frmAyuda()
        {
            InitializeComponent();
        }

        public void MostrarPagina(string ruta)
        {
            wbAyuda.Navigate(new Uri(ruta));
        }

        // El titulo de la ventana es el de la pagina que se esta viendo.
        private void wbAyuda_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                Text = "Ayuda – " + wbAyuda.DocumentTitle;
            }
            catch (Exception)
            {
                Text = "Ayuda";
            }
        }
    }
}
