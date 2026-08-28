namespace Vista
{
    partial class frmBaseCorrupta
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
            this.groupBox_BaseCorrupta = new System.Windows.Forms.GroupBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblBaseCorrupta = new System.Windows.Forms.Label();
            this.groupBox_BaseCorrupta.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_BaseCorrupta
            // 
            this.groupBox_BaseCorrupta.Controls.Add(this.btnIngresar);
            this.groupBox_BaseCorrupta.Controls.Add(this.txtContrasena);
            this.groupBox_BaseCorrupta.Controls.Add(this.txtEmail);
            this.groupBox_BaseCorrupta.Controls.Add(this.lblContrasena);
            this.groupBox_BaseCorrupta.Controls.Add(this.lblEmail);
            this.groupBox_BaseCorrupta.Controls.Add(this.lblBaseCorrupta);
            this.groupBox_BaseCorrupta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_BaseCorrupta.Location = new System.Drawing.Point(471, 57);
            this.groupBox_BaseCorrupta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_BaseCorrupta.Name = "groupBox_BaseCorrupta";
            this.groupBox_BaseCorrupta.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_BaseCorrupta.Size = new System.Drawing.Size(1064, 671);
            this.groupBox_BaseCorrupta.TabIndex = 0;
            this.groupBox_BaseCorrupta.TabStop = false;
            this.groupBox_BaseCorrupta.Text = "Base de Datos Corrupta";
            // 
            // btnIngresar
            // 
            this.btnIngresar.Location = new System.Drawing.Point(384, 566);
            this.btnIngresar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(250, 69);
            this.btnIngresar.TabIndex = 5;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = true;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // txtContrasena
            // 
            this.txtContrasena.Location = new System.Drawing.Point(420, 420);
            this.txtContrasena.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.PasswordChar = '*';
            this.txtContrasena.Size = new System.Drawing.Size(391, 35);
            this.txtContrasena.TabIndex = 4;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(420, 289);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(391, 35);
            this.txtEmail.TabIndex = 3;
            // 
            // lblContrasena
            // 
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.Location = new System.Drawing.Point(126, 429);
            this.lblContrasena.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(153, 29);
            this.lblContrasena.TabIndex = 2;
            this.lblContrasena.Text = "Contraseña:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(126, 291);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(86, 29);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Email:";
            // 
            // lblBaseCorrupta
            // 
            this.lblBaseCorrupta.AutoSize = true;
            this.lblBaseCorrupta.Location = new System.Drawing.Point(186, 68);
            this.lblBaseCorrupta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBaseCorrupta.Name = "lblBaseCorrupta";
            this.lblBaseCorrupta.Size = new System.Drawing.Size(0, 29);
            this.lblBaseCorrupta.TabIndex = 0;
            // 
            // frmBaseCorrupta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 832);
            this.Controls.Add(this.groupBox_BaseCorrupta);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "frmBaseCorrupta";
            this.Text = "frmBaseCorrupta";
            this.Load += new System.EventHandler(this.frmBaseCorrupta_Load);
            this.groupBox_BaseCorrupta.ResumeLayout(false);
            this.groupBox_BaseCorrupta.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_BaseCorrupta;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblBaseCorrupta;
    }
}