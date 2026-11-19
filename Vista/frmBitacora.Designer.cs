namespace Vista
{
    partial class frmBitacora
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
            this.groupBox_FiltrosDeBusqueda = new System.Windows.Forms.GroupBox();
            this.lbl_FechaDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblFecha_Hasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lbl_Usuario = new System.Windows.Forms.Label();
            this.cboUsuario = new System.Windows.Forms.ComboBox();
            this.lbl_Modulo = new System.Windows.Forms.Label();
            this.cboModulo = new System.Windows.Forms.ComboBox();
            this.lbl_IP = new System.Windows.Forms.Label();
            this.txtBuscarPorIP = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.dgvBitacora = new System.Windows.Forms.DataGridView();
            this.lblTotalDeRegistros = new System.Windows.Forms.Label();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.btnActualizarBitacora = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFechaHora = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblAccion = new System.Windows.Forms.Label();
            this.txtAccion = new System.Windows.Forms.TextBox();
            this.lblModulo = new System.Windows.Forms.Label();
            this.txtModulo = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblNombreHost = new System.Windows.Forms.Label();
            this.txtNombreHost = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.RichTextBox();
            this.groupBox_FiltrosDeBusqueda.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_FiltrosDeBusqueda
            // 
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.lbl_FechaDesde);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.dtpDesde);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.lblFecha_Hasta);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.dtpHasta);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.lbl_Usuario);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.cboUsuario);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.lbl_Modulo);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.cboModulo);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.lbl_IP);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.txtBuscarPorIP);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.btnBuscar);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.btnLimpiarFiltros);
            this.groupBox_FiltrosDeBusqueda.Location = new System.Drawing.Point(15, 17);
            this.groupBox_FiltrosDeBusqueda.Name = "groupBox_FiltrosDeBusqueda";
            this.groupBox_FiltrosDeBusqueda.Size = new System.Drawing.Size(730, 102);
            this.groupBox_FiltrosDeBusqueda.TabIndex = 0;
            this.groupBox_FiltrosDeBusqueda.TabStop = false;
            this.groupBox_FiltrosDeBusqueda.Text = "Filtros de búsqueda";
            // 
            // lbl_FechaDesde
            // 
            this.lbl_FechaDesde.AutoSize = true;
            this.lbl_FechaDesde.Location = new System.Drawing.Point(15, 36);
            this.lbl_FechaDesde.Name = "lbl_FechaDesde";
            this.lbl_FechaDesde.TabIndex = 1;
            this.lbl_FechaDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.CustomFormat = "dd/MM/yyyy";
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDesde.Location = new System.Drawing.Point(65, 31);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(115, 22);
            this.dtpDesde.TabIndex = 2;
            this.dtpDesde.ShowCheckBox = true;
            // 
            // lblFecha_Hasta
            // 
            this.lblFecha_Hasta.AutoSize = true;
            this.lblFecha_Hasta.Location = new System.Drawing.Point(195, 36);
            this.lblFecha_Hasta.Name = "lblFecha_Hasta";
            this.lblFecha_Hasta.TabIndex = 3;
            this.lblFecha_Hasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.CustomFormat = "dd/MM/yyyy";
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHasta.Location = new System.Drawing.Point(245, 31);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(115, 22);
            this.dtpHasta.TabIndex = 4;
            this.dtpHasta.ShowCheckBox = true;
            // 
            // lbl_Usuario
            // 
            this.lbl_Usuario.AutoSize = true;
            this.lbl_Usuario.Location = new System.Drawing.Point(375, 36);
            this.lbl_Usuario.Name = "lbl_Usuario";
            this.lbl_Usuario.TabIndex = 5;
            this.lbl_Usuario.Text = "Usuario:";
            // 
            // cboUsuario
            // 
            this.cboUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsuario.FormattingEnabled = true;
            this.cboUsuario.Location = new System.Drawing.Point(435, 31);
            this.cboUsuario.Name = "cboUsuario";
            this.cboUsuario.Size = new System.Drawing.Size(280, 22);
            this.cboUsuario.TabIndex = 6;
            // 
            // lbl_Modulo
            // 
            this.lbl_Modulo.AutoSize = true;
            this.lbl_Modulo.Location = new System.Drawing.Point(15, 73);
            this.lbl_Modulo.Name = "lbl_Modulo";
            this.lbl_Modulo.TabIndex = 7;
            this.lbl_Modulo.Text = "Módulo:";
            // 
            // cboModulo
            // 
            this.cboModulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModulo.FormattingEnabled = true;
            this.cboModulo.Location = new System.Drawing.Point(65, 68);
            this.cboModulo.Name = "cboModulo";
            this.cboModulo.Size = new System.Drawing.Size(150, 22);
            this.cboModulo.TabIndex = 8;
            // 
            // lbl_IP
            // 
            this.lbl_IP.AutoSize = true;
            this.lbl_IP.Location = new System.Drawing.Point(235, 73);
            this.lbl_IP.Name = "lbl_IP";
            this.lbl_IP.TabIndex = 9;
            this.lbl_IP.Text = "IP:";
            // 
            // txtBuscarPorIP
            // 
            this.txtBuscarPorIP.Location = new System.Drawing.Point(265, 68);
            this.txtBuscarPorIP.Name = "txtBuscarPorIP";
            this.txtBuscarPorIP.Size = new System.Drawing.Size(130, 22);
            this.txtBuscarPorIP.TabIndex = 10;
            this.txtBuscarPorIP.MaxLength = 45;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(495, 66);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 26);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(605, 66);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(110, 26);
            this.btnLimpiarFiltros.TabIndex = 12;
            this.btnLimpiarFiltros.Text = "Limpiar filtros";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = true;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // dgvBitacora
            // 
            this.dgvBitacora.AllowUserToAddRows = false;
            this.dgvBitacora.AllowUserToDeleteRows = false;
            this.dgvBitacora.AllowUserToResizeRows = false;
            this.dgvBitacora.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBitacora.MultiSelect = false;
            this.dgvBitacora.ReadOnly = true;
            this.dgvBitacora.RowHeadersVisible = false;
            this.dgvBitacora.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBitacora.Location = new System.Drawing.Point(20, 132);
            this.dgvBitacora.Name = "dgvBitacora";
            this.dgvBitacora.Size = new System.Drawing.Size(720, 215);
            this.dgvBitacora.TabIndex = 13;
            this.dgvBitacora.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvBitacora.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBitacora_CellClick);
            // 
            // lblTotalDeRegistros
            // 
            this.lblTotalDeRegistros.AutoSize = true;
            this.lblTotalDeRegistros.Location = new System.Drawing.Point(20, 362);
            this.lblTotalDeRegistros.Name = "lblTotalDeRegistros";
            this.lblTotalDeRegistros.TabIndex = 14;
            this.lblTotalDeRegistros.Text = "Total de registros:";
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Location = new System.Drawing.Point(145, 362);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.TabIndex = 15;
            this.lblTotalRegistros.Text = "";
            // 
            // btnActualizarBitacora
            // 
            this.btnActualizarBitacora.Location = new System.Drawing.Point(620, 355);
            this.btnActualizarBitacora.Name = "btnActualizarBitacora";
            this.btnActualizarBitacora.Size = new System.Drawing.Size(120, 26);
            this.btnActualizarBitacora.TabIndex = 16;
            this.btnActualizarBitacora.Text = "Actualizar";
            this.btnActualizarBitacora.UseVisualStyleBackColor = true;
            this.btnActualizarBitacora.Click += new System.EventHandler(this.btnActualizarBitacora_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFechaHora);
            this.groupBox1.Controls.Add(this.txtFecha);
            this.groupBox1.Controls.Add(this.lblUsuario);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.lblAccion);
            this.groupBox1.Controls.Add(this.txtAccion);
            this.groupBox1.Controls.Add(this.lblModulo);
            this.groupBox1.Controls.Add(this.txtModulo);
            this.groupBox1.Controls.Add(this.lblIP);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.lblNombreHost);
            this.groupBox1.Controls.Add(this.txtNombreHost);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Location = new System.Drawing.Point(15, 392);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(730, 175);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle del registro seleccionado";
            // 
            // lblFechaHora
            // 
            this.lblFechaHora.AutoSize = true;
            this.lblFechaHora.Location = new System.Drawing.Point(15, 33);
            this.lblFechaHora.Name = "lblFechaHora";
            this.lblFechaHora.TabIndex = 18;
            this.lblFechaHora.Text = "Fecha y hora:";
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(110, 28);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(200, 22);
            this.txtFecha.TabIndex = 19;
            this.txtFecha.ReadOnly = true;
            this.txtFecha.TabStop = false;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(15, 68);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.TabIndex = 20;
            this.lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(110, 63);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(200, 22);
            this.txtUsuario.TabIndex = 21;
            this.txtUsuario.ReadOnly = true;
            this.txtUsuario.TabStop = false;
            // 
            // lblAccion
            // 
            this.lblAccion.AutoSize = true;
            this.lblAccion.Location = new System.Drawing.Point(15, 103);
            this.lblAccion.Name = "lblAccion";
            this.lblAccion.TabIndex = 22;
            this.lblAccion.Text = "Acción:";
            // 
            // txtAccion
            // 
            this.txtAccion.Location = new System.Drawing.Point(110, 98);
            this.txtAccion.Name = "txtAccion";
            this.txtAccion.Size = new System.Drawing.Size(200, 22);
            this.txtAccion.TabIndex = 23;
            this.txtAccion.ReadOnly = true;
            this.txtAccion.TabStop = false;
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = true;
            this.lblModulo.Location = new System.Drawing.Point(15, 138);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.TabIndex = 24;
            this.lblModulo.Text = "Módulo:";
            // 
            // txtModulo
            // 
            this.txtModulo.Location = new System.Drawing.Point(110, 133);
            this.txtModulo.Name = "txtModulo";
            this.txtModulo.Size = new System.Drawing.Size(200, 22);
            this.txtModulo.TabIndex = 25;
            this.txtModulo.ReadOnly = true;
            this.txtModulo.TabStop = false;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(330, 33);
            this.lblIP.Name = "lblIP";
            this.lblIP.TabIndex = 26;
            this.lblIP.Text = "IP:";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(435, 28);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(280, 22);
            this.txtIP.TabIndex = 27;
            this.txtIP.ReadOnly = true;
            this.txtIP.TabStop = false;
            // 
            // lblNombreHost
            // 
            this.lblNombreHost.AutoSize = true;
            this.lblNombreHost.Location = new System.Drawing.Point(330, 68);
            this.lblNombreHost.Name = "lblNombreHost";
            this.lblNombreHost.TabIndex = 28;
            this.lblNombreHost.Text = "Nombre del equipo:";
            // 
            // txtNombreHost
            // 
            this.txtNombreHost.Location = new System.Drawing.Point(435, 63);
            this.txtNombreHost.Name = "txtNombreHost";
            this.txtNombreHost.Size = new System.Drawing.Size(280, 22);
            this.txtNombreHost.TabIndex = 29;
            this.txtNombreHost.ReadOnly = true;
            this.txtNombreHost.TabStop = false;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(330, 103);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.TabIndex = 30;
            this.lblDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(435, 98);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(280, 60);
            this.txtDescripcion.TabIndex = 31;
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.TabStop = false;
            // 
            // frmBitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 582);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnActualizarBitacora);
            this.Controls.Add(this.lblTotalRegistros);
            this.Controls.Add(this.lblTotalDeRegistros);
            this.Controls.Add(this.dgvBitacora);
            this.Controls.Add(this.groupBox_FiltrosDeBusqueda);
            this.Name = "frmBitacora";
            this.Text = "Bitácora";
            this.Load += new System.EventHandler(this.frm_Bitacora_Load);
            this.groupBox_FiltrosDeBusqueda.ResumeLayout(false);
            this.groupBox_FiltrosDeBusqueda.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_FiltrosDeBusqueda;
        private System.Windows.Forms.Label lbl_FechaDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblFecha_Hasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lbl_Usuario;
        private System.Windows.Forms.ComboBox cboUsuario;
        private System.Windows.Forms.Label lbl_Modulo;
        private System.Windows.Forms.ComboBox cboModulo;
        private System.Windows.Forms.Label lbl_IP;
        private System.Windows.Forms.TextBox txtBuscarPorIP;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.DataGridView dgvBitacora;
        private System.Windows.Forms.Label lblTotalDeRegistros;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.Button btnActualizarBitacora;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFechaHora;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblAccion;
        private System.Windows.Forms.TextBox txtAccion;
        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.TextBox txtModulo;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblNombreHost;
        private System.Windows.Forms.TextBox txtNombreHost;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.RichTextBox txtDescripcion;
    }
}
