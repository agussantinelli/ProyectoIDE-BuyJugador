namespace WinForms
{
    partial class DetalleVentaForm
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
            dataGridDetalle = new DataGridView();
            lblIdVenta = new Label();
            lblFecha = new Label();
            lblVendedor = new Label();
            lblTotal = new Label();
            btnCerrar = new Button();
            btnAgregarLinea = new Button();
            btnEliminarLinea = new Button();
            btnEditarCantidad = new Button();
            btnConfirmarCambios = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridDetalle).BeginInit();
            SuspendLayout();
            // 
            // dataGridDetalle
            // 
            dataGridDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridDetalle.Location = new Point(12, 77);
            dataGridDetalle.MultiSelect = false;
            dataGridDetalle.Name = "dataGridDetalle";
            dataGridDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridDetalle.Size = new Size(760, 329);
            dataGridDetalle.TabIndex = 0;
            dataGridDetalle.CellEndEdit += dataGridDetalle_CellEndEdit;
            dataGridDetalle.SelectionChanged += dataGridDetalle_SelectionChanged;
            // 
            // lblIdVenta
            // 
            lblIdVenta.AutoSize = true;
            lblIdVenta.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblIdVenta.Location = new Point(12, 10);
            lblIdVenta.Name = "lblIdVenta";
            lblIdVenta.Size = new Size(95, 21);
            lblIdVenta.TabIndex = 1;
            lblIdVenta.Text = "ID Venta: ...";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(12, 44);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(75, 20);
            lblFecha.TabIndex = 2;
            lblFecha.Text = "Fecha: ...";
            // 
            // lblVendedor
            // 
            lblVendedor.AutoSize = true;
            lblVendedor.Location = new Point(200, 44);
            lblVendedor.Name = "lblVendedor";
            lblVendedor.Size = new Size(104, 20);
            lblVendedor.TabIndex = 3;
            lblVendedor.Text = "Vendedor: ...";
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblTotal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotal.Location = new Point(572, 419);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(200, 28);
            lblTotal.TabIndex = 4;
            lblTotal.Text = "Total: $0.00";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCerrar.Location = new Point(697, 464);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 34);
            btnCerrar.TabIndex = 5;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnAgregarLinea
            // 
            btnAgregarLinea.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAgregarLinea.Location = new Point(12, 464);
            btnAgregarLinea.Name = "btnAgregarLinea";
            btnAgregarLinea.Size = new Size(120, 34);
            btnAgregarLinea.TabIndex = 6;
            btnAgregarLinea.Text = "Agregar Producto";
            btnAgregarLinea.UseVisualStyleBackColor = true;
            btnAgregarLinea.Click += btnAgregarLinea_Click;
            // 
            // btnEliminarLinea
            // 
            btnEliminarLinea.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEliminarLinea.Location = new Point(138, 464);
            btnEliminarLinea.Name = "btnEliminarLinea";
            btnEliminarLinea.Size = new Size(120, 34);
            btnEliminarLinea.TabIndex = 7;
            btnEliminarLinea.Text = "Eliminar Producto";
            btnEliminarLinea.UseVisualStyleBackColor = true;
            btnEliminarLinea.Click += btnEliminarLinea_Click;
            // 
            // btnEditarCantidad
            // 
            btnEditarCantidad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditarCantidad.Location = new Point(264, 464);
            btnEditarCantidad.Name = "btnEditarCantidad";
            btnEditarCantidad.Size = new Size(120, 34);
            btnEditarCantidad.TabIndex = 8;
            btnEditarCantidad.Text = "Editar Cantidad";
            btnEditarCantidad.UseVisualStyleBackColor = true;
            btnEditarCantidad.Click += btnEditarCantidad_Click;
            // 
            // btnConfirmarCambios
            // 
            btnConfirmarCambios.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirmarCambios.Enabled = false;
            btnConfirmarCambios.Location = new Point(551, 464);
            btnConfirmarCambios.Name = "btnConfirmarCambios";
            btnConfirmarCambios.Size = new Size(140, 34);
            btnConfirmarCambios.TabIndex = 9;
            btnConfirmarCambios.Text = "Confirmar Cambios";
            btnConfirmarCambios.UseVisualStyleBackColor = true;
            btnConfirmarCambios.Click += btnConfirmarCambios_Click;
            // 
            // DetalleVentaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 511);
            Controls.Add(btnConfirmarCambios);
            Controls.Add(btnEditarCantidad);
            Controls.Add(btnEliminarLinea);
            Controls.Add(btnAgregarLinea);
            Controls.Add(btnCerrar);
            Controls.Add(lblTotal);
            Controls.Add(lblVendedor);
            Controls.Add(lblFecha);
            Controls.Add(lblIdVenta);
            Controls.Add(dataGridDetalle);
            MinimumSize = new Size(800, 550);
            Name = "DetalleVentaForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Detalle de Venta";
            Load += DetalleVentaForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridDetalle;
        private System.Windows.Forms.Label lblIdVenta;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblVendedor;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnAgregarLinea;
        private System.Windows.Forms.Button btnConfirmarCambios;
        private System.Windows.Forms.Button btnEliminarLinea;
        private System.Windows.Forms.Button btnEditarCantidad;
    }
}

