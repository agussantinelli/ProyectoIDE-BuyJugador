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
            dataGridDetalle = new System.Windows.Forms.DataGridView();
            lblIdVenta = new System.Windows.Forms.Label();
            lblFecha = new System.Windows.Forms.Label();
            lblVendedor = new System.Windows.Forms.Label();
            lblTotal = new System.Windows.Forms.Label();
            btnCerrar = new System.Windows.Forms.Button();
            btnAgregarLinea = new System.Windows.Forms.Button();
            btnEliminarLinea = new System.Windows.Forms.Button();
            btnEditarCantidad = new System.Windows.Forms.Button();
            btnConfirmarCambios = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(dataGridDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridDetalle
            // 
            dataGridDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridDetalle.Location = new System.Drawing.Point(12, 77);
            dataGridDetalle.MultiSelect = false;
            dataGridDetalle.Name = "dataGridDetalle";
            dataGridDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridDetalle.Size = new System.Drawing.Size(760, 329);
            dataGridDetalle.TabIndex = 0;
            dataGridDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDetalle_CellEndEdit);
            dataGridDetalle.SelectionChanged += new System.EventHandler(this.dataGridDetalle_SelectionChanged);
            // 
            // lblIdVenta
            // 
            lblIdVenta.AutoSize = true;
            lblIdVenta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblIdVenta.Location = new System.Drawing.Point(12, 10);
            lblIdVenta.Name = "lblIdVenta";
            lblIdVenta.Size = new System.Drawing.Size(95, 21);
            lblIdVenta.TabIndex = 1;
            lblIdVenta.Text = "ID Venta: ...";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new System.Drawing.Point(12, 44);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new System.Drawing.Size(75, 20);
            lblFecha.TabIndex = 2;
            lblFecha.Text = "Fecha: ...";
            // 
            // lblVendedor
            // 
            lblVendedor.AutoSize = true;
            lblVendedor.Location = new System.Drawing.Point(200, 44);
            lblVendedor.Name = "lblVendedor";
            lblVendedor.Size = new System.Drawing.Size(104, 20);
            lblVendedor.TabIndex = 3;
            lblVendedor.Text = "Vendedor: ...";
            // 
            // lblTotal
            // 
            lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTotal.Location = new System.Drawing.Point(572, 9);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new System.Drawing.Size(200, 28);
            lblTotal.TabIndex = 4;
            lblTotal.Text = "Total: $0.00";
            lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnCerrar.Location = new System.Drawing.Point(697, 464);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new System.Drawing.Size(75, 34);
            btnCerrar.TabIndex = 5;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnAgregarLinea
            // 
            btnAgregarLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            btnAgregarLinea.Location = new System.Drawing.Point(12, 464);
            btnAgregarLinea.Name = "btnAgregarLinea";
            btnAgregarLinea.Size = new System.Drawing.Size(120, 34);
            btnAgregarLinea.TabIndex = 6;
            btnAgregarLinea.Text = "Agregar Producto";
            btnAgregarLinea.UseVisualStyleBackColor = true;
            btnAgregarLinea.Click += new System.EventHandler(this.btnAgregarLinea_Click);
            // 
            // btnEliminarLinea
            // 
            btnEliminarLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            btnEliminarLinea.Location = new System.Drawing.Point(138, 464);
            btnEliminarLinea.Name = "btnEliminarLinea";
            btnEliminarLinea.Size = new System.Drawing.Size(120, 34);
            btnEliminarLinea.TabIndex = 7;
            btnEliminarLinea.Text = "Eliminar Producto";
            btnEliminarLinea.UseVisualStyleBackColor = true;
            btnEliminarLinea.Click += new System.EventHandler(this.btnEliminarLinea_Click);
            // 
            // btnEditarCantidad
            // 
            btnEditarCantidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            btnEditarCantidad.Location = new System.Drawing.Point(264, 464);
            btnEditarCantidad.Name = "btnEditarCantidad";
            btnEditarCantidad.Size = new System.Drawing.Size(120, 34);
            btnEditarCantidad.TabIndex = 8;
            btnEditarCantidad.Text = "Editar Cantidad";
            btnEditarCantidad.UseVisualStyleBackColor = true;
            btnEditarCantidad.Click += new System.EventHandler(this.btnEditarCantidad_Click);
            // 
            // btnConfirmarCambios
            // 
            btnConfirmarCambios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnConfirmarCambios.Enabled = false;
            btnConfirmarCambios.Location = new System.Drawing.Point(551, 464);
            btnConfirmarCambios.Name = "btnConfirmarCambios";
            btnConfirmarCambios.Size = new System.Drawing.Size(140, 34);
            btnConfirmarCambios.TabIndex = 9;
            btnConfirmarCambios.Text = "Confirmar Cambios";
            btnConfirmarCambios.UseVisualStyleBackColor = true;
            btnConfirmarCambios.Click += new System.EventHandler(this.btnConfirmarCambios_Click);
            // 
            // DetalleVentaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(btnConfirmarCambios);
            this.Controls.Add(btnEditarCantidad);
            this.Controls.Add(btnEliminarLinea);
            this.Controls.Add(btnAgregarLinea);
            this.Controls.Add(btnCerrar);
            this.Controls.Add(lblTotal);
            this.Controls.Add(lblVendedor);
            this.Controls.Add(lblFecha);
            this.Controls.Add(lblIdVenta);
            this.Controls.Add(dataGridDetalle);
            this.MinimumSize = new System.Drawing.Size(800, 550);
            this.Name = "DetalleVentaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalle de Venta";
            this.Load += new System.EventHandler(this.DetalleVentaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(dataGridDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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

