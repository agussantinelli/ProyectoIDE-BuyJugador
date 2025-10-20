namespace WinForms
{
    partial class PedidoForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridPedidos;
        private System.Windows.Forms.Button btnNuevoPedido;
        private System.Windows.Forms.Button btnVerDetalle;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnFinalizarPedido;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.TextBox txtBuscarProveedor;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.ComboBox cmbFiltroGasto;
        private System.Windows.Forms.Panel panelBotones;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dataGridPedidos = new DataGridView();
            btnNuevoPedido = new Button();
            btnVerDetalle = new Button();
            btnEliminar = new Button();
            btnFinalizarPedido = new Button();
            btnVolver = new Button();
            txtBuscarProveedor = new TextBox();
            lblBuscar = new Label();
            cmbFiltroGasto = new ComboBox();
            panelBotones = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridPedidos).BeginInit();
            panelBotones.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridPedidos
            // 
            dataGridPedidos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridPedidos.Location = new Point(12, 53);
            dataGridPedidos.Name = "dataGridPedidos";
            dataGridPedidos.Size = new Size(754, 427);
            dataGridPedidos.TabIndex = 3;
            dataGridPedidos.SelectionChanged += DataGridPedidos_SelectionChanged;
            // 
            // btnNuevoPedido
            // 
            btnNuevoPedido.Location = new Point(133, 19);
            btnNuevoPedido.Name = "btnNuevoPedido";
            btnNuevoPedido.Size = new Size(115, 34);
            btnNuevoPedido.TabIndex = 1;
            btnNuevoPedido.Text = "Nuevo";
            btnNuevoPedido.UseVisualStyleBackColor = true;
            btnNuevoPedido.Click += btnNuevoPedido_Click;
            // 
            // btnVerDetalle
            // 
            btnVerDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerDetalle.Location = new Point(651, 19);
            btnVerDetalle.Name = "btnVerDetalle";
            btnVerDetalle.Size = new Size(115, 34);
            btnVerDetalle.TabIndex = 4;
            btnVerDetalle.Text = "Ver Detalle";
            btnVerDetalle.UseVisualStyleBackColor = true;
            btnVerDetalle.Click += btnVerDetalle_Click;
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
            // btnFinalizarPedido
            // 
            btnFinalizarPedido.Location = new Point(375, 19);
            btnFinalizarPedido.Name = "btnFinalizarPedido";
            btnFinalizarPedido.Size = new Size(160, 34);
            btnFinalizarPedido.TabIndex = 3;
            btnFinalizarPedido.Text = "Recibido";
            btnFinalizarPedido.UseVisualStyleBackColor = true;
            btnFinalizarPedido.Click += btnFinalizarPedido_Click;
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
            // txtBuscarProveedor
            // 
            txtBuscarProveedor.Location = new Point(88, 12);
            txtBuscarProveedor.Name = "txtBuscarProveedor";
            txtBuscarProveedor.Size = new Size(262, 25);
            txtBuscarProveedor.TabIndex = 0;
            txtBuscarProveedor.TextChanged += FiltrosChanged;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(12, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(62, 20);
            lblBuscar.TabIndex = 2;
            lblBuscar.Text = "Buscar:";
            // 
            // cmbFiltroGasto
            // 
            cmbFiltroGasto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroGasto.FormattingEnabled = true;
            cmbFiltroGasto.Location = new Point(366, 12);
            cmbFiltroGasto.Name = "cmbFiltroGasto";
            cmbFiltroGasto.Size = new Size(225, 28);
            cmbFiltroGasto.TabIndex = 1;
            cmbFiltroGasto.SelectedIndexChanged += FiltrosChanged;
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(btnVolver);
            panelBotones.Controls.Add(btnNuevoPedido);
            panelBotones.Controls.Add(btnEliminar);
            panelBotones.Controls.Add(btnFinalizarPedido);
            panelBotones.Controls.Add(btnVerDetalle);
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Location = new Point(0, 486);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(778, 70);
            panelBotones.TabIndex = 4;
            // 
            // PedidoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            ClientSize = new Size(778, 556);
            Controls.Add(panelBotones);
            Controls.Add(cmbFiltroGasto);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscarProveedor);
            Controls.Add(dataGridPedidos);
            Name = "PedidoForm";
            Text = "Gestión de Pedidos";
            Load += PedidoForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridPedidos).EndInit();
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}