namespace Vista
{
    partial class frm_AccesoMedico
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
            this.groupBox_AtencionMedica = new System.Windows.Forms.GroupBox();
            this.txtMedicamento = new System.Windows.Forms.TextBox();
            this.lblMedicamento = new System.Windows.Forms.Label();
            this.btnCancelarAtencionMedica = new System.Windows.Forms.Button();
            this.btnAtencionMedica = new System.Windows.Forms.Button();
            this.txtIndicaciones = new System.Windows.Forms.TextBox();
            this.lblIndicaciones = new System.Windows.Forms.Label();
            this.rb_RecetaNO = new System.Windows.Forms.RadioButton();
            this.rb_RecetaSI = new System.Windows.Forms.RadioButton();
            this.lbl_Receta = new System.Windows.Forms.Label();
            this.txtMotivoConsulta = new System.Windows.Forms.TextBox();
            this.txtPacienteSeleccionado = new System.Windows.Forms.TextBox();
            this.lbl_MotivoConsulta = new System.Windows.Forms.Label();
            this.lbl_NombrePaciente = new System.Windows.Forms.Label();
            this.dgv_PacientesDelDia = new System.Windows.Forms.DataGridView();
            this.groupBox_PacientesDelDia = new System.Windows.Forms.GroupBox();
            this.groupBox_HistoriaClinina = new System.Windows.Forms.GroupBox();
            this.lstHistoriaClinica = new System.Windows.Forms.ListBox();
            this.groupBox_AtencionMedica.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PacientesDelDia)).BeginInit();
            this.groupBox_PacientesDelDia.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_AtencionMedica
            // 
            this.groupBox_AtencionMedica.Controls.Add(this.txtMedicamento);
            this.groupBox_AtencionMedica.Controls.Add(this.lblMedicamento);
            this.groupBox_AtencionMedica.Controls.Add(this.btnCancelarAtencionMedica);
            this.groupBox_AtencionMedica.Controls.Add(this.btnAtencionMedica);
            this.groupBox_AtencionMedica.Controls.Add(this.txtIndicaciones);
            this.groupBox_AtencionMedica.Controls.Add(this.lblIndicaciones);
            this.groupBox_AtencionMedica.Controls.Add(this.rb_RecetaNO);
            this.groupBox_AtencionMedica.Controls.Add(this.rb_RecetaSI);
            this.groupBox_AtencionMedica.Controls.Add(this.lbl_Receta);
            this.groupBox_AtencionMedica.Controls.Add(this.txtMotivoConsulta);
            this.groupBox_AtencionMedica.Controls.Add(this.txtPacienteSeleccionado);
            this.groupBox_AtencionMedica.Controls.Add(this.lbl_MotivoConsulta);
            this.groupBox_AtencionMedica.Controls.Add(this.lbl_NombrePaciente);
            this.groupBox_AtencionMedica.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_AtencionMedica.Location = new System.Drawing.Point(880, 14);
            this.groupBox_AtencionMedica.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_AtencionMedica.Name = "groupBox_AtencionMedica";
            this.groupBox_AtencionMedica.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_AtencionMedica.Size = new System.Drawing.Size(355, 519);
            this.groupBox_AtencionMedica.TabIndex = 1;
            this.groupBox_AtencionMedica.TabStop = false;
            this.groupBox_AtencionMedica.Text = "Atencion Medica";
            // 
            // txtMedicamento
            // 
            this.txtMedicamento.Location = new System.Drawing.Point(175, 273);
            this.txtMedicamento.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMedicamento.Name = "txtMedicamento";
            this.txtMedicamento.Size = new System.Drawing.Size(169, 23);
            this.txtMedicamento.TabIndex = 12;
            // 
            // lblMedicamento
            // 
            this.lblMedicamento.AutoSize = true;
            this.lblMedicamento.Location = new System.Drawing.Point(17, 273);
            this.lblMedicamento.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMedicamento.Name = "lblMedicamento";
            this.lblMedicamento.Size = new System.Drawing.Size(116, 17);
            this.lblMedicamento.TabIndex = 11;
            this.lblMedicamento.Text = "Medicamentos:";
            // 
            // btnCancelarAtencionMedica
            // 
            this.btnCancelarAtencionMedica.Location = new System.Drawing.Point(255, 466);
            this.btnCancelarAtencionMedica.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancelarAtencionMedica.Name = "btnCancelarAtencionMedica";
            this.btnCancelarAtencionMedica.Size = new System.Drawing.Size(88, 31);
            this.btnCancelarAtencionMedica.TabIndex = 10;
            this.btnCancelarAtencionMedica.Text = "Cancelar";
            this.btnCancelarAtencionMedica.UseVisualStyleBackColor = true;
            // 
            // btnAtencionMedica
            // 
            this.btnAtencionMedica.Location = new System.Drawing.Point(88, 466);
            this.btnAtencionMedica.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAtencionMedica.Name = "btnAtencionMedica";
            this.btnAtencionMedica.Size = new System.Drawing.Size(125, 31);
            this.btnAtencionMedica.TabIndex = 9;
            this.btnAtencionMedica.Text = "Emitir Atencion";
            this.btnAtencionMedica.UseVisualStyleBackColor = true;
            // 
            // txtIndicaciones
            // 
            this.txtIndicaciones.Location = new System.Drawing.Point(175, 333);
            this.txtIndicaciones.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtIndicaciones.Multiline = true;
            this.txtIndicaciones.Name = "txtIndicaciones";
            this.txtIndicaciones.Size = new System.Drawing.Size(169, 114);
            this.txtIndicaciones.TabIndex = 8;
            // 
            // lblIndicaciones
            // 
            this.lblIndicaciones.AutoSize = true;
            this.lblIndicaciones.Location = new System.Drawing.Point(17, 335);
            this.lblIndicaciones.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIndicaciones.Name = "lblIndicaciones";
            this.lblIndicaciones.Size = new System.Drawing.Size(103, 17);
            this.lblIndicaciones.TabIndex = 7;
            this.lblIndicaciones.Text = "Indicaciones:";
            // 
            // rb_RecetaNO
            // 
            this.rb_RecetaNO.AutoSize = true;
            this.rb_RecetaNO.Location = new System.Drawing.Point(237, 206);
            this.rb_RecetaNO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb_RecetaNO.Name = "rb_RecetaNO";
            this.rb_RecetaNO.Size = new System.Drawing.Size(46, 21);
            this.rb_RecetaNO.TabIndex = 6;
            this.rb_RecetaNO.TabStop = true;
            this.rb_RecetaNO.Text = "No";
            this.rb_RecetaNO.UseVisualStyleBackColor = true;
            // 
            // rb_RecetaSI
            // 
            this.rb_RecetaSI.AutoSize = true;
            this.rb_RecetaSI.Location = new System.Drawing.Point(175, 206);
            this.rb_RecetaSI.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb_RecetaSI.Name = "rb_RecetaSI";
            this.rb_RecetaSI.Size = new System.Drawing.Size(40, 21);
            this.rb_RecetaSI.TabIndex = 5;
            this.rb_RecetaSI.TabStop = true;
            this.rb_RecetaSI.Text = "Si";
            this.rb_RecetaSI.UseVisualStyleBackColor = true;
            // 
            // lbl_Receta
            // 
            this.lbl_Receta.AutoSize = true;
            this.lbl_Receta.Location = new System.Drawing.Point(17, 207);
            this.lbl_Receta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Receta.Name = "lbl_Receta";
            this.lbl_Receta.Size = new System.Drawing.Size(64, 17);
            this.lbl_Receta.TabIndex = 4;
            this.lbl_Receta.Text = "Receta:";
            // 
            // txtMotivoConsulta
            // 
            this.txtMotivoConsulta.Location = new System.Drawing.Point(175, 67);
            this.txtMotivoConsulta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMotivoConsulta.Multiline = true;
            this.txtMotivoConsulta.Name = "txtMotivoConsulta";
            this.txtMotivoConsulta.Size = new System.Drawing.Size(169, 114);
            this.txtMotivoConsulta.TabIndex = 3;
            // 
            // txtPacienteSeleccionado
            // 
            this.txtPacienteSeleccionado.Location = new System.Drawing.Point(175, 28);
            this.txtPacienteSeleccionado.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPacienteSeleccionado.Name = "txtPacienteSeleccionado";
            this.txtPacienteSeleccionado.ReadOnly = true;
            this.txtPacienteSeleccionado.Size = new System.Drawing.Size(169, 23);
            this.txtPacienteSeleccionado.TabIndex = 2;
            // 
            // lbl_MotivoConsulta
            // 
            this.lbl_MotivoConsulta.AutoSize = true;
            this.lbl_MotivoConsulta.Location = new System.Drawing.Point(17, 69);
            this.lbl_MotivoConsulta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_MotivoConsulta.Name = "lbl_MotivoConsulta";
            this.lbl_MotivoConsulta.Size = new System.Drawing.Size(151, 17);
            this.lbl_MotivoConsulta.TabIndex = 1;
            this.lbl_MotivoConsulta.Text = "Motivo de Consulta:";
            // 
            // lbl_NombrePaciente
            // 
            this.lbl_NombrePaciente.AutoSize = true;
            this.lbl_NombrePaciente.Location = new System.Drawing.Point(17, 28);
            this.lbl_NombrePaciente.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_NombrePaciente.Name = "lbl_NombrePaciente";
            this.lbl_NombrePaciente.Size = new System.Drawing.Size(76, 17);
            this.lbl_NombrePaciente.TabIndex = 0;
            this.lbl_NombrePaciente.Text = "Paciente:";
            // 
            // dgv_PacientesDelDia
            // 
            this.dgv_PacientesDelDia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_PacientesDelDia.Location = new System.Drawing.Point(15, 28);
            this.dgv_PacientesDelDia.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgv_PacientesDelDia.Name = "dgv_PacientesDelDia";
            this.dgv_PacientesDelDia.RowHeadersWidth = 62;
            this.dgv_PacientesDelDia.RowTemplate.Height = 28;
            this.dgv_PacientesDelDia.Size = new System.Drawing.Size(596, 225);
            this.dgv_PacientesDelDia.TabIndex = 0;
            // 
            // groupBox_PacientesDelDia
            // 
            this.groupBox_PacientesDelDia.Controls.Add(this.dgv_PacientesDelDia);
            this.groupBox_PacientesDelDia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_PacientesDelDia.Location = new System.Drawing.Point(230, 14);
            this.groupBox_PacientesDelDia.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_PacientesDelDia.Name = "groupBox_PacientesDelDia";
            this.groupBox_PacientesDelDia.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_PacientesDelDia.Size = new System.Drawing.Size(634, 281);
            this.groupBox_PacientesDelDia.TabIndex = 0;
            this.groupBox_PacientesDelDia.TabStop = false;
            this.groupBox_PacientesDelDia.Text = "Pacientes del dia";
            // 
            // groupBox_HistoriaClinina
            //
            this.groupBox_HistoriaClinina.Controls.Add(this.lstHistoriaClinica);
            this.groupBox_HistoriaClinina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_HistoriaClinina.Location = new System.Drawing.Point(230, 301);
            this.groupBox_HistoriaClinina.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_HistoriaClinina.Name = "groupBox_HistoriaClinina";
            this.groupBox_HistoriaClinina.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_HistoriaClinina.Size = new System.Drawing.Size(634, 233);
            this.groupBox_HistoriaClinina.TabIndex = 1;
            this.groupBox_HistoriaClinina.TabStop = false;
            this.groupBox_HistoriaClinina.Text = "Historia Clinica";
            //
            // lstHistoriaClinica
            //
            this.lstHistoriaClinica.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstHistoriaClinica.FormattingEnabled = true;
            this.lstHistoriaClinica.ItemHeight = 16;
            this.lstHistoriaClinica.Location = new System.Drawing.Point(15, 28);
            this.lstHistoriaClinica.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstHistoriaClinica.Name = "lstHistoriaClinica";
            this.lstHistoriaClinica.Size = new System.Drawing.Size(604, 196);
            this.lstHistoriaClinica.TabIndex = 0;
            //
            // frm_AccesoMedico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 541);
            this.Controls.Add(this.groupBox_HistoriaClinina);
            this.Controls.Add(this.groupBox_AtencionMedica);
            this.Controls.Add(this.groupBox_PacientesDelDia);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frm_AccesoMedico";
            this.Text = "frm_AccesoMedico";
            this.Load += new System.EventHandler(this.frm_AccesoMedico_Load);
            this.groupBox_AtencionMedica.ResumeLayout(false);
            this.groupBox_AtencionMedica.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PacientesDelDia)).EndInit();
            this.groupBox_PacientesDelDia.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox_AtencionMedica;
        private System.Windows.Forms.RadioButton rb_RecetaNO;
        private System.Windows.Forms.RadioButton rb_RecetaSI;
        private System.Windows.Forms.Label lbl_Receta;
        private System.Windows.Forms.TextBox txtMotivoConsulta;
        private System.Windows.Forms.TextBox txtPacienteSeleccionado;
        private System.Windows.Forms.Label lbl_MotivoConsulta;
        private System.Windows.Forms.Label lbl_NombrePaciente;
        private System.Windows.Forms.Button btnCancelarAtencionMedica;
        private System.Windows.Forms.Button btnAtencionMedica;
        private System.Windows.Forms.TextBox txtIndicaciones;
        private System.Windows.Forms.Label lblIndicaciones;
        private System.Windows.Forms.TextBox txtMedicamento;
        private System.Windows.Forms.Label lblMedicamento;
        private System.Windows.Forms.DataGridView dgv_PacientesDelDia;
        private System.Windows.Forms.GroupBox groupBox_PacientesDelDia;
        private System.Windows.Forms.GroupBox groupBox_HistoriaClinina;
        private System.Windows.Forms.ListBox lstHistoriaClinica;
    }
}