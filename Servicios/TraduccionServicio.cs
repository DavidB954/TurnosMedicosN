using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.BE_Traduccion;

namespace Servicios
{
    public class TraduccionServicio
    {
        private static TraduccionServicio _instancia;
        private Dictionary<string, string> _diccionario;

        public Idioma IdiomaActual { get; private set; }

        public static TraduccionServicio Instancia =>
            _instancia ?? (_instancia = new TraduccionServicio());
        public event Action<Idioma> IdiomaChanged;
        public event Action IdiomasChanged;

        public void CambiarIdioma(Idioma idioma, Dictionary<string, string> diccionario)
        {
            IdiomaActual = idioma;
            _diccionario = diccionario;
            IdiomaChanged?.Invoke(idioma);
        }

        public void NotificarCambioIdiomas()
        {
            IdiomasChanged?.Invoke();
        }
        public string Traducir(string clave)
        {
            if (_diccionario == null) return $"[{clave}]";
            return _diccionario.TryGetValue(clave, out var texto) ? texto : $"[{clave}]";
        }
        public static void ResetearInstancia()
        {
            _instancia = null;
        }
    }
}