using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    partial class EditarPrecioForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblProductoId;
        private TextBox txtProductoId;
        private Label lblProductoNombre;
        private TextBox txtProductoNombre;
        private Label lblPrecioActual;
        private Label lblPrecioActualValor;
        private Label lblNuevoPrecio;
        private NumericUpDown nudNuevoPrecio;
        private Button btnGuardar;
        private Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblProductoId = new Label();
            txtProductoId = new TextBox();
            lblProductoNombre = new Label();
            txtProductoNombre = new TextBox();
            lblPrecioActual = new Label();
            lblPrecioActualValor = new Label();
            lblNuevoPrecio = new Label();
            nudNuevoPrecio = new NumericUpDown();
            btnGuardar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudNuevoPrecio).BeginInit();
            SuspendLayout();
            // 
            // lblProductoId
            // 
            lblProductoId.AutoSize = true;
            lblProductoId.Location = new Point(20, 26);
            lblProductoId.Name = "lblProductoId";
            lblProductoId.Size = new Size(101, 20);
            lblProductoId.TabIndex = 0;
            lblProductoId.Text = "ID Producto:";
            // 
            // txtProductoId
            // 
            txtProductoId.Location = new Point(140, 23);
            txtProductoId.Name = "txtProductoId";
            txtProductoId.Size = new Size(150, 25);
            txtProductoId.TabIndex = 1;
            // 
            // lblProductoNombre
            // 
            lblProductoNombre.AutoSize = true;
            lblProductoNombre.Location = new Point(20, 63);
            lblProductoNombre.Name = "lblProductoNombre";
            lblProductoNombre.Size = new Size(72, 20);
            lblProductoNombre.TabIndex = 2;
            lblProductoNombre.Text = "Nombre:";
            // 
            // txtProductoNombre
            // 
            txtProductoNombre.Location = new Point(140, 63);
            txtProductoNombre.Name = "txtProductoNombre";
            txtProductoNombre.Size = new Size(150, 25);
            txtProductoNombre.TabIndex = 3;
            // 
            // lblPrecioActual
            // 
            lblPrecioActual.AutoSize = true;
            lblPrecioActual.Location = new Point(20, 101);
            lblPrecioActual.Name = "lblPrecioActual";
            lblPrecioActual.Size = new Size(112, 20);
            lblPrecioActual.TabIndex = 4;
            lblPrecioActual.Text = "Precio Actual:";
            // 
            // lblPrecioActualValor
            // 
            lblPrecioActualValor.AutoSize = true;
            lblPrecioActualValor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPrecioActualValor.Location = new Point(117, 101);
            lblPrecioActualValor.Name = "lblPrecioActualValor";
            lblPrecioActualValor.Size = new Size(12, 15);
            lblPrecioActualValor.TabIndex = 5;
            lblPrecioActualValor.Text = "-";
            // 
            // lblNuevoPrecio
            // 
            lblNuevoPrecio.AutoSize = true;
            lblNuevoPrecio.Location = new Point(20, 138);
            lblNuevoPrecio.Name = "lblNuevoPrecio";
            lblNuevoPrecio.Size = new Size(114, 20);
            lblNuevoPrecio.TabIndex = 6;
            lblNuevoPrecio.Text = "Nuevo Precio:";
            // 
            // nudNuevoPrecio
            // 
            nudNuevoPrecio.DecimalPlaces = 2;
            nudNuevoPrecio.Location = new Point(140, 136);
            nudNuevoPrecio.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudNuevoPrecio.Name = "nudNuevoPrecio";
            nudNuevoPrecio.Size = new Size(150, 25);
            nudNuevoPrecio.TabIndex = 7;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(170, 181);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 34);
            btnGuardar.TabIndex = 8;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(20, 181);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 34);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // EditarPrecioForm
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(322, 239);
            Controls.Add(lblProductoId);
            Controls.Add(txtProductoId);
            Controls.Add(lblProductoNombre);
            Controls.Add(txtProductoNombre);
            Controls.Add(lblPrecioActual);
            Controls.Add(lblPrecioActualValor);
            Controls.Add(lblNuevoPrecio);
            Controls.Add(nudNuevoPrecio);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditarPrecioForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Añadir Nuevo Precio";
            Load += EditarPrecioForm_Load;
            ((System.ComponentModel.ISupportInitialize)nudNuevoPrecio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
