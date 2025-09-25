namespace WinForms
{
    partial class CrearProductoForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private NumericUpDown numStock;
        private ComboBox cmbTipoProducto;
        private Button btnGuardar;
        private Button btnCancelar;
        private Label lblNombre;
        private Label lblDescripcion;
        private Label lblStock;
        private Label lblTipoProducto;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtNombre = new TextBox();
            this.txtDescripcion = new TextBox();
            this.numStock = new NumericUpDown();
            this.cmbTipoProducto = new ComboBox();
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();
            this.lblNombre = new Label();
            this.lblDescripcion = new Label();
            this.lblStock = new Label();
            this.lblTipoProducto = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.numStock)).BeginInit();
            this.SuspendLayout();

            // lblNombre
            this.lblNombre.Location = new Point(30, 20);
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.AutoSize = true;

            // txtNombre
            this.txtNombre.Location = new Point(30, 45);
            this.txtNombre.Size = new Size(220, 23);

            // lblDescripcion
            this.lblDescripcion.Location = new Point(30, 85);
            this.lblDescripcion.Text = "Descripción:";
            this.lblDescripcion.AutoSize = true;

            // txtDescripcion
            this.txtDescripcion.Location = new Point(30, 110);
            this.txtDescripcion.Size = new Size(220, 60);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.ScrollBars = ScrollBars.Vertical;

            // lblStock
            this.lblStock.Location = new Point(30, 180);
            this.lblStock.Text = "Stock:";
            this.lblStock.AutoSize = true;

            // numStock
            this.numStock.Location = new Point(30, 205);
            this.numStock.Maximum = 100000;

            // lblTipoProducto
            this.lblTipoProducto.Location = new Point(30, 245);
            this.lblTipoProducto.Text = "Tipo de Producto:";
            this.lblTipoProducto.AutoSize = true;

            // cmbTipoProducto
            this.cmbTipoProducto.Location = new Point(30, 270);
            this.cmbTipoProducto.Size = new Size(220, 23);

            // btnGuardar
            this.btnGuardar.Location = new Point(30, 310);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new EventHandler(this.btnGuardar_Click);

            // btnCancelar
            this.btnCancelar.Location = new Point(150, 310);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new EventHandler(this.btnCancelar_Click);

            // Form
            this.ClientSize = new Size(320, 380);
            this.Controls.AddRange(new Control[] {
                lblNombre, txtNombre,
                lblDescripcion, txtDescripcion,
                lblStock, numStock,
                lblTipoProducto, cmbTipoProducto,
                btnGuardar, btnCancelar
            });
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Crear Producto";
            this.Load += new EventHandler(this.CrearProductoForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.numStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
