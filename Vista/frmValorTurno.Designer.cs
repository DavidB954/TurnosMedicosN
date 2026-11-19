namespace Vista
{
    partial class frmValorTurno
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
            this.dgvEspecialidades = new System.Windows.Forms.DataGridView();
            this.groupBox_ValorTurno = new System.Windows.Forms.GroupBox();
            this.lblEspecialidad = new System.Windows.Forms.Label();
            this.txtEspecialidad = new System.Windows.Forms.TextBox();
            this.lblValorTurno = new System.Windows.Forms.Label();
            this.txtValorTurno = new System.Windows.Forms.TextBox();
            this.lblPorcentajeAusente = new System.Windows.Forms.Label();
            this.txtPorcentajeAusente = new System.Windows.Forms.TextBox();
            this.btnGuardarValorTurno = new System.Windows.Forms.Button();
            this.groupBox_ValorTurno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspecialidades)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEspecialidades
            // 
            this.dgvEspecialidades.AllowUserToAddRows = false;
            this.dgvEspecialidades.AllowUserToDeleteRows = false;
            this.dgvEspecialidades.AllowUserToResizeRows = false;
            this.dgvEspecialidades.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvEspecialidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEspecialidades.MultiSelect = false;
            this.dgvEspecialidades.ReadOnly = true;
            this.dgvEspecialidades.RowHeadersVisible = false;
            this.dgvEspecialidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEspecialidades.Location = new System.Drawing.Point(20, 22);
            this.dgvEspecialidades.Name = "dgvEspecialidades";
            this.dgvEspecialidades.Size = new System.Drawing.Size(580, 220);
            this.dgvEspecialidades.TabIndex = 0;
            this.dgvEspecialidades.SelectionChanged += new System.EventHandler(this.dgvEspecialidades_SelectionChanged);
            // 
            // groupBox_ValorTurno
            // 
            this.groupBox_ValorTurno.Controls.Add(this.lblEspecialidad);
            this.groupBox_ValorTurno.Controls.Add(this.txtEspecialidad);
            this.groupBox_ValorTurno.Controls.Add(this.lblValorTurno);
            this.groupBox_ValorTurno.Controls.Add(this.txtValorTurno);
            this.groupBox_ValorTurno.Controls.Add(this.lblPorcentajeAusente);
            this.groupBox_ValorTurno.Controls.Add(this.txtPorcentajeAusente);
            this.groupBox_ValorTurno.Controls.Add(this.btnGuardarValorTurno);
            this.groupBox_ValorTurno.Location = new System.Drawing.Point(15, 257);
            this.groupBox_ValorTurno.Name = "groupBox_ValorTurno";
            this.groupBox_ValorTurno.Size = new System.Drawing.Size(590, 110);
            this.groupBox_ValorTurno.TabIndex = 1;
            this.groupBox_ValorTurno.TabStop = false;
            this.groupBox_ValorTurno.Text = "Actualizar valor del turno";
            // 
            // lblEspecialidad
            // 
            this.lblEspecialidad.AutoSize = true;
            this.lblEspecialidad.Location = new System.Drawing.Point(15, 38);
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.TabIndex = 2;
            this.lblEspecialidad.Text = "Especialidad:";
            // 
            // txtEspecialidad
            // 
            this.txtEspecialidad.Location = new System.Drawing.Point(145, 33);
            this.txtEspecialidad.Name = "txtEspecialidad";
            this.txtEspecialidad.Size = new System.Drawing.Size(220, 22);
            this.txtEspecialidad.TabIndex = 3;
            this.txtEspecialidad.ReadOnly = true;
            this.txtEspecialidad.TabStop = false;
            // 
            // lblValorTurno
            // 
            this.lblValorTurno.AutoSize = true;
            this.lblValorTurno.Location = new System.Drawing.Point(15, 75);
            this.lblValorTurno.Name = "lblValorTurno";
            this.lblValorTurno.TabIndex = 4;
            this.lblValorTurno.Text = "Valor del turno ($):";
            // 
            // txtValorTurno
            // 
            this.txtValorTurno.Location = new System.Drawing.Point(145, 70);
            this.txtValorTurno.Name = "txtValorTurno";
            this.txtValorTurno.Size = new System.Drawing.Size(100, 22);
            this.txtValorTurno.TabIndex = 5;
            this.txtValorTurno.MaxLength = 15;
            this.txtValorTurno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPorcentajeAusente
            // 
            this.lblPorcentajeAusente.AutoSize = true;
            this.lblPorcentajeAusente.Location = new System.Drawing.Point(260, 75);
            this.lblPorcentajeAusente.Name = "lblPorcentajeAusente";
            this.lblPorcentajeAusente.TabIndex = 6;
            this.lblPorcentajeAusente.Text = "% por ausencia:";
            // 
            // txtPorcentajeAusente
            // 
            this.txtPorcentajeAusente.Location = new System.Drawing.Point(360, 70);
            this.txtPorcentajeAusente.Name = "txtPorcentajeAusente";
            this.txtPorcentajeAusente.Size = new System.Drawing.Size(55, 22);
            this.txtPorcentajeAusente.TabIndex = 7;
            this.txtPorcentajeAusente.MaxLength = 6;
            this.txtPorcentajeAusente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnGuardarValorTurno
            // 
            this.btnGuardarValorTurno.Location = new System.Drawing.Point(435, 68);
            this.btnGuardarValorTurno.Name = "btnGuardarValorTurno";
            this.btnGuardarValorTurno.Size = new System.Drawing.Size(140, 26);
            this.btnGuardarValorTurno.TabIndex = 8;
            this.btnGuardarValorTurno.Text = "Guardar valor del turno";
            this.btnGuardarValorTurno.UseVisualStyleBackColor = true;
            this.btnGuardarValorTurno.Click += new System.EventHandler(this.btnGuardarValorTurno_Click);
            // 
            // frmValorTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 382);
            this.Controls.Add(this.groupBox_ValorTurno);
            this.Controls.Add(this.dgvEspecialidades);
            this.Name = "frmValorTurno";
            this.Text = "Valor del turno por especialidad";
            this.Load += new System.EventHandler(this.frmValorTurno_Load);
            this.groupBox_ValorTurno.ResumeLayout(false);
            this.groupBox_ValorTurno.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspecialidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEspecialidades;
        private System.Windows.Forms.GroupBox groupBox_ValorTurno;
        private System.Windows.Forms.Label lblEspecialidad;
        private System.Windows.Forms.TextBox txtEspecialidad;
        private System.Windows.Forms.Label lblValorTurno;
        private System.Windows.Forms.TextBox txtValorTurno;
        private System.Windows.Forms.Label lblPorcentajeAusente;
        private System.Windows.Forms.TextBox txtPorcentajeAusente;
        private System.Windows.Forms.Button btnGuardarValorTurno;
    }
}
