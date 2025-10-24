namespace WinForms
{
    partial class VentaForm
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
            txtBuscarCliente = new TextBox();
            lblBuscarCliente = new Label();
            cmbFiltroGasto = new ComboBox();
            lblFiltroGasto = new Label();
            dataGridVentas = new DataGridView();
            panelBotones = new Panel();
            btnVolver = new Button();
            btnNuevaVenta = new Button();
            btnEliminar = new Button();
            btnVerDetalle = new Button();
            btnFinalizarVenta = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridVentas).BeginInit();
            panelBotones.SuspendLayout();
            SuspendLayout();
            // 
            // txtBuscarCliente
            // 
            txtBuscarCliente.Location = new Point(210, 16);
            txtBuscarCliente.Margin = new Padding(4, 4, 4, 4);
            txtBuscarCliente.Name = "txtBuscarCliente";
            txtBuscarCliente.Size = new Size(513, 30);
            txtBuscarCliente.TabIndex = 2;
            txtBuscarCliente.TextChanged += FiltrosChanged;
            // 
            // lblBuscarCliente
            // 
            lblBuscarCliente.AutoSize = true;
            lblBuscarCliente.Location = new Point(15, 16);
            lblBuscarCliente.Margin = new Padding(4, 0, 4, 0);
            lblBuscarCliente.Name = "lblBuscarCliente";
            lblBuscarCliente.Size = new Size(178, 22);
            lblBuscarCliente.TabIndex = 3;
            lblBuscarCliente.Text = "Buscar por cliente:";
            // 
            // cmbFiltroGasto
            // 
            cmbFiltroGasto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroGasto.Items.AddRange(new object[] { "Todos", "Hasta $10.000", "$10.001 a $50.000", "Más de $50.000" });
            cmbFiltroGasto.Location = new Point(770, 17);
            cmbFiltroGasto.Margin = new Padding(4, 4, 4, 4);
            cmbFiltroGasto.Name = "cmbFiltroGasto";
            cmbFiltroGasto.Size = new Size(205, 29);
            cmbFiltroGasto.TabIndex = 4;
            cmbFiltroGasto.SelectedIndexChanged += FiltrosChanged;
            // 
            // lblFiltroGasto
            // 
            lblFiltroGasto.AutoSize = true;
            lblFiltroGasto.Location = new Point(707, 16);
            lblFiltroGasto.Margin = new Padding(4, 0, 4, 0);
            lblFiltroGasto.Name = "lblFiltroGasto";
            lblFiltroGasto.Size = new Size(0, 22);
            lblFiltroGasto.TabIndex = 5;
            // 
            // dataGridVentas
            // 
            dataGridVentas.AllowUserToAddRows = false;
            dataGridVentas.AllowUserToDeleteRows = false;
            dataGridVentas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridVentas.Location = new Point(15, 60);
            dataGridVentas.Margin = new Padding(4, 4, 4, 4);
            dataGridVentas.MultiSelect = false;
            dataGridVentas.Name = "dataGridVentas";
            dataGridVentas.ReadOnly = true;
            dataGridVentas.RowHeadersWidth = 51;
            dataGridVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridVentas.Size = new Size(977, 453);
            dataGridVentas.TabIndex = 0;
            dataGridVentas.SelectionChanged += DataGridVentas_SelectionChanged;
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(btnVolver);
            panelBotones.Controls.Add(btnNuevaVenta);
            panelBotones.Controls.Add(btnEliminar);
            panelBotones.Controls.Add(btnVerDetalle);
            panelBotones.Controls.Add(btnFinalizarVenta);
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Location = new Point(0, 532);
            panelBotones.Margin = new Padding(4, 4, 4, 4);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(1008, 82);
            panelBotones.TabIndex = 1;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(15, 22);
            btnVolver.Margin = new Padding(4, 4, 4, 4);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(148, 40);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnNuevaVenta
            // 
            btnNuevaVenta.Location = new Point(171, 22);
            btnNuevaVenta.Margin = new Padding(4, 4, 4, 4);
            btnNuevaVenta.Name = "btnNuevaVenta";
            btnNuevaVenta.Size = new Size(148, 40);
            btnNuevaVenta.TabIndex = 1;
            btnNuevaVenta.Text = "Nueva";
            btnNuevaVenta.UseVisualStyleBackColor = true;
            btnNuevaVenta.Click += btnNuevaVenta_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(327, 22);
            btnEliminar.Margin = new Padding(4, 4, 4, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(148, 40);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnVerDetalle
            // 
            btnVerDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerDetalle.Location = new Point(845, 22);
            btnVerDetalle.Margin = new Padding(4, 4, 4, 4);
            btnVerDetalle.Name = "btnVerDetalle";
            btnVerDetalle.Size = new Size(148, 40);
            btnVerDetalle.TabIndex = 3;
            btnVerDetalle.Text = "Ver Detalle";
            btnVerDetalle.UseVisualStyleBackColor = true;
            btnVerDetalle.Click += btnVerDetalle_Click;
            // 
            // btnFinalizarVenta
            // 
            btnFinalizarVenta.Location = new Point(482, 22);
            btnFinalizarVenta.Margin = new Padding(4, 4, 4, 4);
            btnFinalizarVenta.Name = "btnFinalizarVenta";
            btnFinalizarVenta.Size = new Size(241, 40);
            btnFinalizarVenta.TabIndex = 4;
            btnFinalizarVenta.Text = "Finalizado";
            btnFinalizarVenta.UseVisualStyleBackColor = true;
            btnFinalizarVenta.Click += btnFinalizarVenta_Click;
            // 
            // VentaForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 614);
            Controls.Add(panelBotones);
            Controls.Add(dataGridVentas);
            Controls.Add(txtBuscarCliente);
            Controls.Add(lblBuscarCliente);
            Controls.Add(cmbFiltroGasto);
            Controls.Add(lblFiltroGasto);
            Margin = new Padding(4, 4, 4, 4);
            Name = "VentaForm";
            Text = "Gestión de Ventas";
            Load += VentaForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridVentas).EndInit();
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();



        }

        #endregion
        private System.Windows.Forms.Button btnFinalizarVenta;
        private System.Windows.Forms.DataGridView dataGridVentas;
        private System.Windows.Forms.Button btnNuevaVenta;
        private System.Windows.Forms.Button btnVerDetalle;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.TextBox txtBuscarCliente;
        private System.Windows.Forms.Label lblBuscarCliente;
        private System.Windows.Forms.ComboBox cmbFiltroGasto;
        private System.Windows.Forms.Label lblFiltroGasto;

    }
}
