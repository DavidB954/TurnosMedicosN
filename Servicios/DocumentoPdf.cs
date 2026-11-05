using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Servicios
{
    // Generador de PDF minimo, sin librerias externas: paginas A4 con texto (Helvetica, con
    // acentos y ñ), lineas y rectangulos. Alcanza para documentos simples como la receta.
    // Las coordenadas se pasan en puntos (1/72") desde el borde SUPERIOR izquierdo.
    public class DocumentoPdf
    {
        public const float AnchoPagina = 595f;   // A4
        public const float AltoPagina = 842f;

        public enum Fuente { Normal, Negrita, Cursiva }

        private readonly List<StringBuilder> paginas = new List<StringBuilder>();
        private StringBuilder actual;

        // Windows-1252 = WinAnsiEncoding del PDF (acentos, ñ, °, €...)
        private static readonly Encoding Ansi = Encoding.GetEncoding(1252);

        public DocumentoPdf()
        {
            NuevaPagina();
        }

        public void NuevaPagina()
        {
            actual = new StringBuilder();
            paginas.Add(actual);
        }

        public void Texto(float x, float y, string texto, float tamano, Fuente fuente = Fuente.Normal, float gris = 0f)
        {
            if (string.IsNullOrEmpty(texto))
                return;

            string nombreFuente = fuente == Fuente.Negrita ? "F2" : fuente == Fuente.Cursiva ? "F3" : "F1";
            actual.AppendFormat(CultureInfo.InvariantCulture, "BT {0} g /{1} {2} Tf {3} {4} Td ({5}) Tj ET\n",
                N(gris), nombreFuente, N(tamano), N(x), N(AltoPagina - y - tamano), Escapar(texto));
        }

        // Escribe el texto partido en renglones que no superan el ancho. Devuelve la altura usada.
        public float TextoAjustado(float x, float y, float ancho, string texto, float tamano, Fuente fuente = Fuente.Normal, float interlineado = 1.25f)
        {
            List<string> renglones = Partir(texto ?? "", ancho, tamano, fuente);
            for (int i = 0; i < renglones.Count; i++)
                Texto(x, y + i * tamano * interlineado, renglones[i], tamano, fuente);
            return Math.Max(1, renglones.Count) * tamano * interlineado;
        }

        public void Linea(float x1, float y1, float x2, float y2, float grosor = 0.5f, float gris = 0f)
        {
            actual.AppendFormat(CultureInfo.InvariantCulture, "{0} G {1} w {2} {3} m {4} {5} l S\n",
                N(gris), N(grosor), N(x1), N(AltoPagina - y1), N(x2), N(AltoPagina - y2));
        }

        // Rectangulo relleno de color (r, g, b entre 0 y 1).
        public void Rectangulo(float x, float y, float ancho, float alto, float r, float g, float b)
        {
            actual.AppendFormat(CultureInfo.InvariantCulture, "{0} {1} {2} rg {3} {4} {5} {6} re f 0 g\n",
                N(r), N(g), N(b), N(x), N(AltoPagina - y - alto), N(ancho), N(alto));
        }

        // Ancho aproximado del texto en Helvetica (promedio por tipo de caracter).
        public static float AnchoTexto(string texto, float tamano, Fuente fuente = Fuente.Normal)
        {
            float total = 0;
            foreach (char c in texto)
            {
                float w;
                if (c == ' ') w = 0.278f;
                else if ("il.,:;'|!".IndexOf(c) >= 0) w = 0.25f;
                else if ("fjrt()-/".IndexOf(c) >= 0) w = 0.35f;
                else if ("mwMW".IndexOf(c) >= 0) w = 0.85f;
                else if (char.IsUpper(c)) w = 0.68f;
                else if (char.IsDigit(c)) w = 0.556f;
                else w = 0.54f;
                total += w;
            }
            return total * tamano * (fuente == Fuente.Negrita ? 1.06f : 1f);
        }

        public static List<string> Partir(string texto, float ancho, float tamano, Fuente fuente = Fuente.Normal)
        {
            List<string> renglones = new List<string>();
            foreach (string parrafo in texto.Replace("\r", "").Split('\n'))
            {
                string renglon = "";
                foreach (string palabra in parrafo.Split(' '))
                {
                    string prueba = renglon.Length == 0 ? palabra : renglon + " " + palabra;
                    if (AnchoTexto(prueba, tamano, fuente) <= ancho || renglon.Length == 0)
                        renglon = prueba;
                    else
                    {
                        renglones.Add(renglon);
                        renglon = palabra;
                    }
                }
                renglones.Add(renglon);
            }
            return renglones;
        }

        public void Guardar(string ruta)
        {
            File.WriteAllBytes(ruta, Generar());
        }

        public byte[] Generar()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                List<long> posiciones = new List<long>();
                Escribir(ms, "%PDF-1.4\n%âãÏÓ\n");

                int cantPaginas = paginas.Count;
                // Objetos: 1 catalogo, 2 paginas, 3-5 fuentes, luego (pagina, contenido) por cada pagina
                int primerPagina = 6;

                Objeto(ms, posiciones, 1, "<< /Type /Catalog /Pages 2 0 R >>");

                StringBuilder kids = new StringBuilder();
                for (int i = 0; i < cantPaginas; i++)
                    kids.AppendFormat("{0} 0 R ", primerPagina + i * 2);
                Objeto(ms, posiciones, 2, $"<< /Type /Pages /Kids [{kids}] /Count {cantPaginas} >>");

                Objeto(ms, posiciones, 3, "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>");
                Objeto(ms, posiciones, 4, "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold /Encoding /WinAnsiEncoding >>");
                Objeto(ms, posiciones, 5, "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Oblique /Encoding /WinAnsiEncoding >>");

                for (int i = 0; i < cantPaginas; i++)
                {
                    int idPagina = primerPagina + i * 2;
                    int idContenido = idPagina + 1;
                    Objeto(ms, posiciones, idPagina,
                        $"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 {N(AnchoPagina)} {N(AltoPagina)}] " +
                        $"/Resources << /Font << /F1 3 0 R /F2 4 0 R /F3 5 0 R >> >> /Contents {idContenido} 0 R >>");

                    byte[] contenido = Ansi.GetBytes(paginas[i].ToString());
                    posiciones.Add(ms.Position);
                    Escribir(ms, $"{idContenido} 0 obj\n<< /Length {contenido.Length} >>\nstream\n");
                    ms.Write(contenido, 0, contenido.Length);
                    Escribir(ms, "\nendstream\nendobj\n");
                }

                long inicioXref = ms.Position;
                Escribir(ms, $"xref\n0 {posiciones.Count + 1}\n0000000000 65535 f \n");
                foreach (long pos in posiciones)
                    Escribir(ms, pos.ToString("D10") + " 00000 n \n");
                Escribir(ms, $"trailer\n<< /Size {posiciones.Count + 1} /Root 1 0 R >>\nstartxref\n{inicioXref}\n%%EOF\n");

                return ms.ToArray();
            }
        }

        // Los objetos se escriben en orden (1, 2, 3...) para que la tabla xref quede correcta.
        private static void Objeto(MemoryStream ms, List<long> posiciones, int id, string cuerpo)
        {
            posiciones.Add(ms.Position);
            Escribir(ms, $"{id} 0 obj\n{cuerpo}\nendobj\n");
        }

        private static void Escribir(MemoryStream ms, string texto)
        {
            byte[] bytes = Ansi.GetBytes(texto);
            ms.Write(bytes, 0, bytes.Length);
        }

        private static string Escapar(string texto)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in texto)
            {
                if (c == '(' || c == ')' || c == '\\') sb.Append('\\').Append(c);
                else if (c == '–' || c == '—') sb.Append('-');   // guiones largos -> '-'
                else if (c == '•') sb.Append('-');                   // viñeta
                else if (c < 32) sb.Append(' ');
                else sb.Append(c);
            }
            return sb.ToString();
        }

        private static string N(float valor)
        {
            return valor.ToString("0.##", CultureInfo.InvariantCulture);
        }
    }
}
