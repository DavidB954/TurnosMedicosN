using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_Conexion
    {
        private const string ConexionPorDefecto =
            @"Data Source=localhost\SQLEXPRESS; Initial Catalog=GestionTurnosMedicos; Integrated Security=True";

        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["GestionTurnosMedicos"]?.ConnectionString ?? ConexionPorDefecto;

        public static void EstablecerUsuarioAuditoria(SqlConnection conexion, SqlTransaction transaccion, int? idUsuario)
        {
            SqlCommand cmd = new SqlCommand("EXEC sp_set_session_context @key = N'IdUsuario', @value = @idUsuario;", conexion, transaccion);
            cmd.Parameters.Add("@idUsuario", System.Data.SqlDbType.Int).Value = (object)idUsuario ?? System.DBNull.Value;
            cmd.ExecuteNonQuery();
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
