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
            txtBuscarProveedor = new System.Windows.Forms.TextBox();
            lblBuscarProveedor = new System.Windows.Forms.Label();
            cmbFiltroGasto = new System.Windows.Forms.ComboBox();
            dataGridPedidos = new System.Windows.Forms.DataGridView();
            panelBotones = new System.Windows.Forms.Panel();
            btnVolver = new System.Windows.Forms.Button();
            btnNuevoPedido = new System.Windows.Forms.Button();
            btnEliminar = new System.Windows.Forms.Button();
            btnVerDetalle = new System.Windows.Forms.Button();
            btnFinalizarPedido = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridPedidos).BeginInit();
            panelBotones.SuspendLayout();
            SuspendLayout();
            // 
            // txtBuscarProveedor
            // 
            txtBuscarProveedor.Location = new System.Drawing.Point(206, 12);
            txtBuscarProveedor.Name = "txtBuscarProveedor";
            txtBuscarProveedor.Size = new System.Drawing.Size(400, 25);
            txtBuscarProveedor.TabIndex = 3;
            txtBuscarProveedor.TextChanged += FiltrosChanged;
            // 
            // lblBuscarProveedor
            // 
            lblBuscarProveedor.AutoSize = true;
            lblBuscarProveedor.Location = new System.Drawing.Point(12, 14);
            lblBuscarProveedor.Name = "lblBuscarProveedor";
            lblBuscarProveedor.Size = new System.Drawing.Size(175, 20);
            lblBuscarProveedor.TabIndex = 4;
            lblBuscarProveedor.Text = "Buscar por proveedor:";
            // 
            // cmbFiltroGasto
            // 
            cmbFiltroGasto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbFiltroGasto.FormattingEnabled = true;
            cmbFiltroGasto.Location = new System.Drawing.Point(642, 12);
            cmbFiltroGasto.Name = "cmbFiltroGasto";
            cmbFiltroGasto.Size = new System.Drawing.Size(130, 25);
            cmbFiltroGasto.TabIndex = 5;
            cmbFiltroGasto.SelectedIndexChanged += FiltrosChanged;
            // 
            // dataGridPedidos
            // 
            dataGridPedidos.AllowUserToAddRows = false;
            dataGridPedidos.AllowUserToDeleteRows = false;
            dataGridPedidos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridPedidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridPedidos.Location = new System.Drawing.Point(12, 51);
            dataGridPedidos.MultiSelect = false;
            dataGridPedidos.Name = "dataGridPedidos";
            dataGridPedidos.ReadOnly = true;
            dataGridPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridPedidos.Size = new System.Drawing.Size(760, 385);
            dataGridPedidos.TabIndex = 2;
            dataGridPedidos.SelectionChanged += DataGridPedidos_SelectionChanged;
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(btnVolver);
            panelBotones.Controls.Add(btnNuevoPedido);
            panelBotones.Controls.Add(btnEliminar);
            panelBotones.Controls.Add(btnVerDetalle);
            panelBotones.Controls.Add(btnFinalizarPedido);
            panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelBotones.Location = new System.Drawing.Point(0, 452);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new System.Drawing.Size(784, 70);
            panelBotones.TabIndex = 1;
            // 
            // btnVolver
            // 
            btnVolver.Location = new System.Drawing.Point(12, 19);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new System.Drawing.Size(115, 34);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnNuevoPedido
            // 
            btnNuevoPedido.Location = new System.Drawing.Point(133, 19);
            btnNuevoPedido.Name = "btnNuevoPedido";
            btnNuevoPedido.Size = new System.Drawing.Size(115, 34);
            btnNuevoPedido.TabIndex = 1;
            btnNuevoPedido.Text = "Nuevo Pedido";
            btnNuevoPedido.UseVisualStyleBackColor = true;
            btnNuevoPedido.Click += btnNuevoPedido_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new System.Drawing.Point(254, 19);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new System.Drawing.Size(115, 34);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnVerDetalle
            // 
            btnVerDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnVerDetalle.Location = new System.Drawing.Point(657, 19);
            btnVerDetalle.Name = "btnVerDetalle";
            btnVerDetalle.Size = new System.Drawing.Size(115, 34);
            btnVerDetalle.TabIndex = 3;
            btnVerDetalle.Text = "Ver Detalle";
            btnVerDetalle.UseVisualStyleBackColor = true;
            btnVerDetalle.Click += btnVerDetalle_Click;
            // 
            // btnFinalizarPedido
            // 
            btnFinalizarPedido.Location = new System.Drawing.Point(375, 19);
            btnFinalizarPedido.Name = "btnFinalizarPedido";
            btnFinalizarPedido.Size = new System.Drawing.Size(115, 34);
            btnFinalizarPedido.TabIndex = 4;
            btnFinalizarPedido.Text = "Recibido";
            btnFinalizarPedido.UseVisualStyleBackColor = true;
            btnFinalizarPedido.Click += btnFinalizarPedido_Click;
            // 
            // PedidoForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 522);
            Controls.Add(panelBotones);
            Controls.Add(dataGridPedidos);
            Controls.Add(txtBuscarProveedor);
            Controls.Add(lblBuscarProveedor);
            Controls.Add(cmbFiltroGasto);
            Name = "PedidoForm";
            Text = "Gestión de Pedidos";
            Load += PedidoForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridPedidos).EndInit();
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
    }
}

