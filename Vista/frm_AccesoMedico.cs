using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class frm_AccesoMedico : frmBase
    {
        public frm_AccesoMedico()
        {
            InitializeComponent();
        }

        private void frm_AccesoMedico_Load(object sender, EventArgs e)
        {
            CargarDatosDeEjemplo();
        }

        // Datos de ejemplo hardcodeados solo para que la pantalla no se vea vacía.
        // El módulo de Turnos todavía no tiene BE/BLL/DAL propios: esto es puramente
        // visual, no persiste nada ni se lee de la base.
        private void CargarDatosDeEjemplo()
        {
            dgv_PacientesDelDia.DataSource = new[]
            {
                new { Hora = "09:00", Paciente = "Pedro Gómez", Motivo = "Control de rutina", Estado = "En espera" },
                new { Hora = "09:30", Paciente = "Lucía Fernández", Motivo = "Dolor de cabeza persistente", Estado = "Atendido" },
                new { Hora = "10:00", Paciente = "Martín Alvarez", Motivo = "Renovación de receta", Estado = "En espera" },
                new { Hora = "10:30", Paciente = "Sofía Torres", Motivo = "Estudios de rutina", Estado = "En espera" },
            };

            txtPacienteSeleccionado.Text = "Pedro Gómez";
            txtMotivoConsulta.Text = "Dolor de cabeza persistente hace 3 días, sin fiebre.";
            rb_RecetaSI.Checked = true;
            txtIndicaciones.Text = "Reposo, hidratación abundante. Control en 7 días si persisten los síntomas.";
            txtMedicamento.Text = "Ibuprofeno 400mg cada 8hs por 5 días";

            lstHistoriaClinica.Items.Clear();
            lstHistoriaClinica.Items.Add("12/05/2026 - Control de rutina - Dr. Juan Pérez");
            lstHistoriaClinica.Items.Add("03/03/2026 - Consulta por dolor lumbar - Dra. Laura Sánchez");
            lstHistoriaClinica.Items.Add("18/12/2025 - Vacunación antigripal - Dr. Juan Pérez");
            lstHistoriaClinica.Items.Add("22/09/2025 - Estudios de laboratorio de rutina - Dr. Martín Gómez");
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }
    }
}
