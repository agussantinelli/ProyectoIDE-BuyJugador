namespace WinForms
{
    partial class PedidoForm
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
            this.txtBuscarProveedor = new System.Windows.Forms.TextBox();
            this.lblBuscarProveedor = new System.Windows.Forms.Label();
            this.cmbFiltroGasto = new System.Windows.Forms.ComboBox();
            this.lblFiltroGasto = new System.Windows.Forms.Label();
            this.dataGridPedidos = new System.Windows.Forms.DataGridView();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnNuevoPedido = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnVerDetalle = new System.Windows.Forms.Button();
            this.btnFinalizarPedido = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPedidos)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBuscarProveedor
            // 
            this.lblBuscarProveedor.AutoSize = true;
            this.lblBuscarProveedor.Location = new System.Drawing.Point(12, 12);
            this.lblBuscarProveedor.Name = "lblBuscarProveedor";
            this.lblBuscarProveedor.Size = new System.Drawing.Size(121, 15);
            this.lblBuscarProveedor.Text = "Buscar por proveedor:";
            // 
            // txtBuscarProveedor
            // 
            this.txtBuscarProveedor.Location = new System.Drawing.Point(139, 12);
            this.txtBuscarProveedor.Size = new System.Drawing.Size(400, 23);
            this.txtBuscarProveedor.TextChanged += new System.EventHandler(this.FiltrosChanged);
            // 
            // lblFiltroGasto
            // 
            this.lblFiltroGasto.AutoSize = true;
            this.lblFiltroGasto.Location = new System.Drawing.Point(550, 12);
            this.lblFiltroGasto.Name = "lblFiltroGasto";
            this.lblFiltroGasto.Size = new System.Drawing.Size(86, 15);
            this.lblFiltroGasto.Text = "Filtrar por total:";
            // 
            // cmbFiltroGasto
            // 
            this.cmbFiltroGasto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroGasto.Items.AddRange(new object[] {
                "Todos",
                "Hasta $10.000",
                "$10.001 a $50.000",
                "Más de $50.000"
            });
            this.cmbFiltroGasto.SelectedIndex = 0;
            this.cmbFiltroGasto.Location = new System.Drawing.Point(640, 12);
            this.cmbFiltroGasto.Size = new System.Drawing.Size(130, 23);
            this.cmbFiltroGasto.SelectedIndexChanged += new System.EventHandler(this.FiltrosChanged);
            // 
            // dataGridPedidos
            // 
            this.dataGridPedidos.AllowUserToAddRows = false;
            this.dataGridPedidos.AllowUserToDeleteRows = false;
            this.dataGridPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridPedidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPedidos.Location = new System.Drawing.Point(12, 45);
            this.dataGridPedidos.MultiSelect = false;
            this.dataGridPedidos.ReadOnly = true;
            this.dataGridPedidos.RowTemplate.Height = 25;
            this.dataGridPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridPedidos.Size = new System.Drawing.Size(760, 340);
            this.dataGridPedidos.SelectionChanged += new System.EventHandler(this.DataGridPedidos_SelectionChanged);
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnVolver);
            this.panelBotones.Controls.Add(this.btnNuevoPedido);
            this.panelBotones.Controls.Add(this.btnEliminar);
            this.panelBotones.Controls.Add(this.btnVerDetalle);
            this.panelBotones.Controls.Add(this.btnFinalizarPedido);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 399);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(784, 62);
            this.panelBotones.TabIndex = 1;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(12, 17);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(115, 30);
            this.btnVolver.TabIndex = 0;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnNuevoPedido
            // 
            this.btnNuevoPedido.Location = new System.Drawing.Point(133, 17);
            this.btnNuevoPedido.Name = "btnNuevoPedido";
            this.btnNuevoPedido.Size = new System.Drawing.Size(115, 30);
            this.btnNuevoPedido.TabIndex = 1;
            this.btnNuevoPedido.Text = "Nuevo Pedido";
            this.btnNuevoPedido.UseVisualStyleBackColor = true;
            this.btnNuevoPedido.Click += new System.EventHandler(this.btnNuevoPedido_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(254, 17);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(115, 30);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnVerDetalle
            // 
            this.btnVerDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerDetalle.Location = new System.Drawing.Point(657, 17);
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(115, 30);
            this.btnVerDetalle.TabIndex = 3;
            this.btnVerDetalle.Text = "Ver Detalle";
            this.btnVerDetalle.UseVisualStyleBackColor = true;
            this.btnVerDetalle.Click += new System.EventHandler(this.btnVerDetalle_Click);
            // 
            // btnFinalizarPedido
            // 
            this.btnFinalizarPedido.Location = new System.Drawing.Point(375, 17);
            this.btnFinalizarPedido.Name = "btnFinalizarPedido";
            this.btnFinalizarPedido.Size = new System.Drawing.Size(115, 30);
            this.btnFinalizarPedido.TabIndex = 4;
            this.btnFinalizarPedido.Text = "Recibido";
            this.btnFinalizarPedido.UseVisualStyleBackColor = true;
            this.btnFinalizarPedido.Click += new System.EventHandler(this.btnFinalizarPedido_Click);
            // 
            // PedidoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.dataGridPedidos);
            this.Controls.Add(this.txtBuscarProveedor);
            this.Controls.Add(this.lblBuscarProveedor);
            this.Controls.Add(this.cmbFiltroGasto);
            this.Controls.Add(this.lblFiltroGasto);
            this.Name = "PedidoForm";
            this.Text = "Gestión de Pedidos";
            this.Load += new System.EventHandler(this.PedidoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPedidos)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridPedidos;
        private System.Windows.Forms.Button btnNuevoPedido;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnVerDetalle;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnFinalizarPedido;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.TextBox txtBuscarProveedor;
        private System.Windows.Forms.Label lblBuscarProveedor;
        private System.Windows.Forms.ComboBox cmbFiltroGasto;
        private System.Windows.Forms.Label lblFiltroGasto;
    }
}
