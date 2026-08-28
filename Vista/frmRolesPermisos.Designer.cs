namespace Vista
{
    partial class frmRolesPermisos
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
            this.groupBoxRolesPermisos = new System.Windows.Forms.GroupBox();
            this.btnGuardarRol = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.treeViewRoles = new System.Windows.Forms.TreeView();
            this.groupBoxDetalles = new System.Windows.Forms.GroupBox();
            this.btnGuardarRP = new System.Windows.Forms.Button();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.btnAgregarPR = new System.Windows.Forms.Button();
            this.cboTipo = new System.Windows.Forms.ComboBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.groupBoxRE = new System.Windows.Forms.GroupBox();
            this.cboRolesExistentes = new System.Windows.Forms.ComboBox();
            this.btnEliminarPermisos = new System.Windows.Forms.Button();
            this.btnAgregarPermisos = new System.Windows.Forms.Button();
            this.btnModificarPermisos = new System.Windows.Forms.Button();
            this.cboPermisos = new System.Windows.Forms.ComboBox();
            this.lblPermisos = new System.Windows.Forms.Label();
            this.btnEliminarRol = new System.Windows.Forms.Button();
            this.btnAgregarRolExistente = new System.Windows.Forms.Button();
            this.lblRoles = new System.Windows.Forms.Label();
            this.btnModificar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeViewRolesC = new System.Windows.Forms.TreeView();
            this.groupBoxRolesPermisos.SuspendLayout();
            this.groupBoxDetalles.SuspendLayout();
            this.groupBoxRE.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRolesPermisos
            // 
            this.groupBoxRolesPermisos.Controls.Add(this.btnGuardarRol);
            this.groupBoxRolesPermisos.Controls.Add(this.btnEliminar);
            this.groupBoxRolesPermisos.Controls.Add(this.button1);
            this.groupBoxRolesPermisos.Controls.Add(this.treeViewRoles);
            this.groupBoxRolesPermisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRolesPermisos.Location = new System.Drawing.Point(221, 8);
            this.groupBoxRolesPermisos.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxRolesPermisos.Name = "groupBoxRolesPermisos";
            this.groupBoxRolesPermisos.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxRolesPermisos.Size = new System.Drawing.Size(327, 526);
            this.groupBoxRolesPermisos.TabIndex = 1;
            this.groupBoxRolesPermisos.TabStop = false;
            this.groupBoxRolesPermisos.Text = "Roles y Permisos";
            // 
            // btnGuardarRol
            // 
            this.btnGuardarRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarRol.Location = new System.Drawing.Point(121, 469);
            this.btnGuardarRol.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardarRol.Name = "btnGuardarRol";
            this.btnGuardarRol.Size = new System.Drawing.Size(103, 47);
            this.btnGuardarRol.TabIndex = 6;
            this.btnGuardarRol.Text = "Crear Nuevo Rol";
            this.btnGuardarRol.UseVisualStyleBackColor = true;
            this.btnGuardarRol.Click += new System.EventHandler(this.btnGuardarRol_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(228, 477);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(80, 33);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(17, 476);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 33);
            this.button1.TabIndex = 5;
            this.button1.Text = "Limpiar Arbol";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeViewRoles
            // 
            this.treeViewRoles.Location = new System.Drawing.Point(17, 23);
            this.treeViewRoles.Margin = new System.Windows.Forms.Padding(2);
            this.treeViewRoles.Name = "treeViewRoles";
            this.treeViewRoles.Size = new System.Drawing.Size(291, 432);
            this.treeViewRoles.TabIndex = 4;
            // 
            // groupBoxDetalles
            // 
            this.groupBoxDetalles.Controls.Add(this.btnGuardarRP);
            this.groupBoxDetalles.Controls.Add(this.txtDescripcion);
            this.groupBoxDetalles.Controls.Add(this.lblDescripcion);
            this.groupBoxDetalles.Controls.Add(this.btnAgregarPR);
            this.groupBoxDetalles.Controls.Add(this.cboTipo);
            this.groupBoxDetalles.Controls.Add(this.txtNombre);
            this.groupBoxDetalles.Controls.Add(this.lblTipo);
            this.groupBoxDetalles.Controls.Add(this.lblNombre);
            this.groupBoxDetalles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDetalles.Location = new System.Drawing.Point(559, 8);
            this.groupBoxDetalles.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxDetalles.Name = "groupBoxDetalles";
            this.groupBoxDetalles.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxDetalles.Size = new System.Drawing.Size(357, 243);
            this.groupBoxDetalles.TabIndex = 2;
            this.groupBoxDetalles.TabStop = false;
            this.groupBoxDetalles.Text = "Crear Nuevos Roles O Permisos";
            // 
            // btnGuardarRP
            // 
            this.btnGuardarRP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarRP.Location = new System.Drawing.Point(171, 179);
            this.btnGuardarRP.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardarRP.Name = "btnGuardarRP";
            this.btnGuardarRP.Size = new System.Drawing.Size(135, 37);
            this.btnGuardarRP.TabIndex = 9;
            this.btnGuardarRP.Text = "Crear";
            this.btnGuardarRP.UseVisualStyleBackColor = true;
            this.btnGuardarRP.Click += new System.EventHandler(this.btnGuardarRP_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(125, 63);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(186, 64);
            this.txtDescripcion.TabIndex = 8;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(12, 79);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(103, 20);
            this.lblDescripcion.TabIndex = 7;
            this.lblDescripcion.Text = "Descripcion";
            // 
            // btnAgregarPR
            // 
            this.btnAgregarPR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarPR.Location = new System.Drawing.Point(27, 179);
            this.btnAgregarPR.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarPR.Name = "btnAgregarPR";
            this.btnAgregarPR.Size = new System.Drawing.Size(111, 37);
            this.btnAgregarPR.TabIndex = 5;
            this.btnAgregarPR.Text = "Agregar";
            this.btnAgregarPR.UseVisualStyleBackColor = true;
            this.btnAgregarPR.Click += new System.EventHandler(this.btnAgregarPR_Click);
            // 
            // cboTipo
            // 
            this.cboTipo.FormattingEnabled = true;
            this.cboTipo.Location = new System.Drawing.Point(125, 138);
            this.cboTipo.Margin = new System.Windows.Forms.Padding(2);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new System.Drawing.Size(197, 28);
            this.cboTipo.TabIndex = 3;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(125, 29);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(197, 26);
            this.txtNombre.TabIndex = 2;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(12, 140);
            this.lblTipo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(43, 20);
            this.lblTipo.TabIndex = 1;
            this.lblTipo.Text = "Tipo";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(12, 31);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(71, 20);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre";
            // 
            // groupBoxRE
            // 
            this.groupBoxRE.Controls.Add(this.cboRolesExistentes);
            this.groupBoxRE.Controls.Add(this.btnEliminarPermisos);
            this.groupBoxRE.Controls.Add(this.btnAgregarPermisos);
            this.groupBoxRE.Controls.Add(this.btnModificarPermisos);
            this.groupBoxRE.Controls.Add(this.cboPermisos);
            this.groupBoxRE.Controls.Add(this.lblPermisos);
            this.groupBoxRE.Controls.Add(this.btnEliminarRol);
            this.groupBoxRE.Controls.Add(this.btnAgregarRolExistente);
            this.groupBoxRE.Controls.Add(this.lblRoles);
            this.groupBoxRE.Controls.Add(this.btnModificar);
            this.groupBoxRE.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRE.Location = new System.Drawing.Point(559, 263);
            this.groupBoxRE.Name = "groupBoxRE";
            this.groupBoxRE.Size = new System.Drawing.Size(357, 269);
            this.groupBoxRE.TabIndex = 5;
            this.groupBoxRE.TabStop = false;
            this.groupBoxRE.Text = "Agregar Roles y Permisos Existentes";
            // 
            // cboRolesExistentes
            // 
            this.cboRolesExistentes.FormattingEnabled = true;
            this.cboRolesExistentes.Location = new System.Drawing.Point(125, 38);
            this.cboRolesExistentes.Name = "cboRolesExistentes";
            this.cboRolesExistentes.Size = new System.Drawing.Size(197, 28);
            this.cboRolesExistentes.TabIndex = 13;
            // 
            // btnEliminarPermisos
            // 
            this.btnEliminarPermisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarPermisos.Location = new System.Drawing.Point(206, 213);
            this.btnEliminarPermisos.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminarPermisos.Name = "btnEliminarPermisos";
            this.btnEliminarPermisos.Size = new System.Drawing.Size(87, 33);
            this.btnEliminarPermisos.TabIndex = 12;
            this.btnEliminarPermisos.Text = "Eliminar";
            this.btnEliminarPermisos.UseVisualStyleBackColor = true;
            this.btnEliminarPermisos.Click += new System.EventHandler(this.btnEliminarPermisos_Click);
            // 
            // btnAgregarPermisos
            // 
            this.btnAgregarPermisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarPermisos.Location = new System.Drawing.Point(12, 213);
            this.btnAgregarPermisos.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarPermisos.Name = "btnAgregarPermisos";
            this.btnAgregarPermisos.Size = new System.Drawing.Size(80, 33);
            this.btnAgregarPermisos.TabIndex = 11;
            this.btnAgregarPermisos.Text = "Agregar";
            this.btnAgregarPermisos.UseVisualStyleBackColor = true;
            this.btnAgregarPermisos.Click += new System.EventHandler(this.btnAgregarPermisos_Click);
            // 
            // btnModificarPermisos
            // 
            this.btnModificarPermisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarPermisos.Location = new System.Drawing.Point(105, 213);
            this.btnModificarPermisos.Margin = new System.Windows.Forms.Padding(2);
            this.btnModificarPermisos.Name = "btnModificarPermisos";
            this.btnModificarPermisos.Size = new System.Drawing.Size(87, 33);
            this.btnModificarPermisos.TabIndex = 10;
            this.btnModificarPermisos.Text = "Modificar";
            this.btnModificarPermisos.UseVisualStyleBackColor = true;
            this.btnModificarPermisos.Click += new System.EventHandler(this.btnModificarPermisos_Click);
            // 
            // cboPermisos
            // 
            this.cboPermisos.FormattingEnabled = true;
            this.cboPermisos.Location = new System.Drawing.Point(105, 168);
            this.cboPermisos.Name = "cboPermisos";
            this.cboPermisos.Size = new System.Drawing.Size(202, 28);
            this.cboPermisos.TabIndex = 9;
            // 
            // lblPermisos
            // 
            this.lblPermisos.AutoSize = true;
            this.lblPermisos.Location = new System.Drawing.Point(14, 171);
            this.lblPermisos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPermisos.Name = "lblPermisos";
            this.lblPermisos.Size = new System.Drawing.Size(82, 20);
            this.lblPermisos.TabIndex = 8;
            this.lblPermisos.Text = "Permisos";
            // 
            // btnEliminarRol
            // 
            this.btnEliminarRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarRol.Location = new System.Drawing.Point(209, 83);
            this.btnEliminarRol.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminarRol.Name = "btnEliminarRol";
            this.btnEliminarRol.Size = new System.Drawing.Size(87, 33);
            this.btnEliminarRol.TabIndex = 7;
            this.btnEliminarRol.Text = "Eliminar";
            this.btnEliminarRol.UseVisualStyleBackColor = true;
            this.btnEliminarRol.Click += new System.EventHandler(this.btnEliminarRol_Click);
            // 
            // btnAgregarRolExistente
            // 
            this.btnAgregarRolExistente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarRolExistente.Location = new System.Drawing.Point(16, 83);
            this.btnAgregarRolExistente.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarRolExistente.Name = "btnAgregarRolExistente";
            this.btnAgregarRolExistente.Size = new System.Drawing.Size(80, 33);
            this.btnAgregarRolExistente.TabIndex = 6;
            this.btnAgregarRolExistente.Text = "Agregar";
            this.btnAgregarRolExistente.UseVisualStyleBackColor = true;
            this.btnAgregarRolExistente.Click += new System.EventHandler(this.btnAgregarRolExistente_Click);
            // 
            // lblRoles
            // 
            this.lblRoles.AutoSize = true;
            this.lblRoles.Location = new System.Drawing.Point(11, 41);
            this.lblRoles.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRoles.Name = "lblRoles";
            this.lblRoles.Size = new System.Drawing.Size(55, 20);
            this.lblRoles.TabIndex = 2;
            this.lblRoles.Text = "Roles";
            // 
            // btnModificar
            // 
            this.btnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Location = new System.Drawing.Point(107, 83);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(2);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(87, 33);
            this.btnModificar.TabIndex = 2;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeViewRolesC);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(920, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(330, 526);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Roles Existentes";
            // 
            // treeViewRolesC
            // 
            this.treeViewRolesC.Location = new System.Drawing.Point(19, 23);
            this.treeViewRolesC.Name = "treeViewRolesC";
            this.treeViewRolesC.Size = new System.Drawing.Size(294, 487);
            this.treeViewRolesC.TabIndex = 4;
            // 
            // frmRolesPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 541);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxRE);
            this.Controls.Add(this.groupBoxDetalles);
            this.Controls.Add(this.groupBoxRolesPermisos);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmRolesPermisos";
            this.Text = "frm_GestionRoles";
            this.Load += new System.EventHandler(this.frm_GestionRoles_Load);
            this.groupBoxRolesPermisos.ResumeLayout(false);
            this.groupBoxDetalles.ResumeLayout(false);
            this.groupBoxDetalles.PerformLayout();
            this.groupBoxRE.ResumeLayout(false);
            this.groupBoxRE.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRolesPermisos;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeViewRoles;
        private System.Windows.Forms.GroupBox groupBoxDetalles;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Button btnAgregarPR;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnGuardarRP;
        private System.Windows.Forms.GroupBox groupBoxRE;
        private System.Windows.Forms.Button btnEliminarPermisos;
        private System.Windows.Forms.Button btnAgregarPermisos;
        private System.Windows.Forms.Button btnModificarPermisos;
        private System.Windows.Forms.ComboBox cboPermisos;
        private System.Windows.Forms.Label lblPermisos;
        private System.Windows.Forms.Button btnEliminarRol;
        private System.Windows.Forms.Button btnAgregarRolExistente;
        private System.Windows.Forms.Label lblRoles;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeViewRolesC;
        private System.Windows.Forms.ComboBox cboRolesExistentes;
        private System.Windows.Forms.Button btnGuardarRol;
    }
}