using BE;
using BLL;
using Servicios;
using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class frm_AccesoPaciente : frmBase
    {
        BLL_MedicoHorario bll_medicoHorario = new BLL_MedicoHorario();
        BLL_Turno bll_turno = new BLL_Turno();
        BLL_Atencion bll_atencion = new BLL_Atencion();

        public frm_AccesoPaciente()
        {
            InitializeComponent();
            dtpFecha.MinDate = DateTime.Today;
            dtpFecha.Value = DateTime.Today;

            ucEspecialidad.EspecialidadSeleccionada += ucEspecialidad_EspecialidadSeleccionada;
            ucMedico.MedicoSeleccionado += ucMedico_MedicoSeleccionado;
            dtpFecha.ValueChanged += dtpFecha_ValueChanged;
        }

        private void frm_AccesoPaciente_Load(object sender, EventArgs e)
        {
            try
            {
                ucEspecialidad.CargarEspecialidades();
                CargarMisTurnos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucEspecialidad_EspecialidadSeleccionada(object sender, EventArgs e)
        {
            try
            {
                CargarMedicosPorEspecialidad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Solo se listan los medicos que tengan asignada la especialidad elegida
        // (Medico_Especialidad, cargado desde frmGestionMedicos).
        private void CargarMedicosPorEspecialidad()
        {
            if (ucEspecialidad.IdEspecialidadSeleccionada == null)
                ucMedico.Limpiar();
            else
                ucMedico.CargarMedicosPorEspecialidad(ucEspecialidad.IdEspecialidadSeleccionada.Value);

            // Los horarios ofrecidos dependen tambien de la especialidad elegida.
            CargarHorariosDelMedico();
        }

        private void ucMedico_MedicoSeleccionado(object sender, EventArgs e)
        {
            try
            {
                CargarHorariosDelMedico();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CargarHorariosDelMedico();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Horarios Activos del medico que caen en el dia de la semana de la fecha elegida y
        // que todavia no tienen un turno Confirmado para esa fecha exacta.
        private void CargarHorariosDelMedico()
        {
            if (ucMedico.IdMedicoSeleccionado == null)
            {
                lstHorarios.DataSource = null;
                return;
            }

            lstHorarios.DataSource = bll_medicoHorario._listaDisponibles(ucMedico.IdMedicoSeleccionado.Value, dtpFecha.Value.Date, ucEspecialidad.IdEspecialidadSeleccionada);
            lstHorarios.DisplayMember = "RangoHorario";
        }

        private void btnReservarTurno_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstHorarios.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un horario disponible.");
                    return;
                }

                BE_MedicoHorario horario = (BE_MedicoHorario)lstHorarios.SelectedItem;
                int idPaciente = Sesion.Instancia().UsuarioActual.IdUsuario;

                bll_turno.SolicitarTurno(idPaciente, horario.IdHorario, dtpFecha.Value.Date);

                MessageBox.Show("Turno reservado correctamente.");
                CargarHorariosDelMedico();
                CargarMisTurnos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificarTurno_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione, en 'Mis Turnos', el turno que quiere modificar.");
                    return;
                }

                if (lstHorarios.SelectedItem == null)
                {
                    MessageBox.Show("Elija arriba el nuevo horario disponible antes de modificar.");
                    return;
                }

                int idTurno = Convert.ToInt32(dataGridView1.CurrentRow.Cells["colIdTurno"].Value);
                BE_MedicoHorario horario = (BE_MedicoHorario)lstHorarios.SelectedItem;
                int idPaciente = Sesion.Instancia().UsuarioActual.IdUsuario;

                bll_turno.ModificarTurno(idTurno, horario.IdHorario, dtpFecha.Value.Date, idPaciente);

                MessageBox.Show("Turno modificado correctamente.");
                CargarHorariosDelMedico();
                CargarMisTurnos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarTurnos_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione, en 'Mis Turnos', el turno que quiere cancelar.");
                    return;
                }

                int idTurno = Convert.ToInt32(dataGridView1.CurrentRow.Cells["colIdTurno"].Value);
                int idPaciente = Sesion.Instancia().UsuarioActual.IdUsuario;

                bll_turno.CancelarTurno(idTurno, idPaciente);

                MessageBox.Show("Turno cancelado.");
                CargarHorariosDelMedico();
                CargarMisTurnos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Receta de un turno Atendido (solo la receta: el motivo y las indicaciones son de la
        // Historia Clinica, exclusiva del Medico). Desde la ventana se descarga en PDF.
        private void btnVerReceta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione, en 'Mis Turnos', un turno Atendido.");
                    return;
                }

                int idTurno = Convert.ToInt32(dataGridView1.CurrentRow.Cells["colIdTurno"].Value);
                BE_AtencionMedica receta = bll_atencion.ObtenerMiReceta(idTurno);

                using (frmMiReceta frm = new frmMiReceta(receta))
                {
                    frm.ShowDialog(this);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Mi receta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Doble clic en un turno Atendido: abre su receta.
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !btnVerReceta.Visible)
                return;

            BE_Turno turno = dataGridView1.Rows[e.RowIndex].DataBoundItem as BE_Turno;
            if (turno != null && turno.Estado == "Atendido")
                btnVerReceta_Click(btnVerReceta, EventArgs.Empty);
        }

        public void CargarMisTurnos()
        {
            try
            {
                int idPaciente = Sesion.Instancia().UsuarioActual.IdUsuario;

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                DataGridViewTextBoxColumn colIdTurno = new DataGridViewTextBoxColumn();
                colIdTurno.Name = "colIdTurno";
                colIdTurno.DataPropertyName = "IdTurno";
                colIdTurno.Visible = false;
                dataGridView1.Columns.Add(colIdTurno);

                DataGridViewTextBoxColumn colFecha = new DataGridViewTextBoxColumn();
                colFecha.Name = "colFecha";
                colFecha.HeaderText = "Fecha";
                colFecha.DataPropertyName = "FechaTexto";
                colFecha.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns.Add(colFecha);

                DataGridViewTextBoxColumn colHora = new DataGridViewTextBoxColumn();
                colHora.Name = "colHora";
                colHora.HeaderText = "Turno";
                colHora.DataPropertyName = "RangoHorario";
                colHora.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns.Add(colHora);

                DataGridViewTextBoxColumn colEspecialidad = new DataGridViewTextBoxColumn();
                colEspecialidad.Name = "colEspecialidad";
                colEspecialidad.HeaderText = "Especialidad";
                colEspecialidad.DataPropertyName = "Especialidades";
                colEspecialidad.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns.Add(colEspecialidad);

                DataGridViewTextBoxColumn colMedico = new DataGridViewTextBoxColumn();
                colMedico.Name = "colMedico";
                colMedico.HeaderText = "Medico";
                colMedico.DataPropertyName = "NombreApellidoMedico";
                colMedico.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns.Add(colMedico);

                DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn();
                colEstado.Name = "colEstado";
                colEstado.HeaderText = "Estado";
                colEstado.DataPropertyName = "Estado";
                colEstado.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns.Add(colEstado);

                dataGridView1.DataSource = bll_turno._listaPorPaciente(idPaciente);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }
    }
}
