namespace Vista
{
    partial class ucSelectorMedico
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
            this.cboMedico = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            //
            // lblEtiqueta
            //
            this.lblEtiqueta.AutoSize = true;
            this.lblEtiqueta.Location = new System.Drawing.Point(0, 6);
            this.lblEtiqueta.Name = "lblEtiqueta";
            this.lblEtiqueta.Size = new System.Drawing.Size(100, 20);
            this.lblEtiqueta.TabIndex = 0;
            this.lblEtiqueta.Text = "Medico:";
            //
            // cboMedico
            //
            this.cboMedico.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMedico.FormattingEnabled = true;
            this.cboMedico.Location = new System.Drawing.Point(160, 2);
            this.cboMedico.Name = "cboMedico";
            this.cboMedico.Size = new System.Drawing.Size(220, 28);
            this.cboMedico.TabIndex = 1;
            //
            // ucSelectorMedico
            //
            this.Controls.Add(this.cboMedico);
            this.Controls.Add(this.lblEtiqueta);
            this.Name = "ucSelectorMedico";
            this.Size = new System.Drawing.Size(390, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEtiqueta;
        private System.Windows.Forms.ComboBox cboMedico;
    }
}
