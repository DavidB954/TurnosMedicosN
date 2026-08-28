using BLL;
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
    public partial class frmBitacora : frmBase
    {
        public frmBitacora()
        {
            InitializeComponent();
        }
        public override void AplicarIdioma()  
        {
            AplicarIdiomaAutomatico();
        }
        private void frm_Bitacora_Load(object sender, EventArgs e)
        {
            try
            {
                LimpiarActualizar();
                CboModulos();
                CargarComboUsuarios();

                dgvBitacora.MultiSelect = false;
                dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                cboModulo.SelectedIndex = -1;
                cboUsuario.SelectedIndex = -1;
                dtpDesde.Checked = false;
                dtpHasta.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar la bitácora",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        BLL_Bitacora bll_bitacora = new BLL_Bitacora();

        public void LimpiarActualizar()
        {
            try
            {
                dgvBitacora.DataSource = null;
                dgvBitacora.DataSource = bll_bitacora.ObtenerBitacora();
                FormatearGridBitacora();

                int contador = 0;
                foreach (DataGridViewRow fila in dgvBitacora.Rows)
                {
                    contador++;
                }

                lblTotalRegistros.Text = contador.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Oculta los IDs crudos y muestra el nombre del usuario en su lugar

        private void FormatearGridBitacora()
        {
            if (dgvBitacora.Columns["IdBitacora"] != null)
                dgvBitacora.Columns["IdBitacora"].Visible = false;

            if (dgvBitacora.Columns["IdUsuario"] != null)
                dgvBitacora.Columns["IdUsuario"].Visible = false;

            if (dgvBitacora.Columns["NombreUsuario"] != null)
            {
                dgvBitacora.Columns["NombreUsuario"].HeaderText = "Usuario";
                dgvBitacora.Columns["NombreUsuario"].DisplayIndex = 1;
            }
        }

        private void btnActualizarBitacora_Click(object sender, EventArgs e)
        {
            LimpiarActualizar();
        }

        private void dgvBitacora_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) 
                    return;


                var Fila = dgvBitacora.Rows[e.RowIndex];

                if (Fila == null)
                {
                    return;
                }
                else
                {
                    txtFecha.Text = Fila.Cells["FechaHora"].Value.ToString();
                    txtUsuario.Text = Fila.Cells["NombreUsuario"].Value?.ToString() ?? "";
                    txtAccion.Text = Fila.Cells["Accion"].Value.ToString();
                    txtModulo.Text = Fila.Cells["Modulo"].Value.ToString();
                    txtIP.Text = Fila.Cells["IP"].Value.ToString();
                    txtNombreHost.Text = Fila.Cells["NombreMaquina"].Value.ToString();
                    txtDescripcion.Text = Fila.Cells["Descripcion"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void CboModulos()
        {
            cboModulo.Items.Add("LOGIN");
            cboModulo.Items.Add("USUARIO");
            cboModulo.Items.Add("SEGURIDAD");
            cboModulo.Items.Add("ROLES");
            cboModulo.Items.Add("IDIOMA");
            cboModulo.SelectedIndex = -1;
        }
        public int? IdSeleccionado
        {
            get
            {
                if (cboUsuario.SelectedIndex == -1)
                    return null;
                return (int)cboUsuario.SelectedValue;
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? desde = dtpDesde.Checked ? dtpDesde.Value.Date : (DateTime?)null;
                DateTime? hasta = dtpHasta.Checked ? dtpHasta.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;
                int? idUsuario = IdSeleccionado;
                string modulo = string.IsNullOrEmpty(cboModulo.Text) ? null : cboModulo.Text;
                string ip = string.IsNullOrEmpty(txtBuscarPorIP.Text) ? null : txtBuscarPorIP.Text;

                dgvBitacora.DataSource = null;

                DataTable rdo = bll_bitacora.FiltrarBitacora(desde, hasta, idUsuario, modulo, ip);

                dgvBitacora.DataSource = rdo;
                FormatearGridBitacora();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar en la bitácora",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            try
            {
                dtpDesde.Value = DateTime.Now;
                dtpDesde.Checked = false;
                dtpHasta.Value = DateTime.Now;
                dtpHasta.Checked = false;
                CargarComboUsuarios();
                cboModulo.SelectedIndex = -1;
                cboUsuario.SelectedIndex = -1;
                txtBuscarPorIP.Clear();

                LimpiarActualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public void CargarComboUsuarios()
        {
            BLL.BLL_Usuario bll_usuario = new BLL.BLL_Usuario();
            cboUsuario.DataSource = bll_usuario.ListaUsuarios();
            cboUsuario.DisplayMember = "NombreApellido";
            cboUsuario.ValueMember = "IdUsuario";
        }
    }
}
