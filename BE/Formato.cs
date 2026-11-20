using System.Globalization;

namespace BE
{
    public static class Formato
    {
        public static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-AR");

        public static string Moneda(decimal importe)
        {
            return importe.ToString("C2", Cultura);
        }

        public static string Porcentaje(decimal valor)
        {
            return valor.ToString("0.##", Cultura) + " %";
        }

        public static decimal Importe(int atendidos, int ausentes, decimal valorTurno, decimal porcentajeAusente)
        {
            return decimal.Round(atendidos * valorTurno + ausentes * valorTurno * porcentajeAusente / 100m, 2);
        }
    }
}
