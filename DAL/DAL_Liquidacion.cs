using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    // RF2 - Liquidacion de honorarios: turnos trabajados y tablas Liquidacion y
    // LiquidacionDetalle (CU-02.2 y CU-02.3).
    public class DAL_Liquidacion
    {
        DAL_Conexion conex = new DAL_Conexion();

        // CU-02.2: actividad de cada medico y especialidad con turnos Atendidos, Ausentes o Cancelados
        // entre "desde" y "hasta" inclusive. Los dias trabajados son los dias con algun turno Atendido o Ausente.
        public List<BE_TurnosTrabajados> ObtenerTurnosTrabajados(DateTime desde, DateTime hasta)
        {
            try
            {
                List<BE_TurnosTrabajados> lista = new List<BE_TurnosTrabajados>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT mh.IdMedico, u.Nombre, u.Apellido, md.Matricula,
                               e.IdEspecialidad, e.Nombre AS NombreEspecialidad, e.ValorTurno, e.PorcentajeAusente,
                               COUNT(DISTINCT CASE WHEN t.Estado IN ('Atendido', 'Ausente') THEN t.Fecha END) AS DiasTrabajados,
                               SUM(CASE WHEN t.Estado = 'Atendido' THEN 1 ELSE 0 END) AS TurnosAtendidos,
                               SUM(CASE WHEN t.Estado = 'Ausente' THEN 1 ELSE 0 END) AS TurnosAusentes,
                               SUM(CASE WHEN t.Estado = 'Cancelado' THEN 1 ELSE 0 END) AS TurnosCancelados
                        FROM Turno t
                        INNER JOIN Medico_Horario mh ON mh.IdHorario = t.IdHorario
                        INNER JOIN Medico md ON md.IdUsuario = mh.IdMedico
                        INNER JOIN Usuario u ON u.IdUsuario = mh.IdMedico
                        INNER JOIN Especialidades e ON e.IdEspecialidad = mh.IdEspecialidad
                        WHERE t.Fecha BETWEEN @desde AND @hasta
                          AND t.Estado IN ('Atendido', 'Ausente', 'Cancelado')
                        GROUP BY mh.IdMedico, u.Nombre, u.Apellido, md.Matricula, e.IdEspecialidad, e.Nombre,
                                 e.ValorTurno, e.PorcentajeAusente
                        ORDER BY u.Apellido, u.Nombre, e.Nombre", conexion);
                    cmd.Parameters.Add("@desde", SqlDbType.Date).Value = desde.Date;
                    cmd.Parameters.Add("@hasta", SqlDbType.Date).Value = hasta.Date;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            lista.Add(new BE_TurnosTrabajados
                            {
                                IdMedico = Convert.ToInt32(lector["IdMedico"]),
                                NombreMedico = lector["Nombre"] + " " + lector["Apellido"],
                                Matricula = lector["Matricula"].ToString(),
                                IdEspecialidad = Convert.ToInt32(lector["IdEspecialidad"]),
                                NombreEspecialidad = lector["NombreEspecialidad"].ToString(),
                                ValorTurno = Convert.ToDecimal(lector["ValorTurno"]),
                                PorcentajeAusente = Convert.ToDecimal(lector["PorcentajeAusente"]),
                                DiasTrabajados = Convert.ToInt32(lector["DiasTrabajados"]),
                                TurnosAtendidos = Convert.ToInt32(lector["TurnosAtendidos"]),
                                TurnosAusentes = Convert.ToInt32(lector["TurnosAusentes"]),
                                TurnosCancelados = Convert.ToInt32(lector["TurnosCancelados"])
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

        public bool ExisteLiquidacion(int idMedico, int mes, int anio)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(1) FROM Liquidacion
                        WHERE IdMedico = @idMedico AND Mes = @mes AND Anio = @anio", conexion);
                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;
                    cmd.Parameters.Add("@mes", SqlDbType.TinyInt).Value = mes;
                    cmd.Parameters.Add("@anio", SqlDbType.SmallInt).Value = anio;
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Registra la liquidacion y su detalle en una unica transaccion. Completa en el objeto el Id,
        // el numero y la fecha de emision que asigna la base.
        public void Insertar(BE_Liquidacion liquidacion)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();
                SqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO Liquidacion (IdMedico, Mes, Anio)
                        OUTPUT INSERTED.IdLiquidacion, INSERTED.NroLiquidacion, INSERTED.FechaEmision
                        VALUES (@idMedico, @mes, @anio)", conexion, transaccion);
                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = liquidacion.IdMedico;
                    cmd.Parameters.Add("@mes", SqlDbType.TinyInt).Value = liquidacion.Mes;
                    cmd.Parameters.Add("@anio", SqlDbType.SmallInt).Value = liquidacion.Anio;

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        lector.Read();
                        liquidacion.IdLiquidacion = Convert.ToInt32(lector["IdLiquidacion"]);
                        liquidacion.NroLiquidacion = Convert.ToInt32(lector["NroLiquidacion"]);
                        liquidacion.FechaEmision = Convert.ToDateTime(lector["FechaEmision"]);
                    }

                    foreach (BE_LiquidacionDetalle detalle in liquidacion.Detalles)
                    {
                        SqlCommand cmdDetalle = new SqlCommand(@"
                            INSERT INTO LiquidacionDetalle (IdLiquidacion, IdEspecialidad, DiasTrabajados, TurnosAtendidos,
                                                            TurnosAusentes, ValorTurnoAplicado, PorcentajeAusenteAplicado)
                            OUTPUT INSERTED.IdDetalle
                            VALUES (@idLiquidacion, @idEspecialidad, @dias, @atendidos, @ausentes, @valorTurno, @porcentaje)", conexion, transaccion);
                        cmdDetalle.Parameters.Add("@idLiquidacion", SqlDbType.Int).Value = liquidacion.IdLiquidacion;
                        cmdDetalle.Parameters.Add("@idEspecialidad", SqlDbType.Int).Value = detalle.IdEspecialidad;
                        cmdDetalle.Parameters.Add("@dias", SqlDbType.Int).Value = detalle.DiasTrabajados;
                        cmdDetalle.Parameters.Add("@atendidos", SqlDbType.Int).Value = detalle.TurnosAtendidos;
                        cmdDetalle.Parameters.Add("@ausentes", SqlDbType.Int).Value = detalle.TurnosAusentes;
                        SqlParameter valor = cmdDetalle.Parameters.Add("@valorTurno", SqlDbType.Decimal);
                        valor.Precision = 12;
                        valor.Scale = 2;
                        valor.Value = detalle.ValorTurnoAplicado;
                        SqlParameter porcentaje = cmdDetalle.Parameters.Add("@porcentaje", SqlDbType.Decimal);
                        porcentaje.Precision = 5;
                        porcentaje.Scale = 2;
                        porcentaje.Value = detalle.PorcentajeAusenteAplicado;
                        detalle.IdDetalle = (int)cmdDetalle.ExecuteScalar();
                    }

                    transaccion.Commit();
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        public List<BE_Liquidacion> ListarPorPeriodo(int mes, int anio)
        {
            return Listar("l.Mes = @mes AND l.Anio = @anio", cmd =>
            {
                cmd.Parameters.Add("@mes", SqlDbType.TinyInt).Value = mes;
                cmd.Parameters.Add("@anio", SqlDbType.SmallInt).Value = anio;
            });
        }

        public BE_Liquidacion ObtenerPorId(int idLiquidacion)
        {
            return Listar("l.IdLiquidacion = @idLiquidacion", cmd =>
                cmd.Parameters.Add("@idLiquidacion", SqlDbType.Int).Value = idLiquidacion).FirstOrDefault();
        }

        // Liquidaciones con su medico y su detalle (una fila por especialidad liquidada).
        private List<BE_Liquidacion> Listar(string filtro, Action<SqlCommand> parametros)
        {
            try
            {
                Dictionary<int, BE_Liquidacion> liquidaciones = new Dictionary<int, BE_Liquidacion>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT l.IdLiquidacion, l.NroLiquidacion, l.IdMedico, l.Mes, l.Anio, l.FechaEmision,
                               u.Nombre, u.Apellido, md.Matricula,
                               d.IdDetalle, d.IdEspecialidad, e.Nombre AS NombreEspecialidad,
                               d.DiasTrabajados, d.TurnosAtendidos, d.TurnosAusentes,
                               d.ValorTurnoAplicado, d.PorcentajeAusenteAplicado
                        FROM Liquidacion l
                        INNER JOIN Medico md ON md.IdUsuario = l.IdMedico
                        INNER JOIN Usuario u ON u.IdUsuario = l.IdMedico
                        INNER JOIN LiquidacionDetalle d ON d.IdLiquidacion = l.IdLiquidacion
                        INNER JOIN Especialidades e ON e.IdEspecialidad = d.IdEspecialidad
                        WHERE " + filtro + @"
                        ORDER BY l.NroLiquidacion, e.Nombre", conexion);
                    parametros(cmd);

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            int id = Convert.ToInt32(lector["IdLiquidacion"]);
                            BE_Liquidacion liquidacion;
                            if (!liquidaciones.TryGetValue(id, out liquidacion))
                            {
                                liquidacion = new BE_Liquidacion
                                {
                                    IdLiquidacion = id,
                                    NroLiquidacion = Convert.ToInt32(lector["NroLiquidacion"]),
                                    IdMedico = Convert.ToInt32(lector["IdMedico"]),
                                    NombreMedico = lector["Nombre"] + " " + lector["Apellido"],
                                    Matricula = lector["Matricula"].ToString(),
                                    Mes = Convert.ToInt32(lector["Mes"]),
                                    Anio = Convert.ToInt32(lector["Anio"]),
                                    FechaEmision = Convert.ToDateTime(lector["FechaEmision"])
                                };
                                liquidaciones.Add(id, liquidacion);
                            }

                            liquidacion.Detalles.Add(new BE_LiquidacionDetalle
                            {
                                IdDetalle = Convert.ToInt32(lector["IdDetalle"]),
                                IdEspecialidad = Convert.ToInt32(lector["IdEspecialidad"]),
                                NombreEspecialidad = lector["NombreEspecialidad"].ToString(),
                                DiasTrabajados = Convert.ToInt32(lector["DiasTrabajados"]),
                                TurnosAtendidos = Convert.ToInt32(lector["TurnosAtendidos"]),
                                TurnosAusentes = Convert.ToInt32(lector["TurnosAusentes"]),
                                ValorTurnoAplicado = Convert.ToDecimal(lector["ValorTurnoAplicado"]),
                                PorcentajeAusenteAplicado = Convert.ToDecimal(lector["PorcentajeAusenteAplicado"])
                            });
                        }
                    }
                }
                return liquidaciones.Values.OrderBy(l => l.NroLiquidacion).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
