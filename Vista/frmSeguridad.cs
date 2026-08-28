using BE;
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
    public partial class frmSeguridad : frmBase
    {
        public frmSeguridad()
        {
            InitializeComponent();
        }
        BLL_DVV bll_dvv = new BLL_DVV();
        BLL_Usuario bll_usuario = new BLL_Usuario();

        public void MostrarCorruptos(List<BE_RegistrosCorruptos> lista)
        {
            dgvSeguridad.DataSource = null;
            dgvSeguridad.DataSource = lista;

            dgvSeguridad.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSeguridad.Columns["IdUsuario"].HeaderText = "ID Usuario";
            dgvSeguridad.Columns["DVH_Almacenado"].HeaderText = "DVH Guardado";
            dgvSeguridad.Columns["DVH_Recalculado"].HeaderText = "DVH Recalculado";

            btnBackUp.Enabled = false;
        }
        public override void AplicarIdioma()    
        {
            AplicarIdiomaAutomatico();
        }

        private void frm_Seguridad_Load(object sender, EventArgs e)
        {

        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Backup SQL (*.bak)|*.bak";
                    sfd.Title = "Guardar Backup de la Base de Datos";
                    sfd.InitialDirectory = @"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup";
                    sfd.FileName = "Backup_GestionTurnosMedicos.bak";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string rutaBackup = sfd.FileName;

                        bll_dvv.GenerarBackUp(rutaBackup);

                        MessageBox.Show("Backup generado correctamente en: " + rutaBackup);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnRestaurarBD_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Backup SQL (*.bak)|*.bak";
                    ofd.Title = "Seleccionar Backup para Restaurar";
                    ofd.InitialDirectory = @"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string rutaBackup = ofd.FileName;

                        bll_dvv.RestaurarBackup(rutaBackup);

                        bll_dvv.ActualizarDVV("Usuario");

                        MessageBox.Show("Base restaurada correctamente desde: " + rutaBackup + " " + "La aplicación se cerrará para actualizar la información.", "Restauración exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Application.Restart();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBloquearUsuYRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> idsUsuarios = new List<int>();

                foreach (DataGridViewRow row in dgvSeguridad.Rows)
                {
                    if (row.Cells["IdUsuario"].Value != null)
                    {
                        idsUsuarios.Add(Convert.ToInt32(row.Cells["IdUsuario"].Value));
                    }
                }
                foreach (int idUsuario in idsUsuarios)
                {
                    BE_Usuario usuario = bll_usuario.ObtenerUsuarioPorId(idUsuario);

                    if (usuario == null)
                        continue; // si no existe, lo salteamos

                    // Bloquear usuario
                    usuario.Activo = false;

                    // Recalcular DVH
                    bll_usuario.CalcularDVH(usuario);

                    // Guardar cambios en BD
                    bll_usuario.ModificarUsuario(usuario);
                }

                // Al final, recalculamos DVV de la tabla completa
                bll_dvv.ActualizarDVV("Usuario");

                MessageBox.Show("Usuarios bloqueados y DVH/DVV recalculados." + " " + "La aplicación se cerrará para actualizar la información.", "Restauración exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
