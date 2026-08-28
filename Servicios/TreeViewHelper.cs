using BE.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicios
{
    public static class TreeViewHelper
    {
        public static void AgregarNodosRecursivo(RolComposite rolPadre, TreeNode nodoPadre)
        {
            foreach (var hijo in rolPadre.Hijos())
            {
                TreeNode nodoHijo = new TreeNode(hijo.Nombre);
                nodoHijo.Tag = hijo;

                nodoPadre.Nodes.Add(nodoHijo);

                // Si es un rol, seguir bajando
                if (hijo is RolComposite subRol)
                    AgregarNodosRecursivo(subRol, nodoHijo);
            }
        }

    }
}
