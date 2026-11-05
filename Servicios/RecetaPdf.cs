using BE;
using System;

namespace Servicios
{
    // Arma el PDF de la receta que descarga el paciente: datos del paciente y del medico,
    // N° de receta, fecha y medicamentos. NO incluye motivo ni indicaciones (Historia Clinica).
    public static class RecetaPdf
    {
        private const float Margen = 50f;
        private const float Ancho = DocumentoPdf.AnchoPagina - 2 * Margen;

        public static void Guardar(BE_AtencionMedica atencion, string ruta)
        {
            if (atencion == null || atencion.Receta == null)
                throw new ArgumentException("No hay receta para descargar.");

            DocumentoPdf pdf = new DocumentoPdf();
            BE_Turno turno = atencion.Turno;
            BE_Receta receta = atencion.Receta;

            // Encabezado
            pdf.Rectangulo(0, 0, DocumentoPdf.AnchoPagina, 95, 0.145f, 0.388f, 0.922f);
            pdf.Texto(Margen, 28, "RECETA", 26, DocumentoPdf.Fuente.Negrita, 1f);
            pdf.Texto(Margen, 62, "Sistema de Gestión de Turnos Médicos", 11, DocumentoPdf.Fuente.Normal, 1f);
            TextoDerecha(pdf, 30, "N° " + receta.NroReceta, 14, DocumentoPdf.Fuente.Negrita, 1f);
            TextoDerecha(pdf, 52, "Fecha de emisión: " + receta.FechaEmision.ToString("dd/MM/yyyy"), 11, DocumentoPdf.Fuente.Normal, 1f);

            // Paciente / Medico
            float y = 125;
            float columna2 = Margen + Ancho / 2 + 10;
            pdf.Texto(Margen, y, "PACIENTE", 9, DocumentoPdf.Fuente.Negrita, 0.45f);
            pdf.Texto(columna2, y, "MÉDICO", 9, DocumentoPdf.Fuente.Negrita, 0.45f);
            y += 16;

            BE_Paciente paciente = turno.Paciente;
            pdf.Texto(Margen, y, turno.NombreApellidoPaciente, 13, DocumentoPdf.Fuente.Negrita);
            pdf.Texto(columna2, y, turno.NombreApellidoMedico, 13, DocumentoPdf.Fuente.Negrita);
            y += 19;
            if (paciente != null && !string.IsNullOrEmpty(paciente.DNI))
                pdf.Texto(Margen, y, $"DNI {paciente.DNI}   ·   H.C. N° {paciente.NroHistoriaClinica}", 10);
            pdf.Texto(columna2, y, "Matrícula: " + (string.IsNullOrEmpty(turno.MatriculaMedico) ? "no informada" : turno.MatriculaMedico), 10);
            y += 16;
            if (!string.IsNullOrEmpty(turno.Especialidades))
                pdf.TextoAjustado(columna2, y, Ancho / 2 - 10, turno.Especialidades, 10);
            pdf.Texto(Margen, y, $"Turno del {turno.Fecha:dd/MM/yyyy} a las {turno.HoraInicio:hh\\:mm}", 10, DocumentoPdf.Fuente.Normal, 0.35f);

            y += 30;
            pdf.Linea(Margen, y, Margen + Ancho, y, 1f, 0.8f);
            y += 18;

            // Medicamentos
            pdf.Texto(Margen, y, "Rp/", 18, DocumentoPdf.Fuente.Negrita);
            y += 32;

            float xMed = Margen, xPres = Margen + 150, xPos = Margen + 285, xCant = Margen + Ancho - 40;
            pdf.Rectangulo(Margen, y - 6, Ancho, 24, 0.886f, 0.91f, 0.941f);
            pdf.Texto(xMed + 6, y, "Medicamento", 10, DocumentoPdf.Fuente.Negrita);
            pdf.Texto(xPres, y, "Presentación", 10, DocumentoPdf.Fuente.Negrita);
            pdf.Texto(xPos, y, "Posología", 10, DocumentoPdf.Fuente.Negrita);
            pdf.Texto(xCant, y, "Cant.", 10, DocumentoPdf.Fuente.Negrita);
            y += 28;

            foreach (BE_RecetaItem item in receta.Items)
            {
                if (y > DocumentoPdf.AltoPagina - 160)
                {
                    pdf.NuevaPagina();
                    y = Margen;
                }

                float altoMed = pdf.TextoAjustado(xMed + 6, y, xPres - xMed - 14, item.Medicamento, 11, DocumentoPdf.Fuente.Negrita);
                float altoPres = pdf.TextoAjustado(xPres, y, xPos - xPres - 10, item.Presentacion ?? "-", 10);
                float altoPos = pdf.TextoAjustado(xPos, y, xCant - xPos - 10, item.Posologia, 10);
                pdf.Texto(xCant, y, item.Cantidad.ToString(), 11, DocumentoPdf.Fuente.Negrita);

                y += Math.Max(altoMed, Math.Max(altoPres, altoPos)) + 8;
                pdf.Linea(Margen, y - 4, Margen + Ancho, y - 4, 0.5f, 0.85f);
                y += 6;
            }

            // Pie
            float yPie = DocumentoPdf.AltoPagina - 110;
            pdf.Linea(Margen, yPie, Margen + Ancho, yPie, 0.5f, 0.8f);
            pdf.TextoAjustado(Margen, yPie + 12, Ancho,
                "Documento informativo con la prescripción registrada por el médico. La receta oficial (Ley 27.553 y " +
                "Decreto 345/2024) la emite el médico en una plataforma de receta electrónica registrada.",
                9, DocumentoPdf.Fuente.Cursiva);
            pdf.Texto(Margen, yPie + 52, "Descargado el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 8, DocumentoPdf.Fuente.Normal, 0.45f);

            pdf.Guardar(ruta);
        }

        private static void TextoDerecha(DocumentoPdf pdf, float y, string texto, float tamano, DocumentoPdf.Fuente fuente, float gris)
        {
            float x = Margen + Ancho - DocumentoPdf.AnchoTexto(texto, tamano, fuente);
            pdf.Texto(x, y, texto, tamano, fuente, gris);
        }
    }
}
