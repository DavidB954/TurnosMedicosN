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

                if (ctrl is Label || ctrl is Button || ctrl is GroupBox)
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


    }
}
