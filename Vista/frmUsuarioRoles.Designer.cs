namespace Vista
{
    partial class frmUsuarioRoles
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
            this.lbl_Rol = new System.Windows.Forms.Label();
            this.cboFiltroRol = new System.Windows.Forms.ComboBox();
            this.lbl_Usuario = new System.Windows.Forms.Label();
            this.cboUsuarios = new System.Windows.Forms.ComboBox();
            this.groupBoxRoles = new System.Windows.Forms.GroupBox();
            this.clbRoles = new System.Windows.Forms.CheckedListBox();
            this.btnGuardarRoles = new System.Windows.Forms.Button();
            this.groupBoxPermisosDirectos = new System.Windows.Forms.GroupBox();
            this.tvPermisosDirectos = new System.Windows.Forms.TreeView();
            this.btnGuardarPermisos = new System.Windows.Forms.Button();
            this.groupBoxPermisosEfectivos = new System.Windows.Forms.GroupBox();
            this.tvPermisosEfectivos = new System.Windows.Forms.TreeView();
            this.groupBoxRoles.SuspendLayout();
            this.groupBoxPermisosDirectos.SuspendLayout();
            this.groupBoxPermisosEfectivos.SuspendLayout();
            this.SuspendLayout();
            //
            // lbl_Usuario
            //
            this.lbl_Rol.AutoSize = true;
            this.lbl_Rol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Rol.Location = new System.Drawing.Point(327, 32);
            this.lbl_Rol.Name = "lbl_Rol";
            this.lbl_Rol.Size = new System.Drawing.Size(62, 29);
            this.lbl_Rol.TabIndex = 5;
            this.lbl_Rol.Text = "Rol:";
            //
            // cboFiltroRol
            //
            this.cboFiltroRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltroRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFiltroRol.FormattingEnabled = true;
            this.cboFiltroRol.Location = new System.Drawing.Point(400, 28);
            this.cboFiltroRol.Name = "cboFiltroRol";
            this.cboFiltroRol.Size = new System.Drawing.Size(300, 37);
            this.cboFiltroRol.TabIndex = 6;
            this.cboFiltroRol.SelectedIndexChanged += new System.EventHandler(this.cboFiltroRol_SelectedIndexChanged);
            //
            // lbl_Usuario
            //
            this.lbl_Usuario.AutoSize = true;
            this.lbl_Usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Usuario.Location = new System.Drawing.Point(745, 32);
            this.lbl_Usuario.Name = "lbl_Usuario";
            this.lbl_Usuario.Size = new System.Drawing.Size(110, 29);
            this.lbl_Usuario.TabIndex = 0;
            this.lbl_Usuario.Text = "Usuario:";
            //
            // cboUsuarios
            //
            this.cboUsuarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUsuarios.FormattingEnabled = true;
            this.cboUsuarios.Location = new System.Drawing.Point(865, 28);
            this.cboUsuarios.Name = "cboUsuarios";
            this.cboUsuarios.Size = new System.Drawing.Size(380, 37);
            this.cboUsuarios.TabIndex = 1;
            this.cboUsuarios.SelectedIndexChanged += new System.EventHandler(this.cboUsuarios_SelectedIndexChanged);
            //
            // groupBoxRoles
            //
            this.groupBoxRoles.Controls.Add(this.clbRoles);
            this.groupBoxRoles.Controls.Add(this.btnGuardarRoles);
            this.groupBoxRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRoles.Location = new System.Drawing.Point(327, 85);
            this.groupBoxRoles.Name = "groupBoxRoles";
            this.groupBoxRoles.Size = new System.Drawing.Size(400, 700);
            this.groupBoxRoles.TabIndex = 2;
            this.groupBoxRoles.TabStop = false;
            this.groupBoxRoles.Text = "Roles";
            //
            // clbRoles
            //
            this.clbRoles.CheckOnClick = true;
            this.clbRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbRoles.FormattingEnabled = true;
            this.clbRoles.Location = new System.Drawing.Point(15, 38);
            this.clbRoles.Name = "clbRoles";
            this.clbRoles.Size = new System.Drawing.Size(370, 568);
            this.clbRoles.TabIndex = 0;
            //
            // btnGuardarRoles
            //
            this.btnGuardarRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarRoles.Location = new System.Drawing.Point(15, 630);
            this.btnGuardarRoles.Name = "btnGuardarRoles";
            this.btnGuardarRoles.Size = new System.Drawing.Size(200, 52);
            this.btnGuardarRoles.TabIndex = 1;
            this.btnGuardarRoles.Text = "Guardar Roles";
            this.btnGuardarRoles.UseVisualStyleBackColor = true;
            this.btnGuardarRoles.Click += new System.EventHandler(this.btnGuardarRoles_Click);
            //
            // groupBoxPermisosDirectos
            //
            this.groupBoxPermisosDirectos.Controls.Add(this.tvPermisosDirectos);
            this.groupBoxPermisosDirectos.Controls.Add(this.btnGuardarPermisos);
            this.groupBoxPermisosDirectos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPermisosDirectos.Location = new System.Drawing.Point(745, 85);
            this.groupBoxPermisosDirectos.Name = "groupBoxPermisosDirectos";
            this.groupBoxPermisosDirectos.Size = new System.Drawing.Size(560, 700);
            this.groupBoxPermisosDirectos.TabIndex = 3;
            this.groupBoxPermisosDirectos.TabStop = false;
            this.groupBoxPermisosDirectos.Text = "Permisos directos (página o botón) — en gris: ya los tiene por un rol";
            //
            // tvPermisosDirectos
            //
            this.tvPermisosDirectos.CheckBoxes = true;
            this.tvPermisosDirectos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvPermisosDirectos.Location = new System.Drawing.Point(15, 38);
            this.tvPermisosDirectos.Name = "tvPermisosDirectos";
            this.tvPermisosDirectos.Size = new System.Drawing.Size(530, 575);
            this.tvPermisosDirectos.TabIndex = 0;
            //
            // btnGuardarPermisos
            //
            this.btnGuardarPermisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarPermisos.Location = new System.Drawing.Point(15, 630);
            this.btnGuardarPermisos.Name = "btnGuardarPermisos";
            this.btnGuardarPermisos.Size = new System.Drawing.Size(220, 52);
            this.btnGuardarPermisos.TabIndex = 1;
            this.btnGuardarPermisos.Text = "Guardar Permisos";
            this.btnGuardarPermisos.UseVisualStyleBackColor = true;
            this.btnGuardarPermisos.Click += new System.EventHandler(this.btnGuardarPermisos_Click);
            //
            // groupBoxPermisosEfectivos
            //
            this.groupBoxPermisosEfectivos.Controls.Add(this.tvPermisosEfectivos);
            this.groupBoxPermisosEfectivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPermisosEfectivos.Location = new System.Drawing.Point(1323, 85);
            this.groupBoxPermisosEfectivos.Name = "groupBoxPermisosEfectivos";
            this.groupBoxPermisosEfectivos.Size = new System.Drawing.Size(560, 700);
            this.groupBoxPermisosEfectivos.TabIndex = 4;
            this.groupBoxPermisosEfectivos.TabStop = false;
            this.groupBoxPermisosEfectivos.Text = "Permisos efectivos del usuario";
            //
            // tvPermisosEfectivos
            //
            this.tvPermisosEfectivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvPermisosEfectivos.Location = new System.Drawing.Point(15, 38);
            this.tvPermisosEfectivos.Name = "tvPermisosEfectivos";
            this.tvPermisosEfectivos.Size = new System.Drawing.Size(530, 644);
            this.tvPermisosEfectivos.TabIndex = 0;
            //
            // frmUsuarioRoles
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 832);
            this.Controls.Add(this.groupBoxPermisosEfectivos);
            this.Controls.Add(this.groupBoxPermisosDirectos);
            this.Controls.Add(this.groupBoxRoles);
            this.Controls.Add(this.cboUsuarios);
            this.Controls.Add(this.lbl_Usuario);
            this.Controls.Add(this.cboFiltroRol);
            this.Controls.Add(this.lbl_Rol);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "frmUsuarioRoles";
            this.Text = "Asignación de Roles";
            this.Load += new System.EventHandler(this.frmUsuarioRoles_Load);
            this.groupBoxRoles.ResumeLayout(false);
            this.groupBoxPermisosDirectos.ResumeLayout(false);
            this.groupBoxPermisosEfectivos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Rol;
        private System.Windows.Forms.ComboBox cboFiltroRol;
        private System.Windows.Forms.Label lbl_Usuario;
        private System.Windows.Forms.ComboBox cboUsuarios;
        private System.Windows.Forms.GroupBox groupBoxRoles;
        private System.Windows.Forms.CheckedListBox clbRoles;
        private System.Windows.Forms.Button btnGuardarRoles;
        private System.Windows.Forms.GroupBox groupBoxPermisosDirectos;
        private System.Windows.Forms.TreeView tvPermisosDirectos;
        private System.Windows.Forms.Button btnGuardarPermisos;
        private System.Windows.Forms.GroupBox groupBoxPermisosEfectivos;
        private System.Windows.Forms.TreeView tvPermisosEfectivos;
    }
}
