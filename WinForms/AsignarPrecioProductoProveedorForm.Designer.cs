namespace WinForms
{
    partial class AsignarPrecioProductoProveedorForm
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
            lblProductoInfo = new Label();
            lblProveedorInfo = new Label();
            lblPrecio = new Label();
            numPrecio = new NumericUpDown();
            btnGuardar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)numPrecio).BeginInit();
            SuspendLayout();
            lblProductoInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProductoInfo.Location = new Point(12, 10);
            lblProductoInfo.Name = "lblProductoInfo";
            lblProductoInfo.Size = new Size(260, 26);
            lblProductoInfo.TabIndex = 0;
            lblProductoInfo.Text = "Producto: 'Nombre del Producto'";
            lblProductoInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblProveedorInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProveedorInfo.Location = new Point(12, 36);
            lblProveedorInfo.Name = "lblProveedorInfo";
            lblProveedorInfo.Size = new Size(260, 26);
            lblProveedorInfo.TabIndex = 5;
            lblProveedorInfo.Text = "Proveedor: 'Razón Social'";
            lblProveedorInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblPrecio.AutoSize = true;
            lblPrecio.Location = new Point(34, 85);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(114, 20);
            lblPrecio.TabIndex = 1;
            lblPrecio.Text = "Nuevo Precio:";
            numPrecio.DecimalPlaces = 2;
            numPrecio.Location = new Point(154, 83);
            numPrecio.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPrecio.Name = "numPrecio";
            numPrecio.Size = new Size(120, 25);
            numPrecio.TabIndex = 2;
            btnGuardar.Location = new Point(172, 135);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 34);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Location = new Point(12, 135);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 34);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 182);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(numPrecio);
            Controls.Add(lblPrecio);
            Controls.Add(lblProveedorInfo);
            Controls.Add(lblProductoInfo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AsignarPrecioProductoProveedorForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Asignar Precio";
            ((System.ComponentModel.ISupportInitialize)numPrecio).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProductoInfo;
        private System.Windows.Forms.Label lblProveedorInfo;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown numPrecio;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}

