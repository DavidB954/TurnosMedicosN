using BE;
using BLL;
using Servicios;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Vista
{
    // Descarga de la receta en PDF, compartida por "Mis recetas" (frmMisRecetas) y "Ver receta"
    // de Mis Turnos (frmMiReceta): pide donde guardar, genera el PDF, lo audita y ofrece abrirlo.
    // Los textos se traducen con las claves "<Formulario>.Msg.*" de quien la llama.
    public static class DescargadorReceta
    {
        public static void Descargar(Form duenio, BE_AtencionMedica receta)
        {
            Func<string, string, string> T = (clave, porDefecto) =>
                TraduccionServicio.Instancia.TraducirODefecto($"{duenio.Name}.{clave}", porDefecto);

            try
            {
                if (receta == null || receta.Receta == null)
                    throw new ArgumentException("No hay una receta seleccionada.");

                using (SaveFileDialog dialogo = new SaveFileDialog())
                {
                    dialogo.Title = T("Msg.GuardarComo", "Guardar receta");
                    dialogo.Filter = "PDF (*.pdf)|*.pdf";
                    dialogo.FileName = $"Receta_{receta.Receta.NroReceta}.pdf";
                    dialogo.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    if (dialogo.ShowDialog(duenio) != DialogResult.OK)
                        return;

                    RecetaPdf.Guardar(receta, dialogo.FileName);
                    new BLL_Atencion().RegistrarDescargaReceta(receta);

                    if (MessageBox.Show(string.Format(T("Msg.Descargada", "Receta guardada en: {0}. ¿Desea abrirla?"), dialogo.FileName),
                            duenio.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        Process.Start(dialogo.FileName);
                }
            }
            catch (IOException ex)
            {
                // Tipicamente: el PDF anterior esta abierto en otro programa
                MessageBox.Show(T("Msg.ArchivoEnUso", "No se pudo guardar el archivo. Si está abierto, ciérrelo e intente de nuevo.") +
                    Environment.NewLine + ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, duenio.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, T("Msg.Error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
