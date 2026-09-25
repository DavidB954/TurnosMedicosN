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

            // DisplayMember/ValueMember deben quedar seteados ANTES de asignar DataSource:
            // al asignar DataSource el combo dispara SelectedIndexChanged en el acto, y si en
            // ese momento ValueMember todavia esta vacio, SelectedValue devuelve el objeto
            // BE_Especialidad completo en vez del IdEspecialidad (revienta cualquier cascada
            // que escuche este evento y haga Convert.ToInt32).
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
            set { lblEtiqueta.Text = value; }
        }

        public int? IdEspecialidadSeleccionada
        {
            get { return cboEspecialidad.SelectedValue == null ? (int?)null : Convert.ToInt32(cboEspecialidad.SelectedValue); }
        }

        public void CargarEspecialidadesPorMedico(int idMedico)
        {
            try
            {
                cboEspecialidad.DataSource = bll_especialidad._listaEspecialidadesPorMedico(idMedico);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                cboEspecialidad.DataSource = bll_especialidad._listaEspecialidad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
