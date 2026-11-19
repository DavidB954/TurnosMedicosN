namespace Vista
{
    partial class frmGestionMedicos
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
            this.groupBoxEspecialidades = new System.Windows.Forms.GroupBox();
            this.lblNombreEspecialidad = new System.Windows.Forms.Label();
            this.txtNombreEspecialidad = new System.Windows.Forms.TextBox();
            this.btnEspecialidadCrear = new System.Windows.Forms.Button();
            this.ucEspecialidadCrud = new Vista.ucSelectorEspecialidad();
            this.btnEspecialidadEliminar = new System.Windows.Forms.Button();
            this.groupBoxAsignacionEspecialidades = new System.Windows.Forms.GroupBox();
            this.lblMedicoAsignar = new System.Windows.Forms.Label();
            this.ucMedicoAsignar = new Vista.ucSelectorMedico();
            this.lblMatricula = new System.Windows.Forms.Label();
            this.txtMatricula = new System.Windows.Forms.TextBox();
            this.btnGuardarMatricula = new System.Windows.Forms.Button();
            this.lblEspecialidadAsignar = new System.Windows.Forms.Label();
            this.ucEspecialidadAsignar = new Vista.ucSelectorEspecialidad();
            this.btnAsignarEspecialidad = new System.Windows.Forms.Button();
            this.btnQuitarEspecialidad = new System.Windows.Forms.Button();
            this.groupBoxHorariosMedicos = new System.Windows.Forms.GroupBox();
            this.lblMedicoHorario = new System.Windows.Forms.Label();
            this.ucMedicoHorario = new Vista.ucSelectorMedico();
            this.lblEspecialidadHorario = new System.Windows.Forms.Label();
            this.ucEspecialidadHorario = new Vista.ucSelectorEspecialidad();
            this.checkBoxLunes = new System.Windows.Forms.CheckBox();
            this.checkBoxMartes = new System.Windows.Forms.CheckBox();
            this.checkBoxMiercoles = new System.Windows.Forms.CheckBox();
            this.checkBoxJueves = new System.Windows.Forms.CheckBox();
            this.checkBoxViernes = new System.Windows.Forms.CheckBox();
            this.checkBoxSabado = new System.Windows.Forms.CheckBox();
            this.lblDesde = new System.Windows.Forms.Label();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.lblHasta = new System.Windows.Forms.Label();
            this.txtHasta = new System.Windows.Forms.TextBox();
            this.lblTurnosCadaXMin = new System.Windows.Forms.Label();
            this.txtTurnosCada = new System.Windows.Forms.TextBox();
            this.btnAsignarDiaYHorario = new System.Windows.Forms.Button();
            this.btnModificarDiasYHorarios = new System.Windows.Forms.Button();
            this.btnEditarFranjas = new System.Windows.Forms.Button();
            this.checkBoxMostrarInactivos = new System.Windows.Forms.CheckBox();
            this.dataGridViewMedicosDiasHorarios = new System.Windows.Forms.DataGridView();
            this.groupBoxEspecialidades.SuspendLayout();
            this.groupBoxAsignacionEspecialidades.SuspendLayout();
            this.groupBoxHorariosMedicos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMedicosDiasHorarios)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxEspecialidades
            // 
            this.groupBoxEspecialidades.Controls.Add(this.lblNombreEspecialidad);
            this.groupBoxEspecialidades.Controls.Add(this.txtNombreEspecialidad);
            this.groupBoxEspecialidades.Controls.Add(this.btnEspecialidadCrear);
            this.groupBoxEspecialidades.Controls.Add(this.ucEspecialidadCrud);
            this.groupBoxEspecialidades.Controls.Add(this.btnEspecialidadEliminar);
            this.groupBoxEspecialidades.Location = new System.Drawing.Point(15, 17);
            this.groupBoxEspecialidades.Name = "groupBoxEspecialidades";
            this.groupBoxEspecialidades.Size = new System.Drawing.Size(730, 75);
            this.groupBoxEspecialidades.TabIndex = 0;
            this.groupBoxEspecialidades.TabStop = false;
            this.groupBoxEspecialidades.Text = "Especialidades";
            // 
            // lblNombreEspecialidad
            // 
            this.lblNombreEspecialidad.AutoSize = true;
            this.lblNombreEspecialidad.Location = new System.Drawing.Point(15, 38);
            this.lblNombreEspecialidad.Name = "lblNombreEspecialidad";
            this.lblNombreEspecialidad.TabIndex = 1;
            this.lblNombreEspecialidad.Text = "Nombre:";
            // 
            // txtNombreEspecialidad
            // 
            this.txtNombreEspecialidad.Location = new System.Drawing.Point(80, 33);
            this.txtNombreEspecialidad.Name = "txtNombreEspecialidad";
            this.txtNombreEspecialidad.Size = new System.Drawing.Size(170, 22);
            this.txtNombreEspecialidad.TabIndex = 2;
            this.txtNombreEspecialidad.MaxLength = 50;
            // 
            // btnEspecialidadCrear
            // 
            this.btnEspecialidadCrear.Location = new System.Drawing.Point(260, 31);
            this.btnEspecialidadCrear.Name = "btnEspecialidadCrear";
            this.btnEspecialidadCrear.Size = new System.Drawing.Size(140, 26);
            this.btnEspecialidadCrear.TabIndex = 3;
            this.btnEspecialidadCrear.Text = "Crear especialidad";
            this.btnEspecialidadCrear.UseVisualStyleBackColor = true;
            this.btnEspecialidadCrear.Click += new System.EventHandler(this.btnEspecialidadCrear_Click);
            // 
            // ucEspecialidadCrud
            // 
            this.ucEspecialidadCrud.Location = new System.Drawing.Point(420, 33);
            this.ucEspecialidadCrud.Name = "ucEspecialidadCrud";
            this.ucEspecialidadCrud.Size = new System.Drawing.Size(160, 22);
            this.ucEspecialidadCrud.TabIndex = 4;
            this.ucEspecialidadCrud.Etiqueta = "";
            // 
            // btnEspecialidadEliminar
            // 
            this.btnEspecialidadEliminar.Location = new System.Drawing.Point(590, 31);
            this.btnEspecialidadEliminar.Name = "btnEspecialidadEliminar";
            this.btnEspecialidadEliminar.Size = new System.Drawing.Size(120, 26);
            this.btnEspecialidadEliminar.TabIndex = 5;
            this.btnEspecialidadEliminar.Text = "Eliminar";
            this.btnEspecialidadEliminar.UseVisualStyleBackColor = true;
            this.btnEspecialidadEliminar.Click += new System.EventHandler(this.btnEspecialidadEliminar_Click);
            // 
            // groupBoxAsignacionEspecialidades
            // 
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.lblMedicoAsignar);
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.ucMedicoAsignar);
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.lblMatricula);
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.txtMatricula);
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.btnGuardarMatricula);
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.lblEspecialidadAsignar);
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.ucEspecialidadAsignar);
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.btnAsignarEspecialidad);
            this.groupBoxAsignacionEspecialidades.Controls.Add(this.btnQuitarEspecialidad);
            this.groupBoxAsignacionEspecialidades.Location = new System.Drawing.Point(15, 102);
            this.groupBoxAsignacionEspecialidades.Name = "groupBoxAsignacionEspecialidades";
            this.groupBoxAsignacionEspecialidades.Size = new System.Drawing.Size(730, 110);
            this.groupBoxAsignacionEspecialidades.TabIndex = 6;
            this.groupBoxAsignacionEspecialidades.TabStop = false;
            this.groupBoxAsignacionEspecialidades.Text = "Médicos y especialidades";
            // 
            // lblMedicoAsignar
            // 
            this.lblMedicoAsignar.AutoSize = true;
            this.lblMedicoAsignar.Location = new System.Drawing.Point(15, 38);
            this.lblMedicoAsignar.Name = "lblMedicoAsignar";
            this.lblMedicoAsignar.TabIndex = 7;
            this.lblMedicoAsignar.Text = "Médico:";
            // 
            // ucMedicoAsignar
            // 
            this.ucMedicoAsignar.Location = new System.Drawing.Point(80, 33);
            this.ucMedicoAsignar.Name = "ucMedicoAsignar";
            this.ucMedicoAsignar.Size = new System.Drawing.Size(200, 22);
            this.ucMedicoAsignar.TabIndex = 8;
            this.ucMedicoAsignar.Etiqueta = "";
            // 
            // lblMatricula
            // 
            this.lblMatricula.AutoSize = true;
            this.lblMatricula.Location = new System.Drawing.Point(300, 38);
            this.lblMatricula.Name = "lblMatricula";
            this.lblMatricula.TabIndex = 9;
            this.lblMatricula.Text = "Matrícula:";
            // 
            // txtMatricula
            // 
            this.txtMatricula.Location = new System.Drawing.Point(370, 33);
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.Size = new System.Drawing.Size(120, 22);
            this.txtMatricula.TabIndex = 10;
            this.txtMatricula.MaxLength = 30;
            // 
            // btnGuardarMatricula
            // 
            this.btnGuardarMatricula.Location = new System.Drawing.Point(570, 31);
            this.btnGuardarMatricula.Name = "btnGuardarMatricula";
            this.btnGuardarMatricula.Size = new System.Drawing.Size(140, 26);
            this.btnGuardarMatricula.TabIndex = 11;
            this.btnGuardarMatricula.Text = "Guardar matrícula";
            this.btnGuardarMatricula.UseVisualStyleBackColor = true;
            this.btnGuardarMatricula.Click += new System.EventHandler(this.btnGuardarMatricula_Click);
            // 
            // lblEspecialidadAsignar
            // 
            this.lblEspecialidadAsignar.AutoSize = true;
            this.lblEspecialidadAsignar.Location = new System.Drawing.Point(15, 75);
            this.lblEspecialidadAsignar.Name = "lblEspecialidadAsignar";
            this.lblEspecialidadAsignar.TabIndex = 12;
            this.lblEspecialidadAsignar.Text = "Especialidad:";
            // 
            // ucEspecialidadAsignar
            // 
            this.ucEspecialidadAsignar.Location = new System.Drawing.Point(105, 70);
            this.ucEspecialidadAsignar.Name = "ucEspecialidadAsignar";
            this.ucEspecialidadAsignar.Size = new System.Drawing.Size(175, 22);
            this.ucEspecialidadAsignar.TabIndex = 13;
            this.ucEspecialidadAsignar.Etiqueta = "";
            // 
            // btnAsignarEspecialidad
            // 
            this.btnAsignarEspecialidad.Location = new System.Drawing.Point(300, 68);
            this.btnAsignarEspecialidad.Name = "btnAsignarEspecialidad";
            this.btnAsignarEspecialidad.Size = new System.Drawing.Size(145, 26);
            this.btnAsignarEspecialidad.TabIndex = 14;
            this.btnAsignarEspecialidad.Text = "Asignar especialidad";
            this.btnAsignarEspecialidad.UseVisualStyleBackColor = true;
            this.btnAsignarEspecialidad.Click += new System.EventHandler(this.btnAsignarEspecialidad_Click);
            // 
            // btnQuitarEspecialidad
            // 
            this.btnQuitarEspecialidad.Location = new System.Drawing.Point(455, 68);
            this.btnQuitarEspecialidad.Name = "btnQuitarEspecialidad";
            this.btnQuitarEspecialidad.Size = new System.Drawing.Size(140, 26);
            this.btnQuitarEspecialidad.TabIndex = 15;
            this.btnQuitarEspecialidad.Text = "Quitar especialidad";
            this.btnQuitarEspecialidad.UseVisualStyleBackColor = true;
            this.btnQuitarEspecialidad.Click += new System.EventHandler(this.btnQuitarEspecialidad_Click);
            // 
            // groupBoxHorariosMedicos
            // 
            this.groupBoxHorariosMedicos.Controls.Add(this.lblMedicoHorario);
            this.groupBoxHorariosMedicos.Controls.Add(this.ucMedicoHorario);
            this.groupBoxHorariosMedicos.Controls.Add(this.lblEspecialidadHorario);
            this.groupBoxHorariosMedicos.Controls.Add(this.ucEspecialidadHorario);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxLunes);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxMartes);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxMiercoles);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxJueves);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxViernes);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxSabado);
            this.groupBoxHorariosMedicos.Controls.Add(this.lblDesde);
            this.groupBoxHorariosMedicos.Controls.Add(this.txtDesde);
            this.groupBoxHorariosMedicos.Controls.Add(this.lblHasta);
            this.groupBoxHorariosMedicos.Controls.Add(this.txtHasta);
            this.groupBoxHorariosMedicos.Controls.Add(this.lblTurnosCadaXMin);
            this.groupBoxHorariosMedicos.Controls.Add(this.txtTurnosCada);
            this.groupBoxHorariosMedicos.Controls.Add(this.btnAsignarDiaYHorario);
            this.groupBoxHorariosMedicos.Controls.Add(this.btnModificarDiasYHorarios);
            this.groupBoxHorariosMedicos.Controls.Add(this.btnEditarFranjas);
            this.groupBoxHorariosMedicos.Controls.Add(this.checkBoxMostrarInactivos);
            this.groupBoxHorariosMedicos.Controls.Add(this.dataGridViewMedicosDiasHorarios);
            this.groupBoxHorariosMedicos.Location = new System.Drawing.Point(15, 222);
            this.groupBoxHorariosMedicos.Name = "groupBoxHorariosMedicos";
            this.groupBoxHorariosMedicos.Size = new System.Drawing.Size(730, 345);
            this.groupBoxHorariosMedicos.TabIndex = 16;
            this.groupBoxHorariosMedicos.TabStop = false;
            this.groupBoxHorariosMedicos.Text = "Días y horarios de atención";
            // 
            // lblMedicoHorario
            // 
            this.lblMedicoHorario.AutoSize = true;
            this.lblMedicoHorario.Location = new System.Drawing.Point(15, 38);
            this.lblMedicoHorario.Name = "lblMedicoHorario";
            this.lblMedicoHorario.TabIndex = 17;
            this.lblMedicoHorario.Text = "Médico:";
            // 
            // ucMedicoHorario
            // 
            this.ucMedicoHorario.Location = new System.Drawing.Point(80, 33);
            this.ucMedicoHorario.Name = "ucMedicoHorario";
            this.ucMedicoHorario.Size = new System.Drawing.Size(200, 22);
            this.ucMedicoHorario.TabIndex = 18;
            this.ucMedicoHorario.Etiqueta = "";
            // 
            // lblEspecialidadHorario
            // 
            this.lblEspecialidadHorario.AutoSize = true;
            this.lblEspecialidadHorario.Location = new System.Drawing.Point(300, 38);
            this.lblEspecialidadHorario.Name = "lblEspecialidadHorario";
            this.lblEspecialidadHorario.TabIndex = 19;
            this.lblEspecialidadHorario.Text = "Especialidad:";
            // 
            // ucEspecialidadHorario
            // 
            this.ucEspecialidadHorario.Location = new System.Drawing.Point(390, 33);
            this.ucEspecialidadHorario.Name = "ucEspecialidadHorario";
            this.ucEspecialidadHorario.Size = new System.Drawing.Size(175, 22);
            this.ucEspecialidadHorario.TabIndex = 20;
            this.ucEspecialidadHorario.Etiqueta = "";
            // 
            // checkBoxLunes
            // 
            this.checkBoxLunes.Location = new System.Drawing.Point(15, 70);
            this.checkBoxLunes.Name = "checkBoxLunes";
            this.checkBoxLunes.Size = new System.Drawing.Size(100, 20);
            this.checkBoxLunes.TabIndex = 21;
            this.checkBoxLunes.Text = "Lunes";
            this.checkBoxLunes.UseVisualStyleBackColor = true;
            // 
            // checkBoxMartes
            // 
            this.checkBoxMartes.Location = new System.Drawing.Point(120, 70);
            this.checkBoxMartes.Name = "checkBoxMartes";
            this.checkBoxMartes.Size = new System.Drawing.Size(100, 20);
            this.checkBoxMartes.TabIndex = 22;
            this.checkBoxMartes.Text = "Martes";
            this.checkBoxMartes.UseVisualStyleBackColor = true;
            // 
            // checkBoxMiercoles
            // 
            this.checkBoxMiercoles.Location = new System.Drawing.Point(225, 70);
            this.checkBoxMiercoles.Name = "checkBoxMiercoles";
            this.checkBoxMiercoles.Size = new System.Drawing.Size(100, 20);
            this.checkBoxMiercoles.TabIndex = 23;
            this.checkBoxMiercoles.Text = "Miércoles";
            this.checkBoxMiercoles.UseVisualStyleBackColor = true;
            // 
            // checkBoxJueves
            // 
            this.checkBoxJueves.Location = new System.Drawing.Point(330, 70);
            this.checkBoxJueves.Name = "checkBoxJueves";
            this.checkBoxJueves.Size = new System.Drawing.Size(100, 20);
            this.checkBoxJueves.TabIndex = 24;
            this.checkBoxJueves.Text = "Jueves";
            this.checkBoxJueves.UseVisualStyleBackColor = true;
            // 
            // checkBoxViernes
            // 
            this.checkBoxViernes.Location = new System.Drawing.Point(435, 70);
            this.checkBoxViernes.Name = "checkBoxViernes";
            this.checkBoxViernes.Size = new System.Drawing.Size(100, 20);
            this.checkBoxViernes.TabIndex = 25;
            this.checkBoxViernes.Text = "Viernes";
            this.checkBoxViernes.UseVisualStyleBackColor = true;
            // 
            // checkBoxSabado
            // 
            this.checkBoxSabado.Location = new System.Drawing.Point(540, 70);
            this.checkBoxSabado.Name = "checkBoxSabado";
            this.checkBoxSabado.Size = new System.Drawing.Size(100, 20);
            this.checkBoxSabado.TabIndex = 26;
            this.checkBoxSabado.Text = "Sábado";
            this.checkBoxSabado.UseVisualStyleBackColor = true;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(15, 110);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.TabIndex = 27;
            this.lblDesde.Text = "Desde:";
            // 
            // txtDesde
            // 
            this.txtDesde.Location = new System.Drawing.Point(65, 105);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(70, 22);
            this.txtDesde.TabIndex = 28;
            this.txtDesde.MaxLength = 5;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(155, 110);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.TabIndex = 29;
            this.lblHasta.Text = "Hasta:";
            // 
            // txtHasta
            // 
            this.txtHasta.Location = new System.Drawing.Point(205, 105);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.Size = new System.Drawing.Size(70, 22);
            this.txtHasta.TabIndex = 30;
            this.txtHasta.MaxLength = 5;
            // 
            // lblTurnosCadaXMin
            // 
            this.lblTurnosCadaXMin.AutoSize = true;
            this.lblTurnosCadaXMin.Location = new System.Drawing.Point(295, 110);
            this.lblTurnosCadaXMin.Name = "lblTurnosCadaXMin";
            this.lblTurnosCadaXMin.TabIndex = 31;
            this.lblTurnosCadaXMin.Text = "Duración de cada turno:";
            // 
            // txtTurnosCada
            // 
            this.txtTurnosCada.Location = new System.Drawing.Point(450, 105);
            this.txtTurnosCada.Name = "txtTurnosCada";
            this.txtTurnosCada.Size = new System.Drawing.Size(60, 22);
            this.txtTurnosCada.TabIndex = 32;
            this.txtTurnosCada.MaxLength = 3;
            // 
            // btnAsignarDiaYHorario
            // 
            this.btnAsignarDiaYHorario.Location = new System.Drawing.Point(15, 142);
            this.btnAsignarDiaYHorario.Name = "btnAsignarDiaYHorario";
            this.btnAsignarDiaYHorario.Size = new System.Drawing.Size(170, 26);
            this.btnAsignarDiaYHorario.TabIndex = 33;
            this.btnAsignarDiaYHorario.Text = "Asignar días y horarios";
            this.btnAsignarDiaYHorario.UseVisualStyleBackColor = true;
            this.btnAsignarDiaYHorario.Click += new System.EventHandler(this.btnAsignarDiaYHorario_Click);
            // 
            // btnModificarDiasYHorarios
            // 
            this.btnModificarDiasYHorarios.Location = new System.Drawing.Point(195, 142);
            this.btnModificarDiasYHorarios.Name = "btnModificarDiasYHorarios";
            this.btnModificarDiasYHorarios.Size = new System.Drawing.Size(175, 26);
            this.btnModificarDiasYHorarios.TabIndex = 34;
            this.btnModificarDiasYHorarios.Text = "Modificar días y horarios";
            this.btnModificarDiasYHorarios.UseVisualStyleBackColor = true;
            this.btnModificarDiasYHorarios.Click += new System.EventHandler(this.btnModificarDiasYHorarios_Click);
            // 
            // btnEditarFranjas
            // 
            this.btnEditarFranjas.Location = new System.Drawing.Point(380, 142);
            this.btnEditarFranjas.Name = "btnEditarFranjas";
            this.btnEditarFranjas.Size = new System.Drawing.Size(130, 26);
            this.btnEditarFranjas.TabIndex = 35;
            this.btnEditarFranjas.Text = "Editar horarios";
            this.btnEditarFranjas.UseVisualStyleBackColor = true;
            this.btnEditarFranjas.Click += new System.EventHandler(this.btnEditarFranjas_Click);
            // 
            // checkBoxMostrarInactivos
            // 
            this.checkBoxMostrarInactivos.Location = new System.Drawing.Point(565, 145);
            this.checkBoxMostrarInactivos.Name = "checkBoxMostrarInactivos";
            this.checkBoxMostrarInactivos.Size = new System.Drawing.Size(145, 20);
            this.checkBoxMostrarInactivos.TabIndex = 36;
            this.checkBoxMostrarInactivos.Text = "Mostrar inactivos";
            this.checkBoxMostrarInactivos.UseVisualStyleBackColor = true;
            this.checkBoxMostrarInactivos.CheckedChanged += new System.EventHandler(this.checkBoxMostrarInactivos_CheckedChanged);
            // 
            // dataGridViewMedicosDiasHorarios
            // 
            this.dataGridViewMedicosDiasHorarios.AllowUserToAddRows = false;
            this.dataGridViewMedicosDiasHorarios.AllowUserToDeleteRows = false;
            this.dataGridViewMedicosDiasHorarios.AllowUserToResizeRows = false;
            this.dataGridViewMedicosDiasHorarios.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewMedicosDiasHorarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMedicosDiasHorarios.MultiSelect = false;
            this.dataGridViewMedicosDiasHorarios.ReadOnly = true;
            this.dataGridViewMedicosDiasHorarios.RowHeadersVisible = false;
            this.dataGridViewMedicosDiasHorarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMedicosDiasHorarios.Location = new System.Drawing.Point(15, 180);
            this.dataGridViewMedicosDiasHorarios.Name = "dataGridViewMedicosDiasHorarios";
            this.dataGridViewMedicosDiasHorarios.Size = new System.Drawing.Size(700, 150);
            this.dataGridViewMedicosDiasHorarios.TabIndex = 37;
            // 
            // frmGestionMedicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 582);
            this.Controls.Add(this.groupBoxHorariosMedicos);
            this.Controls.Add(this.groupBoxAsignacionEspecialidades);
            this.Controls.Add(this.groupBoxEspecialidades);
            this.Name = "frmGestionMedicos";
            this.Text = "Gestión de Agenda Médica";
            this.Load += new System.EventHandler(this.frmGestionMedicos_Load);
            this.groupBoxEspecialidades.ResumeLayout(false);
            this.groupBoxEspecialidades.PerformLayout();
            this.groupBoxAsignacionEspecialidades.ResumeLayout(false);
            this.groupBoxAsignacionEspecialidades.PerformLayout();
            this.groupBoxHorariosMedicos.ResumeLayout(false);
            this.groupBoxHorariosMedicos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMedicosDiasHorarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxEspecialidades;
        private System.Windows.Forms.Label lblNombreEspecialidad;
        private System.Windows.Forms.TextBox txtNombreEspecialidad;
        private System.Windows.Forms.Button btnEspecialidadCrear;
        private Vista.ucSelectorEspecialidad ucEspecialidadCrud;
        private System.Windows.Forms.Button btnEspecialidadEliminar;
        private System.Windows.Forms.GroupBox groupBoxAsignacionEspecialidades;
        private System.Windows.Forms.Label lblMedicoAsignar;
        private Vista.ucSelectorMedico ucMedicoAsignar;
        private System.Windows.Forms.Label lblMatricula;
        private System.Windows.Forms.TextBox txtMatricula;
        private System.Windows.Forms.Button btnGuardarMatricula;
        private System.Windows.Forms.Label lblEspecialidadAsignar;
        private Vista.ucSelectorEspecialidad ucEspecialidadAsignar;
        private System.Windows.Forms.Button btnAsignarEspecialidad;
        private System.Windows.Forms.Button btnQuitarEspecialidad;
        private System.Windows.Forms.GroupBox groupBoxHorariosMedicos;
        private System.Windows.Forms.Label lblMedicoHorario;
        private Vista.ucSelectorMedico ucMedicoHorario;
        private System.Windows.Forms.Label lblEspecialidadHorario;
        private Vista.ucSelectorEspecialidad ucEspecialidadHorario;
        private System.Windows.Forms.CheckBox checkBoxLunes;
        private System.Windows.Forms.CheckBox checkBoxMartes;
        private System.Windows.Forms.CheckBox checkBoxMiercoles;
        private System.Windows.Forms.CheckBox checkBoxJueves;
        private System.Windows.Forms.CheckBox checkBoxViernes;
        private System.Windows.Forms.CheckBox checkBoxSabado;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.TextBox txtHasta;
        private System.Windows.Forms.Label lblTurnosCadaXMin;
        private System.Windows.Forms.TextBox txtTurnosCada;
        private System.Windows.Forms.Button btnAsignarDiaYHorario;
        private System.Windows.Forms.Button btnModificarDiasYHorarios;
        private System.Windows.Forms.Button btnEditarFranjas;
        private System.Windows.Forms.CheckBox checkBoxMostrarInactivos;
        private System.Windows.Forms.DataGridView dataGridViewMedicosDiasHorarios;
    }
}
