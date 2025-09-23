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

            // lblBuscar
            lblBuscar.Location = new Point(20, 10);
            lblBuscar.Text = "Buscar:";
            lblBuscar.AutoSize = true;

            // txtBuscar
            txtBuscar.Location = new Point(80, 7);
            txtBuscar.Size = new Size(600, 23);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // dgvTiposProducto
            dgvTiposProducto.Location = new Point(20, 40);
            dgvTiposProducto.Size = new Size(1040, 400);
            dgvTiposProducto.ReadOnly = true;
            dgvTiposProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTiposProducto.MultiSelect = false;
            dgvTiposProducto.SelectionChanged += dgvTiposProducto_SelectionChanged;

            // Botones
            btnVolver.Location = new Point(20, 470);
            btnVolver.Size = new Size(100, 30);
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;

            btnNuevo.Location = new Point(140, 470);
            btnNuevo.Size = new Size(100, 30);
            btnNuevo.Text = "Nuevo";
            btnNuevo.Click += btnNuevo_Click;

            btnEditar.Location = new Point(260, 470);
            btnEditar.Size = new Size(100, 30);
            btnEditar.Text = "Editar";
            btnEditar.Click += btnEditar_Click;

            btnEliminar.Location = new Point(380, 470);
            btnEliminar.Size = new Size(100, 30);
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;

            // TipoProducto
            ClientSize = new Size(1100, 520);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(dgvTiposProducto);
            Controls.Add(btnNuevo);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(btnVolver);
            Name = "TipoProducto";
            Text = "Gestión de Tipos de Producto";
            Load += TipoProductoForm_Load;

            ((System.ComponentModel.ISupportInitialize)dgvTiposProducto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
