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
               Application.Run(new frmLogin());
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
