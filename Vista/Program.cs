using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    internal static class Program
    {
       
        [STAThread]
        static void Main()
        {
          
         
            BLL_DVV bll_dvv = new BLL_DVV();
            bool integra = bll_dvv.VerificarIntegridad("Usuario");
            if (!integra)
            {
                Application.Run(new frmBaseCorrupta());
            }
            else
            {
               VerificarIntegridadHistoriaClinica();
               Application.Run(new frmLogin());
            }
        }

        // DVH/DVV de AtencionMedica (Ley 26.529: la Historia Clinica es inalterable). Si se detecta
        // una alteracion se avisa y queda registrada en la Bitacora; la app sigue funcionando
        // porque el DVV de Usuario (acceso al sistema) esta integro.
        private static void VerificarIntegridadHistoriaClinica()
        {
            try
            {
                var corruptas = new BLL_Atencion().VerificarIntegridadAtenciones();
                if (corruptas.Count == 0)
                    return;

                string detalle = corruptas.Contains(-1)
                    ? "Se agregaron o eliminaron atenciones fuera del sistema (DVV de AtencionMedica)."
                    : "Atenciones alteradas (Id): " + string.Join(", ", corruptas);
                MessageBox.Show("ATENCION: se detecto una alteracion en la Historia Clinica.\n" + detalle +
                    "\nEl evento quedo registrado en la Bitacora.", "Integridad de la Historia Clinica",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Integridad de la Historia Clinica",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MostrarError(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MostrarError(e.ExceptionObject as Exception);
        }

        private static void MostrarError(Exception ex)
        {
            string mensaje = ex != null ? ex.Message : "Ocurrio un error inesperado.";
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
