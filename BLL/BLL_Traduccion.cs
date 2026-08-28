using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.BE_Traduccion;

namespace BLL
{
    public class BLL_Traduccion
    {
        private DAL_Traduccion dal = new DAL_Traduccion();
        private BLL_Bitacora bll_bitacora = new BLL_Bitacora();

        public List<Idioma> ObtenerIdiomas()
        {
            return dal.ObtenerIdiomas();
        }

        public List<Traduccion> ObtenerTodas()
        {
            return dal.ObtenerTodas();
        }

        public List<Traduccion> ObtenerPorIdioma(int idIdioma)
        {
            return dal.ObtenerPorIdioma(idIdioma);
        }

        public Dictionary<string, string> ObtenerDiccionario(int idIdioma)
        {
            return dal.ObtenerPorIdioma(idIdioma)
                      .ToDictionary(t => t.Clave, t => t.Texto);
        }

        public void Guardar(Traduccion t)
        {
            if (t.Id == 0)
                dal.Insertar(t);
            else
                dal.Actualizar(t);
        }

        public void Eliminar(int id)
        {
            dal.Eliminar(id);
        }

        public void GuardarIdioma(Idioma idioma)
        {
            dal.InsertarIdioma(idioma);

            bll_bitacora.RegistrarEvento(
                Sesion.Instancia().UsuarioActual?.IdUsuario,
                AccionBitacora.IDIOMA_ALTA,
                "IDIOMA",
                $"Se agrega el idioma: {idioma.Nombre} ({idioma.Codigo})"
            );
        }

        public void EliminarIdioma(int idIdioma)
        {
            dal.EliminarTraduccionesPorIdioma(idIdioma);
            dal.EliminarIdioma(idIdioma);

            bll_bitacora.RegistrarEvento(
                Sesion.Instancia().UsuarioActual?.IdUsuario,
                AccionBitacora.IDIOMA_BAJA,
                "IDIOMA",
                $"Se elimina el idioma con ID: {idIdioma}"
            );
        }

        public void GuardarIdiomaUsuario(int idUsuario, int idIdioma)
        {
            dal.GuardarIdiomaUsuario(idUsuario, idIdioma);
        }

        public Idioma ObtenerIdiomaPorUsuario(int idUsuario)
        {
            return dal.ObtenerIdiomaPorUsuario(idUsuario);
        }
    }
}