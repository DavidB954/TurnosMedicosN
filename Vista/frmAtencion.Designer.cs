namespace Vista
{
    partial class frmAtencion
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
            this.groupBoxPaciente = new System.Windows.Forms.GroupBox();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.txtPaciente = new System.Windows.Forms.TextBox();
            this.lblHistoriaClinica = new System.Windows.Forms.Label();
            this.txtHistoriaClinica = new System.Windows.Forms.TextBox();
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.lblIndicaciones = new System.Windows.Forms.Label();
            this.txtIndicaciones = new System.Windows.Forms.TextBox();
            this.lblReceta = new System.Windows.Forms.Label();
            this.rbRecetaSi = new System.Windows.Forms.RadioButton();
            this.rbRecetaNo = new System.Windows.Forms.RadioButton();
            this.groupBoxReceta = new System.Windows.Forms.GroupBox();
            this.lblMedicamento = new System.Windows.Forms.Label();
            this.txtMedicamento = new System.Windows.Forms.TextBox();
            this.lblPresentacion = new System.Windows.Forms.Label();
            this.txtPresentacion = new System.Windows.Forms.TextBox();
            this.lblPosologia = new System.Windows.Forms.Label();
            this.txtPosologia = new System.Windows.Forms.TextBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dgvMedicamentos = new System.Windows.Forms.DataGridView();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.groupBoxPaciente.SuspendLayout();
            this.groupBoxReceta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamentos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxPaciente
            // 
            this.groupBoxPaciente.Controls.Add(this.lblPaciente);
            this.groupBoxPaciente.Controls.Add(this.txtPaciente);
            this.groupBoxPaciente.Controls.Add(this.lblHistoriaClinica);
            this.groupBoxPaciente.Controls.Add(this.txtHistoriaClinica);
            this.groupBoxPaciente.Controls.Add(this.lblDNI);
            this.groupBoxPaciente.Controls.Add(this.txtDNI);
            this.groupBoxPaciente.Location = new System.Drawing.Point(15, 17);
            this.groupBoxPaciente.Name = "groupBoxPaciente";
            this.groupBoxPaciente.Size = new System.Drawing.Size(730, 65);
            this.groupBoxPaciente.TabIndex = 0;
            this.groupBoxPaciente.TabStop = false;
            this.groupBoxPaciente.Text = "Paciente";
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Location = new System.Drawing.Point(15, 36);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.TabIndex = 1;
            this.lblPaciente.Text = "Paciente:";
            // 
            // txtPaciente
            // 
            this.txtPaciente.Location = new System.Drawing.Point(80, 31);
            this.txtPaciente.Name = "txtPaciente";
            this.txtPaciente.Size = new System.Drawing.Size(230, 22);
            this.txtPaciente.TabIndex = 2;
            this.txtPaciente.ReadOnly = true;
            this.txtPaciente.TabStop = false;
            // 
            // lblHistoriaClinica
            // 
            this.lblHistoriaClinica.AutoSize = true;
            this.lblHistoriaClinica.Location = new System.Drawing.Point(330, 36);
            this.lblHistoriaClinica.Name = "lblHistoriaClinica";
            this.lblHistoriaClinica.TabIndex = 3;
            this.lblHistoriaClinica.Text = "H.C.:";
            // 
            // txtHistoriaClinica
            // 
            this.txtHistoriaClinica.Location = new System.Drawing.Point(375, 31);
            this.txtHistoriaClinica.Name = "txtHistoriaClinica";
            this.txtHistoriaClinica.Size = new System.Drawing.Size(90, 22);
            this.txtHistoriaClinica.TabIndex = 4;
            this.txtHistoriaClinica.ReadOnly = true;
            this.txtHistoriaClinica.TabStop = false;
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Location = new System.Drawing.Point(485, 36);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.TabIndex = 5;
            this.lblDNI.Text = "DNI:";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(525, 31);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(120, 22);
            this.txtDNI.TabIndex = 6;
            this.txtDNI.ReadOnly = true;
            this.txtDNI.TabStop = false;
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Location = new System.Drawing.Point(20, 97);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.TabIndex = 7;
            this.lblMotivo.Text = "Motivo de consulta:";
            // 
            // txtMotivo
            // 
            this.txtMotivo.Location = new System.Drawing.Point(20, 117);
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(360, 70);
            this.txtMotivo.TabIndex = 8;
            this.txtMotivo.MaxLength = 500;
            this.txtMotivo.Multiline = true;
            this.txtMotivo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // lblIndicaciones
            // 
            this.lblIndicaciones.AutoSize = true;
            this.lblIndicaciones.Location = new System.Drawing.Point(395, 97);
            this.lblIndicaciones.Name = "lblIndicaciones";
            this.lblIndicaciones.TabIndex = 9;
            this.lblIndicaciones.Text = "Indicaciones:";
            // 
            // txtIndicaciones
            // 
            this.txtIndicaciones.Location = new System.Drawing.Point(395, 117);
            this.txtIndicaciones.Name = "txtIndicaciones";
            this.txtIndicaciones.Size = new System.Drawing.Size(345, 70);
            this.txtIndicaciones.TabIndex = 10;
            this.txtIndicaciones.MaxLength = 1000;
            this.txtIndicaciones.Multiline = true;
            this.txtIndicaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // lblReceta
            // 
            this.lblReceta.AutoSize = true;
            this.lblReceta.Location = new System.Drawing.Point(20, 205);
            this.lblReceta.Name = "lblReceta";
            this.lblReceta.TabIndex = 11;
            this.lblReceta.Text = "¿Emite receta?";
            // 
            // rbRecetaSi
            // 
            this.rbRecetaSi.Location = new System.Drawing.Point(125, 202);
            this.rbRecetaSi.Name = "rbRecetaSi";
            this.rbRecetaSi.Size = new System.Drawing.Size(50, 20);
            this.rbRecetaSi.TabIndex = 12;
            this.rbRecetaSi.Text = "Sí";
            this.rbRecetaSi.UseVisualStyleBackColor = true;
            this.rbRecetaSi.CheckedChanged += new System.EventHandler(this.rbReceta_CheckedChanged);
            // 
            // rbRecetaNo
            // 
            this.rbRecetaNo.Location = new System.Drawing.Point(180, 202);
            this.rbRecetaNo.Name = "rbRecetaNo";
            this.rbRecetaNo.Size = new System.Drawing.Size(50, 20);
            this.rbRecetaNo.TabIndex = 13;
            this.rbRecetaNo.Text = "No";
            this.rbRecetaNo.UseVisualStyleBackColor = true;
            this.rbRecetaNo.Checked = true;
            this.rbRecetaNo.TabStop = true;
            this.rbRecetaNo.CheckedChanged += new System.EventHandler(this.rbReceta_CheckedChanged);
            // 
            // groupBoxReceta
            // 
            this.groupBoxReceta.Controls.Add(this.lblMedicamento);
            this.groupBoxReceta.Controls.Add(this.txtMedicamento);
            this.groupBoxReceta.Controls.Add(this.lblPresentacion);
            this.groupBoxReceta.Controls.Add(this.txtPresentacion);
            this.groupBoxReceta.Controls.Add(this.lblPosologia);
            this.groupBoxReceta.Controls.Add(this.txtPosologia);
            this.groupBoxReceta.Controls.Add(this.lblCantidad);
            this.groupBoxReceta.Controls.Add(this.nudCantidad);
            this.groupBoxReceta.Controls.Add(this.btnAgregar);
            this.groupBoxReceta.Controls.Add(this.dgvMedicamentos);
            this.groupBoxReceta.Location = new System.Drawing.Point(15, 232);
            this.groupBoxReceta.Name = "groupBoxReceta";
            this.groupBoxReceta.Size = new System.Drawing.Size(730, 255);
            this.groupBoxReceta.TabIndex = 14;
            this.groupBoxReceta.TabStop = false;
            this.groupBoxReceta.Text = "Receta";
            this.groupBoxReceta.Enabled = false;
            // 
            // lblMedicamento
            // 
            this.lblMedicamento.AutoSize = true;
            this.lblMedicamento.Location = new System.Drawing.Point(15, 36);
            this.lblMedicamento.Name = "lblMedicamento";
            this.lblMedicamento.TabIndex = 15;
            this.lblMedicamento.Text = "Medicamento:";
            // 
            // txtMedicamento
            // 
            this.txtMedicamento.Location = new System.Drawing.Point(110, 31);
            this.txtMedicamento.Name = "txtMedicamento";
            this.txtMedicamento.Size = new System.Drawing.Size(200, 22);
            this.txtMedicamento.TabIndex = 16;
            this.txtMedicamento.MaxLength = 150;
            // 
            // lblPresentacion
            // 
            this.lblPresentacion.AutoSize = true;
            this.lblPresentacion.Location = new System.Drawing.Point(330, 36);
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.TabIndex = 17;
            this.lblPresentacion.Text = "Presentación:";
            // 
            // txtPresentacion
            // 
            this.txtPresentacion.Location = new System.Drawing.Point(425, 31);
            this.txtPresentacion.Name = "txtPresentacion";
            this.txtPresentacion.Size = new System.Drawing.Size(160, 22);
            this.txtPresentacion.TabIndex = 18;
            this.txtPresentacion.MaxLength = 100;
            // 
            // lblPosologia
            // 
            this.lblPosologia.AutoSize = true;
            this.lblPosologia.Location = new System.Drawing.Point(15, 71);
            this.lblPosologia.Name = "lblPosologia";
            this.lblPosologia.TabIndex = 19;
            this.lblPosologia.Text = "Posología:";
            // 
            // txtPosologia
            // 
            this.txtPosologia.Location = new System.Drawing.Point(110, 66);
            this.txtPosologia.Name = "txtPosologia";
            this.txtPosologia.Size = new System.Drawing.Size(300, 22);
            this.txtPosologia.TabIndex = 20;
            this.txtPosologia.MaxLength = 200;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(430, 71);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.TabIndex = 21;
            this.lblCantidad.Text = "Cantidad:";
            // 
            // nudCantidad
            // 
            this.nudCantidad.Location = new System.Drawing.Point(500, 66);
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(85, 22);
            this.nudCantidad.TabIndex = 22;
            this.nudCantidad.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(605, 64);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(110, 26);
            this.btnAgregar.TabIndex = 23;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // dgvMedicamentos
            // 
            this.dgvMedicamentos.AllowUserToAddRows = false;
            this.dgvMedicamentos.AllowUserToDeleteRows = false;
            this.dgvMedicamentos.AllowUserToResizeRows = false;
            this.dgvMedicamentos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMedicamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicamentos.MultiSelect = false;
            this.dgvMedicamentos.ReadOnly = true;
            this.dgvMedicamentos.RowHeadersVisible = false;
            this.dgvMedicamentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicamentos.Location = new System.Drawing.Point(15, 102);
            this.dgvMedicamentos.Name = "dgvMedicamentos";
            this.dgvMedicamentos.Size = new System.Drawing.Size(700, 140);
            this.dgvMedicamentos.TabIndex = 24;
            this.dgvMedicamentos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMedicamentos_CellClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(500, 502);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 26);
            this.btnCancelar.TabIndex = 25;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(620, 502);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(130, 26);
            this.btnRegistrar.TabIndex = 26;
            this.btnRegistrar.Text = "Registrar atención";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // frmAtencion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 542);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.groupBoxReceta);
            this.Controls.Add(this.rbRecetaNo);
            this.Controls.Add(this.rbRecetaSi);
            this.Controls.Add(this.lblReceta);
            this.Controls.Add(this.txtIndicaciones);
            this.Controls.Add(this.lblIndicaciones);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.lblMotivo);
            this.Controls.Add(this.groupBoxPaciente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Name = "frmAtencion";
            this.Text = "Registrar atención";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAtencion_FormClosing);
            this.Load += new System.EventHandler(this.frmAtencion_Load);
            this.groupBoxPaciente.ResumeLayout(false);
            this.groupBoxPaciente.PerformLayout();
            this.groupBoxReceta.ResumeLayout(false);
            this.groupBoxReceta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamentos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPaciente;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.TextBox txtPaciente;
        private System.Windows.Forms.Label lblHistoriaClinica;
        private System.Windows.Forms.TextBox txtHistoriaClinica;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label lblIndicaciones;
        private System.Windows.Forms.TextBox txtIndicaciones;
        private System.Windows.Forms.Label lblReceta;
        private System.Windows.Forms.RadioButton rbRecetaSi;
        private System.Windows.Forms.RadioButton rbRecetaNo;
        private System.Windows.Forms.GroupBox groupBoxReceta;
        private System.Windows.Forms.Label lblMedicamento;
        private System.Windows.Forms.TextBox txtMedicamento;
        private System.Windows.Forms.Label lblPresentacion;
        private System.Windows.Forms.TextBox txtPresentacion;
        private System.Windows.Forms.Label lblPosologia;
        private System.Windows.Forms.TextBox txtPosologia;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView dgvMedicamentos;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRegistrar;
    }
}
