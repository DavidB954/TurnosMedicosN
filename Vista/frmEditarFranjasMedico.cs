using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vista
{
    // CU-01.4 Administrar agenda medica, subflujo Inactivar o reactivar horarios: el recepcionista elige el
    // medico y una fecha (por ejemplo, el lunes 20) y deja de ofrecer algunos horarios solo en esa fecha
    // (por ejemplo, de 10 a 12). Los demas lunes esos horarios siguen activos.
    public partial class frmEditarFranjasMedico : frmBase
    {
        BLL_MedicoHorario bll_medicoHorario = new BLL_MedicoHorario();

        private int? idMedicoInicial;
        private bool cargando = false;

        public frmEditarFranjasMedico(int? idMedicoInicial = null)
        {
            InitializeComponent();
            this.idMedicoInicial = idMedicoInicial;
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }

        private void frmEditarFranjasMedico_Load(object sender, EventArgs e)
        {
            cargando = true;
            try
            {
                ConfigurarGrilla();
                dtpFecha.MinDate = DateTime.Today;
                dtpFecha.Value = DateTime.Today;
                ucMedico.CargarMedicos();
                if (idMedicoInicial != null)
                    ucMedico.SeleccionarMedico(idMedicoInicial.Value);
                ucMedico.MedicoSeleccionado += (s, ev) => CargarHorarios();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
            finally
            {
                cargando = false;
            }
            CargarHorarios();
        }

        private void ConfigurarGrilla()
        {
            dgvHorarios.Columns.Clear();
            AgregarColumna("colHorario", "Horario", 70);
            AgregarColumna("colEspecialidad", "Especialidad", 100);
            AgregarColumna("colEstado", "Estado en la fecha", 90);
            AgregarColumna("colPaciente", "Turno reservado", 120);
        }

        private void AgregarColumna(string nombre, string encabezado, float peso)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = nombre;
            columna.HeaderText = TraduccionServicio.Instancia.TraducirODefecto($"{Name}.{dgvHorarios.Name}.{nombre}", encabezado);
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            columna.FillWeight = peso;
            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvHorarios.Columns.Add(columna);
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!cargando)
                    CargarHorarios();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        // Horarios del medico para el dia de la semana de la fecha elegida, con su estado en esa fecha.
        private void CargarHorarios()
        {
            try
            {
                dgvHorarios.Rows.Clear();
                string dia = dtpFecha.Value.ToString("dddd dd/MM/yyyy", new CultureInfo("es-AR"));
                lblHorarios.Text = "Horarios del " + dia;
                if (ucMedico.IdMedicoSeleccionado == null)
                    return;

                List<BE_MedicoHorario> horarios = bll_medicoHorario.ListarHorariosDelDia(ucMedico.IdMedicoSeleccionado.Value, dtpFecha.Value.Date);
                if (horarios.Count == 0)
                    lblHorarios.Text = "El médico no atiende el " + dia + ".";

                foreach (BE_MedicoHorario h in horarios)
                {
                    int i = dgvHorarios.Rows.Add(h.RangoHorario, h.NombreEspecialidad,
                        h.InactivoEnFecha ? "Inactivo" : "Activo", h.PacienteEnFecha);
                    dgvHorarios.Rows[i].Tag = h;
                    if (h.InactivoEnFecha)
                        dgvHorarios.Rows[i].DefaultCellStyle.ForeColor = SystemColors.GrayText;
                }
                dgvHorarios.ClearSelection();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                CambiarEstado(false);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btnReactivar_Click(object sender, EventArgs e)
        {
            try
            {
                CambiarEstado(true);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void CambiarEstado(bool reactivar)
        {
            try
            {
                if (dgvHorarios.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione uno o más horarios de la grilla (Ctrl o Shift para elegir varios).");
                    return;
                }

                List<int> ids = dgvHorarios.SelectedRows.Cast<DataGridViewRow>()
                    .Select(r => (BE_MedicoHorario)r.Tag)
                    .Where(h => h.InactivoEnFecha == reactivar)
                    .Select(h => h.IdHorario)
                    .ToList();
                if (ids.Count == 0)
                {
                    MessageBox.Show(reactivar ? "Los horarios seleccionados ya están activos en esa fecha."
                                              : "Los horarios seleccionados ya están inactivos en esa fecha.");
                    return;
                }

                DateTime fecha = dtpFecha.Value.Date;
                int idUsuario = Sesion.Instancia().UsuarioActual.IdUsuario;
                if (reactivar)
                {
                    bll_medicoHorario.ReactivarEnFecha(ids, fecha, idUsuario);
                    MessageBox.Show("Horarios reactivados en esa fecha.");
                }
                else
                {
                    // Punto de extension Turnos afectados (flujo alternativo A4 si no confirma).
                    List<BE_Turno> turnos = bll_medicoHorario.ObtenerTurnosAfectados(ids, fecha);
                    if (turnos.Count > 0 && !ConfirmarCancelacionTurnos(turnos))
                        return;

                    bll_medicoHorario.InactivarEnFecha(ids, fecha, idUsuario);
                    MessageBox.Show("Horarios inactivados solo en esa fecha. Los demás días siguen activos.");
                }
                CargarHorarios();
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

        private bool ConfirmarCancelacionTurnos(List<BE_Turno> turnos)
        {
            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine("Se van a cancelar los siguientes turnos:");
            mensaje.AppendLine();
            foreach (BE_Turno turno in turnos)
                mensaje.AppendLine($"- {turno.NombreApellidoPaciente}: {turno.FechaTexto} {turno.RangoHorario}");
            mensaje.AppendLine();
            mensaje.Append("¿Confirma?");

            return MessageBox.Show(mensaje.ToString(), "Turnos afectados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
    }
}
