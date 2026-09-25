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
    public partial class frmLogin : frmBase
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        BLL_Usuario bll_Usu = new BLL_Usuario();

        BE_LoginResultado Obj_Usuario = new BE_LoginResultado();

       
        public override void AplicarIdioma()   
        {
            AplicarIdiomaAutomatico();
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                //MAil de prueba: JB@GMAIL.COM -Paciente, DB@GMAIL.COM -Medico , GS@GMAIL.COM - Administrador, 
                //Todas las contraseñas = 123;
                
                //string pass = HashHelper.GenerarHash(pas);

                //Clipboard.SetText(pass);
                //MessageBox.Show(pass);

                Obj_Usuario = bll_Usu.ObtenerUsuarioPorEmail(txtEmail.Text, txtPassword.Text);

                if (Obj_Usuario.Usuario != null)
                {
                    //Para agarrar la sesion del singleton y guardar el usuario logueado
                    Sesion Sesion = Sesion.Instancia();

                    Sesion.UsuarioActual = Obj_Usuario.Usuario;

                    // Cargamos los roles del usuario para poder filtrar el menú por permisos
                    Sesion.RolesActuales = new BLL_Roles().ObtenerRolesDeUsuario(Obj_Usuario.Usuario.IdUsuario)
                                            ?? new List<BE.Composite.RolComposite>();

                    var idioma = new BLL_Traduccion().ObtenerIdiomaPorUsuario(Obj_Usuario.Usuario.IdUsuario);
                    if (idioma == null)
                        idioma = new BLL_Traduccion().ObtenerIdiomas().FirstOrDefault(i => i.Codigo == "ES");

                    if (idioma != null)
                    {
                        var diccionario = new BLL_Traduccion().ObtenerDiccionario(idioma.Id);
                        TraduccionServicio.Instancia.CambiarIdioma(idioma, diccionario);
                    }

                    // Cubre el caso de que la app haya estado cerrada y queden turnos vencidos
                    // desde antes; se ignoran los errores para no bloquear el login por esto.
                    if (Sesion.TieneRol("Administrativo") || Sesion.TieneRol("Administrador"))
                    {
                        try
                        {
                            new BLL_Turno().ProcesarVencimientos();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    frmPrincipal formP = new frmPrincipal();

                    formP.Show();

                    this.Hide();

                    LimpiarTextBox();
                }
                else
                {
                    MessageBox.Show(Obj_Usuario.Mensaje);
                    LimpiarTextBox();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LimpiarTextBox()
        {
            try
            {
                txtEmail.Clear();
                txtPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
