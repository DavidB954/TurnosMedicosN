using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_Conexion
    {
        // La cadena de conexion se lee del App.config de la aplicacion (clave "GestionTurnosMedicos"),
        // asi el instalador puede apuntarla a otro servidor sin recompilar. Si no esta configurada se
        // usa la instancia local de SQL Server Express.
        private const string ConexionPorDefecto =
            @"Data Source=localhost\SQLEXPRESS; Initial Catalog=GestionTurnosMedicos; Integrated Security=True";

        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["GestionTurnosMedicos"]?.ConnectionString ?? ConexionPorDefecto;

        // T06b: deja el usuario de la aplicacion en el contexto de la sesion de SQL Server para que el
        // trigger TR_Turno_Historial lo grabe en Turno_Historial.IdUsuarioCambio. Se llama en la MISMA
        // conexion (y transaccion) del INSERT/UPDATE, justo antes. null = cambio sin usuario identificado.
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
