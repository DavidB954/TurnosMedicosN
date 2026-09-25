using BE;
using BLL;
using System;
using System.Windows.Forms;

namespace Vista
{
    // Bitacora adicional, orientada a eventos: solo lectura, alimentada por los triggers
    // TR_Turno_Insert_BitacoraEventos / TR_Turno_Update_BitacoraEventos (ver
    // DB/Actualizacion_BitacoraEventosTurno.sql). A diferencia de frmBitacora (que registra
    // lo que la BLL decide auditar), esta queda completa aunque el cambio sobre Turno se
    // haga por fuera de la aplicacion.
    public partial class frm_BitacoraTurno : frmBase
    {
        BLL_BitacoraEventoTurno bll_bitacoraEventoTurno = new BLL_BitacoraEventoTurno();

        public frm_BitacoraTurno()
        {
            InitializeComponent();
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }

        private void frm_BitacoraTurno_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDesde.Checked = false;
                dtpHasta.Checked = false;
                ConfigurarColumnas();
                CargarEventos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar la bitácora de eventos de turno",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigurarColumnas()
        {
            dgvEventos.AutoGenerateColumns = false;
            dgvEventos.Columns.Clear();

            AgregarColumna("colFechaHora", "Fecha y Hora", "FechaHora");
            AgregarColumna("colTipoEvento", "Evento", "TipoEvento");
            AgregarColumna("colEstadoAnterior", "Estado Anterior", "EstadoAnterior");
            AgregarColumna("colEstadoNuevo", "Estado Nuevo", "EstadoNuevo");
            AgregarColumna("colPaciente", "Paciente", "NombreApellidoPaciente");
            AgregarColumna("colMedico", "Medico", "NombreApellidoMedico");
            AgregarColumna("colFechaTurno", "Fecha Turno", "FechaTurnoTexto");
            AgregarColumna("colHorario", "Horario", "RangoHorario");
            AgregarColumna("colIdTurno", "Turno", "IdTurno");
            AgregarColumna("colUsuarioBD", "Usuario SQL", "UsuarioBD");
            AgregarColumna("colHostBD", "Equipo", "HostBD");
        }

        private void AgregarColumna(string nombre, string encabezado, string propiedad)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = nombre;
            columna.HeaderText = encabezado;
            columna.DataPropertyName = propiedad;
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvEventos.Columns.Add(columna);
        }

        public void CargarEventos()
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
                DateTime? hasta = dtpHasta.Checked ? dtpHasta.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

                dgvEventos.DataSource = null;
                dgvEventos.DataSource = bll_bitacoraEventoTurno.Filtrar(idTurno, desde, hasta);

                lblTotalRegistros.Text = dgvEventos.Rows.Count.ToString();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar en la bitácora de eventos de turno",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarEventos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarEventos();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtIdTurno.Clear();
            dtpDesde.Value = DateTime.Now;
            dtpDesde.Checked = false;
            dtpHasta.Value = DateTime.Now;
            dtpHasta.Checked = false;
            CargarEventos();
        }
    }
}
