using BE;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Vista
{
    public partial class frmValorTurno : frmBase
    {
        BLL_Especialidad bll_especialidad = new BLL_Especialidad();

        private BE_Especialidad especialidadSeleccionada = null;
        private bool cargandoGrilla = false;

        public frmValorTurno()
        {
            InitializeComponent();
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }

        private void frmValorTurno_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla();
                CargarEspecialidades();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void ConfigurarGrilla()
        {
            dgvEspecialidades.AutoGenerateColumns = false;
            dgvEspecialidades.Columns.Clear();
            AgregarColumna("colNombre", "Especialidad", "Nombre", DataGridViewContentAlignment.MiddleLeft);
            AgregarColumna("colValorTurno", "Valor del turno", "ValorTurnoTexto", DataGridViewContentAlignment.MiddleRight);
            AgregarColumna("colPorcentajeAusente", "% por ausencia", "PorcentajeAusenteTexto", DataGridViewContentAlignment.MiddleRight);
        }

        private void AgregarColumna(string nombre, string encabezado, string propiedad, DataGridViewContentAlignment alineacion)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = nombre;
            columna.HeaderText = TraduccionServicio.Instancia.TraducirODefecto($"{Name}.dgvEspecialidades.{nombre}", encabezado);
            columna.DataPropertyName = propiedad;
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            columna.DefaultCellStyle.Alignment = alineacion;
            dgvEspecialidades.Columns.Add(columna);
        }

        private void CargarEspecialidades()
        {
            int? idAnterior = especialidadSeleccionada?.IdEspecialidad;
            cargandoGrilla = true;
            try
            {
                List<BE_Especialidad> especialidades = bll_especialidad.ListarEspecialidades();
                dgvEspecialidades.DataSource = null;
                dgvEspecialidades.DataSource = especialidades;
                dgvEspecialidades.ClearSelection();
                dgvEspecialidades.CurrentCell = null;
                MostrarEspecialidad(null);

                if (especialidades.Count == 0)
                {
                    MessageBox.Show("No hay especialidades registradas.", "Valor del turno",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (DataGridViewRow fila in dgvEspecialidades.Rows)
                {
                    if (idAnterior.HasValue && ((BE_Especialidad)fila.DataBoundItem).IdEspecialidad == idAnterior.Value)
                    {
                        dgvEspecialidades.CurrentCell = fila.Cells[0];
                        fila.Selected = true;
                        MostrarEspecialidad((BE_Especialidad)fila.DataBoundItem);
                    }
                }
            }
            finally
            {
                cargandoGrilla = false;
            }
        }

        private void dgvEspecialidades_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (cargandoGrilla)
                    return;
                MostrarEspecialidad(dgvEspecialidades.CurrentRow?.DataBoundItem as BE_Especialidad);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void MostrarEspecialidad(BE_Especialidad especialidad)
        {
            especialidadSeleccionada = especialidad;
            txtEspecialidad.Text = especialidad?.Nombre ?? "";
            txtValorTurno.Text = especialidad == null ? "" : especialidad.ValorTurno.ToString("0.00", Formato.Cultura);
            txtPorcentajeAusente.Text = especialidad == null ? "" : especialidad.PorcentajeAusente.ToString("0.##", Formato.Cultura);
        }

        private void btnGuardarValorTurno_Click(object sender, EventArgs e)
        {
            try
            {
                if (especialidadSeleccionada == null)
                {
                    MessageBox.Show("Seleccione una especialidad de la lista.");
                    return;
                }

                decimal valorTurno, porcentajeAusente;
                if (!LeerNumero(txtValorTurno.Text, out valorTurno))
                {
                    MessageBox.Show("Ingrese el valor del turno como un número (por ejemplo 18000,50).");
                    txtValorTurno.Focus();
                    return;
                }
                if (!LeerNumero(txtPorcentajeAusente.Text, out porcentajeAusente))
                {
                    MessageBox.Show("Ingrese el porcentaje por ausencia como un número entre 0 y 100 (por ejemplo 50).");
                    txtPorcentajeAusente.Focus();
                    return;
                }

                bll_especialidad.ActualizarValorTurno(especialidadSeleccionada.IdEspecialidad, valorTurno, porcentajeAusente);

                MessageBox.Show($"Valor del turno de {especialidadSeleccionada.Nombre} actualizado: {Formato.Moneda(valorTurno)} " +
                                $"y {Formato.Porcentaje(porcentajeAusente)} por ausencia.");
                CargarEspecialidades();
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

        private static bool LeerNumero(string texto, out decimal numero)
        {
            texto = (texto ?? "").Trim().Replace("$", "").Replace("%", "").Replace(" ", "");
            numero = 0;
            if (texto.Length == 0 || texto.Count(c => c == ',' || c == '.') > 1)
                return false;
            return decimal.TryParse(texto.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out numero);
        }
    }
}
