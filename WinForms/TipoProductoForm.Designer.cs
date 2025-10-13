namespace WinForms
{
    partial class TipoProductoForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvTiposProducto;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblBuscar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtBuscar = new TextBox();
            dgvTiposProducto = new DataGridView();
            btnNuevo = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnVolver = new Button();
            lblBuscar = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTiposProducto).BeginInit();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(103, 9);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(600, 25);
            txtBuscar.TabIndex = 1;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // dgvTiposProducto
            // 
            dgvTiposProducto.Location = new Point(20, 40);
            dgvTiposProducto.MultiSelect = false;
            dgvTiposProducto.Name = "dgvTiposProducto";
            dgvTiposProducto.ReadOnly = true;
            dgvTiposProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTiposProducto.Size = new Size(1040, 400);
            dgvTiposProducto.TabIndex = 2;
            dgvTiposProducto.SelectionChanged += dgvTiposProducto_SelectionChanged;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(140, 470);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(100, 30);
            btnNuevo.TabIndex = 3;
            btnNuevo.Text = "Nuevo";
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(260, 470);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(100, 30);
            btnEditar.TabIndex = 4;
            btnEditar.Text = "Editar";
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(380, 470);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(100, 30);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(20, 470);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 30);
            btnVolver.TabIndex = 6;
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(20, 10);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(62, 20);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Buscar:";
            // 
            // TipoProductoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            ClientSize = new Size(1100, 520);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(dgvTiposProducto);
            Controls.Add(btnNuevo);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(btnVolver);
            Name = "TipoProductoForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Tipos de Producto";
            Load += TipoProductoForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTiposProducto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
