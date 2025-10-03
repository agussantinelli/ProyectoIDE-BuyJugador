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
            this.lblProductoNombre = new System.Windows.Forms.Label();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProductoNombre
            // 
            this.lblProductoNombre.AutoSize = true;
            this.lblProductoNombre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblProductoNombre.Location = new System.Drawing.Point(25, 20);
            this.lblProductoNombre.Name = "lblProductoNombre";
            this.lblProductoNombre.Size = new System.Drawing.Size(155, 21);
            this.lblProductoNombre.TabIndex = 0;
            this.lblProductoNombre.Text = "Nombre Producto";
            // 
            // numCantidad
            // 
            this.numCantidad.Location = new System.Drawing.Point(95, 60);
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(120, 23);
            this.numCantidad.TabIndex = 1;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(29, 62);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(61, 15);
            this.lblCantidad.TabIndex = 2;
            this.lblCantidad.Text = "Cantidad:";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(140, 110);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(100, 30);
            this.btnConfirmar.TabIndex = 3;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(30, 110);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // AñanirProductoPedidoForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.numCantidad);
            this.Controls.Add(this.lblProductoNombre);
            this.Name = "AñanirProductoPedidoForm";
            this.Text = "Añadir Producto a Pedido";
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

