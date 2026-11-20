using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    public class BE_Liquidacion
    {
        public int IdLiquidacion { get; set; }

        public int NroLiquidacion { get; set; }

        public int IdMedico { get; set; }

        public string NombreMedico { get; set; }

        public string Matricula { get; set; }

        public int Mes { get; set; }

        public int Anio { get; set; }

        public DateTime FechaEmision { get; set; }

        public List<BE_LiquidacionDetalle> Detalles { get; set; } = new List<BE_LiquidacionDetalle>();

        public int TotalAtendidos { get { return Detalles.Sum(d => d.TurnosAtendidos); } }

        public int TotalAusentes { get { return Detalles.Sum(d => d.TurnosAusentes); } }

        public decimal ImporteTotal { get { return Detalles.Sum(d => d.Subtotal); } }

        public string PeriodoTexto
        {
            get
            {
                string mes = Formato.Cultura.DateTimeFormat.GetMonthName(Mes);
                return char.ToUpper(mes[0]) + mes.Substring(1) + " " + Anio;
            }
        }

        public string ImporteTotalTexto { get { return Formato.Moneda(ImporteTotal); } }

        public string FechaEmisionTexto { get { return FechaEmision.ToString("dd/MM/yyyy HH:mm"); } }
    }
}
