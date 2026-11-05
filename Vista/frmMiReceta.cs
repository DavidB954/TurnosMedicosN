using BE;
using BLL;
using Servicios;
using System;
using System.Windows.Forms;

namespace Vista
{
    // Receta de un turno Atendido del paciente (medico, fecha, N° y medicamentos) y su descarga en PDF.
    // NO muestra motivo ni indicaciones: la Historia Clinica es exclusiva del rol Medico.
    // Se abre desde "Mis Turnos" (frm_AccesoPaciente).
    public partial class frmMiReceta : frmBase
    {
        BLL_Atencion bll_atencion = new BLL_Atencion();

        private readonly BE_AtencionMedica receta;

        // Constructor sin parametros solo para el diseñador de Visual Studio.
        public frmMiReceta()
        {
            InitializeComponent();
        }

        public frmMiReceta(BE_AtencionMedica receta) : this()
        {
            this.receta = receta;
        }

        private string T(string clave, string porDefecto)
        {
            return TraduccionServicio.Instancia.TraducirODefecto($"{Name}.{clave}", porDefecto);
        }

        private void frmMiReceta_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla();
                MostrarReceta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
            Text = T("Titulo", "Mi receta");
            MostrarReceta();
        }

        private void ConfigurarGrilla()
        {
            dgvMedicamentos.AutoGenerateColumns = false;
            dgvMedicamentos.Columns.Clear();
            AgregarColumna("colMedicamento", "Medicamento", "Medicamento", 30);
            AgregarColumna("colPresentacion", "Presentación", "Presentacion", 25);
            AgregarColumna("colPosologia", "Posología", "Posologia", 35);
            AgregarColumna("colCantidad", "Cant.", "Cantidad", 10);
        }

        private void AgregarColumna(string nombre, string encabezado, string propiedad, float peso)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = nombre;
            columna.HeaderText = T($"dgvMedicamentos.{nombre}", encabezado);
            columna.DataPropertyName = propiedad;
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            columna.FillWeight = peso;
            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvMedicamentos.Columns.Add(columna);
        }

        private void MostrarReceta()
        {
            if (receta == null || receta.Receta == null)
                return;

            lblTitulo.Text = string.Format(T("Msg.RecetaNro", "Receta N° {0}"), receta.Receta.NroReceta);
            lblFecha.Text = string.Format(T("Msg.EmitidaEl", "Emitida el {0:dd/MM/yyyy}"), receta.Receta.FechaEmision);

            string especialidad = string.IsNullOrEmpty(receta.Turno.Especialidades) ? "" : $" ({receta.Turno.Especialidades})";
            string matricula = string.IsNullOrEmpty(receta.Turno.MatriculaMedico) ? "" : $" · {T("Msg.Matricula", "Matrícula")} {receta.Turno.MatriculaMedico}";
            lblMedico.Text = $"{T("Msg.Medico", "Médico")}: {receta.Turno.NombreApellidoMedico}{especialidad}{matricula}";

            if (dgvMedicamentos.DataSource == null)
                dgvMedicamentos.DataSource = receta.Receta.Items;
            dgvMedicamentos.ClearSelection();
        }

        private void btnDescargarPdf_Click(object sender, EventArgs e)
        {
            DescargadorReceta.Descargar(this, receta);
        }
    }
}
