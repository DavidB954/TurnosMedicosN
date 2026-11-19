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
using static BE.BE_Traduccion;

namespace Vista
{
    public partial class frmBase : Form
    {
        public frmBase()
        {
            InitializeComponent();
        }

        private void frmBase_Load(object sender, EventArgs e)
        {

        }

        public virtual void AplicarIdioma()
        {
        }
        protected void CambiarIdioma(Idioma idioma)
        {
            var diccionario = new BLL_Traduccion().ObtenerDiccionario(idioma.Id);
            TraduccionServicio.Instancia.CambiarIdioma(idioma, diccionario);
            var usuario = Sesion.Instancia().UsuarioActual;
            if (usuario != null)
                new BLL_Traduccion().GuardarIdiomaUsuario(usuario.IdUsuario, idioma.Id);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            TraduccionServicio.Instancia.IdiomaChanged += OnIdiomaChanged;
            if (TraduccionServicio.Instancia.IdiomaActual != null)
                AplicarIdioma();
            AplicarPermisosAutomatico();
        }

        // Oculta cada botón cuyo "<ClaseFormulario>.<NombreBoton>" esté en el catálogo de
        // permisos y el usuario no tenga. Los botones que no están en el catálogo
        // (Buscar, Limpiar, Volver...) no se tocan.
        // D02 Ayuda en linea: F1 en cualquier pantalla abre su pagina de ayuda.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                try
                {
                    Ayuda.Mostrar(Name, this);
                }
                catch (Exception ex)
                {
                    Errores.Mostrar(ex);
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected void AplicarPermisosAutomatico()
        {
            AplicarPermisosEnControles(this.Controls, GetType().Name);
        }

        private void AplicarPermisosEnControles(Control.ControlCollection controles, string formulario)
        {
            var sesion = Sesion.Instancia();

            foreach (Control ctrl in controles)
            {
                if (ctrl is Button)
                {
                    string clave = $"{formulario}.{ctrl.Name}";
                    if (sesion.EstaEnCatalogo(clave))
                        ctrl.Visible = sesion.TienePermiso(clave);
                }

                if (ctrl.Controls.Count > 0)
                    AplicarPermisosEnControles(ctrl.Controls, formulario);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            TraduccionServicio.Instancia.IdiomaChanged -= OnIdiomaChanged;
        }

        private void OnIdiomaChanged(Idioma idioma)
        {
            if (InvokeRequired)
                Invoke(new Action<Idioma>(OnIdiomaChanged), idioma);
            else
                AplicarIdioma();
        }

        protected void AplicarIdiomaAutomatico()
        {
            var t = TraduccionServicio.Instancia;
            AplicarIdiomaEnControles(this.Controls, this.Name, t);
        }

        protected bool AplicarPermiso(Control control, string permiso, bool ocultarSiNoTiene = false)
        {
            if (control == null)
                return false;

            bool tienePermiso = Sesion.Instancia().TienePermiso(permiso);

            if (ocultarSiNoTiene)
                control.Visible = tienePermiso;
            else
                control.Enabled = tienePermiso;

            return tienePermiso;
        }

        // Variante para aplicar varios controles de una sola vez.
        // Devuelve el mismo diccionario de permisos resueltos (control -> tienePermiso),
        // por si el formulario necesita lógica adicional (ej. reacomodar layout).
        protected Dictionary<Control, bool> AplicarPermisos(Dictionary<Control, string> mapaPermisos, bool ocultarSiNoTiene = false)
        {
            var resultado = new Dictionary<Control, bool>();

            if (mapaPermisos == null)
                return resultado;

            foreach (var par in mapaPermisos)
                resultado[par.Key] = AplicarPermiso(par.Key, par.Value, ocultarSiNoTiene);

            return resultado;
        }

        // Atajo para verificar un permiso sin tocar ningún control,
        // útil para condicionar lógica dentro de un método (ej. antes de ejecutar una acción).
        protected bool TienePermiso(string permiso)
        {
            return Sesion.Instancia().TienePermiso(permiso);
        }

        private void AplicarIdiomaEnControles(Control.ControlCollection controles, string nombreForm, TraduccionServicio t)
        {
            foreach (Control ctrl in controles)
            {
                string clave = $"{nombreForm}.{ctrl.Name}";

                if (ctrl is Label || ctrl is Button || ctrl is GroupBox || ctrl is CheckBox)
                {
                    string traduccion = t.Traducir(clave);
                    if (!traduccion.StartsWith("["))
                        ctrl.Text = traduccion;
                }

                // ucSelectorEspecialidad/ucSelectorMedico son UserControl reutilizados en varias
                // pantallas: se traducen por el nombre de la instancia (clave), no por sus
                // controles internos, que se llaman igual en todas las instancias.
                if (ctrl is ucSelectorEspecialidad || ctrl is ucSelectorMedico)
                {
                    string traduccion = t.Traducir(clave);
                    if (!traduccion.StartsWith("["))
                    {
                        if (ctrl is ucSelectorEspecialidad)
                            ((ucSelectorEspecialidad)ctrl).Etiqueta = traduccion;
                        else
                            ((ucSelectorMedico)ctrl).Etiqueta = traduccion;
                    }
                    continue;
                }

                if (ctrl is DataGridView dgv)
                {
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        string claveCol = $"{nombreForm}.{dgv.Name}.{col.Name}";
                        string traduccion = t.Traducir(claveCol);
                        if (!traduccion.StartsWith("["))
                            col.HeaderText = traduccion;
                    }
                }

                if (ctrl is MenuStrip menu)
                {
                    foreach (ToolStripMenuItem item in menu.Items)
                    {
                        string claveItem = $"{nombreForm}.{item.Name}";
                        string traduccion = t.Traducir(claveItem);
                        if (!traduccion.StartsWith("["))
                            item.Text = traduccion;

                        foreach (ToolStripItem subItem in item.DropDownItems)
                        {
                            string claveSubItem = $"{nombreForm}.{subItem.Name}";
                            string traduccionSub = t.Traducir(claveSubItem);
                            if (!traduccionSub.StartsWith("["))
                                subItem.Text = traduccionSub;
                        }
                    }
                }

                if (ctrl.Controls.Count > 0)
                    AplicarIdiomaEnControles(ctrl.Controls, nombreForm, t);
            }
        }

        // ---------------------------------------------------------------- Estilo visual comun

        // Distribucion de los prototipos de TD2026.docx con la paleta del sistema: azul marino (titulos de
        // paneles y encabezados de grillas), verde azulado (acciones) y rojo (acciones que anulan algo).
        public static readonly Color ColorPrimario = Color.FromArgb(31, 58, 95);
        public static readonly Color ColorAccion = Color.FromArgb(0, 122, 135);
        public static readonly Color ColorPeligro = Color.FromArgb(192, 57, 43);
        public static readonly Color ColorFondo = Color.FromArgb(236, 241, 246);
        public static readonly Color ColorFilaAlterna = Color.FromArgb(232, 241, 246);
        public static readonly Color ColorLinea = Color.FromArgb(206, 217, 228);

        // Las pantallas tienen tamaño fijo de diseño: si no entran en el monitor (resolucion baja o
        // Windows con escala mayor a 100%) aparecen barras de desplazamiento en lugar de cortarse.
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!IsMdiContainer)
                AutoScroll = true;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            AplicarEstilo();
        }

        private void AplicarEstilo()
        {
            if (IsMdiContainer)
            {
                foreach (MdiClient area in Controls.OfType<MdiClient>())
                    area.BackColor = ColorFondo;
            }
            else
            {
                BackColor = ColorFondo;
            }
            AplicarEstiloEnControles(Controls);
        }

        private void AplicarEstiloEnControles(Control.ControlCollection controles)
        {
            foreach (Control ctrl in controles)
            {
                // El menu lateral del formulario principal tiene su propio estilo.
                if (ctrl.Name == "groupBox_Menu")
                    continue;

                if (ctrl is Button boton)
                    AplicarEstiloBoton(boton);
                else if (ctrl is DataGridView grilla)
                    AplicarEstiloGrilla(grilla);
                else if (ctrl is GroupBox panel)
                {
                    // El titulo del panel en azul; los controles de adentro conservan el texto negro.
                    panel.ForeColor = ColorPrimario;
                    foreach (Control hijo in panel.Controls)
                        if (!(hijo is Button))
                            hijo.ForeColor = SystemColors.ControlText;
                }

                if (ctrl.Controls.Count > 0 && !(ctrl is DataGridView))
                    AplicarEstiloEnControles(ctrl.Controls);
            }
        }

        // Los botones que ya tienen un color propio (UseVisualStyleBackColor = false) no se tocan.
        private void AplicarEstiloBoton(Button boton)
        {
            if (!boton.UseVisualStyleBackColor)
                return;

            string nombre = boton.Name.ToLowerInvariant();
            bool peligro = nombre.Contains("eliminar") || nombre.Contains("cancelarturno") || nombre.Contains("quitar")
                           || nombre.Contains("inactivar") || nombre.Contains("baja") || nombre.Contains("bloquear");
            bool secundario = !peligro && (nombre.Contains("limpiar") || nombre == "btncancelar" || nombre.Contains("volver")
                                           || nombre.Contains("cerrar") || nombre.Contains("hoy"));

            boton.FlatStyle = FlatStyle.Flat;
            boton.Cursor = Cursors.Hand;
            if (secundario)
            {
                boton.BackColor = Color.White;
                boton.ForeColor = ColorPrimario;
                boton.FlatAppearance.BorderColor = ColorPrimario;
                boton.FlatAppearance.BorderSize = 1;
            }
            else
            {
                boton.BackColor = peligro ? ColorPeligro : ColorAccion;
                boton.ForeColor = Color.White;
                boton.FlatAppearance.BorderSize = 0;
            }
        }

        private void AplicarEstiloGrilla(DataGridView grilla)
        {
            grilla.BackgroundColor = Color.White;
            grilla.BorderStyle = BorderStyle.FixedSingle;
            grilla.GridColor = ColorLinea;
            grilla.EnableHeadersVisualStyles = false;
            grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grilla.ColumnHeadersDefaultCellStyle.BackColor = ColorPrimario;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorPrimario;
            grilla.DefaultCellStyle.SelectionBackColor = ColorAccion;
            grilla.DefaultCellStyle.SelectionForeColor = Color.White;
            grilla.AlternatingRowsDefaultCellStyle.BackColor = ColorFilaAlterna;
        }


    }
}
