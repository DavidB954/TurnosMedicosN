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
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            try
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
            catch (Exception ex)
            {
                Exception causa = ex;
                while (causa != null && !(causa is System.Data.SqlClient.SqlException))
                    causa = causa.InnerException;
                if (causa != null)
                {
                    Servicios.RegistroErrores.Registrar(ex, "Conexion con la base de datos");
                    MessageBox.Show("No se pudo conectar con la base de datos del sistema.\n\n" +
                        "Verifique que la instalacion haya creado la base: el detalle esta en el archivo " +
                        "GestionTurnos_instalacion.log de la carpeta del programa. Para crearla a mano, siga el " +
                        "Manual de instalacion (seccion Creacion manual de la base de datos).\n\nDetalle: " + causa.Message,
                        "Base de datos no disponible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MostrarError(ex);
                }
            }
        }

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
            Servicios.RegistroErrores.Registrar(ex, "Error no controlado");
            string mensaje = ex != null ? ex.Message : "Ocurrio un error inesperado.";
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
