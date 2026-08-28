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
            this.groupBox_Menu = new System.Windows.Forms.GroupBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnGestionIdiomas = new System.Windows.Forms.Button();
            this.btn_AccesoPaciente = new System.Windows.Forms.Button();
            this.btn_AccesoMedico = new System.Windows.Forms.Button();
            this.btn_Salir = new System.Windows.Forms.Button();
            this.btn_Bitacora = new System.Windows.Forms.Button();
            this.btn_Seguridad = new System.Windows.Forms.Button();
            this.btn_GestionRoles = new System.Windows.Forms.Button();
            this.btn_GestionUsuarios = new System.Windows.Forms.Button();
            this.CbPrincipalIdioma = new System.Windows.Forms.ComboBox();
            this.groupBox_Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Menu
            // 
            this.groupBox_Menu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox_Menu.Controls.Add(this.CbPrincipalIdioma);
            this.groupBox_Menu.Controls.Add(this.lblUsuario);
            this.groupBox_Menu.Controls.Add(this.btnGestionIdiomas);
            this.groupBox_Menu.Controls.Add(this.btn_AccesoPaciente);
            this.groupBox_Menu.Controls.Add(this.btn_AccesoMedico);
            this.groupBox_Menu.Controls.Add(this.btn_Salir);
            this.groupBox_Menu.Controls.Add(this.btn_Bitacora);
            this.groupBox_Menu.Controls.Add(this.btn_Seguridad);
            this.groupBox_Menu.Controls.Add(this.btn_GestionRoles);
            this.groupBox_Menu.Controls.Add(this.btn_GestionUsuarios);
            this.groupBox_Menu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Menu.Location = new System.Drawing.Point(0, 2);
            this.groupBox_Menu.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Menu.Name = "groupBox_Menu";
            this.groupBox_Menu.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Menu.Size = new System.Drawing.Size(200, 538);
            this.groupBox_Menu.TabIndex = 1;
            this.groupBox_Menu.TabStop = false;
            this.groupBox_Menu.Text = "Menu";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(32, 450);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(0, 20);
            this.lblUsuario.TabIndex = 8;
            // 
            // btnGestionIdiomas
            // 
            this.btnGestionIdiomas.Location = new System.Drawing.Point(0, 222);
            this.btnGestionIdiomas.Margin = new System.Windows.Forms.Padding(2);
            this.btnGestionIdiomas.Name = "btnGestionIdiomas";
            this.btnGestionIdiomas.Size = new System.Drawing.Size(200, 34);
            this.btnGestionIdiomas.TabIndex = 7;
            this.btnGestionIdiomas.Text = "Gestion Idiomas";
            this.btnGestionIdiomas.UseVisualStyleBackColor = true;
            this.btnGestionIdiomas.Click += new System.EventHandler(this.btnGestionIdiomas_Click);
            // 
            // btn_AccesoPaciente
            // 
            this.btn_AccesoPaciente.Location = new System.Drawing.Point(0, 318);
            this.btn_AccesoPaciente.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AccesoPaciente.Name = "btn_AccesoPaciente";
            this.btn_AccesoPaciente.Size = new System.Drawing.Size(200, 34);
            this.btn_AccesoPaciente.TabIndex = 6;
            this.btn_AccesoPaciente.Text = "Acceso Paciente";
            this.btn_AccesoPaciente.UseVisualStyleBackColor = true;
            this.btn_AccesoPaciente.Click += new System.EventHandler(this.btn_AccesoPaciente_Click);
            // 
            // btn_AccesoMedico
            // 
            this.btn_AccesoMedico.Location = new System.Drawing.Point(0, 269);
            this.btn_AccesoMedico.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AccesoMedico.Name = "btn_AccesoMedico";
            this.btn_AccesoMedico.Size = new System.Drawing.Size(200, 34);
            this.btn_AccesoMedico.TabIndex = 5;
            this.btn_AccesoMedico.Text = "Acceso Medico";
            this.btn_AccesoMedico.UseVisualStyleBackColor = true;
            this.btn_AccesoMedico.Click += new System.EventHandler(this.btn_AccesoMedico_Click);
            // 
            // btn_Salir
            // 
            this.btn_Salir.Location = new System.Drawing.Point(119, 505);
            this.btn_Salir.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Salir.Name = "btn_Salir";
            this.btn_Salir.Size = new System.Drawing.Size(81, 33);
            this.btn_Salir.TabIndex = 4;
            this.btn_Salir.Text = "Salir";
            this.btn_Salir.UseVisualStyleBackColor = true;
            this.btn_Salir.Click += new System.EventHandler(this.btn_Salir_Click);
            // 
            // btn_Bitacora
            // 
            this.btn_Bitacora.Location = new System.Drawing.Point(0, 173);
            this.btn_Bitacora.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Bitacora.Name = "btn_Bitacora";
            this.btn_Bitacora.Size = new System.Drawing.Size(200, 34);
            this.btn_Bitacora.TabIndex = 3;
            this.btn_Bitacora.Text = "Bitacora";
            this.btn_Bitacora.UseVisualStyleBackColor = true;
            this.btn_Bitacora.Click += new System.EventHandler(this.btn_Bitacora_Click);
            // 
            // btn_Seguridad
            // 
            this.btn_Seguridad.Location = new System.Drawing.Point(0, 122);
            this.btn_Seguridad.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Seguridad.Name = "btn_Seguridad";
            this.btn_Seguridad.Size = new System.Drawing.Size(200, 34);
            this.btn_Seguridad.TabIndex = 2;
            this.btn_Seguridad.Text = "Seguridad";
            this.btn_Seguridad.UseVisualStyleBackColor = true;
            this.btn_Seguridad.Click += new System.EventHandler(this.btn_Seguridad_Click);
            // 
            // btn_GestionRoles
            // 
            this.btn_GestionRoles.Location = new System.Drawing.Point(0, 72);
            this.btn_GestionRoles.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GestionRoles.Name = "btn_GestionRoles";
            this.btn_GestionRoles.Size = new System.Drawing.Size(200, 34);
            this.btn_GestionRoles.TabIndex = 1;
            this.btn_GestionRoles.Text = "Gestion de Roles";
            this.btn_GestionRoles.UseVisualStyleBackColor = true;
            this.btn_GestionRoles.Click += new System.EventHandler(this.btn_GestionRoles_Click);
            // 
            // btn_GestionUsuarios
            // 
            this.btn_GestionUsuarios.Location = new System.Drawing.Point(0, 22);
            this.btn_GestionUsuarios.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GestionUsuarios.Name = "btn_GestionUsuarios";
            this.btn_GestionUsuarios.Size = new System.Drawing.Size(200, 34);
            this.btn_GestionUsuarios.TabIndex = 0;
            this.btn_GestionUsuarios.Text = "Gestion de Usuarios";
            this.btn_GestionUsuarios.UseVisualStyleBackColor = true;
            this.btn_GestionUsuarios.Click += new System.EventHandler(this.btn_GestionUsuarios_Click);
            // 
            // CbPrincipalIdioma
            // 
            this.CbPrincipalIdioma.FormattingEnabled = true;
            this.CbPrincipalIdioma.Location = new System.Drawing.Point(79, 380);
            this.CbPrincipalIdioma.Name = "CbPrincipalIdioma";
            this.CbPrincipalIdioma.Size = new System.Drawing.Size(121, 28);
            this.CbPrincipalIdioma.TabIndex = 9;
            this.CbPrincipalIdioma.SelectedIndexChanged += new System.EventHandler(this.CbPrincipalIdioma_SelectedIndexChanged);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 541);
            this.Controls.Add(this.groupBox_Menu);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Button btn_Seguridad;
        private System.Windows.Forms.Button btn_GestionRoles;
        private System.Windows.Forms.Button btn_GestionUsuarios;
        private System.Windows.Forms.Button btn_AccesoPaciente;
        private System.Windows.Forms.Button btn_AccesoMedico;
        private System.Windows.Forms.Button btnGestionIdiomas;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox CbPrincipalIdioma;
    }
}

