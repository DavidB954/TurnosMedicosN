namespace Vista
{
    partial class frmRolesPermisos
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
            this.groupBoxRoles = new System.Windows.Forms.GroupBox();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnNuevoRol = new System.Windows.Forms.Button();
            this.btnModificarRol = new System.Windows.Forms.Button();
            this.btnEliminarRol = new System.Windows.Forms.Button();
            this.groupBoxPermisos = new System.Windows.Forms.GroupBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.tvPermisos = new System.Windows.Forms.TreeView();
            this.btnGuardarPermisos = new System.Windows.Forms.Button();
            this.groupBoxRoles.SuspendLayout();
            this.groupBoxPermisos.SuspendLayout();
            this.SuspendLayout();
            this.groupBoxRoles.Controls.Add(this.lstRoles);
            this.groupBoxRoles.Controls.Add(this.lblNombre);
            this.groupBoxRoles.Controls.Add(this.txtNombre);
            this.groupBoxRoles.Controls.Add(this.lblDescripcion);
            this.groupBoxRoles.Controls.Add(this.txtDescripcion);
            this.groupBoxRoles.Controls.Add(this.btnNuevoRol);
            this.groupBoxRoles.Controls.Add(this.btnModificarRol);
            this.groupBoxRoles.Controls.Add(this.btnEliminarRol);
            this.groupBoxRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRoles.Location = new System.Drawing.Point(30, 28);
            this.groupBoxRoles.Name = "groupBoxRoles";
            this.groupBoxRoles.Size = new System.Drawing.Size(440, 760);
            this.groupBoxRoles.TabIndex = 0;
            this.groupBoxRoles.TabStop = false;
            this.groupBoxRoles.Text = "Roles";
            this.lstRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.ItemHeight = 25;
            this.lstRoles.Location = new System.Drawing.Point(15, 38);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(410, 334);
            this.lstRoles.TabIndex = 0;
            this.lstRoles.SelectedIndexChanged += new System.EventHandler(this.lstRoles_SelectedIndexChanged);
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(15, 390);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(101, 29);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre";
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(15, 422);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(410, 32);
            this.txtNombre.TabIndex = 2;
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(15, 468);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(149, 29);
            this.lblDescripcion.TabIndex = 3;
            this.lblDescripcion.Text = "Descripción";
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(15, 500);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Size = new System.Drawing.Size(410, 110);
            this.txtDescripcion.TabIndex = 4;
            this.btnNuevoRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoRol.Location = new System.Drawing.Point(15, 630);
            this.btnNuevoRol.Name = "btnNuevoRol";
            this.btnNuevoRol.Size = new System.Drawing.Size(130, 52);
            this.btnNuevoRol.TabIndex = 5;
            this.btnNuevoRol.Text = "Nuevo";
            this.btnNuevoRol.UseVisualStyleBackColor = true;
            this.btnNuevoRol.Click += new System.EventHandler(this.btnNuevoRol_Click);
            this.btnModificarRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarRol.Location = new System.Drawing.Point(155, 630);
            this.btnModificarRol.Name = "btnModificarRol";
            this.btnModificarRol.Size = new System.Drawing.Size(130, 52);
            this.btnModificarRol.TabIndex = 6;
            this.btnModificarRol.Text = "Modificar";
            this.btnModificarRol.UseVisualStyleBackColor = true;
            this.btnModificarRol.Click += new System.EventHandler(this.btnModificarRol_Click);
            this.btnEliminarRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarRol.Location = new System.Drawing.Point(295, 630);
            this.btnEliminarRol.Name = "btnEliminarRol";
            this.btnEliminarRol.Size = new System.Drawing.Size(130, 52);
            this.btnEliminarRol.TabIndex = 7;
            this.btnEliminarRol.Text = "Eliminar";
            this.btnEliminarRol.UseVisualStyleBackColor = true;
            this.btnEliminarRol.Click += new System.EventHandler(this.btnEliminarRol_Click);
            this.groupBoxPermisos.Controls.Add(this.lblAyuda);
            this.groupBoxPermisos.Controls.Add(this.tvPermisos);
            this.groupBoxPermisos.Controls.Add(this.btnGuardarPermisos);
            this.groupBoxPermisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPermisos.Location = new System.Drawing.Point(493, 28);
            this.groupBoxPermisos.Name = "groupBoxPermisos";
            this.groupBoxPermisos.Size = new System.Drawing.Size(760, 760);
            this.groupBoxPermisos.TabIndex = 1;
            this.groupBoxPermisos.TabStop = false;
            this.groupBoxPermisos.Text = "Formularios y permisos del rol";
            this.lblAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyuda.Location = new System.Drawing.Point(15, 34);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(730, 50);
            this.lblAyuda.TabIndex = 0;
            this.lblAyuda.Text = "Tildar un formulario = toda la página. Tildar solo botones = únicamente esos botones.";
            this.tvPermisos.CheckBoxes = true;
            this.tvPermisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvPermisos.Location = new System.Drawing.Point(15, 90);
            this.tvPermisos.Name = "tvPermisos";
            this.tvPermisos.Size = new System.Drawing.Size(730, 590);
            this.tvPermisos.TabIndex = 1;
            this.btnGuardarPermisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarPermisos.Location = new System.Drawing.Point(15, 692);
            this.btnGuardarPermisos.Name = "btnGuardarPermisos";
            this.btnGuardarPermisos.Size = new System.Drawing.Size(220, 52);
            this.btnGuardarPermisos.TabIndex = 2;
            this.btnGuardarPermisos.Text = "Guardar Permisos";
            this.btnGuardarPermisos.UseVisualStyleBackColor = true;
            this.btnGuardarPermisos.Click += new System.EventHandler(this.btnGuardarPermisos_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 832);
            this.Controls.Add(this.groupBoxPermisos);
            this.Controls.Add(this.groupBoxRoles);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "frmRolesPermisos";
            this.Text = "Gestión de Roles";
            this.Load += new System.EventHandler(this.frmRolesPermisos_Load);
            this.groupBoxRoles.ResumeLayout(false);
            this.groupBoxRoles.PerformLayout();
            this.groupBoxPermisos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRoles;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnNuevoRol;
        private System.Windows.Forms.Button btnModificarRol;
        private System.Windows.Forms.Button btnEliminarRol;
        private System.Windows.Forms.GroupBox groupBoxPermisos;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.TreeView tvPermisos;
        private System.Windows.Forms.Button btnGuardarPermisos;
    }
}
