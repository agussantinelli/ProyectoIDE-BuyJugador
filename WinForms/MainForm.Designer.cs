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
            btnProvincia = new Button();
            btnTipoProducto = new Button();
            btnMenuProductos = new Button();
            SuspendLayout();
            // 
            // btnProvincia
            // 
            btnProvincia.Location = new Point(118, 56);
            btnProvincia.Name = "btnProvincia";
            btnProvincia.Size = new Size(200, 50);
            btnProvincia.TabIndex = 0;
            btnProvincia.Text = "Ver Provincias";
            btnProvincia.UseVisualStyleBackColor = true;
            btnProvincia.Click += btnMenuProvincias_Click;
            // 
            // btnTipoProducto
            // 
            btnTipoProducto.Location = new Point(118, 128);
            btnTipoProducto.Name = "btnTipoProducto";
            btnTipoProducto.Size = new Size(200, 50);
            btnTipoProducto.TabIndex = 1;
            btnTipoProducto.Text = "Gestionar Tipos de Producto";
            btnTipoProducto.UseVisualStyleBackColor = true;
            btnTipoProducto.Click += btnMenuTiposProducto_Click;
            // 
            // btnMenuProductos
            // 
            btnMenuProductos.Location = new Point(118, 205);
            btnMenuProductos.Name = "btnMenuProductos";
            btnMenuProductos.Size = new Size(200, 50);
            btnMenuProductos.TabIndex = 2;
            btnMenuProductos.Text = "Gestionar Productos";
            btnMenuProductos.UseVisualStyleBackColor = true;
            btnMenuProductos.Click += btnMenuProductos_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 292);
            Controls.Add(btnMenuProductos);
            Controls.Add(btnTipoProducto);
            Controls.Add(btnProvincia);
            Name = "MainForm";
            Text = "Menú Principal";
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProvincia;
        private System.Windows.Forms.Button btnTipoProducto;
        private Button btnMenuProductos;
    }
}
