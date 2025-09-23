namespace WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnMenuProductos;
        private System.Windows.Forms.Button btnMenuPersonas;
        private System.Windows.Forms.Button btnTipoProducto;
        private System.Windows.Forms.Button btnProvincia;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnMenuProductos = new System.Windows.Forms.Button();
            btnMenuPersonas = new System.Windows.Forms.Button();
            btnTipoProducto = new System.Windows.Forms.Button();
            btnProvincia = new System.Windows.Forms.Button();
            lblTitle = new System.Windows.Forms.Label();
            SuspendLayout();

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.Location = new Point(0, 20); 
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(230, 30);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Panel del Administrador";

            // 
            // btnMenuProductos
            // 
            btnMenuProductos.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnMenuProductos.Location = new Point(110, 70);
            btnMenuProductos.Name = "btnMenuProductos";
            btnMenuProductos.Size = new Size(280, 50);
            btnMenuProductos.TabIndex = 0;
            btnMenuProductos.Text = "Gestionar Productos";
            btnMenuProductos.UseVisualStyleBackColor = true;
            btnMenuProductos.Click += btnMenuProductos_Click;

            // 
            // btnMenuPersonas
            // 
            btnMenuPersonas.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnMenuPersonas.Location = new Point(110, 130);
            btnMenuPersonas.Name = "btnMenuPersonas";
            btnMenuPersonas.Size = new Size(280, 50);
            btnMenuPersonas.TabIndex = 1;
            btnMenuPersonas.Text = "Gestionar Personas";
            btnMenuPersonas.UseVisualStyleBackColor = true;
            btnMenuPersonas.Click += btnMenuPersonas_Click;

            // 
            // btnTipoProducto
            // 
            btnTipoProducto.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnTipoProducto.Location = new Point(110, 190);
            btnTipoProducto.Name = "btnTipoProducto";
            btnTipoProducto.Size = new Size(280, 50);
            btnTipoProducto.TabIndex = 2;
            btnTipoProducto.Text = "Gestionar Tipos de Producto";
            btnTipoProducto.UseVisualStyleBackColor = true;
            btnTipoProducto.Click += btnMenuTiposProducto_Click;

            // 
            // btnProvincia
            // 
            btnProvincia.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnProvincia.Location = new Point(110, 250);
            btnProvincia.Name = "btnProvincia";
            btnProvincia.Size = new Size(280, 50);
            btnProvincia.TabIndex = 3;
            btnProvincia.Text = "Ver Provincias";
            btnProvincia.UseVisualStyleBackColor = true;
            btnProvincia.Click += btnMenuProvincias_Click;

            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 340);
            Controls.Add(lblTitle);
            Controls.Add(btnMenuProductos);
            Controls.Add(btnMenuPersonas);
            Controls.Add(btnTipoProducto);
            Controls.Add(btnProvincia);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Panel del Administrador";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
