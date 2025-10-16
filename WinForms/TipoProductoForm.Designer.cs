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
        private System.Windows.Forms.Button btnVerProductos; // #NUEVO

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
            btnVerProductos = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTiposProducto).BeginInit();
            SuspendLayout();
            // 
            // txtBuscar
            // 
<<<<<<< HEAD
            txtBuscar.Location = new Point(97, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(330, 25);
=======
            txtBuscar.Location = new Point(116, 9);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(453, 30);
>>>>>>> e07ebd2500fecea977caaf8569c1e82532d1fe90
            txtBuscar.TabIndex = 1;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // dgvTiposProducto
            // 
<<<<<<< HEAD
            dgvTiposProducto.Location = new Point(18, 52);
            dgvTiposProducto.MultiSelect = false;
            dgvTiposProducto.Name = "dgvTiposProducto";
            dgvTiposProducto.ReadOnly = true;
            dgvTiposProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTiposProducto.Size = new Size(576, 327);
=======
            dgvTiposProducto.ColumnHeadersHeight = 29;
            dgvTiposProducto.Location = new Point(22, 54);
            dgvTiposProducto.MultiSelect = false;
            dgvTiposProducto.Name = "dgvTiposProducto";
            dgvTiposProducto.ReadOnly = true;
            dgvTiposProducto.RowHeadersWidth = 51;
            dgvTiposProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTiposProducto.Size = new Size(663, 400);
>>>>>>> e07ebd2500fecea977caaf8569c1e82532d1fe90
            dgvTiposProducto.TabIndex = 2;
            dgvTiposProducto.SelectionChanged += dgvTiposProducto_SelectionChanged;
            // 
            // btnNuevo
            // 
<<<<<<< HEAD
            btnNuevo.Location = new Point(118, 400);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(88, 26);
=======
            btnNuevo.Location = new Point(152, 470);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(112, 30);
>>>>>>> e07ebd2500fecea977caaf8569c1e82532d1fe90
            btnNuevo.TabIndex = 3;
            btnNuevo.Text = "Nuevo";
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEditar
            // 
<<<<<<< HEAD
            btnEditar.Location = new Point(211, 400);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(88, 26);
=======
            btnEditar.Location = new Point(271, 470);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(112, 30);
>>>>>>> e07ebd2500fecea977caaf8569c1e82532d1fe90
            btnEditar.TabIndex = 4;
            btnEditar.Text = "Editar";
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
<<<<<<< HEAD
            btnEliminar.Location = new Point(304, 400);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(88, 26);
=======
            btnEliminar.Location = new Point(390, 470);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(112, 30);
>>>>>>> e07ebd2500fecea977caaf8569c1e82532d1fe90
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnVolver
            // 
<<<<<<< HEAD
            btnVolver.Location = new Point(18, 400);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(88, 26);
=======
            btnVolver.Location = new Point(22, 470);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(112, 30);
>>>>>>> e07ebd2500fecea977caaf8569c1e82532d1fe90
            btnVolver.TabIndex = 6;
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
<<<<<<< HEAD
            lblBuscar.Location = new Point(18, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(62, 20);
=======
            lblBuscar.Location = new Point(22, 12);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(76, 22);
>>>>>>> e07ebd2500fecea977caaf8569c1e82532d1fe90
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Buscar:";
            // 
            // btnVerProductos
            // 
<<<<<<< HEAD
            btnVerProductos.Location = new Point(549, 400);
            btnVerProductos.Name = "btnVerProductos";
            btnVerProductos.Size = new Size(116, 26);
=======
            btnVerProductos.Location = new Point(574, 470);
            btnVerProductos.Name = "btnVerProductos";
            btnVerProductos.Size = new Size(150, 30);
>>>>>>> e07ebd2500fecea977caaf8569c1e82532d1fe90
            btnVerProductos.TabIndex = 7;
            btnVerProductos.Text = "Ver Productos";
            btnVerProductos.UseVisualStyleBackColor = true;
            btnVerProductos.Click += btnVerProductos_Click;
            // 
            // TipoProductoForm
            // 
<<<<<<< HEAD
            AutoScaleDimensions = new SizeF(7F, 17F);
            ClientSize = new Size(688, 442);
=======
            AutoScaleDimensions = new SizeF(9F, 20F);
            ClientSize = new Size(751, 520);
>>>>>>> e07ebd2500fecea977caaf8569c1e82532d1fe90
            Controls.Add(btnVerProductos);
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
