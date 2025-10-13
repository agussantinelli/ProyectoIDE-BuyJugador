namespace WinForms
{
    partial class CrearPedidoForm
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
            cmbProveedores = new ComboBox();
            btnAgregarProducto = new Button();
            dataGridLineasPedido = new DataGridView();
            btnEliminarLinea = new Button();
            btnConfirmarPedido = new Button();
            btnCancelar = new Button();
            lblProveedor = new Label();
            lblTotalPedido = new Label();
            lblProducto = new Label();
            cmbProductos = new ComboBox();
            lblCantidad = new Label();
            numCantidad = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)dataGridLineasPedido).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            SuspendLayout();
            // 
            // cmbProveedores
            // 
            cmbProveedores.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedores.FormattingEnabled = true;
            cmbProveedores.Location = new Point(121, 28);
            cmbProveedores.Name = "cmbProveedores";
            cmbProveedores.Size = new Size(278, 28);
            cmbProveedores.TabIndex = 0;
            cmbProveedores.SelectedIndexChanged += cmbProveedores_SelectedIndexChanged;
            // 
            // btnAgregarProducto
            // 
            btnAgregarProducto.Location = new Point(509, 75);
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.Size = new Size(120, 34);
            btnAgregarProducto.TabIndex = 4;
            btnAgregarProducto.Text = "Agregar Producto";
            btnAgregarProducto.UseVisualStyleBackColor = true;
            btnAgregarProducto.Click += btnAgregarProducto_Click;
            // 
            // dataGridLineasPedido
            // 
            dataGridLineasPedido.AllowUserToAddRows = false;
            dataGridLineasPedido.AllowUserToDeleteRows = false;
            dataGridLineasPedido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridLineasPedido.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridLineasPedido.Location = new Point(15, 125);
            dataGridLineasPedido.Name = "dataGridLineasPedido";
            dataGridLineasPedido.ReadOnly = true;
            dataGridLineasPedido.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridLineasPedido.Size = new Size(603, 220);
            dataGridLineasPedido.TabIndex = 6;
            // 
            // btnEliminarLinea
            // 
            btnEliminarLinea.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEliminarLinea.Location = new Point(121, 362);
            btnEliminarLinea.Name = "btnEliminarLinea";
            btnEliminarLinea.Size = new Size(120, 34);
            btnEliminarLinea.TabIndex = 8;
            btnEliminarLinea.Text = "Eliminar Producto";
            btnEliminarLinea.UseVisualStyleBackColor = true;
            btnEliminarLinea.Click += btnEliminarLinea_Click;
            // 
            // btnConfirmarPedido
            // 
            btnConfirmarPedido.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirmarPedido.Location = new Point(498, 362);
            btnConfirmarPedido.Name = "btnConfirmarPedido";
            btnConfirmarPedido.Size = new Size(120, 34);
            btnConfirmarPedido.TabIndex = 9;
            btnConfirmarPedido.Text = "Confirmar Pedido";
            btnConfirmarPedido.UseVisualStyleBackColor = true;
            btnConfirmarPedido.Click += btnConfirmarPedido_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancelar.Location = new Point(15, 362);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 34);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.Location = new Point(15, 31);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(92, 20);
            lblProveedor.TabIndex = 11;
            lblProveedor.Text = "Proveedor:";
            // 
            // lblTotalPedido
            // 
            lblTotalPedido.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblTotalPedido.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblTotalPedido.Location = new Point(354, 362);
            lblTotalPedido.Name = "lblTotalPedido";
            lblTotalPedido.Size = new Size(138, 34);
            lblTotalPedido.TabIndex = 10;
            lblTotalPedido.Text = "Total: $0.00";
            lblTotalPedido.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(15, 82);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(81, 20);
            lblProducto.TabIndex = 12;
            lblProducto.Text = "Producto:";
            // 
            // cmbProductos
            // 
            cmbProductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProductos.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbProductos.FormattingEnabled = true;
            cmbProductos.Location = new Point(102, 78);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new Size(236, 28);
            cmbProductos.TabIndex = 1;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(354, 82);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(49, 20);
            lblCantidad.TabIndex = 13;
            lblCantidad.Text = "Cant:";
            // 
            // numCantidad
            // 
            numCantidad.Location = new Point(425, 79);
            numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(67, 25);
            numCantidad.TabIndex = 3;
            numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // CrearPedidoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(630, 409);
            Controls.Add(numCantidad);
            Controls.Add(lblCantidad);
            Controls.Add(cmbProductos);
            Controls.Add(lblProducto);
            Controls.Add(lblTotalPedido);
            Controls.Add(lblProveedor);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmarPedido);
            Controls.Add(btnEliminarLinea);
            Controls.Add(dataGridLineasPedido);
            Controls.Add(btnAgregarProducto);
            Controls.Add(cmbProveedores);
            MinimumSize = new Size(600, 448);
            Name = "CrearPedidoForm";
            Text = "Crear Nuevo Pedido";
            Load += CrearPedidoForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridLineasPedido).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProveedores;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.DataGridView dataGridLineasPedido;
        private System.Windows.Forms.Button btnEliminarLinea;
        private System.Windows.Forms.Button btnConfirmarPedido;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Label lblTotalPedido;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cmbProductos;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown numCantidad;
    }
}

