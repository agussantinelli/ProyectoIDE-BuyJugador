namespace WinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnProvincia = new System.Windows.Forms.Button();
            this.btnTipoProducto = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnProvincia
            // 
            this.btnProvincia.Location = new System.Drawing.Point(118, 56);
            this.btnProvincia.Name = "btnProvincia";
            this.btnProvincia.Size = new System.Drawing.Size(200, 50);
            this.btnProvincia.TabIndex = 0;
            this.btnProvincia.Text = "Gestionar Provincias";
            this.btnProvincia.UseVisualStyleBackColor = true;
            this.btnProvincia.Click += new System.EventHandler(this.btnProvincia_Click);
            // 
            // btnTipoProducto
            // 
            this.btnTipoProducto.Location = new System.Drawing.Point(118, 128);
            this.btnTipoProducto.Name = "btnTipoProducto";
            this.btnTipoProducto.Size = new System.Drawing.Size(200, 50);
            this.btnTipoProducto.TabIndex = 1;
            this.btnTipoProducto.Text = "Gestionar Tipos de Producto";
            this.btnTipoProducto.UseVisualStyleBackColor = true;
            this.btnTipoProducto.Click += new System.EventHandler(this.btnTipoProducto_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 231);
            this.Controls.Add(this.btnTipoProducto);
            this.Controls.Add(this.btnProvincia);
            this.Name = "MainForm";
            this.Text = "Menú Principal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProvincia;
        private System.Windows.Forms.Button btnTipoProducto;
    }
}
