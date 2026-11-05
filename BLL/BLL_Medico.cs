using BE;
using DAL;
using Servicios;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace BLL
{
    // Matricula profesional del medico: se carga en Gestion de Medicos y se imprime en la receta.
    public class BLL_Medico
    {
        DAL_Medico dal_medico = new DAL_Medico();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();

        public string ObtenerMatricula(int idMedico)
        {
            try
            {
                return dal_medico.ObtenerMatricula(idMedico);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener la matricula: " + ex.Message, ex);
            }
        }

        public void GuardarMatricula(int idMedico, string matricula)
        {
            matricula = (matricula ?? "").Trim().ToUpper();

            if (matricula.Length == 0)
                throw new ArgumentException("Ingrese la matricula del medico (por ejemplo: MN 123456).");
            if (matricula.Length > 20)
                throw new ArgumentException("La matricula no puede superar los 20 caracteres.");
            if (!matricula.All(c => char.IsLetterOrDigit(c) || c == ' ' || c == '-' || c == '.' || c == '/'))
                throw new ArgumentException("La matricula solo puede tener letras, numeros, espacios y los signos - . /");
            if (!matricula.Any(char.IsDigit))
                throw new ArgumentException("La matricula debe tener al menos un numero.");

            string anterior;
            try
            {
                anterior = dal_medico.ObtenerMatricula(idMedico);
                if (anterior == matricula)
                    return;

                dal_medico.GuardarMatricula(idMedico, matricula);
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new ArgumentException($"La matricula {matricula} ya la tiene otro medico.");
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo guardar la matricula: " + ex.Message, ex);
            }

            bll_bitacora.RegistrarEvento(Sesion.Instancia().UsuarioActual?.IdUsuario, AccionBitacora.MEDICO_MATRICULA_MODIFICADA, "MEDICOS",
                $"Matricula del medico {idMedico}: '{anterior ?? "(sin matricula)"}' -> '{matricula}'.");
        }
    }
}
