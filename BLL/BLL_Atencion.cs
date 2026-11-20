using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BLL_Atencion
    {
        DAL_Atencion dal = new DAL_Atencion();
        DAL_DVV dal_dvv = new DAL_DVV();
        BLL_Bitacora bitacora = new BLL_Bitacora();

        private const string Modulo = "ATENCION";
        private const string TablaDVV = "AtencionMedica";

        public const int LargoMaximoMotivo = 500;
        public const int LargoMaximoIndicaciones = 1000;

        public DateTime? ObtenerProximaFechaConTurnos(int idMedico, DateTime desde)
        {
            try
            {
                ValidarRolMedico();
                ValidarMedicoLogueado(idMedico);

                return dal.ObtenerProximaFechaConTurnos(idMedico, desde);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar la proxima fecha con turnos: " + ex.Message, ex);
            }
        }

        public List<BE_Turno> ObtenerAgenda(int idMedico, DateTime fecha, bool registrarConsulta = true)
        {
            try
            {
                ValidarRolMedico();
                ValidarMedicoLogueado(idMedico);

                List<BE_Turno> agenda = dal.ObtenerAgenda(idMedico, fecha);

                if (registrarConsulta)
                {
                    bitacora.RegistrarEvento(idMedico, AccionBitacora.AGENDA_CONSULTADA, Modulo,
                        $"Agenda del {fecha:dd/MM/yyyy} consultada por el medico {idMedico} ({agenda.Count} turnos).");
                }

                return agenda;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener la agenda: " + ex.Message, ex);
            }
        }

        public bool PuedeAtender(BE_Turno turno)
        {
            return turno != null && turno.Fecha.Date == DateTime.Today && turno.Estado == "Confirmado";
        }

        public bool PuedeMarcarAusente(BE_Turno turno)
        {
            return PuedeAtender(turno) && turno.HoraInicio <= DateTime.Now.TimeOfDay;
        }

        public BE_Turno ObtenerTurnoParaAtender(int idTurno, int idMedico)
        {
            try
            {
                ValidarRolMedico();
                ValidarMedicoLogueado(idMedico);

                BE_Turno turno = dal.ObtenerTurnoConPaciente(idTurno);
                if (turno == null || turno.IdMedico != idMedico)
                    throw new ArgumentException("El turno no puede atenderse: no pertenece a su agenda.");

                ValidarTurnoAtendible(turno);
                return turno;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener el turno: " + ex.Message, ex);
            }
        }

        public void RegistrarAtencion(BE_AtencionMedica a)
        {
            if (a == null || a.Turno == null)
                throw new ArgumentException("No hay un turno seleccionado para atender.");

            ValidarRolMedico();
            int idMedico = Sesion.Instancia().UsuarioActual.IdUsuario;

            BE_Turno turno;
            try
            {
                turno = dal.ObtenerTurnoConPaciente(a.Turno.IdTurno);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo verificar el turno: " + ex.Message, ex);
            }
            if (turno == null || turno.IdMedico != idMedico)
                throw new ArgumentException("El turno no puede atenderse: no pertenece a su agenda.");
            ValidarTurnoAtendible(turno);
            a.Turno = turno;

            ValidarDatos(a);

            DateTime ahora = DateTime.Now;
            a.FechaHora = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, ahora.Minute, ahora.Second);
            a.MotivoConsulta = a.MotivoConsulta.Trim();
            a.Indicaciones = string.IsNullOrWhiteSpace(a.Indicaciones) ? null : a.Indicaciones.Trim();
            if (a.Receta != null)
                a.Receta.FechaEmision = a.FechaHora;
            a.DVH = CalcularDVH(a);

            try
            {
                dal.GuardarAtencion(a);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Ese turno ya no esta activo. No se guardo la atencion.");
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new ArgumentException("Ese turno ya tiene una atencion registrada.");
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo registrar la atencion. No se guardo ningun dato y el turno sigue Confirmado. Detalle: " + ex.Message, ex);
            }

            ActualizarDVV();

            bitacora.RegistrarEvento(idMedico, AccionBitacora.ATENCION_REGISTRADA, Modulo,
                $"Atencion {a.IdAtencion} registrada en la HC {turno.Paciente.NroHistoriaClinica} (turno {turno.IdTurno}).");
            if (a.Receta != null)
            {
                bitacora.RegistrarEvento(idMedico, AccionBitacora.RECETA_EMITIDA, Modulo,
                    $"Receta {a.Receta.NroReceta} emitida con {a.Receta.Items.Count} medicamento(s) en la atencion {a.IdAtencion}.");
            }
            bitacora.RegistrarEvento(idMedico, AccionBitacora.TURNO_ATENDIDO, "TURNOS",
                $"Turno {turno.IdTurno} (paciente {turno.IdPaciente}) marcado como Atendido por el medico {idMedico}.");
        }

        private void ValidarTurnoAtendible(BE_Turno t)
        {
            if (t.Fecha.Date != DateTime.Today)
                throw new ArgumentException("El turno no puede atenderse: no es de la fecha actual.");
            if (t.Estado != "Confirmado")
                throw new ArgumentException($"El turno no puede atenderse: esta en estado {t.Estado}.");
            if (t.Paciente == null || t.Paciente.NroHistoriaClinica <= 0)
                throw new ArgumentException("El turno no puede atenderse: el paciente no tiene Historia Clinica. Debe registrarlo la recepcion.");
        }

        private void ValidarDatos(BE_AtencionMedica a)
        {
            List<string> errores = new List<string>();

            if (string.IsNullOrWhiteSpace(a.MotivoConsulta))
                errores.Add("El motivo de la consulta es obligatorio.");
            else if (a.MotivoConsulta.Trim().Length > LargoMaximoMotivo)
                errores.Add($"El motivo de la consulta no puede superar los {LargoMaximoMotivo} caracteres.");

            if (a.Indicaciones != null && a.Indicaciones.Trim().Length > LargoMaximoIndicaciones)
                errores.Add($"Las indicaciones no pueden superar los {LargoMaximoIndicaciones} caracteres.");

            if (a.Receta != null)
            {
                if (a.Receta.Items == null || a.Receta.Items.Count == 0)
                    errores.Add("Con Receta = SI debe cargar al menos un medicamento.");
                else
                {
                    foreach (BE_RecetaItem item in a.Receta.Items)
                    {
                        try { ValidarItemReceta(item); }
                        catch (ArgumentException ex) { errores.Add(ex.Message); }
                    }
                }
            }

            if (errores.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, errores.Distinct()));
        }

        public void ValidarItemReceta(BE_RecetaItem item)
        {
            if (item == null)
                throw new ArgumentException("Medicamento invalido.");

            item.Medicamento = item.Medicamento?.Trim();
            item.Presentacion = string.IsNullOrWhiteSpace(item.Presentacion) ? null : item.Presentacion.Trim();
            item.Posologia = item.Posologia?.Trim();

            if (string.IsNullOrEmpty(item.Medicamento))
                throw new ArgumentException("Falta el nombre del medicamento.");
            if (string.IsNullOrEmpty(item.Posologia))
                throw new ArgumentException($"Falta la posologia de {item.Medicamento}.");
            if (item.Medicamento.Length > 150)
                throw new ArgumentException("El medicamento no puede superar los 150 caracteres.");
            if (item.Presentacion != null && item.Presentacion.Length > 100)
                throw new ArgumentException("La presentacion no puede superar los 100 caracteres.");
            if (item.Posologia.Length > 200)
                throw new ArgumentException("La posologia no puede superar los 200 caracteres.");
            if (item.Cantidad <= 0)
                throw new ArgumentException($"La cantidad de {item.Medicamento} debe ser mayor a 0.");
        }

        public void MarcarAusente(int idTurno)
        {
            ValidarRolMedico();
            int idMedico = Sesion.Instancia().UsuarioActual.IdUsuario;

            BE_Turno turno;
            try
            {
                turno = dal.ObtenerTurnoConPaciente(idTurno);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo verificar el turno: " + ex.Message, ex);
            }

            if (turno == null || turno.IdMedico != idMedico)
                throw new ArgumentException("Ese turno no pertenece a su agenda.");
            if (turno.Estado != "Confirmado")
                throw new ArgumentException("Ese turno ya no esta activo.");
            if (turno.Fecha.Date != DateTime.Today)
                throw new ArgumentException("Solo se pueden marcar ausentes los turnos del dia.");
            if (turno.HoraInicio > DateTime.Now.TimeOfDay)
                throw new ArgumentException("Todavia no paso la hora de inicio del turno.");

            try
            {
                dal.ActualizarEstadoTurno(idTurno, "Ausente", idMedico);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Ese turno ya no esta activo.");
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo marcar el turno como Ausente: " + ex.Message, ex);
            }

            bitacora.RegistrarEvento(idMedico, AccionBitacora.TURNO_AUSENTE, "TURNOS",
                $"Turno {idTurno} (paciente {turno.IdPaciente}) marcado como Ausente por el medico {idMedico}.");
        }

        public int ContarTurnosSinAtender(int idMedico)
        {
            try
            {
                ValidarRolMedico();
                ValidarMedicoLogueado(idMedico);
                return dal.ContarPendientes(idMedico, DateTime.Today);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo verificar la agenda del dia: " + ex.Message, ex);
            }
        }

        public int ContarTurnosSinAtenderAnteriores(int idMedico)
        {
            try
            {
                ValidarRolMedico();
                ValidarMedicoLogueado(idMedico);
                return dal.ContarPendientes(idMedico, DateTime.Today.AddDays(-1));
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo verificar la agenda del dia: " + ex.Message, ex);
            }
        }

        public int MarcarAusentesPendientes(int idMedico)
        {
            ValidarRolMedico();
            ValidarMedicoLogueado(idMedico);

            List<int> marcados;
            try
            {
                marcados = dal.MarcarAusentesPendientes(idMedico, DateTime.Today);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudieron marcar los turnos como Ausentes. No se modifico ningun turno. Detalle: " + ex.Message, ex);
            }

            foreach (int idTurno in marcados)
            {
                bitacora.RegistrarEvento(idMedico, AccionBitacora.TURNO_AUSENTE, "TURNOS",
                    $"Turno {idTurno} marcado como Ausente al finalizar la atencion (medico {idMedico}).");
            }

            RegistrarAtencionFinalizada(idMedico, marcados.Count);
            return marcados.Count;
        }

        public void RegistrarAtencionFinalizada(int idMedico, int ausentesMarcados)
        {
            bitacora.RegistrarEvento(idMedico, AccionBitacora.ATENCION_FINALIZADA, Modulo,
                $"Atencion del {DateTime.Today:dd/MM/yyyy} finalizada por el medico {idMedico}. Ausentes marcados al cierre: {ausentesMarcados}.");
        }

        public List<BE_Paciente> BuscarPacientes(string texto)
        {
            ValidarRolMedico();

            texto = (texto ?? "").Trim();
            bool esNumero = texto.Length > 0 && texto.All(char.IsDigit);
            if (!esNumero && texto.Length < 3)
                throw new ArgumentException("Ingrese al menos 3 caracteres del nombre y apellido, o un N° de Historia Clinica / DNI.");
            if (texto.Length > 100)
                throw new ArgumentException("El texto de busqueda es demasiado largo.");

            try
            {
                return dal.BuscarPacientes(texto);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo buscar pacientes: " + ex.Message, ex);
            }
        }

        public List<BE_AtencionMedica> ObtenerHistoriaClinica(int nroHC)
        {
            ValidarRolMedico();

            List<BE_AtencionMedica> atenciones;
            try
            {
                atenciones = dal.ObtenerHistoriaClinica(nroHC);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener la Historia Clinica: " + ex.Message, ex);
            }

            bitacora.RegistrarEvento(Sesion.Instancia().UsuarioActual.IdUsuario, AccionBitacora.HC_CONSULTADA, Modulo,
                $"Historia Clinica N° {nroHC} consultada ({atenciones.Count} atenciones).");

            return atenciones;
        }

        public string CalcularDVH(BE_AtencionMedica a)
        {
            return HashHelper.GenerarHash(
                $"{a.Turno.IdTurno}|{a.FechaHora:yyyyMMddHHmmss}|{a.MotivoConsulta}|{a.Indicaciones}");
        }

        public List<int> VerificarIntegridadAtenciones()
        {
            try
            {
                List<BE_AtencionMedica> atenciones = dal.ListarParaIntegridad();
                List<int> corruptas = atenciones.Where(a => a.DVH != CalcularDVH(a)).Select(a => a.IdAtencion).ToList();

                string dvvAlmacenado = dal_dvv.ObtenerDVV(TablaDVV);
                if (dvvAlmacenado != null && dvvAlmacenado != CalcularDVV(atenciones) && corruptas.Count == 0)
                    corruptas.Add(-1);

                if (corruptas.Count > 0)
                {
                    bitacora.RegistrarEvento(null, AccionBitacora.INTEGRIDAD_ERROR, "SEGURIDAD",
                        corruptas.Contains(-1)
                            ? "Fallo de integridad del DVV en la tabla 'AtencionMedica' (se agregaron o borraron filas fuera del sistema)."
                            : $"Se detectaron {corruptas.Count} atencion(es) medica(s) con DVH invalido (posible alteracion de la Historia Clinica).");
                }

                return corruptas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la integridad de las atenciones: " + ex.Message, ex);
            }
        }

        private void ActualizarDVV()
        {
            try
            {
                dal_dvv.ActualizarDVV(CalcularDVV(dal.ListarParaIntegridad()), TablaDVV);
            }
            catch (Exception ex)
            {
                throw new Exception("La atencion se registro, pero no se pudo actualizar el DVV: " + ex.Message, ex);
            }
        }

        private string CalcularDVV(List<BE_AtencionMedica> atenciones)
        {
            StringBuilder concatenacion = new StringBuilder();
            foreach (BE_AtencionMedica a in atenciones)
                concatenacion.Append(CalcularDVH(a));
            return HashHelper.GenerarHash(concatenacion.ToString());
        }

        private void ValidarRolMedico()
        {
            Sesion sesion = Sesion.Instancia();
            if (sesion.UsuarioActual == null || !sesion.TieneRol("Medico"))
                throw new ArgumentException("Acceso denegado: esta funcion es exclusiva del rol Medico.");
        }

        private void ValidarMedicoLogueado(int idMedico)
        {
            if (Sesion.Instancia().UsuarioActual.IdUsuario != idMedico)
                throw new ArgumentException("Acceso denegado: solo puede consultar su propia agenda.");
        }
    }
}
