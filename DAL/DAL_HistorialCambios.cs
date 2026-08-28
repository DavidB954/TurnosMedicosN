using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_HistorialCambios
    {
        DAL_Conexion conex = new DAL_Conexion();

        public void RegistrarCambio(string tabla, int idRegistro, string campo, string valorAnterior, string valorNuevo, int idUsuario, string accion)
        {
            List<BE_CambioCampo> cambios = new List<BE_CambioCampo>
            {
                new BE_CambioCampo { Campo = campo, ValorAnterior = valorAnterior, ValorNuevo = valorNuevo }
            };

            RegistrarCambios(tabla, idRegistro, cambios, idUsuario, accion);
        }

        // Inserta todos los campos modificados de una misma edición como UNA sola
        // transacción: comparten IdTransaccion y FechaCambio, y si falla un insert
        // se revierte todo (no quedan cambios registrados a medias).
        public void RegistrarCambios(string tabla, int idRegistro, List<BE_CambioCampo> cambios, int idUsuario, string accion)
        {
            if (cambios == null || cambios.Count == 0)
                return;

            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        Guid idTransaccion = Guid.NewGuid();

                        // Fecha única para todo el grupo, tomada del reloj del servidor
                        // (misma fuente que usaba el DEFAULT GETDATE() de la tabla).
                        SqlCommand cmdFecha = new SqlCommand("SELECT GETDATE()", conexion, transaccion);
                        DateTime fechaCambio = (DateTime)cmdFecha.ExecuteScalar();

                        foreach (BE_CambioCampo cambio in cambios)
                        {
                            SqlCommand cmdRegistrarCambio = new SqlCommand(@"Insert into HistorialCambios
                                (Tabla, IdRegistro, Campo, ValorAnterior, ValorNuevo, IdUsuario, Accion, FechaCambio, IdTransaccion)
                                VALUES (@tabla, @idRegistro, @campo, @valorAnt, @valorNuevo, @idUsu, @accion, @fecha, @idTransaccion)", conexion, transaccion);

                            cmdRegistrarCambio.Parameters.Add("@tabla", SqlDbType.VarChar, 50).Value = tabla;
                            cmdRegistrarCambio.Parameters.Add("@idRegistro", SqlDbType.Int).Value = idRegistro;
                            cmdRegistrarCambio.Parameters.Add("@campo", SqlDbType.VarChar, 50).Value = cambio.Campo;
                            cmdRegistrarCambio.Parameters.Add("@valorAnt", SqlDbType.VarChar, 500).Value = cambio.ValorAnterior ?? (object)DBNull.Value;
                            cmdRegistrarCambio.Parameters.Add("@valorNuevo", SqlDbType.VarChar, 500).Value = cambio.ValorNuevo;
                            cmdRegistrarCambio.Parameters.Add("@idUsu", SqlDbType.Int).Value = idUsuario;
                            cmdRegistrarCambio.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fechaCambio;
                            cmdRegistrarCambio.Parameters.Add("@idTransaccion", SqlDbType.UniqueIdentifier).Value = idTransaccion;
                            cmdRegistrarCambio.Parameters.Add("@accion", SqlDbType.VarChar, 20).Value = accion;

                            cmdRegistrarCambio.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataTable ObtenerHistorial(string tabla, int idRegistro)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdObtHistorial = new SqlCommand(@"
                SELECT h.FechaCambio, u.Nombre + ' ' + u.Apellido AS Usuario,
                       h.Accion, h.Campo, h.ValorAnterior, h.ValorNuevo, h.IdTransaccion
                FROM HistorialCambios h
                LEFT JOIN Usuario u ON h.IdUsuario = u.IdUsuario
                WHERE h.Tabla = @tabla AND h.IdRegistro = @id
                ORDER BY h.FechaCambio DESC", conexion);

                cmdObtHistorial.Parameters.Add("@tabla", SqlDbType.VarChar, 50).Value = tabla;
                cmdObtHistorial.Parameters.Add("@id", SqlDbType.Int).Value = idRegistro;

                SqlDataAdapter da = new SqlDataAdapter(cmdObtHistorial);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public DataTable ObtenerHistorialHastaFecha(string tabla, int idRegistro, DateTime fechaHasta)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(@"
            SELECT Campo, ValorAnterior, ValorNuevo, FechaCambio
            FROM HistorialCambios
            WHERE Tabla = @tabla 
              AND IdRegistro = @id 
              AND FechaCambio >= @fecha
              AND Accion = 'MODIFICACION'
            ORDER BY FechaCambio DESC", conexion);

                cmd.Parameters.Add("@tabla", SqlDbType.VarChar, 50).Value = tabla;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idRegistro;
                cmd.Parameters.AddWithValue("@fecha", fechaHasta);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
