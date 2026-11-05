namespace Vista
{
    partial class frm_AccesoMedico
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
            this.components = new System.ComponentModel.Container();
            this.tlpPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.pnlAgenda = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblMedico = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnHoy = new System.Windows.Forms.Button();
            this.lblModo = new System.Windows.Forms.Label();
            this.dgvAgenda = new System.Windows.Forms.DataGridView();
            this.lblSinTurnos = new System.Windows.Forms.Label();
            this.lblAyudaAusente = new System.Windows.Forms.Label();
            this.btnFinalizarAtencion = new System.Windows.Forms.Button();
            this.groupBoxHC = new System.Windows.Forms.GroupBox();
            this.tlpHC = new System.Windows.Forms.TableLayoutPanel();
            this.tlpBuscarHC = new System.Windows.Forms.TableLayoutPanel();
            this.lblBuscarHC = new System.Windows.Forms.Label();
            this.txtBuscarHC = new System.Windows.Forms.TextBox();
            this.btnBuscarHC = new System.Windows.Forms.Button();
            this.dgvPacientes = new System.Windows.Forms.DataGridView();
            this.lblPacienteHC = new System.Windows.Forms.Label();
            this.flpAtenciones = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNotaHC = new System.Windows.Forms.Label();
            this.timerAgenda = new System.Windows.Forms.Timer(this.components);
            this.tlpPrincipal.SuspendLayout();
            this.pnlAgenda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgenda)).BeginInit();
            this.groupBoxHC.SuspendLayout();
            this.tlpHC.SuspendLayout();
            this.tlpBuscarHC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).BeginInit();
            this.SuspendLayout();
            //
            // tlpPrincipal
            //
            this.tlpPrincipal.ColumnCount = 2;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 380F));
            this.tlpPrincipal.Controls.Add(this.pnlAgenda, 0, 0);
            this.tlpPrincipal.Controls.Add(this.groupBoxHC, 1, 0);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(10);
            this.tlpPrincipal.RowCount = 1;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.Size = new System.Drawing.Size(1600, 860);
            this.tlpPrincipal.TabIndex = 0;
            //
            // pnlAgenda
            //
            this.pnlAgenda.Controls.Add(this.lblTitulo);
            this.pnlAgenda.Controls.Add(this.lblMedico);
            this.pnlAgenda.Controls.Add(this.lblFecha);
            this.pnlAgenda.Controls.Add(this.dtpFecha);
            this.pnlAgenda.Controls.Add(this.btnHoy);
            this.pnlAgenda.Controls.Add(this.lblModo);
            this.pnlAgenda.Controls.Add(this.dgvAgenda);
            this.pnlAgenda.Controls.Add(this.lblSinTurnos);
            this.pnlAgenda.Controls.Add(this.lblAyudaAusente);
            this.pnlAgenda.Controls.Add(this.btnFinalizarAtencion);
            this.pnlAgenda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAgenda.Location = new System.Drawing.Point(13, 13);
            this.pnlAgenda.Name = "pnlAgenda";
            this.pnlAgenda.Size = new System.Drawing.Size(1006, 834);
            this.pnlAgenda.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(6, 6);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(130, 26);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Mi Agenda";
            //
            // lblMedico
            //
            this.lblMedico.AutoSize = true;
            this.lblMedico.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMedico.Location = new System.Drawing.Point(8, 38);
            this.lblMedico.Name = "lblMedico";
            this.lblMedico.Size = new System.Drawing.Size(60, 18);
            this.lblMedico.TabIndex = 1;
            this.lblMedico.Text = "Medico:";
            //
            // lblFecha
            //
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(8, 72);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(53, 17);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha";
            //
            // dtpFecha
            //
            this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.dtpFecha.CustomFormat = "dd/MM/yyyy";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(10, 94);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(180, 24);
            this.dtpFecha.TabIndex = 3;
            this.dtpFecha.CloseUp += new System.EventHandler(this.dtpFecha_CloseUp);
            this.dtpFecha.DropDown += new System.EventHandler(this.dtpFecha_DropDown);
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            //
            // btnHoy
            //
            this.btnHoy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnHoy.Location = new System.Drawing.Point(200, 91);
            this.btnHoy.Name = "btnHoy";
            this.btnHoy.Size = new System.Drawing.Size(100, 30);
            this.btnHoy.TabIndex = 4;
            this.btnHoy.Text = "Hoy";
            this.btnHoy.UseVisualStyleBackColor = true;
            this.btnHoy.Click += new System.EventHandler(this.btnHoy_Click);
            //
            // lblModo
            //
            this.lblModo.AutoSize = true;
            this.lblModo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.lblModo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblModo.Location = new System.Drawing.Point(315, 98);
            this.lblModo.Name = "lblModo";
            this.lblModo.Size = new System.Drawing.Size(0, 17);
            this.lblModo.TabIndex = 5;
            //
            // dgvAgenda
            //
            this.dgvAgenda.AllowUserToAddRows = false;
            this.dgvAgenda.AllowUserToDeleteRows = false;
            this.dgvAgenda.AllowUserToResizeRows = false;
            this.dgvAgenda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAgenda.BackgroundColor = System.Drawing.Color.White;
            this.dgvAgenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAgenda.Location = new System.Drawing.Point(10, 134);
            this.dgvAgenda.MultiSelect = false;
            this.dgvAgenda.Name = "dgvAgenda";
            this.dgvAgenda.ReadOnly = true;
            this.dgvAgenda.RowHeadersVisible = false;
            this.dgvAgenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAgenda.Size = new System.Drawing.Size(990, 600);
            this.dgvAgenda.TabIndex = 6;
            this.dgvAgenda.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAgenda_CellClick);
            this.dgvAgenda.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAgenda_CellDoubleClick);
            this.dgvAgenda.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAgenda_CellMouseMove);
            this.dgvAgenda.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvAgenda_CellPainting);
            //
            // lblSinTurnos
            //
            this.lblSinTurnos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSinTurnos.BackColor = System.Drawing.Color.White;
            this.lblSinTurnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblSinTurnos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSinTurnos.Location = new System.Drawing.Point(11, 200);
            this.lblSinTurnos.Name = "lblSinTurnos";
            this.lblSinTurnos.Size = new System.Drawing.Size(988, 60);
            this.lblSinTurnos.TabIndex = 7;
            this.lblSinTurnos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSinTurnos.Visible = false;
            //
            // lblAyudaAusente
            //
            this.lblAyudaAusente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAyudaAusente.AutoSize = true;
            this.lblAyudaAusente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.lblAyudaAusente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyudaAusente.Location = new System.Drawing.Point(8, 750);
            this.lblAyudaAusente.Name = "lblAyudaAusente";
            this.lblAyudaAusente.Size = new System.Drawing.Size(420, 17);
            this.lblAyudaAusente.TabIndex = 8;
            this.lblAyudaAusente.Text = "«Ausente» se habilita solo cuando la hora de inicio del turno ya pasó.";
            //
            // btnFinalizarAtencion
            //
            this.btnFinalizarAtencion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalizarAtencion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnFinalizarAtencion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizarAtencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnFinalizarAtencion.ForeColor = System.Drawing.Color.White;
            this.btnFinalizarAtencion.Location = new System.Drawing.Point(760, 776);
            this.btnFinalizarAtencion.Name = "btnFinalizarAtencion";
            this.btnFinalizarAtencion.Size = new System.Drawing.Size(240, 46);
            this.btnFinalizarAtencion.TabIndex = 9;
            this.btnFinalizarAtencion.Text = "Finalizar atención";
            this.btnFinalizarAtencion.UseVisualStyleBackColor = false;
            this.btnFinalizarAtencion.Click += new System.EventHandler(this.btnFinalizarAtencion_Click);
            //
            // groupBoxHC
            //
            this.groupBoxHC.Controls.Add(this.tlpHC);
            this.groupBoxHC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxHC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxHC.Location = new System.Drawing.Point(1207, 13);
            this.groupBoxHC.Name = "groupBoxHC";
            this.groupBoxHC.Padding = new System.Windows.Forms.Padding(8);
            this.groupBoxHC.Size = new System.Drawing.Size(380, 834);
            this.groupBoxHC.TabIndex = 1;
            this.groupBoxHC.TabStop = false;
            this.groupBoxHC.Text = "Consultar Historia Clínica";
            //
            // tlpHC (filas: etiqueta, busqueda, pacientes, paciente elegido, atenciones, nota)
            //
            this.tlpHC.ColumnCount = 1;
            this.tlpHC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHC.Controls.Add(this.lblBuscarHC, 0, 0);
            this.tlpHC.Controls.Add(this.tlpBuscarHC, 0, 1);
            this.tlpHC.Controls.Add(this.dgvPacientes, 0, 2);
            this.tlpHC.Controls.Add(this.lblPacienteHC, 0, 3);
            this.tlpHC.Controls.Add(this.flpAtenciones, 0, 4);
            this.tlpHC.Controls.Add(this.lblNotaHC, 0, 5);
            this.tlpHC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHC.Location = new System.Drawing.Point(8, 27);
            this.tlpHC.Name = "tlpHC";
            this.tlpHC.RowCount = 6;
            this.tlpHC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpHC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpHC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpHC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpHC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpHC.Size = new System.Drawing.Size(364, 799);
            this.tlpHC.TabIndex = 0;
            //
            // lblBuscarHC
            //
            this.lblBuscarHC.AutoSize = true;
            this.lblBuscarHC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBuscarHC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblBuscarHC.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblBuscarHC.Name = "lblBuscarHC";
            this.lblBuscarHC.TabIndex = 0;
            this.lblBuscarHC.Text = "Nombre y apellido, N° HC o DNI";
            //
            // tlpBuscarHC (texto que se estira + boton de ancho fijo)
            //
            this.tlpBuscarHC.ColumnCount = 2;
            this.tlpBuscarHC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBuscarHC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpBuscarHC.Controls.Add(this.txtBuscarHC, 0, 0);
            this.tlpBuscarHC.Controls.Add(this.btnBuscarHC, 1, 0);
            this.tlpBuscarHC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBuscarHC.Margin = new System.Windows.Forms.Padding(0);
            this.tlpBuscarHC.Name = "tlpBuscarHC";
            this.tlpBuscarHC.RowCount = 1;
            this.tlpBuscarHC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBuscarHC.TabIndex = 1;
            //
            // txtBuscarHC
            //
            this.txtBuscarHC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscarHC.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txtBuscarHC.MaxLength = 100;
            this.txtBuscarHC.Name = "txtBuscarHC";
            this.txtBuscarHC.TabIndex = 0;
            this.txtBuscarHC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscarHC_KeyDown);
            //
            // btnBuscarHC
            //
            this.btnBuscarHC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnBuscarHC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBuscarHC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarHC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuscarHC.ForeColor = System.Drawing.Color.White;
            this.btnBuscarHC.Margin = new System.Windows.Forms.Padding(6, 4, 3, 4);
            this.btnBuscarHC.Name = "btnBuscarHC";
            this.btnBuscarHC.TabIndex = 1;
            this.btnBuscarHC.Text = "Buscar";
            this.btnBuscarHC.UseVisualStyleBackColor = false;
            this.btnBuscarHC.Click += new System.EventHandler(this.btnBuscarHC_Click);
            //
            // dgvPacientes
            //
            this.dgvPacientes.AllowUserToAddRows = false;
            this.dgvPacientes.AllowUserToDeleteRows = false;
            this.dgvPacientes.AllowUserToResizeRows = false;
            this.dgvPacientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvPacientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPacientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPacientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dgvPacientes.MultiSelect = false;
            this.dgvPacientes.Name = "dgvPacientes";
            this.dgvPacientes.ReadOnly = true;
            this.dgvPacientes.RowHeadersVisible = false;
            this.dgvPacientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPacientes.TabIndex = 2;
            this.dgvPacientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPacientes_CellClick);
            //
            // lblPacienteHC
            //
            this.lblPacienteHC.AutoSize = true;
            this.lblPacienteHC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPacienteHC.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblPacienteHC.Margin = new System.Windows.Forms.Padding(3, 8, 3, 6);
            this.lblPacienteHC.Name = "lblPacienteHC";
            this.lblPacienteHC.TabIndex = 3;
            //
            // flpAtenciones
            //
            this.flpAtenciones.AutoScroll = true;
            this.flpAtenciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.flpAtenciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAtenciones.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpAtenciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.flpAtenciones.Name = "flpAtenciones";
            this.flpAtenciones.Padding = new System.Windows.Forms.Padding(4);
            this.flpAtenciones.TabIndex = 4;
            this.flpAtenciones.WrapContents = false;
            this.flpAtenciones.Resize += new System.EventHandler(this.flpAtenciones_Resize);
            //
            // lblNotaHC
            //
            this.lblNotaHC.AutoSize = true;
            this.lblNotaHC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNotaHC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this.lblNotaHC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNotaHC.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblNotaHC.Name = "lblNotaHC";
            this.lblNotaHC.TabIndex = 5;
            this.lblNotaHC.Text = "Solo lectura. Cada consulta de Historia Clínica queda registrada en la Bitácora.";
            //
            // timerAgenda
            //
            this.timerAgenda.Interval = 60000;
            this.timerAgenda.Tick += new System.EventHandler(this.timerAgenda_Tick);
            //
            // frm_AccesoMedico
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1600, 860);
            this.Controls.Add(this.tlpPrincipal);
            this.Name = "frm_AccesoMedico";
            this.Text = "Mi Agenda";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_AccesoMedico_FormClosed);
            this.Load += new System.EventHandler(this.frm_AccesoMedico_Load);
            this.tlpPrincipal.ResumeLayout(false);
            this.pnlAgenda.ResumeLayout(false);
            this.pnlAgenda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgenda)).EndInit();
            this.tlpBuscarHC.ResumeLayout(false);
            this.tlpBuscarHC.PerformLayout();
            this.tlpHC.ResumeLayout(false);
            this.tlpHC.PerformLayout();
            this.groupBoxHC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Panel pnlAgenda;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblMedico;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnHoy;
        private System.Windows.Forms.Label lblModo;
        private System.Windows.Forms.DataGridView dgvAgenda;
        private System.Windows.Forms.Label lblSinTurnos;
        private System.Windows.Forms.Label lblAyudaAusente;
        private System.Windows.Forms.Button btnFinalizarAtencion;
        private System.Windows.Forms.GroupBox groupBoxHC;
        private System.Windows.Forms.TableLayoutPanel tlpHC;
        private System.Windows.Forms.TableLayoutPanel tlpBuscarHC;
        private System.Windows.Forms.Label lblBuscarHC;
        private System.Windows.Forms.TextBox txtBuscarHC;
        private System.Windows.Forms.Button btnBuscarHC;
        private System.Windows.Forms.DataGridView dgvPacientes;
        private System.Windows.Forms.Label lblPacienteHC;
        private System.Windows.Forms.FlowLayoutPanel flpAtenciones;
        private System.Windows.Forms.Label lblNotaHC;
        private System.Windows.Forms.Timer timerAgenda;
    }
}
