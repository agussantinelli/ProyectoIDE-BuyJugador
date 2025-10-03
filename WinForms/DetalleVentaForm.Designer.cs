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
            this.dataGridDetalle = new System.Windows.Forms.DataGridView();
            this.lblIdVenta = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblVendedor = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnAgregarLinea = new System.Windows.Forms.Button();
            this.btnEliminarLinea = new System.Windows.Forms.Button();
            this.btnEditarCantidad = new System.Windows.Forms.Button();
            this.btnConfirmarCambios = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridDetalle
            // 
            this.dataGridDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDetalle.Location = new System.Drawing.Point(12, 68);
            this.dataGridDetalle.MultiSelect = false;
            this.dataGridDetalle.Name = "dataGridDetalle";
            this.dataGridDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridDetalle.Size = new System.Drawing.Size(760, 290);
            this.dataGridDetalle.TabIndex = 0;
            this.dataGridDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDetalle_CellEndEdit);
            this.dataGridDetalle.SelectionChanged += new System.EventHandler(this.dataGridDetalle_SelectionChanged);
            // 
            // lblIdVenta
            // 
            this.lblIdVenta.AutoSize = true;
            this.lblIdVenta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblIdVenta.Location = new System.Drawing.Point(12, 9);
            this.lblIdVenta.Name = "lblIdVenta";
            this.lblIdVenta.Size = new System.Drawing.Size(94, 21);
            this.lblIdVenta.TabIndex = 1;
            this.lblIdVenta.Text = "ID Venta: ...";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(12, 39);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(53, 15);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha: ...";
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Location = new System.Drawing.Point(200, 39);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(73, 15);
            this.lblVendedor.TabIndex = 3;
            this.lblVendedor.Text = "Vendedor: ...";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(572, 361);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(200, 25);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total: $0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Location = new System.Drawing.Point(697, 409);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 30);
            this.btnCerrar.TabIndex = 5;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnAgregarLinea
            // 
            this.btnAgregarLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregarLinea.Location = new System.Drawing.Point(12, 409);
            this.btnAgregarLinea.Name = "btnAgregarLinea";
            this.btnAgregarLinea.Size = new System.Drawing.Size(120, 30);
            this.btnAgregarLinea.TabIndex = 6;
            this.btnAgregarLinea.Text = "Agregar Producto";
            this.btnAgregarLinea.UseVisualStyleBackColor = true;
            this.btnAgregarLinea.Click += new System.EventHandler(this.btnAgregarLinea_Click);
            // 
            // btnEliminarLinea
            // 
            this.btnEliminarLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminarLinea.Location = new System.Drawing.Point(138, 409);
            this.btnEliminarLinea.Name = "btnEliminarLinea";
            this.btnEliminarLinea.Size = new System.Drawing.Size(120, 30);
            this.btnEliminarLinea.TabIndex = 7;
            this.btnEliminarLinea.Text = "Eliminar Producto";
            this.btnEliminarLinea.UseVisualStyleBackColor = true;
            this.btnEliminarLinea.Click += new System.EventHandler(this.btnEliminarLinea_Click);
            // 
            // btnEditarCantidad
            // 
            this.btnEditarCantidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditarCantidad.Location = new System.Drawing.Point(264, 409);
            this.btnEditarCantidad.Name = "btnEditarCantidad";
            this.btnEditarCantidad.Size = new System.Drawing.Size(120, 30);
            this.btnEditarCantidad.TabIndex = 8;
            this.btnEditarCantidad.Text = "Editar Cantidad";
            this.btnEditarCantidad.UseVisualStyleBackColor = true;
            this.btnEditarCantidad.Click += new System.EventHandler(this.btnEditarCantidad_Click);
            // 
            // btnConfirmarCambios
            // 
            this.btnConfirmarCambios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarCambios.Enabled = false;
            this.btnConfirmarCambios.Location = new System.Drawing.Point(551, 409);
            this.btnConfirmarCambios.Name = "btnConfirmarCambios";
            this.btnConfirmarCambios.Size = new System.Drawing.Size(140, 30);
            this.btnConfirmarCambios.TabIndex = 9;
            this.btnConfirmarCambios.Text = "Confirmar Cambios";
            this.btnConfirmarCambios.UseVisualStyleBackColor = true;
            this.btnConfirmarCambios.Click += new System.EventHandler(this.btnConfirmarCambios_Click);
            // 
            // DetalleVentaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 451);
            this.Controls.Add(this.btnConfirmarCambios);
            this.Controls.Add(this.btnEditarCantidad);
            this.Controls.Add(this.btnEliminarLinea);
            this.Controls.Add(this.btnAgregarLinea);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblVendedor);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblIdVenta);
            this.Controls.Add(this.dataGridDetalle);
            this.MinimumSize = new System.Drawing.Size(800, 490);
            this.Name = "DetalleVentaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalle de Venta";
            this.Load += new System.EventHandler(this.DetalleVentaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetalle)).EndInit();
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

