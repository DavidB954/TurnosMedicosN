namespace Vista
{
    partial class ucSelectorEspecialidad
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblEtiqueta = new System.Windows.Forms.Label();
            this.cboEspecialidad = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            //
            // lblEtiqueta
            //
            this.lblEtiqueta.AutoSize = true;
            this.lblEtiqueta.Location = new System.Drawing.Point(0, 6);
            this.lblEtiqueta.Name = "lblEtiqueta";
            this.lblEtiqueta.Size = new System.Drawing.Size(130, 20);
            this.lblEtiqueta.TabIndex = 0;
            this.lblEtiqueta.Text = "Especialidad:";
            //
            // cboEspecialidad
            //
            this.cboEspecialidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEspecialidad.FormattingEnabled = true;
            this.cboEspecialidad.Location = new System.Drawing.Point(160, 2);
            this.cboEspecialidad.Name = "cboEspecialidad";
            this.cboEspecialidad.Size = new System.Drawing.Size(220, 28);
            this.cboEspecialidad.TabIndex = 1;
            //
            // ucSelectorEspecialidad
            //
            this.Controls.Add(this.cboEspecialidad);
            this.Controls.Add(this.lblEtiqueta);
            this.Name = "ucSelectorEspecialidad";
            this.Size = new System.Drawing.Size(390, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEtiqueta;
        private System.Windows.Forms.ComboBox cboEspecialidad;
    }
}
