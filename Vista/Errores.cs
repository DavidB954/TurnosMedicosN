using Servicios;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Vista
{
    // Punto unico de manejo de errores inesperados de las pantallas: los registra serializados (A03, RegistroErrores)
    // y los informa al usuario sin cerrar el sistema.
    public static class Errores
    {
        public static void Mostrar(Exception ex)
        {
            RegistroErrores.Registrar(ex, Origen());
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Pantalla y metodo que llamaron a Mostrar (por ejemplo "frmGestionTurnos.btnRegistrarTurno_Click").
        private static string Origen()
        {
            var metodo = new StackTrace().GetFrame(2)?.GetMethod();
            return metodo == null ? "Vista" : metodo.DeclaringType?.Name + "." + metodo.Name;
        }
    }
}
