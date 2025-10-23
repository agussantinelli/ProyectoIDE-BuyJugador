namespace WinForms
{
    partial class CrearProductoForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.NumericUpDown numStock;
        private System.Windows.Forms.Label lblTipoProducto;
        private System.Windows.Forms.ComboBox cmbTipoProducto;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown numPrecio;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnCancelar;

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
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblStock = new Label();
            numStock = new NumericUpDown();
            lblTipoProducto = new Label();
            cmbTipoProducto = new ComboBox();
            lblPrecio = new Label();
            numPrecio = new NumericUpDown();
            btnCrear = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)numStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrecio).BeginInit();
            SuspendLayout();
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(26, 28);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(72, 20);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            txtNombre.Location = new Point(169, 28);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(200, 25);
            txtNombre.TabIndex = 1;
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(26, 68);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(100, 20);
            lblDescripcion.TabIndex = 2;
            lblDescripcion.Text = "Descripción:";
            txtDescripcion.Location = new Point(169, 65);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(200, 25);
            txtDescripcion.TabIndex = 3;
            lblStock.AutoSize = true;
            lblStock.Location = new Point(26, 108);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(53, 20);
            lblStock.TabIndex = 4;
            lblStock.Text = "Stock:";
            numStock.Location = new Point(169, 108);
            numStock.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numStock.Name = "numStock";
            numStock.Size = new Size(120, 25);
            numStock.TabIndex = 5;
            lblTipoProducto.AutoSize = true;
            lblTipoProducto.Location = new Point(26, 147);
            lblTipoProducto.Name = "lblTipoProducto";
            lblTipoProducto.Size = new Size(137, 20);
            lblTipoProducto.TabIndex = 8;
            lblTipoProducto.Text = "Tipo de Producto:";
            cmbTipoProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoProducto.FormattingEnabled = true;
            cmbTipoProducto.Location = new Point(169, 144);
            cmbTipoProducto.Name = "cmbTipoProducto";
            cmbTipoProducto.Size = new Size(200, 28);
            cmbTipoProducto.TabIndex = 9;
            lblPrecio.AutoSize = true;
            lblPrecio.Location = new Point(26, 187);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(60, 20);
            lblPrecio.TabIndex = 10;
            lblPrecio.Text = "Precio:";
            numPrecio.DecimalPlaces = 2;
            numPrecio.Location = new Point(169, 185);
            numPrecio.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPrecio.Name = "numPrecio";
            numPrecio.Size = new Size(120, 25);
            numPrecio.TabIndex = 11;
            btnCrear.Location = new Point(262, 236);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(100, 34);
            btnCrear.TabIndex = 12;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            btnCancelar.Location = new Point(150, 236);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 34);
            btnCancelar.TabIndex = 13;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 296);
            Controls.Add(btnCancelar);
            Controls.Add(btnCrear);
            Controls.Add(numPrecio);
            Controls.Add(lblPrecio);
            Controls.Add(cmbTipoProducto);
            Controls.Add(lblTipoProducto);
            Controls.Add(numStock);
            Controls.Add(lblStock);
            Controls.Add(txtDescripcion);
            Controls.Add(lblDescripcion);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Name = "CrearProductoForm";
            Text = "Crear Nuevo Producto";
            Load += CrearProductoForm_Load;
            ((System.ComponentModel.ISupportInitialize)numStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrecio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

