using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmGestionMedicos : frmBase
    {
        BLL_Especialidad bll_especialidad = new BLL_Especialidad();
        BLL_MedicoHorario bll_medicoHorario = new BLL_MedicoHorario();

        bool cargandoGrid = false;

        // Ultima lista cargada en la grilla, para poder calcular Desde/Hasta/Turnos cada
        // de un medico+dia sin volver a consultar la base al seleccionar una fila.
        List<BE_MedicoHorario> ultimaListaHorarios = new List<BE_MedicoHorario>();

        public frmGestionMedicos()
        {
            InitializeComponent();
            dataGridViewMedicosDiasHorarios.SelectionChanged += dataGridViewMedicosDiasHorarios_SelectionChanged;
            ucMedicoHorario.MedicoSeleccionado += ucMedicoHorario_MedicoSeleccionado;
            dataGridViewMedicosDiasHorarios.CellDoubleClick += dataGridViewMedicosDiasHorarios_CellDoubleClick;
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }

        private void btnEspecialidadCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombreEspecialidad.Text))
                {
                    MessageBox.Show("Por favor complete el nombre de la especialidad");
                    return;
                }
                else
                {
                    bll_especialidad.AgregarEspecialidad(txtNombreEspecialidad.Text);
                    txtNombreEspecialidad.Clear();
                    CargarEspecialidades();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmGestionMedicos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarEspecialidades();
                CargarListaMedicos();
                CargarGridHorarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarEspecialidades()
        {
            ucEspecialidadCrud.CargarEspecialidades();
            ucEspecialidadAsignar.CargarEspecialidades();
        }

        public void CargarListaMedicos()
        {
            ucMedicoAsignar.CargarMedicos();
            ucMedicoHorario.CargarMedicos();
        }

        private void btnEspecialidadEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucEspecialidadCrud.IdEspecialidadSeleccionada == null)
                {
                    MessageBox.Show("Por favor seleccione una especialidad.");
                    return;
                }

                bll_especialidad.EliminarEspecialidad(ucEspecialidadCrud.IdEspecialidadSeleccionada.Value);
                CargarEspecialidades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAsignarEspecialidad_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucMedicoAsignar.IdMedicoSeleccionado != null && ucEspecialidadAsignar.IdEspecialidadSeleccionada != null)
                {
                    bll_especialidad.AsignarEspecialidadMedico(ucMedicoAsignar.IdMedicoSeleccionado.Value, ucEspecialidadAsignar.IdEspecialidadSeleccionada.Value);
                    MessageBox.Show("Asignacion de especialidad confirmada", "Asignacion de especialidad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un medico y una especialidad.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitarEspecialidad_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucMedicoAsignar.IdMedicoSeleccionado != null && ucEspecialidadAsignar.IdEspecialidadSeleccionada != null)
                {
                    bll_especialidad.DesasignarEspecialidadMedico(ucMedicoAsignar.IdMedicoSeleccionado.Value, ucEspecialidadAsignar.IdEspecialidadSeleccionada.Value);
                    MessageBox.Show("Especialidad quitada correctamente", "Asignacion de especialidad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un medico y una especialidad.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Solo se ofrecen las especialidades que el medico tiene asignadas.
        private void ucMedicoHorario_MedicoSeleccionado(object sender, EventArgs e)
        {
            if (ucMedicoHorario.IdMedicoSeleccionado == null)
                ucEspecialidadHorario.Limpiar();
            else
                ucEspecialidadHorario.CargarEspecialidadesPorMedico(ucMedicoHorario.IdMedicoSeleccionado.Value);
        }

        private void lblFranjaHoraria_Click(object sender, EventArgs e)
        {

        }

        // Abre la pantalla simplificada para ver los dias/horarios de un medico e inactivar
        // o reactivar franjas puntuales. Al cerrarla se refresca la grilla de esta pantalla.
        private void btnEditarFranjas_Click(object sender, EventArgs e)
        {
            using (frmEditarFranjasMedico frm = new frmEditarFranjasMedico(ucMedicoHorario.IdMedicoSeleccionado))
            {
                frm.ShowDialog(this);
            }

            CargarGridHorarios();
        }

        private void btnAsignarDiaYHorario_Click(object sender, EventArgs e)
        {
            try
            {
                int idMedico;
                List<int> dias;
                TimeSpan desde, hasta;
                int minutos;
                int? idEspecialidad;
                if (!LeerParametrosHorario(out idMedico, out dias, out desde, out hasta, out minutos, out idEspecialidad))
                    return;

                bll_medicoHorario.AsignarDiasYHorarios(idMedico, dias, desde, hasta, minutos, idEspecialidad);

                MessageBox.Show("Horarios asignados correctamente.");
                CargarGridHorarios();
                LimpiarFormularioHorarios();
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

        // Reemplaza el horario del medico en los dias tildados por el que resulta de los
        // valores actuales de Desde/Hasta/Turnos cada.
        private void btnModificarDiasYHorarios_Click(object sender, EventArgs e)
        {
            try
            {
                int idMedico;
                List<int> dias;
                TimeSpan desde, hasta;
                int minutos;
                int? idEspecialidad;
                if (!LeerParametrosHorario(out idMedico, out dias, out desde, out hasta, out minutos, out idEspecialidad))
                    return;

                List<BE_Turno> conflictos = bll_medicoHorario.ObtenerConflictosModificacion(idMedico, dias, idEspecialidad);
                if (conflictos.Count > 0 && !ConfirmarCancelacionTurnos(conflictos))
                    return;

                bll_medicoHorario.ModificarDiasYHorarios(idMedico, dias, desde, hasta, minutos, idEspecialidad, Servicios.Sesion.Instancia().UsuarioActual.IdUsuario);

                MessageBox.Show("Horarios modificados correctamente.");
                CargarGridHorarios();
                LimpiarFormularioHorarios();
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

        private void checkBoxMostrarInactivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarGridHorarios();
        }

        // Doble clic en una fila: abre la pantalla de edicion de franjas de ese medico.
        private void dataGridViewMedicosDiasHorarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            btnEditarFranjas_Click(sender, EventArgs.Empty);
        }

        // Al seleccionar una fila de la grilla, precarga el medico, el dia y la franja horaria
        // (Desde/Hasta/Turnos cada) que ese medico ya tiene ese dia, para que "Modificar" actue
        // sobre lo que se esta mirando y no sobre lo que haya quedado elegido en el combo antes.
        private void dataGridViewMedicosDiasHorarios_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (cargandoGrid || dataGridViewMedicosDiasHorarios.CurrentRow == null)
                    return;

                int idMedico = Convert.ToInt32(dataGridViewMedicosDiasHorarios.CurrentRow.Cells["colIdMedico"].Value);
                int diaSemana = Convert.ToInt32(dataGridViewMedicosDiasHorarios.CurrentRow.Cells["colDiaSemanaValor"].Value);
                object valorEsp = dataGridViewMedicosDiasHorarios.CurrentRow.Cells["colIdEspecialidad"].Value;
                int? idEspecialidad = valorEsp == null || valorEsp == DBNull.Value ? (int?)null : Convert.ToInt32(valorEsp);

                CargarFormularioDesdeSeleccion(idMedico, diaSemana, idEspecialidad);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarFormularioDesdeSeleccion(int idMedico, int diaSemana, int? idEspecialidad)
        {
            List<BE_MedicoHorario> horariosDelDia = ultimaListaHorarios.FindAll(h => h.IdMedico == idMedico && h.DiaSemana == diaSemana && h.IdEspecialidad == idEspecialidad);
            if (horariosDelDia.Count == 0)
                return;

            ucMedicoHorario.SeleccionarMedico(idMedico);
            if (idEspecialidad != null)
                ucEspecialidadHorario.SeleccionarEspecialidad(idEspecialidad.Value);

            LimpiarDiasSeleccionados();
            MarcarDia(diaSemana);

            TimeSpan desde = horariosDelDia[0].HoraInicio;
            TimeSpan hasta = horariosDelDia[0].HoraFin;
            foreach (BE_MedicoHorario h in horariosDelDia)
            {
                if (h.HoraInicio < desde) desde = h.HoraInicio;
                if (h.HoraFin > hasta) hasta = h.HoraFin;
            }

            txtDesde.Text = string.Format("{0:hh\\:mm}", desde);
            txtHasta.Text = string.Format("{0:hh\\:mm}", hasta);
            txtTurnosCada.Text = ((int)(horariosDelDia[0].HoraFin - horariosDelDia[0].HoraInicio).TotalMinutes).ToString();
        }

        private void MarcarDia(int diaSemana)
        {
            switch (diaSemana)
            {
                case 1: checkBoxLunes.Checked = true; break;
                case 2: checkBoxMartes.Checked = true; break;
                case 3: checkBoxMiercoles.Checked = true; break;
                case 4: checkBoxJueves.Checked = true; break;
                case 5: checkBoxViernes.Checked = true; break;
                case 6: checkBoxSabado.Checked = true; break;
            }
        }

        private bool LeerParametrosHorario(out int idMedico, out List<int> dias, out TimeSpan desde, out TimeSpan hasta, out int minutos, out int? idEspecialidad)
        {
            idMedico = 0;
            dias = null;
            desde = TimeSpan.Zero;
            hasta = TimeSpan.Zero;
            minutos = 0;
            idEspecialidad = null;

            if (ucMedicoHorario.IdMedicoSeleccionado == null)
            {
                MessageBox.Show("Por favor seleccione un medico.");
                return false;
            }

            if (ucEspecialidadHorario.IdEspecialidadSeleccionada == null)
            {
                MessageBox.Show("El medico no tiene especialidades asignadas. Asignele una especialidad antes de cargar sus horarios.");
                return false;
            }

            dias = ObtenerDiasSeleccionados();
            if (dias.Count == 0)
            {
                MessageBox.Show("Por favor seleccione al menos un dia trabajado.");
                return false;
            }

            if (!TryParseHora(txtDesde.Text, out desde))
            {
                MessageBox.Show("El horario 'Desde' no es valido. Ingrese una hora entre 0 y 23, por ejemplo 9 o 09:00.");
                return false;
            }

            if (!TryParseHora(txtHasta.Text, out hasta))
            {
                MessageBox.Show("El horario 'Hasta' no es valido. Ingrese una hora entre 0 y 23, por ejemplo 17 o 17:00.");
                return false;
            }

            if (!int.TryParse(txtTurnosCada.Text, out minutos))
            {
                MessageBox.Show("La duracion de cada turno debe ser un numero de minutos, por ejemplo 20.");
                return false;
            }

            idMedico = ucMedicoHorario.IdMedicoSeleccionado.Value;
            idEspecialidad = ucEspecialidadHorario.IdEspecialidadSeleccionada;
            return true;
        }

        private void LimpiarFormularioHorarios()
        {
            txtDesde.Clear();
            txtHasta.Clear();
            txtTurnosCada.Clear();
            LimpiarDiasSeleccionados();
        }

        private void LimpiarDiasSeleccionados()
        {
            checkBoxLunes.Checked = false;
            checkBoxMartes.Checked = false;
            checkBoxMiercoles.Checked = false;
            checkBoxJueves.Checked = false;
            checkBoxViernes.Checked = false;
            checkBoxSabado.Checked = false;
        }

        
        private bool TryParseHora(string texto, out TimeSpan resultado)
        {
            resultado = TimeSpan.Zero;

            if (string.IsNullOrWhiteSpace(texto))
                return false;

            texto = texto.Trim();

            if (!texto.Contains(":"))
            {
                int horas;
                if (!int.TryParse(texto, out horas) || horas < 0 || horas > 23)
                    return false;

                resultado = new TimeSpan(horas, 0, 0);
                return true;
            }

            TimeSpan valor;
            string[] formatos = { "h\\:mm", "hh\\:mm", "h\\:mm\\:ss", "hh\\:mm\\:ss" };
            if (!TimeSpan.TryParseExact(texto, formatos, System.Globalization.CultureInfo.InvariantCulture, out valor))
                return false;

            if (valor.Days != 0 || valor >= TimeSpan.FromDays(1))
                return false;

            resultado = valor;
            return true;
        }

        private List<int> ObtenerDiasSeleccionados()
        {
            List<int> dias = new List<int>();
            if (checkBoxLunes.Checked) dias.Add(1);
            if (checkBoxMartes.Checked) dias.Add(2);
            if (checkBoxMiercoles.Checked) dias.Add(3);
            if (checkBoxJueves.Checked) dias.Add(4);
            if (checkBoxViernes.Checked) dias.Add(5);
            if (checkBoxSabado.Checked) dias.Add(6);
            return dias;
        }

        // Una fila por medico y dia (en vez de una por franja de 20 min), con el horario
        // resumido en bloques y el estado del dia.
        class ResumenDiaMedico
        {
            public int IdMedico { get; set; }
            public int DiaSemana { get; set; }
            public int? IdEspecialidad { get; set; }
            public string Medico { get; set; }
            public string Especialidad { get; set; }
            public string Dia { get; set; }
            public string Horario { get; set; }
            public string TurnosCada { get; set; }
            public string Estado { get; set; }
        }

        private static List<ResumenDiaMedico> ResumirPorMedicoYDia(List<BE_MedicoHorario> horarios)
        {
            List<ResumenDiaMedico> resumen = new List<ResumenDiaMedico>();

            foreach (var grupo in horarios.GroupBy(h => new { h.IdMedico, h.DiaSemana, h.IdEspecialidad }))
            {
                List<BE_MedicoHorario> franjas = grupo.ToList();
                List<BE_MedicoHorario> activas = franjas.Where(h => h.Activo).ToList();
                List<BE_MedicoHorario> mostradas = activas.Count > 0 ? activas : franjas;

                string estado;
                if (activas.Count == franjas.Count) estado = "Activo";
                else if (activas.Count == 0) estado = "Inactivo";
                else estado = $"Parcial ({activas.Count} de {franjas.Count} franjas)";

                resumen.Add(new ResumenDiaMedico
                {
                    IdMedico = grupo.Key.IdMedico,
                    DiaSemana = grupo.Key.DiaSemana,
                    IdEspecialidad = grupo.Key.IdEspecialidad,
                    Medico = franjas[0].NombreApellido,
                    Especialidad = franjas[0].Especialidades,
                    Dia = franjas[0].DiaSemanaTexto,
                    Horario = BE_MedicoHorario.ResumirBloques(mostradas),
                    TurnosCada = ((int)(mostradas[0].HoraFin - mostradas[0].HoraInicio).TotalMinutes) + " min",
                    Estado = estado
                });
            }

            return resumen;
        }

        private void AgregarColumna(string nombre, string titulo, string propiedad, float peso, bool visible = true)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = nombre;
            col.HeaderText = titulo;
            col.DataPropertyName = propiedad;
            col.Visible = visible;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col.FillWeight = peso;
            dataGridViewMedicosDiasHorarios.Columns.Add(col);
        }

        public void CargarGridHorarios()
        {
            try
            {
                cargandoGrid = true;
                dataGridViewMedicosDiasHorarios.AutoGenerateColumns = false;
                dataGridViewMedicosDiasHorarios.DataSource = null;
                dataGridViewMedicosDiasHorarios.Columns.Clear();

                AgregarColumna("colIdMedico", "", "IdMedico", 1, false);
                AgregarColumna("colDiaSemanaValor", "", "DiaSemana", 1, false);
                AgregarColumna("colIdEspecialidad", "", "IdEspecialidad", 1, false);
                AgregarColumna("colMedico", "Medico", "Medico", 22);
                AgregarColumna("colEspecialidad", "Especialidad", "Especialidad", 18);
                AgregarColumna("colDia", "Dia", "Dia", 11);
                AgregarColumna("colHorario", "Horario de atencion", "Horario", 24);
                AgregarColumna("colTurnosCada", "Turnos cada", "TurnosCada", 9);
                AgregarColumna("colEstado", "Estado", "Estado", 16);

                dataGridViewMedicosDiasHorarios.ReadOnly = true;
                dataGridViewMedicosDiasHorarios.AllowUserToAddRows = false;
                dataGridViewMedicosDiasHorarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewMedicosDiasHorarios.MultiSelect = false;
                dataGridViewMedicosDiasHorarios.RowHeadersVisible = false;

                ultimaListaHorarios = bll_medicoHorario._listaParaGrid(checkBoxMostrarInactivos.Checked);
                dataGridViewMedicosDiasHorarios.DataSource = ResumirPorMedicoYDia(ultimaListaHorarios);

                foreach (DataGridViewRow fila in dataGridViewMedicosDiasHorarios.Rows)
                {
                    string estado = Convert.ToString(fila.Cells["colEstado"].Value);
                    if (estado == "Inactivo")
                        fila.DefaultCellStyle.ForeColor = SystemColors.GrayText;
                    else if (estado.StartsWith("Parcial"))
                        fila.Cells["colEstado"].Style.ForeColor = Color.DarkOrange;
                }

                dataGridViewMedicosDiasHorarios.ClearSelection();
                dataGridViewMedicosDiasHorarios.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cargandoGrid = false;
            }
        }
    }
}
