namespace WinForms
{
    partial class EmpleadoForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlMenu;
        private Panel pnlContenido;
        private Button btnProductos;
        private Button btnProvincias;
        private Button btnVolver;
        private Label lblTitulo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlMenu = new Panel();
            btnProductos = new Button();
            btnVolver = new Button();
            lblTitulo = new Label();
            btnProvincias = new Button();
            pnlContenido = new Panel();
            pnlMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.LightSteelBlue;
            pnlMenu.Controls.Add(btnProductos);
            pnlMenu.Controls.Add(btnVolver);
            pnlMenu.Controls.Add(lblTitulo);
            pnlMenu.Controls.Add(btnProvincias);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(215, 600);
            pnlMenu.TabIndex = 1;
            // 
            // btnProductos
            // 
            btnProductos.Location = new Point(20, 80);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(160, 40);
            btnProductos.TabIndex = 0;
            btnProductos.Text = "Productos";
            btnProductos.Click += btnProductos_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(20, 200);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(160, 40);
            btnVolver.TabIndex = 1;
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(64, 25);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Menú Empleado";
            // 
            // btnProvincias
            // 
            btnProvincias.Location = new Point(20, 140);
            btnProvincias.Name = "btnProvincias";
            btnProvincias.Size = new Size(160, 40);
            btnProvincias.TabIndex = 3;
            btnProvincias.Text = "Provincias";
            btnProvincias.Click += btnProvincias_Click;
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.White;
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(200, 0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(700, 600);
            pnlContenido.TabIndex = 0;
            // 
            // EmpleadoForm
            // 
            ClientSize = new Size(900, 600);
            Controls.Add(pnlContenido);
            Controls.Add(pnlMenu);
            Name = "EmpleadoForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Panel del Empleado";
            pnlMenu.ResumeLayout(false);
            pnlMenu.PerformLayout();
            ResumeLayout(false);
        }
    }
}
