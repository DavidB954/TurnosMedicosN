using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class BLL_DVV
    {
        DAL_DVV dal_dvv = new DAL_DVV();
        DAL_Usuario dal_usuario = new DAL_Usuario();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();
        public void ActualizarDVV(string NombreTabla)
        {
            try
            {
                string concatenacion = CalcularDVV();

                dal_dvv.ActualizarDVV(concatenacion, NombreTabla);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar DVV: {ex.Message}", ex);
            }
            
        }

        public bool VerificarIntegridad(string nombreTabla)
        {
            try
            {
                string dvvAlmacenado = dal_dvv.ObtenerDVV(nombreTabla);

                string dvvRecalculado = CalcularDVV();

            //    MessageBox.Show(
            //$"DVV almacenado:\n{dvvAlmacenado}\n\nDVV recalculado:\n{dvvRecalculado}");

                bool integro = dvvAlmacenado == dvvRecalculado;

                if (!integro)
                {
                    // Se corre antes del login (arranque de la app), por eso IdUsuario es null.
                    bll_bitacora.RegistrarEvento(
                        null,
                        AccionBitacora.INTEGRIDAD_ERROR,
                        "SEGURIDAD",
                        $"Fallo de integridad del DVV en la tabla '{nombreTabla}' detectado al iniciar la aplicación."
                    );
                }

                return integro;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar integridad: {ex.Message}", ex);
            }
           

        }

        public string CalcularDVV()
        {
            try
            {
                List<BE_Usuario> ListaUsuario = dal_usuario.ListaUsuario();

                StringBuilder concatenacion = new StringBuilder();

                foreach (var usuario in ListaUsuario)
                {
                    string activo = usuario.Activo ? "1" : "0";

                    string dvhRecalculado = HashHelper.GenerarHash(
                        $"{usuario.Nombre}|{usuario.Apellido}|{usuario.Email}|{usuario.HashPassword}|{usuario.IntentosFallidos}|{activo}"
                    );
                    concatenacion.Append(dvhRecalculado);
                }

                return HashHelper.GenerarHash(concatenacion.ToString());
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al calcular DVV: {ex.Message}", ex);
            }
           
        }

        public void GenerarBackUp(string rutaBackup)
        {
            try
            {
                dal_dvv.GenerarBackUp(rutaBackup);

                bll_bitacora.RegistrarEvento(
                    Sesion.Instancia().UsuarioActual?.IdUsuario,
                    AccionBitacora.BACKUP_GENERADO,
                    "SEGURIDAD",
                    $"Se generó un backup de la base de datos en: {rutaBackup}"
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar backup: {ex.Message}", ex);
            }

        }

        public void RestaurarBackup(string rutaBackup)
        {
            try
            {
                dal_dvv.RestaurarBackup(rutaBackup);

                bll_bitacora.RegistrarEvento(
                    Sesion.Instancia().UsuarioActual?.IdUsuario,
                    AccionBitacora.BACKUP_RESTAURADO,
                    "SEGURIDAD",
                    $"Se restauró la base de datos desde el backup: {rutaBackup}"
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al restaurar backup: {ex.Message}", ex);
            }

        }
    }
}
