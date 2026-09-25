using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Vista
{
    // Pantalla simplificada para ver los dias y horarios de un medico y elegir cuales
    // franjas inactivar o reactivar. Se arma por codigo (sin Designer) para no tocar el
    // formulario principal de gestion de medicos.
    public class frmEditarFranjasMedico : Form
    {
        const string TodosLosDias = "Todos los dias";

        BLL_MedicoHorario bll_medicoHorario = new BLL_MedicoHorario();

        ucSelectorMedico ucMedico = new ucSelectorMedico();
        Label lblResumen = new Label();
        ComboBox cboDia = new ComboBox();
        DataGridView grilla = new DataGridView();
        Button btnSeleccionarTodo = new Button();
        Button btnInactivar = new Button();
        Button btnReactivar = new Button();
        Button btnCerrar = new Button();

        List<BE_MedicoHorario> franjas = new List<BE_MedicoHorario>();
        bool cargando = false;

        public frmEditarFranjasMedico(int? idMedicoInicial = null)
        {
            Text = "Editar franjas horarias de un medico";
            ClientSize = new Size(760, 560);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            ucMedico.Etiqueta = "Seleccione un Medico:";
            ucMedico.Location = new Point(12, 10);
            ucMedico.Size = new Size(400, 30);
            ucMedico.MedicoSeleccionado += (s, e) => CargarFranjas();

            Label lblTitResumen = new Label { Text = "Dias y horarios en los que trabaja (franjas activas):", AutoSize = true, Location = new Point(12, 50), Font = new Font(Font, FontStyle.Bold) };
            lblResumen.Location = new Point(12, 72);
            lblResumen.Size = new Size(736, 110);
            lblResumen.BorderStyle = BorderStyle.FixedSingle;
            lblResumen.BackColor = SystemColors.Info;
            lblResumen.Padding = new Padding(6);

            Label lblFiltro = new Label { Text = "Ver dia:", AutoSize = true, Location = new Point(12, 197) };
            cboDia.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDia.Location = new Point(70, 193);
            cboDia.Width = 160;
            cboDia.SelectedIndexChanged += (s, e) => { if (!cargando) MostrarGrilla(); };

            btnSeleccionarTodo.Text = "Seleccionar todo lo visible";
            btnSeleccionarTodo.Location = new Point(245, 191);
            btnSeleccionarTodo.Size = new Size(190, 28);
            btnSeleccionarTodo.Click += (s, e) => grilla.SelectAll();

            Label lblAyuda = new Label { Text = "Seleccione una o varias franjas (Ctrl / Shift) y elija la accion.", AutoSize = true, Location = new Point(12, 228), ForeColor = SystemColors.GrayText };

            grilla.Location = new Point(12, 250);
            grilla.Size = new Size(736, 250);
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.ReadOnly = true;
            grilla.MultiSelect = true;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.RowHeadersVisible = false;
            grilla.BackgroundColor = SystemColors.Window;
            grilla.Columns.Add("colDia", "Dia");
            grilla.Columns.Add("colEspecialidad", "Especialidad");
            grilla.Columns.Add("colHorario", "Horario");
            grilla.Columns.Add("colEstado", "Estado");
            grilla.Columns.Add("colTurnos", "Turnos reservados");
            foreach (DataGridViewColumn col in grilla.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            btnInactivar.Text = "Inactivar seleccionadas";
            btnInactivar.Location = new Point(12, 512);
            btnInactivar.Size = new Size(190, 34);
            btnInactivar.Click += (s, e) => CambiarEstadoSeleccion(false);

            btnReactivar.Text = "Reactivar seleccionadas";
            btnReactivar.Location = new Point(212, 512);
            btnReactivar.Size = new Size(190, 34);
            btnReactivar.Click += (s, e) => CambiarEstadoSeleccion(true);

            btnCerrar.Text = "Cerrar";
            btnCerrar.Location = new Point(638, 512);
            btnCerrar.Size = new Size(110, 34);
            btnCerrar.DialogResult = DialogResult.Cancel;

            CancelButton = btnCerrar;

            Controls.AddRange(new Control[] { ucMedico, lblTitResumen, lblResumen, lblFiltro, cboDia, btnSeleccionarTodo, lblAyuda, grilla, btnInactivar, btnReactivar, btnCerrar });

            Load += (s, e) =>
            {
                ucMedico.CargarMedicos();
                if (idMedicoInicial != null)
                    ucMedico.SeleccionarMedico(idMedicoInicial.Value);
                CargarFranjas();
            };
        }

        private void CargarFranjas()
        {
            try
            {
                cargando = true;

                franjas = ucMedico.IdMedicoSeleccionado == null
                    ? new List<BE_MedicoHorario>()
                    : bll_medicoHorario.ListarPorMedico(ucMedico.IdMedicoSeleccionado.Value);

                string diaElegido = cboDia.SelectedItem as string;
                cboDia.Items.Clear();
                cboDia.Items.Add(TodosLosDias);
                foreach (int dia in franjas.Select(f => f.DiaSemana).Distinct().OrderBy(d => d))
                    cboDia.Items.Add(BE_MedicoHorario.NombreDia(dia));
                cboDia.SelectedItem = diaElegido != null && cboDia.Items.Contains(diaElegido) ? diaElegido : TodosLosDias;

                lblResumen.Text = ArmarResumen();
                cargando = false;

                MostrarGrilla();
            }
            catch (Exception ex)
            {
                cargando = false;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarGrilla()
        {
            try
            {
                grilla.Rows.Clear();
                string filtro = cboDia.SelectedItem as string;

                foreach (BE_MedicoHorario f in franjas)
                {
                    if (filtro != null && filtro != TodosLosDias && f.DiaSemanaTexto != filtro)
                        continue;

                    int turnos = bll_medicoHorario.ObtenerTurnosAfectadosPorInactivacion(f.IdHorario).Count;
                    int i = grilla.Rows.Add(f.DiaSemanaTexto, f.Especialidades, f.RangoHorario, f.Activo ? "Activa" : "Inactiva", turnos);
                    grilla.Rows[i].Tag = f;

                    if (!f.Activo)
                        grilla.Rows[i].DefaultCellStyle.ForeColor = SystemColors.GrayText;
                }

                grilla.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Une las franjas activas contiguas de cada dia en bloques: "Lunes: 08:00 a 12:00 y 13:00 a 17:00".
        private string ArmarResumen()
        {
            if (franjas.Count == 0)
                return "El medico seleccionado no tiene horarios asignados.";

            List<string> lineas = new List<string>();
            int sinAtencion = 0;

            foreach (var grupo in franjas.GroupBy(f => new { f.DiaSemana, f.IdEspecialidad }).OrderBy(g => g.Key.DiaSemana).ThenBy(g => g.Min(f => f.HoraInicio)))
            {
                List<BE_MedicoHorario> activas = grupo.Where(f => f.Activo).OrderBy(f => f.HoraInicio).ToList();
                int inactivas = grupo.Count() - activas.Count;
                string esp = grupo.First().NombreEspecialidad;
                string nombre = BE_MedicoHorario.NombreDia(grupo.Key.DiaSemana) + (esp != null ? " (" + esp + ")" : string.Empty);

                // Los grupos sin ninguna franja activa no se resumen (se ven en gris en la grilla).
                if (activas.Count == 0)
                {
                    sinAtencion += inactivas;
                    continue;
                }

                int minutos = (int)(activas[0].HoraFin - activas[0].HoraInicio).TotalMinutes;
                string linea = $"{nombre}: {BE_MedicoHorario.ResumirBloques(activas)} (turnos cada {minutos} min)";
                if (inactivas > 0)
                    linea += $" - {inactivas} franja(s) inactiva(s)";
                lineas.Add(linea);
            }

            if (sinAtencion > 0)
                lineas.Add($"({sinAtencion} franja(s) inactiva(s) sin otra atencion: se ven en gris en la grilla)");

            return string.Join(Environment.NewLine, lineas);
        }

        private void CambiarEstadoSeleccion(bool activar)
        {
            try
            {
                List<BE_MedicoHorario> elegidas = grilla.SelectedRows.Cast<DataGridViewRow>()
                    .Select(r => (BE_MedicoHorario)r.Tag)
                    .Where(f => f.Activo != activar)
                    .ToList();

                if (grilla.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione al menos una franja de la grilla.");
                    return;
                }
                if (elegidas.Count == 0)
                {
                    MessageBox.Show(activar ? "Las franjas seleccionadas ya estan activas." : "Las franjas seleccionadas ya estan inactivas.");
                    return;
                }

                List<int> ids = elegidas.Select(f => f.IdHorario).ToList();

                if (!activar)
                {
                    List<BE_Turno> turnos = bll_medicoHorario.ObtenerTurnosAfectadosPorInactivacion(ids);
                    if (turnos.Count > 0 && !ConfirmarCancelacionTurnos(turnos))
                        return;
                }

                bll_medicoHorario.CambiarActivo(ids, activar, Servicios.Sesion.Instancia().UsuarioActual.IdUsuario);

                MessageBox.Show(activar ? "Franjas reactivadas correctamente." : "Franjas inactivadas correctamente.");
                CargarFranjas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ConfirmarCancelacionTurnos(List<BE_Turno> turnos)
        {
            System.Text.StringBuilder mensaje = new System.Text.StringBuilder();
            mensaje.AppendLine("Se van a cancelar los siguientes turnos:");
            mensaje.AppendLine();

            foreach (BE_Turno turno in turnos)
                mensaje.AppendLine($"- {turno.NombreApellidoPaciente}: {turno.FechaTexto} {turno.RangoHorario}");

            mensaje.AppendLine();
            mensaje.Append("¿Confirma?");

            return MessageBox.Show(mensaje.ToString(), "Turnos asignados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmEditarFranjasMedico
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "frmEditarFranjasMedico";
            this.Load += new System.EventHandler(this.frmEditarFranjasMedico_Load);
            this.ResumeLayout(false);

        }

        private void frmEditarFranjasMedico_Load(object sender, EventArgs e)
        {

        }
    }
}
