using BE.Composite;
using BLL;
using Microsoft.VisualBasic;
using Servicios;
using System;
using System.CodeDom;
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
    public partial class frmRolesPermisos : frmBase
    {
        public frmRolesPermisos()
        {
            InitializeComponent();
            Raiz();
        }

        public override void AplicarIdioma()    
        {
            AplicarIdiomaAutomatico();
        }

        private void frm_GestionRoles_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombo();
                RefrescarVistas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar roles y permisos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        BLL_Roles bll_roles = new BLL_Roles();



        private void Raiz()
        {
            RolComposite raiz = new RolComposite
            {
                Nombre = "Roles"
            };

            TreeNode root = new TreeNode("Roles");
            root.Tag = raiz;
            treeViewRoles.Nodes.Clear();
            treeViewRoles.Nodes.Add(root);
        }

        public void RefrescarVistas()
        {
            CargarTreeView();
            CargarComboRolesExistentes();
            CargarPermisosAlCombo();
        }
        private bool ConfirmarEliminacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        private bool ValidarNodoSeleccionado(out RolComposite padreDestino)
        {
            padreDestino = null;

            if (treeViewRoles.SelectedNode == null)
            {
                MessageBox.Show("Seleccione un nodo destino.");
                return false;
            }

            padreDestino = treeViewRoles.SelectedNode.Tag as RolComposite;

            if (padreDestino == null)
            {
                MessageBox.Show("Solo se puede agregar dentro de un rol.");
                return false;
            }

            return true;
        }

        public class TipoItem
        {
            public int ID
            {
                get; set;
            }
            public string Nombre
            {
                get; set;
            }
        }

        public void CargarCombo()
        {
            var tipos = new List<TipoItem>
            {
                new TipoItem { ID = 1, Nombre = "Permiso" },
                new TipoItem { ID = 2, Nombre = "Rol" }
            };

            cboTipo.DataSource = tipos;
            cboTipo.DisplayMember = "Nombre";
            cboTipo.ValueMember = "ID";
        }

        private void btnGuardarRol_Click(object sender, EventArgs e)
        {
            try
            {
                // El nodo raíz del TreeView tiene como Tag el RolComposite raíz
                RolComposite raiz = treeViewRoles.Nodes[0].Tag as RolComposite;

                if (raiz == null || !raiz.Hijos().Any())
                {
                    MessageBox.Show("No hay nada para guardar.");
                    return;
                }
                bll_roles.GuardarArbol(raiz, txtDescripcion.Text);

                MessageBox.Show("Guardado correctamente.");

                RefrescarVistas();

                CargarComboRolesExistentes();

                Raiz();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }

        }
        public bool ExisteHijo(TreeNode nodoPadre, string nombre)
        {

            foreach (TreeNode hijo in nodoPadre.Nodes)
            {
                Componente comp = hijo.Tag as Componente;

                if (comp != null && comp.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (ExisteHijo(hijo, nombre))
                {
                    return true;
                }
            }

            return false;
        }

        private void btnAgregarPR_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewRoles.SelectedNode == null)
                {
                    MessageBox.Show("Seleccione un nodo");
                    return;
                }

                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese un nombre");
                    return;
                }
                string nombre = txtNombre.Text;

                Componente compSeleccionado = treeViewRoles.SelectedNode.Tag as Componente;

                if (!(compSeleccionado is RolComposite))
                {
                    MessageBox.Show("Solo se pueden agregar componentes dentro de un rol");
                    return;
                }

                TreeNode nodoSeleccionado = treeViewRoles.SelectedNode;

                TreeNode nodoActual = nodoSeleccionado;

                while (nodoActual != null)
                {
                    Componente comp = nodoActual.Tag as Componente;

                    if (comp != null && comp.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("No puede agregar un componente con el mismo nombre que un padre");
                        return;
                    }
                    if (ExisteHijo(nodoActual, nombre))
                    {
                        MessageBox.Show("Ya existe un Rol o Permiso con ese nombre en este nivel o subniveles");
                        return;
                    }
                    nodoActual = nodoActual.Parent;
                }

                Componente nuevoC = null;

                if (cboTipo.Text == "Rol")
                {
                    TreeNode raizBD = treeViewRolesC.Nodes.Count > 0 ? treeViewRolesC.Nodes[0] : null;
                    if (raizBD != null && ExisteRolEnArbol(raizBD, nombre))
                    {
                        MessageBox.Show("Ya existe un Rol con ese nombre en la base de datos.");
                        return;
                    }

                    nuevoC = new RolComposite
                    {
                        Nombre = nombre

                    };
                }

                else if (cboTipo.Text == "Permiso")
                {
                    if (treeViewRoles.SelectedNode.Parent == null)
                    {
                        MessageBox.Show("Un permiso debe estar dentro de un Rol, no directamente en la raíz.");
                        txtNombre.Clear();

                        return;
                    }

                    TreeNode raizBD = treeViewRolesC.Nodes.Count > 0 ? treeViewRolesC.Nodes[0] : null;

                    if (raizBD != null && ExistePermisoEnArbol(raizBD, nombre))
                    {
                        MessageBox.Show("Ya existe un Permiso con ese nombre en la base de datos.");
                        return;
                    }


                    nuevoC = new Permiso
                    {
                        Nombre = nombre
                    };
                }


                ((RolComposite)compSeleccionado).Agregar(nuevoC);

                TreeNode nodoNuevo = new TreeNode(nombre);
                nodoNuevo.Tag = nuevoC;

                treeViewRoles.SelectedNode.Nodes.Add(nodoNuevo);

                txtNombre.Clear();

                treeViewRoles.ExpandAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool ExistePermisoEnArbol(TreeNode nodo, string nombre)
        {
            foreach (TreeNode hijo in nodo.Nodes)
            {
                if (hijo.Tag is Permiso &&
                    hijo.Text.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                    return true;

                if (ExistePermisoEnArbol(hijo, nombre))
                    return true;
            }
            return false;
        }

        private bool ExisteRolEnArbol(TreeNode nodo, string nombre)
        {
            foreach (TreeNode hijo in nodo.Nodes)
            {
                if (hijo.Tag is RolComposite &&
                    hijo.Text.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                    return true;

                if (ExisteRolEnArbol(hijo, nombre))
                    return true;
            }
            return false;
        }

        private void CargarTreeView()
        {
            treeViewRolesC.Nodes.Clear();

            RolComposite raiz = bll_roles.CargarArbol();

            TreeNode nodoRaiz = new TreeNode(raiz.Nombre);
            nodoRaiz.Tag = raiz;

            TreeViewHelper.AgregarNodosRecursivo(raiz, nodoRaiz);

            treeViewRolesC.Nodes.Add(nodoRaiz);
            treeViewRolesC.ExpandAll();
        }


        private void CargarComboRolesExistentes()
        {
            cboRolesExistentes.Items.Clear();

            cboRolesExistentes.Text = "";

            if (treeViewRolesC.Nodes.Count == 0)
                return;

            RolComposite raiz = treeViewRolesC.Nodes[0].Tag as RolComposite;
            if (raiz == null)
                return;

            // Recorre todos los RolComposite del árbol y los agrega al combo

            AgregarRolesAlCombo(raiz);
        }

        private void AgregarRolesAlCombo(RolComposite rol)
        {

            foreach (var hijo in rol.Hijos())
            {
                // Solo hijos directos de la raíz, sin recursividad

                if (hijo is RolComposite subRol && !subRol.EsReferencia)
                    cboRolesExistentes.Items.Add(subRol);
            }
        }
        private void CargarPermisosAlCombo()
        {
            cboPermisos.Items.Clear();

            cboPermisos.Text = "";

            List<Permiso> listaPermisos = bll_roles.ObtenerPermisos();

            foreach (Permiso permisos in listaPermisos)
            {
                cboPermisos.Items.Add(permisos);
            }
            cboPermisos.DisplayMember = "Nombre";
            cboPermisos.ValueMember = "IdPermisos";
        }

        private void btnAgregarRolExistente_Click(object sender, EventArgs e)
        {
            try
            {
                //if (treeViewRoles.SelectedNode == null)
                //{
                //    MessageBox.Show("Seleccione un nodo destino.");
                //    return;
                //}

                if (cboRolesExistentes.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un rol existente.");
                    return;
                }

                //Componente compDestino = treeViewRoles.SelectedNode.Tag as Componente;

                //if (!(compDestino is RolComposite padreDestino))
                //{
                //    MessageBox.Show("Solo se puede agregar dentro de un rol.");
                //    return;
                //}

                RolComposite rolOrigen = cboRolesExistentes.SelectedItem as RolComposite;

                if (!ValidarNodoSeleccionado(out RolComposite padreDestino))
                    return;

                if (padreDestino.Id > 0 && rolOrigen.Id > 0 && ContieneRol(rolOrigen, padreDestino.Id))
                {
                    MessageBox.Show(
                        "No se puede agregar porque generaría una referencia circular.");
                    return;
                }


                // Validar que no sea el mismo nodo ni un ancestro
                if (EsAncestro(treeViewRoles.SelectedNode, rolOrigen.Nombre))
                {
                    MessageBox.Show("No puede agregar un rol que sea padre del nodo seleccionado.");
                    return;
                }





                // Validar que no exista ya ese nombre en el subárbol destino
                if (ExisteHijo(treeViewRoles.SelectedNode, rolOrigen.Nombre))
                {
                    MessageBox.Show("Ya existe ese rol en este nivel.");
                    return;
                }


                //AgregarComponenteAlArbol(ClonarRol(rolOrigen));




                // Clonar el rol con todos sus hijos (permisos incluidos)
                RolComposite clon = ClonarRol(rolOrigen);


                // Agregar al TreeView
                TreeNode nodoNuevo = new TreeNode(clon.Nombre) { Tag = clon };
                TreeViewHelper.AgregarNodosRecursivo(clon, nodoNuevo);
                treeViewRoles.SelectedNode.Nodes.Add(nodoNuevo);
                treeViewRoles.ExpandAll();

                padreDestino.Agregar(clon);

                //                MessageBox.Show(
                //    $"Original Hash: {rolOrigen.GetHashCode()}\n" +
                //    $"Clon Hash: {clon.GetHashCode()}"
                //);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ContieneRol(RolComposite rol, int idBuscado)
        {
            if (rol.Id == idBuscado)
                return true;

            foreach (var hijo in rol.Hijos())
            {
                if (hijo is RolComposite subRol)
                {
                    if (ContieneRol(subRol, idBuscado))
                        return true;
                }
            }

            return false;
        }

        private RolComposite ClonarRol(RolComposite original)
        {
            try
            {
                var clon = new RolComposite { Nombre = original.Nombre, Id = original.Id };

                foreach (var hijo in original.Hijos())
                {
                    if (hijo is RolComposite subRol)
                        clon.Agregar(ClonarRol(subRol));
                    else if (hijo is Permiso p)
                        clon.Agregar(new Permiso
                        {
                            Nombre = p.Nombre,
                            Id = p.Id,
                            IdPermiso = p.IdPermiso
                        });
                }

                return clon;
            }
            catch (Exception ex)
            {
                throw new Exception("Error ", ex);
            }

        }

        // Verifica que el nodo seleccionado no sea descendiente del rol que queremos agregar
        private bool EsAncestro(TreeNode nodo, string nombre)
        {
            TreeNode actual = nodo;
            while (actual != null)
            {
                if (actual.Text.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                    return true;
                actual = actual.Parent;
            }
            return false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboRolesExistentes.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un rol existente.");
                    return;
                }

                RolComposite raiz = treeViewRoles.Nodes[0].Tag as RolComposite;
                TreeNode nodoRaiz = treeViewRoles.Nodes[0];
                RolComposite rolNuevo = ClonarRol(cboRolesExistentes.SelectedItem as RolComposite);

                // Limpiar todo lo que haya bajo la raíz
                foreach (TreeNode n in nodoRaiz.Nodes)
                    raiz.Eliminar(n.Tag as Componente);
                nodoRaiz.Nodes.Clear();

                // Agregar solo el nuevo
                raiz.Agregar(rolNuevo);
                TreeNode nodoNuevo = new TreeNode(rolNuevo.Nombre) { Tag = rolNuevo };
                TreeViewHelper.AgregarNodosRecursivo(rolNuevo, nodoNuevo);
                nodoRaiz.Nodes.Add(nodoNuevo);

                treeViewRoles.SelectedNode = nodoNuevo;
                treeViewRoles.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (treeViewRoles.SelectedNode == null)
            {
                MessageBox.Show("Seleccione un nodo para eliminar.");
                return;
            }

            TreeNode nodoSeleccionado = treeViewRoles.SelectedNode;

            //Hacemos que no se elimine la raiz

            if (nodoSeleccionado.Parent == null)
            {
                MessageBox.Show("No se puede eliminar la raiz");
                return;
            }

            string tipo = nodoSeleccionado.Tag is RolComposite ? "Rol" : "Permiso";

            var confimacion = MessageBox.Show($"Deasignar {tipo} '{nodoSeleccionado.Text}'?", "Confirmar eliminacion", MessageBoxButtons.YesNo);

            if (confimacion != DialogResult.Yes)
                return;

            //Eliminar del composite padre
            try
            {
                TreeNode nodoPadre = nodoSeleccionado.Parent;
                RolComposite compPadre = nodoPadre.Tag as RolComposite;
                Componente compEliminar = nodoSeleccionado.Tag as Componente;

                // Solo desasignar si el nodo ya tiene ID (existe en BD)
                if (compEliminar.Id > 0 && compPadre != null)
                    bll_roles.EliminarComponente(compPadre, compEliminar);

                // Quitar del Composite en memoria
                compPadre?.Eliminar(compEliminar);

                // Quitar del TreeView
                nodoSeleccionado.Remove();

                // Refrescar combo por si se desasignó un rol
                RefrescarVistas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            RolComposite rolSeleccionado = cboRolesExistentes.SelectedItem as RolComposite;

            if (rolSeleccionado == null)
            {
                MessageBox.Show("Seleccione un rol existente.");
                return;
            }

            string tipo = rolSeleccionado is RolComposite ? "Rol" : "Permiso";

            //string advertencia = rolSeleccionado is RolComposite
            //    ? $"¿Eliminar completamente el Rol '{rolSeleccionado.Nombre}'?\nSe borrará de todas las relaciones."
            //    : $"¿Eliminar completamente el Permiso '{rolSeleccionado.Nombre}'?\nSe borrará de todos los roles.";

            //var confirmacion = MessageBox.Show(advertencia, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (!ConfirmarEliminacion($"¿Eliminar el Rol '{rolSeleccionado.Nombre}'?\nSe borrará de todas las relaciones."))
                return;

            //if (confirmacion != DialogResult.Yes)
            //    return;

            try
            {
                // Eliminar de la BD
                bll_roles.EliminarRol(rolSeleccionado.Id);

                RefrescarVistas();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }

        }

        private void btnAgregarPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewRoles.SelectedNode == null)
                {
                    MessageBox.Show("Seleccione un nodo destino.");
                    return;
                }

                Componente compDestino = treeViewRoles.SelectedNode.Tag as Componente;

                if (!(compDestino is RolComposite padreDestino))
                {
                    MessageBox.Show("Solo se puede agregar dentro de un rol.");
                    return;
                }



                if (cboPermisos.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un permiso existente.");
                    return;
                }
                //if (!ValidarNodoSeleccionado(out RolComposite padreDestino))
                //    return;

                Permiso permisoOrigen = cboPermisos.SelectedItem as Permiso;

                //Evitar duplicados dentro del mismo subárbol
                if (ExisteHijo(treeViewRoles.SelectedNode, permisoOrigen.Nombre))
                {
                    MessageBox.Show("Ya existe ese permiso en este rol.");
                    return;
                }

                // Crear copia del permiso
                Permiso clon = new Permiso
                {
                    Nombre = permisoOrigen.Nombre,
                    Id = permisoOrigen.Id,
                    IdPermiso = permisoOrigen.IdPermiso
                };


                //AgregarComponenteAlArbol(clon);


                padreDestino.Agregar(clon);

                TreeNode nodoNuevo = new TreeNode(clon.Nombre)
                {
                    Tag = clon
                };

                treeViewRoles.SelectedNode.Nodes.Add(nodoNuevo);
                treeViewRoles.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminarPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                Permiso permisoSeleccionado = cboPermisos.SelectedItem as Permiso;
                if (permisoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un permiso");
                    return;
                }

                //string advertencia = $"¿Eliminar completamente el Permiso '{permisoSeleccionado.Nombre}'?\nSe borrará de todos los roles.";

                //var confirmacion = MessageBox.Show(advertencia, "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //if (confirmacion != DialogResult.Yes)
                //    return;
                if (!ConfirmarEliminacion($"¿Eliminar el Permiso '{permisoSeleccionado.Nombre}'?\nSe borrará de todos los roles."))
                    return;

                bll_roles.EliminarPermiso(permisoSeleccionado.Id);

                RefrescarVistas();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void btnModificarPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                Permiso permisoSeleccionado = cboPermisos.SelectedItem as Permiso;
                if (permisoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un permiso");
                    return;
                }

                string nuevoNombre = Interaction.InputBox("Ingrese el nuevo nombre del permiso: ", "Modificar Permiso", permisoSeleccionado.Nombre);

                if (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    return;
                }

                bool existe = cboPermisos.Items.Cast<Permiso>().Any(p => p.Id != permisoSeleccionado.Id && p.Nombre.Equals(nuevoNombre, StringComparison.OrdinalIgnoreCase));

                if (existe)
                {
                    MessageBox.Show("Ya existe un permiso con ese nombre.");
                    return;
                }

                permisoSeleccionado.Nombre = nuevoNombre;

                bll_roles.ModificarPermiso(permisoSeleccionado.Id, permisoSeleccionado.Nombre);

                RefrescarVistas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al modificar permiso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Raiz();
        }

        private void btnGuardarRP_Click(object sender, EventArgs e)
        {

        }
    }
}
