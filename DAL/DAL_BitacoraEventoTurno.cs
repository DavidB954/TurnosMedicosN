using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   
    public class DAL_BitacoraEventoTurno
    {
        DAL_Conexion conex = new DAL_Conexion();

        public List<BE_BitacoraEventoTurno> Filtrar(int? idTurno, DateTime? desde, DateTime? hasta)
        {
            try
            {
                List<BE_BitacoraEventoTurno> lista = new List<BE_BitacoraEventoTurno>();

                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT bet.IdBitacoraEvento, bet.IdTurno, bet.TipoEvento, bet.EstadoAnterior,
                               bet.EstadoNuevo, bet.FechaHora, bet.UsuarioBD, bet.HostBD,
                               up.Nombre AS NombrePaciente, up.Apellido AS ApellidoPaciente,
                               um.Nombre AS NombreMedico, um.Apellido AS ApellidoMedico,
                               t.Fecha AS FechaTurno, mh.HoraInicio, mh.HoraFin
                        FROM BitacoraEventosTurno bet
                        INNER JOIN Turno t ON t.IdTurno = bet.IdTurno
                        INNER JOIN Usuario up ON up.IdUsuario = t.IdPaciente
                        INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                        INNER JOIN Usuario um ON um.IdUsuario = mh.IdMedico
                        WHERE (@idTurno IS NULL OR bet.IdTurno = @idTurno)
                          AND (@desde IS NULL OR bet.FechaHora >= @desde)
                          AND (@hasta IS NULL OR bet.FechaHora <= @hasta)
                        ORDER BY bet.FechaHora DESC", conexion);

                    cmd.Parameters.Add("@idTurno", SqlDbType.Int).Value = (object)idTurno ?? DBNull.Value;
                    cmd.Parameters.Add("@desde", SqlDbType.DateTime).Value = (object)desde ?? DBNull.Value;
                    cmd.Parameters.Add("@hasta", SqlDbType.DateTime).Value = (object)hasta ?? DBNull.Value;

                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        BE_BitacoraEventoTurno evento = new BE_BitacoraEventoTurno();
                        evento.IdBitacoraEvento = Convert.ToInt32(lector["IdBitacoraEvento"]);
                        evento.IdTurno = Convert.ToInt32(lector["IdTurno"]);
                        evento.TipoEvento = lector["TipoEvento"].ToString();
                        evento.EstadoAnterior = lector["EstadoAnterior"] as string;
                        evento.EstadoNuevo = lector["EstadoNuevo"].ToString();
                        evento.FechaHora = Convert.ToDateTime(lector["FechaHora"]);
                        evento.UsuarioBD = lector["UsuarioBD"].ToString();
                        evento.HostBD = lector["HostBD"].ToString();
                        evento.NombreApellidoPaciente = lector["NombrePaciente"] + " " + lector["ApellidoPaciente"];
                        evento.NombreApellidoMedico = lector["NombreMedico"] + " " + lector["ApellidoMedico"];
                        evento.FechaTurno = Convert.ToDateTime(lector["FechaTurno"]);
                        evento.HoraInicio = (TimeSpan)lector["HoraInicio"];
                        evento.HoraFin = (TimeSpan)lector["HoraFin"];

                        lista.Add(evento);
                    }
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
