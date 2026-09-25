using BE;
using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BLL_BitacoraEventoTurno
    {
        DAL_BitacoraEventoTurno dal_bitacoraEventoTurno = new DAL_BitacoraEventoTurno();

        public List<BE_BitacoraEventoTurno> Filtrar(int? idTurno, DateTime? desde, DateTime? hasta)
        {
            try
            {
                return dal_bitacoraEventoTurno.Filtrar(idTurno, desde, hasta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
