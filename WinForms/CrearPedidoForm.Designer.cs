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
            lblProveedor = new Label();
            dataGridLineasPedido = new DataGridView();
            btnConfirmarPedido = new Button();
            btnAgregarProducto = new Button();
            btnEliminarLinea = new Button();
            lblTotalPedido = new Label();
            btnCancelar = new Button();
            cmbProductos = new ComboBox();
            numCantidad = new NumericUpDown();
            lblProducto = new Label();
            lblCantidad = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridLineasPedido).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            SuspendLayout();
            // 
            // cmbProveedores
            // 
            cmbProveedores.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedores.FormattingEnabled = true;
            cmbProveedores.Location = new Point(108, 12);
            cmbProveedores.Name = "cmbProveedores";
            cmbProveedores.Size = new Size(263, 28);
            cmbProveedores.TabIndex = 0;
            cmbProveedores.SelectedIndexChanged += cmbProveedores_SelectedIndexChanged;
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.Location = new Point(10, 15);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(92, 20);
            lblProveedor.TabIndex = 1;
            lblProveedor.Text = "Proveedor:";
            // 
            // dataGridLineasPedido
            // 
            dataGridLineasPedido.AllowUserToAddRows = false;
            dataGridLineasPedido.AllowUserToDeleteRows = false;
            dataGridLineasPedido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridLineasPedido.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridLineasPedido.Location = new Point(10, 110);
            dataGridLineasPedido.MultiSelect = false;
            dataGridLineasPedido.Name = "dataGridLineasPedido";
            dataGridLineasPedido.ReadOnly = true;
            dataGridLineasPedido.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridLineasPedido.Size = new Size(665, 275);
            dataGridLineasPedido.TabIndex = 2;
            // 
            // btnConfirmarPedido
            // 
            btnConfirmarPedido.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirmarPedido.Location = new Point(561, 400);
            btnConfirmarPedido.Name = "btnConfirmarPedido";
            btnConfirmarPedido.Size = new Size(115, 35);
            btnConfirmarPedido.TabIndex = 3;
            btnConfirmarPedido.Text = "Confirmar Pedido";
            btnConfirmarPedido.UseVisualStyleBackColor = true;
            btnConfirmarPedido.Click += btnConfirmarPedido_Click;
            // 
            // btnAgregarProducto
            // 
            btnAgregarProducto.Location = new Point(451, 67);
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.Size = new Size(105, 28);
            btnAgregarProducto.TabIndex = 4;
            btnAgregarProducto.Text = "Agregar";
            btnAgregarProducto.UseVisualStyleBackColor = true;
            btnAgregarProducto.Click += btnAgregarProducto_Click;
            // 
            // btnEliminarLinea
            // 
            btnEliminarLinea.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminarLinea.Location = new Point(570, 68);
            btnEliminarLinea.Name = "btnEliminarLinea";
            btnEliminarLinea.Size = new Size(105, 28);
            btnEliminarLinea.TabIndex = 5;
            btnEliminarLinea.Text = "Eliminar Línea";
            btnEliminarLinea.UseVisualStyleBackColor = true;
            btnEliminarLinea.Click += btnEliminarLinea_Click;
            // 
            // lblTotalPedido
            // 
            lblTotalPedido.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotalPedido.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalPedido.Location = new Point(10, 400);
            lblTotalPedido.Name = "lblTotalPedido";
            lblTotalPedido.Size = new Size(167, 35);
            lblTotalPedido.TabIndex = 6;
            lblTotalPedido.Text = "Total: $0.00";
            lblTotalPedido.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.Location = new Point(490, 400);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(66, 35);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // cmbProductos
            // 
            cmbProductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProductos.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbProductos.FormattingEnabled = true;
            cmbProductos.Location = new Point(97, 67);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new Size(210, 28);
            cmbProductos.TabIndex = 8;
            // 
            // numCantidad
            // 
            numCantidad.Location = new Point(312, 68);
            numCantidad.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(122, 25);
            numCantidad.TabIndex = 9;
            numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(10, 72);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(81, 20);
            lblProducto.TabIndex = 10;
            lblProducto.Text = "Producto:";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(312, 45);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(82, 20);
            lblCantidad.TabIndex = 11;
            lblCantidad.Text = "Cantidad:";
            // 
            // CrearPedidoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 447);
            Controls.Add(lblCantidad);
            Controls.Add(lblProducto);
            Controls.Add(numCantidad);
            Controls.Add(cmbProductos);
            Controls.Add(btnCancelar);
            Controls.Add(lblTotalPedido);
            Controls.Add(btnEliminarLinea);
            Controls.Add(btnAgregarProducto);
            Controls.Add(btnConfirmarPedido);
            Controls.Add(dataGridLineasPedido);
            Controls.Add(lblProveedor);
            Controls.Add(cmbProveedores);
            MinimumSize = new Size(702, 480);
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
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.DataGridView dataGridLineasPedido;
        private System.Windows.Forms.Button btnConfirmarPedido;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.Button btnEliminarLinea;
        private System.Windows.Forms.Label lblTotalPedido;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cmbProductos;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Label lblCantidad;
    }
}

