namespace WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelLateral;
        private Panel pnlContenido;
        private Button btnProductos;
        private Button btnPersonas;
        private Button btnTipoProducto;
        private Button btnProvincias;
        private Button btnSalir;
        private Label lblTitulo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelLateral = new Panel();
            btnSalir = new Button();
            btnProvincias = new Button();
            btnTipoProducto = new Button();
            btnPersonas = new Button();
            btnProductos = new Button();
            lblTitulo = new Label();
            pnlContenido = new Panel();
            panelLateral.SuspendLayout();
            SuspendLayout();
            // 
            // panelLateral
            // 
            panelLateral.BackColor = Color.LightSteelBlue;
            panelLateral.Controls.Add(btnSalir);
            panelLateral.Controls.Add(btnProvincias);
            panelLateral.Controls.Add(btnTipoProducto);
            panelLateral.Controls.Add(btnPersonas);
            panelLateral.Controls.Add(btnProductos);
            panelLateral.Controls.Add(lblTitulo);
            panelLateral.Dock = DockStyle.Left;
            panelLateral.Location = new Point(0, 0);
            panelLateral.Name = "panelLateral";
            panelLateral.Size = new Size(215, 600);
            panelLateral.TabIndex = 0;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(20, 270);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(160, 40);
            btnSalir.TabIndex = 0;
            btnSalir.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // btnProvincias
            // 
            btnProvincias.Location = new Point(20, 220);
            btnProvincias.Name = "btnProvincias";
            btnProvincias.Size = new Size(160, 40);
            btnProvincias.TabIndex = 1;
            btnProvincias.Text = "Provincias";
            btnProvincias.Click += btnProvincias_Click;
            // 
            // btnTipoProducto
            // 
            btnTipoProducto.Location = new Point(20, 170);
            btnTipoProducto.Name = "btnTipoProducto";
            btnTipoProducto.Size = new Size(160, 40);
            btnTipoProducto.TabIndex = 2;
            btnTipoProducto.Text = "Tipos de Producto";
            btnTipoProducto.Click += btnTipoProducto_Click;
            // 
            // btnPersonas
            // 
            btnPersonas.Location = new Point(20, 120);
            btnPersonas.Name = "btnPersonas";
            btnPersonas.Size = new Size(160, 40);
            btnPersonas.TabIndex = 3;
            btnPersonas.Text = "Personas";
            btnPersonas.Click += btnPersonas_Click;
            // 
            // btnProductos
            // 
            btnProductos.Location = new Point(20, 70);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(160, 40);
            btnProductos.TabIndex = 4;
            btnProductos.Text = "Productos";
            btnProductos.Click += btnProductos_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(11, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(198, 25);
            lblTitulo.TabIndex = 5;
            lblTitulo.Text = "Menú Administrador";
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.White;
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(215, 0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(685, 600);
            pnlContenido.TabIndex = 1;
            // 
            // MainForm
            // 
            ClientSize = new Size(900, 600);
            Controls.Add(pnlContenido);
            Controls.Add(panelLateral);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Panel del Administrador";
            panelLateral.ResumeLayout(false);
            panelLateral.PerformLayout();
            ResumeLayout(false);
        }
    }
}
