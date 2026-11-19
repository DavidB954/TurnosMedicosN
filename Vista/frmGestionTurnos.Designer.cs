namespace Vista
{
    partial class frmGestionTurnos
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
            this.groupBox_Paciente = new System.Windows.Forms.GroupBox();
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.btnBuscarPaciente = new System.Windows.Forms.Button();
            this.lblHC = new System.Windows.Forms.Label();
            this.txtHC = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnRegistrarPaciente = new System.Windows.Forms.Button();
            this.groupBox_NuevoTurno = new System.Windows.Forms.GroupBox();
            this.lblEspecialidad = new System.Windows.Forms.Label();
            this.ucEspecialidad = new Vista.ucSelectorEspecialidad();
            this.lblMedico = new System.Windows.Forms.Label();
            this.ucMedico = new Vista.ucSelectorMedico();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblHorarios = new System.Windows.Forms.Label();
            this.lstHorarios = new System.Windows.Forms.ListBox();
            this.btnRegistrarTurno = new System.Windows.Forms.Button();
            this.groupBox_TurnosPaciente = new System.Windows.Forms.GroupBox();
            this.dgvTurnosPaciente = new System.Windows.Forms.DataGridView();
            this.btnModificarTurno = new System.Windows.Forms.Button();
            this.btnCancelarTurno = new System.Windows.Forms.Button();
            this.btnVerHistorial = new System.Windows.Forms.Button();
            this.groupBox_Paciente.SuspendLayout();
            this.groupBox_NuevoTurno.SuspendLayout();
            this.groupBox_TurnosPaciente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnosPaciente)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_Paciente
            // 
            this.groupBox_Paciente.Controls.Add(this.lblDNI);
            this.groupBox_Paciente.Controls.Add(this.txtDNI);
            this.groupBox_Paciente.Controls.Add(this.btnBuscarPaciente);
            this.groupBox_Paciente.Controls.Add(this.lblHC);
            this.groupBox_Paciente.Controls.Add(this.txtHC);
            this.groupBox_Paciente.Controls.Add(this.lblNombre);
            this.groupBox_Paciente.Controls.Add(this.txtNombre);
            this.groupBox_Paciente.Controls.Add(this.lblApellido);
            this.groupBox_Paciente.Controls.Add(this.txtApellido);
            this.groupBox_Paciente.Controls.Add(this.lblFechaNacimiento);
            this.groupBox_Paciente.Controls.Add(this.dtpFechaNacimiento);
            this.groupBox_Paciente.Controls.Add(this.lblTelefono);
            this.groupBox_Paciente.Controls.Add(this.txtTelefono);
            this.groupBox_Paciente.Controls.Add(this.lblEmail);
            this.groupBox_Paciente.Controls.Add(this.txtEmail);
            this.groupBox_Paciente.Controls.Add(this.btnRegistrarPaciente);
            this.groupBox_Paciente.Location = new System.Drawing.Point(15, 17);
            this.groupBox_Paciente.Name = "groupBox_Paciente";
            this.groupBox_Paciente.Size = new System.Drawing.Size(730, 150);
            this.groupBox_Paciente.TabIndex = 0;
            this.groupBox_Paciente.TabStop = false;
            this.groupBox_Paciente.Text = "Paciente";
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Location = new System.Drawing.Point(15, 33);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.TabIndex = 1;
            this.lblDNI.Text = "DNI:";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(60, 28);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(120, 22);
            this.txtDNI.TabIndex = 2;
            this.txtDNI.MaxLength = 10;
            this.txtDNI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDNI_KeyDown);
            this.txtDNI.TextChanged += new System.EventHandler(this.txtDNI_TextChanged);
            // 
            // btnBuscarPaciente
            // 
            this.btnBuscarPaciente.Location = new System.Drawing.Point(190, 26);
            this.btnBuscarPaciente.Name = "btnBuscarPaciente";
            this.btnBuscarPaciente.Size = new System.Drawing.Size(80, 26);
            this.btnBuscarPaciente.TabIndex = 3;
            this.btnBuscarPaciente.Text = "Buscar";
            this.btnBuscarPaciente.UseVisualStyleBackColor = true;
            this.btnBuscarPaciente.Click += new System.EventHandler(this.btnBuscarPaciente_Click);
            // 
            // lblHC
            // 
            this.lblHC.AutoSize = true;
            this.lblHC.Location = new System.Drawing.Point(295, 33);
            this.lblHC.Name = "lblHC";
            this.lblHC.TabIndex = 4;
            this.lblHC.Text = "Historia clínica Nro.:";
            // 
            // txtHC
            // 
            this.txtHC.Location = new System.Drawing.Point(440, 28);
            this.txtHC.Name = "txtHC";
            this.txtHC.Size = new System.Drawing.Size(90, 22);
            this.txtHC.TabIndex = 5;
            this.txtHC.ReadOnly = true;
            this.txtHC.TabStop = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(15, 70);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.TabIndex = 6;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(80, 65);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(150, 22);
            this.txtNombre.TabIndex = 7;
            this.txtNombre.MaxLength = 50;
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Location = new System.Drawing.Point(245, 70);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.TabIndex = 8;
            this.lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(310, 65);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(150, 22);
            this.txtApellido.TabIndex = 9;
            this.txtApellido.MaxLength = 50;
            // 
            // lblFechaNacimiento
            // 
            this.lblFechaNacimiento.AutoSize = true;
            this.lblFechaNacimiento.Location = new System.Drawing.Point(475, 70);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.TabIndex = 10;
            this.lblFechaNacimiento.Text = "Fecha de nacimiento:";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(610, 65);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(105, 22);
            this.dtpFechaNacimiento.TabIndex = 11;
            this.dtpFechaNacimiento.ShowCheckBox = true;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(15, 107);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.TabIndex = 12;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(80, 102);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(150, 22);
            this.txtTelefono.TabIndex = 13;
            this.txtTelefono.MaxLength = 30;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(245, 107);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 14;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(310, 102);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(150, 22);
            this.txtEmail.TabIndex = 15;
            this.txtEmail.MaxLength = 100;
            // 
            // btnRegistrarPaciente
            // 
            this.btnRegistrarPaciente.Location = new System.Drawing.Point(575, 100);
            this.btnRegistrarPaciente.Name = "btnRegistrarPaciente";
            this.btnRegistrarPaciente.Size = new System.Drawing.Size(140, 26);
            this.btnRegistrarPaciente.TabIndex = 16;
            this.btnRegistrarPaciente.Text = "Registrar paciente";
            this.btnRegistrarPaciente.UseVisualStyleBackColor = true;
            this.btnRegistrarPaciente.Click += new System.EventHandler(this.btnRegistrarPaciente_Click);
            // 
            // groupBox_NuevoTurno
            // 
            this.groupBox_NuevoTurno.Controls.Add(this.lblEspecialidad);
            this.groupBox_NuevoTurno.Controls.Add(this.ucEspecialidad);
            this.groupBox_NuevoTurno.Controls.Add(this.lblMedico);
            this.groupBox_NuevoTurno.Controls.Add(this.ucMedico);
            this.groupBox_NuevoTurno.Controls.Add(this.lblFecha);
            this.groupBox_NuevoTurno.Controls.Add(this.dtpFecha);
            this.groupBox_NuevoTurno.Controls.Add(this.lblHorarios);
            this.groupBox_NuevoTurno.Controls.Add(this.lstHorarios);
            this.groupBox_NuevoTurno.Controls.Add(this.btnRegistrarTurno);
            this.groupBox_NuevoTurno.Location = new System.Drawing.Point(15, 177);
            this.groupBox_NuevoTurno.Name = "groupBox_NuevoTurno";
            this.groupBox_NuevoTurno.Size = new System.Drawing.Size(730, 170);
            this.groupBox_NuevoTurno.TabIndex = 17;
            this.groupBox_NuevoTurno.TabStop = false;
            this.groupBox_NuevoTurno.Text = "Nuevo turno";
            // 
            // lblEspecialidad
            // 
            this.lblEspecialidad.AutoSize = true;
            this.lblEspecialidad.Location = new System.Drawing.Point(15, 33);
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.TabIndex = 18;
            this.lblEspecialidad.Text = "Especialidad:";
            // 
            // ucEspecialidad
            // 
            this.ucEspecialidad.Location = new System.Drawing.Point(110, 28);
            this.ucEspecialidad.Name = "ucEspecialidad";
            this.ucEspecialidad.Size = new System.Drawing.Size(190, 22);
            this.ucEspecialidad.TabIndex = 19;
            this.ucEspecialidad.Etiqueta = "";
            // 
            // lblMedico
            // 
            this.lblMedico.AutoSize = true;
            this.lblMedico.Location = new System.Drawing.Point(15, 70);
            this.lblMedico.Name = "lblMedico";
            this.lblMedico.TabIndex = 20;
            this.lblMedico.Text = "Médico:";
            // 
            // ucMedico
            // 
            this.ucMedico.Location = new System.Drawing.Point(110, 65);
            this.ucMedico.Name = "ucMedico";
            this.ucMedico.Size = new System.Drawing.Size(190, 22);
            this.ucMedico.TabIndex = 21;
            this.ucMedico.Etiqueta = "";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(15, 107);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.TabIndex = 22;
            this.lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(110, 102);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(190, 22);
            this.dtpFecha.TabIndex = 23;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // lblHorarios
            // 
            this.lblHorarios.AutoSize = true;
            this.lblHorarios.Location = new System.Drawing.Point(335, 33);
            this.lblHorarios.Name = "lblHorarios";
            this.lblHorarios.TabIndex = 24;
            this.lblHorarios.Text = "Horarios disponibles:";
            // 
            // lstHorarios
            // 
            this.lstHorarios.FormattingEnabled = true;
            this.lstHorarios.IntegralHeight = false;
            this.lstHorarios.Location = new System.Drawing.Point(335, 53);
            this.lstHorarios.Name = "lstHorarios";
            this.lstHorarios.Size = new System.Drawing.Size(220, 105);
            this.lstHorarios.TabIndex = 25;
            // 
            // btnRegistrarTurno
            // 
            this.btnRegistrarTurno.Location = new System.Drawing.Point(575, 132);
            this.btnRegistrarTurno.Name = "btnRegistrarTurno";
            this.btnRegistrarTurno.Size = new System.Drawing.Size(140, 26);
            this.btnRegistrarTurno.TabIndex = 26;
            this.btnRegistrarTurno.Text = "Registrar turno";
            this.btnRegistrarTurno.UseVisualStyleBackColor = true;
            this.btnRegistrarTurno.Click += new System.EventHandler(this.btnRegistrarTurno_Click);
            // 
            // groupBox_TurnosPaciente
            // 
            this.groupBox_TurnosPaciente.Controls.Add(this.dgvTurnosPaciente);
            this.groupBox_TurnosPaciente.Controls.Add(this.btnModificarTurno);
            this.groupBox_TurnosPaciente.Controls.Add(this.btnCancelarTurno);
            this.groupBox_TurnosPaciente.Controls.Add(this.btnVerHistorial);
            this.groupBox_TurnosPaciente.Location = new System.Drawing.Point(15, 357);
            this.groupBox_TurnosPaciente.Name = "groupBox_TurnosPaciente";
            this.groupBox_TurnosPaciente.Size = new System.Drawing.Size(730, 170);
            this.groupBox_TurnosPaciente.TabIndex = 27;
            this.groupBox_TurnosPaciente.TabStop = false;
            this.groupBox_TurnosPaciente.Text = "Turnos del paciente";
            // 
            // dgvTurnosPaciente
            // 
            this.dgvTurnosPaciente.AllowUserToAddRows = false;
            this.dgvTurnosPaciente.AllowUserToDeleteRows = false;
            this.dgvTurnosPaciente.AllowUserToResizeRows = false;
            this.dgvTurnosPaciente.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTurnosPaciente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTurnosPaciente.MultiSelect = false;
            this.dgvTurnosPaciente.ReadOnly = true;
            this.dgvTurnosPaciente.RowHeadersVisible = false;
            this.dgvTurnosPaciente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTurnosPaciente.Location = new System.Drawing.Point(15, 30);
            this.dgvTurnosPaciente.Name = "dgvTurnosPaciente";
            this.dgvTurnosPaciente.Size = new System.Drawing.Size(540, 125);
            this.dgvTurnosPaciente.TabIndex = 28;
            this.dgvTurnosPaciente.SelectionChanged += new System.EventHandler(this.dgvTurnosPaciente_SelectionChanged);
            // 
            // btnModificarTurno
            // 
            this.btnModificarTurno.Location = new System.Drawing.Point(575, 30);
            this.btnModificarTurno.Name = "btnModificarTurno";
            this.btnModificarTurno.Size = new System.Drawing.Size(140, 26);
            this.btnModificarTurno.TabIndex = 29;
            this.btnModificarTurno.Text = "Modificar turno";
            this.btnModificarTurno.UseVisualStyleBackColor = true;
            this.btnModificarTurno.Click += new System.EventHandler(this.btnModificarTurno_Click);
            // 
            // btnCancelarTurno
            // 
            this.btnCancelarTurno.Location = new System.Drawing.Point(575, 66);
            this.btnCancelarTurno.Name = "btnCancelarTurno";
            this.btnCancelarTurno.Size = new System.Drawing.Size(140, 26);
            this.btnCancelarTurno.TabIndex = 30;
            this.btnCancelarTurno.Text = "Cancelar turno";
            this.btnCancelarTurno.UseVisualStyleBackColor = true;
            this.btnCancelarTurno.Click += new System.EventHandler(this.btnCancelarTurno_Click);
            // 
            // btnVerHistorial
            // 
            this.btnVerHistorial.Location = new System.Drawing.Point(575, 102);
            this.btnVerHistorial.Name = "btnVerHistorial";
            this.btnVerHistorial.Size = new System.Drawing.Size(140, 26);
            this.btnVerHistorial.TabIndex = 31;
            this.btnVerHistorial.Text = "Ver historial";
            this.btnVerHistorial.UseVisualStyleBackColor = true;
            this.btnVerHistorial.Click += new System.EventHandler(this.btnVerHistorial_Click);
            // 
            // frmGestionTurnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 542);
            this.Controls.Add(this.groupBox_TurnosPaciente);
            this.Controls.Add(this.groupBox_NuevoTurno);
            this.Controls.Add(this.groupBox_Paciente);
            this.Name = "frmGestionTurnos";
            this.Text = "Gestión de Turnos";
            this.Load += new System.EventHandler(this.frmGestionTurnos_Load);
            this.groupBox_Paciente.ResumeLayout(false);
            this.groupBox_Paciente.PerformLayout();
            this.groupBox_NuevoTurno.ResumeLayout(false);
            this.groupBox_NuevoTurno.PerformLayout();
            this.groupBox_TurnosPaciente.ResumeLayout(false);
            this.groupBox_TurnosPaciente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnosPaciente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Paciente;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Button btnBuscarPaciente;
        private System.Windows.Forms.Label lblHC;
        private System.Windows.Forms.TextBox txtHC;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblFechaNacimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnRegistrarPaciente;
        private System.Windows.Forms.GroupBox groupBox_NuevoTurno;
        private System.Windows.Forms.Label lblEspecialidad;
        private Vista.ucSelectorEspecialidad ucEspecialidad;
        private System.Windows.Forms.Label lblMedico;
        private Vista.ucSelectorMedico ucMedico;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblHorarios;
        private System.Windows.Forms.ListBox lstHorarios;
        private System.Windows.Forms.Button btnRegistrarTurno;
        private System.Windows.Forms.GroupBox groupBox_TurnosPaciente;
        private System.Windows.Forms.DataGridView dgvTurnosPaciente;
        private System.Windows.Forms.Button btnModificarTurno;
        private System.Windows.Forms.Button btnCancelarTurno;
        private System.Windows.Forms.Button btnVerHistorial;
    }
}
