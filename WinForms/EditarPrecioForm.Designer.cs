using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    partial class EditarPrecioForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblProducto;
        private Label lblProductoValor;
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
            components = new System.ComponentModel.Container();

            lblProducto = new Label();
            lblProductoValor = new Label();
            lblPrecioActual = new Label();
            lblPrecioActualValor = new Label();
            lblNuevoPrecio = new Label();
            nudNuevoPrecio = new NumericUpDown();
            btnGuardar = new Button();
            btnCancelar = new Button();

            ((System.ComponentModel.ISupportInitialize)nudNuevoPrecio).BeginInit();
            SuspendLayout();

            // lblProducto
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(20, 20);
            lblProducto.Text = "Producto:";

            // lblProductoValor
            lblProductoValor.AutoSize = true;
            lblProductoValor.Location = new Point(95, 20);
            lblProductoValor.Text = "-";

            // lblPrecioActual
            lblPrecioActual.AutoSize = true;
            lblPrecioActual.Location = new Point(20, 55);
            lblPrecioActual.Text = "Precio actual:";

            // lblPrecioActualValor
            lblPrecioActualValor.AutoSize = true;
            lblPrecioActualValor.Location = new Point(110, 55);
            lblPrecioActualValor.Text = "-";

            // lblNuevoPrecio
            lblNuevoPrecio.AutoSize = true;
            lblNuevoPrecio.Location = new Point(20, 95);
            lblNuevoPrecio.Text = "Nuevo precio:";

            // nudNuevoPrecio
            nudNuevoPrecio.Location = new Point(110, 92);
            nudNuevoPrecio.Size = new Size(150, 23);
            nudNuevoPrecio.DecimalPlaces = 2;
            nudNuevoPrecio.Maximum = 100000000m;
            nudNuevoPrecio.Minimum = 0m;
            nudNuevoPrecio.ThousandsSeparator = true;

            // btnGuardar
            btnGuardar.Location = new Point(20, 140);
            btnGuardar.Size = new Size(100, 30);
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;

            // btnCancelar
            btnCancelar.Location = new Point(160, 140);
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;

            // EditarPrecioForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(290, 190);
            Controls.Add(lblProducto);
            Controls.Add(lblProductoValor);
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
            Text = "Editar precio (fecha hoy)";
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
            Load += EditarPrecioForm_Load;

            ((System.ComponentModel.ISupportInitialize)nudNuevoPrecio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
