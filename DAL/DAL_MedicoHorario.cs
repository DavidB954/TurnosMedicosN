using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_MedicoHorario
    {
        DAL_Conexion conex = new DAL_Conexion();

        public void InsertarOReactivarHorario(BE_MedicoHorario horario)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        IF EXISTS (SELECT 1 FROM Medico_Horario WHERE IdMedico = @idMedico AND DiaSemana = @diaSemana AND HoraInicio = @horaInicio)
                            UPDATE Medico_Horario
                            SET HoraFin = @horaFin, Activo = 1, IdEspecialidad = @idEspecialidad
                            WHERE IdMedico = @idMedico AND DiaSemana = @diaSemana AND HoraInicio = @horaInicio
                        ELSE
                            INSERT INTO Medico_Horario (IdMedico, DiaSemana, HoraInicio, HoraFin, Activo, IdEspecialidad)
                            VALUES (@idMedico, @diaSemana, @horaInicio, @horaFin, 1, @idEspecialidad)", conexion);

                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = horario.IdMedico;
                    cmd.Parameters.Add("@diaSemana", SqlDbType.TinyInt).Value = horario.DiaSemana;
                    cmd.Parameters.Add("@horaInicio", SqlDbType.Time).Value = horario.HoraInicio;
                    cmd.Parameters.Add("@horaFin", SqlDbType.Time).Value = horario.HoraFin;
                    cmd.Parameters.Add("@idEspecialidad", SqlDbType.Int).Value = (object)horario.IdEspecialidad ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DesactivarHorariosPorMedicoYDia(int idMedico, int diaSemana, int? idEspecialidad)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmdDesactivar = new SqlCommand(@"UPDATE Medico_Horario SET Activo = 0 WHERE IdMedico = @idMedico AND DiaSemana = @diaSemana AND Activo = 1
                          AND (@idEspecialidad IS NULL OR IdEspecialidad IS NULL OR IdEspecialidad = @idEspecialidad)", conexion);
                    cmdDesactivar.Parameters.Add("@idEspecialidad", SqlDbType.Int).Value = (object)idEspecialidad ?? DBNull.Value;
                    cmdDesactivar.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                    cmdDesactivar.Parameters.Add("@diaSemana", SqlDbType.TinyInt).Value = diaSemana;
                    cmdDesactivar.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BE_MedicoHorario ObtenerPorId(int idHorario)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT IdHorario, IdMedico, DiaSemana, HoraInicio, HoraFin, Activo, IdEspecialidad FROM Medico_Horario WHERE IdHorario = @idHorario", conexion);
                    cmd.Parameters.Add("@idHorario", SqlDbType.Int).Value = idHorario;

                    SqlDataReader lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {
                        BE_MedicoHorario horario = new BE_MedicoHorario();
                        horario.IdHorario = Convert.ToInt32(lector["IdHorario"]);
                        horario.IdMedico = Convert.ToInt32(lector["IdMedico"]);
                        horario.DiaSemana = Convert.ToInt32(lector["DiaSemana"]);
                        horario.HoraInicio = (TimeSpan)lector["HoraInicio"];
                        horario.HoraFin = (TimeSpan)lector["HoraFin"];
                        horario.Activo = Convert.ToBoolean(lector["Activo"]);
                        horario.IdEspecialidad = lector["IdEspecialidad"] == DBNull.Value ? (int?)null : Convert.ToInt32(lector["IdEspecialidad"]);
                        return horario;
                    }

                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BE_MedicoHorario> ListarDisponibles(int idMedico, int diaSemana, DateTime fecha, int? idEspecialidad)
        {
            try
            {
                List<BE_MedicoHorario> lista = new List<BE_MedicoHorario>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT mh.IdHorario, mh.IdMedico, mh.DiaSemana, mh.HoraInicio, mh.HoraFin, mh.Activo
                        FROM Medico_Horario mh
                        WHERE mh.IdMedico = @idMedico AND mh.DiaSemana = @diaSemana AND mh.Activo = 1
                          AND (@idEspecialidad IS NULL OR mh.IdEspecialidad IS NULL OR mh.IdEspecialidad = @idEspecialidad)
                          AND NOT EXISTS (
                              SELECT 1 FROM Turno t
                              WHERE t.IdHorario = mh.IdHorario AND t.Fecha = @fecha AND t.Estado IN ('Confirmado', 'Atendido', 'Ausente')
                          )
                          AND NOT EXISTS (
                              SELECT 1 FROM Medico_HorarioInactivo hi WHERE hi.IdHorario = mh.IdHorario AND hi.Fecha = @fecha
                          )
                          AND (@fecha > CAST(GETDATE() AS date) OR mh.HoraInicio > CAST(GETDATE() AS time))
                        ORDER BY mh.HoraInicio", conexion);
                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                    cmd.Parameters.Add("@diaSemana", SqlDbType.TinyInt).Value = diaSemana;
                    cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha.Date;
                    cmd.Parameters.Add("@idEspecialidad", SqlDbType.Int).Value = (object)idEspecialidad ?? DBNull.Value;

                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        BE_MedicoHorario horario = new BE_MedicoHorario();
                        horario.IdHorario = Convert.ToInt32(lector["IdHorario"]);
                        horario.IdMedico = Convert.ToInt32(lector["IdMedico"]);
                        horario.DiaSemana = Convert.ToInt32(lector["DiaSemana"]);
                        horario.HoraInicio = (TimeSpan)lector["HoraInicio"];
                        horario.HoraFin = (TimeSpan)lector["HoraFin"];
                        horario.Activo = Convert.ToBoolean(lector["Activo"]);
                        lista.Add(horario);
                    }
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BE_MedicoHorario> ListarPorMedicoYFecha(int idMedico, DateTime fecha)
        {
            try
            {
                List<BE_MedicoHorario> lista = new List<BE_MedicoHorario>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT mh.IdHorario, mh.IdMedico, mh.DiaSemana, mh.HoraInicio, mh.HoraFin, mh.Activo, mh.IdEspecialidad,
                               esp.Nombre AS NombreEspecialidad,
                               CASE WHEN hi.IdHorario IS NULL THEN 0 ELSE 1 END AS InactivoEnFecha,
                               (SELECT TOP 1 p.Nombre + ' ' + p.Apellido FROM Turno t INNER JOIN Paciente p ON p.IdPaciente = t.IdPaciente
                                WHERE t.IdHorario = mh.IdHorario AND t.Fecha = @fecha AND t.Estado = 'Confirmado') AS PacienteEnFecha
                        FROM Medico_Horario mh
                        LEFT JOIN Especialidades esp ON esp.IdEspecialidad = mh.IdEspecialidad
                        LEFT JOIN Medico_HorarioInactivo hi ON hi.IdHorario = mh.IdHorario AND hi.Fecha = @fecha
                        WHERE mh.IdMedico = @idMedico AND mh.DiaSemana = @diaSemana AND mh.Activo = 1
                        ORDER BY mh.HoraInicio", conexion);
                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                    cmd.Parameters.Add("@diaSemana", SqlDbType.TinyInt).Value = (int)fecha.DayOfWeek;
                    cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha.Date;

                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        BE_MedicoHorario horario = new BE_MedicoHorario();
                        horario.IdHorario = Convert.ToInt32(lector["IdHorario"]);
                        horario.IdMedico = Convert.ToInt32(lector["IdMedico"]);
                        horario.DiaSemana = Convert.ToInt32(lector["DiaSemana"]);
                        horario.HoraInicio = (TimeSpan)lector["HoraInicio"];
                        horario.HoraFin = (TimeSpan)lector["HoraFin"];
                        horario.Activo = Convert.ToBoolean(lector["Activo"]);
                        horario.IdEspecialidad = lector["IdEspecialidad"] == DBNull.Value ? (int?)null : Convert.ToInt32(lector["IdEspecialidad"]);
                        horario.NombreEspecialidad = lector["NombreEspecialidad"] == DBNull.Value ? null : lector["NombreEspecialidad"].ToString();
                        horario.InactivoEnFecha = Convert.ToBoolean(lector["InactivoEnFecha"]);
                        horario.PacienteEnFecha = lector["PacienteEnFecha"] == DBNull.Value ? string.Empty : lector["PacienteEnFecha"].ToString();
                        lista.Add(horario);
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InactivarEnFecha(int idHorario, DateTime fecha, int idUsuario)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        IF NOT EXISTS (SELECT 1 FROM Medico_HorarioInactivo WHERE IdHorario = @idHorario AND Fecha = @fecha)
                            INSERT INTO Medico_HorarioInactivo (IdHorario, Fecha, IdUsuario) VALUES (@idHorario, @fecha, @idUsuario)", conexion);
                    cmd.Parameters.Add("@idHorario", SqlDbType.Int).Value = idHorario;
                    cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha.Date;
                    cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ReactivarEnFecha(int idHorario, DateTime fecha)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"DELETE FROM Medico_HorarioInactivo WHERE IdHorario = @idHorario AND Fecha = @fecha", conexion);
                    cmd.Parameters.Add("@idHorario", SqlDbType.Int).Value = idHorario;
                    cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha.Date;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EstaInactivoEnFecha(int idHorario, DateTime fecha)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT COUNT(1) FROM Medico_HorarioInactivo WHERE IdHorario = @idHorario AND Fecha = @fecha", conexion);
                    cmd.Parameters.Add("@idHorario", SqlDbType.Int).Value = idHorario;
                    cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha.Date;
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BE_MedicoHorario> ListarParaGrid(bool incluirInactivos)
        {
            try
            {
                Dictionary<int, List<string>> especialidadesPorMedico = new Dictionary<int, List<string>>();

                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmdEspecialidades = new SqlCommand(@"
                        SELECT me.IdMedico, e.Nombre
                        FROM Medico_Especialidad me
                        INNER JOIN Especialidades e ON e.IdEspecialidad = me.IdEsp", conexion);

                    SqlDataReader lectorEsp = cmdEspecialidades.ExecuteReader();
                    while (lectorEsp.Read())
                    {
                        int idMedico = Convert.ToInt32(lectorEsp["IdMedico"]);
                        string nombreEsp = lectorEsp["Nombre"].ToString();

                        if (!especialidadesPorMedico.ContainsKey(idMedico))
                        {
                            especialidadesPorMedico[idMedico] = new List<string>();
                        }

                        especialidadesPorMedico[idMedico].Add(nombreEsp);
                    }
                }

                List<BE_MedicoHorario> listaHorarios = new List<BE_MedicoHorario>();

                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    string filtroActivo = incluirInactivos ? "" : "WHERE mh.Activo = 1";
                    SqlCommand cmdHorarios = new SqlCommand($@"
                        SELECT mh.IdHorario, mh.IdMedico, u.Nombre, u.Apellido, mh.DiaSemana, mh.HoraInicio, mh.HoraFin, mh.Activo,
                               mh.IdEspecialidad, esp.Nombre AS NombreEspecialidad
                        FROM Medico_Horario mh
                        INNER JOIN Usuario u ON u.IdUsuario = mh.IdMedico
                        LEFT JOIN Especialidades esp ON esp.IdEspecialidad = mh.IdEspecialidad
                        {filtroActivo}
                        ORDER BY u.Apellido, u.Nombre, mh.DiaSemana, mh.HoraInicio", conexion);

                    SqlDataReader lector = cmdHorarios.ExecuteReader();
                    while (lector.Read())
                    {
                        int idMedico = Convert.ToInt32(lector["IdMedico"]);

                        BE_MedicoHorario horario = new BE_MedicoHorario();
                        horario.IdHorario = Convert.ToInt32(lector["IdHorario"]);
                        horario.IdMedico = idMedico;
                        horario.NombreApellido = lector["Nombre"].ToString() + " " + lector["Apellido"].ToString();
                        horario.DiaSemana = Convert.ToInt32(lector["DiaSemana"]);
                        horario.HoraInicio = (TimeSpan)lector["HoraInicio"];
                        horario.HoraFin = (TimeSpan)lector["HoraFin"];
                        horario.Activo = Convert.ToBoolean(lector["Activo"]);
                        horario.IdEspecialidad = lector["IdEspecialidad"] == DBNull.Value ? (int?)null : Convert.ToInt32(lector["IdEspecialidad"]);
                        horario.NombreEspecialidad = lector["NombreEspecialidad"] == DBNull.Value ? null : lector["NombreEspecialidad"].ToString();

                        horario.Especialidades = horario.NombreEspecialidad
                            ?? (especialidadesPorMedico.ContainsKey(idMedico)
                                ? string.Join(", ", especialidadesPorMedico[idMedico])
                                : string.Empty);

                        listaHorarios.Add(horario);
                    }
                }

                return listaHorarios;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
