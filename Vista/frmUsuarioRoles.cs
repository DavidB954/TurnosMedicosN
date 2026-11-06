using BE;
using BE.Composite;
using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Vista
{
    // Gestión de Usuarios > Asignación de Roles:
    //   - Filtro de usuarios por rol
    //   - Roles comunes del usuario (Usuario_Rol)
    //   - Permisos directos: una página completa (familia de formulario) o botones sueltos (Usuario_Permiso)
    //   - Vista de solo lectura con todo lo que el usuario puede hacer (Composite resultante)
    public partial class frmUsuarioRoles : frmBase
    {
        public frmUsuarioRoles()
        {
            InitializeComponent();
            arbolPermisos = new ArbolPermisos(tvPermisosDirectos);
        }

        BLL_Usuario bll_usuario = new BLL_Usuario();
        BLL_Roles bll_roles = new BLL_Roles();

        private readonly ArbolPermisos arbolPermisos;
        private bool _cargandoCombo = false;
        private List<BE_Usuario> todosLosUsuarios = new List<BE_Usuario>();

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
            arbolPermisos.RefrescarTextos();

            // Texto de la opción "(Todos)" del filtro
            if (cboFiltroRol.Items.Count > 0 && cboFiltroRol.Items[0] is RolComposite todos && todos.Id == 0)
            {
                todos.Nombre = TextoTodos();
                _cargandoCombo = true;
                cboFiltroRol.Items[0] = todos;
                _cargandoCombo = false;
            }

            BE_Usuario usuario = UsuarioSeleccionado();
            if (usuario != null)
                CargarPermisosEfectivos(usuario.IdUsuario);
        }

        private string TextoTodos()
        {
            return TraduccionServicio.Instancia.TraducirODefecto($"{Name}.Todos", "(Todos)");
        }

        private void frmUsuarioRoles_Load(object sender, EventArgs e)
        {
            try
            {
                List<RolComposite> roles = bll_roles.ObtenerRoles();

                clbRoles.Items.Clear();
                foreach (var rol in roles)
                    clbRoles.Items.Add(rol);

                // Filtro por rol: "(Todos)" + los roles comunes
                _cargandoCombo = true;
                cboFiltroRol.Items.Clear();
                cboFiltroRol.Items.Add(new RolComposite { Id = 0, Nombre = TextoTodos() });
                foreach (var rol in roles)
                    cboFiltroRol.Items.Add(rol);
                cboFiltroRol.SelectedIndex = 0;
                _cargandoCombo = false;

                arbolPermisos.Cargar(bll_roles.ObtenerCatalogo());

                todosLosUsuarios = bll_usuario.ListaUsuarios();
                FiltrarUsuarios();
            }
            catch (Exception ex)
            {
                _cargandoCombo = false;
                MessageBox.Show(ex.Message, "Error al cargar la asignación de roles",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Muestra en el combo de usuarios solo los que tienen el rol elegido en el filtro
        private void FiltrarUsuarios()
        {
            int? idAnterior = UsuarioSeleccionado()?.IdUsuario;

            List<BE_Usuario> usuarios = todosLosUsuarios;
            if (cboFiltroRol.SelectedItem is RolComposite rolFiltro && rolFiltro.Id > 0)
            {
                var ids = new HashSet<int>(bll_roles.ObtenerIdsUsuariosConRol(rolFiltro.Id));
                usuarios = todosLosUsuarios.Where(u => ids.Contains(u.IdUsuario)).ToList();
            }

            _cargandoCombo = true;
            try
            {
                cboUsuarios.DataSource = null;
                cboUsuarios.DataSource = usuarios;
                cboUsuarios.DisplayMember = "NombreApellido";
                cboUsuarios.ValueMember = "IdUsuario";
                if (idAnterior.HasValue && usuarios.Any(u => u.IdUsuario == idAnterior.Value))
                    cboUsuarios.SelectedValue = idAnterior.Value;
            }
            finally
            {
                _cargandoCombo = false;
            }

            if (usuarios.Count == 0)
                LimpiarUsuario();
            else
                CargarUsuarioSeleccionado();
        }

        // Sin usuarios para el filtro: se vacían las marcas para no mostrar datos de otro usuario
        private void LimpiarUsuario()
        {
            for (int i = 0; i < clbRoles.Items.Count; i++)
                clbRoles.SetItemChecked(i, false);

            arbolPermisos.Marcar(Enumerable.Empty<int>(), Enumerable.Empty<int>());
            tvPermisosEfectivos.Nodes.Clear();
        }

        private BE_Usuario UsuarioSeleccionado()
        {
            return cboUsuarios.SelectedItem as BE_Usuario;
        }

        private void CargarUsuarioSeleccionado()
        {
            BE_Usuario usuario = UsuarioSeleccionado();
            if (usuario == null)
                return;

            List<RolComposite> rolesUsuario = bll_roles.ObtenerRolesDeUsuario(usuario.IdUsuario);
            var idsRoles = new HashSet<int>(rolesUsuario.Where(r => !r.EsFormulario).Select(r => r.Id));

            for (int i = 0; i < clbRoles.Items.Count; i++)
                clbRoles.SetItemChecked(i, idsRoles.Contains(((RolComposite)clbRoles.Items[i]).Id));

            RolComposite permisosEfectivos = bll_roles.ObtenerPermisosUsuario(usuario.IdUsuario);

            arbolPermisos.Marcar(
                rolesUsuario.Where(r => r.EsFormulario).Select(r => r.Id),
                bll_roles.ObtenerPermisosDirectosUsuario(usuario.IdUsuario).Select(p => p.Id),
                permisosEfectivos);

            CargarPermisosEfectivos(usuario.IdUsuario, permisosEfectivos);
        }

        private void CargarPermisosEfectivos(int idUsuario, RolComposite permisos = null)
        {
            tvPermisosEfectivos.BeginUpdate();
            tvPermisosEfectivos.Nodes.Clear();

            permisos = permisos ?? bll_roles.ObtenerPermisosUsuario(idUsuario);
            TreeNode raiz = new TreeNode(UsuarioSeleccionado()?.NombreApellido ?? permisos.Nombre) { Tag = permisos };
            TreeViewHelper.AgregarNodosRecursivo(permisos, raiz);

            tvPermisosEfectivos.Nodes.Add(raiz);
            raiz.Expand();
            tvPermisosEfectivos.EndUpdate();
        }

        private void cboFiltroRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoCombo)
                return;

            try
            {
                FiltrarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al filtrar usuarios",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoCombo)
                return;

            try
            {
                CargarUsuarioSeleccionado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar el usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AvisarGuardado()
        {
            MessageBox.Show("Cambios guardados. Si el usuario tiene la sesión abierta, los verá al volver a ingresar.");
        }

        private void btnGuardarRoles_Click(object sender, EventArgs e)
        {
            try
            {
                BE_Usuario usuario = UsuarioSeleccionado();
                if (usuario == null)
                {
                    MessageBox.Show("Seleccione un usuario.");
                    return;
                }

                List<int> idsRoles = clbRoles.CheckedItems.Cast<RolComposite>().Select(r => r.Id).ToList();

                bll_roles.GuardarRolesUsuario(usuario.IdUsuario, idsRoles);

                // El usuario puede haber dejado de tener el rol del filtro
                FiltrarUsuarios();
                AvisarGuardado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar roles",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarUsuarioSeleccionadoSinError();
            }
        }

        private void btnGuardarPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                BE_Usuario usuario = UsuarioSeleccionado();
                if (usuario == null)
                {
                    MessageBox.Show("Seleccione un usuario.");
                    return;
                }

                arbolPermisos.ObtenerSeleccion(out List<int> idsFormularios, out List<int> idsPermisos);

                bll_roles.GuardarPermisosDirectosUsuario(usuario.IdUsuario, idsFormularios, idsPermisos);

                CargarUsuarioSeleccionado();
                AvisarGuardado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar permisos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarUsuarioSeleccionadoSinError();
            }
        }

        // Si un guardado falla a mitad de camino, se vuelve a mostrar lo que realmente quedó en la base.
        private void CargarUsuarioSeleccionadoSinError()
        {
            try
            {
                CargarUsuarioSeleccionado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al recargar el usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
