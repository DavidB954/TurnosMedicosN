using BLL;
using BE;
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
using Servicios;


namespace Vista
{
    public partial class frmABM : frmBase
    {
        public frmABM()
        {
            InitializeComponent();
            CargarIdiomas();
            ConfigurarGrilla();
            CargarGrilla();
        }

        private void frmABM_Load(object sender, EventArgs e)
        {

        }

        private BLL_Traduccion bll = new BLL_Traduccion();

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }
        private void CargarIdiomas()
        {
            cmbIdioma.DataSource = bll.ObtenerIdiomas();
            cmbIdioma.DisplayMember = "Nombre";
            cmbIdioma.ValueMember = "Id";
        }

        private void ConfigurarGrilla()
        {
            dgvTraducciones.AutoGenerateColumns = false;
            dgvTraducciones.Columns.Clear();

            dgvTraducciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Visible = false
            });
            dgvTraducciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colClave",
                HeaderText = "Control",
                DataPropertyName = "Clave",
                Width = 300,
                ReadOnly = true
            });
            dgvTraducciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTexto",
                HeaderText = "Traducción",
                DataPropertyName = "Texto",
                Width = 300
            });
        }

        private void CargarGrilla()
        {
            var idioma = (Idioma)cmbIdioma.SelectedItem;
            if (idioma == null)
                return;
            dgvTraducciones.DataSource = bll.ObtenerPorIdioma(idioma.Id);
        }

        private void btnAgregarIdioma_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmNuevoIdioma();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                var nuevoIdioma = new Idioma
                {
                    Codigo = frm.Codigo,
                    Nombre = frm.Nombre
                };

                bll.GuardarIdioma(nuevoIdioma);

                var idiomaGuardado = bll.ObtenerIdiomas().Find(i => i.Codigo == nuevoIdioma.Codigo);

                // Copia las traducciones del idioma base (Id=1 Español) como texto por defecto
                var traduccionesBase = bll.ObtenerPorIdioma(1);

                foreach (var tBase in traduccionesBase)
                {
                    bll.Guardar(new Traduccion
                    {
                        Clave = tBase.Clave,
                        IdIdioma = idiomaGuardado.Id,
                        Texto = tBase.Texto
                    });
                }

                CargarIdiomas();
                CargarGrilla();
                MessageBox.Show($"Idioma '{nuevoIdioma.Nombre}' agregado con textos base en Español.", "Listo");
                TraduccionServicio.Instancia.NotificarCambioIdiomas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar idioma",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmbIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar traducciones",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCargarControles_Click(object sender, EventArgs e)
        {
            try
            {
                var idiomas = bll.ObtenerIdiomas();
                var existentes = bll.ObtenerTodas();
                int agregados = 0;

                var tiposForms = System.Reflection.Assembly.GetEntryAssembly()
                                 .GetTypes()
                                 .Where(t => t.IsSubclassOf(typeof(Form)) && !t.IsAbstract);

                foreach (var tipo in tiposForms)
                {
                    try
                    {
                        var frmRef = (Form)Activator.CreateInstance(tipo);
                        EscanearControles(frmRef.Controls, frmRef.Name, idiomas, existentes, ref agregados);
                    }
                    catch { }
                }

                CargarGrilla();
                MessageBox.Show(agregados > 0
                    ? $"Se agregaron {agregados} controles."
                    : "Todos los controles ya están cargados.", "Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar controles",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var idioma = (Idioma)cmbIdioma.SelectedItem;
                if (idioma == null)
                    return;

                var confirm = MessageBox.Show(
                    $"¿Eliminar el idioma '{idioma.Nombre}' y todas sus traducciones?",
                    "Confirmar", MessageBoxButtons.YesNo);

                if (confirm != DialogResult.Yes)
                    return;

                bll.EliminarIdioma(idioma.Id);
                CargarIdiomas();
                CargarGrilla();
                TraduccionServicio.Instancia.NotificarCambioIdiomas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al eliminar idioma",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var idioma = (Idioma)cmbIdioma.SelectedItem;
                if (idioma == null)
                    return;

                foreach (DataGridViewRow row in dgvTraducciones.Rows)
                {
                    if (row.IsNewRow)
                        continue;
                    bll.Guardar(new Traduccion
                    {
                        Id = (int)row.Cells["colId"].Value,
                        Clave = row.Cells["colClave"].Value.ToString(),
                        IdIdioma = idioma.Id,
                        Texto = row.Cells["colTexto"].Value?.ToString() ?? ""
                    });
                }
                MessageBox.Show("Traducciones guardadas.", "Listo");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar traducciones",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RegistrarClaveSiFalta(string clave, string textoPorDefecto, List<Idioma> idiomas, List<Traduccion> existentes, ref int agregados)
        {
            foreach (var idioma in idiomas)
            {
                bool yaExiste = existentes.Exists(t => t.Clave == clave && t.IdIdioma == idioma.Id);
                if (yaExiste)
                    continue;

                Traduccion nueva = new Traduccion
                {
                    Clave = clave,
                    IdIdioma = idioma.Id,
                    Texto = textoPorDefecto
                };
                bll.Guardar(nueva);

                // Se agrega a "existentes" al toque: si no, dos controles con el mismo nombre
                // (ej. "lblEtiqueta" dentro de cada instancia de ucSelectorMedico/ucSelectorEspecialidad)
                // generan la misma clave y, al no verse entre si en esta misma pasada, se duplican.
                existentes.Add(nueva);
                agregados++;
            }
        }

        private void EscanearControles(Control.ControlCollection controles, string nombreForm, List<Idioma> idiomas, List<Traduccion> existentes, ref int agregados)
        {
            foreach (Control ctrl in controles)
            {
                if (ctrl is Label || ctrl is Button || ctrl is GroupBox || ctrl is CheckBox)
                {
                    RegistrarClaveSiFalta($"{nombreForm}.{ctrl.Name}", ctrl.Text, idiomas, existentes, ref agregados);
                }

                // ucSelectorEspecialidad/ucSelectorMedico son UserControl reutilizados en varias
                // pantallas (frmGestionMedicos los repite varias veces): se registran por el nombre
                // de la instancia, no por sus controles internos (ej. "lblEtiqueta"), que se llaman
                // igual en todas las instancias y generarian la misma clave repetida. Ver el mismo
                // criterio en frmBase.AplicarIdiomaEnControles.
                if (ctrl is ucSelectorEspecialidad || ctrl is ucSelectorMedico)
                {
                    string etiqueta = ctrl is ucSelectorEspecialidad
                        ? ((ucSelectorEspecialidad)ctrl).Etiqueta
                        : ((ucSelectorMedico)ctrl).Etiqueta;
                    RegistrarClaveSiFalta($"{nombreForm}.{ctrl.Name}", etiqueta, idiomas, existentes, ref agregados);
                    continue;
                }

                if (ctrl is DataGridView dgv)
                {
                    foreach (DataGridViewColumn col in dgv.Columns)
                        RegistrarClaveSiFalta($"{nombreForm}.{dgv.Name}.{col.Name}", col.HeaderText, idiomas, existentes, ref agregados);
                }

                if (ctrl.Controls.Count > 0)
                    EscanearControles(ctrl.Controls, nombreForm, idiomas, existentes, ref agregados);
            }
        }
    }
}
