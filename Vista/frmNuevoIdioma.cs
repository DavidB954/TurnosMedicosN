using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmNuevoIdioma : frmBase
    {
        public frmNuevoIdioma()
        {
            InitializeComponent();
        }

        private void frmNuevoIdioma_Load(object sender, EventArgs e)
        {

        }


        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }

        public string Codigo
        {
            get; private set;
        }
        public string Nombre
        {
            get; private set;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
               string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Completá ambos campos.", "Error");
                return;
            }

            Codigo = txtCodigo.Text.Trim().ToUpper();
            Nombre = txtNombre.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
