using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BLL
{
    public class BLL_Turno
    {
        DAL_Turno dal_turno = new DAL_Turno();
        DAL_MedicoHorario dal_medicoHorario = new DAL_MedicoHorario();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();

        private const string Modulo = "TURNOS";

        private static int? UsuarioActual()
        {
            return Sesion.Instancia().UsuarioActual?.IdUsuario;
        }

        public void RegistrarTurno(int idPaciente, int idHorario, DateTime fecha)
        {
            if (idPaciente <= 0)
                throw new ArgumentException("Busque o registre al paciente antes de registrar el turno.");
            if (fecha.Date < DateTime.Today)
                throw new ArgumentException("No se puede registrar un turno en una fecha pasada.");

            BE_MedicoHorario horario = dal_medicoHorario.ObtenerPorId(idHorario);
            if (horario == null || !horario.Activo || dal_medicoHorario.EstaInactivoEnFecha(idHorario, fecha))
                throw new ArgumentException("El horario elegido ya no está disponible.");
            if ((int)fecha.DayOfWeek != horario.DiaSemana)
                throw new ArgumentException("La fecha elegida no coincide con el día de atención del horario seleccionado.");
            if (fecha.Date == DateTime.Today && horario.HoraInicio <= DateTime.Now.TimeOfDay)
                throw new ArgumentException("No se puede registrar un turno en un horario que ya pasó.");
            if (dal_turno.ExisteTurnoActivo(idHorario, fecha))
                throw new ArgumentException("Ya existe un turno confirmado para ese horario en esa fecha.");

            BE_Turno turno = new BE_Turno { IdPaciente = idPaciente, IdHorario = idHorario, Fecha = fecha.Date };

            try
            {
                dal_turno.Insertar(turno, UsuarioActual());
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new ArgumentException("Ese horario acaba de ser reservado por otro recepcionista. Elija otro horario.");
            }

            bll_bitacora.RegistrarEvento(UsuarioActual(), AccionBitacora.TURNO_SOLICITADO, Modulo,
                $"Turno registrado para el paciente {idPaciente} en el horario {idHorario} el {fecha:dd/MM/yyyy}.");
        }

        public void ModificarTurno(int idTurno, int nuevoIdHorario, DateTime nuevaFecha, int idPaciente)
        {
            BE_Turno turnoActual = dal_turno.ObtenerPorId(idTurno);
            if (turnoActual == null)
                throw new ArgumentException("El turno no existe.");
            if (turnoActual.Estado != "Confirmado")
                throw new ArgumentException("Solo se pueden modificar turnos en estado Confirmado.");
            if (turnoActual.IdPaciente != idPaciente)
                throw new ArgumentException("El turno seleccionado no pertenece al paciente.");
            if (turnoActual.IdHorario == nuevoIdHorario && turnoActual.Fecha.Date == nuevaFecha.Date)
                throw new ArgumentException("Elija un horario o una fecha distintos de los del turno actual.");

            RegistrarTurno(idPaciente, nuevoIdHorario, nuevaFecha);

            dal_turno.CambiarEstado(idTurno, "Cancelado", UsuarioActual());

            bll_bitacora.RegistrarEvento(UsuarioActual(), AccionBitacora.TURNO_MODIFICADO, Modulo,
                $"Turno {idTurno} del paciente {idPaciente} modificado: reemplazado por el horario {nuevoIdHorario} el {nuevaFecha:dd/MM/yyyy}.");
        }

        public void CancelarTurno(int idTurno, int idUsuario)
        {
            BE_Turno turno = dal_turno.ObtenerPorId(idTurno);
            if (turno == null)
                throw new ArgumentException("El turno no existe.");
            if (turno.Estado != "Confirmado")
                throw new ArgumentException("Solo se pueden cancelar turnos en estado Confirmado.");

            dal_turno.CambiarEstado(idTurno, "Cancelado", idUsuario);

            bll_bitacora.RegistrarEvento(idUsuario, AccionBitacora.TURNO_CANCELADO, Modulo,
                $"Turno {idTurno} del paciente {turno.IdPaciente} cancelado.");
        }

        public void CancelarPorCambioDeHorario(List<BE_Turno> turnos, int idUsuario)
        {
            foreach (BE_Turno turno in turnos)
            {
                dal_turno.CambiarEstado(turno.IdTurno, "Cancelado", idUsuario);

                bll_bitacora.RegistrarEvento(idUsuario, AccionBitacora.TURNO_CANCELADO, Modulo,
                    $"Turno {turno.IdTurno} (paciente {turno.IdPaciente}) del {turno.Fecha:dd/MM/yyyy} cancelado por modificación o inactivación del horario del médico.");
            }
        }

        public List<BE_Turno> ListarPorPaciente(int idPaciente)
        {
            try
            {
                return dal_turno.ListarPorPaciente(idPaciente);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudieron obtener los turnos del paciente: " + ex.Message, ex);
            }
        }

        public List<BE_Turno> Filtrar(int? idTurno, DateTime? desde, DateTime? hasta, string estado, string textoPaciente = null)
        {
            try
            {
                estado = string.IsNullOrWhiteSpace(estado) ? null : estado.Trim();
                return dal_turno.Filtrar(idTurno, desde, hasta, estado, textoPaciente);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudieron buscar los turnos: " + ex.Message, ex);
            }
        }
    }
}
