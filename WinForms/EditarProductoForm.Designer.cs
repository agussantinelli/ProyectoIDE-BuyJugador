namespace WinForms
{
    partial class EditarProductoForm
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
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtPrecio;


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
            btnGuardar = new Button();
            btnCancelar = new Button();
            lblId = new Label();
            txtId = new TextBox();
            txtPrecio = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numStock).BeginInit();
            SuspendLayout();
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(23, 62);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(72, 20);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre:";
            txtNombre.Location = new Point(144, 62);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(245, 25);
            txtNombre.TabIndex = 3;
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(23, 100);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(100, 20);
            lblDescripcion.TabIndex = 4;
            lblDescripcion.Text = "Descripción:";
            txtDescripcion.Location = new Point(144, 97);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(245, 52);
            txtDescripcion.TabIndex = 5;
            lblStock.AutoSize = true;
            lblStock.Location = new Point(23, 159);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(53, 20);
            lblStock.TabIndex = 6;
            lblStock.Text = "Stock:";
            numStock.Location = new Point(144, 157);
            numStock.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numStock.Name = "numStock";
            numStock.Size = new Size(120, 25);
            numStock.TabIndex = 7;
            lblTipoProducto.AutoSize = true;
            lblTipoProducto.Location = new Point(23, 195);
            lblTipoProducto.Name = "lblTipoProducto";
            lblTipoProducto.Size = new Size(113, 20);
            lblTipoProducto.TabIndex = 8;
            lblTipoProducto.Text = "Tipo Producto:";
            cmbTipoProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoProducto.FormattingEnabled = true;
            cmbTipoProducto.Location = new Point(144, 192);
            cmbTipoProducto.Name = "cmbTipoProducto";
            cmbTipoProducto.Size = new Size(245, 28);
            cmbTipoProducto.TabIndex = 9;
            lblPrecio.AutoSize = true;
            lblPrecio.Location = new Point(23, 231);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(112, 20);
            lblPrecio.TabIndex = 10;
            lblPrecio.Text = "Precio Actual:";
            btnGuardar.Location = new Point(260, 272);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 34);
            btnGuardar.TabIndex = 12;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Location = new Point(144, 272);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 34);
            btnCancelar.TabIndex = 13;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            lblId.AutoSize = true;
            lblId.Location = new Point(23, 25);
            lblId.Name = "lblId";
            lblId.Size = new Size(29, 20);
            lblId.TabIndex = 0;
            lblId.Text = "ID:";
            txtId.Location = new Point(144, 25);
            txtId.Name = "txtId";
            txtId.Size = new Size(245, 25);
            txtId.TabIndex = 1;
            txtPrecio.Location = new Point(144, 228);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(120, 25);
            txtPrecio.TabIndex = 11;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(414, 330);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(txtPrecio);
            Controls.Add(lblPrecio);
            Controls.Add(cmbTipoProducto);
            Controls.Add(lblTipoProducto);
            Controls.Add(numStock);
            Controls.Add(lblStock);
            Controls.Add(txtDescripcion);
            Controls.Add(lblDescripcion);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(txtId);
            Controls.Add(lblId);
            Name = "EditarProductoForm";
            Text = "Editar Producto";
            Load += EditarProductoForm_Load;
            ((System.ComponentModel.ISupportInitialize)numStock).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

