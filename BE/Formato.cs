using System.Globalization;

namespace BE
{
    // Formato argentino para importes en pesos y porcentajes, independiente de la configuracion regional
    // del equipo donde se instale el sistema, y el calculo del importe de los honorarios (RN2.2).
    public static class Formato
    {
        public static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-AR");

        // Ejemplo: $ 15.000,50
        public static string Moneda(decimal importe)
        {
            return importe.ToString("C2", Cultura);
        }

        // Ejemplo: 50 %
        public static string Porcentaje(decimal valor)
        {
            return valor.ToString("0.##", Cultura) + " %";
        }

        // RN2.2: el turno atendido paga el valor del turno; el ausente, el porcentaje indicado de ese valor;
        // el cancelado no paga. Se redondea a centavos.
        public static decimal Importe(int atendidos, int ausentes, decimal valorTurno, decimal porcentajeAusente)
        {
            return decimal.Round(atendidos * valorTurno + ausentes * valorTurno * porcentajeAusente / 100m, 2);
        }
    }
}
