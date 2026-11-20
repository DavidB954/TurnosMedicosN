using System;
using System.Windows.Forms;

namespace Vista
{
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
