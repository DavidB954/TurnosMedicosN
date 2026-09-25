namespace Vista
{
    partial class frmGestionMedicos
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
            this.groupBoxEspecialidades = new System.Windows.Forms.GroupBox();
            this.ucEspecialidadCrud = new Vista.ucSelectorEspecialidad();
            this.btnEspecialidadEliminar = new System.Windows.Forms.Button();
            this.btnEspecialidadCrear = new System.Windows.Forms.Button();
            this.txtNombreEspecialidad = new System.Windows.Forms.TextBox();
            this.lblNombreEspecialidad = new System.Windows.Forms.Label();
            this.groupBoxAsignacionEspecialidades = new System.Windows.Forms.GroupBox();
            this.btnQuitarEspecialidad = new System.Windows.Forms.Button();
            this.btnAsignarEspecialidad = new System.Windows.Forms.Button();
            this.ucEspecialidadAsignar = new Vista.ucSelectorEspecialidad();
            this.ucMedicoAsignar = new Vista.ucSelectorMedico();
            this.groupBoxHorariosMedicos = new System.Windows.Forms.GroupBox();
            this.ucMedicoHorario = new Vista.ucSelectorMedico();
            this.ucEspecialidadHorario = new Vista.ucSelectorEspecialidad();
            this.lblFranjaHoraria = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.lblTurnosCadaXMin = new System.Windows.Forms.Label();
            this.lblDiasTrabajados = new System.Windows.Forms.Label();
            this.checkBoxLunes = new System.Windows.Forms.CheckBox();
            this.checkBoxMartes = new System.Windows.Forms.CheckBox();
            this.checkBoxMiercoles = new System.Windows.Forms.CheckBox();
            this.checkBoxJueves = new System.Windows.Forms.CheckBox();
            this.checkBoxViernes = new System.Windows.Forms.CheckBox();
            this.checkBoxSabado = new System.Windows.Forms.CheckBox();
            this.btnAsignarDiaYHorario = new System.Windows.Forms.Button();
            this.btnModificarDiasYHorarios = new System.Windows.Forms.Button();
            this.btnEditarFranjas = new System.Windows.Forms.Button();
            this.dataGridViewMedicosDiasHorarios = new System.Windows.Forms.DataGridView();
            this.checkBoxMostrarInactivos = new System.Windows.Forms.CheckBox();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.txtHasta = new System.Windows.Forms.TextBox();
            this.txtTurnosCada = new System.Windows.Forms.TextBox();
            this.groupBoxEspecialidades.SuspendLayout();
            this.groupBoxAsignacionEspecialidades.SuspendLayout();
            this.groupBoxHorariosMedicos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMedicosDiasHorarios)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxEspecialidades
            //
            this.groupBoxEspecialidades.Controls.Add(this.ucEspecialidadCrud);
            this.groupBoxEspecialidades.Controls.Add(this.btnEspecialidadEliminar);
            this.groupBoxEspecialidades.Controls.Add(this.btnEspecialidadCrear);
            this.groupBoxEspecialidades.Controls.Add(this.txtNombreEspecialidad);
            this.groupBoxEspecialidades.Controls.Add(this.lblNombreEspecialidad);
            this.groupBoxEspecialidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxEspecialidades.Location = new System.Drawing.Point(365, 8);
            this.groupBoxEspecialidades.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxEspecialidades.Name = "groupBoxEspecialidades";
            this.groupBoxEspecialidades.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxEspecialidades.Size = new System.Drawing.Size(483, 161);
            this.groupBoxEspecialidades.TabIndex = 0;
            this.groupBoxEspecialidades.TabStop = false;
            this.groupBoxEspecialidades.Text = "Especialidades";
            // 
            // ucEspecialidadCrud
            //
            this.ucEspecialidadCrud.Etiqueta = "Especialidades:";
            this.ucEspecialidadCrud.Location = new System.Drawing.Point(11, 70);
            this.ucEspecialidadCrud.Margin = new System.Windows.Forms.Padding(2);
            this.ucEspecialidadCrud.Name = "ucEspecialidadCrud";
            this.ucEspecialidadCrud.Size = new System.Drawing.Size(460, 30);
            this.ucEspecialidadCrud.TabIndex = 4;
            //
            // btnEspecialidadEliminar
            // 
            this.btnEspecialidadEliminar.Location = new System.Drawing.Point(383, 116);
            this.btnEspecialidadEliminar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEspecialidadEliminar.Name = "btnEspecialidadEliminar";
            this.btnEspecialidadEliminar.Size = new System.Drawing.Size(96, 30);
            this.btnEspecialidadEliminar.TabIndex = 3;
            this.btnEspecialidadEliminar.Text = "Eliminar";
            this.btnEspecialidadEliminar.UseVisualStyleBackColor = true;
            this.btnEspecialidadEliminar.Click += new System.EventHandler(this.btnEspecialidadEliminar_Click);
            //
            // btnEspecialidadCrear
            // 
            this.btnEspecialidadCrear.Location = new System.Drawing.Point(270, 116);
            this.btnEspecialidadCrear.Margin = new System.Windows.Forms.Padding(2);
            this.btnEspecialidadCrear.Name = "btnEspecialidadCrear";
            this.btnEspecialidadCrear.Size = new System.Drawing.Size(91, 30);
            this.btnEspecialidadCrear.TabIndex = 2;
            this.btnEspecialidadCrear.Text = "Crear";
            this.btnEspecialidadCrear.UseVisualStyleBackColor = true;
            this.btnEspecialidadCrear.Click += new System.EventHandler(this.btnEspecialidadCrear_Click);
            // 
            // txtNombreEspecialidad
            // 
            this.txtNombreEspecialidad.Location = new System.Drawing.Point(270, 36);
            this.txtNombreEspecialidad.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombreEspecialidad.Name = "txtNombreEspecialidad";
            this.txtNombreEspecialidad.Size = new System.Drawing.Size(195, 26);
            this.txtNombreEspecialidad.TabIndex = 1;
            // 
            // lblNombreEspecialidad
            // 
            this.lblNombreEspecialidad.AutoSize = true;
            this.lblNombreEspecialidad.Location = new System.Drawing.Point(11, 35);
            this.lblNombreEspecialidad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreEspecialidad.Name = "lblNombreEspecialidad";
            this.lblNombreEspecialidad.Size = new System.Drawing.Size(208, 20);
            this.lblNombreEspecialidad.TabIndex = 0;
            this.lblNombreEspecialidad.Text = "Nombre de Especialidad:";
            // 
            // groupBoxAsignacionEspecialidades
            // 
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.btnQuitarEspecialidad);
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.btnAsignarEspecialidad);
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.ucEspecialidadAsignar);
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.ucMedicoAsignar);
            this.groupBoxAsignacionEspecialidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAsignacionEspecialidades.Location = new System.Drawing.Point(365, 192);
            this.groupBoxAsignacionEspecialidades.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxAsignacionEspecialidades.Name = "groupBoxAsignacionEspecialidades";
            this.groupBoxAsignacionEspecialidades.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxAsignacionEspecialidades.Size = new System.Drawing.Size(483, 194);
            this.groupBoxAsignacionEspecialidades.TabIndex = 1;
            this.groupBoxAsignacionEspecialidades.TabStop = false;
            this.groupBoxAsignacionEspecialidades.Text = "Asignacion de Especialidades";
            // 
            // btnQuitarEspecialidad
            // 
            this.btnQuitarEspecialidad.Location = new System.Drawing.Point(388, 144);
            this.btnQuitarEspecialidad.Name = "btnQuitarEspecialidad";
            this.btnQuitarEspecialidad.Size = new System.Drawing.Size(90, 32);
            this.btnQuitarEspecialidad.TabIndex = 5;
            this.btnQuitarEspecialidad.Text = "Quitar";
            this.btnQuitarEspecialidad.UseVisualStyleBackColor = true;
            this.btnQuitarEspecialidad.Click += new System.EventHandler(this.btnQuitarEspecialidad_Click);
            // 
            // btnAsignarEspecialidad
            // 
            this.btnAsignarEspecialidad.Location = new System.Drawing.Point(270, 144);
            this.btnAsignarEspecialidad.Name = "btnAsignarEspecialidad";
            this.btnAsignarEspecialidad.Size = new System.Drawing.Size(106, 32);
            this.btnAsignarEspecialidad.TabIndex = 4;
            this.btnAsignarEspecialidad.Text = "Asignar";
            this.btnAsignarEspecialidad.UseVisualStyleBackColor = true;
            this.btnAsignarEspecialidad.Click += new System.EventHandler(this.btnAsignarEspecialidad_Click);
            // 
            // ucEspecialidadAsignar
            //
            this.ucEspecialidadAsignar.Etiqueta = "Seleccione una Especialidad:";
            this.ucEspecialidadAsignar.Location = new System.Drawing.Point(14, 84);
            this.ucEspecialidadAsignar.Name = "ucEspecialidadAsignar";
            this.ucEspecialidadAsignar.Size = new System.Drawing.Size(460, 30);
            this.ucEspecialidadAsignar.TabIndex = 3;
            //
            // ucMedicoAsignar
            //
            this.ucMedicoAsignar.Etiqueta = "Seleccione un Medico:";
            this.ucMedicoAsignar.Location = new System.Drawing.Point(14, 35);
            this.ucMedicoAsignar.Margin = new System.Windows.Forms.Padding(2);
            this.ucMedicoAsignar.Name = "ucMedicoAsignar";
            this.ucMedicoAsignar.Size = new System.Drawing.Size(460, 30);
            this.ucMedicoAsignar.TabIndex = 2;
            //
            // groupBoxHorariosMedicos
            // 
            this.groupBoxHorariosMedicos.Controls.Add(this.txtTurnosCada);
            this.groupBoxHorariosMedicos.Controls.Add(this.txtHasta);
            this.groupBoxHorariosMedicos.Controls.Add(this.txtDesde);
            this.groupBoxHorariosMedicos.Controls.Add(this.btnModificarDiasYHorarios);
            this.groupBoxHorariosMedicos.Controls.Add(this.btnAsignarDiaYHorario);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxSabado);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxViernes);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxJueves);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxMiercoles);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxMartes);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxLunes);
            this.groupBoxHorariosMedicos.Controls.Add(this.lblDiasTrabajados);
            this.groupBoxHorariosMedicos.Controls.Add(this.lblTurnosCadaXMin);
            this.groupBoxHorariosMedicos.Controls.Add(this.lblHasta);
            this.groupBoxHorariosMedicos.Controls.Add(this.lblDesde);
            this.groupBoxHorariosMedicos.Controls.Add(this.lblFranjaHoraria);
            this.groupBoxHorariosMedicos.Controls.Add(this.ucEspecialidadHorario);
            this.groupBoxHorariosMedicos.Controls.Add(this.ucMedicoHorario);
            this.groupBoxHorariosMedicos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxHorariosMedicos.Location = new System.Drawing.Point(876, 13);
            this.groupBoxHorariosMedicos.Name = "groupBoxHorariosMedicos";
            this.groupBoxHorariosMedicos.Size = new System.Drawing.Size(395, 373);
            this.groupBoxHorariosMedicos.TabIndex = 2;
            this.groupBoxHorariosMedicos.TabStop = false;
            this.groupBoxHorariosMedicos.Text = "Asignacion de Horarios";
            // 
            // ucEspecialidadHorario
            // 
            this.ucEspecialidadHorario.Etiqueta = "Especialidad:";
            this.ucEspecialidadHorario.Location = new System.Drawing.Point(5, 206);
            this.ucEspecialidadHorario.Margin = new System.Windows.Forms.Padding(2);
            this.ucEspecialidadHorario.Name = "ucEspecialidadHorario";
            this.ucEspecialidadHorario.Size = new System.Drawing.Size(385, 30);
            this.ucEspecialidadHorario.TabIndex = 4;
            // 
            // ucMedicoHorario
            //
            this.ucMedicoHorario.Etiqueta = "Seleccione un Medico:";
            this.ucMedicoHorario.Location = new System.Drawing.Point(5, 26);
            this.ucMedicoHorario.Margin = new System.Windows.Forms.Padding(2);
            this.ucMedicoHorario.Name = "ucMedicoHorario";
            this.ucMedicoHorario.Size = new System.Drawing.Size(365, 30);
            this.ucMedicoHorario.TabIndex = 3;
            //
            // lblFranjaHoraria
            // 
            this.lblFranjaHoraria.AutoSize = true;
            this.lblFranjaHoraria.Location = new System.Drawing.Point(9, 86);
            this.lblFranjaHoraria.Name = "lblFranjaHoraria";
            this.lblFranjaHoraria.Size = new System.Drawing.Size(206, 20);
            this.lblFranjaHoraria.TabIndex = 5;
            this.lblFranjaHoraria.Text = "Ingrese la franja horaria:";
            this.lblFranjaHoraria.Click += new System.EventHandler(this.lblFranjaHoraria_Click);
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(35, 121);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(61, 20);
            this.lblDesde.TabIndex = 6;
            this.lblDesde.Text = "Desde";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(208, 121);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(57, 20);
            this.lblHasta.TabIndex = 7;
            this.lblHasta.Text = "Hasta";
            // 
            // lblTurnosCadaXMin
            // 
            this.lblTurnosCadaXMin.AutoSize = true;
            this.lblTurnosCadaXMin.Location = new System.Drawing.Point(35, 170);
            this.lblTurnosCadaXMin.Name = "lblTurnosCadaXMin";
            this.lblTurnosCadaXMin.Size = new System.Drawing.Size(113, 20);
            this.lblTurnosCadaXMin.TabIndex = 8;
            this.lblTurnosCadaXMin.Text = "Turnos cada:";
            // 
            // lblDiasTrabajados
            // 
            this.lblDiasTrabajados.AutoSize = true;
            this.lblDiasTrabajados.Location = new System.Drawing.Point(13, 248);
            this.lblDiasTrabajados.Name = "lblDiasTrabajados";
            this.lblDiasTrabajados.Size = new System.Drawing.Size(144, 20);
            this.lblDiasTrabajados.TabIndex = 9;
            this.lblDiasTrabajados.Text = "Dias Trabajados:";
            // 
            // checkBoxLunes
            // 
            this.checkBoxLunes.AutoSize = true;
            this.checkBoxLunes.Location = new System.Drawing.Point(17, 281);
            this.checkBoxLunes.Name = "checkBoxLunes";
            this.checkBoxLunes.Size = new System.Drawing.Size(38, 24);
            this.checkBoxLunes.TabIndex = 10;
            this.checkBoxLunes.Text = "L";
            this.checkBoxLunes.UseVisualStyleBackColor = true;
            // 
            // checkBoxMartes
            // 
            this.checkBoxMartes.AutoSize = true;
            this.checkBoxMartes.Location = new System.Drawing.Point(62, 281);
            this.checkBoxMartes.Name = "checkBoxMartes";
            this.checkBoxMartes.Size = new System.Drawing.Size(54, 24);
            this.checkBoxMartes.TabIndex = 11;
            this.checkBoxMartes.Text = "MA";
            this.checkBoxMartes.UseVisualStyleBackColor = true;
            // 
            // checkBoxMiercoles
            // 
            this.checkBoxMiercoles.AutoSize = true;
            this.checkBoxMiercoles.Location = new System.Drawing.Point(123, 281);
            this.checkBoxMiercoles.Name = "checkBoxMiercoles";
            this.checkBoxMiercoles.Size = new System.Drawing.Size(48, 24);
            this.checkBoxMiercoles.TabIndex = 12;
            this.checkBoxMiercoles.Text = "MI";
            this.checkBoxMiercoles.UseVisualStyleBackColor = true;
            // 
            // checkBoxJueves
            // 
            this.checkBoxJueves.AutoSize = true;
            this.checkBoxJueves.Location = new System.Drawing.Point(188, 281);
            this.checkBoxJueves.Name = "checkBoxJueves";
            this.checkBoxJueves.Size = new System.Drawing.Size(37, 24);
            this.checkBoxJueves.TabIndex = 13;
            this.checkBoxJueves.Text = "J";
            this.checkBoxJueves.UseVisualStyleBackColor = true;
            // 
            // checkBoxViernes
            // 
            this.checkBoxViernes.AutoSize = true;
            this.checkBoxViernes.Location = new System.Drawing.Point(231, 281);
            this.checkBoxViernes.Name = "checkBoxViernes";
            this.checkBoxViernes.Size = new System.Drawing.Size(40, 24);
            this.checkBoxViernes.TabIndex = 14;
            this.checkBoxViernes.Text = "V";
            this.checkBoxViernes.UseVisualStyleBackColor = true;
            // 
            // checkBoxSabado
            // 
            this.checkBoxSabado.AutoSize = true;
            this.checkBoxSabado.Location = new System.Drawing.Point(291, 281);
            this.checkBoxSabado.Name = "checkBoxSabado";
            this.checkBoxSabado.Size = new System.Drawing.Size(40, 24);
            this.checkBoxSabado.TabIndex = 15;
            this.checkBoxSabado.Text = "S";
            this.checkBoxSabado.UseVisualStyleBackColor = true;
            // 
            // btnAsignarDiaYHorario
            // 
            this.btnAsignarDiaYHorario.Location = new System.Drawing.Point(17, 323);
            this.btnAsignarDiaYHorario.Name = "btnAsignarDiaYHorario";
            this.btnAsignarDiaYHorario.Size = new System.Drawing.Size(225, 32);
            this.btnAsignarDiaYHorario.TabIndex = 16;
            this.btnAsignarDiaYHorario.Text = "Asignar Dias y Horarios";
            this.btnAsignarDiaYHorario.UseVisualStyleBackColor = true;
            this.btnAsignarDiaYHorario.Click += new System.EventHandler(this.btnAsignarDiaYHorario_Click);
            //
            // btnEditarFranjas
            //
            this.btnEditarFranjas.Location = new System.Drawing.Point(600, 396);
            this.btnEditarFranjas.Name = "btnEditarFranjas";
            this.btnEditarFranjas.Size = new System.Drawing.Size(265, 30);
            this.btnEditarFranjas.TabIndex = 18;
            this.btnEditarFranjas.Text = "Editar / inactivar franjas de un medico";
            this.btnEditarFranjas.UseVisualStyleBackColor = true;
            this.btnEditarFranjas.Click += new System.EventHandler(this.btnEditarFranjas_Click);
            //
            // btnModificarDiasYHorarios
            //
            this.btnModificarDiasYHorarios.Location = new System.Drawing.Point(256, 323);
            this.btnModificarDiasYHorarios.Name = "btnModificarDiasYHorarios";
            this.btnModificarDiasYHorarios.Size = new System.Drawing.Size(124, 32);
            this.btnModificarDiasYHorarios.TabIndex = 17;
            this.btnModificarDiasYHorarios.Text = "Modificar";
            this.btnModificarDiasYHorarios.UseVisualStyleBackColor = true;
            this.btnModificarDiasYHorarios.Click += new System.EventHandler(this.btnModificarDiasYHorarios_Click);
            // 
            // dataGridViewMedicosDiasHorarios
            //
            this.dataGridViewMedicosDiasHorarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMedicosDiasHorarios.Location = new System.Drawing.Point(365, 432);
            this.dataGridViewMedicosDiasHorarios.Name = "dataGridViewMedicosDiasHorarios";
            this.dataGridViewMedicosDiasHorarios.Size = new System.Drawing.Size(906, 127);
            this.dataGridViewMedicosDiasHorarios.TabIndex = 3;
            //
            // checkBoxMostrarInactivos
            //
            this.checkBoxMostrarInactivos.AutoSize = true;
            this.checkBoxMostrarInactivos.Location = new System.Drawing.Point(365, 404);
            this.checkBoxMostrarInactivos.Name = "checkBoxMostrarInactivos";
            this.checkBoxMostrarInactivos.Size = new System.Drawing.Size(220, 24);
            this.checkBoxMostrarInactivos.TabIndex = 21;
            this.checkBoxMostrarInactivos.Text = "Mostrar horarios inactivos";
            this.checkBoxMostrarInactivos.UseVisualStyleBackColor = true;
            this.checkBoxMostrarInactivos.CheckedChanged += new System.EventHandler(this.checkBoxMostrarInactivos_CheckedChanged);
            //
            // txtDesde
            // 
            this.txtDesde.Location = new System.Drawing.Point(102, 118);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(89, 26);
            this.txtDesde.TabIndex = 18;
            // 
            // txtHasta
            // 
            this.txtHasta.Location = new System.Drawing.Point(281, 118);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.Size = new System.Drawing.Size(100, 26);
            this.txtHasta.TabIndex = 19;
            // 
            // txtTurnosCada
            // 
            this.txtTurnosCada.Location = new System.Drawing.Point(154, 167);
            this.txtTurnosCada.Name = "txtTurnosCada";
            this.txtTurnosCada.Size = new System.Drawing.Size(100, 26);
            this.txtTurnosCada.TabIndex = 20;
            // 
            // frmGestionMedicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 575);
            this.Controls.Add(this.dataGridViewMedicosDiasHorarios);
            this.Controls.Add(this.btnEditarFranjas);
            this.Controls.Add(this.checkBoxMostrarInactivos);
            this.Controls.Add(this.groupBoxHorariosMedicos);
            this.Controls.Add(this.groupBoxAsignacionEspecialidades);
            this.Controls.Add(this.groupBoxEspecialidades);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmGestionMedicos";
            this.Text = "frmGestionMedicos";
            this.Load += new System.EventHandler(this.frmGestionMedicos_Load);
            this.groupBoxEspecialidades.ResumeLayout(false);
            this.groupBoxEspecialidades.PerformLayout();
            this.groupBoxAsignacionEspecialidades.ResumeLayout(false);
            this.groupBoxAsignacionEspecialidades.PerformLayout();
            this.groupBoxHorariosMedicos.ResumeLayout(false);
            this.groupBoxHorariosMedicos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMedicosDiasHorarios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxEspecialidades;
        private System.Windows.Forms.Label lblNombreEspecialidad;
        private ucSelectorEspecialidad ucEspecialidadCrud;
        private System.Windows.Forms.Button btnEspecialidadEliminar;
        private System.Windows.Forms.Button btnEspecialidadCrear;
        private System.Windows.Forms.TextBox txtNombreEspecialidad;
        private System.Windows.Forms.GroupBox groupBoxAsignacionEspecialidades;
        private ucSelectorEspecialidad ucEspecialidadAsignar;
        private ucSelectorMedico ucMedicoAsignar;
        private System.Windows.Forms.Button btnQuitarEspecialidad;
        private System.Windows.Forms.Button btnAsignarEspecialidad;
        private System.Windows.Forms.GroupBox groupBoxHorariosMedicos;
        private ucSelectorMedico ucMedicoHorario;
        private ucSelectorEspecialidad ucEspecialidadHorario;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label lblFranjaHoraria;
        private System.Windows.Forms.CheckBox checkBoxJueves;
        private System.Windows.Forms.CheckBox checkBoxMiercoles;
        private System.Windows.Forms.CheckBox checkBoxMartes;
        private System.Windows.Forms.CheckBox checkBoxLunes;
        private System.Windows.Forms.Label lblDiasTrabajados;
        private System.Windows.Forms.Label lblTurnosCadaXMin;
        private System.Windows.Forms.Button btnModificarDiasYHorarios;
        private System.Windows.Forms.Button btnEditarFranjas;
        private System.Windows.Forms.Button btnAsignarDiaYHorario;
        private System.Windows.Forms.CheckBox checkBoxSabado;
        private System.Windows.Forms.CheckBox checkBoxViernes;
        private System.Windows.Forms.DataGridView dataGridViewMedicosDiasHorarios;
        private System.Windows.Forms.CheckBox checkBoxMostrarInactivos;
        private System.Windows.Forms.TextBox txtTurnosCada;
        private System.Windows.Forms.TextBox txtHasta;
        private System.Windows.Forms.TextBox txtDesde;
    }
}