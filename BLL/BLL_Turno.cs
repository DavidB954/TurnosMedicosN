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

        public void SolicitarTurno(int idPaciente, int idHorario, DateTime fecha)
        {
            if (idPaciente <= 0)
                throw new ArgumentException("Debe iniciar sesion para reservar un turno.");
            if (fecha.Date < DateTime.Today)
                throw new ArgumentException("No se puede reservar un turno en una fecha pasada.");

            BE_MedicoHorario horario = dal_medicoHorario.ObtenerPorId(idHorario);
            if (horario == null || !horario.Activo)
                throw new ArgumentException("El horario elegido ya no esta disponible.");
            if ((int)fecha.DayOfWeek != horario.DiaSemana)
                throw new ArgumentException("La fecha elegida no coincide con el dia de atencion del horario seleccionado.");
            if (fecha.Date == DateTime.Today && horario.HoraInicio <= DateTime.Now.TimeOfDay)
                throw new ArgumentException("No se puede reservar un turno en un horario que ya paso.");
            if (dal_turno.ExisteTurnoActivo(idHorario, fecha))
                throw new ArgumentException("Ya existe un turno reservado para ese horario en esa fecha.");

            BE_Turno turno = new BE_Turno();
            turno.IdPaciente = idPaciente;
            turno.IdHorario = idHorario;
            turno.Fecha = fecha.Date;

            try
            {
                dal_turno.Insertar(turno);
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new ArgumentException("Ese horario ya no esta disponible: acaba de ser reservado por otro paciente. Elija otro horario.");
            }

            bll_bitacora.RegistrarEvento(idPaciente, AccionBitacora.TURNO_SOLICITADO, "TURNOS",
                $"Turno solicitado por el paciente {idPaciente} para el horario {idHorario} el {fecha:dd/MM/yyyy}");
        }

     
        public void CancelarTurno(int idTurno, int idUsuarioQueCancela)
        {
            BE_Turno turno = dal_turno.ObtenerPorId(idTurno);
            if (turno == null)
                throw new ArgumentException("El turno no existe.");
            if (turno.Estado != "Confirmado")
                throw new ArgumentException("Ese turno ya no esta activo.");

            bool esPropio = turno.IdPaciente == idUsuarioQueCancela;
            bool puedeCancelarCualquiera = Sesion.Instancia().TieneRol("Administrativo") || Sesion.Instancia().TieneRol("Administrador");
            if (!esPropio && !puedeCancelarCualquiera)
                throw new ArgumentException("Ese turno no pertenece al paciente logueado.");

            dal_turno.CambiarEstado(idTurno, "Cancelado");

            string descripcion = esPropio
                ? $"Turno {idTurno} cancelado por el paciente {idUsuarioQueCancela}"
                : $"Turno {idTurno} (paciente {turno.IdPaciente}) cancelado por el usuario {idUsuarioQueCancela}";
            bll_bitacora.RegistrarEvento(idUsuarioQueCancela, AccionBitacora.TURNO_CANCELADO, "TURNOS", descripcion);
        }

        // Cancela los turnos afectados por la modificacion/inactivacion de un horario medico.
        // El actor ya fue autorizado a gestionar horarios, por eso no se repite la validacion
        // de pertenencia de CancelarTurno.
        public void CancelarPorCambioDeHorario(List<BE_Turno> turnos, int idUsuarioQueCancela)
        {
            foreach (BE_Turno turno in turnos)
            {
                dal_turno.CambiarEstado(turno.IdTurno, "Cancelado");

                bll_bitacora.RegistrarEvento(idUsuarioQueCancela, AccionBitacora.TURNO_CANCELADO, "TURNOS",
                    $"Turno {turno.IdTurno} (paciente {turno.IdPaciente}) del {turno.Fecha:dd/MM/yyyy} cancelado por el usuario {idUsuarioQueCancela} por modificacion o inactivacion del horario medico");
            }
        }

        public void ModificarTurno(int idTurno, int nuevoIdHorario, DateTime nuevaFecha, int idPaciente)
        {
            BE_Turno turnoActual = dal_turno.ObtenerPorId(idTurno);
            if (turnoActual == null)
                throw new ArgumentException("El turno no existe.");
            if (turnoActual.Estado != "Confirmado")
                throw new ArgumentException("Ese turno ya no esta activo.");
            if (turnoActual.IdPaciente != idPaciente)
                throw new ArgumentException("Ese turno no pertenece al paciente logueado.");

            // Se valida y crea el turno nuevo ANTES de tocar el actual, para no perder la
            // reserva original si el horario elegido no es valido.
            SolicitarTurno(idPaciente, nuevoIdHorario, nuevaFecha);

            dal_turno.CambiarEstado(idTurno, "Cancelado");

            bll_bitacora.RegistrarEvento(idPaciente, AccionBitacora.TURNO_MODIFICADO, "TURNOS",
                $"Turno {idTurno} modificado por el paciente {idPaciente}: reemplazado por el horario {nuevoIdHorario} el {nuevaFecha:dd/MM/yyyy}");
        }

        public List<BE_Turno> _listaPorPaciente(int idPaciente)
        {
            return dal_turno.ListarPorPaciente(idPaciente);
        }

        // Turnos de cualquier paciente, con filtros opcionales. Usado por frmGestionTurnos
        // (Administrativo/Administrador) para ubicar el turno que van a cancelar.
        public List<BE_Turno> Filtrar(int? idTurno, DateTime? desde, DateTime? hasta, string estado)
        {
            return dal_turno.Filtrar(idTurno, desde, hasta, estado);
        }

        // Cierra automaticamente los turnos vencidos: Confirmado -> Ausente cuando ya paso
        // 1 hora desde el fin del horario y nadie lo marco Atendido. Se llama desde el Timer
        // de frmPrincipal y una vez al loguearse un Administrativo/Administrador.
        // Devuelve la cantidad de turnos efectivamente marcados.
        public int ProcesarVencimientos()
        {
            List<int> vencidos = dal_turno.ListarVencidosParaMarcarAusente();

            int marcados = 0;
            foreach (int idTurno in vencidos)
            {
                // Si no se actualizo es que el turno cambio de estado (ej. lo atendieron)
                // entre que se detecto como vencido y que se lo quiso marcar: se salta sin error.
                if (!dal_turno.MarcarAusenteSiConfirmado(idTurno))
                    continue;

                marcados++;

                bll_bitacora.RegistrarEvento(null, AccionBitacora.TURNO_AUSENTE, "TURNOS",
                    $"Turno {idTurno} marcado como Ausente automaticamente por vencimiento de horario.");
            }

            return marcados;
        }
    }
}
