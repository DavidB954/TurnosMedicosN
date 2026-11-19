using BE;
using DAL;
using System;
using System.Collections.Generic;
using Servicios;

namespace BLL
{
    public class BLL_MedicoHorario
    {
        DAL_MedicoHorario dal_medicoHorario = new DAL_MedicoHorario();
        DAL_Turno dal_turno = new DAL_Turno();
        DAL_Especialidad dal_especialidad = new DAL_Especialidad();
        BLL_Turno bll_turno = new BLL_Turno();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();

        static readonly TimeSpan DescansoInicio = new TimeSpan(12, 0, 0);
        static readonly TimeSpan DescansoFin = new TimeSpan(13, 0, 0);

        // Devuelve las horas de inicio de cada turno entre Desde y Hasta, excluyendo
        // cualquier turno que se solape con el descanso de 12:00 a 13:00.
        public List<TimeSpan> GenerarFranjas(TimeSpan desde, TimeSpan hasta, int minutos)
        {
            List<TimeSpan> inicios = new List<TimeSpan>();
            TimeSpan duracion = TimeSpan.FromMinutes(minutos);
            TimeSpan actual = desde;

            while (actual + duracion <= hasta)
            {
                TimeSpan fin = actual + duracion;
                bool solapaDescanso = actual < DescansoFin && fin > DescansoInicio;

                if (!solapaDescanso)
                {
                    inicios.Add(actual);
                }

                actual += duracion;
            }

            return inicios;
        }

        // Agrega los horarios generados a los que ya tenga el medico en esos dias (no borra nada).
        // idEspecialidad: especialidad con la que atiende en esa franja (null = cualquiera de las suyas).
        public void AsignarDiasYHorarios(int idMedico, List<int> diasSemana, TimeSpan desde, TimeSpan hasta, int minutos, int? idEspecialidad)
        {
            List<TimeSpan> inicios = ValidarYGenerarFranjas(idMedico, diasSemana, desde, hasta, minutos);
            TimeSpan duracion = TimeSpan.FromMinutes(minutos);

            ValidarEspecialidadDelMedico(idMedico, idEspecialidad);
            ValidarSinSuperposicionConOtraEspecialidad(idMedico, diasSemana, inicios, duracion, idEspecialidad);

            foreach (int dia in diasSemana)
                foreach (TimeSpan inicio in inicios)
                    dal_medicoHorario.InsertarOReactivarHorario(CrearHorario(idMedico, dia, inicio, duracion, idEspecialidad));

            RegistrarCambioDisponibilidad($"Se asignan horarios al medico {idMedico} (especialidad {idEspecialidad}): dias {string.Join(",", diasSemana)} de {Hora(desde)} a {Hora(hasta)}, turnos de {minutos} min");
        }

        // Reemplaza los horarios que el medico ya tenga en esos dias (para esa especialidad,
        // sin tocar los de otra especialidad) por los generados con estos nuevos parametros.
        // No borra fisicamente los anteriores (la FK de Turno lo impide si tienen algun turno
        // asociado, aunque este cancelado): los desactiva, y los que coincidan con la nueva
        // franja se reactivan.
        // Cancela los turnos Confirmados futuros afectados (la vista ya pidio confirmacion
        // con ObtenerConflictosModificacion) antes de desactivar los horarios.
        public void ModificarDiasYHorarios(int idMedico, List<int> diasSemana, TimeSpan desde, TimeSpan hasta, int minutos, int? idEspecialidad, int idUsuario)
        {
            List<TimeSpan> inicios = ValidarYGenerarFranjas(idMedico, diasSemana, desde, hasta, minutos);
            TimeSpan duracion = TimeSpan.FromMinutes(minutos);

            ValidarEspecialidadDelMedico(idMedico, idEspecialidad);
            ValidarSinSuperposicionConOtraEspecialidad(idMedico, diasSemana, inicios, duracion, idEspecialidad);

            bll_turno.CancelarPorCambioDeHorario(ObtenerConflictosModificacion(idMedico, diasSemana, desde, hasta, minutos, idEspecialidad), idUsuario);

            foreach (int dia in diasSemana)
            {
                dal_medicoHorario.DesactivarHorariosPorMedicoYDia(idMedico, dia, idEspecialidad);

                foreach (TimeSpan inicio in inicios)
                    dal_medicoHorario.InsertarOReactivarHorario(CrearHorario(idMedico, dia, inicio, duracion, idEspecialidad));
            }

            RegistrarCambioDisponibilidad($"Se modifican los horarios del medico {idMedico} (especialidad {idEspecialidad}): dias {string.Join(",", diasSemana)} de {Hora(desde)} a {Hora(hasta)}, turnos de {minutos} min");
        }

        private static string Hora(TimeSpan t)
        {
            return string.Format("{0:hh\\:mm}", t);
        }

        // Los cambios de disponibilidad se auditan en la Bitacora con el usuario logueado.
        private void RegistrarCambioDisponibilidad(string descripcion)
        {
            int? idUsuario = Sesion.Instancia().UsuarioActual == null ? (int?)null : Sesion.Instancia().UsuarioActual.IdUsuario;
            bll_bitacora.RegistrarEvento(idUsuario, AccionBitacora.DISPONIBILIDAD_MODIFICADA, "DISPONIBILIDAD", descripcion);
        }

        // Turnos Confirmados de hoy en adelante que se cancelarian al modificar el horario del medico (para esa
        // especialidad) en estos dias: solo los de horarios que dejan de existir con los nuevos parametros. Un
        // turno cuyo horario se mantiene (mismo inicio y misma duracion) conserva su horario y no se cancela.
        public List<BE_Turno> ObtenerConflictosModificacion(int idMedico, List<int> diasSemana, TimeSpan desde, TimeSpan hasta, int minutos, int? idEspecialidad)
        {
            List<TimeSpan> inicios = ValidarYGenerarFranjas(idMedico, diasSemana, desde, hasta, minutos);
            TimeSpan duracion = TimeSpan.FromMinutes(minutos);
            List<BE_Turno> conflictos = new List<BE_Turno>();

            foreach (int dia in diasSemana)
                conflictos.AddRange(dal_turno.ListarActivosPorMedicoYDiaDesde(idMedico, dia, DateTime.Today, idEspecialidad)
                    .FindAll(t => !(inicios.Contains(t.HoraInicio) && t.HoraFin == t.HoraInicio + duracion)));

            return conflictos;
        }

        private void ValidarEspecialidadDelMedico(int idMedico, int? idEspecialidad)
        {
            // Cada horario de atencion corresponde a una especialidad (modelo conceptual RF1).
            if (idEspecialidad == null)
                throw new ArgumentException("Seleccione la especialidad del horario.");

            if (!dal_especialidad.ListarEspecialidadesPorMedico(idMedico).Exists(e => e.IdEspecialidad == idEspecialidad.Value))
                throw new ArgumentException("El medico no tiene asignada la especialidad elegida.");
        }

        // Un medico no puede atender dos especialidades a la vez: si ya tiene una franja activa
        // de OTRA especialidad que se pisa con las nuevas, se rechaza.
        private void ValidarSinSuperposicionConOtraEspecialidad(int idMedico, List<int> diasSemana, List<TimeSpan> inicios, TimeSpan duracion, int? idEspecialidad)
        {
            if (idEspecialidad == null)
                return;

            foreach (BE_MedicoHorario existente in ListarPorMedico(idMedico))
            {
                if (!existente.Activo || existente.IdEspecialidad == null || existente.IdEspecialidad == idEspecialidad)
                    continue;
                if (!diasSemana.Contains(existente.DiaSemana))
                    continue;

                foreach (TimeSpan inicio in inicios)
                {
                    if (existente.HoraInicio < inicio + duracion && existente.HoraFin > inicio)
                        throw new ArgumentException($"El medico ya atiende otra especialidad ({existente.NombreEspecialidad}) el {existente.DiaSemanaTexto} de {existente.RangoHorario}. Elija otro horario o inactive esa franja primero.");
                }
            }
        }

        private List<TimeSpan> ValidarYGenerarFranjas(int idMedico, List<int> diasSemana, TimeSpan desde, TimeSpan hasta, int minutos)
        {
            if (idMedico <= 0)
                throw new ArgumentException("Debe seleccionar un medico.");
            if (diasSemana == null || diasSemana.Count == 0)
                throw new ArgumentException("Debe seleccionar al menos un dia trabajado.");
            if (minutos <= 0)
                throw new ArgumentException("La duracion de cada turno debe ser mayor a 0 minutos.");
            if (desde.Days != 0 || desde >= TimeSpan.FromDays(1))
                throw new ArgumentException("El horario 'Desde' debe ser una hora del dia, entre 00:00 y 23:59.");
            if (hasta.Days != 0 || hasta >= TimeSpan.FromDays(1))
                throw new ArgumentException("El horario 'Hasta' debe ser una hora del dia, entre 00:00 y 23:59.");
            if (desde >= hasta)
                throw new ArgumentException("El horario 'Desde' debe ser anterior al horario 'Hasta'.");

            List<TimeSpan> inicios = GenerarFranjas(desde, hasta, minutos);

            if (inicios.Count == 0)
                throw new ArgumentException("La franja horaria ingresada no genera ningun turno.");

            return inicios;
        }

        private static BE_MedicoHorario CrearHorario(int idMedico, int dia, TimeSpan inicio, TimeSpan duracion, int? idEspecialidad)
        {
            BE_MedicoHorario horario = new BE_MedicoHorario();
            horario.IdEspecialidad = idEspecialidad;
            horario.IdMedico = idMedico;
            horario.DiaSemana = dia;
            horario.HoraInicio = inicio;
            horario.HoraFin = inicio + duracion;
            horario.Activo = true;
            return horario;
        }

        // Todas las franjas (activas e inactivas) de un medico, ordenadas por dia y hora.
        public List<BE_MedicoHorario> ListarPorMedico(int idMedico)
        {
            return dal_medicoHorario.ListarParaGrid(true).FindAll(h => h.IdMedico == idMedico);
        }

        // CU-01.4 Inactivar o reactivar horarios en una fecha: horarios activos del medico para el dia de
        // la semana de la fecha, con su estado en esa fecha y el paciente que tiene turno en cada uno.
        public List<BE_MedicoHorario> ListarHorariosDelDia(int idMedico, DateTime fecha)
        {
            if (idMedico <= 0)
                throw new ArgumentException("Debe seleccionar un medico.");
            return dal_medicoHorario.ListarPorMedicoYFecha(idMedico, fecha);
        }

        // Turnos Confirmados de esa fecha en estos horarios: se cancelan si se los inactiva (punto de
        // extension Turnos afectados).
        public List<BE_Turno> ObtenerTurnosAfectados(List<int> idsHorario, DateTime fecha)
        {
            List<BE_Turno> turnos = new List<BE_Turno>();
            foreach (int idHorario in idsHorario)
                turnos.AddRange(dal_turno.ListarConfirmadosPorHorarioDesde(idHorario, fecha.Date).FindAll(t => t.Fecha.Date == fecha.Date));

            turnos.Sort((a, b) => a.HoraInicio.CompareTo(b.HoraInicio));
            return turnos;
        }

        // El horario deja de atenderse solo en esa fecha; los demas dias de la semana sigue activo.
        // Cancela antes los turnos Confirmados de esa fecha (la vista ya pidio confirmacion con
        // ObtenerTurnosAfectados).
        public void InactivarEnFecha(List<int> idsHorario, DateTime fecha, int idUsuario)
        {
            ValidarInactivacion(idsHorario, fecha);

            bll_turno.CancelarPorCambioDeHorario(ObtenerTurnosAfectados(idsHorario, fecha), idUsuario);
            foreach (int idHorario in idsHorario)
                dal_medicoHorario.InactivarEnFecha(idHorario, fecha, idUsuario);

            bll_bitacora.RegistrarEvento(idUsuario, AccionBitacora.DISPONIBILIDAD_MODIFICADA, "DISPONIBILIDAD",
                $"Se inactivan {idsHorario.Count} horario(s) de medico el {fecha:dd/MM/yyyy} (ids {string.Join(",", idsHorario)})");
        }

        // El horario vuelve a atenderse en esa fecha.
        public void ReactivarEnFecha(List<int> idsHorario, DateTime fecha, int idUsuario)
        {
            ValidarInactivacion(idsHorario, fecha);

            foreach (int idHorario in idsHorario)
                dal_medicoHorario.ReactivarEnFecha(idHorario, fecha);

            bll_bitacora.RegistrarEvento(idUsuario, AccionBitacora.DISPONIBILIDAD_MODIFICADA, "DISPONIBILIDAD",
                $"Se reactivan {idsHorario.Count} horario(s) de medico el {fecha:dd/MM/yyyy} (ids {string.Join(",", idsHorario)})");
        }

        // Solo se inactivan o reactivan horarios de hoy en adelante.
        private static void ValidarInactivacion(List<int> idsHorario, DateTime fecha)
        {
            if (idsHorario == null || idsHorario.Count == 0)
                throw new ArgumentException("Seleccione al menos un horario.");
            if (fecha.Date < DateTime.Today)
                throw new ArgumentException("No se pueden cambiar horarios de una fecha pasada.");
        }

        public List<BE_MedicoHorario> _listaParaGrid(bool incluirInactivos)
        {
            return dal_medicoHorario.ListarParaGrid(incluirInactivos);
        }

        // CU-01.1 paso 8: horarios activos del medico para el dia de la semana de la fecha que no
        // tienen un turno confirmado en esa fecha.
        public List<BE_MedicoHorario> ConsultarDisponibilidad(int idMedico, DateTime fecha, int? idEspecialidad)
        {
            if (idEspecialidad == null)
                throw new ArgumentException("Seleccione la especialidad.");

            int diaSemana = (int)fecha.DayOfWeek;
            return dal_medicoHorario.ListarDisponibles(idMedico, diaSemana, fecha, idEspecialidad);
        }
    }
}
