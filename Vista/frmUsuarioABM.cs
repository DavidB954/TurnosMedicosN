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
        BLL_Paciente bll_paciente = new BLL_Paciente();

        private int? idSeleccionado = null;

        // Paciente del usuario seleccionado (null si todavia no tiene Historia Clinica).
        private BE_Paciente pacienteSeleccionado = null;

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
            LimpiarDatosPaciente();
        }

        private void LimpiarDatosPaciente()
        {
            pacienteSeleccionado = null;
            txtDNI.Clear();
            txtTelefono.Clear();
            dtpFechaNacimiento.Value = DateTime.Today;
            dtpFechaNacimiento.Checked = false;
            lblNroHC.Text = "";
        }

        private void MostrarDatosPaciente(int idUsuario)
        {
            LimpiarDatosPaciente();
            pacienteSeleccionado = bll_paciente.ObtenerPorUsuario(idUsuario);
            if (pacienteSeleccionado == null)
                return;

            txtDNI.Text = pacienteSeleccionado.DNI;
            txtTelefono.Text = pacienteSeleccionado.Telefono;
            if (pacienteSeleccionado.FechaNacimiento.HasValue)
            {
                dtpFechaNacimiento.Value = pacienteSeleccionado.FechaNacimiento.Value;
                dtpFechaNacimiento.Checked = true;
            }
            lblNroHC.Text = pacienteSeleccionado.HistoriaClinicaTexto;
        }

        // Devuelve los datos de paciente cargados, o null si no se completo ninguno.
        // Si ya tiene Historia Clinica, el DNI no puede quedar vacio (la HC no se borra).
        private BE_Paciente LeerDatosPaciente(int idUsuario)
        {
            bool algunDato = !string.IsNullOrWhiteSpace(txtDNI.Text)
                          || !string.IsNullOrWhiteSpace(txtTelefono.Text)
                          || dtpFechaNacimiento.Checked;

            if (!algunDato && pacienteSeleccionado == null)
                return null;

            if (string.IsNullOrWhiteSpace(txtDNI.Text))
                throw new ArgumentException(pacienteSeleccionado != null
                    ? "El DNI no puede quedar vacío: el paciente ya tiene Historia Clínica."
                    : "Para cargar datos de paciente ingrese el DNI.");

            return new BE_Paciente
            {
                IdUsuario = idUsuario,
                DNI = txtDNI.Text,
                Telefono = txtTelefono.Text,
                FechaNacimiento = dtpFechaNacimiento.Checked ? dtpFechaNacimiento.Value.Date : (DateTime?)null
            };
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
                MostrarDatosPaciente(idSeleccionado.Value);
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

                // Los datos de paciente se validan ANTES de crear el usuario.
                pacienteSeleccionado = null;
                BE_Paciente paciente = LeerDatosPaciente(0);
                if (paciente != null)
                    bll_paciente.ValidarDatosPaciente(paciente);

                bll_usuario.AgregarUsuario(Usuario);

                if (paciente != null)
                {
                    paciente.IdUsuario = Usuario.IdUsuario;
                    bll_paciente.GuardarDatosPaciente(paciente);
                }

                CargarUsuarios();
                LimpiarCampos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Datos de paciente",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                BE_Paciente paciente = LeerDatosPaciente(Usuario.IdUsuario);
                if (paciente != null)
                    bll_paciente.ValidarDatosPaciente(paciente);

                bll_usuario.ModificarUsuario(Usuario);

                if (paciente != null)
                    bll_paciente.GuardarDatosPaciente(paciente);

                CargarUsuarios();
                LimpiarCampos();
            }
            catch (ArgumentException ex) when (!ex.Message.StartsWith("Error en ModificarUsuario"))
            {
                MessageBox.Show(ex.Message, "Datos de paciente",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
