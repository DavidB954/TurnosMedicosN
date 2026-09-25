namespace Vista
{
    partial class frm_BitacoraTurno
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBoxFiltros = new System.Windows.Forms.GroupBox();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.txtIdTurno = new System.Windows.Forms.TextBox();
            this.lblIdTurno = new System.Windows.Forms.Label();
            this.dgvEventos = new System.Windows.Forms.DataGridView();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.lblTotalDeRegistros = new System.Windows.Forms.Label();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.groupBoxFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(488, 27);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(825, 36);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "BITACORA DE EVENTOS DE TURNO (SQL TRIGGERS)";
            // 
            // groupBoxFiltros
            // 
            this.groupBoxFiltros.Controls.Add(this.btnLimpiarFiltros);
            this.groupBoxFiltros.Controls.Add(this.btnBuscar);
            this.groupBoxFiltros.Controls.Add(this.dtpHasta);
            this.groupBoxFiltros.Controls.Add(this.lblHasta);
            this.groupBoxFiltros.Controls.Add(this.dtpDesde);
            this.groupBoxFiltros.Controls.Add(this.lblDesde);
            this.groupBoxFiltros.Controls.Add(this.txtIdTurno);
            this.groupBoxFiltros.Controls.Add(this.lblIdTurno);
            this.groupBoxFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxFiltros.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBoxFiltros.Location = new System.Drawing.Point(494, 83);
            this.groupBoxFiltros.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxFiltros.Name = "groupBoxFiltros";
            this.groupBoxFiltros.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxFiltros.Size = new System.Drawing.Size(1119, 125);
            this.groupBoxFiltros.TabIndex = 1;
            this.groupBoxFiltros.TabStop = false;
            this.groupBoxFiltros.Text = "Filtros de Busqueda";
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLimpiarFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(908, 68);
            this.btnLimpiarFiltros.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(160, 43);
            this.btnLimpiarFiltros.TabIndex = 7;
            this.btnLimpiarFiltros.Text = "Limpiar Filtros";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = false;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(908, 12);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(160, 41);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(645, 77);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.ShowCheckBox = true;
            this.dtpHasta.Size = new System.Drawing.Size(238, 30);
            this.dtpHasta.TabIndex = 5;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(640, 45);
            this.lblHasta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(161, 29);
            this.lblHasta.TabIndex = 4;
            this.lblHasta.Text = "Fecha hasta:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(330, 77);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.ShowCheckBox = true;
            this.dtpDesde.Size = new System.Drawing.Size(238, 30);
            this.dtpDesde.TabIndex = 3;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(326, 45);
            this.lblDesde.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(172, 29);
            this.lblDesde.TabIndex = 2;
            this.lblDesde.Text = "Fecha desde:";
            // 
            // txtIdTurno
            // 
            this.txtIdTurno.Location = new System.Drawing.Point(146, 77);
            this.txtIdTurno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtIdTurno.Name = "txtIdTurno";
            this.txtIdTurno.Size = new System.Drawing.Size(148, 35);
            this.txtIdTurno.TabIndex = 1;
            // 
            // lblIdTurno
            // 
            this.lblIdTurno.AutoSize = true;
            this.lblIdTurno.Location = new System.Drawing.Point(26, 82);
            this.lblIdTurno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIdTurno.Name = "lblIdTurno";
            this.lblIdTurno.Size = new System.Drawing.Size(124, 29);
            this.lblIdTurno.TabIndex = 0;
            this.lblIdTurno.Text = "N° Turno:";
            // 
            // dgvEventos
            // 
            this.dgvEventos.AllowUserToAddRows = false;
            this.dgvEventos.AllowUserToDeleteRows = false;
            this.dgvEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEventos.Location = new System.Drawing.Point(494, 218);
            this.dgvEventos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvEventos.Name = "dgvEventos";
            this.dgvEventos.ReadOnly = true;
            this.dgvEventos.RowHeadersWidth = 62;
            this.dgvEventos.Size = new System.Drawing.Size(1170, 592);
            this.dgvEventos.TabIndex = 2;
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.Location = new System.Drawing.Point(494, 840);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(246, 49);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // lblTotalDeRegistros
            // 
            this.lblTotalDeRegistros.AutoSize = true;
            this.lblTotalDeRegistros.Location = new System.Drawing.Point(1343, 840);
            this.lblTotalDeRegistros.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalDeRegistros.Name = "lblTotalDeRegistros";
            this.lblTotalDeRegistros.Size = new System.Drawing.Size(113, 20);
            this.lblTotalDeRegistros.TabIndex = 4;
            this.lblTotalDeRegistros.Text = "Total registros:";
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Location = new System.Drawing.Point(1628, 840);
            this.lblTotalRegistros.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(0, 20);
            this.lblTotalRegistros.TabIndex = 5;
            // 
            // frm_BitacoraTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1800, 923);
            this.Controls.Add(this.lblTotalRegistros);
            this.Controls.Add(this.lblTotalDeRegistros);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.dgvEventos);
            this.Controls.Add(this.groupBoxFiltros);
            this.Controls.Add(this.lblTitulo);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "frm_BitacoraTurno";
            this.Text = "frm_BitacoraTurno";
            this.Load += new System.EventHandler(this.frm_BitacoraTurno_Load);
            this.groupBoxFiltros.ResumeLayout(false);
            this.groupBoxFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBoxFiltros;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.TextBox txtIdTurno;
        private System.Windows.Forms.Label lblIdTurno;
        private System.Windows.Forms.DataGridView dgvEventos;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblTotalDeRegistros;
        private System.Windows.Forms.Label lblTotalRegistros;
    }
}
