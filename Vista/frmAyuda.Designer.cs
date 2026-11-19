namespace Vista
{
    partial class frmAyuda
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
            this.wbAyuda = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbAyuda
            // 
            this.wbAyuda.Location = new System.Drawing.Point(0, 0);
            this.wbAyuda.Name = "wbAyuda";
            this.wbAyuda.Size = new System.Drawing.Size(760, 600);
            this.wbAyuda.TabIndex = 0;
            this.wbAyuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbAyuda.AllowWebBrowserDrop = false;
            this.wbAyuda.IsWebBrowserContextMenuEnabled = false;
            this.wbAyuda.ScriptErrorsSuppressed = true;
            this.wbAyuda.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbAyuda_DocumentCompleted);
            // 
            // frmAyuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 600);
            this.Controls.Add(this.wbAyuda);
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "frmAyuda";
            this.Text = "Ayuda";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.WebBrowser wbAyuda;
    }
}
