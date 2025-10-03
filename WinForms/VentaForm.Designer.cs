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
            this.txtBuscarCliente = new System.Windows.Forms.TextBox();
            this.lblBuscarCliente = new System.Windows.Forms.Label();
            this.cmbFiltroGasto = new System.Windows.Forms.ComboBox();
            this.lblFiltroGasto = new System.Windows.Forms.Label();
            this.dataGridVentas = new System.Windows.Forms.DataGridView();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnNuevaVenta = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnVerDetalle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVentas)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // lblBuscarCliente
            this.lblBuscarCliente.AutoSize = true;
            lblBuscarCliente.Text = "Buscar por cliente:";
            this.lblBuscarCliente.Location = new System.Drawing.Point(12, 12);

            // txtBuscarCliente
            this.txtBuscarCliente.Location = new System.Drawing.Point(135, 12);
            this.txtBuscarCliente.Size = new System.Drawing.Size(400, 23);
            this.txtBuscarCliente.TextChanged += new System.EventHandler(this.FiltrosChanged);

            // lblFiltroGasto
            this.lblFiltroGasto.AutoSize = true;
            this.lblFiltroGasto.Location = new System.Drawing.Point(550, 12);

            // cmbFiltroGasto
            this.cmbFiltroGasto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroGasto.Items.AddRange(new object[] {
                "Todos",
                "Hasta $10.000",
                "$10.001 a $50.000",
                "Más de $50.000"
            });
            this.cmbFiltroGasto.SelectedIndex = 0;
            this.cmbFiltroGasto.Location = new System.Drawing.Point(550, 12);
            this.cmbFiltroGasto.Size = new System.Drawing.Size(160, 23);
            this.cmbFiltroGasto.SelectedIndexChanged += new System.EventHandler(this.FiltrosChanged);
            // 
            // dataGridVentas
            // 
            this.dataGridVentas.AllowUserToAddRows = false;
            this.dataGridVentas.AllowUserToDeleteRows = false;
            this.dataGridVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridVentas.Location = new System.Drawing.Point(12, 45);
            this.dataGridVentas.Size = new System.Drawing.Size(760, 340); 
            this.dataGridVentas.MultiSelect = false;
            this.dataGridVentas.Name = "dataGridVentas";
            this.dataGridVentas.ReadOnly = true;
            this.dataGridVentas.RowTemplate.Height = 25;
            this.dataGridVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridVentas.TabIndex = 0;
            dataGridVentas.SelectionChanged += DataGridVentas_SelectionChanged;

            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnVolver);
            this.panelBotones.Controls.Add(this.btnNuevaVenta);
            this.panelBotones.Controls.Add(this.btnEliminar);
            this.panelBotones.Controls.Add(this.btnVerDetalle);
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
            // btnNuevaVenta
            // 
            this.btnNuevaVenta.Location = new System.Drawing.Point(133, 17);
            this.btnNuevaVenta.Name = "btnNuevaVenta";
            this.btnNuevaVenta.Size = new System.Drawing.Size(115, 30);
            this.btnNuevaVenta.TabIndex = 1;
            this.btnNuevaVenta.Text = "Nueva";
            this.btnNuevaVenta.UseVisualStyleBackColor = true;
            this.btnNuevaVenta.Click += new System.EventHandler(this.btnNuevaVenta_Click);
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
            // btnFinalizarVenta
            // 
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.btnFinalizarVenta = new System.Windows.Forms.Button();
            this.btnFinalizarVenta.Location = new System.Drawing.Point(375, 17);
            this.btnFinalizarVenta.Name = "btnFinalizarVenta";
            this.btnFinalizarVenta.Size = new System.Drawing.Size(115, 30);
            this.btnFinalizarVenta.TabIndex = 4;
            this.btnFinalizarVenta.Text = "Finalizar Venta";
            this.btnFinalizarVenta.UseVisualStyleBackColor = true;
            this.btnFinalizarVenta.Click += new System.EventHandler(this.btnFinalizarVenta_Click);
            this.panelBotones.Controls.Add(this.btnFinalizarVenta);
            // 
            // VentaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.dataGridVentas);
            this.Controls.Add(this.txtBuscarCliente);
            this.Controls.Add(this.lblBuscarCliente);
            this.Controls.Add(this.cmbFiltroGasto);
            this.Controls.Add(this.lblFiltroGasto);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;

            this.Name = "VentaForm";
            this.Text = "Gestión de Ventas";
            this.Load += new System.EventHandler(this.VentaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVentas)).EndInit();



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
