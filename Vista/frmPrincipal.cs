using BLL;
using Servicios;
using System;
using System.Linq;
using System.Windows.Forms;
using static BE.BE_Traduccion;

namespace Vista
{
    public partial class frmPrincipal : frmBase
    {
        public frmPrincipal()
        {
            InitializeComponent();
            CbPrincipalIdioma.SelectedIndexChanged -= CbPrincipalIdioma_SelectedIndexChanged;
            CbPrincipalIdioma.DataSource = new BLL_Traduccion().ObtenerIdiomas();
            CbPrincipalIdioma.DisplayMember = "Nombre";
            CbPrincipalIdioma.ValueMember = "Id";
            CbPrincipalIdioma.SelectedIndexChanged += CbPrincipalIdioma_SelectedIndexChanged;

            TraduccionServicio.Instancia.IdiomasChanged += CargarIdiomas;
        }
        private void CargarIdiomas()
        {
            CbPrincipalIdioma.SelectedIndexChanged -= CbPrincipalIdioma_SelectedIndexChanged;
            CbPrincipalIdioma.DataSource = new BLL_Traduccion().ObtenerIdiomas();
            CbPrincipalIdioma.DisplayMember = "Nombre";
            CbPrincipalIdioma.ValueMember = "Id";

            var idiomaActual = TraduccionServicio.Instancia.IdiomaActual;
            if (idiomaActual != null)
                foreach (Idioma item in CbPrincipalIdioma.Items)
                    if (item.Id == idiomaActual.Id)
                    {
                        CbPrincipalIdioma.SelectedItem = item;
                        break;
                    }

            CbPrincipalIdioma.SelectedIndexChanged += CbPrincipalIdioma_SelectedIndexChanged;
        }

        public override void AplicarIdioma()
        {
            var idiomaActual = TraduccionServicio.Instancia.IdiomaActual;
            if (idiomaActual != null)
            {
                CbPrincipalIdioma.SelectedIndexChanged -= CbPrincipalIdioma_SelectedIndexChanged;
                foreach (Idioma item in CbPrincipalIdioma.Items)
                {
                    if (item.Id == idiomaActual.Id)
                    {
                        CbPrincipalIdioma.SelectedItem = item;
                        break;
                    }
                }
                CbPrincipalIdioma.SelectedIndexChanged += CbPrincipalIdioma_SelectedIndexChanged;
            }

            AplicarIdiomaAutomatico();

            var t = TraduccionServicio.Instancia;
            var sesion = Sesion.Instancia();
            if (sesion.UsuarioActual != null)
            {
                string etiqueta = TraduccionServicio.Instancia.Traducir("frmPrincipal.lblUsuario");
                if (etiqueta.StartsWith("["))
                    etiqueta = "Usuario";
                lblUsuario.Text = $"{etiqueta}: {sesion.UsuarioActual.Nombre}";
            }
        }


        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = Sesion.Instancia();
                if (sesion.UsuarioActual != null)
                    lblUsuario.Text = $"Usuario: {sesion.UsuarioActual.Nombre}";

                AplicarPermisosMenu();

                timerVencimientoTurnos.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Autolimpieza mientras la app esta abierta: cada 5 minutos, cierra los turnos
        // Confirmado que ya vencieron (RF ausentismo automatico). Se ignoran los errores
        // para no interrumpir al usuario con un mensaje cada vez que corre el Timer;
        // si falla, simplemente lo vuelve a intentar en la proxima vuelta.
        private void timerVencimientoTurnos_Tick(object sender, EventArgs e)
        {
            try
            {
                new BLL_Turno().ProcesarVencimientos();
            }
            catch (Exception)
            {
            }
        }

        // Submenú desplegable de "Gestión de Usuarios"
        private bool submenuUsuariosAbierto = false;

        // Cada botón del menú abre un formulario; se muestra si el usuario tiene al menos
        // una patente de ese formulario (página completa, "Ver" o un solo botón).
        private bool PuedeAbrir(string formulario)
        {
            return Sesion.Instancia().TieneAccesoFormulario(formulario);
        }

        private void AplicarPermisosMenu()
        {
            btn_SubUsuariosABM.Tag = PuedeAbrir(nameof(frmUsuarioABM));
            btn_SubUsuariosRoles.Tag = PuedeAbrir(nameof(frmUsuarioRoles));
            btn_SubUsuariosHistorial.Tag = PuedeAbrir(nameof(frmUsuarioHistorial));

            btn_GestionUsuarios.Visible = (bool)btn_SubUsuariosABM.Tag
                                       || (bool)btn_SubUsuariosRoles.Tag
                                       || (bool)btn_SubUsuariosHistorial.Tag;

            btn_GestionRoles.Visible = PuedeAbrir(nameof(frmRolesPermisos));
            btn_Seguridad.Visible = PuedeAbrir(nameof(frmSeguridad));
            btn_Bitacora.Visible = PuedeAbrir(nameof(frmBitacora));
            btn_BitacoraTurno.Visible = PuedeAbrir(nameof(frm_BitacoraTurno));
            btnGestionIdiomas.Visible = PuedeAbrir(nameof(frmABM));
            btn_AccesoMedico.Visible = PuedeAbrir(nameof(frm_AccesoMedico));
            btn_AccesoPaciente.Visible = PuedeAbrir(nameof(frm_AccesoPaciente));
            btn_GestionMedicos.Visible = PuedeAbrir(nameof(frmGestionMedicos));
            btn_GestionTurnos.Visible = PuedeAbrir(nameof(frmGestionTurnos));

            ActualizarSubmenuUsuarios();
        }

        // Los subítems se ven solo con el submenú abierto y si el usuario tiene acceso a ese formulario.
        private void ActualizarSubmenuUsuarios()
        {
            foreach (var sub in new[] { btn_SubUsuariosABM, btn_SubUsuariosRoles, btn_SubUsuariosHistorial })
                sub.Visible = submenuUsuariosAbierto && sub.Tag is bool permitido && permitido;

            ReacomodarBotonesMenu();
        }

        // Apila verticalmente los botones visibles del menú, en el mismo orden original,
        // para que no queden huecos cuando alguno se oculta por falta de permiso.
        private void ReacomodarBotonesMenu()
        {
            var botonesEnOrden = new[]
            {
                btn_GestionUsuarios,
                btn_SubUsuariosABM,
                btn_SubUsuariosRoles,
                btn_SubUsuariosHistorial,
                btn_GestionRoles,
                btn_Seguridad,
                btn_Bitacora,
                btn_BitacoraTurno,
                btnGestionIdiomas,
                btn_AccesoMedico,
                btn_AccesoPaciente,
                btn_GestionMedicos,
                btn_GestionTurnos
            };

            int y = 34;
            foreach (var boton in botonesEnOrden)
            {
                if (!boton.Visible)
                    continue;

                boton.Top = y;
                y += boton.Height;
            }
        }


        public void AbrirFormulario<T>() where T : Form, new()
        {
            try
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is T)
                    {
                        frm.BringToFront();
                        frm.WindowState = FormWindowState.Maximized;
                        frm.Show();
                        return;
                    }
                }

                Form f = new T();
                f.WindowState = FormWindowState.Maximized;
                f.MdiParent = this;
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void AbrirSiTieneAcceso<T>() where T : Form, new()
        {
            if (!PuedeAbrir(typeof(T).Name))
                return;
            AbrirFormulario<T>();
        }

        private void btn_GestionUsuarios_Click(object sender, EventArgs e)
        {
            submenuUsuariosAbierto = !submenuUsuariosAbierto;
            ActualizarSubmenuUsuarios();
        }

        private void btn_SubUsuariosABM_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frmUsuarioABM>();
        }

        private void btn_SubUsuariosRoles_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frmUsuarioRoles>();
        }

        private void btn_SubUsuariosHistorial_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frmUsuarioHistorial>();
        }

        private void btn_GestionRoles_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frmRolesPermisos>();
        }

        private void btn_Seguridad_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frmSeguridad>();
        }

        private void btn_Bitacora_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frmBitacora>();
        }

        private void btn_BitacoraTurno_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frm_BitacoraTurno>();
        }

        private void btn_AccesoMedico_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frm_AccesoMedico>();
        }

        private void btn_AccesoPaciente_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frm_AccesoPaciente>();
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            try
            {
                new BLL_Usuario().CerrarSesion();
                TraduccionServicio.ResetearInstancia();
                foreach (Form form in this.MdiChildren)
                    form.Close();
                frmLogin login = Application.OpenForms.OfType<frmLogin>().FirstOrDefault();
                if (login != null)
                    login.Show();
                else
                    new frmLogin().Show();
                this.Close();
                //this.Hide();
                //frmLogin frm = new frmLogin();
                //frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            
        }

        private void btnGestionIdiomas_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frmABM>();
        }

        private void CbPrincipalIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var idiomaSeleccionado = (Idioma)CbPrincipalIdioma.SelectedItem;
                CambiarIdioma(idiomaSeleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cambiar idioma",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_GestionMedicos_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frmGestionMedicos>();
        }

        private void btn_GestionTurnos_Click(object sender, EventArgs e)
        {
            AbrirSiTieneAcceso<frmGestionTurnos>();
        }
    }
}
