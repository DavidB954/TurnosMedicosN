using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class DAL_Atencion
    {
        DAL_Conexion conexion = new DAL_Conexion();

        private const string SelectTurnoConPaciente = @"
            SELECT t.IdTurno, t.IdPaciente, t.IdHorario, t.Fecha, t.Estado,
                   mh.IdMedico, mh.HoraInicio, mh.HoraFin,
                   p.Nombre AS NombrePaciente, p.Apellido AS ApellidoPaciente,
                   um.Nombre AS NombreMedico, um.Apellido AS ApellidoMedico,
                   p.DNI, p.NroHistoriaClinica, p.FechaNacimiento, p.Telefono, p.Email, p.FechaAperturaHC,
                   md.Matricula
            FROM Turno t
            INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
            INNER JOIN Paciente p ON p.IdPaciente = t.IdPaciente
            INNER JOIN Usuario um ON um.IdUsuario = mh.IdMedico
            LEFT JOIN Medico md ON md.IdUsuario = mh.IdMedico";

        public DateTime? ObtenerProximaFechaConTurnos(int idMedico, DateTime desde)
        {
            try
            {
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT MIN(t.Fecha)
                        FROM Turno t
                        INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                        WHERE mh.IdMedico = @idMedico AND t.Fecha >= @desde AND t.Estado <> 'Cancelado'", cn);
                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                    cmd.Parameters.Add("@desde", SqlDbType.Date).Value = desde.Date;

                    object resultado = cmd.ExecuteScalar();
                    return resultado == null || resultado == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(resultado);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BE_Turno> ObtenerAgenda(int idMedico, DateTime fecha)
        {
            try
            {
                List<BE_Turno> lista = new List<BE_Turno>();
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(SelectTurnoConPaciente + @"
                        WHERE mh.IdMedico = @idMedico AND t.Fecha = @fecha AND t.Estado <> 'Cancelado'
                        ORDER BY mh.HoraInicio", cn);
                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                    cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha.Date;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                            lista.Add(MapearTurno(lector));
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BE_Turno ObtenerTurnoConPaciente(int idTurno)
        {
            try
            {
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(SelectTurnoConPaciente + " WHERE t.IdTurno = @idTurno", cn);
                    cmd.Parameters.Add("@idTurno", SqlDbType.Int).Value = idTurno;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        return lector.Read() ? MapearTurno(lector) : null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GuardarAtencion(BE_AtencionMedica a)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                cn.Open();
                using (SqlTransaction transaccion = cn.BeginTransaction())
                {
                    try
                    {
                        DAL_Conexion.EstablecerUsuarioAuditoria(cn, transaccion, a.Turno.IdMedico);

                        SqlCommand cmdAtencion = new SqlCommand(@"
                            INSERT INTO AtencionMedica (IdTurno, FechaHora, MotivoConsulta, Indicaciones, DVH)
                            OUTPUT INSERTED.IdAtencion
                            VALUES (@idTurno, @fechaHora, @motivo, @indicaciones, @dvh)", cn, transaccion);
                        cmdAtencion.Parameters.Add("@idTurno", SqlDbType.Int).Value = a.Turno.IdTurno;
                        cmdAtencion.Parameters.Add("@fechaHora", SqlDbType.DateTime).Value = a.FechaHora;
                        cmdAtencion.Parameters.Add("@motivo", SqlDbType.VarChar, 500).Value = a.MotivoConsulta;
                        cmdAtencion.Parameters.Add("@indicaciones", SqlDbType.VarChar, 1000).Value = (object)a.Indicaciones ?? DBNull.Value;
                        cmdAtencion.Parameters.Add("@dvh", SqlDbType.VarChar, 64).Value = a.DVH;
                        a.IdAtencion = (int)cmdAtencion.ExecuteScalar();

                        if (a.Receta != null)
                            GuardarReceta(a.IdAtencion, a.Receta, cn, transaccion);

                        ActualizarEstadoTurno(a.Turno.IdTurno, "Atendido", a.Turno.IdMedico, cn, transaccion);

                        transaccion.Commit();
                    }
                    catch (Exception)
                    {
                        transaccion.Rollback();
                        a.IdAtencion = 0;
                        if (a.Receta != null)
                        {
                            a.Receta.IdReceta = 0;
                            a.Receta.NroReceta = null;
                        }
                        throw;
                    }
                }
            }
        }

        private void GuardarReceta(int idAtencion, BE_Receta receta, SqlConnection cn, SqlTransaction transaccion)
        {
            SqlCommand cmdReceta = new SqlCommand(@"
                DECLARE @numero INT = NEXT VALUE FOR dbo.SEQ_NroReceta;
                INSERT INTO Receta (IdAtencion, NroReceta, FechaEmision)
                OUTPUT INSERTED.IdReceta, INSERTED.NroReceta
                VALUES (@idAtencion,
                        CONCAT('R-', YEAR(@fechaEmision), '-', RIGHT('000000' + CAST(@numero AS VARCHAR(10)), 6)),
                        @fechaEmision)", cn, transaccion);
            cmdReceta.Parameters.Add("@idAtencion", SqlDbType.Int).Value = idAtencion;
            cmdReceta.Parameters.Add("@fechaEmision", SqlDbType.DateTime).Value = receta.FechaEmision;

            using (SqlDataReader lector = cmdReceta.ExecuteReader())
            {
                lector.Read();
                receta.IdReceta = lector.GetInt32(0);
                receta.NroReceta = lector.GetString(1);
            }

            foreach (BE_RecetaItem item in receta.Items)
            {
                SqlCommand cmdItem = new SqlCommand(@"
                    INSERT INTO RecetaItem (IdReceta, Medicamento, Presentacion, Posologia, Cantidad)
                    OUTPUT INSERTED.IdRecetaItem
                    VALUES (@idReceta, @medicamento, @presentacion, @posologia, @cantidad)", cn, transaccion);
                cmdItem.Parameters.Add("@idReceta", SqlDbType.Int).Value = receta.IdReceta;
                cmdItem.Parameters.Add("@medicamento", SqlDbType.VarChar, 150).Value = item.Medicamento;
                cmdItem.Parameters.Add("@presentacion", SqlDbType.VarChar, 100).Value = (object)item.Presentacion ?? DBNull.Value;
                cmdItem.Parameters.Add("@posologia", SqlDbType.VarChar, 200).Value = item.Posologia;
                cmdItem.Parameters.Add("@cantidad", SqlDbType.Int).Value = item.Cantidad;
                item.IdRecetaItem = (int)cmdItem.ExecuteScalar();
            }
        }

        public void ActualizarEstadoTurno(int idTurno, string estado, int idUsuarioCambio)
        {
            try
            {
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    ActualizarEstadoTurno(idTurno, estado, idUsuarioCambio, cn, null);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ActualizarEstadoTurno(int idTurno, string estado, int idUsuarioCambio, SqlConnection cn, SqlTransaction transaccion)
        {
            DAL_Conexion.EstablecerUsuarioAuditoria(cn, transaccion, idUsuarioCambio);

            SqlCommand cmd = new SqlCommand(
                "UPDATE Turno SET Estado = @estado WHERE IdTurno = @idTurno AND Estado = 'Confirmado'", cn, transaccion);
            cmd.Parameters.Add("@estado", SqlDbType.VarChar, 20).Value = estado;
            cmd.Parameters.Add("@idTurno", SqlDbType.Int).Value = idTurno;

            if (cmd.ExecuteNonQuery() == 0)
                throw new InvalidOperationException("Ese turno ya no esta activo.");
        }

        public int ContarPendientes(int idMedico, DateTime hasta)
        {
            try
            {
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(1)
                        FROM Turno t
                        INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                        WHERE mh.IdMedico = @idMedico AND t.Fecha <= @hasta AND t.Estado = 'Confirmado'", cn);
                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                    cmd.Parameters.Add("@hasta", SqlDbType.Date).Value = hasta.Date;
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<int> MarcarAusentesPendientes(int idMedico, DateTime hasta)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                cn.Open();
                using (SqlTransaction transaccion = cn.BeginTransaction())
                {
                    try
                    {
                        DAL_Conexion.EstablecerUsuarioAuditoria(cn, transaccion, idMedico);

                        SqlCommand cmd = new SqlCommand(@"
                            DECLARE @marcados TABLE (IdTurno INT);
                            UPDATE t SET t.Estado = 'Ausente'
                            OUTPUT INSERTED.IdTurno INTO @marcados
                            FROM Turno t
                            INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                            WHERE mh.IdMedico = @idMedico AND t.Fecha <= @hasta AND t.Estado = 'Confirmado';
                            SELECT IdTurno FROM @marcados;", cn, transaccion);
                        cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                        cmd.Parameters.Add("@hasta", SqlDbType.Date).Value = hasta.Date;

                        List<int> ids = new List<int>();
                        using (SqlDataReader lector = cmd.ExecuteReader())
                        {
                            while (lector.Read())
                                ids.Add(lector.GetInt32(0));
                        }

                        transaccion.Commit();
                        return ids;
                    }
                    catch (Exception)
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<BE_Paciente> BuscarPacientes(string texto)
        {
            try
            {
                List<BE_Paciente> lista = new List<BE_Paciente>();
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT p.IdPaciente, p.Nombre AS NombrePaciente, p.Apellido AS ApellidoPaciente,
                               p.DNI, p.NroHistoriaClinica, p.FechaNacimiento, p.Telefono, p.Email, p.FechaAperturaHC
                        FROM Paciente p
                        WHERE (p.Nombre + ' ' + p.Apellido) LIKE @patron
                           OR (p.Apellido + ' ' + p.Nombre) LIKE @patron
                           OR p.DNI LIKE @patron
                           OR (@numero IS NOT NULL AND p.NroHistoriaClinica = @numero)
                        ORDER BY p.Apellido, p.Nombre", cn);

                    int numero;
                    bool esNumero = int.TryParse(texto, out numero);
                    cmd.Parameters.Add("@patron", SqlDbType.VarChar, 120).Value = "%" + texto + "%";
                    cmd.Parameters.Add("@numero", SqlDbType.Int).Value = esNumero ? (object)numero : DBNull.Value;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            BE_Paciente paciente = MapearPaciente(lector);
                            paciente.IdPaciente = Convert.ToInt32(lector["IdPaciente"]);
                            lista.Add(paciente);
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BE_AtencionMedica> ObtenerHistoriaClinica(int nroHC)
        {
            return ObtenerAtenciones("p.NroHistoriaClinica = @valor", nroHC);
        }

        private List<BE_AtencionMedica> ObtenerAtenciones(string condicion, int valor)
        {
            try
            {
                List<BE_AtencionMedica> lista = new List<BE_AtencionMedica>();
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT am.IdAtencion, am.FechaHora, am.MotivoConsulta, am.Indicaciones, am.DVH,
                               t.IdTurno, t.IdPaciente, t.IdHorario, t.Fecha, t.Estado,
                               mh.IdMedico, mh.HoraInicio, mh.HoraFin,
                               um.Nombre AS NombreMedico, um.Apellido AS ApellidoMedico,
                               COALESCE(e.Nombre,
                                        (SELECT STRING_AGG(e2.Nombre, ', ')
                                         FROM Medico_Especialidad me
                                         INNER JOIN Especialidades e2 ON e2.IdEspecialidad = me.IdEsp
                                         WHERE me.IdMedico = mh.IdMedico)) AS Especialidad,
                               r.IdReceta, r.NroReceta, r.FechaEmision,
                               p.Nombre AS NombrePaciente, p.Apellido AS ApellidoPaciente,
                               p.DNI, p.NroHistoriaClinica, md.Matricula
                        FROM AtencionMedica am
                        INNER JOIN Turno t ON t.IdTurno = am.IdTurno
                        INNER JOIN Paciente p ON p.IdPaciente = t.IdPaciente
                        INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                        INNER JOIN Usuario um ON um.IdUsuario = mh.IdMedico
                        LEFT JOIN Medico md ON md.IdUsuario = mh.IdMedico
                        LEFT JOIN Especialidades e ON e.IdEspecialidad = mh.IdEspecialidad
                        LEFT JOIN Receta r ON r.IdAtencion = am.IdAtencion
                        WHERE " + condicion + @"
                        ORDER BY am.FechaHora DESC", cn);
                    cmd.Parameters.Add("@valor", SqlDbType.Int).Value = valor;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            BE_AtencionMedica atencion = new BE_AtencionMedica();
                            atencion.IdAtencion = Convert.ToInt32(lector["IdAtencion"]);
                            atencion.FechaHora = Convert.ToDateTime(lector["FechaHora"]);
                            atencion.MotivoConsulta = lector["MotivoConsulta"].ToString();
                            atencion.Indicaciones = lector["Indicaciones"] as string;
                            atencion.DVH = lector["DVH"].ToString();

                            BE_Turno turno = new BE_Turno();
                            turno.IdTurno = Convert.ToInt32(lector["IdTurno"]);
                            turno.IdPaciente = Convert.ToInt32(lector["IdPaciente"]);
                            turno.IdHorario = Convert.ToInt32(lector["IdHorario"]);
                            turno.Fecha = Convert.ToDateTime(lector["Fecha"]);
                            turno.Estado = lector["Estado"].ToString();
                            turno.IdMedico = Convert.ToInt32(lector["IdMedico"]);
                            turno.HoraInicio = (TimeSpan)lector["HoraInicio"];
                            turno.HoraFin = (TimeSpan)lector["HoraFin"];
                            turno.NombreApellidoMedico = lector["NombreMedico"] + " " + lector["ApellidoMedico"];
                            turno.Especialidades = lector["Especialidad"] as string;
                            turno.MatriculaMedico = lector["Matricula"] as string;
                            turno.NombreApellidoPaciente = lector["NombrePaciente"] + " " + lector["ApellidoPaciente"];
                            turno.Paciente = new BE_Paciente
                            {
                                IdPaciente = turno.IdPaciente,
                                Nombre = lector["NombrePaciente"].ToString(),
                                Apellido = lector["ApellidoPaciente"].ToString(),
                                DNI = lector["DNI"].ToString(),
                                NroHistoriaClinica = Convert.ToInt32(lector["NroHistoriaClinica"])
                            };
                            atencion.Turno = turno;

                            if (lector["IdReceta"] != DBNull.Value)
                            {
                                atencion.Receta = new BE_Receta
                                {
                                    IdReceta = Convert.ToInt32(lector["IdReceta"]),
                                    NroReceta = lector["NroReceta"].ToString(),
                                    FechaEmision = Convert.ToDateTime(lector["FechaEmision"])
                                };
                            }

                            lista.Add(atencion);
                        }
                    }

                    CargarItemsDeRecetas(lista.Where(a => a.Receta != null).Select(a => a.Receta).ToList(), cn);
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CargarItemsDeRecetas(List<BE_Receta> recetas, SqlConnection cn)
        {
            if (recetas.Count == 0)
                return;

            Dictionary<int, BE_Receta> porId = recetas.ToDictionary(r => r.IdReceta);

            SqlCommand cmd = new SqlCommand(
                "SELECT IdRecetaItem, IdReceta, Medicamento, Presentacion, Posologia, Cantidad FROM RecetaItem " +
                "WHERE IdReceta IN (" + string.Join(",", porId.Keys) + ") ORDER BY IdRecetaItem", cn);

            using (SqlDataReader lector = cmd.ExecuteReader())
            {
                while (lector.Read())
                {
                    porId[Convert.ToInt32(lector["IdReceta"])].Items.Add(new BE_RecetaItem
                    {
                        IdRecetaItem = Convert.ToInt32(lector["IdRecetaItem"]),
                        Medicamento = lector["Medicamento"].ToString(),
                        Presentacion = lector["Presentacion"] as string,
                        Posologia = lector["Posologia"].ToString(),
                        Cantidad = Convert.ToInt32(lector["Cantidad"])
                    });
                }
            }
        }

        public List<BE_AtencionMedica> ListarParaIntegridad()
        {
            try
            {
                List<BE_AtencionMedica> lista = new List<BE_AtencionMedica>();
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT IdAtencion, IdTurno, FechaHora, MotivoConsulta, Indicaciones, DVH FROM AtencionMedica ORDER BY IdAtencion", cn);

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            lista.Add(new BE_AtencionMedica
                            {
                                IdAtencion = Convert.ToInt32(lector["IdAtencion"]),
                                Turno = new BE_Turno { IdTurno = Convert.ToInt32(lector["IdTurno"]) },
                                FechaHora = Convert.ToDateTime(lector["FechaHora"]),
                                MotivoConsulta = lector["MotivoConsulta"].ToString(),
                                Indicaciones = lector["Indicaciones"] as string,
                                DVH = lector["DVH"].ToString()
                            });
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private BE_Turno MapearTurno(SqlDataReader lector)
        {
            BE_Turno turno = new BE_Turno();
            turno.IdTurno = Convert.ToInt32(lector["IdTurno"]);
            turno.IdPaciente = Convert.ToInt32(lector["IdPaciente"]);
            turno.IdHorario = Convert.ToInt32(lector["IdHorario"]);
            turno.Fecha = Convert.ToDateTime(lector["Fecha"]);
            turno.Estado = lector["Estado"].ToString();
            turno.IdMedico = Convert.ToInt32(lector["IdMedico"]);
            turno.HoraInicio = (TimeSpan)lector["HoraInicio"];
            turno.HoraFin = (TimeSpan)lector["HoraFin"];
            turno.NombreApellidoPaciente = lector["NombrePaciente"] + " " + lector["ApellidoPaciente"];
            turno.NombreApellidoMedico = lector["NombreMedico"] + " " + lector["ApellidoMedico"];
            turno.MatriculaMedico = lector["Matricula"] as string;

            BE_Paciente paciente = MapearPaciente(lector);
            paciente.IdPaciente = turno.IdPaciente;
            turno.Paciente = paciente;
            turno.DNIPaciente = paciente.DNI;

            return turno;
        }

        private BE_Paciente MapearPaciente(SqlDataReader lector)
        {
            return new BE_Paciente
            {
                Nombre = lector["NombrePaciente"].ToString(),
                Apellido = lector["ApellidoPaciente"].ToString(),
                DNI = lector["DNI"].ToString(),
                NroHistoriaClinica = Convert.ToInt32(lector["NroHistoriaClinica"]),
                FechaNacimiento = lector["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(lector["FechaNacimiento"]),
                Telefono = lector["Telefono"] as string,
                Email = lector["Email"] as string,
                FechaAperturaHC = Convert.ToDateTime(lector["FechaAperturaHC"])
            };
        }
    }
}
