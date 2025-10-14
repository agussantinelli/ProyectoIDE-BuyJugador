namespace WinForms
{
    partial class CrearVentaForm
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
            cmbProductos = new ComboBox();
            numCantidad = new NumericUpDown();
            btnAgregarProducto = new Button();
            dataGridLineasVenta = new DataGridView();
            btnEliminarLinea = new Button();
            btnFinalizarVenta = new Button();
            btnCancelar = new Button();
            lblProducto = new Label();
            lblCantidad = new Label();
            lblTotalVenta = new Label();
            chkMarcarFinalizada = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridLineasVenta).BeginInit();
            SuspendLayout();
            // 
            // cmbProductos
            // 
            cmbProductos.FormattingEnabled = true;
            cmbProductos.Location = new Point(15, 34);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new Size(395, 28);
            cmbProductos.TabIndex = 0;
            // 
            // numCantidad
            // 
            numCantidad.Location = new Point(535, 37);
            numCantidad.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(103, 25);
            numCantidad.TabIndex = 1;
            numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAgregarProducto
            // 
            btnAgregarProducto.Location = new Point(856, 33);
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.Size = new Size(120, 28);
            btnAgregarProducto.TabIndex = 2;
            btnAgregarProducto.Text = "Agregar Producto";
            btnAgregarProducto.UseVisualStyleBackColor = true;
            btnAgregarProducto.Click += btnAgregarProducto_Click;
            // 
            // dataGridLineasVenta
            // 
            dataGridLineasVenta.AllowUserToAddRows = false;
            dataGridLineasVenta.AllowUserToDeleteRows = false;
            dataGridLineasVenta.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridLineasVenta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridLineasVenta.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridLineasVenta.Location = new Point(15, 68);
            dataGridLineasVenta.MultiSelect = false;
            dataGridLineasVenta.Name = "dataGridLineasVenta";
            dataGridLineasVenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridLineasVenta.Size = new Size(1118, 340);
            dataGridLineasVenta.TabIndex = 3;
            dataGridLineasVenta.CellEndEdit += DataGridLineasVenta_CellEndEdit;
            // 
            // btnEliminarLinea
            // 
            btnEliminarLinea.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminarLinea.Location = new Point(1013, 33);
            btnEliminarLinea.Name = "btnEliminarLinea";
            btnEliminarLinea.Size = new Size(120, 28);
            btnEliminarLinea.TabIndex = 4;
            btnEliminarLinea.Text = "Eliminar";
            btnEliminarLinea.UseVisualStyleBackColor = true;
            btnEliminarLinea.Click += btnEliminarLinea_Click;
            // 
            // btnFinalizarVenta
            // 
            btnFinalizarVenta.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnFinalizarVenta.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnFinalizarVenta.Location = new Point(1013, 432);
            btnFinalizarVenta.Name = "btnFinalizarVenta";
            btnFinalizarVenta.Size = new Size(120, 34);
            btnFinalizarVenta.TabIndex = 5;
            btnFinalizarVenta.Text = "Confirmar Venta";
            btnFinalizarVenta.UseVisualStyleBackColor = true;
            btnFinalizarVenta.Click += btnFinalizarVenta_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.Location = new Point(887, 432);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 34);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(15, 14);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(81, 20);
            lblProducto.TabIndex = 7;
            lblProducto.Text = "Producto:";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(535, 14);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(82, 20);
            lblCantidad.TabIndex = 8;
            lblCantidad.Text = "Cantidad:";
            // 
            // lblTotalVenta
            // 
            lblTotalVenta.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotalVenta.AutoSize = true;
            lblTotalVenta.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            lblTotalVenta.Location = new Point(15, 441);
            lblTotalVenta.Name = "lblTotalVenta";
            lblTotalVenta.Size = new Size(114, 25);
            lblTotalVenta.TabIndex = 9;
            lblTotalVenta.Text = "Total: $0.00";
            // 
            // chkMarcarFinalizada
            // 
            chkMarcarFinalizada.AutoSize = true;
            chkMarcarFinalizada.Location = new Point(14, 414);
            chkMarcarFinalizada.Name = "chkMarcarFinalizada";
            chkMarcarFinalizada.Size = new Size(251, 24);
            chkMarcarFinalizada.TabIndex = 10;
            chkMarcarFinalizada.Text = "Marcar venta como finalizada";
            chkMarcarFinalizada.UseVisualStyleBackColor = true;
            // 
            // CrearVentaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1145, 477);
            Controls.Add(lblTotalVenta);
            Controls.Add(lblCantidad);
            Controls.Add(lblProducto);
            Controls.Add(btnCancelar);
            Controls.Add(chkMarcarFinalizada);
            Controls.Add(btnFinalizarVenta);
            Controls.Add(btnEliminarLinea);
            Controls.Add(dataGridLineasVenta);
            Controls.Add(btnAgregarProducto);
            Controls.Add(numCantidad);
            Controls.Add(cmbProductos);
            MinimumSize = new Size(600, 391);
            Name = "CrearVentaForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Crear Nueva Venta";
            Load += CrearVentaForm_Load;
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridLineasVenta).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMarcarFinalizada;
        private System.Windows.Forms.ComboBox cmbProductos;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.DataGridView dataGridLineasVenta;
        private System.Windows.Forms.Button btnEliminarLinea;
        private System.Windows.Forms.Button btnFinalizarVenta;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblTotalVenta;
    }
}

