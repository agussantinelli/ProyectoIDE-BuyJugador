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
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.numStock = new System.Windows.Forms.NumericUpDown();
            this.lblTipoProducto = new System.Windows.Forms.Label();
            this.cmbTipoProducto = new System.Windows.Forms.ComboBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.numPrecio = new System.Windows.Forms.NumericUpDown();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecio)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(26, 25);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(54, 15);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(137, 22);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(200, 23);
            this.txtNombre.TabIndex = 1;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(26, 60);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(72, 15);
            this.lblDescripcion.TabIndex = 2;
            this.lblDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(137, 57);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(200, 23);
            this.txtDescripcion.TabIndex = 3;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(26, 95);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(39, 15);
            this.lblStock.TabIndex = 4;
            this.lblStock.Text = "Stock:";
            // 
            // numStock
            // 
            this.numStock.Location = new System.Drawing.Point(137, 93);
            this.numStock.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numStock.Name = "numStock";
            this.numStock.Size = new System.Drawing.Size(120, 23);
            this.numStock.TabIndex = 5;
            // 
            // lblTipoProducto
            // 
            this.lblTipoProducto.AutoSize = true;
            this.lblTipoProducto.Location = new System.Drawing.Point(26, 130);
            this.lblTipoProducto.Name = "lblTipoProducto";
            this.lblTipoProducto.Size = new System.Drawing.Size(100, 15);
            this.lblTipoProducto.TabIndex = 8;
            this.lblTipoProducto.Text = "Tipo de Producto:";
            // 
            // cmbTipoProducto
            // 
            this.cmbTipoProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoProducto.FormattingEnabled = true;
            this.cmbTipoProducto.Location = new System.Drawing.Point(137, 127);
            this.cmbTipoProducto.Name = "cmbTipoProducto";
            this.cmbTipoProducto.Size = new System.Drawing.Size(200, 23);
            this.cmbTipoProducto.TabIndex = 9;
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(26, 165);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(43, 15);
            this.lblPrecio.TabIndex = 10;
            this.lblPrecio.Text = "Precio:";
            // 
            // numPrecio
            // 
            this.numPrecio.DecimalPlaces = 2;
            this.numPrecio.Location = new System.Drawing.Point(137, 163);
            this.numPrecio.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numPrecio.Name = "numPrecio";
            this.numPrecio.Size = new System.Drawing.Size(120, 23);
            this.numPrecio.TabIndex = 11;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(150, 208);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(262, 208);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(100, 30);
            this.btnCrear.TabIndex = 12;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // CrearProductoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.numPrecio);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.cmbTipoProducto);
            this.Controls.Add(this.lblTipoProducto);
            this.Controls.Add(this.numStock);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Name = "CrearProductoForm";
            this.Text = "Crear Nuevo Producto";
            this.Load += new System.EventHandler(this.CrearProductoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

