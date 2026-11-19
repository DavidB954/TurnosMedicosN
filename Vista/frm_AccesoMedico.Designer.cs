namespace Vista
{
    partial class frm_AccesoMedico
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
            this.components = new System.ComponentModel.Container();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnHoy = new System.Windows.Forms.Button();
            this.dgvAgenda = new System.Windows.Forms.DataGridView();
            this.lblSinTurnos = new System.Windows.Forms.Label();
            this.btnFinalizarAtencion = new System.Windows.Forms.Button();
            this.groupBoxHC = new System.Windows.Forms.GroupBox();
            this.lblBuscarHC = new System.Windows.Forms.Label();
            this.txtBuscarHC = new System.Windows.Forms.TextBox();
            this.btnBuscarHC = new System.Windows.Forms.Button();
            this.lblPacienteHC = new System.Windows.Forms.Label();
            this.dgvPacientes = new System.Windows.Forms.DataGridView();
            this.flpAtenciones = new System.Windows.Forms.FlowLayoutPanel();
            this.timerAgenda = new System.Windows.Forms.Timer(this.components);
            this.groupBoxHC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgenda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 25);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Agenda del día";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(410, 25);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(460, 20);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(140, 22);
            this.dtpFecha.TabIndex = 2;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            this.dtpFecha.DropDown += new System.EventHandler(this.dtpFecha_DropDown);
            this.dtpFecha.CloseUp += new System.EventHandler(this.dtpFecha_CloseUp);
            // 
            // btnHoy
            // 
            this.btnHoy.Location = new System.Drawing.Point(615, 18);
            this.btnHoy.Name = "btnHoy";
            this.btnHoy.Size = new System.Drawing.Size(110, 26);
            this.btnHoy.TabIndex = 3;
            this.btnHoy.Text = "Hoy";
            this.btnHoy.UseVisualStyleBackColor = true;
            this.btnHoy.Click += new System.EventHandler(this.btnHoy_Click);
            // 
            // dgvAgenda
            // 
            this.dgvAgenda.AllowUserToAddRows = false;
            this.dgvAgenda.AllowUserToDeleteRows = false;
            this.dgvAgenda.AllowUserToResizeRows = false;
            this.dgvAgenda.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvAgenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAgenda.MultiSelect = false;
            this.dgvAgenda.ReadOnly = true;
            this.dgvAgenda.RowHeadersVisible = false;
            this.dgvAgenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAgenda.Location = new System.Drawing.Point(20, 57);
            this.dgvAgenda.Name = "dgvAgenda";
            this.dgvAgenda.Size = new System.Drawing.Size(720, 200);
            this.dgvAgenda.TabIndex = 4;
            this.dgvAgenda.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAgenda_CellClick);
            this.dgvAgenda.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAgenda_CellDoubleClick);
            this.dgvAgenda.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAgenda_CellMouseMove);
            this.dgvAgenda.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvAgenda_CellPainting);
            // 
            // lblSinTurnos
            // 
            this.lblSinTurnos.AutoSize = true;
            this.lblSinTurnos.Location = new System.Drawing.Point(21, 103);
            this.lblSinTurnos.Name = "lblSinTurnos";
            this.lblSinTurnos.Size = new System.Drawing.Size(718, 40);
            this.lblSinTurnos.TabIndex = 5;
            this.lblSinTurnos.Text = "";
            this.lblSinTurnos.AutoSize = false;
            this.lblSinTurnos.BackColor = System.Drawing.Color.White;
            this.lblSinTurnos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSinTurnos.Visible = false;
            // 
            // btnFinalizarAtencion
            // 
            this.btnFinalizarAtencion.Location = new System.Drawing.Point(555, 267);
            this.btnFinalizarAtencion.Name = "btnFinalizarAtencion";
            this.btnFinalizarAtencion.Size = new System.Drawing.Size(185, 26);
            this.btnFinalizarAtencion.TabIndex = 6;
            this.btnFinalizarAtencion.Text = "Finalizar atención del día";
            this.btnFinalizarAtencion.UseVisualStyleBackColor = true;
            this.btnFinalizarAtencion.Click += new System.EventHandler(this.btnFinalizarAtencion_Click);
            // 
            // groupBoxHC
            // 
            this.groupBoxHC.Controls.Add(this.lblBuscarHC);
            this.groupBoxHC.Controls.Add(this.txtBuscarHC);
            this.groupBoxHC.Controls.Add(this.btnBuscarHC);
            this.groupBoxHC.Controls.Add(this.lblPacienteHC);
            this.groupBoxHC.Controls.Add(this.dgvPacientes);
            this.groupBoxHC.Controls.Add(this.flpAtenciones);
            this.groupBoxHC.Location = new System.Drawing.Point(15, 307);
            this.groupBoxHC.Name = "groupBoxHC";
            this.groupBoxHC.Size = new System.Drawing.Size(730, 245);
            this.groupBoxHC.TabIndex = 7;
            this.groupBoxHC.TabStop = false;
            this.groupBoxHC.Text = "Historia clínica";
            // 
            // lblBuscarHC
            // 
            this.lblBuscarHC.AutoSize = true;
            this.lblBuscarHC.Location = new System.Drawing.Point(15, 38);
            this.lblBuscarHC.Name = "lblBuscarHC";
            this.lblBuscarHC.TabIndex = 8;
            this.lblBuscarHC.Text = "Buscar paciente (DNI, nombre o H.C.):";
            // 
            // txtBuscarHC
            // 
            this.txtBuscarHC.Location = new System.Drawing.Point(250, 33);
            this.txtBuscarHC.Name = "txtBuscarHC";
            this.txtBuscarHC.Size = new System.Drawing.Size(200, 22);
            this.txtBuscarHC.TabIndex = 9;
            this.txtBuscarHC.MaxLength = 100;
            this.txtBuscarHC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscarHC_KeyDown);
            // 
            // btnBuscarHC
            // 
            this.btnBuscarHC.Location = new System.Drawing.Point(460, 31);
            this.btnBuscarHC.Name = "btnBuscarHC";
            this.btnBuscarHC.Size = new System.Drawing.Size(100, 26);
            this.btnBuscarHC.TabIndex = 10;
            this.btnBuscarHC.Text = "Buscar";
            this.btnBuscarHC.UseVisualStyleBackColor = true;
            this.btnBuscarHC.Click += new System.EventHandler(this.btnBuscarHC_Click);
            // 
            // lblPacienteHC
            // 
            this.lblPacienteHC.AutoSize = true;
            this.lblPacienteHC.Location = new System.Drawing.Point(570, 32);
            this.lblPacienteHC.Name = "lblPacienteHC";
            this.lblPacienteHC.Size = new System.Drawing.Size(140, 34);
            this.lblPacienteHC.TabIndex = 11;
            this.lblPacienteHC.Text = "";
            this.lblPacienteHC.AutoSize = false;
            this.lblPacienteHC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvPacientes
            // 
            this.dgvPacientes.AllowUserToAddRows = false;
            this.dgvPacientes.AllowUserToDeleteRows = false;
            this.dgvPacientes.AllowUserToResizeRows = false;
            this.dgvPacientes.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPacientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPacientes.MultiSelect = false;
            this.dgvPacientes.ReadOnly = true;
            this.dgvPacientes.RowHeadersVisible = false;
            this.dgvPacientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPacientes.Location = new System.Drawing.Point(15, 70);
            this.dgvPacientes.Name = "dgvPacientes";
            this.dgvPacientes.Size = new System.Drawing.Size(260, 160);
            this.dgvPacientes.TabIndex = 12;
            this.dgvPacientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPacientes_CellClick);
            // 
            // flpAtenciones
            // 
            this.flpAtenciones.Location = new System.Drawing.Point(290, 70);
            this.flpAtenciones.Name = "flpAtenciones";
            this.flpAtenciones.Size = new System.Drawing.Size(425, 160);
            this.flpAtenciones.TabIndex = 13;
            this.flpAtenciones.AutoScroll = true;
            this.flpAtenciones.BackColor = System.Drawing.SystemColors.Window;
            this.flpAtenciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpAtenciones.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpAtenciones.WrapContents = false;
            this.flpAtenciones.Resize += new System.EventHandler(this.flpAtenciones_Resize);
            // 
            // timerAgenda
            // 
            this.timerAgenda.Interval = 60000;
            this.timerAgenda.Tick += new System.EventHandler(this.timerAgenda_Tick);
            // 
            // frm_AccesoMedico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 562);
            this.Controls.Add(this.groupBoxHC);
            this.Controls.Add(this.btnFinalizarAtencion);
            this.Controls.Add(this.lblSinTurnos);
            this.Controls.Add(this.dgvAgenda);
            this.Controls.Add(this.btnHoy);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblTitulo);
            this.Name = "frm_AccesoMedico";
            this.Text = "Mi agenda";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_AccesoMedico_FormClosed);
            this.Load += new System.EventHandler(this.frm_AccesoMedico_Load);
            this.groupBoxHC.ResumeLayout(false);
            this.groupBoxHC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgenda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnHoy;
        private System.Windows.Forms.DataGridView dgvAgenda;
        private System.Windows.Forms.Label lblSinTurnos;
        private System.Windows.Forms.Button btnFinalizarAtencion;
        private System.Windows.Forms.GroupBox groupBoxHC;
        private System.Windows.Forms.Label lblBuscarHC;
        private System.Windows.Forms.TextBox txtBuscarHC;
        private System.Windows.Forms.Button btnBuscarHC;
        private System.Windows.Forms.Label lblPacienteHC;
        private System.Windows.Forms.DataGridView dgvPacientes;
        private System.Windows.Forms.FlowLayoutPanel flpAtenciones;
        private System.Windows.Forms.Timer timerAgenda;
    }
}
