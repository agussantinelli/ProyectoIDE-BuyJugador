namespace WinForms
{
    partial class AñanirProductoPedidoForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblProductoNombre;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;

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
            lblProductoNombre = new Label();
            numCantidad = new NumericUpDown();
            lblCantidad = new Label();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            SuspendLayout();
            // 
            // lblProductoNombre
            // 
            lblProductoNombre.AutoSize = true;
            lblProductoNombre.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblProductoNombre.Location = new Point(25, 20);
            lblProductoNombre.Name = "lblProductoNombre";
            lblProductoNombre.Size = new Size(147, 21);
            lblProductoNombre.TabIndex = 0;
            lblProductoNombre.Text = "Nombre Producto";
            // 
            // numCantidad
            // 
            numCantidad.Location = new Point(120, 62);
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(120, 25);
            numCantidad.TabIndex = 1;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(29, 62);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(82, 20);
            lblCantidad.TabIndex = 2;
            lblCantidad.Text = "Cantidad:";
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(140, 110);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(100, 30);
            btnConfirmar.TabIndex = 3;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(30, 110);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // AñanirProductoPedidoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            ClientSize = new Size(284, 161);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(lblCantidad);
            Controls.Add(numCantidad);
            Controls.Add(lblProductoNombre);
            Name = "AñanirProductoPedidoForm";
            Text = "Añadir Producto a Pedido";
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

