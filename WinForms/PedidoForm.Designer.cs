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
            txtBuscarProveedor = new TextBox();
            lblBuscarProveedor = new Label();
            cmbFiltroGasto = new ComboBox();
            dataGridPedidos = new DataGridView();
            panelBotones = new Panel();
            btnVolver = new Button();
            btnNuevoPedido = new Button();
            btnEliminar = new Button();
            btnVerDetalle = new Button();
            btnFinalizarPedido = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridPedidos).BeginInit();
            panelBotones.SuspendLayout();
            SuspendLayout();
            // 
            // txtBuscarProveedor
            // 
            txtBuscarProveedor.Location = new Point(206, 12);
            txtBuscarProveedor.Name = "txtBuscarProveedor";
            txtBuscarProveedor.Size = new Size(400, 25);
            txtBuscarProveedor.TabIndex = 3;
            txtBuscarProveedor.TextChanged += FiltrosChanged;
            // 
            // lblBuscarProveedor
            // 
            lblBuscarProveedor.AutoSize = true;
            lblBuscarProveedor.Location = new Point(12, 14);
            lblBuscarProveedor.Name = "lblBuscarProveedor";
            lblBuscarProveedor.Size = new Size(175, 20);
            lblBuscarProveedor.TabIndex = 4;
            lblBuscarProveedor.Text = "Buscar por proveedor:";
            // 
            // cmbFiltroGasto
            // 
            cmbFiltroGasto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroGasto.Items.AddRange(new object[] { "Todos", "Hasta $10.000", "$10.001 a $50.000", "Más de $50.000" });
            cmbFiltroGasto.Location = new Point(642, 12);
            cmbFiltroGasto.Name = "cmbFiltroGasto";
            cmbFiltroGasto.Size = new Size(130, 28);
            cmbFiltroGasto.TabIndex = 5;
            cmbFiltroGasto.SelectedIndexChanged += FiltrosChanged;
            // 
            // dataGridPedidos
            // 
            dataGridPedidos.AllowUserToAddRows = false;
            dataGridPedidos.AllowUserToDeleteRows = false;
            dataGridPedidos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridPedidos.Location = new Point(12, 51);
            dataGridPedidos.MultiSelect = false;
            dataGridPedidos.Name = "dataGridPedidos";
            dataGridPedidos.ReadOnly = true;
            dataGridPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridPedidos.Size = new Size(760, 385);
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
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Location = new Point(0, 452);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(784, 70);
            panelBotones.TabIndex = 1;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(12, 19);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(115, 34);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnNuevoPedido
            // 
            btnNuevoPedido.Location = new Point(133, 19);
            btnNuevoPedido.Name = "btnNuevoPedido";
            btnNuevoPedido.Size = new Size(115, 34);
            btnNuevoPedido.TabIndex = 1;
            btnNuevoPedido.Text = "Nuevo Pedido";
            btnNuevoPedido.UseVisualStyleBackColor = true;
            btnNuevoPedido.Click += btnNuevoPedido_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(254, 19);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(115, 34);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnVerDetalle
            // 
            btnVerDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerDetalle.Location = new Point(657, 19);
            btnVerDetalle.Name = "btnVerDetalle";
            btnVerDetalle.Size = new Size(115, 34);
            btnVerDetalle.TabIndex = 3;
            btnVerDetalle.Text = "Ver Detalle";
            btnVerDetalle.UseVisualStyleBackColor = true;
            btnVerDetalle.Click += btnVerDetalle_Click;
            // 
            // btnFinalizarPedido
            // 
            btnFinalizarPedido.Location = new Point(375, 19);
            btnFinalizarPedido.Name = "btnFinalizarPedido";
            btnFinalizarPedido.Size = new Size(115, 34);
            btnFinalizarPedido.TabIndex = 4;
            btnFinalizarPedido.Text = "Recibido";
            btnFinalizarPedido.UseVisualStyleBackColor = true;
            btnFinalizarPedido.Click += btnFinalizarPedido_Click;
            // 
            // PedidoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 522);
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
