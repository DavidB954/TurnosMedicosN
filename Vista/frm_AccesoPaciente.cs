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
    public partial class frm_AccesoPaciente : frmBase
    {
        public frm_AccesoPaciente()
        {
            InitializeComponent();
        }

        private void frm_AccesoPaciente_Load(object sender, EventArgs e)
        {
            CargarDatosDeEjemplo();
        }

        // Datos de ejemplo hardcodeados solo para que la pantalla no se vea vacía.
        // El módulo de Turnos todavía no tiene BE/BLL/DAL propios: esto es puramente
        // visual, no persiste nada ni se lee de la base.
        private void CargarDatosDeEjemplo()
        {
            cboEspecialidades.Items.Clear();
            cboEspecialidades.Items.AddRange(new object[]
            {
                "Clínica Médica", "Cardiología", "Pediatría", "Traumatología", "Dermatología"
            });
            cboEspecialidades.SelectedIndex = 0;

            cboMedicos.Items.Clear();
            cboMedicos.Items.AddRange(new object[]
            {
                "Dr. Juan Pérez", "Dra. Laura Sánchez", "Dr. Martín Gómez"
            });
            cboMedicos.SelectedIndex = 0;

            lstHorarios.Items.Clear();
            lstHorarios.Items.AddRange(new object[] { "09:00", "09:30", "10:00", "10:30", "11:00", "11:30" });

            dataGridView1.DataSource = new[]
            {
                new { Fecha = "05/07/2026", Hora = "09:00", Especialidad = "Clínica Médica", Medico = "Dr. Juan Pérez", Estado = "Confirmado" },
                new { Fecha = "12/07/2026", Hora = "10:30", Especialidad = "Cardiología", Medico = "Dra. Laura Sánchez", Estado = "Pendiente" },
                new { Fecha = "20/07/2026", Hora = "11:00", Especialidad = "Dermatología", Medico = "Dr. Martín Gómez", Estado = "Confirmado" },
            };
        }

        public override void AplicarIdioma()
        {
            AplicarIdiomaAutomatico();
        }


    }
}
