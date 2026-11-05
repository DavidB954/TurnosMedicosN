using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Vista
{
    // Menu "Mis recetas" del Paciente: todas sus recetas (de la mas reciente a la mas antigua),
    // los medicamentos de la elegida y su descarga en PDF. No muestra motivo ni indicaciones:
    // la Historia Clinica es exclusiva del rol Medico.
    public partial class frmMisRecetas : frmBase
    {
        BLL_Atencion bll_atencion = new BLL_Atencion();

        private List<BE_AtencionMedica> recetas = new List<BE_AtencionMedica>();

        // Fila de la grilla de recetas (solo para mostrar)
        public class FilaReceta
        {
            public BE_AtencionMedica Atencion { get; set; }
            public string Fecha { get; set; }
            public string NroReceta { get; set; }
            public string Medico { get; set; }
            public string Matricula { get; set; }
            public string Especialidad { get; set; }
            public string Medicamentos { get; set; }
        }

        public frmMisRecetas()
        {
            InitializeComponent();
        }

        private string T(string clave, string porDefecto)
        {
            return TraduccionServicio.Instancia.TraducirODefecto($"{Name}.{clave}", porDefecto);
        }

        private void frmMisRecetas_Load(object sender, EventArgs e)
        {
            try
            {
                DejarLugarAlMenu(tlpPrincipal);
                ConfigurarGrillas();
                CargarRecetas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
            Text = T("Titulo", "Mis recetas");
            ActualizarAyuda();
            MostrarMedicamentos();
        }

        private void ConfigurarGrillas()
        {
            dgvRecetas.AutoGenerateColumns = false;
            dgvRecetas.Columns.Clear();
            dgvRecetas.RowTemplate.Height = 30;
            AgregarColumna(dgvRecetas, "colFecha", "Fecha", "Fecha", 12);
            AgregarColumna(dgvRecetas, "colNroReceta", "N° de receta", "NroReceta", 16);
            AgregarColumna(dgvRecetas, "colMedico", "Médico", "Medico", 18);
            AgregarColumna(dgvRecetas, "colMatricula", "Matrícula", "Matricula", 12);
            AgregarColumna(dgvRecetas, "colEspecialidad", "Especialidad", "Especialidad", 16);
            AgregarColumna(dgvRecetas, "colMedicamentos", "Medicamentos", "Medicamentos", 26);

            dgvMedicamentos.AutoGenerateColumns = false;
            dgvMedicamentos.Columns.Clear();
            dgvMedicamentos.RowTemplate.Height = 28;
            AgregarColumna(dgvMedicamentos, "colMedicamento", "Medicamento", "Medicamento", 28);
            AgregarColumna(dgvMedicamentos, "colPresentacion", "Presentación", "Presentacion", 24);
            AgregarColumna(dgvMedicamentos, "colPosologia", "Posología", "Posologia", 38);
            AgregarColumna(dgvMedicamentos, "colCantidad", "Cant.", "Cantidad", 10);
        }

        private void AgregarColumna(DataGridView grilla, string nombre, string encabezado, string propiedad, float peso)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = nombre;
            columna.HeaderText = T($"{grilla.Name}.{nombre}", encabezado);
            columna.DataPropertyName = propiedad;
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            columna.FillWeight = peso;
            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            grilla.Columns.Add(columna);
        }

        private void CargarRecetas()
        {
            recetas = bll_atencion.ObtenerMisRecetas();

            dgvRecetas.DataSource = recetas.Select(a => new FilaReceta
            {
                Atencion = a,
                Fecha = a.Receta.FechaEmision.ToString("dd/MM/yyyy"),
                NroReceta = a.Receta.NroReceta,
                Medico = a.Turno.NombreApellidoMedico,
                Matricula = a.Turno.MatriculaMedico,
                Especialidad = a.Turno.Especialidades,
                Medicamentos = string.Join(", ", a.Receta.Items.Select(i => i.Medicamento))
            }).ToList();

            ActualizarAyuda();
            MostrarMedicamentos();
        }

        private BE_AtencionMedica RecetaSeleccionada()
        {
            if (dgvRecetas.CurrentRow == null || !dgvRecetas.CurrentRow.Selected)
                return null;
            return (dgvRecetas.CurrentRow.DataBoundItem as FilaReceta)?.Atencion;
        }

        private void ActualizarAyuda()
        {
            lblAyuda.Text = recetas.Count == 0
                ? T("Msg.SinRecetas", "Todavía no tiene recetas emitidas.")
                : T("lblAyuda", "Seleccione una receta para ver sus medicamentos y descargarla en PDF.");
            btnDescargarPdf.Enabled = recetas.Count > 0;
        }

        private void MostrarMedicamentos()
        {
            BE_AtencionMedica seleccionada = RecetaSeleccionada();
            if (seleccionada == null)
            {
                lblMedicamentos.Text = recetas.Count == 0 ? "" : T("Msg.ElegirReceta", "Seleccione una receta de la lista.");
                dgvMedicamentos.DataSource = null;
                return;
            }

            lblMedicamentos.Text = string.Format(T("Msg.Medicamentos", "Medicamentos de la receta {0}"), seleccionada.Receta.NroReceta);
            dgvMedicamentos.DataSource = seleccionada.Receta.Items;
            dgvMedicamentos.ClearSelection();
        }

        private void dgvRecetas_SelectionChanged(object sender, EventArgs e)
        {
            MostrarMedicamentos();
        }

        private void dgvRecetas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && btnDescargarPdf.Visible)
                btnDescargarPdf_Click(btnDescargarPdf, EventArgs.Empty);
        }

        private void btnDescargarPdf_Click(object sender, EventArgs e)
        {
            BE_AtencionMedica seleccionada = RecetaSeleccionada();
            if (seleccionada == null)
            {
                MessageBox.Show(T("Msg.ElegirReceta", "Seleccione una receta de la lista."), Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DescargadorReceta.Descargar(this, seleccionada);
        }
    }
}
