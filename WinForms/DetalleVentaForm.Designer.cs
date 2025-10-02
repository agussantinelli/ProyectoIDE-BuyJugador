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
            this.btnEliminarLinea = new System.Windows.Forms.Button();
            this.btnEditarCantidad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridDetalle
            // 
            this.dataGridDetalle.AllowUserToAddRows = false;
            this.dataGridDetalle.AllowUserToDeleteRows = false;
            this.dataGridDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDetalle.Location = new System.Drawing.Point(12, 76);
            this.dataGridDetalle.Name = "dataGridDetalle";
            this.dataGridDetalle.RowTemplate.Height = 25;
            this.dataGridDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridDetalle.Size = new System.Drawing.Size(760, 283);
            this.dataGridDetalle.TabIndex = 0;
            this.dataGridDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDetalle_CellEndEdit);
            this.dataGridDetalle.SelectionChanged += new System.EventHandler(this.dataGridDetalle_SelectionChanged);
            // 
            // lblIdVenta
            // 
            this.lblIdVenta.AutoSize = true;
            this.lblIdVenta.Location = new System.Drawing.Point(12, 9);
            this.lblIdVenta.Name = "lblIdVenta";
            this.lblIdVenta.Size = new System.Drawing.Size(57, 15);
            this.lblIdVenta.TabIndex = 1;
            this.lblIdVenta.Text = "ID Venta: ";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(12, 33);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(44, 15);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha: ";
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Location = new System.Drawing.Point(12, 58);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(63, 15);
            this.lblVendedor.TabIndex = 3;
            this.lblVendedor.Text = "Vendedor: ";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.Location = new System.Drawing.Point(572, 371);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(200, 23);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total: $0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCerrar.Location = new System.Drawing.Point(12, 419);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 30);
            this.btnCerrar.TabIndex = 5;
            this.btnCerrar.Text = "Cerrar"; // Estandarizado
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnEliminarLinea
            // 
            this.btnEliminarLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminarLinea.Location = new System.Drawing.Point(138, 419);
            this.btnEliminarLinea.Name = "btnEliminarLinea";
            this.btnEliminarLinea.Size = new System.Drawing.Size(120, 30);
            this.btnEliminarLinea.TabIndex = 6;
            this.btnEliminarLinea.Text = "Eliminar Línea";
            this.btnEliminarLinea.UseVisualStyleBackColor = true;
            this.btnEliminarLinea.Click += new System.EventHandler(this.btnEliminarLinea_Click);
            // 
            // btnEditarCantidad
            // 
            this.btnEditarCantidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditarCantidad.Location = new System.Drawing.Point(264, 419);
            this.btnEditarCantidad.Name = "btnEditarCantidad";
            this.btnEditarCantidad.Size = new System.Drawing.Size(120, 30);
            this.btnEditarCantidad.TabIndex = 7;
            this.btnEditarCantidad.Text = "Editar Cantidad";
            this.btnEditarCantidad.UseVisualStyleBackColor = true;
            this.btnEditarCantidad.Click += new System.EventHandler(this.btnEditarCantidad_Click);
            // 
            // DetalleVentaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnEditarCantidad);
            this.Controls.Add(this.btnEliminarLinea);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblVendedor);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblIdVenta);
            this.Controls.Add(this.dataGridDetalle);
            this.MinimumSize = new System.Drawing.Size(600, 350);
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
        private System.Windows.Forms.Button btnEliminarLinea;
        private System.Windows.Forms.Button btnEditarCantidad;
    }
}
