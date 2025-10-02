namespace WinForms
{
    partial class EditarTipoProductoForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblDescripcion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblDescripcion = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // lblDescripcion
            this.lblDescripcion.Location = new System.Drawing.Point(30, 20);
            this.lblDescripcion.Text = "Descripción:";
            this.lblDescripcion.AutoSize = true;

            // txtDescripcion
            this.txtDescripcion.Location = new System.Drawing.Point(30, 45);
            this.txtDescripcion.Size = new System.Drawing.Size(240, 80);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(30, 140);
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // btnGuardar
            this.btnGuardar.Location = new System.Drawing.Point(170, 140);
            this.btnGuardar.Size = new System.Drawing.Size(100, 30);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // EditarTipoProductoForm
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editar Tipo de Producto";
            this.Load += new System.EventHandler(this.EditarTipoProductoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
