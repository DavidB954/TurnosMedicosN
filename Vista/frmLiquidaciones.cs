using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Vista
{
    // RF2 - Liquidacion de honorarios (contador): CU-02.2 Generar liquidacion de honorarios
    // (incluye la consulta de turnos trabajados) y CU-02.3 Emitir recibo de liquidacion.
    public partial class frmLiquidaciones : frmBase
    {
        BLL_Liquidacion bll_liquidacion = new BLL_Liquidacion();

        private BE_Liquidacion liquidacionSeleccionada = null;
        private BE_MedicoPeriodo medicoSeleccionado = null;
        private bool cargando = false;

        public frmLiquidaciones()
        {
            InitializeComponent();
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }

        private int Mes { get { return cboMes.SelectedIndex + 1; } }

        private int Anio { get { return Convert.ToInt32(cboAnio.SelectedItem); } }

        // CU-02.2 paso 2: la pantalla abre con el mes anterior seleccionado.
        private void frmLiquidaciones_Load(object sender, EventArgs e)
        {
            cargando = true;
            try
            {
                ConfigurarGrillas();

                DateTimeFormatInfo formato = new CultureInfo("es-AR").DateTimeFormat;
                for (int m = 1; m <= 12; m++)
                {
                    string nombre = formato.GetMonthName(m);
                    cboMes.Items.Add(char.ToUpper(nombre[0]) + nombre.Substring(1));
                }
                for (int a = DateTime.Today.Year - 5; a <= DateTime.Today.Year; a++)
                    cboAnio.Items.Add(a);

                DateTime mesAnterior = DateTime.Today.AddMonths(-1);
                cboMes.SelectedIndex = mesAnterior.Month - 1;
                cboAnio.SelectedItem = mesAnterior.Year;
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
            finally
            {
                cargando = false;
            }
            CargarLiquidaciones();
        }

        private void ConfigurarGrillas()
        {
            dgvMedicos.AutoGenerateColumns = false;
            dgvMedicos.Columns.Clear();
            AgregarColumna(dgvMedicos, "colMedico", "Médico", "NombreMedico", false, 140);
            AgregarColumna(dgvMedicos, "colMatricula", "Matrícula", "Matricula", false, 75);
            AgregarColumna(dgvMedicos, "colAtendidos", "Atendidos", "TurnosAtendidos", true, 64);
            AgregarColumna(dgvMedicos, "colAusentes", "Ausentes", "TurnosAusentes", true, 60);
            AgregarColumna(dgvMedicos, "colCancelados", "Cancelados", "TurnosCancelados", true, 72);
            AgregarColumna(dgvMedicos, "colImporteEstimado", "Importe estimado", "ImporteEstimadoTexto", true, 92);
            AgregarColumna(dgvMedicos, "colEstado", "Estado", "EstadoTexto", false, 70);

            dgvTurnosTrabajados.AutoGenerateColumns = false;
            dgvTurnosTrabajados.Columns.Clear();
            AgregarColumna(dgvTurnosTrabajados, "colEspecialidad", "Especialidad", "NombreEspecialidad", false, 110);
            AgregarColumna(dgvTurnosTrabajados, "colDias", "Días", "DiasTrabajados", true, 40);
            AgregarColumna(dgvTurnosTrabajados, "colAtendidos", "Atendidos", "TurnosAtendidos", true, 64);
            AgregarColumna(dgvTurnosTrabajados, "colAusentes", "Ausentes", "TurnosAusentes", true, 60);
            AgregarColumna(dgvTurnosTrabajados, "colCancelados", "Cancelados", "TurnosCancelados", true, 72);
            AgregarColumna(dgvTurnosTrabajados, "colValorTurno", "Valor turno", "ValorTurnoTexto", true, 78);
            AgregarColumna(dgvTurnosTrabajados, "colPorcentajeAusente", "% ausencia", "PorcentajeAusenteTexto", true, 62);
            AgregarColumna(dgvTurnosTrabajados, "colImporteEstimado", "Importe estimado", "ImporteEstimadoTexto", true, 92);

            dgvLiquidaciones.AutoGenerateColumns = false;
            dgvLiquidaciones.Columns.Clear();
            AgregarColumna(dgvLiquidaciones, "colNro", "Nro.", "NroLiquidacion", false, 45);
            AgregarColumna(dgvLiquidaciones, "colMedico", "Médico", "NombreMedico", false, 140);
            AgregarColumna(dgvLiquidaciones, "colPeriodo", "Período", "PeriodoTexto", false, 100);
            AgregarColumna(dgvLiquidaciones, "colAtendidos", "Atendidos", "TotalAtendidos", true, 70);
            AgregarColumna(dgvLiquidaciones, "colAusentes", "Ausentes", "TotalAusentes", true, 70);
            AgregarColumna(dgvLiquidaciones, "colImporteTotal", "Importe total", "ImporteTotalTexto", true, 110);
            AgregarColumna(dgvLiquidaciones, "colFechaEmision", "Fecha de emisión", "FechaEmisionTexto", false, 110);
        }

        // El peso reparte el ancho de la grilla entre las columnas (proporcional).
        private void AgregarColumna(DataGridView grilla, string nombre, string encabezado, string propiedad, bool derecha, float peso)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = nombre;
            columna.HeaderText = TraduccionServicio.Instancia.TraducirODefecto($"{Name}.{grilla.Name}.{nombre}", encabezado);
            columna.DataPropertyName = propiedad;
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            columna.FillWeight = peso;
            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            if (derecha)
                columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grilla.Columns.Add(columna);
        }

        // Al cambiar el periodo, los medicos mostrados dejan de corresponder.
        private void Periodo_Changed(object sender, EventArgs e)
        {
            try
            {
                if (cargando)
                    return;
                CargarMedicos(new List<BE_MedicoPeriodo>(), null);
                CargarLiquidaciones();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        // ---------------------------------------------------------------- CU-02.2 pasos 3 a 5: turnos trabajados

        private void btnConsultarTurnos_Click(object sender, EventArgs e)
        {
            try
            {
                List<BE_MedicoPeriodo> medicos = bll_liquidacion.ObtenerMedicosDelPeriodo(Mes, Anio);
                CargarMedicos(medicos, null);
                if (!medicos.Any(m => m.TurnosAtendidos + m.TurnosAusentes > 0))
                {
                    // Flujo alternativo A1.
                    MessageBox.Show("No hay turnos atendidos ni ausentes en el período.", "Turnos trabajados",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Flujo alternativo A2: especialidad con turnos a liquidar sin valor del turno.
                List<string> sinValor = medicos.SelectMany(m => m.TurnosTrabajados)
                    .Where(t => t.TurnosAtendidos + t.TurnosAusentes > 0 && t.ValorTurno <= 0)
                    .Select(t => t.NombreEspecialidad).Distinct().ToList();
                if (sinValor.Count > 0)
                    MessageBox.Show("Sin valor del turno: " + string.Join(", ", sinValor) +
                        ".\nCárguelo en la opción Valor del turno antes de generar las liquidaciones.", "Turnos trabajados",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        // Muestra los medicos del periodo y deja seleccionado el indicado (o ninguno).
        private void CargarMedicos(List<BE_MedicoPeriodo> medicos, int? idMedico)
        {
            cargando = true;
            try
            {
                dgvMedicos.DataSource = null;
                dgvMedicos.DataSource = medicos;
                dgvMedicos.ClearSelection();
                dgvMedicos.CurrentCell = null;
                medicoSeleccionado = null;
                foreach (DataGridViewRow fila in dgvMedicos.Rows)
                {
                    BE_MedicoPeriodo medico = (BE_MedicoPeriodo)fila.DataBoundItem;
                    if (idMedico != null && medico.IdMedico == idMedico.Value)
                    {
                        dgvMedicos.CurrentCell = fila.Cells[0];
                        fila.Selected = true;
                        medicoSeleccionado = medico;
                    }
                }
            }
            finally
            {
                cargando = false;
            }
            MostrarTurnosDelMedico();
        }

        // CU-02.2 paso 5: al elegir un medico se ven sus turnos por especialidad y el importe estimado.
        private void dgvMedicos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (cargando)
                    return;
                medicoSeleccionado = dgvMedicos.SelectedRows.Count > 0 ? dgvMedicos.SelectedRows[0].DataBoundItem as BE_MedicoPeriodo : null;
                MostrarTurnosDelMedico();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void MostrarTurnosDelMedico()
        {
            dgvTurnosTrabajados.DataSource = null;
            lblTotales.Text = "";
            if (medicoSeleccionado == null)
                return;

            dgvTurnosTrabajados.DataSource = medicoSeleccionado.TurnosTrabajados;
            dgvTurnosTrabajados.ClearSelection();
            lblTotales.Text = $"Total del médico: {medicoSeleccionado.TurnosAtendidos} atendidos · " +
                              $"{medicoSeleccionado.TurnosAusentes} ausentes · {medicoSeleccionado.TurnosCancelados} cancelados · " +
                              medicoSeleccionado.ImporteEstimadoTexto;
        }

        // ---------------------------------------------------------------- CU-02.2 pasos 6 a 11: generar

        private void btnGenerarLiquidacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (medicoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione, en \"Médicos del período\", el médico que quiere liquidar.");
                    return;
                }
                if (medicoSeleccionado.Liquidado)
                {
                    // Flujo alternativo A6.
                    MessageBox.Show("El médico ya tiene liquidación de ese período.", "Liquidación de honorarios",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Generar la liquidación de {medicoSeleccionado.NombreMedico} de {cboMes.Text} {Anio}?\n\n" +
                    $"Turnos atendidos: {medicoSeleccionado.TurnosAtendidos}\n" +
                    $"Turnos ausentes: {medicoSeleccionado.TurnosAusentes}\n" +
                    $"Importe total: {medicoSeleccionado.ImporteEstimadoTexto}",
                    "Confirmar liquidación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion != DialogResult.Yes)
                    return;   // Flujo alternativo A3

                int idMedico = medicoSeleccionado.IdMedico;
                BE_Liquidacion liquidacion = bll_liquidacion.GenerarLiquidacion(idMedico, Mes, Anio);
                MessageBox.Show($"Liquidación N° {liquidacion.NroLiquidacion} generada para {liquidacion.NombreMedico}: " +
                                $"{liquidacion.ImporteTotalTexto}.", "Liquidación de honorarios",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarMedicos(bll_liquidacion.ObtenerMedicosDelPeriodo(Mes, Anio), idMedico);
                CargarLiquidaciones();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Liquidación de honorarios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void CargarLiquidaciones()
        {
            cargando = true;
            try
            {
                liquidacionSeleccionada = null;
                dgvLiquidaciones.DataSource = null;
                if (cboMes.SelectedIndex < 0 || cboAnio.SelectedItem == null)
                    return;

                dgvLiquidaciones.DataSource = bll_liquidacion.ListarLiquidaciones(Mes, Anio);
                dgvLiquidaciones.ClearSelection();
                dgvLiquidaciones.CurrentCell = null;
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
            finally
            {
                cargando = false;
            }
        }

        // ---------------------------------------------------------------- CU-02.3: emitir el recibo

        private void dgvLiquidaciones_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (cargando)
                    return;
                liquidacionSeleccionada = dgvLiquidaciones.CurrentRow?.DataBoundItem as BE_Liquidacion;
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void btnEmitirRecibo_Click(object sender, EventArgs e)
        {
            try
            {
                if (liquidacionSeleccionada == null)
                {
                    MessageBox.Show("Seleccione, en \"Liquidaciones del período\", la liquidación del recibo.");
                    return;
                }

                string ruta;
                using (SaveFileDialog dialogo = new SaveFileDialog())
                {
                    dialogo.Title = "Guardar recibo de liquidación";
                    dialogo.Filter = "Documento PDF (*.pdf)|*.pdf";
                    dialogo.FileName = NombreDeArchivo(liquidacionSeleccionada);
                    if (dialogo.ShowDialog(this) != DialogResult.OK)
                        return;   // Flujo alternativo A1: el contador cancela
                    ruta = dialogo.FileName;
                }

                bll_liquidacion.EmitirRecibo(liquidacionSeleccionada.IdLiquidacion, ruta);

                if (MessageBox.Show($"Recibo emitido en:\n{ruta}\n\n¿Desea abrirlo?", "Recibo emitido",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    Process.Start(ruta);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Recibo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private static string NombreDeArchivo(BE_Liquidacion liquidacion)
        {
            string medico = new string(liquidacion.NombreMedico.Where(c => !Path.GetInvalidFileNameChars().Contains(c)).ToArray())
                .Replace(' ', '_');
            return $"Recibo_{liquidacion.NroLiquidacion}_{medico}_{liquidacion.Anio}-{liquidacion.Mes:00}.pdf";
        }
    }
}
