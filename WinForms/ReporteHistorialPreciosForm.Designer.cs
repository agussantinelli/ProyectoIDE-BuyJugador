namespace WinForms
{
    partial class ReporteHistorialPreciosForm
    {
        private System.ComponentModel.IContainer components = null;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private System.Windows.Forms.Button btnExportarPdf;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            this.btnExportarPdf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.btnExportarPdf.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExportarPdf.Height = 40;
            this.btnExportarPdf.Text = "Exportar PDF";
            this.btnExportarPdf.UseVisualStyleBackColor = true;
            this.btnExportarPdf.Click += new System.EventHandler(this.btnExportarPdf_Click);
            this.btnExportarPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarPdf.BackColor = System.Drawing.Color.FromArgb(49, 108, 244);
            this.btnExportarPdf.ForeColor = System.Drawing.Color.White;
            this.btnExportarPdf.UseVisualStyleBackColor = false;
            this.formsPlot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot1.Location = new System.Drawing.Point(0, 0);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(800, 410);
            this.formsPlot1.TabIndex = 0;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.formsPlot1);
            this.Controls.Add(this.btnExportarPdf);
            this.Name = "ReporteHistorialPreciosForm";
            this.Text = "Reporte de Evolución de Precios";
            this.Load += new System.EventHandler(this.HistorialPreciosForm_Load);
            this.ResumeLayout(false);
        }
    }
}
