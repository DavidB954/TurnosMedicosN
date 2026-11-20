using Servicios;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Vista
{
    public static class Errores
    {
        public static void Mostrar(Exception ex)
        {
            RegistroErrores.Registrar(ex, Origen());
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static string Origen()
        {
            var metodo = new StackTrace().GetFrame(2)?.GetMethod();
            return metodo == null ? "Vista" : metodo.DeclaringType?.Name + "." + metodo.Name;
        }
    }
}
