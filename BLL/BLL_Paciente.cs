using BE;
using DAL;
using Servicios;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace BLL
{
    public class BLL_Paciente
    {
        DAL_Paciente dal_paciente = new DAL_Paciente();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();

        public BE_Paciente BuscarPorDni(string dni)
        {
            string normalizado = NormalizarDni(dni);
            if (!DniValido(normalizado))
                throw new ArgumentException("El DNI debe tener 7 u 8 números.");

            try
            {
                return dal_paciente.ObtenerPorDni(normalizado);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar al paciente: " + ex.Message, ex);
            }
        }

        public void ValidarDatosPaciente(BE_Paciente paciente)
        {
            if (paciente == null)
                throw new ArgumentException("Faltan los datos del paciente.");

            paciente.DNI = NormalizarDni(paciente.DNI);
            paciente.Nombre = (paciente.Nombre ?? "").Trim();
            paciente.Apellido = (paciente.Apellido ?? "").Trim();
            paciente.Telefono = string.IsNullOrWhiteSpace(paciente.Telefono) ? null : paciente.Telefono.Trim();
            paciente.Email = string.IsNullOrWhiteSpace(paciente.Email) ? null : paciente.Email.Trim();

            if (!DniValido(paciente.DNI))
                throw new ArgumentException("El DNI debe tener 7 u 8 números.");
            if (paciente.Nombre.Length == 0 || paciente.Apellido.Length == 0)
                throw new ArgumentException("El nombre y el apellido del paciente son obligatorios.");
            if (paciente.Nombre.Length > 50 || paciente.Apellido.Length > 50)
                throw new ArgumentException("El nombre y el apellido no pueden superar los 50 caracteres.");

            if (paciente.FechaNacimiento.HasValue)
            {
                if (paciente.FechaNacimiento.Value.Date > DateTime.Today)
                    throw new ArgumentException("La fecha de nacimiento no puede ser futura.");
                if (paciente.FechaNacimiento.Value.Year < 1900)
                    throw new ArgumentException("La fecha de nacimiento no es válida.");
            }

            if (paciente.Telefono != null)
            {
                if (paciente.Telefono.Length > 30)
                    throw new ArgumentException("El teléfono no puede superar los 30 caracteres.");
                if (!paciente.Telefono.All(c => char.IsDigit(c) || c == ' ' || c == '-' || c == '+' || c == '(' || c == ')'))
                    throw new ArgumentException("El teléfono solo puede tener números, espacios y los signos + - ( ).");
            }

            if (paciente.Email != null)
            {
                if (paciente.Email.Length > 100 || !Regex.IsMatch(paciente.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    throw new ArgumentException("El email no es válido.");
            }

            try
            {
                if (dal_paciente.ExisteDNI(paciente.DNI, paciente.IdPaciente))
                    throw new ArgumentException($"Ya existe un paciente con el DNI {paciente.DNI}.");
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

        public void RegistrarPaciente(BE_Paciente paciente)
        {
            ValidarDatosPaciente(paciente);

            try
            {
                dal_paciente.Insertar(paciente);
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new ArgumentException($"Ya existe un paciente con el DNI {paciente.DNI}.");
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar al paciente: " + ex.Message, ex);
            }

            int? idUsuario = Sesion.Instancia().UsuarioActual?.IdUsuario;
            bll_bitacora.RegistrarEvento(idUsuario, AccionBitacora.PACIENTE_ALTA, "TURNOS",
                $"Paciente {paciente.NombreApellido} (DNI {paciente.DNI}) registrado. Historia Clinica N° {paciente.NroHistoriaClinica}.");
        }

        private static string NormalizarDni(string dni)
        {
            return (dni ?? "").Trim().Replace(".", "").Replace(" ", "");
        }

        private static bool DniValido(string dni)
        {
            return dni.Length >= 7 && dni.Length <= 8 && dni.All(char.IsDigit);
        }
    }
}
