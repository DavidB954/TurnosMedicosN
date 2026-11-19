using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // Pacientes (tabla Paciente). El paciente no es usuario del sistema: lo registra el recepcionista.
    public class DAL_Paciente
    {
        DAL_Conexion conex = new DAL_Conexion();

        public BE_Paciente ObtenerPorDni(string dni)
        {
            try
            {
                using (SqlConnection cn = conex.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT IdPaciente, DNI, Nombre, Apellido, FechaNacimiento, Telefono, Email,
                               NroHistoriaClinica, FechaAperturaHC
                        FROM Paciente WHERE DNI = @dni", cn);
                    cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = dni;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        if (!lector.Read())
                            return null;
                        return Mapear(lector);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // true si el DNI ya lo tiene OTRO paciente (se excluye al propio paciente al modificar).
        public bool ExisteDNI(string dni, int idPacienteExcluido)
        {
            try
            {
                using (SqlConnection cn = conex.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(1) FROM Paciente WHERE DNI = @dni AND IdPaciente <> @idPaciente", cn);
                    cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = dni;
                    cmd.Parameters.Add("@idPaciente", SqlDbType.Int).Value = idPacienteExcluido;
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Registra al paciente y abre su Historia Clinica: el N° de HC y la fecha de apertura los
        // genera la base (SEQ_NroHistoriaClinica y GETDATE) y se devuelven en el mismo objeto.
        public void Insertar(BE_Paciente paciente)
        {
            try
            {
                using (SqlConnection cn = conex.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO Paciente (DNI, Nombre, Apellido, FechaNacimiento, Telefono, Email)
                        OUTPUT INSERTED.IdPaciente, INSERTED.NroHistoriaClinica, INSERTED.FechaAperturaHC
                        VALUES (@dni, @nombre, @apellido, @fechaNacimiento, @telefono, @email)", cn);
                    cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = paciente.DNI;
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = paciente.Nombre;
                    cmd.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = paciente.Apellido;
                    cmd.Parameters.Add("@fechaNacimiento", SqlDbType.Date).Value = (object)paciente.FechaNacimiento ?? DBNull.Value;
                    cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 30).Value = (object)paciente.Telefono ?? DBNull.Value;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = (object)paciente.Email ?? DBNull.Value;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        lector.Read();
                        paciente.IdPaciente = lector.GetInt32(0);
                        paciente.NroHistoriaClinica = lector.GetInt32(1);
                        paciente.FechaAperturaHC = lector.GetDateTime(2);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private BE_Paciente Mapear(SqlDataReader lector)
        {
            return new BE_Paciente
            {
                IdPaciente = Convert.ToInt32(lector["IdPaciente"]),
                DNI = lector["DNI"].ToString(),
                Nombre = lector["Nombre"].ToString(),
                Apellido = lector["Apellido"].ToString(),
                FechaNacimiento = lector["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(lector["FechaNacimiento"]),
                Telefono = lector["Telefono"] as string,
                Email = lector["Email"] as string,
                NroHistoriaClinica = Convert.ToInt32(lector["NroHistoriaClinica"]),
                FechaAperturaHC = Convert.ToDateTime(lector["FechaAperturaHC"])
            };
        }
    }
}
