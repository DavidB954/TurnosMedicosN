using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // T06b Control de cambios: lectura de la tabla copia Turno_Historial. Las filas las escribe el
    // trigger TR_Turno_Historial; esta clase solo las consulta.
    public class DAL_TurnoHistorial
    {
        DAL_Conexion conex = new DAL_Conexion();

        private const string SelectVersion = @"
            SELECT h.IdHistorial, h.IdTurno, h.IdPaciente, h.IdHorario, h.Fecha, h.Estado, h.FechaCreacion,
                   h.Operacion, h.FechaHoraCambio, h.IdUsuarioCambio,
                   p.Nombre AS NombrePaciente, p.Apellido AS ApellidoPaciente,
                   mh.HoraInicio, mh.HoraFin,
                   u.Nombre AS NombreUsuario, u.Apellido AS ApellidoUsuario
            FROM Turno_Historial h
            LEFT JOIN Paciente p ON p.IdPaciente = h.IdPaciente
            LEFT JOIN Medico_Horario mh ON mh.IdHorario = h.IdHorario
            LEFT JOIN Usuario u ON u.IdUsuario = h.IdUsuarioCambio";

        // Versiones de un turno, de la mas reciente a la mas antigua.
        public List<BE_TurnoHistorial> ListarPorTurno(int idTurno)
        {
            try
            {
                List<BE_TurnoHistorial> lista = new List<BE_TurnoHistorial>();
                using (SqlConnection cn = conex.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(SelectVersion + @"
                        WHERE h.IdTurno = @idTurno
                        ORDER BY h.FechaHoraCambio DESC, h.IdHistorial DESC", cn);
                    cmd.Parameters.Add("@idTurno", SqlDbType.Int).Value = idTurno;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                            lista.Add(Mapear(lector));
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BE_TurnoHistorial ObtenerPorId(int idHistorial)
        {
            try
            {
                using (SqlConnection cn = conex.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(SelectVersion + " WHERE h.IdHistorial = @idHistorial", cn);
                    cmd.Parameters.Add("@idHistorial", SqlDbType.Int).Value = idHistorial;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        return lector.Read() ? Mapear(lector) : null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private BE_TurnoHistorial Mapear(SqlDataReader lector)
        {
            BE_TurnoHistorial v = new BE_TurnoHistorial
            {
                IdHistorial = Convert.ToInt32(lector["IdHistorial"]),
                IdTurno = Convert.ToInt32(lector["IdTurno"]),
                IdPaciente = Convert.ToInt32(lector["IdPaciente"]),
                IdHorario = Convert.ToInt32(lector["IdHorario"]),
                Fecha = Convert.ToDateTime(lector["Fecha"]),
                Estado = lector["Estado"].ToString(),
                FechaCreacion = Convert.ToDateTime(lector["FechaCreacion"]),
                Operacion = lector["Operacion"].ToString(),
                FechaHoraCambio = Convert.ToDateTime(lector["FechaHoraCambio"]),
                IdUsuarioCambio = lector["IdUsuarioCambio"] == DBNull.Value ? (int?)null : Convert.ToInt32(lector["IdUsuarioCambio"])
            };
            v.NombrePaciente = lector["NombrePaciente"] == DBNull.Value
                ? "" : lector["NombrePaciente"] + " " + lector["ApellidoPaciente"];
            v.RangoHorario = lector["HoraInicio"] == DBNull.Value
                ? "" : string.Format("{0:hh\\:mm} - {1:hh\\:mm}", (TimeSpan)lector["HoraInicio"], (TimeSpan)lector["HoraFin"]);
            v.NombreUsuarioCambio = lector["NombreUsuario"] == DBNull.Value
                ? "(sin usuario)" : lector["NombreUsuario"] + " " + lector["ApellidoUsuario"];
            return v;
        }
    }
}
