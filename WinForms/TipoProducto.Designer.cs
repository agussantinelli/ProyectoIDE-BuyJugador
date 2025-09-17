namespace WinForms
{
    partial class TipoProducto
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
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvTiposProducto = new System.Windows.Forms.DataGridView();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblBuscar = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvTiposProducto)).BeginInit();
            this.SuspendLayout();

            // lblBuscar
            this.lblBuscar.Location = new System.Drawing.Point(20, 10);
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.AutoSize = true;

            // txtBuscar
            this.txtBuscar.Location = new System.Drawing.Point(20, 35);
            this.txtBuscar.Size = new System.Drawing.Size(400, 23);
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);

            // dgvTiposProducto
            this.dgvTiposProducto.Location = new System.Drawing.Point(20, 70);
            this.dgvTiposProducto.Size = new System.Drawing.Size(740, 280);
            this.dgvTiposProducto.ReadOnly = true;
            this.dgvTiposProducto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTiposProducto.MultiSelect = false;
            this.dgvTiposProducto.SelectionChanged += new System.EventHandler(this.dgvTiposProducto_SelectionChanged);

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

            // TipoProducto (Form)
            this.ClientSize = new System.Drawing.Size(780, 420);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.dgvTiposProducto);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnVolver);
            this.Name = "TipoProducto";
            this.Text = "Gestión de Tipos de Producto";
            this.Load += new System.EventHandler(this.TipoProductoForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvTiposProducto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
