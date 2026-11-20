using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmGestionTurnos : frmBase
    {
        BLL_Paciente bll_paciente = new BLL_Paciente();
        BLL_MedicoHorario bll_medicoHorario = new BLL_MedicoHorario();
        BLL_Turno bll_turno = new BLL_Turno();

        private BE_Paciente pacienteSeleccionado = null;
        private BE_Turno turnoSeleccionado = null;

        private List<int> diasAtencionMedico = new List<int>();
        private bool ajustandoFecha = false;
        private bool cargandoGrilla = false;

        public frmGestionTurnos()
        {
            InitializeComponent();
            dtpFecha.MinDate = DateTime.Today;
            dtpFecha.Value = DateTime.Today;

            ucEspecialidad.EspecialidadSeleccionada += ucEspecialidad_EspecialidadSeleccionada;
            ucMedico.MedicoSeleccionado += ucMedico_MedicoSeleccionado;
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }

        private void frmGestionTurnos_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla();
                LimpiarPaciente();
                btnVerHistorial.Visible = Sesion.Instancia().TieneAccesoFormulario(nameof(frmHistorialTurnos));
                ucEspecialidad.CargarEspecialidades();
                ActualizarDiasDeAtencion();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void txtDNI_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    btnBuscarPaciente_Click(sender, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (pacienteSeleccionado != null && txtDNI.Text.Trim() != pacienteSeleccionado.DNI)
                    LimpiarPaciente();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                BE_Paciente paciente = bll_paciente.BuscarPorDni(txtDNI.Text);

                if (paciente == null)
                {
                    LimpiarPaciente();
                    HabilitarCargaPaciente(true);
                    txtNombre.Focus();
                    MessageBox.Show("No hay un paciente registrado con ese DNI. Complete sus datos y presione \"Registrar paciente\".",
                        "Paciente no registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MostrarPaciente(paciente);
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

        private void btnRegistrarPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                BE_Paciente paciente = new BE_Paciente
                {
                    DNI = txtDNI.Text,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    FechaNacimiento = dtpFechaNacimiento.Checked ? dtpFechaNacimiento.Value.Date : (DateTime?)null,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text
                };

                bll_paciente.RegistrarPaciente(paciente);

                MostrarPaciente(paciente);
                MessageBox.Show($"Paciente registrado. Se abrió la Historia Clínica N° {paciente.NroHistoriaClinica}.",
                    "Paciente registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void MostrarPaciente(BE_Paciente paciente)
        {
            pacienteSeleccionado = paciente;
            txtDNI.Text = paciente.DNI;
            txtHC.Text = paciente.NroHistoriaClinica.ToString();
            txtNombre.Text = paciente.Nombre;
            txtApellido.Text = paciente.Apellido;
            dtpFechaNacimiento.Checked = paciente.FechaNacimiento.HasValue;
            if (paciente.FechaNacimiento.HasValue)
                dtpFechaNacimiento.Value = paciente.FechaNacimiento.Value;
            txtTelefono.Text = paciente.Telefono;
            txtEmail.Text = paciente.Email;

            HabilitarCargaPaciente(false);
            CargarTurnosDelPaciente();
        }

        private void LimpiarPaciente()
        {
            pacienteSeleccionado = null;
            turnoSeleccionado = null;
            txtHC.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            dtpFechaNacimiento.Value = DateTime.Today;
            dtpFechaNacimiento.Checked = false;
            txtTelefono.Clear();
            txtEmail.Clear();
            HabilitarCargaPaciente(false);

            cargandoGrilla = true;
            try { dgvTurnosPaciente.DataSource = null; }
            finally { cargandoGrilla = false; }
        }

        private void HabilitarCargaPaciente(bool habilitar)
        {
            txtNombre.ReadOnly = !habilitar;
            txtApellido.ReadOnly = !habilitar;
            dtpFechaNacimiento.Enabled = habilitar;
            txtTelefono.ReadOnly = !habilitar;
            txtEmail.ReadOnly = !habilitar;
            btnRegistrarPaciente.Enabled = habilitar;
        }

        private void ucEspecialidad_EspecialidadSeleccionada(object sender, EventArgs e)
        {
            try
            {
                if (ucEspecialidad.IdEspecialidadSeleccionada == null)
                    ucMedico.Limpiar();
                else
                    ucMedico.CargarMedicosPorEspecialidad(ucEspecialidad.IdEspecialidadSeleccionada.Value);

                ActualizarDiasDeAtencion();
                CargarHorariosDelMedico();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
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
                Errores.Mostrar(ex);
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            if (ajustandoFecha)
                return;

            try
            {
                if (diasAtencionMedico.Count > 0 && !diasAtencionMedico.Contains((int)dtpFecha.Value.DayOfWeek))
                {
                    DateTime fechaValida = ProximaFechaDeAtencion(dtpFecha.Value.Date);

                    MessageBox.Show(
                        $"El médico no atiende ese día. Atiende los: {TextoDiasAtencion()}.\nSe seleccionó el {fechaValida:dddd dd/MM/yyyy}.",
                        "Día sin atención", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ajustandoFecha = true;
                    try { dtpFecha.Value = fechaValida; }
                    finally { ajustandoFecha = false; }
                }

                CargarHorariosDelMedico();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private string TextoDiasAtencion()
        {
            return string.Join(", ", diasAtencionMedico.OrderBy(d => d).Select(BE_MedicoHorario.NombreDia));
        }

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

        private void ActualizarDiasDeAtencion()
        {
            if (ucMedico.IdMedicoSeleccionado == null || ucEspecialidad.IdEspecialidadSeleccionada == null)
            {
                diasAtencionMedico = new List<int>();
                return;
            }

            int idEspecialidad = ucEspecialidad.IdEspecialidadSeleccionada.Value;
            diasAtencionMedico = bll_medicoHorario.ListarPorMedico(ucMedico.IdMedicoSeleccionado.Value)
                .Where(h => h.Activo && h.IdEspecialidad == idEspecialidad)
                .Select(h => h.DiaSemana)
                .Distinct()
                .ToList();

            if (diasAtencionMedico.Count > 0 && !diasAtencionMedico.Contains((int)dtpFecha.Value.DayOfWeek))
            {
                ajustandoFecha = true;
                try { dtpFecha.Value = ProximaFechaDeAtencion(dtpFecha.Value.Date); }
                finally { ajustandoFecha = false; }
            }
        }

        private void CargarHorariosDelMedico()
        {
            if (ucMedico.IdMedicoSeleccionado == null || ucEspecialidad.IdEspecialidadSeleccionada == null)
            {
                lstHorarios.DataSource = null;
                return;
            }

            lstHorarios.DataSource = bll_medicoHorario.ConsultarDisponibilidad(ucMedico.IdMedicoSeleccionado.Value,
                dtpFecha.Value.Date, ucEspecialidad.IdEspecialidadSeleccionada);
            lstHorarios.DisplayMember = "RangoHorario";

            lstHorarios.ClearSelected();
        }

        private void btnRegistrarTurno_Click(object sender, EventArgs e)
        {
            try
            {
                if (pacienteSeleccionado == null)
                {
                    MessageBox.Show("Busque o registre al paciente antes de registrar el turno.");
                    return;
                }
                if (lstHorarios.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un horario disponible.");
                    return;
                }

                BE_MedicoHorario horario = (BE_MedicoHorario)lstHorarios.SelectedItem;
                bll_turno.RegistrarTurno(pacienteSeleccionado.IdPaciente, horario.IdHorario, dtpFecha.Value.Date);

                MessageBox.Show($"Turno registrado para {pacienteSeleccionado.NombreApellido}: " +
                    $"{dtpFecha.Value:dd/MM/yyyy} {horario.RangoHorario}.");
                CargarHorariosDelMedico();
                CargarTurnosDelPaciente();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                CargarHorariosDelMedico();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void ConfigurarGrilla()
        {
            dgvTurnosPaciente.AutoGenerateColumns = false;
            dgvTurnosPaciente.Columns.Clear();

            AgregarColumna("colFecha", "Fecha", "FechaTexto");
            AgregarColumna("colHora", "Hora", "HoraTexto");
            AgregarColumna("colMedico", "Médico", "NombreApellidoMedico");
            AgregarColumna("colEspecialidad", "Especialidad", "Especialidades");
            AgregarColumna("colEstado", "Estado", "Estado");
        }

        private void AgregarColumna(string nombre, string encabezado, string propiedad)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = nombre;
            columna.HeaderText = TraduccionServicio.Instancia.TraducirODefecto($"{Name}.dgvTurnosPaciente.{nombre}", encabezado);
            columna.DataPropertyName = propiedad;
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvTurnosPaciente.Columns.Add(columna);
        }

        private void CargarTurnosDelPaciente()
        {
            turnoSeleccionado = null;
            cargandoGrilla = true;
            try
            {
                dgvTurnosPaciente.DataSource = null;
                if (pacienteSeleccionado == null)
                    return;

                dgvTurnosPaciente.DataSource = bll_turno.ListarPorPaciente(pacienteSeleccionado.IdPaciente)
                    .OrderBy(t => t.Estado == "Confirmado" ? 0 : 1)
                    .ThenByDescending(t => t.Fecha)
                    .ThenBy(t => t.HoraInicio)
                    .ToList();
                dgvTurnosPaciente.ClearSelection();
                dgvTurnosPaciente.CurrentCell = null;
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

        private void dgvTurnosPaciente_SelectionChanged(object sender, EventArgs e)
        {
            if (cargandoGrilla)
                return;

            try
            {
                turnoSeleccionado = dgvTurnosPaciente.CurrentRow?.DataBoundItem as BE_Turno;
                if (turnoSeleccionado == null || turnoSeleccionado.Estado != "Confirmado")
                    return;

                if (turnoSeleccionado.IdEspecialidad.HasValue)
                    ucEspecialidad.SeleccionarEspecialidad(turnoSeleccionado.IdEspecialidad.Value);
                ucMedico.SeleccionarMedico(turnoSeleccionado.IdMedico);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btnModificarTurno_Click(object sender, EventArgs e)
        {
            try
            {
                if (pacienteSeleccionado == null || turnoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione, en \"Turnos del paciente\", el turno que quiere modificar.");
                    return;
                }
                if (lstHorarios.SelectedItem == null)
                {
                    MessageBox.Show("Elija en \"Nuevo turno\" el nuevo horario disponible antes de modificar.");
                    return;
                }

                BE_MedicoHorario horario = (BE_MedicoHorario)lstHorarios.SelectedItem;
                DialogResult confirmacion = MessageBox.Show(
                    $"¿Modificar el turno del {turnoSeleccionado.FechaTexto} {turnoSeleccionado.RangoHorario} " +
                    $"por el {dtpFecha.Value:dd/MM/yyyy} {horario.RangoHorario}?",
                    "Confirmar modificación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                    return;

                bll_turno.ModificarTurno(turnoSeleccionado.IdTurno, horario.IdHorario, dtpFecha.Value.Date,
                    pacienteSeleccionado.IdPaciente);

                MessageBox.Show("Turno modificado: el turno anterior quedó Cancelado y se registró el nuevo horario.");
                CargarHorariosDelMedico();
                CargarTurnosDelPaciente();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                CargarHorariosDelMedico();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btnVerHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                if (turnoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione, en \"Turnos del paciente\", el turno cuyo historial quiere ver.");
                    return;
                }

                frmPrincipal principal = MdiParent as frmPrincipal;
                if (principal == null)
                    return;
                principal.AbrirFormulario<frmHistorialTurnos>();
                principal.MdiChildren.OfType<frmHistorialTurnos>().First().MostrarTurno(turnoSeleccionado.IdTurno);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btnCancelarTurno_Click(object sender, EventArgs e)
        {
            try
            {
                if (pacienteSeleccionado == null || turnoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione, en \"Turnos del paciente\", el turno que quiere cancelar.");
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Cancelar el turno del {turnoSeleccionado.FechaTexto} {turnoSeleccionado.RangoHorario} " +
                    $"con {turnoSeleccionado.NombreApellidoMedico}?",
                    "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                    return;

                bll_turno.CancelarTurno(turnoSeleccionado.IdTurno, Sesion.Instancia().UsuarioActual.IdUsuario);

                MessageBox.Show("Turno cancelado. El horario quedó disponible.");
                CargarHorariosDelMedico();
                CargarTurnosDelPaciente();
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
    }
}
