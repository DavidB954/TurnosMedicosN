using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Conexion
    {
    //    private static readonly string ConnectionString = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=GestionTurnosMedicos; Integrated Security=True";
        private static readonly string ConnectionString = "Data Source= .; Initial Catalog=GestionTurnosMedicos; Integrated Security=True";
        // T06.B: deja el usuario de la aplicacion en la sesion de SQL para que los triggers de
        // Turno lo graben en BitacoraEventosTurno.IdUsuarioCambio. Se llama en la MISMA conexion
        // (y transaccion) del INSERT/UPDATE, justo antes. null = proceso automatico.
        public static void EstablecerUsuarioAuditoria(SqlConnection conexion, SqlTransaction transaccion, int? idUsuario)
        {
            SqlCommand cmd = new SqlCommand("EXEC sp_set_session_context @key = N'IdUsuario', @value = @idUsuario;", conexion, transaccion);
            cmd.Parameters.Add("@idUsuario", System.Data.SqlDbType.Int).Value = (object)idUsuario ?? DBNull.Value;
            cmd.ExecuteNonQuery();
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(ConnectionString);        
        }

    }
}
