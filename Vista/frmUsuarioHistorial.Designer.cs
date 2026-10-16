namespace Vista
{
    partial class frmUsuarioHistorial
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
            this.GroupBoxUsuarioHistorial = new System.Windows.Forms.GroupBox();
            this.lbl_Usuario = new System.Windows.Forms.Label();
            this.cboUsuario_Historial = new System.Windows.Forms.ComboBox();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.GroupBoxUsuarioHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            //
            // GroupBoxUsuarioHistorial
            //
            this.GroupBoxUsuarioHistorial.Controls.Add(this.lbl_Usuario);
            this.GroupBoxUsuarioHistorial.Controls.Add(this.cboUsuario_Historial);
            this.GroupBoxUsuarioHistorial.Controls.Add(this.btnRestaurar);
            this.GroupBoxUsuarioHistorial.Controls.Add(this.dgvHistorial);
            this.GroupBoxUsuarioHistorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBoxUsuarioHistorial.Location = new System.Drawing.Point(327, 28);
            this.GroupBoxUsuarioHistorial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBoxUsuarioHistorial.Name = "GroupBoxUsuarioHistorial";
            this.GroupBoxUsuarioHistorial.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBoxUsuarioHistorial.Size = new System.Drawing.Size(1545, 760);
            this.GroupBoxUsuarioHistorial.TabIndex = 0;
            this.GroupBoxUsuarioHistorial.TabStop = false;
            this.GroupBoxUsuarioHistorial.Text = "Historial";
            //
            // lbl_Usuario
            //
            this.lbl_Usuario.AutoSize = true;
            this.lbl_Usuario.Location = new System.Drawing.Point(15, 40);
            this.lbl_Usuario.Name = "lbl_Usuario";
            this.lbl_Usuario.Size = new System.Drawing.Size(73, 25);
            this.lbl_Usuario.TabIndex = 3;
            this.lbl_Usuario.Text = "Usuario:";
            //
            // cboUsuario_Historial
            //
            this.cboUsuario_Historial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsuario_Historial.FormattingEnabled = true;
            this.cboUsuario_Historial.Location = new System.Drawing.Point(120, 36);
            this.cboUsuario_Historial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboUsuario_Historial.Name = "cboUsuario_Historial";
            this.cboUsuario_Historial.Size = new System.Drawing.Size(360, 33);
            this.cboUsuario_Historial.TabIndex = 0;
            this.cboUsuario_Historial.SelectedIndexChanged += new System.EventHandler(this.cboUsuario_Historial_SelectedIndexChanged);
            //
            // btnRestaurar
            //
            this.btnRestaurar.Location = new System.Drawing.Point(500, 28);
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
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Location = new System.Drawing.Point(15, 95);
            this.dgvHistorial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersWidth = 62;
            this.dgvHistorial.Size = new System.Drawing.Size(1515, 645);
            this.dgvHistorial.TabIndex = 2;
            //
            // frmUsuarioHistorial
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 832);
            this.Controls.Add(this.GroupBoxUsuarioHistorial);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "frmUsuarioHistorial";
            this.Text = "Historial de Usuarios";
            this.Load += new System.EventHandler(this.frmUsuarioHistorial_Load);
            this.GroupBoxUsuarioHistorial.ResumeLayout(false);
            this.GroupBoxUsuarioHistorial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxUsuarioHistorial;
        private System.Windows.Forms.Label lbl_Usuario;
        private System.Windows.Forms.ComboBox cboUsuario_Historial;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.DataGridView dgvHistorial;
    }
}
