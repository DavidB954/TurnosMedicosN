using BE;
using BLL;
using Servicios;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Vista
{
    // CU-02.1 Atender Consulta (+ CU-02.5 Emitir Receta, «extend» si Receta = SI).
    // Se abre desde «Atender» en Mi Agenda (frm_AccesoMedico).
    public partial class frmAtencion : frmBase
    {
        BLL_Atencion bll_atencion = new BLL_Atencion();

        private readonly BE_Turno turno;
        private readonly BindingList<BE_RecetaItem> itemsReceta = new BindingList<BE_RecetaItem>();
        private bool registrada = false;
        private bool cambiandoReceta = false;

        // Constructor sin parametros solo para el diseñador de Visual Studio.
        public frmAtencion()
        {
            InitializeComponent();
        }

        public frmAtencion(BE_Turno turno) : this()
        {
            this.turno = turno;
        }

        private string T(string clave, string porDefecto)
        {
            return TraduccionServicio.Instancia.TraducirODefecto($"{Name}.{clave}", porDefecto);
        }

        private void frmAtencion_Load(object sender, EventArgs e)
        {
            try
            {
                AjustarAPantalla();
                ConfigurarGrillaMedicamentos();
                MostrarDatosPaciente();
                ActiveControl = txtMotivo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Con escalado de Windows alto (125 % / 150 %) el formulario puede quedar mas alto que la
        // pantalla: se limita al area de trabajo (sin la barra de tareas), el contenido se desplaza
        // y los botones quedan siempre visibles abajo. Se centra sobre la ventana que lo abrio.
        private void AjustarAPantalla()
        {
            Screen pantalla = Owner != null ? Screen.FromControl(Owner) : Screen.FromPoint(Cursor.Position);
            System.Drawing.Rectangle area = pantalla.WorkingArea;

            Width = Math.Min(Width, area.Width);
            Height = Math.Min(Height, area.Height);

            System.Drawing.Rectangle referencia = Owner != null ? Owner.Bounds : area;
            int x = referencia.Left + (referencia.Width - Width) / 2;
            int y = referencia.Top + (referencia.Height - Height) / 2;
            Location = new System.Drawing.Point(
                Math.Max(area.Left, Math.Min(x, area.Right - Width)),
                Math.Max(area.Top, Math.Min(y, area.Bottom - Height)));
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
            ActualizarTitulo();
            rbRecetaSi.Text = T("rbRecetaSi", "SÍ");
            rbRecetaNo.Text = T("rbRecetaNo", "NO");
        }

        // Paciente, N° de Historia Clinica y DNI en solo lectura.
        private void MostrarDatosPaciente()
        {
            if (turno == null)
                return;

            txtPaciente.Text = turno.NombreApellidoPaciente;
            txtHistoriaClinica.Text = turno.Paciente != null ? turno.Paciente.NroHistoriaClinica.ToString() : "";
            txtDNI.Text = turno.Paciente != null ? turno.Paciente.DNI : "";
            ActualizarTitulo();
        }

        private void ActualizarTitulo()
        {
            Text = turno == null
                ? T("Titulo", "Registrar Atención")
                : $"{T("Titulo", "Registrar Atención")} – {turno.FechaTexto} {turno.HoraTexto}";
        }

        private void ConfigurarGrillaMedicamentos()
        {
            dgvMedicamentos.AutoGenerateColumns = false;
            dgvMedicamentos.Columns.Clear();
            AgregarColumna("colMedicamento", "Medicamento", "Medicamento", 32);
            AgregarColumna("colPresentacion", "Presentación", "Presentacion", 22);
            AgregarColumna("colPosologia", "Posología", "Posologia", 30);
            AgregarColumna("colCantidad", "Cant.", "Cantidad", 8);

            DataGridViewButtonColumn quitar = new DataGridViewButtonColumn();
            quitar.Name = "colQuitar";
            quitar.HeaderText = T("dgvMedicamentos.colQuitar", "Quitar");
            quitar.Text = "✕";
            quitar.UseColumnTextForButtonValue = true;
            quitar.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            quitar.FillWeight = 8;
            dgvMedicamentos.Columns.Add(quitar);

            dgvMedicamentos.DataSource = itemsReceta;
        }

        private void AgregarColumna(string nombre, string encabezado, string propiedad, float peso)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.Name = nombre;
            columna.HeaderText = T($"dgvMedicamentos.{nombre}", encabezado);
            columna.DataPropertyName = propiedad;
            columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            columna.FillWeight = peso;
            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvMedicamentos.Columns.Add(columna);
        }

        // ---------------------------------------------------------------- Receta (CU-02.5)

        private void rbReceta_CheckedChanged(object sender, EventArgs e)
        {
            // CheckedChanged se dispara en los dos radio: se procesa solo el que quedo marcado.
            RadioButton radio = sender as RadioButton;
            if (cambiandoReceta || radio == null || !radio.Checked)
                return;

            if (rbRecetaNo.Checked && itemsReceta.Count > 0)
            {
                // A2 - Cambio a Receta = NO: se descartan los items cargados
                if (MessageBox.Show(T("Msg.DescartarReceta", "Se descartarán los medicamentos cargados. ¿Continuar?"),
                        T("Msg.Receta", "Receta"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    cambiandoReceta = true;
                    rbRecetaSi.Checked = true;
                    cambiandoReceta = false;
                    return;
                }
                itemsReceta.Clear();
                LimpiarItem();
            }

            groupBoxReceta.Visible = rbRecetaSi.Checked;
            if (rbRecetaSi.Checked)
                txtMedicamento.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                BE_RecetaItem item = new BE_RecetaItem
                {
                    Medicamento = txtMedicamento.Text,
                    Presentacion = txtPresentacion.Text,
                    Posologia = txtPosologia.Text,
                    Cantidad = (int)nudCantidad.Value
                };

                bll_atencion.ValidarItemReceta(item);
                itemsReceta.Add(item);
                LimpiarItem();
                txtMedicamento.Focus();
            }
            catch (ArgumentException ex)
            {
                // A1 - Item incompleto: se informa y no se agrega
                MessageBox.Show(ex.Message, T("Msg.Receta", "Receta"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarItem()
        {
            txtMedicamento.Clear();
            txtPresentacion.Clear();
            txtPosologia.Clear();
            nudCantidad.Value = 1;
        }

        private void dgvMedicamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= itemsReceta.Count)
                return;
            if (dgvMedicamentos.Columns[e.ColumnIndex].Name == "colQuitar")
                itemsReceta.RemoveAt(e.RowIndex);
        }

        // ---------------------------------------------------------------- Registrar (CU-02.1)

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            BE_AtencionMedica atencion = new BE_AtencionMedica
            {
                Turno = turno,
                MotivoConsulta = txtMotivo.Text,
                Indicaciones = txtIndicaciones.Text
            };

            if (rbRecetaSi.Checked)
            {
                atencion.Receta = new BE_Receta();
                foreach (BE_RecetaItem item in itemsReceta)
                    atencion.Receta.Items.Add(item);
            }

            btnRegistrar.Enabled = false;
            Cursor = Cursors.WaitCursor;
            try
            {
                bll_atencion.RegistrarAtencion(atencion);
                registrada = true;

                string mensaje = T("Msg.AtencionRegistrada", "Atención registrada en la Historia Clínica. El turno quedó Atendido.");
                if (atencion.Receta != null)
                    mensaje += Environment.NewLine + string.Format(T("Msg.RecetaEmitida", "Receta N° {0} emitida."), atencion.Receta.NroReceta);

                Cursor = Cursors.Default;
                MessageBox.Show(mensaje, T("Titulo", "Registrar Atención"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                // A2 - Datos incompletos / turno no atendible: se conserva lo cargado
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, T("Msg.DatosIncompletos", "Revise los datos"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // A4 - Error al guardar: la transaccion se revirtio, el turno sigue Confirmado
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnRegistrar.Enabled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        // A3 - Cancelacion: si hay datos cargados se pide confirmacion. No se guarda nada.
        private void frmAtencion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (registrada)
                return;

            bool hayDatos = !string.IsNullOrWhiteSpace(txtMotivo.Text)
                         || !string.IsNullOrWhiteSpace(txtIndicaciones.Text)
                         || itemsReceta.Count > 0;
            if (!hayDatos)
                return;

            if (MessageBox.Show(T("Msg.ConfirmarCancelar", "¿Cerrar sin registrar la atención? No se guardará nada y el turno seguirá Confirmado."),
                    T("Titulo", "Registrar Atención"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;
        }
    }
}
