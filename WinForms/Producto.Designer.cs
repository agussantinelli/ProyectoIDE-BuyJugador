namespace WinForms
{
    partial class Producto
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvProductos;
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
            this.txtBuscar = new TextBox();
            this.dgvProductos = new DataGridView();
            this.btnNuevo = new Button();
            this.btnEditar = new Button();
            this.btnEliminar = new Button();
            this.btnVolver = new Button();
            this.lblBuscar = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();

            // lblBuscar
            this.lblBuscar.Location = new System.Drawing.Point(20, 10);
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.AutoSize = true;

            // txtBuscar
            this.txtBuscar.Location = new System.Drawing.Point(20, 35);
            this.txtBuscar.Size = new System.Drawing.Size(400, 23);
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);

            // dgvProductos
            this.dgvProductos.Location = new System.Drawing.Point(20, 70);
            this.dgvProductos.Size = new System.Drawing.Size(740, 280);
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.MultiSelect = false;

            // btnNuevo
            this.btnNuevo.Location = new System.Drawing.Point(20, 370);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Size = new System.Drawing.Size(100, 30);
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);

            // btnEditar
            this.btnEditar.Location = new System.Drawing.Point(140, 370);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Size = new System.Drawing.Size(100, 30);
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);

            // btnEliminar
            this.btnEliminar.Location = new System.Drawing.Point(260, 370);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 30);
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            // btnVolver
            this.btnVolver.Location = new System.Drawing.Point(380, 370);
            this.btnVolver.Text = "Volver";
            this.btnVolver.Size = new System.Drawing.Size(100, 30);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);

            // Producto (Form)
            this.ClientSize = new System.Drawing.Size(780, 420);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnVolver);
            this.Name = "Producto";
            this.Text = "Gestión de Productos";
            this.Load += new System.EventHandler(this.ProductoForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
