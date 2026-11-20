using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace BLL
{
    public class BLL_Liquidacion
    {
        DAL_Liquidacion dal_liquidacion = new DAL_Liquidacion();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();
        ReciboLiquidacionPdf reciboPdf = new ReciboLiquidacionPdf();

        private const string Modulo = "HONORARIOS";

        public List<BE_MedicoPeriodo> ObtenerMedicosDelPeriodo(int mes, int anio)
        {
            ValidarPeriodo(mes, anio);
            DateTime desde = new DateTime(anio, mes, 1);
            try
            {
                List<BE_TurnosTrabajados> turnos = dal_liquidacion.ObtenerTurnosTrabajados(desde, desde.AddMonths(1).AddDays(-1));
                List<int> liquidados = dal_liquidacion.ListarPorPeriodo(mes, anio).Select(l => l.IdMedico).ToList();

                return turnos.GroupBy(t => t.IdMedico)
                             .Select(g => new BE_MedicoPeriodo
                             {
                                 IdMedico = g.Key,
                                 NombreMedico = g.First().NombreMedico,
                                 Matricula = g.First().Matricula,
                                 TurnosTrabajados = g.ToList(),
                                 Liquidado = liquidados.Contains(g.Key)
                             })
                             .OrderBy(m => m.NombreMedico)
                             .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudieron obtener los turnos trabajados: " + ex.Message, ex);
            }
        }

        public BE_Liquidacion GenerarLiquidacion(int idMedico, int mes, int anio)
        {
            ValidarPeriodoFinalizado(mes, anio);
            if (dal_liquidacion.ExisteLiquidacion(idMedico, mes, anio))
                throw new ArgumentException("El médico ya tiene liquidación de ese período.");

            DateTime desde = new DateTime(anio, mes, 1);
            List<BE_TurnosTrabajados> turnos = dal_liquidacion.ObtenerTurnosTrabajados(desde, desde.AddMonths(1).AddDays(-1))
                .Where(t => t.IdMedico == idMedico && t.TurnosAtendidos + t.TurnosAusentes > 0).ToList();
            if (turnos.Count == 0)
                throw new ArgumentException("No hay turnos para liquidar: el médico no tiene turnos atendidos ni ausentes en el período.");

            ValidarValoresTurno(turnos);

            BE_Liquidacion liquidacion = ArmarLiquidacion(idMedico, turnos);
            liquidacion.Mes = mes;
            liquidacion.Anio = anio;
            try
            {
                dal_liquidacion.Insertar(liquidacion);
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new ArgumentException("El médico ya tiene liquidación de ese período.");
            }

            bll_bitacora.RegistrarEvento(Sesion.Instancia().UsuarioActual?.IdUsuario, AccionBitacora.LIQUIDACION_GENERADA, Modulo,
                $"Liquidación N° {liquidacion.NroLiquidacion} de {liquidacion.NombreMedico} ({liquidacion.PeriodoTexto}): " +
                $"{liquidacion.TotalAtendidos} atendidos, {liquidacion.TotalAusentes} ausentes, importe {Formato.Moneda(liquidacion.ImporteTotal)}.");
            return liquidacion;
        }

        public List<BE_Liquidacion> ListarLiquidaciones(int mes, int anio)
        {
            ValidarPeriodo(mes, anio);
            try
            {
                return dal_liquidacion.ListarPorPeriodo(mes, anio);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudieron obtener las liquidaciones: " + ex.Message, ex);
            }
        }

        public void EmitirRecibo(int idLiquidacion, string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                throw new ArgumentException("Indique dónde guardar el recibo.");

            BE_Liquidacion liquidacion = dal_liquidacion.ObtenerPorId(idLiquidacion);
            if (liquidacion == null)
                throw new ArgumentException("La liquidación no existe.");

            try
            {
                reciboPdf.Generar(liquidacion, ruta);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                throw new ArgumentException("No se pudo guardar el recibo. Si el archivo está abierto, ciérrelo; si no, " +
                                            "elija otra ubicación. Detalle: " + ex.Message);
            }

            bll_bitacora.RegistrarEvento(Sesion.Instancia().UsuarioActual?.IdUsuario, AccionBitacora.RECIBO_EMITIDO, Modulo,
                $"Recibo de la liquidación N° {liquidacion.NroLiquidacion} de {liquidacion.NombreMedico} ({liquidacion.PeriodoTexto}).");
        }

        private void ValidarPeriodoFinalizado(int mes, int anio)
        {
            ValidarPeriodo(mes, anio);
            if (new DateTime(anio, mes, 1).AddMonths(1) > DateTime.Today)
                throw new ArgumentException("Solo se liquidan meses cerrados: el período elegido todavía no finalizó.");
        }

        private void ValidarValoresTurno(List<BE_TurnosTrabajados> turnos)
        {
            List<string> sinValor = turnos.Where(t => t.ValorTurno <= 0)
                                          .Select(t => t.NombreEspecialidad)
                                          .Distinct()
                                          .OrderBy(n => n)
                                          .ToList();
            if (sinValor.Count > 0)
                throw new ArgumentException("No se generó la liquidación. Cargue el valor del turno de: " +
                                            string.Join(", ", sinValor) + " (opción Valor del turno).");
        }

        private BE_Liquidacion ArmarLiquidacion(int idMedico, List<BE_TurnosTrabajados> turnos)
        {
            List<BE_TurnosTrabajados> delMedico = turnos.Where(t => t.IdMedico == idMedico).ToList();
            BE_Liquidacion liquidacion = new BE_Liquidacion
            {
                IdMedico = idMedico,
                NombreMedico = delMedico[0].NombreMedico,
                Matricula = delMedico[0].Matricula
            };
            foreach (BE_TurnosTrabajados t in delMedico)
            {
                liquidacion.Detalles.Add(new BE_LiquidacionDetalle
                {
                    IdEspecialidad = t.IdEspecialidad,
                    NombreEspecialidad = t.NombreEspecialidad,
                    DiasTrabajados = t.DiasTrabajados,
                    TurnosAtendidos = t.TurnosAtendidos,
                    TurnosAusentes = t.TurnosAusentes,
                    ValorTurnoAplicado = t.ValorTurno,
                    PorcentajeAusenteAplicado = t.PorcentajeAusente
                });
            }
            return liquidacion;
        }

        private static void ValidarPeriodo(int mes, int anio)
        {
            if (mes < 1 || mes > 12)
                throw new ArgumentException("El mes debe estar entre 1 y 12.");
            if (anio < 2000 || anio > 2100)
                throw new ArgumentException("El año no es válido.");
        }
    }
}
