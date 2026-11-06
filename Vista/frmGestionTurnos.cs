using BE;
using BLL;
using Servicios;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Vista
{
    // Pantalla de Administrativo/Administrador para RF 01.2: a diferencia de
    // frm_AccesoPaciente (donde el paciente solo ve y cancela sus propios turnos), aca se
    // puede buscar el turno de cualquier paciente y cancelarlo.
    public partial class frmGestionTurnos : frmBase
    {
        BLL_Turno bll_turno = new BLL_Turno();

        public frmGestionTurnos()
        {
            InitializeComponent();
        }

        // Valores del filtro de estado tal como están en la base (índice 0 = Todos = sin filtro).
        // El combo muestra el texto traducido; el filtro usa siempre este valor.
        private static readonly string[] estadosFiltro = { null, "Confirmado", "Cancelado", "Atendido", "Ausente" };

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
            CargarComboEstado();
        }

        private void CargarComboEstado()
        {
            var t = Servicios.TraduccionServicio.Instancia;
            int indice = cbEstado.SelectedIndex < 0 ? 1 : cbEstado.SelectedIndex;

            cbEstado.Items.Clear();
            cbEstado.Items.Add(t.TraducirODefecto($"{Name}.Estado.Todos", "Todos"));
            foreach (string estado in estadosFiltro.Skip(1))
                cbEstado.Items.Add(t.TraducirODefecto($"{Name}.Estado.{estado}", estado));
            cbEstado.SelectedIndex = indice;
        }

        private void frmGestionTurnos_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDesde.Checked = false;
                dtpHasta.Checked = false;

                CargarComboEstado();

                ConfigurarColumnas();
                CargarTurnos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar la gestion de turnos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigurarColumnas()
        {
            dgvTurnos.AutoGenerateColumns = false;
            dgvTurnos.Columns.Clear();

            AgregarColumna("colIdTurno", "Turno", "IdTurno");
            AgregarColumna("colFecha", "Fecha", "FechaTexto");
            AgregarColumna("colHorario", "Horario", "RangoHorario");
            AgregarColumna("colPaciente", "Paciente", "NombreApellidoPaciente");
            AgregarColumna("colMedico", "Medico", "NombreApellidoMedico");
            AgregarColumna("colEspecialidad", "Especialidad", "Especialidades");
            AgregarColumna("colEstado", "Estado", "Estado");
        }

        private void AgregarColumna(string nombre, string encabezado, string propiedad)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = nombre;
            columna.HeaderText = Servicios.TraduccionServicio.Instancia.TraducirODefecto($"{Name}.dgvTurnos.{nombre}", encabezado);
            columna.DataPropertyName = propiedad;
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTurnos.Columns.Add(columna);
        }

        public void CargarTurnos()
        {
            try
            {
                int? idTurno = null;
                if (!string.IsNullOrWhiteSpace(txtIdTurno.Text))
                {
                    int valor;
                    if (!int.TryParse(txtIdTurno.Text, out valor))
                        throw new ArgumentException("El numero de turno debe ser numerico.");
                    idTurno = valor;
                }

                DateTime? desde = dtpDesde.Checked ? dtpDesde.Value.Date : (DateTime?)null;
                DateTime? hasta = dtpHasta.Checked ? dtpHasta.Value.Date : (DateTime?)null;

                string estado = cbEstado.SelectedIndex > 0 ? estadosFiltro[cbEstado.SelectedIndex] : null;

                dgvTurnos.DataSource = null;
                dgvTurnos.DataSource = bll_turno.Filtrar(idTurno, desde, hasta, estado, txtPaciente.Text);

                lblTotalRegistros.Text = dgvTurnos.Rows.Count.ToString();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar turnos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarTurnos();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtIdTurno.Clear();
            txtPaciente.Clear();
            dtpDesde.Value = DateTime.Now;
            dtpDesde.Checked = false;
            dtpHasta.Value = DateTime.Now;
            dtpHasta.Checked = false;
            cbEstado.SelectedIndex = 0;
            CargarTurnos();
        }

        private void btnCancelarTurno_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTurnos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione, en la grilla, el turno que quiere cancelar.");
                    return;
                }

                BE_Turno turno = (BE_Turno)dgvTurnos.CurrentRow.DataBoundItem;

                if (turno.Estado != "Confirmado")
                {
                    MessageBox.Show("Solo se pueden cancelar turnos en estado Confirmado.");
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Cancelar el turno {turno.IdTurno} de {turno.NombreApellidoPaciente} ({turno.FechaTexto} {turno.RangoHorario})?",
                    "Confirmar cancelacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                    return;

                int idUsuarioQueCancela = Sesion.Instancia().UsuarioActual.IdUsuario;
                bll_turno.CancelarTurno(turno.IdTurno, idUsuarioQueCancela);

                MessageBox.Show("Turno cancelado.");
                CargarTurnos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cancelar el turno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
