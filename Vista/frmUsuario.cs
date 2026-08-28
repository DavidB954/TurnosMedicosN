using BE;
using BE.Composite;
using BLL;
using Servicios;
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
    public partial class frmUsuario : frmBase
    {
        public frmUsuario()
        {
            InitializeComponent();
        }

        BLL_Usuario bll_usuario = new BLL_Usuario();
        BLL_Roles bll_Roles = new BLL_Roles();
        BLL_HistorialCambios bll_historial = new BLL_HistorialCambios();

        private int? idSeleccionado = null;
        private bool _cargandoCombo = false;
        public override void AplicarIdioma()   
        {
            AplicarIdiomaAutomatico();
        }
        private void frm_GestionUsuarios_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrid();
                CargarUsuarios();
                CargarUsuarioCbo();
                CargarRoles();
                CargarComboEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar usuarios",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarComboEstado()
        {
            cboActivo.Items.Clear();
            cboActivo.Items.Add("Activo"); 
            cboActivo.Items.Add("Inactivo");
            cboActivo.SelectedIndex = 0;
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
        }

        private void CargarUsuarioCbo()
        {

            _cargandoCombo = true;

            cboUsuarios.DataSource = bll_usuario.ListaUsuarios();
            cboUsuario_Historial.DataSource = bll_usuario.ListaUsuarios();

            cboUsuario_Historial.DisplayMember = "NombreApellido";
            cboUsuario_Historial.ValueMember = "IdUsuario";


            cboUsuarios.DisplayMember = "NombreApellido";
            cboUsuarios.ValueMember = "IdUsuario";
            _cargandoCombo = false;

            // Cargar todo para el primer usuario al iniciar
            CargarCboRolesDelUsuario();
            //  CargarHistorial();
            CargarTreeViewUsuarios();

        }
        private void CargarCboRolesDelUsuario()
        {
            try
            {
                cboRoles.DataSource = null;

                BE_Usuario usuarioSeleccionado = cboUsuarios.SelectedItem as BE_Usuario;
                if (usuarioSeleccionado == null)
                    return;

                int idUsuario = usuarioSeleccionado.IdUsuario;

                List<RolComposite> roles = bll_Roles.ObtenerRolesDeUsuario(idUsuario);

                cboRoles.DataSource = roles;
                cboRoles.DisplayMember = "Nombre";
                cboRoles.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void CargarHistorial()
        {
            if (cboUsuario_Historial.SelectedValue == null)
                return;
            BE_Usuario usuarioSeleccionado = cboUsuario_Historial.SelectedItem as BE_Usuario;
            if (usuarioSeleccionado == null)
                return;


            int idUsuario = usuarioSeleccionado.IdUsuario;

            DataTable historial = bll_historial.ObtenerHistorial("Usuario", idUsuario);

            dgvHistorial.DataSource = historial;
            var t = Servicios.TraduccionServicio.Instancia;
            void SetHeader(string col, string clave)
            {
                string trad = t.Traducir($"frmUsuario.dgvHistorial.{clave}");
                dgvHistorial.Columns[col].HeaderText = trad.StartsWith("[") ? clave : trad;
            }
            SetHeader("FechaCambio", "FechaCambio");
            SetHeader("Usuario", "Usuario");
            SetHeader("Accion", "Accion");
            SetHeader("Campo", "Campo");
            SetHeader("ValorAnterior", "ValorAnterior");
            SetHeader("ValorNuevo", "ValorNuevo");

            //dgvHistorial.Columns["FechaCambio"].HeaderText = "Fecha";
            //dgvHistorial.Columns["Usuario"].HeaderText = "Modificado por";
            //dgvHistorial.Columns["Accion"].HeaderText = "Acción";
            //dgvHistorial.Columns["Campo"].HeaderText = "Campo";
            //dgvHistorial.Columns["ValorAnterior"].HeaderText = "Valor anterior";
            //dgvHistorial.Columns["ValorNuevo"].HeaderText = "Valor nuevo";
        }

        private void CargarTreeViewUsuarios()
        {
            treeViewUsuarios.Nodes.Clear();

            TreeNode raiz = new TreeNode("Usuarios");

            List<BE_Usuario> usuarios = bll_usuario.ListaUsuarios();


            foreach (var usuario in usuarios)
            {
                TreeNode nodoUsuario = new TreeNode($"{usuario.Nombre} {usuario.Apellido}");
                nodoUsuario.Tag = usuario;

                // Reutiliza ObtenerRolesDeUsuario de BLL_Roles
                List<RolComposite> roles = bll_Roles.ObtenerRolesDeUsuario(usuario.IdUsuario);

                foreach (var rol in roles)
                {
                    TreeNode nodoRol = new TreeNode(rol.Nombre) { Tag = rol };
                    TreeViewHelper.AgregarNodosRecursivo(rol, nodoRol);
                    nodoUsuario.Nodes.Add(nodoRol);
                }

                raiz.Nodes.Add(nodoUsuario);
            }

            treeViewUsuarios.Nodes.Add(raiz);
            treeViewUsuarios.ExpandAll();
        }


        private void CargarRoles()
        {
            cboRoles.DataSource = bll_Roles.ObtenerRoles();
            cboRoles.DisplayMember = "Nombre";
            cboRoles.ValueMember = "Id";

        }
        public void LimpiarCampos()
        {
            txtNombreUsuario.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtPassword.Clear();

        }

        private void cboUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoCombo)
                return;


            if (cboUsuarios.SelectedItem == null)
                return;

            // Refrescar el TreeView enfocado en ese usuario
            CargarTreeViewUsuarios();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                else if (e.RowIndex >= 0)
                {
                    idSeleccionado = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells["IdUsuario"].Value);
                    txtNombreUsuario.Text = dgvUsuarios.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                    txtApellido.Text = dgvUsuarios.Rows[e.RowIndex].Cells["Apellido"].Value.ToString();
                    txtEmail.Text = dgvUsuarios.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                    bool activo = Convert.ToBoolean(dgvUsuarios.Rows[e.RowIndex].Cells["Activo"].Value);
                    cboActivo.SelectedItem = activo ? "Activo" : "Inactivo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al seleccionar usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboUsuario_Historial_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtNombreUsuario.Text)|| String.IsNullOrEmpty(txtApellido.Text)|| String.IsNullOrEmpty(txtEmail.Text)|| String.IsNullOrEmpty(txtPassword.Text))
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
                CargarUsuarioCbo();
                CargarUsuarios();
                LimpiarCampos();
                CargarRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar usuario",
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

            else
            {
                try
                {
                    var fila = dgvUsuarios.SelectedRows[0];
                    int id = Convert.ToInt32(fila.Cells["IdUsuario"].Value);
                    bll_usuario.EliminarUsuario(id);
                    CargarUsuarios();
                    CargarRoles();
                    LimpiarCampos();
                    CargarHistorial();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al eliminar usuario",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == null || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor seleccione el usuario a modificar y asegúrese de ingresar una contraseña");
                return;
            }

            else
            {
                try
                {
                    var Fila = dgvUsuarios.SelectedRows[0];

                    BE_Usuario Usuario = new BE_Usuario();
                    Usuario.IdUsuario = Convert.ToInt32(Fila.Cells["IdUsuario"].Value);
                    Usuario.Nombre = txtNombreUsuario.Text;
                    Usuario.Apellido = txtApellido.Text;
                    Usuario.Email = txtEmail.Text;
                    Usuario.HashPassword = txtPassword.Text;
                    Usuario.Activo = cboActivo.SelectedItem.ToString() == "Activo";

                    bll_usuario.ModificarUsuario(Usuario);
                    CargarUsuarioCbo();
                    CargarUsuarios();
                    CargarHistorial();
                    LimpiarCampos();
                    CargarRoles();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al modificar usuario",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnAsignarRol_Click(object sender, EventArgs e)
        {

            try
            {
                int idUsuario = Convert.ToInt32(cboUsuarios.SelectedValue);
                int idRol = Convert.ToInt32(cboRoles.SelectedValue);

                // Verificar si ya tiene el rol antes de intentar asignarlo
                List<RolComposite> rolesActuales = bll_Roles.ObtenerRolesDeUsuario(idUsuario);
                bool yaTieneRol = rolesActuales.Any(r => r.Id == idRol);

                if (yaTieneRol)
                {
                    MessageBox.Show("El usuario ya tiene ese rol asignado.");
                    return;
                }

                bll_Roles.AgregarRolUsuario(idUsuario, idRol);
                MessageBox.Show("Rol asignado correctamente.");
                CargarTreeViewUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
           
        }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewUsuarios.SelectedNode == null)
                {
                    MessageBox.Show("Seleccione un rol para quitar.");
                    return;
                }

                TreeNode nodoSeleccionado = treeViewUsuarios.SelectedNode;
                // Tiene que ser un Rol (hijo directo de un usuario)
                // El padre tiene que tener Tag de BE_Usuario
                if (nodoSeleccionado.Parent == null ||
                    nodoSeleccionado.Parent.Tag == null ||
                    !(nodoSeleccionado.Parent.Tag is BE_Usuario))
                {
                    MessageBox.Show("Seleccione un rol directamente asignado a un usuario.");
                    return;
                }

                // El nodo tiene que ser un RolComposite
                if (!(nodoSeleccionado.Tag is RolComposite rol))
                {
                    MessageBox.Show("Seleccione un rol para quitar.");
                    return;
                }

                BE_Usuario usuario = nodoSeleccionado.Parent.Tag as BE_Usuario;

                var confirmacion = MessageBox.Show(
                    $"¿Quitar el rol '{rol.Nombre}' del usuario '{usuario.Nombre} {usuario.Apellido}'?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion != DialogResult.Yes)
                    return;
                bll_Roles.QuitarRolUsuario(usuario.IdUsuario, rol.Id);

                // Quitar el nodo del TreeView
                nodoSeleccionado.Remove();
                MessageBox.Show("Rol quitado correctamente.");

                CargarTreeViewUsuarios();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar rol: " + ex.Message);
            }
        
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
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

            try
            {
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
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al restaurar: " + ex.Message);
            }
        }


        private void treeViewUsuarios_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Tag is BE_Usuario usuario)
                {
                    cboUsuarios.SelectedValue = usuario.IdUsuario;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        
    }
}
