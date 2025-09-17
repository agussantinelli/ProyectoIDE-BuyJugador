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
            btnMenuProductos = new Button();
            btnMenuPersonas = new Button();
            btnTipoProducto = new Button();
            btnProvincia = new Button();
            SuspendLayout();
            // 
            // btnMenuProductos
            // 
            btnMenuProductos.Location = new Point(118, 30);
            btnMenuProductos.Name = "btnMenuProductos";
            btnMenuProductos.Size = new Size(200, 50);
            btnMenuProductos.TabIndex = 0;
            btnMenuProductos.Text = "Gestionar Productos";
            btnMenuProductos.UseVisualStyleBackColor = true;
            btnMenuProductos.Click += btnMenuProductos_Click;
            // 
            // btnMenuPersonas
            // 
            btnMenuPersonas.Location = new Point(118, 95);
            btnMenuPersonas.Name = "btnMenuPersonas";
            btnMenuPersonas.Size = new Size(200, 50);
            btnMenuPersonas.TabIndex = 1;
            btnMenuPersonas.Text = "Gestionar Personas";
            btnMenuPersonas.UseVisualStyleBackColor = true;
            btnMenuPersonas.Click += btnMenuPersonas_Click;
            // 
            // btnTipoProducto
            // 
            btnTipoProducto.Location = new Point(118, 160);
            btnTipoProducto.Name = "btnTipoProducto";
            btnTipoProducto.Size = new Size(200, 50);
            btnTipoProducto.TabIndex = 2;
            btnTipoProducto.Text = "Gestionar Tipos de Producto";
            btnTipoProducto.UseVisualStyleBackColor = true;
            btnTipoProducto.Click += btnMenuTiposProducto_Click;
            // 
            // btnProvincia
            // 
            btnProvincia.Location = new Point(118, 225);
            btnProvincia.Name = "btnProvincia";
            btnProvincia.Size = new Size(200, 50);
            btnProvincia.TabIndex = 3;
            btnProvincia.Text = "Ver Provincias";
            btnProvincia.UseVisualStyleBackColor = true;
            btnProvincia.Click += btnMenuProvincias_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 311);
            Controls.Add(btnProvincia);
            Controls.Add(btnTipoProducto);
            Controls.Add(btnMenuPersonas);
            Controls.Add(btnMenuProductos);
            Name = "MainForm";
            Text = "Menú Principal";
            ResumeLayout(false);

        }

        #endregion

        private Button btnMenuProductos;
        private Button btnMenuPersonas;
        private Button btnTipoProducto;
        private Button btnProvincia;
    }
}
