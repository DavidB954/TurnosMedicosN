using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // Datos propios del medico (tabla Medico, 1:1 con Usuario): matricula profesional.
    public class DAL_Medico
    {
        DAL_Conexion conexion = new DAL_Conexion();

        public string ObtenerMatricula(int idMedico)
        {
            try
            {
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Matricula FROM Medico WHERE IdUsuario = @idMedico", cn);
                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                    return cmd.ExecuteScalar() as string;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Alta o modificacion de la matricula.
        public void GuardarMatricula(int idMedico, string matricula)
        {
            try
            {
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        IF EXISTS (SELECT 1 FROM Medico WHERE IdUsuario = @idMedico)
                            UPDATE Medico SET Matricula = @matricula WHERE IdUsuario = @idMedico;
                        ELSE
                            INSERT INTO Medico (IdUsuario, Matricula) VALUES (@idMedico, @matricula);", cn);
                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                    cmd.Parameters.Add("@matricula", SqlDbType.VarChar, 20).Value = matricula;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
