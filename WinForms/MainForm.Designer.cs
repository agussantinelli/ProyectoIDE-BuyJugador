namespace WinForms
{
    partial class MainForm
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
            this.btnProvincias = new System.Windows.Forms.Button();
            this.btnTiposProducto = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // btnProvincias
            //
            this.btnProvincias.Location = new System.Drawing.Point(50, 50);
            this.btnProvincias.Name = "btnProvincias";
            this.btnProvincias.Size = new System.Drawing.Size(150, 50);
            this.btnProvincias.TabIndex = 0;
            this.btnProvincias.Text = "Provincias";
            this.btnProvincias.UseVisualStyleBackColor = true;
            this.btnProvincias.Click += new System.EventHandler(this.btnProvincias_Click);
            //
            // btnTiposProducto
            //
            this.btnTiposProducto.Location = new System.Drawing.Point(50, 120);
            this.btnTiposProducto.Name = "btnTiposProducto";
            this.btnTiposProducto.Size = new System.Drawing.Size(150, 50);
            this.btnTiposProducto.TabIndex = 1;
            this.btnTiposProducto.Text = "Tipos de Producto";
            this.btnTiposProducto.UseVisualStyleBackColor = true;
            this.btnTiposProducto.Click += new System.EventHandler(this.btnTiposProducto_Click);
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 250);
            this.Controls.Add(this.btnTiposProducto);
            this.Controls.Add(this.btnProvincias);
            this.Name = "MainForm";
            this.Text = "Menú Principal";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnProvincias;
        private System.Windows.Forms.Button btnTiposProducto;
    }
}