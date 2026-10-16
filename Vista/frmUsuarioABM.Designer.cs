namespace Vista
{
    partial class frmUsuarioABM
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
            this.groupBoxUsuarios = new System.Windows.Forms.GroupBox();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.groupBox_DatosUsuario.SuspendLayout();
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
            this.groupBox_DatosUsuario.TabIndex = 0;
            this.groupBox_DatosUsuario.TabStop = false;
            this.groupBox_DatosUsuario.Text = "Datos del Usuario";
            //
            // cboActivo
            //
            this.cboActivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboActivo.FormattingEnabled = true;
            this.cboActivo.Location = new System.Drawing.Point(110, 214);
            this.cboActivo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboActivo.Name = "cboActivo";
            this.cboActivo.Size = new System.Drawing.Size(186, 37);
            this.cboActivo.TabIndex = 5;
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
            this.btnModificar.Location = new System.Drawing.Point(138, 272);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(123, 57);
            this.btnModificar.TabIndex = 7;
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
            //
            // btnAgregar
            //
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(8, 272);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(123, 57);
            this.btnAgregar.TabIndex = 6;
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
            // groupBoxUsuarios
            //
            this.groupBoxUsuarios.Controls.Add(this.dgvUsuarios);
            this.groupBoxUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxUsuarios.Location = new System.Drawing.Point(790, 28);
            this.groupBoxUsuarios.Name = "groupBoxUsuarios";
            this.groupBoxUsuarios.Size = new System.Drawing.Size(1080, 760);
            this.groupBoxUsuarios.TabIndex = 1;
            this.groupBoxUsuarios.TabStop = false;
            this.groupBoxUsuarios.Text = "Usuarios";
            //
            // dgvUsuarios
            //
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Location = new System.Drawing.Point(15, 35);
            this.dgvUsuarios.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersWidth = 62;
            this.dgvUsuarios.Size = new System.Drawing.Size(1050, 705);
            this.dgvUsuarios.TabIndex = 0;
            this.dgvUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellClick);
            //
            // frmUsuarioABM
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 832);
            this.Controls.Add(this.groupBoxUsuarios);
            this.Controls.Add(this.groupBox_DatosUsuario);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "frmUsuarioABM";
            this.Text = "ABM de Usuarios";
            this.Load += new System.EventHandler(this.frmUsuarioABM_Load);
            this.groupBox_DatosUsuario.ResumeLayout(false);
            this.groupBox_DatosUsuario.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBoxUsuarios;
        private System.Windows.Forms.DataGridView dgvUsuarios;
    }
}
