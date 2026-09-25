using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_Turno
    {
        DAL_Conexion conex = new DAL_Conexion();

        public bool ExisteTurnoActivo(int idHorario, DateTime fecha)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT COUNT(1) FROM Turno WHERE IdHorario = @idHorario AND Fecha = @fecha AND Estado = 'Confirmado'", conexion);
                    cmd.Parameters.Add("@idHorario", SqlDbType.Int).Value = idHorario;
                    cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha.Date;

                    int cantidad = (int)cmd.ExecuteScalar();
                    return cantidad > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insertar(BE_Turno turno)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Turno (IdPaciente, IdHorario, Fecha, Estado) VALUES (@idPaciente, @idHorario, @fecha, 'Confirmado')", conexion);
                    cmd.Parameters.Add("@idPaciente", SqlDbType.Int).Value = turno.IdPaciente;
                    cmd.Parameters.Add("@idHorario", SqlDbType.Int).Value = turno.IdHorario;
                    cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = turno.Fecha.Date;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CambiarEstado(int idTurno, string estado)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE Turno SET Estado = @estado WHERE IdTurno = @idTurno", conexion);
                    cmd.Parameters.Add("@estado", SqlDbType.VarChar, 20).Value = estado;
                    cmd.Parameters.Add("@idTurno", SqlDbType.Int).Value = idTurno;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BE_Turno ObtenerPorId(int idTurno)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT IdTurno, IdPaciente, IdHorario, Fecha, Estado FROM Turno WHERE IdTurno = @idTurno", conexion);
                    cmd.Parameters.Add("@idTurno", SqlDbType.Int).Value = idTurno;

                    SqlDataReader lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {
                        BE_Turno turno = new BE_Turno();
                        turno.IdTurno = Convert.ToInt32(lector["IdTurno"]);
                        turno.IdPaciente = Convert.ToInt32(lector["IdPaciente"]);
                        turno.IdHorario = Convert.ToInt32(lector["IdHorario"]);
                        turno.Fecha = Convert.ToDateTime(lector["Fecha"]);
                        turno.Estado = lector["Estado"].ToString();
                        return turno;
                    }

                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BE_Turno> ListarPorPaciente(int idPaciente)
        {
            try
            {
                Dictionary<int, List<string>> especialidadesPorMedico = ObtenerEspecialidadesPorMedico();

                List<BE_Turno> lista = new List<BE_Turno>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT t.IdTurno, t.IdPaciente, t.IdHorario, t.Fecha, t.Estado,
                               mh.IdMedico, mh.HoraInicio, mh.HoraFin,
                               u.Nombre, u.Apellido, esp.Nombre AS NombreEspecialidad
                        FROM Turno t
                        INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                        INNER JOIN Usuario u ON u.IdUsuario = mh.IdMedico
                        LEFT JOIN Especialidades esp ON esp.IdEspecialidad = mh.IdEspecialidad
                        WHERE t.IdPaciente = @idPaciente
                        ORDER BY t.Fecha DESC, mh.HoraInicio", conexion);
                    cmd.Parameters.Add("@idPaciente", SqlDbType.Int).Value = idPaciente;

                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        int idMedico = Convert.ToInt32(lector["IdMedico"]);

                        BE_Turno turno = new BE_Turno();
                        turno.IdTurno = Convert.ToInt32(lector["IdTurno"]);
                        turno.IdPaciente = Convert.ToInt32(lector["IdPaciente"]);
                        turno.IdHorario = Convert.ToInt32(lector["IdHorario"]);
                        turno.Fecha = Convert.ToDateTime(lector["Fecha"]);
                        turno.Estado = lector["Estado"].ToString();
                        turno.IdMedico = idMedico;
                        turno.HoraInicio = (TimeSpan)lector["HoraInicio"];
                        turno.HoraFin = (TimeSpan)lector["HoraFin"];
                        turno.NombreApellidoMedico = lector["Nombre"].ToString() + " " + lector["Apellido"].ToString();
                        // Con especialidad asignada en la franja se muestra solo esa.
                        turno.Especialidades = lector["NombreEspecialidad"] != DBNull.Value
                            ? lector["NombreEspecialidad"].ToString()
                            : (especialidadesPorMedico.ContainsKey(idMedico)
                                ? string.Join(", ", especialidadesPorMedico[idMedico])
                                : string.Empty);

                        lista.Add(turno);
                    }
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Turnos Confirmados de hoy en adelante de un horario puntual (una franja).
        public List<BE_Turno> ListarConfirmadosPorHorarioDesde(int idHorario, DateTime desde)
        {
            try
            {
                List<BE_Turno> lista = new List<BE_Turno>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT t.IdTurno, t.IdPaciente, t.IdHorario, t.Fecha, t.Estado,
                               mh.HoraInicio, mh.HoraFin, u.Nombre, u.Apellido
                        FROM Turno t
                        INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                        INNER JOIN Usuario u ON u.IdUsuario = t.IdPaciente
                        WHERE t.IdHorario = @idHorario
                          AND t.Estado = 'Confirmado' AND t.Fecha >= @desde
                        ORDER BY t.Fecha, mh.HoraInicio", conexion);
                    cmd.Parameters.Add("@idHorario", SqlDbType.Int).Value = idHorario;
                    cmd.Parameters.Add("@desde", SqlDbType.Date).Value = desde.Date;

                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        BE_Turno turno = new BE_Turno();
                        turno.IdTurno = Convert.ToInt32(lector["IdTurno"]);
                        turno.IdPaciente = Convert.ToInt32(lector["IdPaciente"]);
                        turno.IdHorario = Convert.ToInt32(lector["IdHorario"]);
                        turno.Fecha = Convert.ToDateTime(lector["Fecha"]);
                        turno.Estado = lector["Estado"].ToString();
                        turno.HoraInicio = (TimeSpan)lector["HoraInicio"];
                        turno.HoraFin = (TimeSpan)lector["HoraFin"];
                        turno.NombreApellidoPaciente = lector["Nombre"].ToString() + " " + lector["Apellido"].ToString();

                        lista.Add(turno);
                    }
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Turnos Confirmados de hoy en adelante para un medico en un dia de la semana puntual,
        // usado para detectar conflictos antes de modificar el horario de ese medico.
        public List<BE_Turno> ListarActivosPorMedicoYDiaDesde(int idMedico, int diaSemana, DateTime desde, int? idEspecialidad)
        {
            try
            {
                List<BE_Turno> lista = new List<BE_Turno>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT t.IdTurno, t.IdPaciente, t.IdHorario, t.Fecha, t.Estado,
                               mh.HoraInicio, mh.HoraFin, u.Nombre, u.Apellido
                        FROM Turno t
                        INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                        INNER JOIN Usuario u ON u.IdUsuario = t.IdPaciente
                        WHERE mh.IdMedico = @idMedico AND mh.DiaSemana = @diaSemana
                          AND t.Estado = 'Confirmado' AND t.Fecha >= @desde
                          AND (@idEspecialidad IS NULL OR mh.IdEspecialidad IS NULL OR mh.IdEspecialidad = @idEspecialidad)
                        ORDER BY t.Fecha, mh.HoraInicio", conexion);
                    cmd.Parameters.Add("@idEspecialidad", SqlDbType.Int).Value = (object)idEspecialidad ?? DBNull.Value;
                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                    cmd.Parameters.Add("@diaSemana", SqlDbType.TinyInt).Value = diaSemana;
                    cmd.Parameters.Add("@desde", SqlDbType.Date).Value = desde.Date;

                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        BE_Turno turno = new BE_Turno();
                        turno.IdTurno = Convert.ToInt32(lector["IdTurno"]);
                        turno.IdPaciente = Convert.ToInt32(lector["IdPaciente"]);
                        turno.IdHorario = Convert.ToInt32(lector["IdHorario"]);
                        turno.Fecha = Convert.ToDateTime(lector["Fecha"]);
                        turno.Estado = lector["Estado"].ToString();
                        turno.HoraInicio = (TimeSpan)lector["HoraInicio"];
                        turno.HoraFin = (TimeSpan)lector["HoraFin"];
                        turno.NombreApellidoPaciente = lector["Nombre"].ToString() + " " + lector["Apellido"].ToString();

                        lista.Add(turno);
                    }
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Turnos de cualquier paciente, con filtros opcionales, para la pantalla de Gestion
        // de Turnos usada por Administrativo/Administrador (RF 01.2: cancelar el turno de
        // cualquier paciente). A diferencia de ListarPorPaciente, no filtra por IdPaciente.
        public List<BE_Turno> Filtrar(int? idTurno, DateTime? desde, DateTime? hasta, string estado)
        {
            try
            {
                Dictionary<int, List<string>> especialidadesPorMedico = ObtenerEspecialidadesPorMedico();

                List<BE_Turno> lista = new List<BE_Turno>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT t.IdTurno, t.IdPaciente, t.IdHorario, t.Fecha, t.Estado,
                               mh.IdMedico, mh.HoraInicio, mh.HoraFin,
                               um.Nombre AS NombreMedico, um.Apellido AS ApellidoMedico,
                               up.Nombre AS NombrePaciente, up.Apellido AS ApellidoPaciente,
                               esp.Nombre AS NombreEspecialidad
                        FROM Turno t
                        INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                        LEFT JOIN Especialidades esp ON esp.IdEspecialidad = mh.IdEspecialidad
                        INNER JOIN Usuario um ON um.IdUsuario = mh.IdMedico
                        INNER JOIN Usuario up ON up.IdUsuario = t.IdPaciente
                        WHERE (@idTurno IS NULL OR t.IdTurno = @idTurno)
                          AND (@desde IS NULL OR t.Fecha >= @desde)
                          AND (@hasta IS NULL OR t.Fecha <= @hasta)
                          AND (@estado IS NULL OR t.Estado = @estado)
                        ORDER BY t.Fecha DESC, mh.HoraInicio", conexion);

                    cmd.Parameters.Add("@idTurno", SqlDbType.Int).Value = (object)idTurno ?? DBNull.Value;
                    cmd.Parameters.Add("@desde", SqlDbType.Date).Value = (object)desde ?? DBNull.Value;
                    cmd.Parameters.Add("@hasta", SqlDbType.Date).Value = (object)hasta ?? DBNull.Value;
                    cmd.Parameters.Add("@estado", SqlDbType.VarChar, 20).Value = (object)estado ?? DBNull.Value;

                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        int idMedico = Convert.ToInt32(lector["IdMedico"]);

                        BE_Turno turno = new BE_Turno();
                        turno.IdTurno = Convert.ToInt32(lector["IdTurno"]);
                        turno.IdPaciente = Convert.ToInt32(lector["IdPaciente"]);
                        turno.IdHorario = Convert.ToInt32(lector["IdHorario"]);
                        turno.Fecha = Convert.ToDateTime(lector["Fecha"]);
                        turno.Estado = lector["Estado"].ToString();
                        turno.IdMedico = idMedico;
                        turno.HoraInicio = (TimeSpan)lector["HoraInicio"];
                        turno.HoraFin = (TimeSpan)lector["HoraFin"];
                        turno.NombreApellidoMedico = lector["NombreMedico"].ToString() + " " + lector["ApellidoMedico"].ToString();
                        turno.NombreApellidoPaciente = lector["NombrePaciente"].ToString() + " " + lector["ApellidoPaciente"].ToString();
                        // Con especialidad asignada en la franja se muestra solo esa.
                        turno.Especialidades = lector["NombreEspecialidad"] != DBNull.Value
                            ? lector["NombreEspecialidad"].ToString()
                            : (especialidadesPorMedico.ContainsKey(idMedico)
                                ? string.Join(", ", especialidadesPorMedico[idMedico])
                                : string.Empty);

                        lista.Add(turno);
                    }
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Turnos Confirmados cuyo horario de fin, mas 1 hora de margen, ya paso respecto de
        // la hora del servidor de base de datos (GETDATE()). Solo trae los ids: el proceso de
        // vencimiento (BLL_Turno.ProcesarVencimientos) no necesita el turno completo.
        public List<int> ListarVencidosParaMarcarAusente()
        {
            try
            {
                List<int> lista = new List<int>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT t.IdTurno
                        FROM Turno t
                        INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                        WHERE t.Estado = 'Confirmado'
                          AND DATEADD(HOUR, 1, DATEADD(MINUTE, DATEDIFF(MINUTE, 0, mh.HoraFin), CAST(t.Fecha AS DATETIME))) < GETDATE()", conexion);

                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                        lista.Add(Convert.ToInt32(lector["IdTurno"]));
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Marca un turno puntual como Ausente, solo si en este momento sigue Confirmado.
        // Ese filtro evita pisar un turno que el medico acaba de marcar Atendido. Devuelve
        // si realmente actualizo la fila, para que la BLL sepa si tiene que auditar el evento.
        public bool MarcarAusenteSiConfirmado(int idTurno)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE Turno SET Estado = 'Ausente' WHERE IdTurno = @idTurno AND Estado = 'Confirmado'", conexion);
                    cmd.Parameters.Add("@idTurno", SqlDbType.Int).Value = idTurno;

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Dictionary<int, List<string>> ObtenerEspecialidadesPorMedico()
        {
            Dictionary<int, List<string>> especialidadesPorMedico = new Dictionary<int, List<string>>();
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT me.IdMedico, e.Nombre
                    FROM Medico_Especialidad me
                    INNER JOIN Especialidades e ON e.IdEspecialidad = me.IdEsp", conexion);

                SqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    int idMedico = Convert.ToInt32(lector["IdMedico"]);
                    string nombreEsp = lector["Nombre"].ToString();

                    if (!especialidadesPorMedico.ContainsKey(idMedico))
                        especialidadesPorMedico[idMedico] = new List<string>();

                    especialidadesPorMedico[idMedico].Add(nombreEsp);
                }
            }

            return especialidadesPorMedico;
        }
    }
}
