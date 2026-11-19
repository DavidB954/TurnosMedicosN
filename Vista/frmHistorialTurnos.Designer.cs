namespace Vista
{
    partial class frmHistorialTurnos
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
            this.groupBoxFiltros = new System.Windows.Forms.GroupBox();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.txtPaciente = new System.Windows.Forms.TextBox();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblTurnos = new System.Windows.Forms.Label();
            this.dgvTurnos = new System.Windows.Forms.DataGridView();
            this.lblVersiones = new System.Windows.Forms.Label();
            this.dgvVersiones = new System.Windows.Forms.DataGridView();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.groupBoxFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVersiones)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxFiltros
            // 
            this.groupBoxFiltros.Controls.Add(this.lblPaciente);
            this.groupBoxFiltros.Controls.Add(this.txtPaciente);
            this.groupBoxFiltros.Controls.Add(this.lblDesde);
            this.groupBoxFiltros.Controls.Add(this.dtpDesde);
            this.groupBoxFiltros.Controls.Add(this.lblHasta);
            this.groupBoxFiltros.Controls.Add(this.dtpHasta);
            this.groupBoxFiltros.Controls.Add(this.btnBuscar);
            this.groupBoxFiltros.Location = new System.Drawing.Point(15, 17);
            this.groupBoxFiltros.Name = "groupBoxFiltros";
            this.groupBoxFiltros.Size = new System.Drawing.Size(730, 65);
            this.groupBoxFiltros.TabIndex = 0;
            this.groupBoxFiltros.TabStop = false;
            this.groupBoxFiltros.Text = "Buscar turnos";
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Location = new System.Drawing.Point(15, 36);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.TabIndex = 1;
            this.lblPaciente.Text = "Paciente (DNI o nombre):";
            // 
            // txtPaciente
            // 
            this.txtPaciente.Location = new System.Drawing.Point(170, 31);
            this.txtPaciente.Name = "txtPaciente";
            this.txtPaciente.Size = new System.Drawing.Size(170, 22);
            this.txtPaciente.TabIndex = 2;
            this.txtPaciente.MaxLength = 100;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(355, 36);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.TabIndex = 3;
            this.lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.CustomFormat = "dd/MM/yyyy";
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDesde.Location = new System.Drawing.Point(405, 31);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(100, 22);
            this.dtpDesde.TabIndex = 4;
            this.dtpDesde.ShowCheckBox = true;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(515, 36);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.TabIndex = 5;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.CustomFormat = "dd/MM/yyyy";
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHasta.Location = new System.Drawing.Point(565, 31);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(100, 22);
            this.dtpHasta.TabIndex = 6;
            this.dtpHasta.ShowCheckBox = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(670, 29);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(55, 26);
            this.btnBuscar.TabIndex = 7;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblTurnos
            // 
            this.lblTurnos.AutoSize = true;
            this.lblTurnos.Location = new System.Drawing.Point(20, 97);
            this.lblTurnos.Name = "lblTurnos";
            this.lblTurnos.TabIndex = 8;
            this.lblTurnos.Text = "Turnos";
            // 
            // dgvTurnos
            // 
            this.dgvTurnos.AllowUserToAddRows = false;
            this.dgvTurnos.AllowUserToDeleteRows = false;
            this.dgvTurnos.AllowUserToResizeRows = false;
            this.dgvTurnos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTurnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTurnos.MultiSelect = false;
            this.dgvTurnos.ReadOnly = true;
            this.dgvTurnos.RowHeadersVisible = false;
            this.dgvTurnos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTurnos.Location = new System.Drawing.Point(20, 117);
            this.dgvTurnos.Name = "dgvTurnos";
            this.dgvTurnos.Size = new System.Drawing.Size(720, 150);
            this.dgvTurnos.TabIndex = 9;
            this.dgvTurnos.SelectionChanged += new System.EventHandler(this.dgvTurnos_SelectionChanged);
            // 
            // lblVersiones
            // 
            this.lblVersiones.AutoSize = true;
            this.lblVersiones.Location = new System.Drawing.Point(20, 283);
            this.lblVersiones.Name = "lblVersiones";
            this.lblVersiones.TabIndex = 10;
            this.lblVersiones.Text = "Versiones del turno seleccionado";
            // 
            // dgvVersiones
            // 
            this.dgvVersiones.AllowUserToAddRows = false;
            this.dgvVersiones.AllowUserToDeleteRows = false;
            this.dgvVersiones.AllowUserToResizeRows = false;
            this.dgvVersiones.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvVersiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVersiones.MultiSelect = false;
            this.dgvVersiones.ReadOnly = true;
            this.dgvVersiones.RowHeadersVisible = false;
            this.dgvVersiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVersiones.Location = new System.Drawing.Point(20, 303);
            this.dgvVersiones.Name = "dgvVersiones";
            this.dgvVersiones.Size = new System.Drawing.Size(720, 180);
            this.dgvVersiones.TabIndex = 11;
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(520, 497);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(220, 26);
            this.btnRestaurar.TabIndex = 12;
            this.btnRestaurar.Text = "Restaurar versión seleccionada";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // frmHistorialTurnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 542);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.dgvVersiones);
            this.Controls.Add(this.lblVersiones);
            this.Controls.Add(this.dgvTurnos);
            this.Controls.Add(this.lblTurnos);
            this.Controls.Add(this.groupBoxFiltros);
            this.Name = "frmHistorialTurnos";
            this.Text = "Historial de cambios de turnos";
            this.Load += new System.EventHandler(this.frmHistorialTurnos_Load);
            this.groupBoxFiltros.ResumeLayout(false);
            this.groupBoxFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVersiones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFiltros;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.TextBox txtPaciente;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblTurnos;
        private System.Windows.Forms.DataGridView dgvTurnos;
        private System.Windows.Forms.Label lblVersiones;
        private System.Windows.Forms.DataGridView dgvVersiones;
        private System.Windows.Forms.Button btnRestaurar;
    }
}
