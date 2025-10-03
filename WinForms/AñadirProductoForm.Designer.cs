using System.Windows.Forms;
using System.Drawing;

namespace WinForms
{
    partial class AñadirProductoForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblProducto;
        private ComboBox cmbProductos;
        private Label lblPrecio;
        private Label lblCantidad;
        private NumericUpDown numCantidad;
        private Button btnSeleccionar;
        private Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblProducto = new Label();
            this.cmbProductos = new ComboBox();
            this.lblPrecio = new Label();
            this.lblCantidad = new Label();
            this.numCantidad = new NumericUpDown();
            this.btnSeleccionar = new Button();
            this.btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new Point(20, 20);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new Size(60, 15);
            this.lblProducto.Text = "Producto:";
            // 
            // cmbProductos
            // 
            this.cmbProductos.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbProductos.Location = new Point(100, 17);
            this.cmbProductos.Size = new Size(220, 23);
            this.cmbProductos.SelectedIndexChanged += new EventHandler(this.cmbProductos_SelectedIndexChanged);
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new Point(20, 55);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new Size(45, 15);
            this.lblPrecio.Text = "Precio:";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new Point(20, 90);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new Size(58, 15);
            this.lblCantidad.Text = "Cantidad:";
            // 
            // numCantidad
            // 
            this.numCantidad.Location = new Point(100, 88);
            this.numCantidad.Minimum = 1;
            this.numCantidad.Maximum = 100;
            this.numCantidad.Size = new Size(80, 23);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new Point(100, 130);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new Size(100, 30);
            this.btnSeleccionar.Text = "Agregar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new EventHandler(this.btnSeleccionar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new Point(220, 130);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new Size(100, 30);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new EventHandler(this.btnCancelar_Click);
            // 
            // AñadirProductoForm
            // 
            this.ClientSize = new Size(350, 180);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.cmbProductos);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.numCantidad);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.btnCancelar);
            this.Name = "AñadirProductoForm";
            this.Text = "Agregar Producto";
            this.Load += new EventHandler(this.AñadirProductoForm_Load);
            this.StartPosition = FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

