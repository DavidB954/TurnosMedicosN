using BE;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // Datos propios del paciente (tabla Paciente, 1:1 con Usuario). Se cargan desde ABM de Usuarios.
    public class DAL_Paciente
    {
        DAL_Conexion conexion = new DAL_Conexion();

        public BE_Paciente ObtenerPorUsuario(int idUsuario)
        {
            try
            {
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT IdUsuario, DNI, NroHistoriaClinica, FechaNacimiento, Telefono, FechaAperturaHC
                        FROM Paciente WHERE IdUsuario = @idUsuario", cn);
                    cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        if (!lector.Read())
                            return null;

                        return new BE_Paciente
                        {
                            IdUsuario = Convert.ToInt32(lector["IdUsuario"]),
                            DNI = lector["DNI"].ToString(),
                            NroHistoriaClinica = Convert.ToInt32(lector["NroHistoriaClinica"]),
                            FechaNacimiento = lector["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(lector["FechaNacimiento"]),
                            Telefono = lector["Telefono"] as string,
                            FechaAperturaHC = Convert.ToDateTime(lector["FechaAperturaHC"])
                        };
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // true si el DNI ya lo tiene OTRO paciente (se excluye al propio usuario al modificar).
        public bool ExisteDNI(string dni, int idUsuarioExcluido)
        {
            try
            {
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(1) FROM Paciente WHERE DNI = @dni AND IdUsuario <> @idUsuario", cn);
                    cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = dni;
                    cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuarioExcluido;
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Abre la Historia Clinica: el N° de HC lo genera la base (SEQ_NroHistoriaClinica).
        public void Insertar(BE_Paciente paciente)
        {
            try
            {
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO Paciente (IdUsuario, DNI, FechaNacimiento, Telefono)
                        OUTPUT INSERTED.NroHistoriaClinica, INSERTED.FechaAperturaHC
                        VALUES (@idUsuario, @dni, @fechaNacimiento, @telefono)", cn);
                    AgregarParametros(cmd, paciente);

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        lector.Read();
                        paciente.NroHistoriaClinica = lector.GetInt32(0);
                        paciente.FechaAperturaHC = lector.GetDateTime(1);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // El N° de HC y la fecha de apertura no se modifican nunca.
        public void Modificar(BE_Paciente paciente)
        {
            try
            {
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        UPDATE Paciente SET DNI = @dni, FechaNacimiento = @fechaNacimiento, Telefono = @telefono
                        WHERE IdUsuario = @idUsuario", cn);
                    AgregarParametros(cmd, paciente);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AgregarParametros(SqlCommand cmd, BE_Paciente paciente)
        {
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = paciente.IdUsuario;
            cmd.Parameters.Add("@dni", SqlDbType.VarChar, 10).Value = paciente.DNI;
            cmd.Parameters.Add("@fechaNacimiento", SqlDbType.Date).Value = (object)paciente.FechaNacimiento ?? DBNull.Value;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 30).Value = (object)paciente.Telefono ?? DBNull.Value;
        }
    }
}
