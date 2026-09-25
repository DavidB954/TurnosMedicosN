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

            bll_turno.CancelarPorCambioDeHorario(ObtenerConflictosModificacion(idMedico, diasSemana, idEspecialidad), idUsuario);

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

        // Turnos Confirmados de hoy en adelante que se veran afectados si se modifica el
        // horario del medico (para esa especialidad) en estos dias, porque su horario original
        // va a desactivarse.
        public List<BE_Turno> ObtenerConflictosModificacion(int idMedico, List<int> diasSemana, int? idEspecialidad)
        {
            List<BE_Turno> conflictos = new List<BE_Turno>();

            foreach (int dia in diasSemana)
                conflictos.AddRange(dal_turno.ListarActivosPorMedicoYDiaDesde(idMedico, dia, DateTime.Today, idEspecialidad));

            return conflictos;
        }

        private void ValidarEspecialidadDelMedico(int idMedico, int? idEspecialidad)
        {
            if (idEspecialidad == null)
                return;

            if (!dal_especialidad.listaEspecialidadesPorMedico(idMedico).Exists(e => e.idEspecialidad == idEspecialidad.Value))
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

        // Turnos Confirmados de hoy en adelante de una franja puntual, que se cancelan si se la inactiva.
        public List<BE_Turno> ObtenerTurnosAfectadosPorInactivacion(int idHorario)
        {
            return dal_turno.ListarConfirmadosPorHorarioDesde(idHorario, DateTime.Today);
        }

        // Al inactivar, cancela antes los turnos Confirmados futuros de la franja (la vista
        // ya pidio confirmacion con ObtenerTurnosAfectadosPorInactivacion).
        public void CambiarActivo(int idHorario, bool activo, int idUsuario)
        {
            if (!activo)
                bll_turno.CancelarPorCambioDeHorario(ObtenerTurnosAfectadosPorInactivacion(idHorario), idUsuario);

            dal_medicoHorario.CambiarActivo(idHorario, activo);
        }

        // Todas las franjas (activas e inactivas) de un medico, ordenadas por dia y hora.
        public List<BE_MedicoHorario> ListarPorMedico(int idMedico)
        {
            return dal_medicoHorario.ListarParaGrid(true).FindAll(h => h.IdMedico == idMedico);
        }

        // Turnos Confirmados futuros que se cancelarian al inactivar todas estas franjas.
        public List<BE_Turno> ObtenerTurnosAfectadosPorInactivacion(List<int> idsHorario)
        {
            List<BE_Turno> turnos = new List<BE_Turno>();
            foreach (int idHorario in idsHorario)
                turnos.AddRange(ObtenerTurnosAfectadosPorInactivacion(idHorario));

            turnos.Sort((a, b) => a.Fecha != b.Fecha ? a.Fecha.CompareTo(b.Fecha) : a.HoraInicio.CompareTo(b.HoraInicio));
            return turnos;
        }

        public void CambiarActivo(List<int> idsHorario, bool activo, int idUsuario)
        {
            foreach (int idHorario in idsHorario)
                CambiarActivo(idHorario, activo, idUsuario);

            bll_bitacora.RegistrarEvento(idUsuario, AccionBitacora.DISPONIBILIDAD_MODIFICADA, "DISPONIBILIDAD",
                $"Se {(activo ? "reactivan" : "inactivan")} {idsHorario.Count} franja(s) horaria(s) de medico (ids {string.Join(",", idsHorario)})");
        }

        public List<BE_MedicoHorario> _listaParaGrid(bool incluirInactivos)
        {
            return dal_medicoHorario.ListarParaGrid(incluirInactivos);
        }

        public List<BE_MedicoHorario> _listaDisponibles(int idMedico, DateTime fecha, int? idEspecialidad)
        {
            int diaSemana = (int)fecha.DayOfWeek;
            return dal_medicoHorario.ListarDisponibles(idMedico, diaSemana, fecha, idEspecialidad);
        }
    }
}
