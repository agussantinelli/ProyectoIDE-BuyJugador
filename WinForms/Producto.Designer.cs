namespace WinForms
{
    partial class Producto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        private void InitializeComponent()
        {
            dgvProductos = new DataGridView();
            txtNombre = new TextBox();
            txtDescripcion = new TextBox();
            numStock = new NumericUpDown();
            cmbTipoProducto = new ComboBox();
            btnNuevo = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            lblNombre = new Label();
            lblDescripcion = new Label();
            lblStock = new Label();
            lblTipoProducto = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStock).BeginInit();
            SuspendLayout();
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(14, 155);
            dgvProductos.Margin = new Padding(4, 3, 4, 3);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(861, 350);
            dgvProductos.TabIndex = 0;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(133, 15);
            txtNombre.Margin = new Padding(4, 3, 4, 3);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(222, 23);
            txtNombre.TabIndex = 1;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(133, 55);
            txtDescripcion.Margin = new Padding(4, 3, 4, 3);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(222, 23);
            txtDescripcion.TabIndex = 2;
            // 
            // numStock
            // 
            numStock.Location = new Point(133, 95);
            numStock.Margin = new Padding(4, 3, 4, 3);
            numStock.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numStock.Name = "numStock";
            numStock.Size = new Size(222, 23);
            numStock.TabIndex = 3;
            // 
            // cmbTipoProducto
            // 
            cmbTipoProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoProducto.FormattingEnabled = true;
            cmbTipoProducto.Location = new Point(133, 135);
            cmbTipoProducto.Margin = new Padding(4, 3, 4, 3);
            cmbTipoProducto.Name = "cmbTipoProducto";
            cmbTipoProducto.Size = new Size(222, 23);
            cmbTipoProducto.TabIndex = 4;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(758, 12);
            btnNuevo.Margin = new Padding(4, 3, 4, 3);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(117, 35);
            btnNuevo.TabIndex = 5;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(758, 52);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(117, 35);
            btnActualizar.TabIndex = 6;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(758, 92);
            btnEliminar.Margin = new Padding(4, 3, 4, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(117, 35);
            btnEliminar.TabIndex = 7;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(14, 18);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 8;
            lblNombre.Text = "Nombre:";
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(14, 58);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(72, 15);
            lblDescripcion.TabIndex = 9;
            lblDescripcion.Text = "Descripción:";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(14, 97);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(40, 15);
            lblStock.TabIndex = 10;
            lblStock.Text = "Stock:";
            // 
            // lblTipoProducto
            // 
            lblTipoProducto.AutoSize = true;
            lblTipoProducto.Location = new Point(14, 138);
            lblTipoProducto.Name = "lblTipoProducto";
            lblTipoProducto.Size = new Size(89, 15);
            lblTipoProducto.TabIndex = 11;
            lblTipoProducto.Text = "Tipo Producto:";
            // 
            // Producto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(889, 517);
            Controls.Add(lblTipoProducto);
            Controls.Add(lblStock);
            Controls.Add(lblDescripcion);
            Controls.Add(lblNombre);
            Controls.Add(btnEliminar);
            Controls.Add(btnActualizar);
            Controls.Add(btnNuevo);
            Controls.Add(cmbTipoProducto);
            Controls.Add(numStock);
            Controls.Add(txtDescripcion);
            Controls.Add(txtNombre);
            Controls.Add(dgvProductos);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Producto";
            Text = "Gestión de Productos";
            Load += Producto_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStock).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProductos;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private NumericUpDown numStock;
        private ComboBox cmbTipoProducto;
        private Button btnNuevo;
        private Button btnActualizar;
        private Button btnEliminar;
        private Label lblNombre;
        private Label lblDescripcion;
        private Label lblStock;
        private Label lblTipoProducto;
    }
}
