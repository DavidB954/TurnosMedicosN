using BE;
using DAL;
using Servicios;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace BLL
{
    // Datos de paciente (DNI, fecha de nacimiento, telefono) que se cargan desde ABM de Usuarios.
    // Al guardarlos por primera vez se abre la Historia Clinica (N° de HC generado por la base).
    public class BLL_Paciente
    {
        DAL_Paciente dal_paciente = new DAL_Paciente();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();

        public BE_Paciente ObtenerPorUsuario(int idUsuario)
        {
            try
            {
                return dal_paciente.ObtenerPorUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudieron obtener los datos del paciente: " + ex.Message, ex);
            }
        }

        // Valida formato y que el DNI no lo tenga otro paciente. Se llama ANTES de crear el
        // usuario, para no dejar un usuario creado con datos de paciente invalidos.
        public void ValidarDatosPaciente(BE_Paciente paciente)
        {
            if (paciente == null)
                throw new ArgumentException("Faltan los datos del paciente.");

            paciente.DNI = (paciente.DNI ?? "").Trim().Replace(".", "");
            paciente.Telefono = string.IsNullOrWhiteSpace(paciente.Telefono) ? null : paciente.Telefono.Trim();

            if (paciente.DNI.Length < 7 || paciente.DNI.Length > 8 || !paciente.DNI.All(char.IsDigit))
                throw new ArgumentException("El DNI debe tener 7 u 8 numeros.");

            if (paciente.FechaNacimiento.HasValue)
            {
                if (paciente.FechaNacimiento.Value.Date > DateTime.Today)
                    throw new ArgumentException("La fecha de nacimiento no puede ser futura.");
                if (paciente.FechaNacimiento.Value.Year < 1900)
                    throw new ArgumentException("La fecha de nacimiento no es valida.");
            }

            if (paciente.Telefono != null)
            {
                if (paciente.Telefono.Length > 30)
                    throw new ArgumentException("El telefono no puede superar los 30 caracteres.");
                if (!paciente.Telefono.All(c => char.IsDigit(c) || c == ' ' || c == '-' || c == '+' || c == '(' || c == ')'))
                    throw new ArgumentException("El telefono solo puede tener numeros, espacios y los signos + - ( ).");
            }

            try
            {
                if (dal_paciente.ExisteDNI(paciente.DNI, paciente.IdUsuario))
                    throw new ArgumentException($"Ya existe otro paciente con el DNI {paciente.DNI}.");
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo validar el DNI: " + ex.Message, ex);
            }
        }

        // Alta (abre la HC) o modificacion de los datos del paciente.
        public void GuardarDatosPaciente(BE_Paciente paciente)
        {
            ValidarDatosPaciente(paciente);

            int? idEditor = Sesion.Instancia().UsuarioActual?.IdUsuario;
            try
            {
                BE_Paciente existente = dal_paciente.ObtenerPorUsuario(paciente.IdUsuario);
                if (existente == null)
                {
                    dal_paciente.Insertar(paciente);
                    bll_bitacora.RegistrarEvento(idEditor, AccionBitacora.PACIENTE_ALTA, "USUARIO",
                        $"Historia Clinica N° {paciente.NroHistoriaClinica} abierta para el usuario {paciente.IdUsuario}.");
                }
                else
                {
                    paciente.NroHistoriaClinica = existente.NroHistoriaClinica;
                    paciente.FechaAperturaHC = existente.FechaAperturaHC;

                    bool cambio = existente.DNI != paciente.DNI
                        || existente.FechaNacimiento?.Date != paciente.FechaNacimiento?.Date
                        || (existente.Telefono ?? "") != (paciente.Telefono ?? "");
                    if (!cambio)
                        return;

                    dal_paciente.Modificar(paciente);
                    bll_bitacora.RegistrarEvento(idEditor, AccionBitacora.PACIENTE_MODIFICACION, "USUARIO",
                        $"Datos de paciente modificados para el usuario {paciente.IdUsuario} (HC {paciente.NroHistoriaClinica}).");
                }
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new ArgumentException($"Ya existe otro paciente con el DNI {paciente.DNI}.");
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudieron guardar los datos del paciente: " + ex.Message, ex);
            }
        }
    }
}
