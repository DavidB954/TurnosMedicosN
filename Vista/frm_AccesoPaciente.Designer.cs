namespace Vista
{
    partial class frm_AccesoPaciente
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
            this.groupBox_Disponibilidad = new System.Windows.Forms.GroupBox();
            this.btnReservarTurno = new System.Windows.Forms.Button();
            this.lblHorarios = new System.Windows.Forms.Label();
            this.cboMedicos = new System.Windows.Forms.ComboBox();
            this.lblMedico = new System.Windows.Forms.Label();
            this.cboEspecialidades = new System.Windows.Forms.ComboBox();
            this.lblEspecialidad = new System.Windows.Forms.Label();
            this.groupBox_MisTurnos = new System.Windows.Forms.GroupBox();
            this.btnCancelarTurnos = new System.Windows.Forms.Button();
            this.btnModificarTurno = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lstHorarios = new System.Windows.Forms.ListBox();
            this.groupBox_Disponibilidad.SuspendLayout();
            this.groupBox_MisTurnos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_Disponibilidad
            // 
            this.groupBox_Disponibilidad.Controls.Add(this.btnReservarTurno);
            this.groupBox_Disponibilidad.Controls.Add(this.lstHorarios);
            this.groupBox_Disponibilidad.Controls.Add(this.lblHorarios);
            this.groupBox_Disponibilidad.Controls.Add(this.cboMedicos);
            this.groupBox_Disponibilidad.Controls.Add(this.lblMedico);
            this.groupBox_Disponibilidad.Controls.Add(this.cboEspecialidades);
            this.groupBox_Disponibilidad.Controls.Add(this.lblEspecialidad);
            this.groupBox_Disponibilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Disponibilidad.Location = new System.Drawing.Point(329, 8);
            this.groupBox_Disponibilidad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_Disponibilidad.Name = "groupBox_Disponibilidad";
            this.groupBox_Disponibilidad.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_Disponibilidad.Size = new System.Drawing.Size(910, 200);
            this.groupBox_Disponibilidad.TabIndex = 0;
            this.groupBox_Disponibilidad.TabStop = false;
            this.groupBox_Disponibilidad.Text = "Buscar Disponibilidad";
            // 
            // btnReservarTurno
            // 
            this.btnReservarTurno.Location = new System.Drawing.Point(781, 153);
            this.btnReservarTurno.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnReservarTurno.Name = "btnReservarTurno";
            this.btnReservarTurno.Size = new System.Drawing.Size(125, 31);
            this.btnReservarTurno.TabIndex = 5;
            this.btnReservarTurno.Text = "Reservar";
            this.btnReservarTurno.UseVisualStyleBackColor = true;
            // 
            // lblHorarios
            // 
            this.lblHorarios.AutoSize = true;
            this.lblHorarios.Location = new System.Drawing.Point(414, 39);
            this.lblHorarios.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHorarios.Name = "lblHorarios";
            this.lblHorarios.Size = new System.Drawing.Size(77, 20);
            this.lblHorarios.TabIndex = 4;
            this.lblHorarios.Text = "Horarios";
            // 
            // cboMedicos
            // 
            this.cboMedicos.FormattingEnabled = true;
            this.cboMedicos.Location = new System.Drawing.Point(197, 88);
            this.cboMedicos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboMedicos.Name = "cboMedicos";
            this.cboMedicos.Size = new System.Drawing.Size(163, 28);
            this.cboMedicos.TabIndex = 3;
            // 
            // lblMedico
            // 
            this.lblMedico.AutoSize = true;
            this.lblMedico.Location = new System.Drawing.Point(47, 91);
            this.lblMedico.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMedico.Name = "lblMedico";
            this.lblMedico.Size = new System.Drawing.Size(66, 20);
            this.lblMedico.TabIndex = 2;
            this.lblMedico.Text = "Medico";
            // 
            // cboEspecialidades
            // 
            this.cboEspecialidades.FormattingEnabled = true;
            this.cboEspecialidades.Location = new System.Drawing.Point(197, 39);
            this.cboEspecialidades.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboEspecialidades.Name = "cboEspecialidades";
            this.cboEspecialidades.Size = new System.Drawing.Size(163, 28);
            this.cboEspecialidades.TabIndex = 1;
            // 
            // lblEspecialidad
            // 
            this.lblEspecialidad.AutoSize = true;
            this.lblEspecialidad.Location = new System.Drawing.Point(47, 42);
            this.lblEspecialidad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.Size = new System.Drawing.Size(130, 20);
            this.lblEspecialidad.TabIndex = 0;
            this.lblEspecialidad.Text = "Especialidades";
            //
            // lstHorarios
            //
            this.lstHorarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstHorarios.FormattingEnabled = true;
            this.lstHorarios.ItemHeight = 16;
            this.lstHorarios.Location = new System.Drawing.Point(414, 65);
            this.lstHorarios.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstHorarios.Name = "lstHorarios";
            this.lstHorarios.Size = new System.Drawing.Size(150, 116);
            this.lstHorarios.TabIndex = 6;
            //
            // groupBox_MisTurnos
            // 
            this.groupBox_MisTurnos.Controls.Add(this.btnCancelarTurnos);
            this.groupBox_MisTurnos.Controls.Add(this.btnModificarTurno);
            this.groupBox_MisTurnos.Controls.Add(this.dataGridView1);
            this.groupBox_MisTurnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_MisTurnos.Location = new System.Drawing.Point(329, 219);
            this.groupBox_MisTurnos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_MisTurnos.Name = "groupBox_MisTurnos";
            this.groupBox_MisTurnos.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_MisTurnos.Size = new System.Drawing.Size(910, 315);
            this.groupBox_MisTurnos.TabIndex = 7;
            this.groupBox_MisTurnos.TabStop = false;
            this.groupBox_MisTurnos.Text = "Mis Turnos";
            // 
            // btnCancelarTurnos
            // 
            this.btnCancelarTurnos.Location = new System.Drawing.Point(781, 262);
            this.btnCancelarTurnos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancelarTurnos.Name = "btnCancelarTurnos";
            this.btnCancelarTurnos.Size = new System.Drawing.Size(125, 31);
            this.btnCancelarTurnos.TabIndex = 8;
            this.btnCancelarTurnos.Text = "Cancelar";
            this.btnCancelarTurnos.UseVisualStyleBackColor = true;
            // 
            // btnModificarTurno
            // 
            this.btnModificarTurno.Location = new System.Drawing.Point(634, 262);
            this.btnModificarTurno.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnModificarTurno.Name = "btnModificarTurno";
            this.btnModificarTurno.Size = new System.Drawing.Size(125, 31);
            this.btnModificarTurno.TabIndex = 7;
            this.btnModificarTurno.Text = "Modificar";
            this.btnModificarTurno.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 38);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(868, 208);
            this.dataGridView1.TabIndex = 0;
            // 
            // frm_AccesoPaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 541);
            this.Controls.Add(this.groupBox_MisTurnos);
            this.Controls.Add(this.groupBox_Disponibilidad);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frm_AccesoPaciente";
            this.Text = "frm_AccesoPaciente";
            this.Load += new System.EventHandler(this.frm_AccesoPaciente_Load);
            this.groupBox_Disponibilidad.ResumeLayout(false);
            this.groupBox_Disponibilidad.PerformLayout();
            this.groupBox_MisTurnos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Disponibilidad;
        private System.Windows.Forms.Label lblHorarios;
        private System.Windows.Forms.ComboBox cboMedicos;
        private System.Windows.Forms.Label lblMedico;
        private System.Windows.Forms.ComboBox cboEspecialidades;
        private System.Windows.Forms.Label lblEspecialidad;
        private System.Windows.Forms.Button btnReservarTurno;
        private System.Windows.Forms.GroupBox groupBox_MisTurnos;
        private System.Windows.Forms.Button btnCancelarTurnos;
        private System.Windows.Forms.Button btnModificarTurno;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListBox lstHorarios;
    }
}