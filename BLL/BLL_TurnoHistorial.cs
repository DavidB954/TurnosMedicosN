using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BLL
{
    public class BLL_TurnoHistorial
    {
        DAL_TurnoHistorial dal_turnoHistorial = new DAL_TurnoHistorial();
        DAL_Turno dal_turno = new DAL_Turno();
        DAL_MedicoHorario dal_medicoHorario = new DAL_MedicoHorario();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();

        public List<BE_TurnoHistorial> ObtenerHistorial(int idTurno)
        {
            try
            {
                return dal_turnoHistorial.ListarPorTurno(idTurno);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener el historial del turno: " + ex.Message, ex);
            }
        }

        public void RestaurarVersion(int idHistorial)
        {
            BE_TurnoHistorial version = dal_turnoHistorial.ObtenerPorId(idHistorial);
            if (version == null)
                throw new ArgumentException("La versión elegida no existe.");

            BE_Turno actual = dal_turno.ObtenerPorId(version.IdTurno);
            ValidarRestauracion(version, actual);

            int idUsuario = Sesion.Instancia().UsuarioActual.IdUsuario;
            try
            {
                dal_turno.Restaurar(version.IdTurno, version.IdHorario, version.Fecha, version.Estado, idUsuario);
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new ArgumentException("No se puede restaurar: el horario de esa versión ya tiene otro turno confirmado.");
            }

            bll_bitacora.RegistrarEvento(idUsuario, AccionBitacora.TURNO_RESTAURADO, "TURNOS",
                $"Turno {version.IdTurno} restaurado a la versión del {version.FechaHoraCambioTexto} " +
                $"(estado {version.Estado}, {version.FechaTexto} {version.RangoHorario}).");
        }

        private void ValidarRestauracion(BE_TurnoHistorial version, BE_Turno actual)
        {
            if (actual == null)
                throw new ArgumentException("El turno ya no existe.");
            if (actual.Estado == "Atendido" || actual.Estado == "Ausente")
                throw new ArgumentException($"El turno está {actual.Estado}: un turno atendido o ausente no se modifica.");
            if (version.Estado != "Confirmado" && version.Estado != "Cancelado")
                throw new ArgumentException("Solo se pueden restaurar versiones en estado Confirmado o Cancelado.");
            if (actual.IdHorario == version.IdHorario && actual.Fecha.Date == version.Fecha.Date && actual.Estado == version.Estado)
                throw new ArgumentException("El turno ya tiene los datos de esa versión.");

            if (version.Estado != "Confirmado")
                return;

            BE_MedicoHorario horario = dal_medicoHorario.ObtenerPorId(version.IdHorario);
            if (horario == null || !horario.Activo || dal_medicoHorario.EstaInactivoEnFecha(version.IdHorario, version.Fecha))
                throw new ArgumentException("No se puede restaurar: el horario de esa versión ya no está activo en esa fecha.");
            if (version.Fecha.Date < DateTime.Today ||
                (version.Fecha.Date == DateTime.Today && horario.HoraInicio <= DateTime.Now.TimeOfDay))
                throw new ArgumentException("No se puede restaurar: la fecha y la hora de esa versión ya pasaron.");

            bool mismoHorario = actual.Estado == "Confirmado" && actual.IdHorario == version.IdHorario && actual.Fecha.Date == version.Fecha.Date;
            if (!mismoHorario && dal_turno.ExisteTurnoActivo(version.IdHorario, version.Fecha))
                throw new ArgumentException("No se puede restaurar: el horario de esa versión ya tiene otro turno confirmado.");
        }
    }
}
