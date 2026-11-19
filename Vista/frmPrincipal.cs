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

                AplicarEstiloMenu();
                AplicarPermisosMenu();
                groupBox_Menu.Resize += (s, ev) => ReacomodarBotonesMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            btn_HistorialTurnos.Visible = PuedeAbrir(nameof(frmHistorialTurnos));
            btnGestionIdiomas.Visible = PuedeAbrir(nameof(frmABM));
            // Mi Agenda muestra contenido clinico exclusivo del rol Medico (ver frm_AccesoMedico
            // y BLL_Atencion): aunque el Administrador tenga, por diseno, la patente de todos los
            // formularios, el boton no se muestra si no tiene tambien ese rol, para no ofrecer un
            // acceso que siempre va a terminar en "Acceso denegado".
            btn_AccesoMedico.Visible = PuedeAbrir(nameof(frm_AccesoMedico)) && Sesion.Instancia().TieneRol("Medico");
            btn_GestionMedicos.Visible = PuedeAbrir(nameof(frmGestionMedicos));
            btn_GestionTurnos.Visible = PuedeAbrir(nameof(frmGestionTurnos));
            btn_ValorTurno.Visible = PuedeAbrir(nameof(frmValorTurno));
            btn_Liquidaciones.Visible = PuedeAbrir(nameof(frmLiquidaciones));

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
                btn_HistorialTurnos,
                btnGestionIdiomas,
                btn_AccesoMedico,
                btn_GestionMedicos,
                btn_GestionTurnos,
                btn_ValorTurno,
                btn_Liquidaciones,
                btn_Ayuda
            };

            // Si el contenido no entra a lo alto aparece la barra vertical: se le deja lugar para que no
            // aparezca tambien una barra horizontal. Las posiciones se toman relativas al scroll.
            int alto = botonesEnOrden.Where(b => b.Visible).Sum(b => b.Height)
                       + lblUsuario.Height + CbPrincipalIdioma.Height + btn_Salir.Height + 70;
            int ancho = groupBox_Menu.Width - (alto > groupBox_Menu.Height ? SystemInformation.VerticalScrollBarWidth : 0);
            int y = groupBox_Menu.AutoScrollPosition.Y + 12;
            foreach (var boton in botonesEnOrden)
            {
                if (!boton.Visible)
                    continue;

                boton.Left = 0;
                boton.Width = ancho;
                boton.Top = y;
                y += boton.Height;
            }

            // Usuario, idioma y Salir van debajo del ultimo boton (antes tenian posicion fija y se
            // superponian con los botones en pantallas bajas).
            y += 16;
            lblUsuario.Location = new System.Drawing.Point(10, y);
            y += lblUsuario.Height + 8;
            CbPrincipalIdioma.Location = new System.Drawing.Point(10, y);
            CbPrincipalIdioma.Width = ancho - 20;
            y += CbPrincipalIdioma.Height + 10;
            btn_Salir.Location = new System.Drawing.Point(ancho - btn_Salir.Width - 10, y);
        }

        // Menu lateral: fondo azul marino, botones planos con texto blanco y los subitems de
        // Gestion de Usuarios un tono mas claro y con sangria.
        private void AplicarEstiloMenu()
        {
            foreach (Control ctrl in groupBox_Menu.Controls)
            {
                if (ctrl is Button boton)
                {
                    bool subitem = boton.Name.StartsWith("btn_Sub");
                    bool salir = boton == btn_Salir;
                    boton.FlatStyle = FlatStyle.Flat;
                    boton.FlatAppearance.BorderSize = salir ? 1 : 0;
                    boton.FlatAppearance.BorderColor = System.Drawing.Color.White;
                    boton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(46, 90, 136);
                    boton.FlatAppearance.MouseDownBackColor = frmBase.ColorAccion;
                    boton.BackColor = subitem ? System.Drawing.Color.FromArgb(42, 74, 114) : frmBase.ColorPrimario;
                    boton.ForeColor = System.Drawing.Color.White;
                    boton.Cursor = Cursors.Hand;
                    if (!salir)
                    {
                        boton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        boton.Padding = new Padding(subitem ? 34 : 16, 0, 0, 0);
                    }
                }
                else if (ctrl is Label)
                {
                    ctrl.ForeColor = System.Drawing.Color.White;
                }
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
            try
            {
                submenuUsuariosAbierto = !submenuUsuariosAbierto;
                ActualizarSubmenuUsuarios();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btn_SubUsuariosABM_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirSiTieneAcceso<frmUsuarioABM>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btn_SubUsuariosRoles_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirSiTieneAcceso<frmUsuarioRoles>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btn_SubUsuariosHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirSiTieneAcceso<frmUsuarioHistorial>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btn_GestionRoles_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirSiTieneAcceso<frmRolesPermisos>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btn_Seguridad_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirSiTieneAcceso<frmSeguridad>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btn_Bitacora_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirSiTieneAcceso<frmBitacora>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btn_HistorialTurnos_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirSiTieneAcceso<frmHistorialTurnos>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btn_AccesoMedico_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirSiTieneAcceso<frm_AccesoMedico>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btn_ValorTurno_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirSiTieneAcceso<frmValorTurno>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        // D02: ayuda de la pantalla abierta (o el indice de la ayuda si no hay ninguna).
        private void btn_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                Ayuda.Mostrar(ActiveMdiChild?.Name ?? Name, this);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btn_Liquidaciones_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirSiTieneAcceso<frmLiquidaciones>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
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
            try
            {
                AbrirSiTieneAcceso<frmABM>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
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
            try
            {
                AbrirSiTieneAcceso<frmGestionMedicos>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btn_GestionTurnos_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirSiTieneAcceso<frmGestionTurnos>();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }
    }
}
