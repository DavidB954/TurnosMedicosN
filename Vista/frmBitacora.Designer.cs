namespace Vista
{
    partial class frmBitacora
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_FiltrosDeBusqueda = new System.Windows.Forms.GroupBox();
            this.txtBuscarPorIP = new System.Windows.Forms.TextBox();
            this.cboModulo = new System.Windows.Forms.ComboBox();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lbl_IP = new System.Windows.Forms.Label();
            this.lbl_Modulo = new System.Windows.Forms.Label();
            this.lbl_Usuario = new System.Windows.Forms.Label();
            this.lblFecha_Hasta = new System.Windows.Forms.Label();
            this.lbl_FechaDesde = new System.Windows.Forms.Label();
            this.dgvBitacora = new System.Windows.Forms.DataGridView();
            this.btnActualizarBitacora = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.RichTextBox();
            this.txtNombreHost = new System.Windows.Forms.TextBox();
            this.lblNombreHost = new System.Windows.Forms.Label();
            this.lblTotalDeRegistros = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtModulo = new System.Windows.Forms.TextBox();
            this.txtAccion = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.lblModulo = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblAccion = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblFechaHora = new System.Windows.Forms.Label();
            this.cboUsuario = new System.Windows.Forms.ComboBox();
            this.groupBox_FiltrosDeBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(208, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "BITACORA DEL SISTEMA";
            // 
            // groupBox_FiltrosDeBusqueda
            // 
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.cboUsuario);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.txtBuscarPorIP);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.cboModulo);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.dtpHasta);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.dtpDesde);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.btnLimpiarFiltros);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.btnBuscar);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.lbl_IP);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.lbl_Modulo);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.lbl_Usuario);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.lblFecha_Hasta);
            this.groupBox_FiltrosDeBusqueda.Controls.Add(this.lbl_FechaDesde);
            this.groupBox_FiltrosDeBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_FiltrosDeBusqueda.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBox_FiltrosDeBusqueda.Location = new System.Drawing.Point(212, 38);
            this.groupBox_FiltrosDeBusqueda.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_FiltrosDeBusqueda.Name = "groupBox_FiltrosDeBusqueda";
            this.groupBox_FiltrosDeBusqueda.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_FiltrosDeBusqueda.Size = new System.Drawing.Size(1063, 81);
            this.groupBox_FiltrosDeBusqueda.TabIndex = 4;
            this.groupBox_FiltrosDeBusqueda.TabStop = false;
            this.groupBox_FiltrosDeBusqueda.Text = "Filtros de Busqueda";
            // 
            // txtBuscarPorIP
            // 
            this.txtBuscarPorIP.Location = new System.Drawing.Point(652, 48);
            this.txtBuscarPorIP.Name = "txtBuscarPorIP";
            this.txtBuscarPorIP.Size = new System.Drawing.Size(149, 26);
            this.txtBuscarPorIP.TabIndex = 23;
            // 
            // cboModulo
            // 
            this.cboModulo.FormattingEnabled = true;
            this.cboModulo.Location = new System.Drawing.Point(504, 47);
            this.cboModulo.Name = "cboModulo";
            this.cboModulo.Size = new System.Drawing.Size(121, 28);
            this.cboModulo.TabIndex = 22;
            // 
            // dtpHasta
            // 
            this.dtpHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(201, 50);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(104, 23);
            this.dtpHasta.TabIndex = 21;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(18, 50);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(118, 23);
            this.dtpDesde.TabIndex = 20;
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLimpiarFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarFiltros.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(946, 25);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(112, 47);
            this.btnLimpiarFiltros.TabIndex = 19;
            this.btnLimpiarFiltros.Text = "Limpiar Filtros";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = false;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(813, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(119, 47);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lbl_IP
            // 
            this.lbl_IP.AutoSize = true;
            this.lbl_IP.Location = new System.Drawing.Point(649, 29);
            this.lbl_IP.Name = "lbl_IP";
            this.lbl_IP.Size = new System.Drawing.Size(31, 20);
            this.lbl_IP.TabIndex = 17;
            this.lbl_IP.Text = "IP:";
            // 
            // lbl_Modulo
            // 
            this.lbl_Modulo.AutoSize = true;
            this.lbl_Modulo.Location = new System.Drawing.Point(501, 29);
            this.lbl_Modulo.Name = "lbl_Modulo";
            this.lbl_Modulo.Size = new System.Drawing.Size(67, 20);
            this.lbl_Modulo.TabIndex = 16;
            this.lbl_Modulo.Text = "Modulo";
            // 
            // lbl_Usuario
            // 
            this.lbl_Usuario.AutoSize = true;
            this.lbl_Usuario.Location = new System.Drawing.Point(350, 29);
            this.lbl_Usuario.Name = "lbl_Usuario";
            this.lbl_Usuario.Size = new System.Drawing.Size(76, 20);
            this.lbl_Usuario.TabIndex = 15;
            this.lbl_Usuario.Text = "Usuario:";
            // 
            // lblFecha_Hasta
            // 
            this.lblFecha_Hasta.AutoSize = true;
            this.lblFecha_Hasta.Location = new System.Drawing.Point(197, 29);
            this.lblFecha_Hasta.Name = "lblFecha_Hasta";
            this.lblFecha_Hasta.Size = new System.Drawing.Size(114, 20);
            this.lblFecha_Hasta.TabIndex = 14;
            this.lblFecha_Hasta.Text = "Fecha hasta:";
            // 
            // lbl_FechaDesde
            // 
            this.lbl_FechaDesde.AutoSize = true;
            this.lbl_FechaDesde.Location = new System.Drawing.Point(18, 29);
            this.lbl_FechaDesde.Name = "lbl_FechaDesde";
            this.lbl_FechaDesde.Size = new System.Drawing.Size(118, 20);
            this.lbl_FechaDesde.TabIndex = 13;
            this.lbl_FechaDesde.Text = "Fecha desde:";
            // 
            // dgvBitacora
            // 
            this.dgvBitacora.AllowUserToAddRows = false;
            this.dgvBitacora.AllowUserToDeleteRows = false;
            this.dgvBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBitacora.Location = new System.Drawing.Point(212, 124);
            this.dgvBitacora.Name = "dgvBitacora";
            this.dgvBitacora.ReadOnly = true;
            this.dgvBitacora.RowHeadersWidth = 62;
            this.dgvBitacora.Size = new System.Drawing.Size(1058, 226);
            this.dgvBitacora.TabIndex = 5;
            this.dgvBitacora.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBitacora_CellClick);
            // 
            // btnActualizarBitacora
            // 
            this.btnActualizarBitacora.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnActualizarBitacora.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarBitacora.Location = new System.Drawing.Point(212, 350);
            this.btnActualizarBitacora.Name = "btnActualizarBitacora";
            this.btnActualizarBitacora.Size = new System.Drawing.Size(164, 28);
            this.btnActualizarBitacora.TabIndex = 6;
            this.btnActualizarBitacora.Text = "Actualizar";
            this.btnActualizarBitacora.UseVisualStyleBackColor = false;
            this.btnActualizarBitacora.Click += new System.EventHandler(this.btnActualizarBitacora_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotalRegistros);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.txtNombreHost);
            this.groupBox1.Controls.Add(this.lblNombreHost);
            this.groupBox1.Controls.Add(this.lblTotalDeRegistros);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.txtModulo);
            this.groupBox1.Controls.Add(this.txtAccion);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.txtFecha);
            this.groupBox1.Controls.Add(this.lblModulo);
            this.groupBox1.Controls.Add(this.lblIP);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.lblAccion);
            this.groupBox1.Controls.Add(this.lblUsuario);
            this.groupBox1.Controls.Add(this.lblFechaHora);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(212, 384);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(1058, 150);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle Del Registro Seleccionado";
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Location = new System.Drawing.Point(981, 129);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(0, 20);
            this.lblTotalRegistros.TabIndex = 33;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(721, 36);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(246, 91);
            this.txtDescripcion.TabIndex = 32;
            this.txtDescripcion.Text = "";
            // 
            // txtNombreHost
            // 
            this.txtNombreHost.Location = new System.Drawing.Point(514, 92);
            this.txtNombreHost.Name = "txtNombreHost";
            this.txtNombreHost.ReadOnly = true;
            this.txtNombreHost.Size = new System.Drawing.Size(181, 26);
            this.txtNombreHost.TabIndex = 31;
            // 
            // lblNombreHost
            // 
            this.lblNombreHost.AutoSize = true;
            this.lblNombreHost.Location = new System.Drawing.Point(354, 94);
            this.lblNombreHost.Name = "lblNombreHost";
            this.lblNombreHost.Size = new System.Drawing.Size(148, 20);
            this.lblNombreHost.TabIndex = 30;
            this.lblNombreHost.Text = "Nombre del Host:";
            // 
            // lblTotalDeRegistros
            // 
            this.lblTotalDeRegistros.AutoSize = true;
            this.lblTotalDeRegistros.Location = new System.Drawing.Point(849, 131);
            this.lblTotalDeRegistros.Name = "lblTotalDeRegistros";
            this.lblTotalDeRegistros.Size = new System.Drawing.Size(129, 20);
            this.lblTotalDeRegistros.TabIndex = 29;
            this.lblTotalDeRegistros.Text = "Total registros:";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(483, 55);
            this.txtIP.Name = "txtIP";
            this.txtIP.ReadOnly = true;
            this.txtIP.Size = new System.Drawing.Size(212, 26);
            this.txtIP.TabIndex = 28;
            // 
            // txtModulo
            // 
            this.txtModulo.Location = new System.Drawing.Point(483, 20);
            this.txtModulo.Name = "txtModulo";
            this.txtModulo.ReadOnly = true;
            this.txtModulo.Size = new System.Drawing.Size(212, 26);
            this.txtModulo.TabIndex = 27;
            // 
            // txtAccion
            // 
            this.txtAccion.Location = new System.Drawing.Point(138, 90);
            this.txtAccion.Name = "txtAccion";
            this.txtAccion.ReadOnly = true;
            this.txtAccion.Size = new System.Drawing.Size(212, 26);
            this.txtAccion.TabIndex = 26;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(138, 55);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.ReadOnly = true;
            this.txtUsuario.Size = new System.Drawing.Size(212, 26);
            this.txtUsuario.TabIndex = 25;
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(138, 20);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.ReadOnly = true;
            this.txtFecha.Size = new System.Drawing.Size(212, 26);
            this.txtFecha.TabIndex = 24;
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = true;
            this.lblModulo.Location = new System.Drawing.Point(354, 20);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Size = new System.Drawing.Size(72, 20);
            this.lblModulo.TabIndex = 23;
            this.lblModulo.Text = "Modulo:";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(354, 55);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(31, 20);
            this.lblIP.TabIndex = 22;
            this.lblIP.Text = "IP:";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(718, 14);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(108, 20);
            this.lblDescripcion.TabIndex = 21;
            this.lblDescripcion.Text = "Descripcion:";
            // 
            // lblAccion
            // 
            this.lblAccion.AutoSize = true;
            this.lblAccion.Location = new System.Drawing.Point(45, 97);
            this.lblAccion.Name = "lblAccion";
            this.lblAccion.Size = new System.Drawing.Size(68, 20);
            this.lblAccion.TabIndex = 20;
            this.lblAccion.Text = "Accion:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(45, 62);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(76, 20);
            this.lblUsuario.TabIndex = 19;
            this.lblUsuario.Text = "Usuario:";
            // 
            // lblFechaHora
            // 
            this.lblFechaHora.AutoSize = true;
            this.lblFechaHora.Location = new System.Drawing.Point(15, 23);
            this.lblFechaHora.Name = "lblFechaHora";
            this.lblFechaHora.Size = new System.Drawing.Size(116, 20);
            this.lblFechaHora.TabIndex = 18;
            this.lblFechaHora.Text = "Fecha y Hora";
            // 
            // cboUsuario
            // 
            this.cboUsuario.FormattingEnabled = true;
            this.cboUsuario.Location = new System.Drawing.Point(354, 47);
            this.cboUsuario.Name = "cboUsuario";
            this.cboUsuario.Size = new System.Drawing.Size(121, 28);
            this.cboUsuario.TabIndex = 24;
            // 
            // frm_Bitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 541);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnActualizarBitacora);
            this.Controls.Add(this.dgvBitacora);
            this.Controls.Add(this.groupBox_FiltrosDeBusqueda);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frm_Bitacora";
            this.Text = "frm_Bitacora";
            this.Load += new System.EventHandler(this.frm_Bitacora_Load);
            this.groupBox_FiltrosDeBusqueda.ResumeLayout(false);
            this.groupBox_FiltrosDeBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_FiltrosDeBusqueda;
        internal System.Windows.Forms.TextBox txtBuscarPorIP;
        private System.Windows.Forms.ComboBox cboModulo;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lbl_IP;
        private System.Windows.Forms.Label lbl_Modulo;
        private System.Windows.Forms.Label lbl_Usuario;
        private System.Windows.Forms.Label lblFecha_Hasta;
        private System.Windows.Forms.Label lbl_FechaDesde;
        private System.Windows.Forms.DataGridView dgvBitacora;
        private System.Windows.Forms.Button btnActualizarBitacora;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.RichTextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtNombreHost;
        private System.Windows.Forms.Label lblNombreHost;
        private System.Windows.Forms.Label lblTotalDeRegistros;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtModulo;
        private System.Windows.Forms.TextBox txtAccion;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblAccion;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblFechaHora;
        private System.Windows.Forms.ComboBox cboUsuario;
    }
}