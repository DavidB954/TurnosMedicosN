namespace Vista
{
    partial class frmMisRecetas
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
            this.tlpPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.dgvRecetas = new System.Windows.Forms.DataGridView();
            this.lblMedicamentos = new System.Windows.Forms.Label();
            this.dgvMedicamentos = new System.Windows.Forms.DataGridView();
            this.tlpAcciones = new System.Windows.Forms.TableLayoutPanel();
            this.lblNota = new System.Windows.Forms.Label();
            this.btnDescargarPdf = new System.Windows.Forms.Button();
            this.tlpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecetas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamentos)).BeginInit();
            this.tlpAcciones.SuspendLayout();
            this.SuspendLayout();
            //
            // tlpPrincipal (filas: titulo, ayuda, recetas, titulo medicamentos, medicamentos, acciones)
            //
            this.tlpPrincipal.ColumnCount = 1;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.Controls.Add(this.lblTitulo, 0, 0);
            this.tlpPrincipal.Controls.Add(this.lblAyuda, 0, 1);
            this.tlpPrincipal.Controls.Add(this.dgvRecetas, 0, 2);
            this.tlpPrincipal.Controls.Add(this.lblMedicamentos, 0, 3);
            this.tlpPrincipal.Controls.Add(this.dgvMedicamentos, 0, 4);
            this.tlpPrincipal.Controls.Add(this.tlpAcciones, 0, 5);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.Padding = new System.Windows.Forms.Padding(14);
            this.tlpPrincipal.RowCount = 6;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tlpPrincipal.Size = new System.Drawing.Size(1300, 760);
            this.tlpPrincipal.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Mis recetas";
            //
            // lblAyuda
            //
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Seleccione una receta para ver sus medicamentos y descargarla en PDF.";
            //
            // dgvRecetas
            //
            this.dgvRecetas.AllowUserToAddRows = false;
            this.dgvRecetas.AllowUserToDeleteRows = false;
            this.dgvRecetas.AllowUserToResizeRows = false;
            this.dgvRecetas.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecetas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecetas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecetas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.dgvRecetas.MultiSelect = false;
            this.dgvRecetas.Name = "dgvRecetas";
            this.dgvRecetas.ReadOnly = true;
            this.dgvRecetas.RowHeadersVisible = false;
            this.dgvRecetas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecetas.TabIndex = 2;
            this.dgvRecetas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecetas_CellDoubleClick);
            this.dgvRecetas.SelectionChanged += new System.EventHandler(this.dgvRecetas_SelectionChanged);
            //
            // lblMedicamentos
            //
            this.lblMedicamentos.AutoSize = true;
            this.lblMedicamentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblMedicamentos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.lblMedicamentos.Margin = new System.Windows.Forms.Padding(3, 14, 3, 6);
            this.lblMedicamentos.Name = "lblMedicamentos";
            this.lblMedicamentos.TabIndex = 3;
            //
            // dgvMedicamentos
            //
            this.dgvMedicamentos.AllowUserToAddRows = false;
            this.dgvMedicamentos.AllowUserToDeleteRows = false;
            this.dgvMedicamentos.AllowUserToResizeRows = false;
            this.dgvMedicamentos.BackgroundColor = System.Drawing.Color.White;
            this.dgvMedicamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicamentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMedicamentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.dgvMedicamentos.MultiSelect = false;
            this.dgvMedicamentos.Name = "dgvMedicamentos";
            this.dgvMedicamentos.ReadOnly = true;
            this.dgvMedicamentos.RowHeadersVisible = false;
            this.dgvMedicamentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicamentos.TabIndex = 4;
            this.dgvMedicamentos.TabStop = false;
            //
            // tlpAcciones (nota a la izquierda, boton a la derecha)
            //
            this.tlpAcciones.ColumnCount = 2;
            this.tlpAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpAcciones.Controls.Add(this.lblNota, 0, 0);
            this.tlpAcciones.Controls.Add(this.btnDescargarPdf, 1, 0);
            this.tlpAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAcciones.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tlpAcciones.Name = "tlpAcciones";
            this.tlpAcciones.RowCount = 1;
            this.tlpAcciones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAcciones.TabIndex = 5;
            //
            // lblNota
            //
            this.lblNota.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this.lblNota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNota.Name = "lblNota";
            this.lblNota.TabIndex = 0;
            this.lblNota.Text = "Documento informativo. La receta oficial la emite el médico en una plataforma de receta electrónica registrada.";
            this.lblNota.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnDescargarPdf
            //
            this.btnDescargarPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnDescargarPdf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDescargarPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescargarPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnDescargarPdf.ForeColor = System.Drawing.Color.White;
            this.btnDescargarPdf.Margin = new System.Windows.Forms.Padding(8, 4, 3, 4);
            this.btnDescargarPdf.Name = "btnDescargarPdf";
            this.btnDescargarPdf.TabIndex = 1;
            this.btnDescargarPdf.Text = "Descargar PDF";
            this.btnDescargarPdf.UseVisualStyleBackColor = false;
            this.btnDescargarPdf.Click += new System.EventHandler(this.btnDescargarPdf_Click);
            //
            // frmMisRecetas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1300, 760);
            this.Controls.Add(this.tlpPrincipal);
            this.Name = "frmMisRecetas";
            this.Text = "Mis recetas";
            this.Load += new System.EventHandler(this.frmMisRecetas_Load);
            this.tlpPrincipal.ResumeLayout(false);
            this.tlpPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecetas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamentos)).EndInit();
            this.tlpAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.DataGridView dgvRecetas;
        private System.Windows.Forms.Label lblMedicamentos;
        private System.Windows.Forms.DataGridView dgvMedicamentos;
        private System.Windows.Forms.TableLayoutPanel tlpAcciones;
        private System.Windows.Forms.Label lblNota;
        private System.Windows.Forms.Button btnDescargarPdf;
    }
}
