using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Vista
{
    public partial class frm_AccesoPaciente : frmBase
    {
        BLL_MedicoHorario bll_medicoHorario = new BLL_MedicoHorario();
        BLL_Turno bll_turno = new BLL_Turno();

        // Dias (0=Domingo..6=Sabado, igual que DayOfWeek) en que atiende el medico/especialidad
        // elegidos. Vacio = sin medico seleccionado todavia (no se restringe nada).
        private List<int> diasAtencionMedico = new List<int>();
        private bool ajustandoFecha = false;

        public frm_AccesoPaciente()
        {
            InitializeComponent();
            mcFecha.MinDate = DateTime.Today;
            mcFecha.SetDate(DateTime.Today);

            ucEspecialidad.EspecialidadSeleccionada += ucEspecialidad_EspecialidadSeleccionada;
            ucMedico.MedicoSeleccionado += ucMedico_MedicoSeleccionado;
        }

        private void frm_AccesoPaciente_Load(object sender, EventArgs e)
        {
            try
            {
                ucEspecialidad.CargarEspecialidades();
                ActualizarDiasDeAtencion();
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

            // Los horarios ofrecidos y los dias resaltados en el calendario dependen tambien
            // de la especialidad elegida.
            ActualizarDiasDeAtencion();
            CargarHorariosDelMedico();
        }

        private void ucMedico_MedicoSeleccionado(object sender, EventArgs e)
        {
            try
            {
                ActualizarDiasDeAtencion();
                CargarHorariosDelMedico();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mcFecha_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (ajustandoFecha)
                return;

            try
            {
                // Si el dia elegido no es uno de los que el medico atiende, se avisa y se lo
                // lleva al proximo dia valido (ademas de quedar resaltado en negrita en el
                // calendario, esto deja bien claro cuales se pueden elegir).
                if (diasAtencionMedico.Count > 0 && !diasAtencionMedico.Contains((int)mcFecha.SelectionStart.DayOfWeek))
                {
                    DateTime fechaValida = ProximaFechaDeAtencion(mcFecha.SelectionStart.Date);

                    MessageBox.Show(
                        $"El médico no atiende ese día. Atiende los: {TextoDiasAtencion()}.\nSe lo llevó al {fechaValida:dddd dd/MM/yyyy}.",
                        "Día sin atención", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ajustandoFecha = true;
                    try { mcFecha.SetDate(fechaValida); }
                    finally { ajustandoFecha = false; }
                }

                CargarHorariosDelMedico();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string TextoDiasAtencion()
        {
            return string.Join(", ", diasAtencionMedico.OrderBy(d => d).Select(BE_MedicoHorario.NombreDia));
        }

        // Primer dia, desde "desde" en adelante, que el medico atiende.
        private DateTime ProximaFechaDeAtencion(DateTime desde)
        {
            for (int i = 0; i < 14; i++)
            {
                DateTime candidata = desde.AddDays(i);
                if (diasAtencionMedico.Contains((int)candidata.DayOfWeek))
                    return candidata;
            }
            return desde;
        }

        // Resalta en el calendario los dias (de hoy en adelante) en que el medico/especialidad
        // elegidos atienden, y los describe en texto, para distinguir claramente que fechas se
        // pueden elegir antes de intentar buscar horarios.
        private void ActualizarDiasDeAtencion()
        {
            if (ucMedico.IdMedicoSeleccionado == null)
            {
                diasAtencionMedico = new List<int>();
                mcFecha.BoldedDates = new DateTime[0];
                mcFecha.UpdateBoldedDates();
                lblDiasAtencion.Text = "Elija especialidad y médico para ver sus días de atención (resaltados en el calendario).";
                return;
            }

            diasAtencionMedico = bll_medicoHorario.ListarPorMedico(ucMedico.IdMedicoSeleccionado.Value)
                .Where(h => h.Activo
                         && (ucEspecialidad.IdEspecialidadSeleccionada == null
                             || h.IdEspecialidad == null
                             || h.IdEspecialidad == ucEspecialidad.IdEspecialidadSeleccionada))
                .Select(h => h.DiaSemana)
                .Distinct()
                .ToList();

            const int diasAMostrar = 90;
            List<DateTime> fechasResaltadas = new List<DateTime>();
            for (int i = 0; i < diasAMostrar; i++)
            {
                DateTime fecha = DateTime.Today.AddDays(i);
                if (diasAtencionMedico.Contains((int)fecha.DayOfWeek))
                    fechasResaltadas.Add(fecha);
            }

            mcFecha.BoldedDates = fechasResaltadas.ToArray();
            mcFecha.UpdateBoldedDates();

            lblDiasAtencion.Text = diasAtencionMedico.Count == 0
                ? "El médico no tiene horarios configurados para esa especialidad."
                : "Atiende los días en negrita: " + TextoDiasAtencion() + ".";

            // Si al cambiar de medico/especialidad la fecha ya elegida dejo de ser valida, se la
            // reacomoda en silencio (sin cartel: es consecuencia de haber cambiado de medico, no
            // de haber tocado mal el calendario).
            if (diasAtencionMedico.Count > 0 && !diasAtencionMedico.Contains((int)mcFecha.SelectionStart.DayOfWeek))
            {
                ajustandoFecha = true;
                try { mcFecha.SetDate(ProximaFechaDeAtencion(mcFecha.SelectionStart.Date)); }
                finally { ajustandoFecha = false; }
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

            lstHorarios.DataSource = bll_medicoHorario._listaDisponibles(ucMedico.IdMedicoSeleccionado.Value, mcFecha.SelectionStart.Date, ucEspecialidad.IdEspecialidadSeleccionada);
            lstHorarios.DisplayMember = "RangoHorario";

            // Al asignar DataSource, el ListBox autoselecciona el primer horario (comportamiento
            // propio del binding). Sin esto, "Reservar"/"Modificar" tomaban ese horario aunque el
            // paciente no hubiera elegido ninguno a proposito.
            lstHorarios.ClearSelected();
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

                bll_turno.SolicitarTurno(idPaciente, horario.IdHorario, mcFecha.SelectionStart.Date);

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

                bll_turno.ModificarTurno(idTurno, horario.IdHorario, mcFecha.SelectionStart.Date, idPaciente);

                MessageBox.Show("Turno modificado: el turno anterior quedó Cancelado y se reservó el nuevo horario elegido.");
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

        // Al elegir un turno en "Mis Turnos" se precargan arriba su especialidad y medico, para
        // no tener que volver a buscarlos a mano antes de elegir el nuevo horario y Modificar.
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                    return;

                BE_Turno turno = dataGridView1.CurrentRow.DataBoundItem as BE_Turno;
                if (turno == null)
                    return;

                if (turno.IdEspecialidad.HasValue)
                {
                    // Dispara CargarMedicosPorEspecialidad() (filtra ucMedico por esa especialidad).
                    ucEspecialidad.SeleccionarEspecialidad(turno.IdEspecialidad.Value);
                }
                else
                {
                    // La franja del turno no tenia una especialidad puntual asignada: se muestra
                    // el medico igual, sin filtro de especialidad.
                    ucEspecialidad.Limpiar();
                    ucMedico.CargarMedicos();
                }

                ucMedico.SeleccionarMedico(turno.IdMedico);
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
                colIdTurno.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(colIdTurno);

                DataGridViewTextBoxColumn colFecha = new DataGridViewTextBoxColumn();
                colFecha.Name = "colFecha";
                colFecha.HeaderText = "Fecha";
                colFecha.DataPropertyName = "FechaTexto";
                colFecha.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                // NotSortable: "FechaTexto" es "dd/MM/yyyy", un texto. Ordenarlo por columna lo
                // haria alfabeticamente (ej. "01/12" antes que "09/11"), no cronologicamente. El
                // orden real ya viene correcto desde la base (DAL_Turno.ListarPorPaciente).
                colFecha.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(colFecha);

                DataGridViewTextBoxColumn colHora = new DataGridViewTextBoxColumn();
                colHora.Name = "colHora";
                colHora.HeaderText = "Turno";
                colHora.DataPropertyName = "RangoHorario";
                colHora.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colHora.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(colHora);

                DataGridViewTextBoxColumn colEspecialidad = new DataGridViewTextBoxColumn();
                colEspecialidad.Name = "colEspecialidad";
                colEspecialidad.HeaderText = "Especialidad";
                colEspecialidad.DataPropertyName = "Especialidades";
                colEspecialidad.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colEspecialidad.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(colEspecialidad);

                DataGridViewTextBoxColumn colMedico = new DataGridViewTextBoxColumn();
                colMedico.Name = "colMedico";
                colMedico.HeaderText = "Medico";
                colMedico.DataPropertyName = "NombreApellidoMedico";
                colMedico.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colMedico.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(colMedico);

                DataGridViewTextBoxColumn colEstado = new DataGridViewTextBoxColumn();
                colEstado.Name = "colEstado";
                colEstado.HeaderText = "Estado";
                colEstado.DataPropertyName = "Estado";
                colEstado.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colEstado.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(colEstado);

                // Solo Confirmados: Cancelar/Modificar requieren ese estado (BLL_Turno ya lo exige),
                // y Atendido/Ausente/Cancelado son historial, no algo sobre lo que el paciente pueda
                // actuar aqui. Orden cronologico: mas reciente/proximo primero, y por hora.
                dataGridView1.DataSource = bll_turno._listaPorPaciente(idPaciente)
                    .Where(t => t.Estado == "Confirmado")
                    .OrderByDescending(t => t.Fecha)
                    .ThenBy(t => t.HoraInicio)
                    .ToList();
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
