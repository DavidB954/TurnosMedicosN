using System;
using BE.Composite;
using Servicios;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Vista
{
    public class ArbolPermisos
    {
        private readonly TreeView tree;
        private bool actualizando = false;

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

        public void RefrescarTextos()
        {
            foreach (TreeNode nodoFormulario in tree.Nodes)
            {
                nodoFormulario.Text = TraduccionServicio.Instancia.TraducirComponente((Componente)nodoFormulario.Tag);
                foreach (TreeNode nodoPermiso in nodoFormulario.Nodes)
                    nodoPermiso.Text = TraduccionServicio.Instancia.TraducirComponente((Componente)nodoPermiso.Tag);
            }
        }

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
            try
            {
                if (SoloLectura && !actualizando)
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
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
                    foreach (TreeNode hijo in e.Node.Nodes)
                        hijo.Checked = e.Node.Checked;
                }
                else
                {
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
