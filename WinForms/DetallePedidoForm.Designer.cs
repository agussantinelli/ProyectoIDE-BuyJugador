namespace WinForms
{
    partial class DetallePedidoForm
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
            this.lblIdPedido = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.dataGridDetalle = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnAgregarLinea = new System.Windows.Forms.Button();
            this.btnEliminarLinea = new System.Windows.Forms.Button();
            this.btnConfirmarCambios = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnEditarCantidad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIdPedido
            // 
            this.lblIdPedido.AutoSize = true;
            this.lblIdPedido.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblIdPedido.Location = new System.Drawing.Point(12, 9);
            this.lblIdPedido.Name = "lblIdPedido";
            this.lblIdPedido.Size = new System.Drawing.Size(109, 21);
            this.lblIdPedido.TabIndex = 0;
            this.lblIdPedido.Text = "ID Pedido: ...";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(12, 40);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(50, 17);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Fecha: ...";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(200, 40);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(81, 17);
            this.lblProveedor.TabIndex = 2;
            this.lblProveedor.Text = "Proveedor: ...";
            // 
            // dataGridDetalle
            // 
            this.dataGridDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDetalle.Location = new System.Drawing.Point(15, 70);
            this.dataGridDetalle.MultiSelect = false;
            this.dataGridDetalle.Name = "dataGridDetalle";
            this.dataGridDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridDetalle.Size = new System.Drawing.Size(1153, 444);
            this.dataGridDetalle.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(968, 9);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(200, 28);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total: $0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAgregarLinea
            // 
            this.btnAgregarLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregarLinea.Location = new System.Drawing.Point(15, 520);
            this.btnAgregarLinea.Name = "btnAgregarLinea";
            this.btnAgregarLinea.Size = new System.Drawing.Size(120, 30);
            this.btnAgregarLinea.TabIndex = 5;
            this.btnAgregarLinea.Text = "Agregar Línea";
            this.btnAgregarLinea.UseVisualStyleBackColor = true;
            this.btnAgregarLinea.Click += new System.EventHandler(this.btnAgregarLinea_Click);
            // 
            // btnEliminarLinea
            // 
            this.btnEliminarLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminarLinea.Location = new System.Drawing.Point(141, 520);
            this.btnEliminarLinea.Name = "btnEliminarLinea";
            this.btnEliminarLinea.Size = new System.Drawing.Size(120, 30);
            this.btnEliminarLinea.TabIndex = 6;
            this.btnEliminarLinea.Text = "Eliminar Línea";
            this.btnEliminarLinea.UseVisualStyleBackColor = true;
            this.btnEliminarLinea.Click += new System.EventHandler(this.btnEliminarLinea_Click);
            // 
            // btnConfirmarCambios
            // 
            this.btnConfirmarCambios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarCambios.Enabled = false;
            this.btnConfirmarCambios.Location = new System.Drawing.Point(902, 520);
            this.btnConfirmarCambios.Name = "btnConfirmarCambios";
            this.btnConfirmarCambios.Size = new System.Drawing.Size(150, 30);
            this.btnConfirmarCambios.TabIndex = 8;
            this.btnConfirmarCambios.Text = "Confirmar Cambios";
            this.btnConfirmarCambios.UseVisualStyleBackColor = true;
            this.btnConfirmarCambios.Click += new System.EventHandler(this.btnConfirmarCambios_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Location = new System.Drawing.Point(1068, 520);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(100, 30);
            this.btnCerrar.TabIndex = 7;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnEditarCantidad
            // 
            this.btnEditarCantidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditarCantidad.Location = new System.Drawing.Point(267, 520);
            this.btnEditarCantidad.Name = "btnEditarCantidad";
            this.btnEditarCantidad.Size = new System.Drawing.Size(120, 30);
            this.btnEditarCantidad.TabIndex = 9;
            this.btnEditarCantidad.Text = "Editar Cantidad";
            this.btnEditarCantidad.UseVisualStyleBackColor = true;
            this.btnEditarCantidad.Click += new System.EventHandler(this.btnEditarCantidad_Click);
            // 
            // DetallePedidoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 562);
            this.Controls.Add(this.btnEditarCantidad);
            this.Controls.Add(this.btnConfirmarCambios);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnEliminarLinea);
            this.Controls.Add(this.btnAgregarLinea);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dataGridDetalle);
            this.Controls.Add(this.lblProveedor);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblIdPedido);
            this.Name = "DetallePedidoForm";
            this.Text = "Detalle de Pedido";
            this.Load += new System.EventHandler(this.DetallePedidoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIdPedido;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.DataGridView dataGridDetalle;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnAgregarLinea;
        private System.Windows.Forms.Button btnEliminarLinea;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnConfirmarCambios;
        private System.Windows.Forms.Button btnEditarCantidad;
    }
}

