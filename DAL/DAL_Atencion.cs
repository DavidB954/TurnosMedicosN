using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    // Acceso a datos del Proceso 2 (Atencion y Agenda Medica).
    public class DAL_Atencion
    {
        DAL_Conexion conexion = new DAL_Conexion();

        // Columnas comunes de "turno + horario + paciente" usadas por la agenda y por la atencion.
        private const string SelectTurnoConPaciente = @"
            SELECT t.IdTurno, t.IdPaciente, t.IdHorario, t.Fecha, t.Estado,
                   mh.IdMedico, mh.HoraInicio, mh.HoraFin,
                   up.Nombre AS NombrePaciente, up.Apellido AS ApellidoPaciente,
                   um.Nombre AS NombreMedico, um.Apellido AS ApellidoMedico,
                   p.DNI, p.NroHistoriaClinica, p.FechaNacimiento, p.Telefono, p.FechaAperturaHC,
                   md.Matricula
            FROM Turno t
            INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
            INNER JOIN Usuario up ON up.IdUsuario = t.IdPaciente
            INNER JOIN Usuario um ON um.IdUsuario = mh.IdMedico
            LEFT JOIN Paciente p ON p.IdUsuario = t.IdPaciente
            LEFT JOIN Medico md ON md.IdUsuario = mh.IdMedico";

        // Turnos del medico para una fecha, sin los Cancelados (liberaron el horario), por hora.
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

        // RF 02.1 + RF 02.5 en UNA transaccion: AtencionMedica, Receta + items (si hay) y el
        // cambio de estado del turno a Atendido. El trigger de Turno guarda la copia en
        // BitacoraEventosTurno dentro de esta misma transaccion. Ante cualquier error: ROLLBACK.
        public void GuardarAtencion(BE_AtencionMedica a)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                cn.Open();
                using (SqlTransaction transaccion = cn.BeginTransaction())
                {
                    try
                    {
                        // El que registra es el medico del turno (la BLL ya valido que es el logueado).
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

        // El N° de receta lo genera la base con SEQ_NroReceta: R-AAAA-NNNNNN
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

        // Cambia el estado de un turno que sigue Confirmado. Si otro proceso ya lo cambio
        // (lo atendieron, vencio, etc.) no se pisa: se informa con una excepcion.
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

        // Misma operacion, dentro de una conexion/transaccion abierta por quien llama.
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

        // "Finalizar atencion": pasa a Ausente, en una sola transaccion, todos los turnos del
        // medico para esa fecha que siguen Confirmados. Devuelve los ids marcados.
        public List<int> MarcarAusentesPendientes(int idMedico, DateTime fecha)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                cn.Open();
                using (SqlTransaction transaccion = cn.BeginTransaction())
                {
                    try
                    {
                        DAL_Conexion.EstablecerUsuarioAuditoria(cn, transaccion, idMedico);

                        // Turno tiene triggers (BitacoraEventosTurno): SQL Server exige OUTPUT ... INTO.
                        SqlCommand cmd = new SqlCommand(@"
                            DECLARE @marcados TABLE (IdTurno INT);
                            UPDATE t SET t.Estado = 'Ausente'
                            OUTPUT INSERTED.IdTurno INTO @marcados
                            FROM Turno t
                            INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                            WHERE mh.IdMedico = @idMedico AND t.Fecha = @fecha AND t.Estado = 'Confirmado';
                            SELECT IdTurno FROM @marcados;", cn, transaccion);
                        cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                        cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = fecha.Date;

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

        // Busqueda por nombre y apellido, N° de Historia Clinica o DNI.
        public List<BE_Paciente> BuscarPacientes(string texto)
        {
            try
            {
                List<BE_Paciente> lista = new List<BE_Paciente>();
                using (SqlConnection cn = conexion.ObtenerConexion())
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT u.IdUsuario, u.Nombre, u.Apellido, u.Email,
                               p.DNI, p.NroHistoriaClinica, p.FechaNacimiento, p.Telefono, p.FechaAperturaHC
                        FROM Paciente p
                        INNER JOIN Usuario u ON u.IdUsuario = p.IdUsuario
                        WHERE (u.Nombre + ' ' + u.Apellido) LIKE @patron
                           OR (u.Apellido + ' ' + u.Nombre) LIKE @patron
                           OR p.DNI LIKE @patron
                           OR (@numero IS NOT NULL AND p.NroHistoriaClinica = @numero)
                        ORDER BY u.Apellido, u.Nombre", cn);

                    int numero;
                    bool esNumero = int.TryParse(texto, out numero);
                    cmd.Parameters.Add("@patron", SqlDbType.VarChar, 120).Value = "%" + texto + "%";
                    cmd.Parameters.Add("@numero", SqlDbType.Int).Value = esNumero ? (object)numero : DBNull.Value;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            BE_Paciente paciente = MapearPaciente(lector);
                            paciente.IdUsuario = Convert.ToInt32(lector["IdUsuario"]);
                            paciente.Nombre = lector["Nombre"].ToString();
                            paciente.Apellido = lector["Apellido"].ToString();
                            paciente.Email = lector["Email"].ToString();
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

        // Atenciones del paciente, de la mas reciente a la mas antigua, con medico,
        // especialidad y receta (si tiene).
        public List<BE_AtencionMedica> ObtenerHistoriaClinica(int nroHC)
        {
            return ObtenerAtenciones("p.NroHistoriaClinica = @valor", nroHC);
        }

        // La atencion de un turno puntual (lo que ve el paciente en "Mis Turnos"), o null si no tiene.
        public BE_AtencionMedica ObtenerAtencionPorTurno(int idTurno)
        {
            return ObtenerAtenciones("t.IdTurno = @valor", idTurno).FirstOrDefault();
        }

        // Todas las atenciones del paciente que tienen receta (menu "Mis recetas"), de la mas reciente a la mas antigua.
        public List<BE_AtencionMedica> ObtenerRecetasPorPaciente(int idPaciente)
        {
            return ObtenerAtenciones("t.IdPaciente = @valor AND r.IdReceta IS NOT NULL", idPaciente);
        }

        // Consulta comun: la condicion es fija (la arma esta clase), el valor va siempre por parametro.
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
                               up.Nombre AS NombrePaciente, up.Apellido AS ApellidoPaciente,
                               p.DNI, p.NroHistoriaClinica, md.Matricula
                        FROM AtencionMedica am
                        INNER JOIN Turno t ON t.IdTurno = am.IdTurno
                        INNER JOIN Paciente p ON p.IdUsuario = t.IdPaciente
                        INNER JOIN Usuario up ON up.IdUsuario = t.IdPaciente
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
                                IdUsuario = turno.IdPaciente,
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

            // Los ids son enteros generados por la base: se arma la lista IN sin riesgo de inyeccion.
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

        // Todas las atenciones con los campos del DVH, para verificar integridad y calcular el DVV.
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
            paciente.IdUsuario = turno.IdPaciente;
            paciente.Nombre = lector["NombrePaciente"].ToString();
            paciente.Apellido = lector["ApellidoPaciente"].ToString();
            turno.Paciente = paciente;

            return turno;
        }

        // Datos propios de la tabla Paciente (pueden venir NULL por el LEFT JOIN si el usuario
        // todavia no tiene Historia Clinica: en ese caso NroHistoriaClinica queda en 0).
        private BE_Paciente MapearPaciente(SqlDataReader lector)
        {
            BE_Paciente paciente = new BE_Paciente();
            if (lector["NroHistoriaClinica"] != DBNull.Value)
            {
                paciente.DNI = lector["DNI"].ToString();
                paciente.NroHistoriaClinica = Convert.ToInt32(lector["NroHistoriaClinica"]);
                paciente.FechaNacimiento = lector["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(lector["FechaNacimiento"]);
                paciente.Telefono = lector["Telefono"] as string;
                paciente.FechaAperturaHC = Convert.ToDateTime(lector["FechaAperturaHC"]);
            }
            return paciente;
        }
    }
}
