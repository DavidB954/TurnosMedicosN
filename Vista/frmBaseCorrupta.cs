using BE;
using BLL;
using Servicios;
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
    public partial class frmBaseCorrupta : frmBase
    {
        public frmBaseCorrupta()
        {
            InitializeComponent();
        }

        BLL_Usuario bll_Usu = new BLL_Usuario();
        BLL_Roles bll_roles = new BLL_Roles();


        private const string Email_Emergencia = "admin@sistema.com";
        private const string Password_Emergencia = "26D6A8AD97C75FFC548F6873E5E93CE475479E3E1A1097381E54221FB53EC1D2";


        private void frmBaseCorrupta_Load(object sender, EventArgs e)
        {

        }

        public override void AplicarIdioma()  
        {
            AplicarIdiomaAutomatico();
        }



        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {

                if (EsUsuarioEmergencia(txtEmail.Text, txtContrasena.Text))
                {
                    AbrirSeguridad();
                    return;
                }


                BE_Usuario usuario = bll_Usu.ValidarCredencialesEmergencia(txtEmail.Text, txtContrasena.Text);

                if (usuario != null && EsAdministrador(usuario))
                {

                    AbrirSeguridad();
                    return;
                }
                else
                {
                    MessageBox.Show("Credenciales inválidas o sin permisos de Administrador.\nContacte a soporte técnico.",
                        "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Clear();
                    txtContrasena.Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool EsUsuarioEmergencia(string email, string password)
        {
            return email == Email_Emergencia && HashHelper.GenerarHash(password) == Password_Emergencia;
        }

        private bool EsAdministrador(BE_Usuario usuario)
        {
            var roles = bll_roles.ObtenerRolesDeUsuario(usuario.IdUsuario);
            return roles.Any(r => r.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase));
        }

        private void AbrirSeguridad()
        {
            var corruptos = bll_Usu.VerificarUsuarios();

            MessageBox.Show("Se detectó un problema de integridad. Se abrirá el módulo de Seguridad.");

            frmSeguridad frm = new frmSeguridad();
            frm.WindowState = FormWindowState.Maximized;
            frm.MostrarCorruptos(corruptos);
            frm.Show();

            this.Hide();
        }


    }
}
