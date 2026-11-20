namespace Vista
{
    partial class frmLiquidaciones
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
            this.groupBox_Periodo = new System.Windows.Forms.GroupBox();
            this.lblMes = new System.Windows.Forms.Label();
            this.cboMes = new System.Windows.Forms.ComboBox();
            this.lblAnio = new System.Windows.Forms.Label();
            this.cboAnio = new System.Windows.Forms.ComboBox();
            this.btnConsultarTurnos = new System.Windows.Forms.Button();
            this.lblMedicos = new System.Windows.Forms.Label();
            this.dgvMedicos = new System.Windows.Forms.DataGridView();
            this.lblTurnosTrabajados = new System.Windows.Forms.Label();
            this.dgvTurnosTrabajados = new System.Windows.Forms.DataGridView();
            this.lblTotales = new System.Windows.Forms.Label();
            this.btnGenerarLiquidacion = new System.Windows.Forms.Button();
            this.lblLiquidaciones = new System.Windows.Forms.Label();
            this.dgvLiquidaciones = new System.Windows.Forms.DataGridView();
            this.btnEmitirRecibo = new System.Windows.Forms.Button();
            this.groupBox_Periodo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnosTrabajados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiquidaciones)).BeginInit();
            this.SuspendLayout();
            this.groupBox_Periodo.Controls.Add(this.lblMes);
            this.groupBox_Periodo.Controls.Add(this.cboMes);
            this.groupBox_Periodo.Controls.Add(this.lblAnio);
            this.groupBox_Periodo.Controls.Add(this.cboAnio);
            this.groupBox_Periodo.Controls.Add(this.btnConsultarTurnos);
            this.groupBox_Periodo.Location = new System.Drawing.Point(15, 17);
            this.groupBox_Periodo.Name = "groupBox_Periodo";
            this.groupBox_Periodo.Size = new System.Drawing.Size(730, 65);
            this.groupBox_Periodo.TabIndex = 0;
            this.groupBox_Periodo.TabStop = false;
            this.groupBox_Periodo.Text = "Período";
            this.lblMes.AutoSize = true;
            this.lblMes.Location = new System.Drawing.Point(15, 36);
            this.lblMes.Name = "lblMes";
            this.lblMes.TabIndex = 1;
            this.lblMes.Text = "Mes:";
            this.cboMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMes.FormattingEnabled = true;
            this.cboMes.Location = new System.Drawing.Point(60, 31);
            this.cboMes.Name = "cboMes";
            this.cboMes.Size = new System.Drawing.Size(140, 22);
            this.cboMes.TabIndex = 2;
            this.cboMes.SelectedIndexChanged += new System.EventHandler(this.Periodo_Changed);
            this.lblAnio.AutoSize = true;
            this.lblAnio.Location = new System.Drawing.Point(220, 36);
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.TabIndex = 3;
            this.lblAnio.Text = "Año:";
            this.cboAnio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAnio.FormattingEnabled = true;
            this.cboAnio.Location = new System.Drawing.Point(260, 31);
            this.cboAnio.Name = "cboAnio";
            this.cboAnio.Size = new System.Drawing.Size(100, 22);
            this.cboAnio.TabIndex = 4;
            this.cboAnio.SelectedIndexChanged += new System.EventHandler(this.Periodo_Changed);
            this.btnConsultarTurnos.Location = new System.Drawing.Point(385, 29);
            this.btnConsultarTurnos.Name = "btnConsultarTurnos";
            this.btnConsultarTurnos.Size = new System.Drawing.Size(140, 26);
            this.btnConsultarTurnos.TabIndex = 5;
            this.btnConsultarTurnos.Text = "Consultar turnos";
            this.btnConsultarTurnos.UseVisualStyleBackColor = true;
            this.btnConsultarTurnos.Click += new System.EventHandler(this.btnConsultarTurnos_Click);
            this.lblMedicos.AutoSize = true;
            this.lblMedicos.Location = new System.Drawing.Point(20, 97);
            this.lblMedicos.Name = "lblMedicos";
            this.lblMedicos.TabIndex = 6;
            this.lblMedicos.Text = "Médicos del período";
            this.dgvMedicos.AllowUserToAddRows = false;
            this.dgvMedicos.AllowUserToDeleteRows = false;
            this.dgvMedicos.AllowUserToResizeRows = false;
            this.dgvMedicos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMedicos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicos.MultiSelect = false;
            this.dgvMedicos.ReadOnly = true;
            this.dgvMedicos.RowHeadersVisible = false;
            this.dgvMedicos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicos.Location = new System.Drawing.Point(20, 117);
            this.dgvMedicos.Name = "dgvMedicos";
            this.dgvMedicos.Size = new System.Drawing.Size(720, 125);
            this.dgvMedicos.TabIndex = 7;
            this.dgvMedicos.SelectionChanged += new System.EventHandler(this.dgvMedicos_SelectionChanged);
            this.lblTurnosTrabajados.AutoSize = true;
            this.lblTurnosTrabajados.Location = new System.Drawing.Point(20, 257);
            this.lblTurnosTrabajados.Name = "lblTurnosTrabajados";
            this.lblTurnosTrabajados.TabIndex = 8;
            this.lblTurnosTrabajados.Text = "Turnos trabajados del médico seleccionado";
            this.dgvTurnosTrabajados.AllowUserToAddRows = false;
            this.dgvTurnosTrabajados.AllowUserToDeleteRows = false;
            this.dgvTurnosTrabajados.AllowUserToResizeRows = false;
            this.dgvTurnosTrabajados.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTurnosTrabajados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTurnosTrabajados.MultiSelect = false;
            this.dgvTurnosTrabajados.ReadOnly = true;
            this.dgvTurnosTrabajados.RowHeadersVisible = false;
            this.dgvTurnosTrabajados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTurnosTrabajados.Location = new System.Drawing.Point(20, 277);
            this.dgvTurnosTrabajados.Name = "dgvTurnosTrabajados";
            this.dgvTurnosTrabajados.Size = new System.Drawing.Size(720, 100);
            this.dgvTurnosTrabajados.TabIndex = 9;
            this.lblTotales.AutoSize = true;
            this.lblTotales.Location = new System.Drawing.Point(20, 392);
            this.lblTotales.Name = "lblTotales";
            this.lblTotales.Size = new System.Drawing.Size(530, 20);
            this.lblTotales.TabIndex = 10;
            this.lblTotales.Text = "";
            this.lblTotales.AutoSize = false;
            this.lblTotales.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnGenerarLiquidacion.Location = new System.Drawing.Point(560, 387);
            this.btnGenerarLiquidacion.Name = "btnGenerarLiquidacion";
            this.btnGenerarLiquidacion.Size = new System.Drawing.Size(180, 26);
            this.btnGenerarLiquidacion.TabIndex = 11;
            this.btnGenerarLiquidacion.Text = "Generar liquidación del médico";
            this.btnGenerarLiquidacion.UseVisualStyleBackColor = true;
            this.btnGenerarLiquidacion.Click += new System.EventHandler(this.btnGenerarLiquidacion_Click);
            this.lblLiquidaciones.AutoSize = true;
            this.lblLiquidaciones.Location = new System.Drawing.Point(20, 430);
            this.lblLiquidaciones.Name = "lblLiquidaciones";
            this.lblLiquidaciones.TabIndex = 12;
            this.lblLiquidaciones.Text = "Liquidaciones del período";
            this.dgvLiquidaciones.AllowUserToAddRows = false;
            this.dgvLiquidaciones.AllowUserToDeleteRows = false;
            this.dgvLiquidaciones.AllowUserToResizeRows = false;
            this.dgvLiquidaciones.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvLiquidaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLiquidaciones.MultiSelect = false;
            this.dgvLiquidaciones.ReadOnly = true;
            this.dgvLiquidaciones.RowHeadersVisible = false;
            this.dgvLiquidaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLiquidaciones.Location = new System.Drawing.Point(20, 450);
            this.dgvLiquidaciones.Name = "dgvLiquidaciones";
            this.dgvLiquidaciones.Size = new System.Drawing.Size(720, 140);
            this.dgvLiquidaciones.TabIndex = 13;
            this.dgvLiquidaciones.SelectionChanged += new System.EventHandler(this.dgvLiquidaciones_SelectionChanged);
            this.btnEmitirRecibo.Location = new System.Drawing.Point(580, 600);
            this.btnEmitirRecibo.Name = "btnEmitirRecibo";
            this.btnEmitirRecibo.Size = new System.Drawing.Size(160, 26);
            this.btnEmitirRecibo.TabIndex = 14;
            this.btnEmitirRecibo.Text = "Emitir recibo";
            this.btnEmitirRecibo.UseVisualStyleBackColor = true;
            this.btnEmitirRecibo.Click += new System.EventHandler(this.btnEmitirRecibo_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 642);
            this.Controls.Add(this.btnEmitirRecibo);
            this.Controls.Add(this.dgvLiquidaciones);
            this.Controls.Add(this.lblLiquidaciones);
            this.Controls.Add(this.btnGenerarLiquidacion);
            this.Controls.Add(this.lblTotales);
            this.Controls.Add(this.dgvTurnosTrabajados);
            this.Controls.Add(this.lblTurnosTrabajados);
            this.Controls.Add(this.dgvMedicos);
            this.Controls.Add(this.lblMedicos);
            this.Controls.Add(this.groupBox_Periodo);
            this.Name = "frmLiquidaciones";
            this.Text = "Liquidación de honorarios";
            this.Load += new System.EventHandler(this.frmLiquidaciones_Load);
            this.groupBox_Periodo.ResumeLayout(false);
            this.groupBox_Periodo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnosTrabajados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiquidaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Periodo;
        private System.Windows.Forms.Label lblMes;
        private System.Windows.Forms.ComboBox cboMes;
        private System.Windows.Forms.Label lblAnio;
        private System.Windows.Forms.ComboBox cboAnio;
        private System.Windows.Forms.Button btnConsultarTurnos;
        private System.Windows.Forms.Label lblMedicos;
        private System.Windows.Forms.DataGridView dgvMedicos;
        private System.Windows.Forms.Label lblTurnosTrabajados;
        private System.Windows.Forms.DataGridView dgvTurnosTrabajados;
        private System.Windows.Forms.Label lblTotales;
        private System.Windows.Forms.Button btnGenerarLiquidacion;
        private System.Windows.Forms.Label lblLiquidaciones;
        private System.Windows.Forms.DataGridView dgvLiquidaciones;
        private System.Windows.Forms.Button btnEmitirRecibo;
    }
}
