using BE;
using BLL;
using Servicios;
using System;
using System.Windows.Forms;

namespace Vista
{
    // T06b Control de cambios de los turnos. El recepcionista y el administrador ven el historial; solo el
    // administrador restaura (CU-T06.1 Restaurar version de un turno; el boton se oculta por permiso). Las versiones las guarda el trigger
    // TR_Turno_Historial en la tabla copia Turno_Historial.
    public partial class frmHistorialTurnos : frmBase
    {
        BLL_Turno bll_turno = new BLL_Turno();
        BLL_TurnoHistorial bll_turnoHistorial = new BLL_TurnoHistorial();

        private bool cargandoGrilla = false;

        public frmHistorialTurnos()
        {
            InitializeComponent();
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }

        private void frmHistorialTurnos_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDesde.Checked = false;
                dtpHasta.Checked = false;
                ConfigurarGrillas();
                CargarTurnos();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void ConfigurarGrillas()
        {
            dgvTurnos.AutoGenerateColumns = false;
            dgvTurnos.Columns.Clear();
            AgregarColumna(dgvTurnos, "colIdTurno", "Nro.", "IdTurno");
            AgregarColumna(dgvTurnos, "colFecha", "Fecha", "FechaTexto");
            AgregarColumna(dgvTurnos, "colHora", "Hora", "HoraTexto");
            AgregarColumna(dgvTurnos, "colPaciente", "Paciente", "NombreApellidoPaciente");
            AgregarColumna(dgvTurnos, "colMedico", "Médico", "NombreApellidoMedico");
            AgregarColumna(dgvTurnos, "colEstado", "Estado actual", "Estado");

            dgvVersiones.AutoGenerateColumns = false;
            dgvVersiones.Columns.Clear();
            AgregarColumna(dgvVersiones, "colFechaHoraCambio", "Fecha y hora del cambio", "FechaHoraCambioTexto");
            AgregarColumna(dgvVersiones, "colUsuario", "Usuario", "NombreUsuarioCambio");
            AgregarColumna(dgvVersiones, "colOperacion", "Operación", "OperacionTexto");
            AgregarColumna(dgvVersiones, "colFecha", "Fecha", "FechaTexto");
            AgregarColumna(dgvVersiones, "colHorario", "Horario", "RangoHorario");
            AgregarColumna(dgvVersiones, "colEstado", "Estado", "Estado");
        }

        private void AgregarColumna(DataGridView grilla, string nombre, string encabezado, string propiedad)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = nombre;
            columna.HeaderText = TraduccionServicio.Instancia.TraducirODefecto($"{Name}.{grilla.Name}.{nombre}", encabezado);
            columna.DataPropertyName = propiedad;
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            grilla.Columns.Add(columna);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarTurnos();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void CargarTurnos()
        {
            cargandoGrilla = true;
            try
            {
                DateTime? desde = dtpDesde.Checked ? dtpDesde.Value.Date : (DateTime?)null;
                DateTime? hasta = dtpHasta.Checked ? dtpHasta.Value.Date : (DateTime?)null;

                dgvTurnos.DataSource = null;
                dgvTurnos.DataSource = bll_turno.Filtrar(null, desde, hasta, null, txtPaciente.Text);
                dgvTurnos.ClearSelection();
                dgvTurnos.CurrentCell = null;
                dgvVersiones.DataSource = null;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
            finally
            {
                cargandoGrilla = false;
            }
        }

        // CU-T06.1 pasos 3 y 4: al elegir un turno se muestran todas sus versiones.
        private void dgvTurnos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (cargandoGrilla)
                    return;

                BE_Turno turno = dgvTurnos.CurrentRow?.DataBoundItem as BE_Turno;
                if (turno == null)
                    return;

                CargarHistorial(turno.IdTurno);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void CargarHistorial(int idTurno)
        {
            try
            {
                dgvVersiones.DataSource = null;
                dgvVersiones.DataSource = bll_turnoHistorial.ObtenerHistorial(idTurno);
                dgvVersiones.ClearSelection();
                dgvVersiones.CurrentCell = null;
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        // CU-T06.1 pasos 5 a 7: vuelve el turno a los datos de la version elegida.
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            try
            {
                BE_TurnoHistorial version = dgvVersiones.CurrentRow?.DataBoundItem as BE_TurnoHistorial;
                if (version == null || dgvVersiones.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione, en \"Versiones del turno seleccionado\", la versión que quiere restaurar.");
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Restaurar el turno {version.IdTurno} a la versión del {version.FechaHoraCambioTexto}?\n" +
                    $"Quedará: {version.FechaTexto} {version.RangoHorario}, estado {version.Estado}.",
                    "Confirmar restauración", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                    return;

                bll_turnoHistorial.RestaurarVersion(version.IdHistorial);

                MessageBox.Show("Versión restaurada. El cambio quedó registrado como una nueva versión del turno.");
                int idTurno = version.IdTurno;
                CargarTurnos();
                SeleccionarTurno(idTurno);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        // Desde Gestion de turnos (boton Ver historial): muestra solo ese turno y sus versiones.
        public void MostrarTurno(int idTurno)
        {
            cargandoGrilla = true;
            try
            {
                txtPaciente.Text = "";
                dtpDesde.Checked = false;
                dtpHasta.Checked = false;
                dgvTurnos.DataSource = null;
                dgvTurnos.DataSource = bll_turno.Filtrar(idTurno, null, null, null, "");
                dgvVersiones.DataSource = null;
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
            finally
            {
                cargandoGrilla = false;
            }
            SeleccionarTurno(idTurno);
        }

        private void SeleccionarTurno(int idTurno)
        {
            foreach (DataGridViewRow fila in dgvTurnos.Rows)
            {
                BE_Turno turno = fila.DataBoundItem as BE_Turno;
                if (turno != null && turno.IdTurno == idTurno)
                {
                    dgvTurnos.CurrentCell = fila.Cells[0];
                    fila.Selected = true;
                    CargarHistorial(idTurno);
                    return;
                }
            }
        }
    }
}
