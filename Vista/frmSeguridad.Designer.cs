namespace Vista
{
    partial class frmSeguridad
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
            this.groupBox_FilasCorruptas = new System.Windows.Forms.GroupBox();
            this.dgvSeguridad = new System.Windows.Forms.DataGridView();
            this.btnBloquearUsuYRecalcular = new System.Windows.Forms.Button();
            this.btnRestaurarBD = new System.Windows.Forms.Button();
            this.btnBackUp = new System.Windows.Forms.Button();
            this.groupBox_Opciones = new System.Windows.Forms.GroupBox();
            this.groupBox_FilasCorruptas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeguridad)).BeginInit();
            this.groupBox_Opciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_FilasCorruptas
            // 
            this.groupBox_FilasCorruptas.Controls.Add(this.dgvSeguridad);
            this.groupBox_FilasCorruptas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_FilasCorruptas.Location = new System.Drawing.Point(251, 18);
            this.groupBox_FilasCorruptas.Name = "groupBox_FilasCorruptas";
            this.groupBox_FilasCorruptas.Size = new System.Drawing.Size(541, 249);
            this.groupBox_FilasCorruptas.TabIndex = 2;
            this.groupBox_FilasCorruptas.TabStop = false;
            this.groupBox_FilasCorruptas.Text = "Filas Corrompidas";
            // 
            // dgvSeguridad
            // 
            this.dgvSeguridad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSeguridad.Location = new System.Drawing.Point(6, 38);
            this.dgvSeguridad.Name = "dgvSeguridad";
            this.dgvSeguridad.RowHeadersWidth = 62;
            this.dgvSeguridad.Size = new System.Drawing.Size(524, 182);
            this.dgvSeguridad.TabIndex = 0;
            // 
            // btnBloquearUsuYRecalcular
            // 
            this.btnBloquearUsuYRecalcular.Location = new System.Drawing.Point(373, 46);
            this.btnBloquearUsuYRecalcular.Name = "btnBloquearUsuYRecalcular";
            this.btnBloquearUsuYRecalcular.Size = new System.Drawing.Size(157, 54);
            this.btnBloquearUsuYRecalcular.TabIndex = 7;
            this.btnBloquearUsuYRecalcular.Text = "Bloquear Usuario y Recalcular DV";
            this.btnBloquearUsuYRecalcular.UseVisualStyleBackColor = true;
            this.btnBloquearUsuYRecalcular.Click += new System.EventHandler(this.btnBloquearUsuYRecalcular_Click);
            // 
            // btnRestaurarBD
            // 
            this.btnRestaurarBD.Location = new System.Drawing.Point(189, 46);
            this.btnRestaurarBD.Name = "btnRestaurarBD";
            this.btnRestaurarBD.Size = new System.Drawing.Size(152, 54);
            this.btnRestaurarBD.TabIndex = 6;
            this.btnRestaurarBD.Text = "Restaurar BD";
            this.btnRestaurarBD.UseVisualStyleBackColor = true;
            this.btnRestaurarBD.Click += new System.EventHandler(this.btnRestaurarBD_Click);
            // 
            // btnBackUp
            // 
            this.btnBackUp.Location = new System.Drawing.Point(6, 46);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Size = new System.Drawing.Size(153, 54);
            this.btnBackUp.TabIndex = 5;
            this.btnBackUp.Text = "Generar Back Up";
            this.btnBackUp.UseVisualStyleBackColor = true;
            this.btnBackUp.Click += new System.EventHandler(this.btnBackUp_Click);
            // 
            // groupBox_Opciones
            // 
            this.groupBox_Opciones.Controls.Add(this.btnBackUp);
            this.groupBox_Opciones.Controls.Add(this.btnBloquearUsuYRecalcular);
            this.groupBox_Opciones.Controls.Add(this.btnRestaurarBD);
            this.groupBox_Opciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Opciones.Location = new System.Drawing.Point(251, 273);
            this.groupBox_Opciones.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_Opciones.Name = "groupBox_Opciones";
            this.groupBox_Opciones.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_Opciones.Size = new System.Drawing.Size(541, 140);
            this.groupBox_Opciones.TabIndex = 8;
            this.groupBox_Opciones.TabStop = false;
            this.groupBox_Opciones.Text = "Opciones";
            // 
            // frm_Seguridad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 541);
            this.Controls.Add(this.groupBox_Opciones);
            this.Controls.Add(this.groupBox_FilasCorruptas);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frm_Seguridad";
            this.Text = "frm_Seguridad";
            this.Load += new System.EventHandler(this.frm_Seguridad_Load);
            this.groupBox_FilasCorruptas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeguridad)).EndInit();
            this.groupBox_Opciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_FilasCorruptas;
        private System.Windows.Forms.DataGridView dgvSeguridad;
        private System.Windows.Forms.Button btnBloquearUsuYRecalcular;
        private System.Windows.Forms.Button btnRestaurarBD;
        private System.Windows.Forms.Button btnBackUp;
        private System.Windows.Forms.GroupBox groupBox_Opciones;
    }
}