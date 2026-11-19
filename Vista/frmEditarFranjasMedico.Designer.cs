namespace Vista
{
    partial class frmEditarFranjasMedico
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblMedico = new System.Windows.Forms.Label();
            this.ucMedico = new Vista.ucSelectorMedico();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblHorarios = new System.Windows.Forms.Label();
            this.dgvHorarios = new System.Windows.Forms.DataGridView();
            this.btnInactivar = new System.Windows.Forms.Button();
            this.btnReactivar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHorarios)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMedico
            // 
            this.lblMedico.AutoSize = true;
            this.lblMedico.Location = new System.Drawing.Point(20, 25);
            this.lblMedico.Name = "lblMedico";
            this.lblMedico.TabIndex = 0;
            this.lblMedico.Text = "Médico:";
            // 
            // ucMedico
            // 
            this.ucMedico.Location = new System.Drawing.Point(85, 20);
            this.ucMedico.Name = "ucMedico";
            this.ucMedico.Size = new System.Drawing.Size(220, 22);
            this.ucMedico.TabIndex = 1;
            this.ucMedico.Etiqueta = "";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(325, 25);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(375, 20);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(120, 22);
            this.dtpFecha.TabIndex = 3;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // lblHorarios
            // 
            this.lblHorarios.AutoSize = true;
            this.lblHorarios.Location = new System.Drawing.Point(20, 60);
            this.lblHorarios.Name = "lblHorarios";
            this.lblHorarios.Size = new System.Drawing.Size(580, 20);
            this.lblHorarios.TabIndex = 4;
            this.lblHorarios.Text = "Horarios del día";
            this.lblHorarios.AutoSize = false;
            // 
            // dgvHorarios
            // 
            this.dgvHorarios.AllowUserToAddRows = false;
            this.dgvHorarios.AllowUserToDeleteRows = false;
            this.dgvHorarios.AllowUserToResizeRows = false;
            this.dgvHorarios.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvHorarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHorarios.MultiSelect = false;
            this.dgvHorarios.ReadOnly = true;
            this.dgvHorarios.RowHeadersVisible = false;
            this.dgvHorarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHorarios.Location = new System.Drawing.Point(20, 80);
            this.dgvHorarios.Name = "dgvHorarios";
            this.dgvHorarios.Size = new System.Drawing.Size(580, 295);
            this.dgvHorarios.TabIndex = 5;
            this.dgvHorarios.MultiSelect = true;
            // 
            // btnInactivar
            // 
            this.btnInactivar.Location = new System.Drawing.Point(20, 390);
            this.btnInactivar.Name = "btnInactivar";
            this.btnInactivar.Size = new System.Drawing.Size(170, 26);
            this.btnInactivar.TabIndex = 6;
            this.btnInactivar.Text = "Inactivar en esta fecha";
            this.btnInactivar.UseVisualStyleBackColor = true;
            this.btnInactivar.Click += new System.EventHandler(this.btnInactivar_Click);
            // 
            // btnReactivar
            // 
            this.btnReactivar.Location = new System.Drawing.Point(200, 390);
            this.btnReactivar.Name = "btnReactivar";
            this.btnReactivar.Size = new System.Drawing.Size(170, 26);
            this.btnReactivar.TabIndex = 7;
            this.btnReactivar.Text = "Reactivar en esta fecha";
            this.btnReactivar.UseVisualStyleBackColor = true;
            this.btnReactivar.Click += new System.EventHandler(this.btnReactivar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(490, 390);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(110, 26);
            this.btnCerrar.TabIndex = 8;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            // 
            // frmEditarFranjasMedico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 432);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnReactivar);
            this.Controls.Add(this.btnInactivar);
            this.Controls.Add(this.dgvHorarios);
            this.Controls.Add(this.lblHorarios);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.ucMedico);
            this.Controls.Add(this.lblMedico);
            this.CancelButton = this.btnCerrar;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "frmEditarFranjasMedico";
            this.Text = "Inactivar horarios en una fecha";
            this.Load += new System.EventHandler(this.frmEditarFranjasMedico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHorarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblMedico;
        private Vista.ucSelectorMedico ucMedico;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblHorarios;
        private System.Windows.Forms.DataGridView dgvHorarios;
        private System.Windows.Forms.Button btnInactivar;
        private System.Windows.Forms.Button btnReactivar;
        private System.Windows.Forms.Button btnCerrar;
    }
}
