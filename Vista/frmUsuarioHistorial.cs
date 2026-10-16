using BE;
using BLL;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vista
{
    // Gestión de Usuarios > Historial: cambios de un usuario y restauración a un estado anterior.
    public partial class frmUsuarioHistorial : frmBase
    {
        public frmUsuarioHistorial()
        {
            InitializeComponent();
        }

        BLL_Usuario bll_usuario = new BLL_Usuario();
        BLL_HistorialCambios bll_historial = new BLL_HistorialCambios();

        private bool _cargandoCombo = false;

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
            TraducirEncabezados();
        }

        private void frmUsuarioHistorial_Load(object sender, EventArgs e)
        {
            try
            {
                dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvHistorial.MultiSelect = false;

                _cargandoCombo = true;
                cboUsuario_Historial.DataSource = bll_usuario.ListaUsuarios();
                cboUsuario_Historial.DisplayMember = "NombreApellido";
                cboUsuario_Historial.ValueMember = "IdUsuario";
                _cargandoCombo = false;

                CargarHistorial();
            }
            catch (Exception ex)
            {
                _cargandoCombo = false;
                MessageBox.Show(ex.Message, "Error al cargar historial",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarHistorial()
        {
            BE_Usuario usuarioSeleccionado = cboUsuario_Historial.SelectedItem as BE_Usuario;
            if (usuarioSeleccionado == null)
                return;

            DataTable historial = bll_historial.ObtenerHistorial("Usuario", usuarioSeleccionado.IdUsuario);

            dgvHistorial.DataSource = historial;
            TraducirEncabezados();
        }

        private void TraducirEncabezados()
        {
            if (dgvHistorial.Columns.Count == 0)
                return;

            var t = Servicios.TraduccionServicio.Instancia;
            void SetHeader(string col)
            {
                if (!dgvHistorial.Columns.Contains(col))
                    return;
                string trad = t.Traducir($"{Name}.dgvHistorial.{col}");
                dgvHistorial.Columns[col].HeaderText = trad.StartsWith("[") ? col : trad;
            }
            SetHeader("FechaCambio");
            SetHeader("Usuario");
            SetHeader("Accion");
            SetHeader("Campo");
            SetHeader("ValorAnterior");
            SetHeader("ValorNuevo");
        }

        private void cboUsuario_Historial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoCombo)
                return;

            try
            {
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar historial",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvHistorial.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un punto del historial para restaurar.");
                    return;
                }

                // Fecha del registro seleccionado en la grilla
                DateTime fechaSeleccionada = Convert.ToDateTime(
                    dgvHistorial.SelectedRows[0].Cells["FechaCambio"].Value);

                int idUsuario = Convert.ToInt32(cboUsuario_Historial.SelectedValue);

                var confirmacion = MessageBox.Show(
                    $"¿Restaurar el estado del usuario al {fechaSeleccionada:dd/MM/yyyy HH:mm}?",
                    "Confirmar restauración",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion != DialogResult.Yes)
                    return;

                // Recomponer el estado anterior
                BE_Usuario usuarioRestaurado = bll_historial.RecomponerEstado(idUsuario, fechaSeleccionada);

                // Confirmar al usuario qué se va a restaurar
                string detalle = $"Se restaurará:\n" +
                                 $"Nombre: {usuarioRestaurado.Nombre}\n" +
                                 $"Apellido: {usuarioRestaurado.Apellido}\n" +
                                 $"Email: {usuarioRestaurado.Email}\n" +
                                 $"Activo: {usuarioRestaurado.Activo}";

                if (MessageBox.Show(detalle + "\n\n¿Confirmar?", "Estado a restaurar",
                    MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                // Guardar el estado restaurado — esto también registra el cambio en el historial y en la bitácora
                bll_usuario.RestaurarUsuario(usuarioRestaurado, fechaSeleccionada);

                MessageBox.Show("Estado restaurado correctamente.");

                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al restaurar: " + ex.Message);
            }
        }
    }
}
