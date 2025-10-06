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
            this.lblProductoId = new System.Windows.Forms.Label();
            this.txtProductoId = new System.Windows.Forms.TextBox();
            this.lblProductoNombre = new System.Windows.Forms.Label();
            this.txtProductoNombre = new System.Windows.Forms.TextBox();
            this.lblPrecioActual = new System.Windows.Forms.Label();
            this.lblPrecioActualValor = new System.Windows.Forms.Label();
            this.lblNuevoPrecio = new System.Windows.Forms.Label();
            this.nudNuevoPrecio = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudNuevoPrecio)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProductoId
            // 
            this.lblProductoId.AutoSize = true;
            this.lblProductoId.Location = new System.Drawing.Point(20, 23);
            this.lblProductoId.Name = "lblProductoId";
            this.lblProductoId.Size = new System.Drawing.Size(69, 15);
            this.lblProductoId.TabIndex = 0;
            this.lblProductoId.Text = "ID Producto:";
            // 
            // txtProductoId
            // 
            this.txtProductoId.Location = new System.Drawing.Point(120, 20);
            this.txtProductoId.Name = "txtProductoId";
            this.txtProductoId.Size = new System.Drawing.Size(150, 23);
            this.txtProductoId.TabIndex = 1;
            // 
            // lblProductoNombre
            // 
            this.lblProductoNombre.AutoSize = true;
            this.lblProductoNombre.Location = new System.Drawing.Point(20, 56);
            this.lblProductoNombre.Name = "lblProductoNombre";
            this.lblProductoNombre.Size = new System.Drawing.Size(54, 15);
            this.lblProductoNombre.TabIndex = 2;
            this.lblProductoNombre.Text = "Nombre:";
            // 
            // txtProductoNombre
            // 
            this.txtProductoNombre.Location = new System.Drawing.Point(120, 53);
            this.txtProductoNombre.Name = "txtProductoNombre";
            this.txtProductoNombre.Size = new System.Drawing.Size(150, 23);
            this.txtProductoNombre.TabIndex = 3;
            // 
            // lblPrecioActual
            // 
            this.lblPrecioActual.AutoSize = true;
            this.lblPrecioActual.Location = new System.Drawing.Point(20, 89);
            this.lblPrecioActual.Name = "lblPrecioActual";
            this.lblPrecioActual.Size = new System.Drawing.Size(81, 15);
            this.lblPrecioActual.TabIndex = 4;
            this.lblPrecioActual.Text = "Precio Actual:";
            // 
            // lblPrecioActualValor
            // 
            this.lblPrecioActualValor.AutoSize = true;
            this.lblPrecioActualValor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPrecioActualValor.Location = new System.Drawing.Point(117, 89);
            this.lblPrecioActualValor.Name = "lblPrecioActualValor";
            this.lblPrecioActualValor.Size = new System.Drawing.Size(12, 15);
            this.lblPrecioActualValor.TabIndex = 5;
            this.lblPrecioActualValor.Text = "-";
            // 
            // lblNuevoPrecio
            // 
            this.lblNuevoPrecio.AutoSize = true;
            this.lblNuevoPrecio.Location = new System.Drawing.Point(20, 122);
            this.lblNuevoPrecio.Name = "lblNuevoPrecio";
            this.lblNuevoPrecio.Size = new System.Drawing.Size(82, 15);
            this.lblNuevoPrecio.TabIndex = 6;
            this.lblNuevoPrecio.Text = "Nuevo Precio:";
            // 
            // nudNuevoPrecio
            // 
            this.nudNuevoPrecio.DecimalPlaces = 2;
            this.nudNuevoPrecio.Location = new System.Drawing.Point(120, 120);
            this.nudNuevoPrecio.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudNuevoPrecio.Name = "nudNuevoPrecio";
            this.nudNuevoPrecio.Size = new System.Drawing.Size(150, 23);
            this.nudNuevoPrecio.TabIndex = 7;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(20, 160);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(170, 160);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 30);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // EditarPrecioForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 211);
            this.Controls.Add(this.lblProductoId);
            this.Controls.Add(this.txtProductoId);
            this.Controls.Add(this.lblProductoNombre);
            this.Controls.Add(this.txtProductoNombre);
            this.Controls.Add(this.lblPrecioActual);
            this.Controls.Add(this.lblPrecioActualValor);
            this.Controls.Add(this.lblNuevoPrecio);
            this.Controls.Add(this.nudNuevoPrecio);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditarPrecioForm";
            this.Text = "Añadir Nuevo Precio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.AcceptButton = this.btnGuardar;
            this.CancelButton = this.btnCancelar;
            this.Load += new System.EventHandler(this.EditarPrecioForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudNuevoPrecio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
