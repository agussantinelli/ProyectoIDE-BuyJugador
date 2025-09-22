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
            btnMenuProductos = new Button();
            btnMenuPersonas = new Button();
            btnTipoProducto = new Button();
            btnProvincia = new Button();
            SuspendLayout();

            // Botón base config
            Size botonSize = new Size(280, 60);
            int leftMargin = 110;
            int topStart = 40;
            int verticalSpacing = 80;

            // 
            // btnMenuProductos
            // 
            btnMenuProductos.Location = new Point(leftMargin, topStart);
            btnMenuProductos.Name = "btnMenuProductos";
            btnMenuProductos.Size = botonSize;
            btnMenuProductos.TabIndex = 0;
            btnMenuProductos.Text = "Gestionar Productos";
            btnMenuProductos.UseVisualStyleBackColor = true;
            btnMenuProductos.Click += btnMenuProductos_Click;

            // 
            // btnMenuPersonas
            // 
            btnMenuPersonas.Location = new Point(leftMargin, topStart + verticalSpacing);
            btnMenuPersonas.Name = "btnMenuPersonas";
            btnMenuPersonas.Size = botonSize;
            btnMenuPersonas.TabIndex = 1;
            btnMenuPersonas.Text = "Gestionar Personas";
            btnMenuPersonas.UseVisualStyleBackColor = true;
            btnMenuPersonas.Click += btnMenuPersonas_Click;

            // 
            // btnTipoProducto
            // 
            btnTipoProducto.Location = new Point(leftMargin, topStart + 2 * verticalSpacing);
            btnTipoProducto.Name = "btnTipoProducto";
            btnTipoProducto.Size = botonSize;
            btnTipoProducto.TabIndex = 2;
            btnTipoProducto.Text = "Gestionar Tipos de Producto";
            btnTipoProducto.UseVisualStyleBackColor = true;
            btnTipoProducto.Click += btnMenuTiposProducto_Click;

            // 
            // btnProvincia
            // 
            btnProvincia.Location = new Point(leftMargin, topStart + 3 * verticalSpacing);
            btnProvincia.Name = "btnProvincia";
            btnProvincia.Size = botonSize;
            btnProvincia.TabIndex = 3;
            btnProvincia.Text = "Ver Provincias";
            btnProvincia.UseVisualStyleBackColor = true;
            btnProvincia.Click += btnMenuProvincias_Click;

            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 430);
            StartPosition = FormStartPosition.CenterScreen;
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
