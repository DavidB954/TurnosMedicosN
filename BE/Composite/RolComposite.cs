using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BE.Composite
{
    public class RolComposite : Componente
    {
        public List<Componente> hijos = new List<Componente>();
        public override string ToString() => Nombre;
        public string Descripcion { get; set; }
        public bool EsReferencia
        {
            get;
            set;
        }

        // Si tiene Formulario es una "familia de formulario" (agrupa todas las patentes
        // de esa pantalla); si es null es un rol común.
        public string Formulario { get; set; }
        public bool EsFormulario => !string.IsNullOrEmpty(Formulario);

        public void Agregar(Componente componente) => hijos.Add(componente);

        public void Modificar(Componente compViejo, Componente compNuevo)
        {
            int index = hijos.IndexOf(compViejo);
            if (index != -1)
            {
                hijos[index] = compNuevo;
            }
        }

        public void Eliminar(Componente componente) => hijos.Remove(componente);

        public override bool TienePermiso(string Permiso)
        {
            foreach (var hijo in hijos)
            {
                if (hijo.TienePermiso(Permiso))
                {
                    return true;
                }
            }
            return false;
        }

        public IReadOnlyList<Componente> Hijos() => hijos;
    }
}
