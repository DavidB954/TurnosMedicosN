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
    // "Mi Agenda" del Medico (Proceso 2). En una misma pantalla (RNF-05):
    //  - CU-02.2 Agenda del Dia (con Atender / Ausente) y CU-02.3 Agenda General (otra fecha, solo lectura)
    //  - CU-02.6 Marcar Ausente y "Finalizar atencion" del dia
    //  - CU-02.4 Consultar Historia Clinica
    // CU-02.1 Atender Consulta se abre en frmAtencion.
    public partial class frm_AccesoMedico : frmBase
    {
        BLL_Atencion bll_atencion = new BLL_Atencion();

        private List<BE_Turno> agenda = new List<BE_Turno>();
        private DateTime? fechaCargada = null;
        private bool calendarioAbierto = false;
        private bool accesoPermitido = false;

        private bool puedeAtender = true;
        private bool puedeMarcarAusente = true;

        private static readonly Color ColorAtender = Color.FromArgb(37, 99, 235);
        private static readonly Color ColorAtenderDeshabilitado = Color.FromArgb(191, 207, 245);
        private static readonly Color ColorAusente = Color.FromArgb(220, 38, 38);
        private static readonly Color ColorAusenteDeshabilitado = Color.FromArgb(245, 190, 190);

        public frm_AccesoMedico()
        {
            InitializeComponent();
        }

        private int IdMedico { get { return Sesion.Instancia().UsuarioActual.IdUsuario; } }

        private string T(string clave, string porDefecto)
        {
            return TraduccionServicio.Instancia.TraducirODefecto($"{Name}.{clave}", porDefecto);
        }

        // Una patente de grilla se respeta igual que un boton: si esta en el catalogo, hay que tenerla.
        private bool TienePatente(string control)
        {
            string clave = $"{Name}.{control}";
            Sesion sesion = Sesion.Instancia();
            return !sesion.EstaEnCatalogo(clave) || sesion.TienePermiso(clave);
        }

        private void frm_AccesoMedico_Load(object sender, EventArgs e)
        {
            try
            {
                // RNF-01 / P-14: el contenido clinico es exclusivo del rol Medico.
                if (Sesion.Instancia().UsuarioActual == null || !Sesion.Instancia().TieneRol("Medico"))
                {
                    MessageBox.Show(T("Msg.AccesoDenegado", "Acceso denegado: Mi Agenda es exclusiva del rol Médico."),
                        Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BeginInvoke(new Action(Close));
                    return;
                }
                accesoPermitido = true;

                DejarLugarAlMenu(tlpPrincipal);

                puedeAtender = TienePatente("btnAtender");
                puedeMarcarAusente = TienePatente("btnAusente");

                ConfigurarGrillaAgenda();
                ConfigurarGrillaPacientes();

                // La leyenda "sin turnos" se muestra encima de la grilla.
                lblSinTurnos.BringToFront();

                dtpFecha.Value = DateTime.Today;
                CargarAgendaDelDia();
                timerAgenda.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.ErrorCargar", "Error al cargar la agenda"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
            ActualizarTextosDinamicos();
            dgvAgenda.Invalidate();
            if (flpAtenciones.Controls.Count > 0 && pacienteHC != null)
                MostrarHistoriaClinica(pacienteHC, false);
        }

        private void ActualizarTextosDinamicos()
        {
            BE_Usuario usuario = Sesion.Instancia().UsuarioActual;
            Text = T("Titulo", "Mi Agenda");
            lblMedico.Text = usuario != null ? $"{T("Msg.Medico", "Médico")}: {usuario.NombreApellido}" : "";

            bool esHoy = dtpFecha.Value.Date == DateTime.Today;
            lblModo.Text = esHoy ? "" : T("Msg.ModoSoloLectura", "Fecha distinta de hoy: agenda en modo solo lectura.");
            lblAyudaAusente.Visible = esHoy;

            if (lblSinTurnos.Visible)
                lblSinTurnos.Text = TextoSinTurnos();
        }

        // ---------------------------------------------------------------- Grillas

        private void ConfigurarGrillaAgenda()
        {
            dgvAgenda.AutoGenerateColumns = false;
            dgvAgenda.Columns.Clear();
            dgvAgenda.RowTemplate.Height = 40;
            dgvAgenda.ScrollBars = ScrollBars.Vertical;
            dgvAgenda.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F);
            dgvAgenda.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            dgvAgenda.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dgvAgenda.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvAgenda.EnableHeadersVisualStyles = false;
            dgvAgenda.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(226, 232, 240);

            AgregarColumnaTexto(dgvAgenda, "colFecha", "Fecha", "Fecha", 62);
            dgvAgenda.Columns["colFecha"].DefaultCellStyle.Format = "dd/MM";
            AgregarColumnaTexto(dgvAgenda, "colHora", "Hora", "HoraTexto", 55);
            AgregarColumnaTexto(dgvAgenda, "colPaciente", "Paciente", "NombreApellidoPaciente", 150);
            AgregarColumnaTexto(dgvAgenda, "colHistoriaClinica", "Historia Clínica", "HistoriaClinicaTexto", 88);
            AgregarColumnaTexto(dgvAgenda, "colEstado", "Estado", "Estado", 105);
            AgregarColumnaBoton("colAtender", "Opciones", 95);
            AgregarColumnaBoton("colAusente", "Ausente", 90);
            dgvAgenda.Columns["colAusente"].HeaderText = "";

            // Los pesos son los anchos (px) que entran en una pantalla de 1280 px logicos (1920 al 150 %);
            // en pantallas mas grandes todas las columnas crecen en la misma proporcion.
        }

        private void ConfigurarGrillaPacientes()
        {
            dgvPacientes.AutoGenerateColumns = false;
            dgvPacientes.Columns.Clear();
            AgregarColumnaTexto(dgvPacientes, "colPaciente", "Paciente", "NombreApellido", 50);
            AgregarColumnaTexto(dgvPacientes, "colDNI", "DNI", "DNI", 25);
            AgregarColumnaTexto(dgvPacientes, "colHC", "N° HC", "NroHistoriaClinica", 25);
        }

        private void AgregarColumnaTexto(DataGridView grilla, string nombre, string encabezado, string propiedad, float peso)
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

        private void AgregarColumnaBoton(string nombre, string encabezado, float peso)
        {
            DataGridViewButtonColumn columna = new DataGridViewButtonColumn();
            columna.Name = nombre;
            columna.HeaderText = T($"dgvAgenda.{nombre}", encabezado);
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            columna.FillWeight = peso;
            columna.FlatStyle = FlatStyle.Flat;
            dgvAgenda.Columns.Add(columna);
        }

        private BE_Turno TurnoDeFila(int indiceFila)
        {
            if (indiceFila < 0 || indiceFila >= dgvAgenda.Rows.Count)
                return null;
            return dgvAgenda.Rows[indiceFila].DataBoundItem as BE_Turno;
        }

        private bool AtenderHabilitado(BE_Turno turno)
        {
            return puedeAtender && bll_atencion.PuedeAtender(turno);
        }

        private bool AusenteHabilitado(BE_Turno turno)
        {
            return puedeMarcarAusente && bll_atencion.PuedeMarcarAusente(turno);
        }

        // ---------------------------------------------------------------- Agenda (CU-02.2 / CU-02.3)

        // Carga la agenda de la fecha elegida. Con la fecha de hoy habilita Atender / Ausente;
        // con otra fecha la muestra en solo lectura (CU-02.3).
        public void CargarAgendaDelDia()
        {
            CargarAgendaDelDia(true);
        }

        private void CargarAgendaDelDia(bool registrarConsulta)
        {
            if (!accesoPermitido)
                return;

            try
            {
                DateTime fecha = dtpFecha.Value.Date;
                bool esHoy = fecha == DateTime.Today;

                agenda = bll_atencion.ObtenerAgenda(IdMedico, fecha, registrarConsulta);
                fechaCargada = fecha;

                dgvAgenda.DataSource = null;
                dgvAgenda.DataSource = agenda;
                dgvAgenda.ClearSelection();

                dgvAgenda.Columns["colAtender"].Visible = esHoy && puedeAtender;
                dgvAgenda.Columns["colAusente"].Visible = esHoy && puedeMarcarAusente;
                btnFinalizarAtencion.Enabled = esHoy;

                lblSinTurnos.Visible = agenda.Count == 0;
                lblSinTurnos.Text = TextoSinTurnos();

                ActualizarTextosDinamicos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.ErrorCargar", "Error al cargar la agenda"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string TextoSinTurnos()
        {
            return dtpFecha.Value.Date == DateTime.Today
                ? T("Msg.SinTurnosHoy", "No tiene turnos para hoy.")
                : string.Format(T("Msg.SinTurnosFecha", "No tiene turnos para el {0:dd/MM/yyyy}. Puede elegir otra fecha."), dtpFecha.Value.Date);
        }

        private void dtpFecha_DropDown(object sender, EventArgs e)
        {
            calendarioAbierto = true;
        }

        private void dtpFecha_CloseUp(object sender, EventArgs e)
        {
            calendarioAbierto = false;
            CargarSiCambioLaFecha();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            // Mientras el calendario esta abierto no se consulta en cada clic: se espera al CloseUp.
            if (!calendarioAbierto)
                CargarSiCambioLaFecha();
        }

        private void CargarSiCambioLaFecha()
        {
            if (fechaCargada != dtpFecha.Value.Date)
                CargarAgendaDelDia();
        }

        private void btnHoy_Click(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Today;
            CargarSiCambioLaFecha();
        }

        // Cada minuto se repinta la grilla para habilitar «Ausente» cuando llega la hora del turno.
        // Si cambio el dia (la pantalla quedo abierta pasada la medianoche) se vuelve a hoy.
        private void timerAgenda_Tick(object sender, EventArgs e)
        {
            if (fechaCargada.HasValue && fechaCargada.Value.AddDays(1) == DateTime.Today && dtpFecha.Value.Date == fechaCargada.Value)
                dtpFecha.Value = DateTime.Today;
            dgvAgenda.Invalidate();
        }

        private void dgvAgenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BE_Turno turno = TurnoDeFila(e.RowIndex);
            if (turno == null)
                return;

            string columna = dgvAgenda.Columns[e.ColumnIndex].Name;
            if (columna == "colAtender" && AtenderHabilitado(turno))
                btnAtender_Click(turno.IdTurno);
            else if (columna == "colAusente" && AusenteHabilitado(turno))
                btnAusente_Click(turno.IdTurno);
        }

        // Doble clic en un turno: abre su Historia Clinica en el panel de la derecha (CU-02.4).
        private void dgvAgenda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BE_Turno turno = TurnoDeFila(e.RowIndex);
            if (turno == null || turno.Paciente == null || turno.Paciente.NroHistoriaClinica <= 0)
                return;

            string columna = dgvAgenda.Columns[e.ColumnIndex].Name;
            if (columna == "colAtender" || columna == "colAusente")
                return;

            txtBuscarHC.Text = turno.Paciente.NroHistoriaClinica.ToString();
            btnBuscarHC_Click(btnBuscarHC, EventArgs.Empty);
        }

        private void dgvAgenda_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            BE_Turno turno = TurnoDeFila(e.RowIndex);
            bool sobreBoton = false;
            if (turno != null && e.ColumnIndex >= 0)
            {
                string columna = dgvAgenda.Columns[e.ColumnIndex].Name;
                sobreBoton = (columna == "colAtender" && AtenderHabilitado(turno))
                          || (columna == "colAusente" && AusenteHabilitado(turno));
            }
            dgvAgenda.Cursor = sobreBoton ? Cursors.Hand : Cursors.Default;
        }

        // Dibuja los botones Atender / Ausente (habilitados o deshabilitados) y el estado como etiqueta de color.
        private void dgvAgenda_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            BE_Turno turno = TurnoDeFila(e.RowIndex);
            if (turno == null || e.ColumnIndex < 0)
                return;

            string columna = dgvAgenda.Columns[e.ColumnIndex].Name;
            if (columna != "colAtender" && columna != "colAusente" && columna != "colEstado")
                return;

            e.PaintBackground(e.CellBounds, false);
            Rectangle zona = Rectangle.Inflate(e.CellBounds, -6, -6);

            if (columna == "colEstado")
            {
                Color fondo, texto;
                ColoresEstado(turno.Estado, out fondo, out texto);
                string estado = T($"Estado.{turno.Estado}", turno.Estado);
                using (SolidBrush pincel = new SolidBrush(fondo))
                    e.Graphics.FillRectangle(pincel, zona);
                using (Font fuente = new Font(dgvAgenda.DefaultCellStyle.Font, FontStyle.Bold))
                    TextRenderer.DrawText(e.Graphics, estado, fuente, zona, texto,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else if (turno.Estado != "Confirmado")
            {
                // Atendidos y Ausentes se muestran sin opciones (RN-3 CU-02.2)
                TextRenderer.DrawText(e.Graphics, "—", dgvAgenda.DefaultCellStyle.Font, zona, Color.Gray,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else
            {
                bool esAtender = columna == "colAtender";
                bool habilitado = esAtender ? AtenderHabilitado(turno) : AusenteHabilitado(turno);
                Color fondo = esAtender
                    ? (habilitado ? ColorAtender : ColorAtenderDeshabilitado)
                    : (habilitado ? ColorAusente : ColorAusenteDeshabilitado);
                string texto = esAtender ? T("Msg.Atender", "Atender") : T("Msg.Ausente", "Ausente");

                using (SolidBrush pincel = new SolidBrush(fondo))
                    e.Graphics.FillRectangle(pincel, zona);
                using (Font fuente = new Font(dgvAgenda.DefaultCellStyle.Font, FontStyle.Bold))
                    TextRenderer.DrawText(e.Graphics, texto, fuente, zona, Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            e.Handled = true;
        }

        private static void ColoresEstado(string estado, out Color fondo, out Color texto)
        {
            switch (estado)
            {
                case "Atendido":
                    fondo = Color.FromArgb(220, 252, 231); texto = Color.FromArgb(21, 128, 61); break;
                case "Ausente":
                    fondo = Color.FromArgb(254, 226, 226); texto = Color.FromArgb(185, 28, 28); break;
                case "Confirmado":
                    fondo = Color.FromArgb(219, 234, 254); texto = Color.FromArgb(29, 78, 216); break;
                default:
                    fondo = Color.FromArgb(241, 245, 249); texto = Color.FromArgb(71, 85, 105); break;
            }
        }

        // ---------------------------------------------------------------- Atender (CU-02.1)

        private void btnAtender_Click(int idTurno)
        {
            BE_Turno turno;
            try
            {
                turno = bll_atencion.ObtenerTurnoParaAtender(idTurno, IdMedico);
            }
            catch (ArgumentException ex)
            {
                // A1 - Turno no atendible: no se abre el formulario
                MessageBox.Show(ex.Message, T("Msg.TurnoNoAtendible", "El turno no puede atenderse"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarAgendaDelDia(false);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (frmAtencion frm = new frmAtencion(turno))
                {
                    frm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Se refresca siempre: si se registro, el turno pasa a Atendido.
            CargarAgendaDelDia(false);
        }

        // ---------------------------------------------------------------- Ausente (CU-02.6)

        private void btnAusente_Click(int idTurno)
        {
            BE_Turno turno = agenda.FirstOrDefault(t => t.IdTurno == idTurno);
            string paciente = turno != null ? turno.NombreApellidoPaciente : idTurno.ToString();

            if (MessageBox.Show(string.Format(T("Msg.ConfirmarAusente", "¿Confirma que {0} no se presentó?"), paciente),
                    T("Msg.MarcarAusente", "Marcar ausente"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                bll_atencion.MarcarAusente(idTurno);
            }
            catch (ArgumentException ex)
            {
                // A2 - el turno ya no esta Confirmado
                MessageBox.Show(ex.Message, T("Msg.MarcarAusente", "Marcar ausente"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CargarAgendaDelDia(false);
        }

        // Valida que todos los turnos del dia esten Atendidos o Ausentes; si quedan sin atender,
        // ofrece marcarlos como Ausentes.
        private void btnFinalizarAtencion_Click(object sender, EventArgs e)
        {
            try
            {
                int sinAtender = bll_atencion.ContarTurnosSinAtender(IdMedico);

                if (sinAtender == 0)
                {
                    bll_atencion.RegistrarAtencionFinalizada(IdMedico, 0);
                    MessageBox.Show(T("Msg.FinalizadaOk", "Todos los turnos del día están Atendidos o Ausentes. Atención finalizada."),
                        T("Msg.FinalizarAtencion", "Finalizar atención"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string pregunta = string.Format(
                        T("Msg.PreguntaAusentes", "Hay {0} turno(s) sin atender.\n¿Desea marcar como ausente los turnos no atendidos?"), sinAtender);
                    if (MessageBox.Show(pregunta, T("Msg.FinalizarAtencion", "Finalizar atención"),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;

                    int marcados = bll_atencion.MarcarAusentesPendientes(IdMedico);
                    MessageBox.Show(string.Format(T("Msg.AusentesMarcados", "Se marcaron {0} turno(s) como Ausente. Atención finalizada."), marcados),
                        T("Msg.FinalizarAtencion", "Finalizar atención"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, T("Msg.FinalizarAtencion", "Finalizar atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CargarAgendaDelDia(false);
        }

        // ---------------------------------------------------------------- Historia Clinica (CU-02.4)

        private BE_Paciente pacienteHC = null;

        private void txtBuscarHC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnBuscarHC_Click(btnBuscarHC, EventArgs.Empty);
            }
        }

        private void btnBuscarHC_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarHistoriaClinica();

                List<BE_Paciente> pacientes = bll_atencion.BuscarPacientes(txtBuscarHC.Text);
                dgvPacientes.DataSource = pacientes;
                dgvPacientes.ClearSelection();

                if (pacientes.Count == 0)
                {
                    lblPacienteHC.Text = T("Msg.SinPacientes", "No se encontraron pacientes.");
                    return;
                }

                if (pacientes.Count == 1)
                {
                    dgvPacientes.Rows[0].Selected = true;
                    MostrarHistoriaClinica(pacientes[0], true);
                }
                else
                {
                    lblPacienteHC.Text = T("Msg.ElegirPaciente", "Seleccione un paciente de la lista.");
                }
            }
            catch (ArgumentException ex)
            {
                // A3 - criterio invalido
                MessageBox.Show(ex.Message, T("Msg.BuscarHC", "Consultar Historia Clínica"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPacientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            BE_Paciente paciente = dgvPacientes.Rows[e.RowIndex].DataBoundItem as BE_Paciente;
            if (paciente != null && (pacienteHC == null || pacienteHC.IdUsuario != paciente.IdUsuario))
                MostrarHistoriaClinica(paciente, true);
        }

        private void LimpiarHistoriaClinica()
        {
            pacienteHC = null;
            lblPacienteHC.Text = "";
            LimpiarTarjetas();
        }

        private void LimpiarTarjetas()
        {
            List<Control> tarjetas = flpAtenciones.Controls.Cast<Control>().ToList();
            flpAtenciones.Controls.Clear();
            foreach (Control tarjeta in tarjetas)
                tarjeta.Dispose();
        }

        private List<BE_AtencionMedica> atencionesHC = new List<BE_AtencionMedica>();

        // consultar = false solo redibuja (cambio de idioma) sin volver a consultar ni auditar.
        private void MostrarHistoriaClinica(BE_Paciente paciente, bool consultar)
        {
            try
            {
                if (consultar)
                    atencionesHC = bll_atencion.ObtenerHistoriaClinica(paciente.NroHistoriaClinica);

                pacienteHC = paciente;
                lblPacienteHC.Text = $"{paciente.NombreApellido} · DNI {paciente.DNI} · H.C {paciente.NroHistoriaClinica}";

                flpAtenciones.SuspendLayout();
                LimpiarTarjetas();

                if (atencionesHC.Count == 0)
                {
                    flpAtenciones.Controls.Add(CrearTarjeta(null,
                        T("Msg.HCSinAtenciones", "La Historia Clínica no tiene atenciones registradas.")));
                }
                else
                {
                    foreach (BE_AtencionMedica atencion in atencionesHC)
                        flpAtenciones.Controls.Add(CrearTarjeta(TituloAtencion(atencion), DetalleAtencion(atencion)));
                }

                flpAtenciones.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string TituloAtencion(BE_AtencionMedica a)
        {
            string especialidad = string.IsNullOrEmpty(a.Turno.Especialidades) ? "" : $" ({a.Turno.Especialidades})";
            return $"{a.FechaHora:dd/MM/yyyy HH:mm} – {a.Turno.NombreApellidoMedico}{especialidad}";
        }

        private string DetalleAtencion(BE_AtencionMedica a)
        {
            List<string> lineas = new List<string>();
            lineas.Add($"{T("Msg.Motivo", "Motivo")}: {a.MotivoConsulta}");
            if (!string.IsNullOrWhiteSpace(a.Indicaciones))
                lineas.Add($"{T("Msg.Indicaciones", "Indicaciones")}: {a.Indicaciones}");

            if (a.Receta == null)
            {
                lineas.Add(T("Msg.SinReceta", "Sin receta"));
            }
            else
            {
                lineas.Add($"{T("Msg.Receta", "Receta")} {a.Receta.NroReceta}:");
                foreach (BE_RecetaItem item in a.Receta.Items)
                {
                    string presentacion = string.IsNullOrEmpty(item.Presentacion) ? "" : " " + item.Presentacion;
                    lineas.Add($"   • {item.Medicamento}{presentacion} – {item.Posologia} (x{item.Cantidad})");
                }
            }
            return string.Join(Environment.NewLine, lineas);
        }

        private Panel CrearTarjeta(string titulo, string detalle)
        {
            int ancho = AnchoTarjeta();

            Panel tarjeta = new Panel();
            tarjeta.BackColor = Color.White;
            tarjeta.BorderStyle = BorderStyle.FixedSingle;
            tarjeta.Padding = new Padding(10, 8, 10, 8);
            tarjeta.Margin = new Padding(3, 3, 3, 8);
            tarjeta.Width = ancho;
            tarjeta.AutoSize = true;
            tarjeta.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tarjeta.MinimumSize = new Size(ancho, 0);
            tarjeta.MaximumSize = new Size(ancho, 0);

            Label lblDetalle = new Label();
            lblDetalle.AutoSize = true;
            lblDetalle.MaximumSize = new Size(ancho - 24, 0);
            lblDetalle.Font = new Font("Microsoft Sans Serif", 10F);
            lblDetalle.ForeColor = Color.FromArgb(51, 65, 85);
            lblDetalle.Text = detalle;
            lblDetalle.Dock = DockStyle.Top;
            tarjeta.Controls.Add(lblDetalle);

            if (titulo != null)
            {
                Label lblTituloTarjeta = new Label();
                lblTituloTarjeta.AutoSize = true;
                lblTituloTarjeta.MaximumSize = new Size(ancho - 24, 0);
                lblTituloTarjeta.Font = new Font("Microsoft Sans Serif", 10.5F, FontStyle.Bold);
                lblTituloTarjeta.Padding = new Padding(0, 0, 0, 6);
                lblTituloTarjeta.Text = titulo;
                lblTituloTarjeta.Dock = DockStyle.Top;
                tarjeta.Controls.Add(lblTituloTarjeta);
            }

            return tarjeta;
        }

        private int AnchoTarjeta()
        {
            return Math.Max(200, flpAtenciones.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 14);
        }

        private void flpAtenciones_Resize(object sender, EventArgs e)
        {
            int ancho = AnchoTarjeta();
            foreach (Control tarjeta in flpAtenciones.Controls)
            {
                tarjeta.MinimumSize = new Size(ancho, 0);
                tarjeta.MaximumSize = new Size(ancho, 0);
                tarjeta.Width = ancho;
                foreach (Control etiqueta in tarjeta.Controls)
                    etiqueta.MaximumSize = new Size(ancho - 24, 0);
            }
        }

        private void frm_AccesoMedico_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerAgenda.Stop();
        }
    }
}
