using System;
using System.Collections.Generic;
using System.Linq;

namespace BE
{
    public class BE_MedicoHorario
    {
        public int IdHorario { get; set; }

        public int IdMedico { get; set; }

        public int DiaSemana { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public bool Activo { get; set; }

        public bool InactivoEnFecha { get; set; }

        public string PacienteEnFecha { get; set; }

        public int? IdEspecialidad { get; set; }

        public string NombreEspecialidad { get; set; }

        public string NombreApellido { get; set; }

        public string Especialidades { get; set; }

        public string DiaSemanaTexto { get { return NombreDia(DiaSemana); } }

        public string RangoHorario { get { return string.Format("{0:hh\\:mm} - {1:hh\\:mm}", HoraInicio, HoraFin); } }

        public string EstadoTexto { get { return Activo ? "Si" : "No"; } }

        public string DiaYRango { get { return DiaSemanaTexto + " " + RangoHorario; } }

        public static string ResumirBloques(IEnumerable<BE_MedicoHorario> franjas)
        {
            List<BE_MedicoHorario> ordenadas = franjas.OrderBy(f => f.HoraInicio).ToList();
            if (ordenadas.Count == 0)
                return string.Empty;

            List<string> bloques = new List<string>();
            TimeSpan inicio = ordenadas[0].HoraInicio;
            TimeSpan fin = ordenadas[0].HoraFin;

            for (int i = 1; i < ordenadas.Count; i++)
            {
                if (ordenadas[i].HoraInicio == fin)
                {
                    fin = ordenadas[i].HoraFin;
                }
                else
                {
                    bloques.Add(string.Format("{0:hh\\:mm} a {1:hh\\:mm}", inicio, fin));
                    inicio = ordenadas[i].HoraInicio;
                    fin = ordenadas[i].HoraFin;
                }
            }
            bloques.Add(string.Format("{0:hh\\:mm} a {1:hh\\:mm}", inicio, fin));

            return string.Join(" y ", bloques);
        }

        public static string NombreDia(int diaSemana)
        {
            switch (diaSemana)
            {
                case 1: return "Lunes";
                case 2: return "Martes";
                case 3: return "Miercoles";
                case 4: return "Jueves";
                case 5: return "Viernes";
                case 6: return "Sabado";
                default: return string.Empty;
            }
        }
    }
}
