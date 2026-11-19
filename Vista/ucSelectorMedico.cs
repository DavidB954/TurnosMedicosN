using BLL;
using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class ucSelectorMedico : UserControl
    {
        BLL_Especialidad bll_especialidad = new BLL_Especialidad();

        public event EventHandler MedicoSeleccionado;

        public ucSelectorMedico()
        {
            InitializeComponent();

            // Ver comentario equivalente en ucSelectorEspecialidad: DisplayMember/ValueMember
            // van antes de tocar DataSource para que SelectedValue nunca devuelva el BE_Usuario
            // completo durante el SelectedIndexChanged que dispara la propia asignacion.
            cboMedico.DisplayMember = "NombreApellido";
            cboMedico.ValueMember = "IdUsuario";

            cboMedico.SelectedIndexChanged += (s, e) =>
            {
                if (MedicoSeleccionado != null)
                    MedicoSeleccionado(this, EventArgs.Empty);
            };
        }

        public string Etiqueta
        {
            get { return lblEtiqueta.Text; }
            set
            {
                lblEtiqueta.Text = value;
                // Sin etiqueta (la pone la pantalla, como en los prototipos) el combo ocupa todo el
                // control; con etiqueta, arranca despues de ella para que no la tape.
                lblEtiqueta.Visible = !string.IsNullOrEmpty(value);
                int izquierda = lblEtiqueta.Visible ? lblEtiqueta.Left + lblEtiqueta.PreferredWidth + 6 : 0;
                cboMedico.SetBounds(izquierda, 0, Math.Max(80, Width - izquierda), cboMedico.Height);
            }
        }

        public int? IdMedicoSeleccionado
        {
            get { return cboMedico.SelectedValue == null ? (int?)null : Convert.ToInt32(cboMedico.SelectedValue); }
        }

        public void SeleccionarMedico(int idMedico)
        {
            cboMedico.SelectedValue = idMedico;
        }

        public void CargarMedicos()
        {
            try
            {
                cboMedico.DataSource = bll_especialidad.ListarMedicos();
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        public void CargarMedicosPorEspecialidad(int idEspecialidad)
        {
            try
            {
                cboMedico.DataSource = bll_especialidad.ListarMedicosPorEspecialidad(idEspecialidad);
            }
            catch (Exception ex)
            {
                Errores.Mostrar(ex);
            }
        }

        public void Limpiar()
        {
            cboMedico.DataSource = null;
        }
    }
}
