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
            this.dataGridDetalles = new System.Windows.Forms.DataGridView();
            this.lblNumeroPedido = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetalles)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridDetalles
            // 
            this.dataGridDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDetalles.Location = new System.Drawing.Point(12, 98);
            this.dataGridDetalles.Name = "dataGridDetalles";
            this.dataGridDetalles.RowTemplate.Height = 25;
            this.dataGridDetalles.Size = new System.Drawing.Size(760, 279);
            this.dataGridDetalles.TabIndex = 0;
            this.dataGridDetalles.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridDetalles_CellValueChanged);
            // 
            // lblNumeroPedido
            // 
            this.lblNumeroPedido.AutoSize = true;
            this.lblNumeroPedido.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNumeroPedido.Location = new System.Drawing.Point(12, 9);
            this.lblNumeroPedido.Name = "lblNumeroPedido";
            this.lblNumeroPedido.Size = new System.Drawing.Size(171, 21);
            this.lblNumeroPedido.TabIndex = 1;
            this.lblNumeroPedido.Text = "Detalles del Pedido #";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(16, 43);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(41, 15);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha:";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(16, 67);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(64, 15);
            this.lblProveedor.TabIndex = 3;
            this.lblProveedor.Text = "Proveedor:";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.Location = new System.Drawing.Point(624, 386);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(148, 23);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total: $0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(230, 43);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(45, 15);
            this.lblEstado.TabIndex = 5;
            this.lblEstado.Text = "Estado:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Location = new System.Drawing.Point(662, 419);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 30);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar Cambios";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.Location = new System.Drawing.Point(12, 419);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 30);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminar.Location = new System.Drawing.Point(128, 419);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(110, 30);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.Text = "Eliminar Línea";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // DetallePedidoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblProveedor);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblNumeroPedido);
            this.Controls.Add(this.dataGridDetalles);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "DetallePedidoForm";
            this.Text = "Detalle de Pedido";
            this.Load += new System.EventHandler(this.DetallePedidoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetalles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridDetalles;
        private System.Windows.Forms.Label lblNumeroPedido;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEliminar;
    }
}
