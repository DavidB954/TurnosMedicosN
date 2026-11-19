using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Especialidad
    {
        DAL_Especialidad dal_especialidad = new DAL_Especialidad();
        BLL_Bitacora bll_bitacora = new BLL_Bitacora();

        // Tope razonable para detectar errores de tipeo (decimal(12,2) en la base).
        private const decimal ValorTurnoMaximo = 9999999999.99m;

        public void AgregarEspecialidad(string nombre)
        {
            dal_especialidad.CrearEspecialidad(new BE_Especialidad { Nombre = nombre });
        }

        public void EliminarEspecialidad(int id)
        {
            dal_especialidad.EliminarEspecialidad(id);
        }

        public List<BE_Especialidad> ListarEspecialidades()
        {
            return dal_especialidad.ListarEspecialidades();
        }

        public List<BE_Usuario> ListarMedicos()
        {
            return dal_especialidad.ListarMedicos();
        }

        public List<BE_Usuario> ListarMedicosPorEspecialidad(int idEspecialidad)
        {
            return dal_especialidad.ListarMedicosPorEspecialidad(idEspecialidad);
        }

        public List<BE_Especialidad> ListarEspecialidadesPorMedico(int idMedico)
        {
            return dal_especialidad.ListarEspecialidadesPorMedico(idMedico);
        }

        public void AsignarEspecialidadMedico(int idMedico, int idEspecialidad)
        {
            dal_especialidad.AsignarEspecialidadMedico(idMedico, idEspecialidad);
        }

        public void DesasignarEspecialidadMedico(int idMedico, int idEspecialidad)
        {
            dal_especialidad.DesasignarEspecialidadMedico(idMedico, idEspecialidad);
        }

        // CU-02.1 Administrar valor del turno por especialidad.
        public void ActualizarValorTurno(int idEspecialidad, decimal valorTurno, decimal porcentajeAusente)
        {
            ValidarValorTurno(valorTurno, porcentajeAusente);

            BE_Especialidad especialidad = dal_especialidad.ListarEspecialidades()
                .FirstOrDefault(e => e.IdEspecialidad == idEspecialidad);
            if (especialidad == null)
                throw new ArgumentException("La especialidad no existe.");

            try
            {
                dal_especialidad.ActualizarValorTurno(idEspecialidad, valorTurno, porcentajeAusente);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo actualizar el valor del turno: " + ex.Message, ex);
            }

            bll_bitacora.RegistrarEvento(Sesion.Instancia().UsuarioActual?.IdUsuario, AccionBitacora.VALOR_TURNO_ACTUALIZADO,
                "HONORARIOS", $"Valor del turno de {especialidad.Nombre}: de {Formato.Moneda(especialidad.ValorTurno)} " +
                              $"({Formato.Porcentaje(especialidad.PorcentajeAusente)} por ausencia) a {Formato.Moneda(valorTurno)} " +
                              $"({Formato.Porcentaje(porcentajeAusente)} por ausencia).");
        }

        // RN2.4: el valor del turno tiene que ser mayor que cero y el porcentaje por ausencia estar entre
        // 0 y 100 (flujo alternativo A2 de CU-02.1).
        private void ValidarValorTurno(decimal valorTurno, decimal porcentajeAusente)
        {
            if (valorTurno <= 0)
                throw new ArgumentException("El valor del turno debe ser mayor que cero.");
            if (valorTurno > ValorTurnoMaximo)
                throw new ArgumentException("El valor del turno es demasiado grande.");
            if (decimal.Round(valorTurno, 2) != valorTurno)
                throw new ArgumentException("El valor del turno admite como máximo dos decimales.");
            if (porcentajeAusente < 0 || porcentajeAusente > 100)
                throw new ArgumentException("El porcentaje por ausencia debe estar entre 0 y 100.");
            if (decimal.Round(porcentajeAusente, 2) != porcentajeAusente)
                throw new ArgumentException("El porcentaje por ausencia admite como máximo dos decimales.");
        }
    }
}
