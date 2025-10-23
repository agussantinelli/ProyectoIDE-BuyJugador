namespace WinForms
{
    partial class AñadirProductoPedidoForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;

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
            Precio = new DataGridViewTextBoxColumn();
            txtCantidad = new TextBox();
            lblCantidad = new Label();
            btnAceptar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Columns.AddRange(new DataGridViewColumn[] { Precio });
            dgvProductos.Location = new Point(12, 14);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(694, 330);
            dgvProductos.TabIndex = 0;
            Precio.DataPropertyName = "PrecioCompra";
            Precio.HeaderText = "Precio";
            Precio.Name = "Precio";
            Precio.ReadOnly = true;
            txtCantidad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtCantidad.Location = new Point(113, 358);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(100, 25);
            txtCantidad.TabIndex = 1;
            lblCantidad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(12, 361);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(82, 20);
            lblCantidad.TabIndex = 2;
            lblCantidad.Text = "Cantidad:";
            btnAceptar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAceptar.Location = new Point(550, 353);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(75, 33);
            btnAceptar.TabIndex = 3;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.Location = new Point(631, 353);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 33);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(718, 399);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(lblCantidad);
            Controls.Add(txtCantidad);
            Controls.Add(dgvProductos);
            Name = "AñadirProductoPedidoForm";
            Text = "Añadir Producto al Pedido";
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
    }
}
