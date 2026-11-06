using BE.Composite;
using Servicios;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Vista
{
    // Maneja un TreeView con checkboxes que muestra el catálogo de permisos:
    //   Formulario (familia)  -> tildado = acceso a la página completa
    //     └─ Botón (patente)  -> tildado solo = acceso únicamente a ese botón
    // Lo usan Gestión de Roles (permisos de un rol) y Asignación de Roles (permisos directos del usuario).
    public class ArbolPermisos
    {
        private readonly TreeView tree;
        private bool actualizando = false;

        // En solo lectura se muestran las marcas pero no se pueden cambiar (ej. rol Administrador).
        public bool SoloLectura { get; set; }

        public ArbolPermisos(TreeView treeView)
        {
            tree = treeView;
            tree.CheckBoxes = true;
            tree.BeforeCheck += Tree_BeforeCheck;
            tree.AfterCheck += Tree_AfterCheck;
        }

        public void Cargar(List<RolComposite> catalogo)
        {
            tree.BeginUpdate();
            tree.Nodes.Clear();

            foreach (var familia in catalogo)
            {
                TreeNode nodoFormulario = new TreeNode(TraduccionServicio.Instancia.TraducirComponente(familia)) { Tag = familia };

                foreach (var permiso in familia.Hijos().OfType<Permiso>().OrderBy(p => p.Nombre.EndsWith(".Ver") ? 0 : 1))
                    nodoFormulario.Nodes.Add(new TreeNode(TraduccionServicio.Instancia.TraducirComponente(permiso)) { Tag = permiso });

                tree.Nodes.Add(nodoFormulario);
            }

            tree.EndUpdate();
        }

        // Vuelve a poner los textos en el idioma actual (al cambiar de idioma)
        public void RefrescarTextos()
        {
            foreach (TreeNode nodoFormulario in tree.Nodes)
            {
                nodoFormulario.Text = TraduccionServicio.Instancia.TraducirComponente((Componente)nodoFormulario.Tag);
                foreach (TreeNode nodoPermiso in nodoFormulario.Nodes)
                    nodoPermiso.Text = TraduccionServicio.Instancia.TraducirComponente((Componente)nodoPermiso.Tag);
            }
        }

        // Marca las familias completas y las patentes sueltas indicadas; desmarca el resto.
        // permisosEfectivos (opcional): todo lo que el usuario puede hacer (roles + directos). Lo
        // que tiene por ahi pero NO esta tildado aca (es decir, lo tiene por un rol y no por una
        // asignacion directa) se pinta en gris, para no confundir "no esta tildado" con "no se
        // autocompleto": ver groupBoxPermisosDirectos en frmUsuarioRoles.
        public void Marcar(IEnumerable<int> idsFormularios, IEnumerable<int> idsPermisos, RolComposite permisosEfectivos = null)
        {
            var formularios = new HashSet<int>(idsFormularios);
            var permisos = new HashSet<int>(idsPermisos);

            actualizando = true;
            try
            {
                foreach (TreeNode nodoFormulario in tree.Nodes)
                {
                    RolComposite familia = (RolComposite)nodoFormulario.Tag;
                    bool completo = formularios.Contains(familia.Id);
                    nodoFormulario.Checked = completo;
                    nodoFormulario.ForeColor = (!completo && permisosEfectivos != null && permisosEfectivos.TienePermiso($"{familia.Formulario}.Ver"))
                        ? Color.Gray : Color.Empty;

                    foreach (TreeNode nodoPermiso in nodoFormulario.Nodes)
                    {
                        Permiso permiso = (Permiso)nodoPermiso.Tag;
                        bool tildado = completo || permisos.Contains(permiso.Id);
                        nodoPermiso.Checked = tildado;
                        nodoPermiso.ForeColor = (!tildado && permisosEfectivos != null && permisosEfectivos.TienePermiso(permiso.Nombre))
                            ? Color.Gray : Color.Empty;
                    }

                    // Se expanden solo los formularios con algún botón suelto, para que se vea cuál es
                    if (!completo && nodoFormulario.Nodes.Cast<TreeNode>().Any(n => n.Checked))
                        nodoFormulario.Expand();
                    else
                        nodoFormulario.Collapse();
                }
            }
            finally
            {
                actualizando = false;
            }
        }

        // Formulario tildado => familia completa (se ignoran sus botones).
        // Formulario sin tildar => cada botón tildado es una patente suelta.
        public void ObtenerSeleccion(out List<int> idsFormularios, out List<int> idsPermisos)
        {
            idsFormularios = new List<int>();
            idsPermisos = new List<int>();

            foreach (TreeNode nodoFormulario in tree.Nodes)
            {
                if (nodoFormulario.Checked)
                {
                    idsFormularios.Add(((RolComposite)nodoFormulario.Tag).Id);
                    continue;
                }

                foreach (TreeNode nodoPermiso in nodoFormulario.Nodes)
                    if (nodoPermiso.Checked)
                        idsPermisos.Add(((Permiso)nodoPermiso.Tag).Id);
            }
        }

        private void Tree_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (SoloLectura && !actualizando)
                e.Cancel = true;
        }

        private void Tree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (actualizando)
                return;

            actualizando = true;
            try
            {
                if (e.Node.Parent == null)
                {
                    // Formulario: tildar/destildar la página completa arrastra todos sus botones
                    foreach (TreeNode hijo in e.Node.Nodes)
                        hijo.Checked = e.Node.Checked;
                }
                else
                {
                    // Botón: si se destilda uno, el formulario deja de ser "página completa";
                    // si quedan todos tildados, pasa a ser la página completa.
                    TreeNode padre = e.Node.Parent;
                    padre.Checked = padre.Nodes.Cast<TreeNode>().All(n => n.Checked);
                }
            }
            finally
            {
                actualizando = false;
            }
        }
    }
}
