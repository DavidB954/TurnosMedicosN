using BLL;
using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class ucSelectorEspecialidad : UserControl
    {
        BLL_Especialidad bll_especialidad = new BLL_Especialidad();

        public event EventHandler EspecialidadSeleccionada;

        public ucSelectorEspecialidad()
        {
            InitializeComponent();

            cboEspecialidad.DisplayMember = "Nombre";
            cboEspecialidad.ValueMember = "IdEspecialidad";

            cboEspecialidad.SelectedIndexChanged += (s, e) =>
            {
                if (EspecialidadSeleccionada != null)
                    EspecialidadSeleccionada(this, EventArgs.Empty);
            };
        }

        public string Etiqueta
        {
            get { return lblEtiqueta.Text; }
            set
            {
                lblEtiqueta.Text = value;
                lblEtiqueta.Visible = !string.IsNullOrEmpty(value);
                int izquierda = lblEtiqueta.Visible ? lblEtiqueta.Left + lblEtiqueta.PreferredWidth + 6 : 0;
                cboEspecialidad.SetBounds(izquierda, 0, Math.Max(80, Width - izquierda), cboEspecialidad.Height);
            }
        }

        public int? IdEspecialidadSeleccionada
        {
            get { return cboEspecialidad.SelectedValue == null ? (int?)null : Convert.ToInt32(cboEspecialidad.SelectedValue); }
        }

        public void CargarEspecialidadesPorMedico(int idMedico)
        {
            try
            {
                cboEspecialidad.DataSource = bll_especialidad.ListarEspecialidadesPorMedico(idMedico);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        public void Limpiar()
        {
            cboEspecialidad.DataSource = null;
        }

        public void SeleccionarEspecialidad(int idEspecialidad)
        {
            cboEspecialidad.SelectedValue = idEspecialidad;
        }

        public void CargarEspecialidades()
        {
            try
            {
                cboEspecialidad.DataSource = bll_especialidad.ListarEspecialidades();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        private void ucSelectorEspecialidad_Load(object sender, EventArgs e)
        {

        }
    }
}
