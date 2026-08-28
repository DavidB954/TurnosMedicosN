namespace Vista
{
    partial class frmUsuario
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
            this.groupBox_DatosUsuario = new System.Windows.Forms.GroupBox();
            this.cboActivo = new System.Windows.Forms.ComboBox();
            this.lbl_Estado = new System.Windows.Forms.Label();
            this.btnModificar = new System.Windows.Forms.Button();
            this.lbl_Nombre = new System.Windows.Forms.Label();
            this.lbl_Contrasena = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lbl_Email = new System.Windows.Forms.Label();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.lbl_Apellido = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboRoles = new System.Windows.Forms.ComboBox();
            this.cboUsuarios = new System.Windows.Forms.ComboBox();
            this.btnEliminarRol = new System.Windows.Forms.Button();
            this.btnAsignarRol = new System.Windows.Forms.Button();
            this.lbl_Roles = new System.Windows.Forms.Label();
            this.lbl_NombreUsuario = new System.Windows.Forms.Label();
            this.groupBox_RolesDeUsuario = new System.Windows.Forms.GroupBox();
            this.treeViewUsuarios = new System.Windows.Forms.TreeView();
            this.GroupBoxUsuarioHistorial = new System.Windows.Forms.GroupBox();
            this.cboUsuario_Historial = new System.Windows.Forms.ComboBox();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.groupBoxUsuarios = new System.Windows.Forms.GroupBox();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.groupBox_DatosUsuario.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox_RolesDeUsuario.SuspendLayout();
            this.GroupBoxUsuarioHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.groupBoxUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_DatosUsuario
            // 
            this.groupBox_DatosUsuario.Controls.Add(this.cboActivo);
            this.groupBox_DatosUsuario.Controls.Add(this.lbl_Estado);
            this.groupBox_DatosUsuario.Controls.Add(this.btnModificar);
            this.groupBox_DatosUsuario.Controls.Add(this.lbl_Nombre);
            this.groupBox_DatosUsuario.Controls.Add(this.lbl_Contrasena);
            this.groupBox_DatosUsuario.Controls.Add(this.txtEmail);
            this.groupBox_DatosUsuario.Controls.Add(this.btnAgregar);
            this.groupBox_DatosUsuario.Controls.Add(this.txtApellido);
            this.groupBox_DatosUsuario.Controls.Add(this.btnEliminar);
            this.groupBox_DatosUsuario.Controls.Add(this.lbl_Email);
            this.groupBox_DatosUsuario.Controls.Add(this.txtNombreUsuario);
            this.groupBox_DatosUsuario.Controls.Add(this.lbl_Apellido);
            this.groupBox_DatosUsuario.Controls.Add(this.txtPassword);
            this.groupBox_DatosUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_DatosUsuario.Location = new System.Drawing.Point(327, 28);
            this.groupBox_DatosUsuario.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_DatosUsuario.Name = "groupBox_DatosUsuario";
            this.groupBox_DatosUsuario.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_DatosUsuario.Size = new System.Drawing.Size(432, 362);
            this.groupBox_DatosUsuario.TabIndex = 14;
            this.groupBox_DatosUsuario.TabStop = false;
            this.groupBox_DatosUsuario.Text = "Datos del Usuarios";
            // 
            // cboActivo
            // 
            this.cboActivo.FormattingEnabled = true;
            this.cboActivo.Location = new System.Drawing.Point(110, 214);
            this.cboActivo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboActivo.Name = "cboActivo";
            this.cboActivo.Size = new System.Drawing.Size(186, 37);
            this.cboActivo.TabIndex = 12;
            // 
            // lbl_Estado
            // 
            this.lbl_Estado.AutoSize = true;
            this.lbl_Estado.Location = new System.Drawing.Point(8, 214);
            this.lbl_Estado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Estado.Name = "lbl_Estado";
            this.lbl_Estado.Size = new System.Drawing.Size(94, 29);
            this.lbl_Estado.TabIndex = 11;
            this.lbl_Estado.Text = "Estado";
            // 
            // btnModificar
            // 
            this.btnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.Location = new System.Drawing.Point(138, 272);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(123, 57);
            this.btnModificar.TabIndex = 6;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // lbl_Nombre
            // 
            this.lbl_Nombre.AutoSize = true;
            this.lbl_Nombre.Location = new System.Drawing.Point(8, 46);
            this.lbl_Nombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Nombre.Name = "lbl_Nombre";
            this.lbl_Nombre.Size = new System.Drawing.Size(107, 29);
            this.lbl_Nombre.TabIndex = 0;
            this.lbl_Nombre.Text = "Nombre";
            // 
            // lbl_Contrasena
            // 
            this.lbl_Contrasena.AutoSize = true;
            this.lbl_Contrasena.Location = new System.Drawing.Point(218, 129);
            this.lbl_Contrasena.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Contrasena.Name = "lbl_Contrasena";
            this.lbl_Contrasena.Size = new System.Drawing.Size(135, 29);
            this.lbl_Contrasena.TabIndex = 1;
            this.lbl_Contrasena.Text = "Password ";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(12, 163);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(187, 35);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.Text = " ";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(8, 272);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(123, 57);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(222, 80);
            this.txtApellido.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(194, 35);
            this.txtApellido.TabIndex = 2;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(268, 272);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(123, 57);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lbl_Email
            // 
            this.lbl_Email.AutoSize = true;
            this.lbl_Email.Location = new System.Drawing.Point(8, 129);
            this.lbl_Email.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Email.Name = "lbl_Email";
            this.lbl_Email.Size = new System.Drawing.Size(79, 29);
            this.lbl_Email.TabIndex = 9;
            this.lbl_Email.Text = "Email";
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Location = new System.Drawing.Point(8, 80);
            this.txtNombreUsuario.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(192, 35);
            this.txtNombreUsuario.TabIndex = 1;
            // 
            // lbl_Apellido
            // 
            this.lbl_Apellido.AutoSize = true;
            this.lbl_Apellido.Location = new System.Drawing.Point(218, 49);
            this.lbl_Apellido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Apellido.Name = "lbl_Apellido";
            this.lbl_Apellido.Size = new System.Drawing.Size(110, 29);
            this.lbl_Apellido.TabIndex = 8;
            this.lbl_Apellido.Text = "Apellido";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(222, 163);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(194, 35);
            this.txtPassword.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboRoles);
            this.groupBox2.Controls.Add(this.cboUsuarios);
            this.groupBox2.Controls.Add(this.btnEliminarRol);
            this.groupBox2.Controls.Add(this.btnAsignarRol);
            this.groupBox2.Controls.Add(this.lbl_Roles);
            this.groupBox2.Controls.Add(this.lbl_NombreUsuario);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(808, 28);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(432, 362);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Asignacion de Roles";
            // 
            // cboRoles
            // 
            this.cboRoles.FormattingEnabled = true;
            this.cboRoles.Location = new System.Drawing.Point(147, 122);
            this.cboRoles.Name = "cboRoles";
            this.cboRoles.Size = new System.Drawing.Size(238, 37);
            this.cboRoles.TabIndex = 8;

            // 
            // cboUsuarios
            // 
            this.cboUsuarios.FormattingEnabled = true;
            this.cboUsuarios.Location = new System.Drawing.Point(147, 46);
            this.cboUsuarios.Name = "cboUsuarios";
            this.cboUsuarios.Size = new System.Drawing.Size(238, 37);
            this.cboUsuarios.TabIndex = 7;
            this.cboUsuarios.SelectedIndexChanged += new System.EventHandler(this.cboUsuarios_SelectedIndexChanged);
            // 
            // btnEliminarRol
            // 
            this.btnEliminarRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarRol.Location = new System.Drawing.Point(224, 271);
            this.btnEliminarRol.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEliminarRol.Name = "btnEliminarRol";
            this.btnEliminarRol.Size = new System.Drawing.Size(156, 51);
            this.btnEliminarRol.TabIndex = 6;
            this.btnEliminarRol.Text = "Quitar Rol";
            this.btnEliminarRol.UseVisualStyleBackColor = true;
            this.btnEliminarRol.Click += new System.EventHandler(this.btnEliminarRol_Click);
            // 
            // btnAsignarRol
            // 
            this.btnAsignarRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarRol.Location = new System.Drawing.Point(51, 272);
            this.btnAsignarRol.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAsignarRol.Name = "btnAsignarRol";
            this.btnAsignarRol.Size = new System.Drawing.Size(141, 49);
            this.btnAsignarRol.TabIndex = 4;
            this.btnAsignarRol.Text = "Agregar Rol";
            this.btnAsignarRol.UseVisualStyleBackColor = true;
            this.btnAsignarRol.Click += new System.EventHandler(this.btnAsignarRol_Click);
            // 
            // lbl_Roles
            // 
            this.lbl_Roles.AutoSize = true;
            this.lbl_Roles.Location = new System.Drawing.Point(28, 129);
            this.lbl_Roles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Roles.Name = "lbl_Roles";
            this.lbl_Roles.Size = new System.Drawing.Size(88, 29);
            this.lbl_Roles.TabIndex = 2;
            this.lbl_Roles.Text = "Roles:";
            // 
            // lbl_NombreUsuario
            // 
            this.lbl_NombreUsuario.AutoSize = true;
            this.lbl_NombreUsuario.Location = new System.Drawing.Point(28, 46);
            this.lbl_NombreUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_NombreUsuario.Name = "lbl_NombreUsuario";
            this.lbl_NombreUsuario.Size = new System.Drawing.Size(110, 29);
            this.lbl_NombreUsuario.TabIndex = 0;
            this.lbl_NombreUsuario.Text = "Usuario:";
            // 
            // groupBox_RolesDeUsuario
            // 
            this.groupBox_RolesDeUsuario.Controls.Add(this.treeViewUsuarios);
            this.groupBox_RolesDeUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_RolesDeUsuario.Location = new System.Drawing.Point(1294, 28);
            this.groupBox_RolesDeUsuario.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_RolesDeUsuario.Name = "groupBox_RolesDeUsuario";
            this.groupBox_RolesDeUsuario.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_RolesDeUsuario.Size = new System.Drawing.Size(578, 362);
            this.groupBox_RolesDeUsuario.TabIndex = 17;
            this.groupBox_RolesDeUsuario.TabStop = false;
            this.groupBox_RolesDeUsuario.Text = "Roles del Usuario";
            // 
            // treeViewUsuarios
            // 
            this.treeViewUsuarios.Location = new System.Drawing.Point(28, 46);
            this.treeViewUsuarios.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeViewUsuarios.Name = "treeViewUsuarios";
            this.treeViewUsuarios.Size = new System.Drawing.Size(522, 310);
            this.treeViewUsuarios.TabIndex = 15;
            this.treeViewUsuarios.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewUsuarios_AfterSelect);
            // 
            // GroupBoxUsuarioHistorial
            // 
            this.GroupBoxUsuarioHistorial.Controls.Add(this.cboUsuario_Historial);
            this.GroupBoxUsuarioHistorial.Controls.Add(this.btnRestaurar);
            this.GroupBoxUsuarioHistorial.Controls.Add(this.dgvHistorial);
            this.GroupBoxUsuarioHistorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBoxUsuarioHistorial.Location = new System.Drawing.Point(1036, 415);
            this.GroupBoxUsuarioHistorial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBoxUsuarioHistorial.Name = "GroupBoxUsuarioHistorial";
            this.GroupBoxUsuarioHistorial.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBoxUsuarioHistorial.Size = new System.Drawing.Size(836, 398);
            this.GroupBoxUsuarioHistorial.TabIndex = 19;
            this.GroupBoxUsuarioHistorial.TabStop = false;
            this.GroupBoxUsuarioHistorial.Text = "Historial";
            // 
            // cboUsuario_Historial
            // 
            this.cboUsuario_Historial.FormattingEnabled = true;
            this.cboUsuario_Historial.Location = new System.Drawing.Point(285, 18);
            this.cboUsuario_Historial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboUsuario_Historial.Name = "cboUsuario_Historial";
            this.cboUsuario_Historial.Size = new System.Drawing.Size(344, 33);
            this.cboUsuario_Historial.TabIndex = 7;
            this.cboUsuario_Historial.SelectedIndexChanged += new System.EventHandler(this.cboUsuario_Historial_SelectedIndexChanged);
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(651, 18);
            this.btnRestaurar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(176, 51);
            this.btnRestaurar.TabIndex = 1;
            this.btnRestaurar.Text = "Restaurar";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Location = new System.Drawing.Point(8, 80);
            this.dgvHistorial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.RowHeadersWidth = 62;
            this.dgvHistorial.Size = new System.Drawing.Size(819, 289);
            this.dgvHistorial.TabIndex = 0;
            // 
            // groupBoxUsuarios
            // 
            this.groupBoxUsuarios.Controls.Add(this.dgvUsuarios);
            this.groupBoxUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxUsuarios.Location = new System.Drawing.Point(334, 415);
            this.groupBoxUsuarios.Name = "groupBoxUsuarios";
            this.groupBoxUsuarios.Size = new System.Drawing.Size(656, 398);
            this.groupBoxUsuarios.TabIndex = 20;
            this.groupBoxUsuarios.TabStop = false;
            this.groupBoxUsuarios.Text = "Usuarios";
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Location = new System.Drawing.Point(22, 80);
            this.dgvUsuarios.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersWidth = 62;
            this.dgvUsuarios.Size = new System.Drawing.Size(626, 289);
            this.dgvUsuarios.TabIndex = 19;
            this.dgvUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellClick);
            // 
            // frmUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 832);
            this.Controls.Add(this.groupBoxUsuarios);
            this.Controls.Add(this.GroupBoxUsuarioHistorial);
            this.Controls.Add(this.groupBox_RolesDeUsuario);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox_DatosUsuario);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "frmUsuario";
            this.Text = "frm_GestionUsuarios";
            this.Load += new System.EventHandler(this.frm_GestionUsuarios_Load);
            this.groupBox_DatosUsuario.ResumeLayout(false);
            this.groupBox_DatosUsuario.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox_RolesDeUsuario.ResumeLayout(false);
            this.GroupBoxUsuarioHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.groupBoxUsuarios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_DatosUsuario;
        private System.Windows.Forms.ComboBox cboActivo;
        private System.Windows.Forms.Label lbl_Estado;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Label lbl_Nombre;
        private System.Windows.Forms.Label lbl_Contrasena;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lbl_Email;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.Label lbl_Apellido;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnEliminarRol;
        private System.Windows.Forms.Button btnAsignarRol;
        private System.Windows.Forms.Label lbl_Roles;
        private System.Windows.Forms.Label lbl_NombreUsuario;
        private System.Windows.Forms.GroupBox groupBox_RolesDeUsuario;
        private System.Windows.Forms.TreeView treeViewUsuarios;
        private System.Windows.Forms.GroupBox GroupBoxUsuarioHistorial;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.GroupBox groupBoxUsuarios;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.ComboBox cboUsuario_Historial;
        private System.Windows.Forms.ComboBox cboRoles;
        private System.Windows.Forms.ComboBox cboUsuarios;
    }
}