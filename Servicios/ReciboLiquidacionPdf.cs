using BE;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Linq;

namespace Servicios
{
    // CU-02.3 / A02: recibo de liquidacion de honorarios en PDF. Encapsula la libreria PDFsharp
    // (de terceros, codigo abierto) para que ninguna otra capa dependa de ella.
    public class ReciboLiquidacionPdf
    {
        private const string Sanatorio = "Sanatorio Córdoba";
        private const string Sistema = "Sistema de Gestión de Turnos y Honorarios Médicos";

        private const double Margen = 50;
        private const double AnchoUtil = 495;   // A4 (595 pt) menos los dos margenes

        // Columnas del detalle: especialidad, dias, atendidos, ausentes, valor del turno, % por ausencia y subtotal.
        private static readonly double[] ColAncho = { 118, 38, 60, 56, 82, 64, 77 };
        private static readonly double[] ColX = ColAncho.Select((a, i) => Margen + ColAncho.Take(i).Sum()).ToArray();

        private readonly XFont fuenteTitulo = new XFont("Arial", 16, XFontStyle.Bold);
        private readonly XFont fuenteSubtitulo = new XFont("Arial", 12, XFontStyle.Bold);
        private readonly XFont fuenteNegrita = new XFont("Arial", 10, XFontStyle.Bold);
        private readonly XFont fuenteNormal = new XFont("Arial", 10, XFontStyle.Regular);
        private readonly XFont fuentePie = new XFont("Arial", 8, XFontStyle.Italic);

        public void Generar(BE_Liquidacion liquidacion, string ruta)
        {
            using (PdfDocument documento = new PdfDocument())
            {
                documento.Info.Title = "Recibo de liquidación N° " + liquidacion.NroLiquidacion;
                documento.Info.Author = Sanatorio;

                PdfPage pagina = documento.AddPage();
                pagina.Size = PageSize.A4;

                using (XGraphics gfx = XGraphics.FromPdfPage(pagina))
                {
                    DibujarEncabezado(gfx, liquidacion);
                    DibujarDetalle(gfx, liquidacion);
                    gfx.DrawString(Sistema, fuentePie, XBrushes.Gray,
                        new XRect(Margen, pagina.Height.Point - 40, AnchoUtil, 12), XStringFormats.Center);
                }

                documento.Save(ruta);
            }
        }

        // Datos del sanatorio, de la liquidacion y del medico.
        private void DibujarEncabezado(XGraphics gfx, BE_Liquidacion liquidacion)
        {
            gfx.DrawString(Sanatorio, fuenteTitulo, XBrushes.Black, new XRect(Margen, 50, AnchoUtil, 22), XStringFormats.TopLeft);
            gfx.DrawString("Recibo de liquidación de honorarios médicos", fuenteSubtitulo, XBrushes.Black,
                new XRect(Margen, 76, AnchoUtil, 18), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, Margen, 100, Margen + AnchoUtil, 100);

            double y = 115;
            Dato(gfx, "Liquidación N°:", liquidacion.NroLiquidacion.ToString(), y);
            Dato(gfx, "Fecha de emisión:", liquidacion.FechaEmision.ToString("dd/MM/yyyy"), y += 18);
            Dato(gfx, "Período:", liquidacion.PeriodoTexto, y += 18);
            Dato(gfx, "Médico:", liquidacion.NombreMedico, y += 18);
            Dato(gfx, "Matrícula:", liquidacion.Matricula, y += 18);
        }

        // Detalle por especialidad (dias trabajados, turnos atendidos y ausentes, valor del turno, porcentaje
        // por ausencia y subtotal) y totales. Los dias no se suman: un dia puede tener turnos de dos especialidades.
        private void DibujarDetalle(XGraphics gfx, BE_Liquidacion liquidacion)
        {
            double y = 230;
            gfx.DrawRectangle(XBrushes.LightGray, Margen, y, AnchoUtil, 20);
            string[] encabezados = { "Especialidad", "Días", "Atendidos", "Ausentes", "Valor turno", "% ausencia", "Subtotal" };
            for (int i = 0; i < encabezados.Length; i++)
                Celda(gfx, encabezados[i], i, y, fuenteNegrita, i > 0);
            y += 20;

            foreach (BE_LiquidacionDetalle detalle in liquidacion.Detalles)
            {
                Celda(gfx, detalle.NombreEspecialidad, 0, y, fuenteNormal, false);
                Celda(gfx, detalle.DiasTrabajados.ToString(), 1, y, fuenteNormal, true);
                Celda(gfx, detalle.TurnosAtendidos.ToString(), 2, y, fuenteNormal, true);
                Celda(gfx, detalle.TurnosAusentes.ToString(), 3, y, fuenteNormal, true);
                Celda(gfx, Formato.Moneda(detalle.ValorTurnoAplicado), 4, y, fuenteNormal, true);
                Celda(gfx, Formato.Porcentaje(detalle.PorcentajeAusenteAplicado), 5, y, fuenteNormal, true);
                Celda(gfx, Formato.Moneda(detalle.Subtotal), 6, y, fuenteNormal, true);
                y += 20;
                gfx.DrawLine(XPens.LightGray, Margen, y, Margen + AnchoUtil, y);
            }

            y += 8;
            gfx.DrawLine(XPens.Black, Margen, y, Margen + AnchoUtil, y);
            y += 6;
            Celda(gfx, "Total", 0, y, fuenteNegrita, false);
            Celda(gfx, liquidacion.TotalAtendidos.ToString(), 2, y, fuenteNegrita, true);
            Celda(gfx, liquidacion.TotalAusentes.ToString(), 3, y, fuenteNegrita, true);
            Celda(gfx, Formato.Moneda(liquidacion.ImporteTotal), 6, y, fuenteNegrita, true);

            y += 34;
            gfx.DrawString("Turno atendido: valor del turno. Turno ausente: el porcentaje indicado del valor del turno. " +
                           "Turno cancelado: no se paga.", fuentePie, XBrushes.Gray,
                new XRect(Margen, y, AnchoUtil, 12), XStringFormats.TopLeft);
        }

        private void Dato(XGraphics gfx, string etiqueta, string valor, double y)
        {
            gfx.DrawString(etiqueta, fuenteNegrita, XBrushes.Black, new XRect(Margen, y, 120, 14), XStringFormats.TopLeft);
            gfx.DrawString(valor ?? "", fuenteNormal, XBrushes.Black, new XRect(Margen + 120, y, AnchoUtil - 120, 14), XStringFormats.TopLeft);
        }

        private void Celda(XGraphics gfx, string texto, int columna, double y, XFont fuente, bool derecha)
        {
            XRect rect = new XRect(ColX[columna] + 4, y, ColAncho[columna] - 8, 20);
            gfx.DrawString(texto, fuente, XBrushes.Black, rect, derecha ? XStringFormats.CenterRight : XStringFormats.CenterLeft);
        }
    }
}
