using BE;
using BLL;
using System;
using System.Windows.Forms;

namespace Vista
{
    // Gestión de Usuarios > ABM de Usuarios: alta, modificación y baja.
    public partial class frmUsuarioABM : frmBase
    {
        public frmUsuarioABM()
        {
            InitializeComponent();
        }

        BLL_Usuario bll_usuario = new BLL_Usuario();

        private int? idSeleccionado = null;

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
            CargarComboEstado();
        }

        private void frmUsuarioABM_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrid();
                CargarComboEstado();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar usuarios",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Índice 0 = Activo, 1 = Inactivo (el texto depende del idioma)
        private void CargarComboEstado()
        {
            var t = Servicios.TraduccionServicio.Instancia;
            int indice = cboActivo.SelectedIndex < 0 ? 0 : cboActivo.SelectedIndex;

            cboActivo.Items.Clear();
            cboActivo.Items.Add(t.TraducirODefecto($"{Name}.Activo", "Activo"));
            cboActivo.Items.Add(t.TraducirODefecto($"{Name}.Inactivo", "Inactivo"));
            cboActivo.SelectedIndex = indice;
        }

        private void ConfigurarGrid()
        {
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
        }

        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = bll_usuario.ListaUsuarios();

            dgvUsuarios.Columns["HashPassword"].Visible = false;
            dgvUsuarios.Columns["usuario"].Visible = false;
            dgvUsuarios.Columns["NombreApellido"].Visible = false;
            dgvUsuarios.Columns["DVH"].Visible = false;
            dgvUsuarios.ClearSelection();
            dgvUsuarios.CurrentCell = null;
            idSeleccionado = null;

            // Al recargar la grilla se regeneran las columnas: se vuelven a traducir los encabezados
            AplicarIdiomaAutomatico();
        }

        public void LimpiarCampos()
        {
            txtNombreUsuario.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            cboActivo.SelectedIndex = 0;
            idSeleccionado = null;
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                var fila = dgvUsuarios.Rows[e.RowIndex];
                idSeleccionado = Convert.ToInt32(fila.Cells["IdUsuario"].Value);
                txtNombreUsuario.Text = fila.Cells["Nombre"].Value.ToString();
                txtApellido.Text = fila.Cells["Apellido"].Value.ToString();
                txtEmail.Text = fila.Cells["Email"].Value.ToString();
                bool activo = Convert.ToBoolean(fila.Cells["Activo"].Value);
                cboActivo.SelectedIndex = activo ? 0 : 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al seleccionar usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombreUsuario.Text) || String.IsNullOrEmpty(txtApellido.Text) || String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Por favor complete todos los datos");
                    return;
                }
                BE_Usuario Usuario = new BE_Usuario();

                Usuario.Nombre = txtNombreUsuario.Text;
                Usuario.Apellido = txtApellido.Text;
                Usuario.Email = txtEmail.Text;
                Usuario.HashPassword = txtPassword.Text;

                bll_usuario.AgregarUsuario(Usuario);
                CargarUsuarios();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == null || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor seleccione el usuario a modificar y asegúrese de ingresar una contraseña");
                return;
            }

            try
            {
                BE_Usuario Usuario = new BE_Usuario();
                Usuario.IdUsuario = idSeleccionado.Value;
                Usuario.Nombre = txtNombreUsuario.Text;
                Usuario.Apellido = txtApellido.Text;
                Usuario.Email = txtEmail.Text;
                Usuario.HashPassword = txtPassword.Text;
                Usuario.Activo = cboActivo.SelectedIndex == 0;

                bll_usuario.ModificarUsuario(Usuario);
                CargarUsuarios();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == null)
            {
                MessageBox.Show("Por favor seleccione el usuario a eliminar");
                return;
            }

            if (MessageBox.Show($"¿Eliminar el usuario '{txtNombreUsuario.Text} {txtApellido.Text}'?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                bll_usuario.EliminarUsuario(idSeleccionado.Value);
                CargarUsuarios();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al eliminar usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
