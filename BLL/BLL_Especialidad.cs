using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Especialidad
    {
        BE_Especialidad be_especialidad = new BE_Especialidad();
        DAL_Especialidad dal_especialidad = new DAL_Especialidad();

        public void AgregarEspecialidad(string nombre)
        {
            be_especialidad.nombre = nombre;
            dal_especialidad.CrearEspecialidad(be_especialidad);
        }

        public void EliminarEspecialidad(int id)
        {
            dal_especialidad.EliminarEspecialidad(id);
        }

        public List<BE_Especialidad> _listaEspecialidad()
        {
            return dal_especialidad.listaEspecialidad();
        }

        public List<BE_Usuario> _listaMedicos()
        {
            return dal_especialidad.listaMedicos();
        }

        public List<BE_Usuario> _listaMedicosPorEspecialidad(int idEspecialidad)
        {
            return dal_especialidad.listaMedicosPorEspecialidad(idEspecialidad);
        }

        public List<BE_Especialidad> _listaEspecialidadesPorMedico(int idMedico)
        {
            return dal_especialidad.listaEspecialidadesPorMedico(idMedico);
        }

        public void AsignarEspecialidadMedico(int idMedico, int idEspecialidad)
        {
            dal_especialidad.AsignarEspecialidadMedico(idMedico, idEspecialidad);
        }

        public void DesasignarEspecialidadMedico(int idMedico, int idEspecialidad)
        {
            dal_especialidad.DesasignarEspecialidadMedico(idMedico, idEspecialidad);
        }
    }
}
