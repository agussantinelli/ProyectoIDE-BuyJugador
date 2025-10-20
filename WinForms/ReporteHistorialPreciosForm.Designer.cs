namespace WinForms
{
    partial class ReporteHistorialPreciosForm
    {
        private System.ComponentModel.IContainer components = null;
        private ScottPlot.WinForms.FormsPlot formsPlot1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Dock = System.Windows.Forms.DockStyle.Fill; 
            this.formsPlot1.Location = new System.Drawing.Point(0, 0);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(800, 450);
            this.formsPlot1.TabIndex = 0;
            // 
            // HistorialPreciosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.formsPlot1);
            this.Name = "HistorialPreciosForm";
            this.Text = "Reporte de Evolución de Precios";
            this.Load += new System.EventHandler(this.HistorialPreciosForm_Load);
            this.ResumeLayout(false);
        }
    }
}