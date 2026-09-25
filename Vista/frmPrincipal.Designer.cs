namespace Vista
{
    partial class frmPrincipal
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
            this.groupBox_Menu = new System.Windows.Forms.GroupBox();
            this.CbPrincipalIdioma = new System.Windows.Forms.ComboBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnGestionIdiomas = new System.Windows.Forms.Button();
            this.btn_AccesoPaciente = new System.Windows.Forms.Button();
            this.btn_AccesoMedico = new System.Windows.Forms.Button();
            this.btn_Salir = new System.Windows.Forms.Button();
            this.btn_Bitacora = new System.Windows.Forms.Button();
            this.btn_BitacoraTurno = new System.Windows.Forms.Button();
            this.btn_Seguridad = new System.Windows.Forms.Button();
            this.btn_GestionRoles = new System.Windows.Forms.Button();
            this.btn_GestionUsuarios = new System.Windows.Forms.Button();
            this.btn_GestionMedicos = new System.Windows.Forms.Button();
            this.btn_GestionTurnos = new System.Windows.Forms.Button();
            this.timerVencimientoTurnos = new System.Windows.Forms.Timer(this.components);
            this.groupBox_Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Menu
            // 
            this.groupBox_Menu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox_Menu.Controls.Add(this.btn_GestionTurnos);
            this.groupBox_Menu.Controls.Add(this.btn_GestionMedicos);
            this.groupBox_Menu.Controls.Add(this.CbPrincipalIdioma);
            this.groupBox_Menu.Controls.Add(this.lblUsuario);
            this.groupBox_Menu.Controls.Add(this.btnGestionIdiomas);
            this.groupBox_Menu.Controls.Add(this.btn_AccesoPaciente);
            this.groupBox_Menu.Controls.Add(this.btn_AccesoMedico);
            this.groupBox_Menu.Controls.Add(this.btn_Salir);
            this.groupBox_Menu.Controls.Add(this.btn_Bitacora);
            this.groupBox_Menu.Controls.Add(this.btn_BitacoraTurno);
            this.groupBox_Menu.Controls.Add(this.btn_Seguridad);
            this.groupBox_Menu.Controls.Add(this.btn_GestionRoles);
            this.groupBox_Menu.Controls.Add(this.btn_GestionUsuarios);
            this.groupBox_Menu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Menu.Location = new System.Drawing.Point(0, 3);
            this.groupBox_Menu.Name = "groupBox_Menu";
            this.groupBox_Menu.Size = new System.Drawing.Size(300, 828);
            this.groupBox_Menu.TabIndex = 1;
            this.groupBox_Menu.TabStop = false;
            this.groupBox_Menu.Text = "Menu";
            // 
            // CbPrincipalIdioma
            // 
            this.CbPrincipalIdioma.FormattingEnabled = true;
            this.CbPrincipalIdioma.Location = new System.Drawing.Point(120, 640);
            this.CbPrincipalIdioma.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CbPrincipalIdioma.Name = "CbPrincipalIdioma";
            this.CbPrincipalIdioma.Size = new System.Drawing.Size(180, 37);
            this.CbPrincipalIdioma.TabIndex = 9;
            this.CbPrincipalIdioma.SelectedIndexChanged += new System.EventHandler(this.CbPrincipalIdioma_SelectedIndexChanged);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(48, 692);
            this.lblUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(0, 29);
            this.lblUsuario.TabIndex = 8;
            // 
            // btnGestionIdiomas
            // 
            this.btnGestionIdiomas.Location = new System.Drawing.Point(0, 342);
            this.btnGestionIdiomas.Name = "btnGestionIdiomas";
            this.btnGestionIdiomas.Size = new System.Drawing.Size(300, 52);
            this.btnGestionIdiomas.TabIndex = 7;
            this.btnGestionIdiomas.Text = "Gestion Idiomas";
            this.btnGestionIdiomas.UseVisualStyleBackColor = true;
            this.btnGestionIdiomas.Click += new System.EventHandler(this.btnGestionIdiomas_Click);
            // 
            // btn_AccesoPaciente
            // 
            this.btn_AccesoPaciente.Location = new System.Drawing.Point(0, 489);
            this.btn_AccesoPaciente.Name = "btn_AccesoPaciente";
            this.btn_AccesoPaciente.Size = new System.Drawing.Size(300, 52);
            this.btn_AccesoPaciente.TabIndex = 6;
            this.btn_AccesoPaciente.Text = "Acceso Paciente";
            this.btn_AccesoPaciente.UseVisualStyleBackColor = true;
            this.btn_AccesoPaciente.Click += new System.EventHandler(this.btn_AccesoPaciente_Click);
            // 
            // btn_AccesoMedico
            // 
            this.btn_AccesoMedico.Location = new System.Drawing.Point(0, 414);
            this.btn_AccesoMedico.Name = "btn_AccesoMedico";
            this.btn_AccesoMedico.Size = new System.Drawing.Size(300, 52);
            this.btn_AccesoMedico.TabIndex = 5;
            this.btn_AccesoMedico.Text = "Acceso Medico";
            this.btn_AccesoMedico.UseVisualStyleBackColor = true;
            this.btn_AccesoMedico.Click += new System.EventHandler(this.btn_AccesoMedico_Click);
            // 
            // btn_Salir
            // 
            this.btn_Salir.Location = new System.Drawing.Point(178, 777);
            this.btn_Salir.Name = "btn_Salir";
            this.btn_Salir.Size = new System.Drawing.Size(122, 51);
            this.btn_Salir.TabIndex = 4;
            this.btn_Salir.Text = "Salir";
            this.btn_Salir.UseVisualStyleBackColor = true;
            this.btn_Salir.Click += new System.EventHandler(this.btn_Salir_Click);
            // 
            // btn_Bitacora
            // 
            this.btn_Bitacora.Location = new System.Drawing.Point(0, 266);
            this.btn_Bitacora.Name = "btn_Bitacora";
            this.btn_Bitacora.Size = new System.Drawing.Size(300, 52);
            this.btn_Bitacora.TabIndex = 3;
            this.btn_Bitacora.Text = "Bitacora";
            this.btn_Bitacora.UseVisualStyleBackColor = true;
            this.btn_Bitacora.Click += new System.EventHandler(this.btn_Bitacora_Click);
            //
            // btn_BitacoraTurno
            //
            this.btn_BitacoraTurno.Location = new System.Drawing.Point(0, 300);
            this.btn_BitacoraTurno.Name = "btn_BitacoraTurno";
            this.btn_BitacoraTurno.Size = new System.Drawing.Size(300, 52);
            this.btn_BitacoraTurno.TabIndex = 11;
            this.btn_BitacoraTurno.Text = "Bitacora de Turnos";
            this.btn_BitacoraTurno.UseVisualStyleBackColor = true;
            this.btn_BitacoraTurno.Click += new System.EventHandler(this.btn_BitacoraTurno_Click);
            //
            // btn_Seguridad
            // 
            this.btn_Seguridad.Location = new System.Drawing.Point(0, 188);
            this.btn_Seguridad.Name = "btn_Seguridad";
            this.btn_Seguridad.Size = new System.Drawing.Size(300, 52);
            this.btn_Seguridad.TabIndex = 2;
            this.btn_Seguridad.Text = "Seguridad";
            this.btn_Seguridad.UseVisualStyleBackColor = true;
            this.btn_Seguridad.Click += new System.EventHandler(this.btn_Seguridad_Click);
            // 
            // btn_GestionRoles
            // 
            this.btn_GestionRoles.Location = new System.Drawing.Point(0, 111);
            this.btn_GestionRoles.Name = "btn_GestionRoles";
            this.btn_GestionRoles.Size = new System.Drawing.Size(300, 52);
            this.btn_GestionRoles.TabIndex = 1;
            this.btn_GestionRoles.Text = "Gestion de Roles";
            this.btn_GestionRoles.UseVisualStyleBackColor = true;
            this.btn_GestionRoles.Click += new System.EventHandler(this.btn_GestionRoles_Click);
            // 
            // btn_GestionUsuarios
            // 
            this.btn_GestionUsuarios.Location = new System.Drawing.Point(0, 34);
            this.btn_GestionUsuarios.Name = "btn_GestionUsuarios";
            this.btn_GestionUsuarios.Size = new System.Drawing.Size(300, 52);
            this.btn_GestionUsuarios.TabIndex = 0;
            this.btn_GestionUsuarios.Text = "Gestion de Usuarios";
            this.btn_GestionUsuarios.UseVisualStyleBackColor = true;
            this.btn_GestionUsuarios.Click += new System.EventHandler(this.btn_GestionUsuarios_Click);
            // 
            // btn_GestionMedicos
            // 
            this.btn_GestionMedicos.Location = new System.Drawing.Point(0, 563);
            this.btn_GestionMedicos.Name = "btn_GestionMedicos";
            this.btn_GestionMedicos.Size = new System.Drawing.Size(300, 54);
            this.btn_GestionMedicos.TabIndex = 10;
            this.btn_GestionMedicos.Text = "Gestion Medicos";
            this.btn_GestionMedicos.UseVisualStyleBackColor = true;
            this.btn_GestionMedicos.Click += new System.EventHandler(this.btn_GestionMedicos_Click);
            //
            // btn_GestionTurnos
            //
            this.btn_GestionTurnos.Location = new System.Drawing.Point(0, 617);
            this.btn_GestionTurnos.Name = "btn_GestionTurnos";
            this.btn_GestionTurnos.Size = new System.Drawing.Size(300, 54);
            this.btn_GestionTurnos.TabIndex = 12;
            this.btn_GestionTurnos.Text = "Gestion de Turnos";
            this.btn_GestionTurnos.UseVisualStyleBackColor = true;
            this.btn_GestionTurnos.Click += new System.EventHandler(this.btn_GestionTurnos_Click);
            //
            // timerVencimientoTurnos
            //
            this.timerVencimientoTurnos.Interval = 300000;
            this.timerVencimientoTurnos.Tick += new System.EventHandler(this.timerVencimientoTurnos_Tick);
            //
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 832);
            this.Controls.Add(this.groupBox_Menu);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Name = "frmPrincipal";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.groupBox_Menu.ResumeLayout(false);
            this.groupBox_Menu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Menu;
        private System.Windows.Forms.Button btn_Salir;
        private System.Windows.Forms.Button btn_Bitacora;
        private System.Windows.Forms.Button btn_BitacoraTurno;
        private System.Windows.Forms.Button btn_Seguridad;
        private System.Windows.Forms.Button btn_GestionRoles;
        private System.Windows.Forms.Button btn_GestionUsuarios;
        private System.Windows.Forms.Button btn_AccesoPaciente;
        private System.Windows.Forms.Button btn_AccesoMedico;
        private System.Windows.Forms.Button btnGestionIdiomas;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox CbPrincipalIdioma;
        private System.Windows.Forms.Button btn_GestionMedicos;
        private System.Windows.Forms.Button btn_GestionTurnos;
        private System.Windows.Forms.Timer timerVencimientoTurnos;
    }
}

