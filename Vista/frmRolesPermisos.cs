using BE.Composite;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Vista
{
    // Gestión de Roles:
    //   - ABM de roles (nombre y descripción)
    //   - A cada rol se le asignan formularios completos (familia) y/o botones sueltos (patente)
    //     tildándolos en el árbol del catálogo de permisos.
    public partial class frmRolesPermisos : frmBase
    {
        public frmRolesPermisos()
        {
            InitializeComponent();
            arbolPermisos = new ArbolPermisos(tvPermisos);
        }

        BLL_Roles bll_roles = new BLL_Roles();

        private readonly ArbolPermisos arbolPermisos;

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
            arbolPermisos.RefrescarTextos();
        }

        private void frmRolesPermisos_Load(object sender, EventArgs e)
        {
            try
            {
                arbolPermisos.Cargar(bll_roles.ObtenerCatalogo());
                CargarRoles(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar roles y permisos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private RolComposite RolSeleccionado()
        {
            return lstRoles.SelectedItem as RolComposite;
        }

        // Recarga la lista de roles y deja seleccionado el indicado (o el primero).
        private void CargarRoles(int? idSeleccionar)
        {
            List<RolComposite> roles = bll_roles.ObtenerRoles();

            lstRoles.Items.Clear();
            foreach (var rol in roles)
                lstRoles.Items.Add(rol);

            int indice = idSeleccionar.HasValue ? roles.FindIndex(r => r.Id == idSeleccionar.Value) : -1;
            if (indice < 0 && roles.Count > 0)
                indice = 0;

            lstRoles.SelectedIndex = indice;
            if (indice < 0)
                MostrarRol(null);
        }

        private void MostrarRol(RolComposite rol)
        {
            txtNombre.Text = rol?.Nombre ?? "";
            txtDescripcion.Text = rol?.Descripcion ?? "";

            arbolPermisos.SoloLectura = false;
            arbolPermisos.Marcar(
                rol?.Hijos().OfType<RolComposite>().Where(r => r.EsFormulario).Select(r => r.Id) ?? Enumerable.Empty<int>(),
                rol?.Hijos().OfType<Permiso>().Select(p => p.Id) ?? Enumerable.Empty<int>());

            // El Administrador siempre tiene todo: se muestra pero no se puede editar
            bool esAdmin = BLL_Roles.EsAdministrador(rol);
            arbolPermisos.SoloLectura = rol == null || esAdmin;
            btnGuardarPermisos.Enabled = rol != null && !esAdmin;
            btnEliminarRol.Enabled = rol != null && !esAdmin;
        }

        private void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MostrarRol(RolSeleccionado());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al mostrar el rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNuevoRol_Click(object sender, EventArgs e)
        {
            try
            {
                int idNuevo = bll_roles.CrearRol(txtNombre.Text, txtDescripcion.Text);

                CargarRoles(idNuevo);
                MessageBox.Show("Rol creado. Ahora tilde los formularios o botones que puede usar y presione Guardar Permisos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al crear el rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModificarRol_Click(object sender, EventArgs e)
        {
            try
            {
                RolComposite rol = RolSeleccionado();
                if (rol == null)
                {
                    MessageBox.Show("Seleccione un rol.");
                    return;
                }

                bll_roles.ModificarRol(rol.Id, txtNombre.Text, txtDescripcion.Text);

                CargarRoles(rol.Id);
                MessageBox.Show("Rol modificado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar el rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            try
            {
                RolComposite rol = RolSeleccionado();
                if (rol == null)
                {
                    MessageBox.Show("Seleccione un rol.");
                    return;
                }

                if (MessageBox.Show($"¿Eliminar el rol '{rol.Nombre}'?", "Confirmar eliminación",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                bll_roles.EliminarRol(rol.Id);

                CargarRoles(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al eliminar el rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardarPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                RolComposite rol = RolSeleccionado();
                if (rol == null)
                {
                    MessageBox.Show("Seleccione un rol.");
                    return;
                }

                arbolPermisos.ObtenerSeleccion(out List<int> idsFormularios, out List<int> idsPermisos);

                bll_roles.GuardarPermisosRol(rol.Id, idsFormularios, idsPermisos);

                CargarRoles(rol.Id);
                MessageBox.Show("Permisos del rol guardados. Los usuarios con este rol los verán al volver a ingresar.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar permisos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
