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
            this.cmbProductos = new System.Windows.Forms.ComboBox();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.dataGridLineasVenta = new System.Windows.Forms.DataGridView();
            this.btnEliminarLinea = new System.Windows.Forms.Button();
            this.btnFinalizarVenta = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblProducto = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblTotalVenta = new System.Windows.Forms.Label();
            this.chkMarcarFinalizada = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLineasVenta)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbProductos
            // 
            this.cmbProductos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbProductos.FormattingEnabled = true;
            this.cmbProductos.Location = new System.Drawing.Point(15, 30);
            this.cmbProductos.Name = "cmbProductos";
            this.cmbProductos.Size = new System.Drawing.Size(250, 23);
            this.cmbProductos.TabIndex = 0;
            // 
            // numCantidad
            // 
            this.numCantidad.Location = new System.Drawing.Point(271, 30);
            this.numCantidad.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(80, 23);
            this.numCantidad.TabIndex = 1;
            this.numCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.Location = new System.Drawing.Point(357, 29);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(120, 25);
            this.btnAgregarProducto.TabIndex = 2;
            this.btnAgregarProducto.Text = "Agregar Producto";
            this.btnAgregarProducto.UseVisualStyleBackColor = true;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // dataGridLineasVenta
            // 
            this.dataGridLineasVenta.AllowUserToAddRows = false;
            this.dataGridLineasVenta.AllowUserToDeleteRows = false;
            this.dataGridLineasVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridLineasVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridLineasVenta.Location = new System.Drawing.Point(15, 60);
            this.dataGridLineasVenta.MultiSelect = false;
            this.dataGridLineasVenta.Name = "dataGridLineasVenta";
            this.dataGridLineasVenta.ReadOnly = false;
            this.dataGridLineasVenta.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridLineasVenta.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridLineasVenta_CellEndEdit);
            this.dataGridLineasVenta.RowTemplate.Height = 25;
            this.dataGridLineasVenta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridLineasVenta.Size = new System.Drawing.Size(757, 300);
            this.dataGridLineasVenta.TabIndex = 3;
            // 
            // btnEliminarLinea
            // 
            this.btnEliminarLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminarLinea.Location = new System.Drawing.Point(652, 29);
            this.btnEliminarLinea.Name = "btnEliminarLinea";
            this.btnEliminarLinea.Size = new System.Drawing.Size(120, 25);
            this.btnEliminarLinea.TabIndex = 4;
            this.btnEliminarLinea.Text = "Eliminar";
            this.btnEliminarLinea.UseVisualStyleBackColor = true;
            this.btnEliminarLinea.Click += new System.EventHandler(this.btnEliminarLinea_Click);
            // 
            // btnFinalizarVenta
            // 
            this.btnFinalizarVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalizarVenta.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFinalizarVenta.Location = new System.Drawing.Point(652, 381);
            this.btnFinalizarVenta.Name = "btnFinalizarVenta";
            this.btnFinalizarVenta.Size = new System.Drawing.Size(120, 30);
            this.btnFinalizarVenta.TabIndex = 5;
            this.btnFinalizarVenta.Text = "Confirmar Venta";
            this.btnFinalizarVenta.UseVisualStyleBackColor = true;
            this.btnFinalizarVenta.Click += new System.EventHandler(this.btnFinalizarVenta_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(526, 381);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 30);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new System.Drawing.Point(15, 12);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(59, 15);
            this.lblProducto.TabIndex = 7;
            this.lblProducto.Text = "Producto:";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(271, 12);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(58, 15);
            this.lblCantidad.TabIndex = 8;
            this.lblCantidad.Text = "Cantidad:";
            // 
            // lblTotalVenta
            // 
            this.lblTotalVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalVenta.AutoSize = true;
            this.lblTotalVenta.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotalVenta.Location = new System.Drawing.Point(15, 382);
            this.lblTotalVenta.Name = "lblTotalVenta";
            this.lblTotalVenta.Size = new System.Drawing.Size(121, 25);
            this.lblTotalVenta.TabIndex = 9;
            this.lblTotalVenta.Text = "Total: $0.00";
            // 
            // chkMarcarFinalizada
            // 
            this.chkMarcarFinalizada.AutoSize = true;
            this.chkMarcarFinalizada.Location = new System.Drawing.Point(15, 370); 
            this.chkMarcarFinalizada.Name = "chkMarcarFinalizada";
            this.chkMarcarFinalizada.Size = new System.Drawing.Size(182, 19);
            this.chkMarcarFinalizada.TabIndex = 10;
            this.chkMarcarFinalizada.Text = "Marcar venta como finalizada";
            this.chkMarcarFinalizada.UseVisualStyleBackColor = true;
            // 
            // CrearVentaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 421);
            this.Controls.Add(this.lblTotalVenta);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.chkMarcarFinalizada);
            this.Controls.Add(this.btnFinalizarVenta);
            this.Controls.Add(this.btnEliminarLinea);
            this.Controls.Add(this.dataGridLineasVenta);
            this.Controls.Add(this.btnAgregarProducto);
            this.Controls.Add(this.numCantidad);
            this.Controls.Add(this.cmbProductos);
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "CrearVentaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Crear Nueva Venta";
            this.Load += new System.EventHandler(this.CrearVentaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLineasVenta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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

