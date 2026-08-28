using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_HistorialCambios
    {
        DAL_HistorialCambios dal_historialCambios = new DAL_HistorialCambios();
        DAL_Usuario dal_usuario = new DAL_Usuario();

        public void RegistrarCambio(BE_Usuario anterior, BE_Usuario nuevo, int idUsuarioEditor)
        {
            try
            {
                // Se juntan todos los campos modificados y se registran en una sola
                // transacción: si la edición tocó Nombre y Apellido, quedan agrupados
                // bajo el mismo IdTransaccion y la misma FechaCambio.
                List<BE_CambioCampo> cambios = new List<BE_CambioCampo>();

                AgregarSiCambio(cambios, "Nombre", anterior.Nombre, nuevo.Nombre);
                AgregarSiCambio(cambios, "Apellido", anterior.Apellido, nuevo.Apellido);
                AgregarSiCambio(cambios, "Email", anterior.Email, nuevo.Email);
                AgregarSiCambio(cambios, "Activo", anterior.Activo.ToString(), nuevo.Activo.ToString());

                if (cambios.Count == 0)
                    return;

                dal_historialCambios.RegistrarCambios("Usuario", anterior.IdUsuario, cambios, idUsuarioEditor, "MODIFICACION");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void AgregarSiCambio(List<BE_CambioCampo> cambios, string campo, string anterior, string nuevo)
        {
            if (anterior != nuevo)
            {
                cambios.Add(new BE_CambioCampo { Campo = campo, ValorAnterior = anterior, ValorNuevo = nuevo });
            }
        }

        public void RegistrarAlta(string tabla, int idRegistro, int idUsuarioEditor)
        {
            try
            {
                dal_historialCambios.RegistrarCambio(tabla, idRegistro, "-", null, "Registro Creado", idUsuarioEditor, "ALTA");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void RegistrarBaja(string tabla, int idRegistro, int idUsuarioEditor)
        {
            try
            {
                dal_historialCambios.RegistrarCambio(tabla, idRegistro, "-", null, "Registro Existente", idUsuarioEditor, "BAJA");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public DataTable ObtenerHistorial(string tabla, int idRegistro)
        {
            try
            {
                DataTable crudo = dal_historialCambios.ObtenerHistorial(tabla, idRegistro);

                // Una fila por transacción: los campos modificados en una misma edición
                // (mismo IdTransaccion) se muestran juntos en una sola fila de la grilla.
                DataTable agrupado = new DataTable();
                agrupado.Columns.Add("FechaCambio", typeof(DateTime));
                agrupado.Columns.Add("Usuario", typeof(string));
                agrupado.Columns.Add("Accion", typeof(string));
                agrupado.Columns.Add("Campo", typeof(string));
                agrupado.Columns.Add("ValorAnterior", typeof(string));
                agrupado.Columns.Add("ValorNuevo", typeof(string));

                // Las filas viejas (anteriores a IdTransaccion) no tienen grupo: cada una
                // queda como transacción propia.
                var grupos = crudo.Rows.Cast<DataRow>()
                    .GroupBy(r => r["IdTransaccion"] is DBNull ? Guid.NewGuid() : (Guid)r["IdTransaccion"])
                    .OrderByDescending(g => (DateTime)g.First()["FechaCambio"]);

                string[] ordenCampos = { "Nombre", "Apellido", "Email", "Activo" };

                foreach (var grupo in grupos)
                {
                    List<DataRow> filas = grupo
                        .OrderBy(r =>
                        {
                            int indice = Array.IndexOf(ordenCampos, r["Campo"].ToString());
                            return indice < 0 ? int.MaxValue : indice;
                        })
                        .ToList();

                    DataRow primera = filas[0];

                    agrupado.Rows.Add(
                        (DateTime)primera["FechaCambio"],
                        primera["Usuario"] is DBNull ? "" : primera["Usuario"].ToString(),
                        primera["Accion"].ToString(),
                        string.Join(" | ", filas.Select(f => f["Campo"].ToString())),
                        string.Join(" | ", filas.Select(f => f["ValorAnterior"] is DBNull ? "-" : f["ValorAnterior"].ToString())),
                        string.Join(" | ", filas.Select(f => f["ValorNuevo"] is DBNull ? "-" : f["ValorNuevo"].ToString()))
                    );
                }

                return agrupado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public BE_Usuario RecomponerEstado(int idUsuario, DateTime fechaHasta)
        {
            try
            {
                DataTable historial = dal_historialCambios.ObtenerHistorialHastaFecha("Usuario", idUsuario, fechaHasta);

                //Estado Actual

                BE_Usuario usuario = dal_usuario.ObtenerUsuarioPorId(idUsuario);

                foreach (DataRow row in historial.Rows)
                {
                    string campo = row["Campo"].ToString();

                    string valorAnterior = row["ValorAnterior"].ToString();

                    switch (campo)
                    {
                        case "Nombre":
                            usuario.Nombre = valorAnterior;
                            break;
                        case "Apellido":
                            usuario.Apellido = valorAnterior;
                            break;
                        case "Email":
                            usuario.Email = valorAnterior;
                            break;
                        case "Activo":
                            usuario.Activo = valorAnterior == "True";
                            break;
                    }
                }

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           

        }
    }
}
