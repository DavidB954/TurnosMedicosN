namespace Vista
{
    partial class frmAtencion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlBotones = new System.Windows.Forms.Panel();
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
            this.lblAyudaReceta = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.pnlContenido.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.groupBoxReceta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamentos)).BeginInit();
            this.SuspendLayout();
            //
            // pnlContenido (se desplaza si la ventana no entra en la pantalla)
            //
            this.pnlContenido.AutoScroll = true;
            this.pnlContenido.Controls.Add(this.lblPaciente);
            this.pnlContenido.Controls.Add(this.txtPaciente);
            this.pnlContenido.Controls.Add(this.lblHistoriaClinica);
            this.pnlContenido.Controls.Add(this.txtHistoriaClinica);
            this.pnlContenido.Controls.Add(this.lblDNI);
            this.pnlContenido.Controls.Add(this.txtDNI);
            this.pnlContenido.Controls.Add(this.lblMotivo);
            this.pnlContenido.Controls.Add(this.txtMotivo);
            this.pnlContenido.Controls.Add(this.lblIndicaciones);
            this.pnlContenido.Controls.Add(this.txtIndicaciones);
            this.pnlContenido.Controls.Add(this.lblReceta);
            this.pnlContenido.Controls.Add(this.rbRecetaSi);
            this.pnlContenido.Controls.Add(this.rbRecetaNo);
            this.pnlContenido.Controls.Add(this.groupBoxReceta);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 0);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(900, 600);
            this.pnlContenido.TabIndex = 0;
            //
            // pnlBotones (siempre visible abajo)
            //
            this.pnlBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlBotones.Controls.Add(this.btnRegistrar);
            this.pnlBotones.Controls.Add(this.btnCancelar);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotones.Location = new System.Drawing.Point(0, 600);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(900, 70);
            this.pnlBotones.TabIndex = 1;
            //
            // lblPaciente
            //
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblPaciente.Location = new System.Drawing.Point(18, 14);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(72, 17);
            this.lblPaciente.TabIndex = 0;
            this.lblPaciente.Text = "Paciente";
            //
            // txtPaciente
            //
            this.txtPaciente.Location = new System.Drawing.Point(20, 36);
            this.txtPaciente.Name = "txtPaciente";
            this.txtPaciente.ReadOnly = true;
            this.txtPaciente.Size = new System.Drawing.Size(400, 24);
            this.txtPaciente.TabIndex = 1;
            this.txtPaciente.TabStop = false;
            //
            // lblHistoriaClinica
            //
            this.lblHistoriaClinica.AutoSize = true;
            this.lblHistoriaClinica.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblHistoriaClinica.Location = new System.Drawing.Point(438, 14);
            this.lblHistoriaClinica.Name = "lblHistoriaClinica";
            this.lblHistoriaClinica.Size = new System.Drawing.Size(123, 17);
            this.lblHistoriaClinica.TabIndex = 2;
            this.lblHistoriaClinica.Text = "Historia Clínica";
            //
            // txtHistoriaClinica
            //
            this.txtHistoriaClinica.Location = new System.Drawing.Point(440, 36);
            this.txtHistoriaClinica.Name = "txtHistoriaClinica";
            this.txtHistoriaClinica.ReadOnly = true;
            this.txtHistoriaClinica.Size = new System.Drawing.Size(200, 24);
            this.txtHistoriaClinica.TabIndex = 3;
            this.txtHistoriaClinica.TabStop = false;
            //
            // lblDNI
            //
            this.lblDNI.AutoSize = true;
            this.lblDNI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblDNI.Location = new System.Drawing.Point(658, 14);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(35, 17);
            this.lblDNI.TabIndex = 4;
            this.lblDNI.Text = "DNI";
            //
            // txtDNI
            //
            this.txtDNI.Location = new System.Drawing.Point(660, 36);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.ReadOnly = true;
            this.txtDNI.Size = new System.Drawing.Size(220, 24);
            this.txtDNI.TabIndex = 5;
            this.txtDNI.TabStop = false;
            //
            // lblMotivo
            //
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblMotivo.Location = new System.Drawing.Point(18, 76);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(175, 17);
            this.lblMotivo.TabIndex = 6;
            this.lblMotivo.Text = "Motivo de la consulta *";
            //
            // txtMotivo
            //
            this.txtMotivo.Location = new System.Drawing.Point(20, 98);
            this.txtMotivo.MaxLength = 500;
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMotivo.Size = new System.Drawing.Size(860, 80);
            this.txtMotivo.TabIndex = 7;
            //
            // lblIndicaciones
            //
            this.lblIndicaciones.AutoSize = true;
            this.lblIndicaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblIndicaciones.Location = new System.Drawing.Point(18, 190);
            this.lblIndicaciones.Name = "lblIndicaciones";
            this.lblIndicaciones.Size = new System.Drawing.Size(101, 17);
            this.lblIndicaciones.TabIndex = 8;
            this.lblIndicaciones.Text = "Indicaciones";
            //
            // txtIndicaciones
            //
            this.txtIndicaciones.Location = new System.Drawing.Point(20, 212);
            this.txtIndicaciones.MaxLength = 1000;
            this.txtIndicaciones.Multiline = true;
            this.txtIndicaciones.Name = "txtIndicaciones";
            this.txtIndicaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIndicaciones.Size = new System.Drawing.Size(860, 80);
            this.txtIndicaciones.TabIndex = 9;
            //
            // lblReceta
            //
            this.lblReceta.AutoSize = true;
            this.lblReceta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblReceta.Location = new System.Drawing.Point(18, 310);
            this.lblReceta.Name = "lblReceta";
            this.lblReceta.Size = new System.Drawing.Size(76, 17);
            this.lblReceta.TabIndex = 10;
            this.lblReceta.Text = "¿Receta?";
            //
            // rbRecetaSi
            //
            this.rbRecetaSi.AutoSize = true;
            this.rbRecetaSi.Location = new System.Drawing.Point(115, 308);
            this.rbRecetaSi.Name = "rbRecetaSi";
            this.rbRecetaSi.Size = new System.Drawing.Size(42, 22);
            this.rbRecetaSi.TabIndex = 11;
            this.rbRecetaSi.Text = "SÍ";
            this.rbRecetaSi.UseVisualStyleBackColor = true;
            this.rbRecetaSi.CheckedChanged += new System.EventHandler(this.rbReceta_CheckedChanged);
            //
            // rbRecetaNo
            //
            this.rbRecetaNo.AutoSize = true;
            this.rbRecetaNo.Checked = true;
            this.rbRecetaNo.Location = new System.Drawing.Point(185, 308);
            this.rbRecetaNo.Name = "rbRecetaNo";
            this.rbRecetaNo.Size = new System.Drawing.Size(48, 22);
            this.rbRecetaNo.TabIndex = 12;
            this.rbRecetaNo.TabStop = true;
            this.rbRecetaNo.Text = "NO";
            this.rbRecetaNo.UseVisualStyleBackColor = true;
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
            this.groupBoxReceta.Controls.Add(this.lblAyudaReceta);
            this.groupBoxReceta.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.groupBoxReceta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.groupBoxReceta.Location = new System.Drawing.Point(20, 342);
            this.groupBoxReceta.Name = "groupBoxReceta";
            this.groupBoxReceta.Size = new System.Drawing.Size(860, 245);
            this.groupBoxReceta.TabIndex = 13;
            this.groupBoxReceta.TabStop = false;
            this.groupBoxReceta.Text = "Receta";
            this.groupBoxReceta.Visible = false;
            //
            // lblMedicamento
            //
            this.lblMedicamento.AutoSize = true;
            this.lblMedicamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblMedicamento.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMedicamento.Location = new System.Drawing.Point(13, 28);
            this.lblMedicamento.Name = "lblMedicamento";
            this.lblMedicamento.Size = new System.Drawing.Size(116, 17);
            this.lblMedicamento.TabIndex = 0;
            this.lblMedicamento.Text = "Medicamento *";
            //
            // txtMedicamento
            //
            this.txtMedicamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtMedicamento.Location = new System.Drawing.Point(15, 50);
            this.txtMedicamento.MaxLength = 150;
            this.txtMedicamento.Name = "txtMedicamento";
            this.txtMedicamento.Size = new System.Drawing.Size(215, 23);
            this.txtMedicamento.TabIndex = 1;
            //
            // lblPresentacion
            //
            this.lblPresentacion.AutoSize = true;
            this.lblPresentacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblPresentacion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPresentacion.Location = new System.Drawing.Point(238, 28);
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.Size = new System.Drawing.Size(104, 17);
            this.lblPresentacion.TabIndex = 2;
            this.lblPresentacion.Text = "Presentación (ej.: 500 mg comp.)";
            //
            // txtPresentacion
            //
            this.txtPresentacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPresentacion.Location = new System.Drawing.Point(240, 50);
            this.txtPresentacion.MaxLength = 100;
            this.txtPresentacion.Name = "txtPresentacion";
            this.txtPresentacion.Size = new System.Drawing.Size(255, 23);
            this.txtPresentacion.TabIndex = 3;
            //
            // lblPosologia
            //
            this.lblPosologia.AutoSize = true;
            this.lblPosologia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblPosologia.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPosologia.Location = new System.Drawing.Point(503, 28);
            this.lblPosologia.Name = "lblPosologia";
            this.lblPosologia.Size = new System.Drawing.Size(90, 17);
            this.lblPosologia.TabIndex = 4;
            this.lblPosologia.Text = "Posología *";
            //
            // txtPosologia
            //
            this.txtPosologia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPosologia.Location = new System.Drawing.Point(505, 50);
            this.txtPosologia.MaxLength = 200;
            this.txtPosologia.Name = "txtPosologia";
            this.txtPosologia.Size = new System.Drawing.Size(170, 23);
            this.txtPosologia.TabIndex = 5;
            //
            // lblCantidad
            //
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCantidad.Location = new System.Drawing.Point(683, 28);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(44, 17);
            this.lblCantidad.TabIndex = 6;
            this.lblCantidad.Text = "Cant.";
            //
            // nudCantidad
            //
            this.nudCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nudCantidad.Location = new System.Drawing.Point(685, 50);
            this.nudCantidad.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(60, 23);
            this.nudCantidad.TabIndex = 7;
            this.nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnAgregar
            //
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAgregar.Location = new System.Drawing.Point(755, 46);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(90, 30);
            this.btnAgregar.TabIndex = 8;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            //
            // dgvMedicamentos
            //
            this.dgvMedicamentos.AllowUserToAddRows = false;
            this.dgvMedicamentos.AllowUserToDeleteRows = false;
            this.dgvMedicamentos.AllowUserToResizeRows = false;
            this.dgvMedicamentos.BackgroundColor = System.Drawing.Color.White;
            this.dgvMedicamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicamentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dgvMedicamentos.Location = new System.Drawing.Point(15, 88);
            this.dgvMedicamentos.MultiSelect = false;
            this.dgvMedicamentos.Name = "dgvMedicamentos";
            this.dgvMedicamentos.ReadOnly = true;
            this.dgvMedicamentos.RowHeadersVisible = false;
            this.dgvMedicamentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicamentos.Size = new System.Drawing.Size(830, 120);
            this.dgvMedicamentos.TabIndex = 9;
            this.dgvMedicamentos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMedicamentos_CellClick);
            //
            // lblAyudaReceta
            //
            this.lblAyudaReceta.AutoSize = true;
            this.lblAyudaReceta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this.lblAyudaReceta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyudaReceta.Location = new System.Drawing.Point(13, 216);
            this.lblAyudaReceta.Name = "lblAyudaReceta";
            this.lblAyudaReceta.Size = new System.Drawing.Size(250, 15);
            this.lblAyudaReceta.TabIndex = 10;
            this.lblAyudaReceta.Text = "Al menos 1 medicamento si Receta = SÍ.";
            //
            // btnCancelar
            //
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(520, 12);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(170, 46);
            this.btnCancelar.TabIndex = 16;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // btnRegistrar
            //
            this.btnRegistrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnRegistrar.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistrar.Location = new System.Drawing.Point(710, 12);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(170, 46);
            this.btnRegistrar.TabIndex = 15;
            this.btnRegistrar.Text = "Registrar Atención";
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            //
            // frmAtencion
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(900, 670);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlBotones);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAtencion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Registrar Atención";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAtencion_FormClosing);
            this.Load += new System.EventHandler(this.frmAtencion_Load);
            this.pnlContenido.ResumeLayout(false);
            this.pnlContenido.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.groupBoxReceta.ResumeLayout(false);
            this.groupBoxReceta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamentos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlBotones;
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
        private System.Windows.Forms.Label lblAyudaReceta;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRegistrar;
    }
}
