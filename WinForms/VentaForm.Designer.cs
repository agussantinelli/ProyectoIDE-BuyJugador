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
            txtBuscarCliente.Location = new Point(163, 14);
            txtBuscarCliente.Name = "txtBuscarCliente";
            txtBuscarCliente.Size = new Size(400, 25);
            txtBuscarCliente.TabIndex = 2;
            txtBuscarCliente.TextChanged += FiltrosChanged;
            // 
            // lblBuscarCliente
            // 
            lblBuscarCliente.AutoSize = true;
            lblBuscarCliente.Location = new Point(12, 14);
            lblBuscarCliente.Name = "lblBuscarCliente";
            lblBuscarCliente.Size = new Size(145, 20);
            lblBuscarCliente.TabIndex = 3;
            lblBuscarCliente.Text = "Buscar por cliente:";
            // 
            // cmbFiltroGasto
            // 
            cmbFiltroGasto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroGasto.Items.AddRange(new object[] { "Todos", "Hasta $10.000", "$10.001 a $50.000", "Más de $50.000" });
            cmbFiltroGasto.Location = new Point(599, 11);
            cmbFiltroGasto.Name = "cmbFiltroGasto";
            cmbFiltroGasto.Size = new Size(160, 28);
            cmbFiltroGasto.TabIndex = 4;
            cmbFiltroGasto.SelectedIndexChanged += FiltrosChanged;
            // 
            // lblFiltroGasto
            // 
            lblFiltroGasto.AutoSize = true;
            lblFiltroGasto.Location = new Point(550, 14);
            lblFiltroGasto.Name = "lblFiltroGasto";
            lblFiltroGasto.Size = new Size(0, 20);
            lblFiltroGasto.TabIndex = 5;
            // 
            // dataGridVentas
            // 
            dataGridVentas.AllowUserToAddRows = false;
            dataGridVentas.AllowUserToDeleteRows = false;
            dataGridVentas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridVentas.Location = new Point(12, 51);
            dataGridVentas.MultiSelect = false;
            dataGridVentas.Name = "dataGridVentas";
            dataGridVentas.ReadOnly = true;
            dataGridVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridVentas.Size = new Size(760, 385);
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
            // btnNuevaVenta
            // 
            btnNuevaVenta.Location = new Point(133, 19);
            btnNuevaVenta.Name = "btnNuevaVenta";
            btnNuevaVenta.Size = new Size(115, 34);
            btnNuevaVenta.TabIndex = 1;
            btnNuevaVenta.Text = "Nueva";
            btnNuevaVenta.UseVisualStyleBackColor = true;
            btnNuevaVenta.Click += btnNuevaVenta_Click;
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
            // btnFinalizarVenta
            // 
            btnFinalizarVenta.Location = new Point(375, 19);
            btnFinalizarVenta.Name = "btnFinalizarVenta";
            btnFinalizarVenta.Size = new Size(115, 34);
            btnFinalizarVenta.TabIndex = 4;
            btnFinalizarVenta.Text = "Finalizado";
            btnFinalizarVenta.UseVisualStyleBackColor = true;
            btnFinalizarVenta.Click += btnFinalizarVenta_Click;
            // 
            // VentaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 522);
            Controls.Add(panelBotones);
            Controls.Add(dataGridVentas);
            Controls.Add(txtBuscarCliente);
            Controls.Add(lblBuscarCliente);
            Controls.Add(cmbFiltroGasto);
            Controls.Add(lblFiltroGasto);
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
