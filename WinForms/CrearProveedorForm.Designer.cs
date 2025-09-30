using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    partial class CrearProveedorForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtRazonSocial;
        private TextBox txtCuit;
        private TextBox txtEmail;
        private TextBox txtTelefono;
        private TextBox txtDireccion;
        private ComboBox cmbProvincia;
        private ComboBox cmbLocalidad;
        private Label lblRazonSocial;
        private Label lblCuit;
        private Label lblEmail;
        private Label lblTelefono;
        private Label lblDireccion;
        private Label lblProvincia;
        private Label lblLocalidad;
        private Button btnGuardar;
        private Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtRazonSocial = new TextBox();
            txtCuit = new TextBox();
            txtEmail = new TextBox();
            txtTelefono = new TextBox();
            txtDireccion = new TextBox();
            cmbProvincia = new ComboBox();
            cmbLocalidad = new ComboBox();
            lblRazonSocial = new Label();
            lblCuit = new Label();
            lblEmail = new Label();
            lblTelefono = new Label();
            lblDireccion = new Label();
            lblProvincia = new Label();
            lblLocalidad = new Label();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();

            // Razón Social
            lblRazonSocial.Text = "Razón Social:";
            lblRazonSocial.Location = new Point(25, 20);
            lblRazonSocial.AutoSize = true;
            txtRazonSocial.Location = new Point(25, 45);
            txtRazonSocial.Size = new Size(300, 23);

            // CUIT
            lblCuit.Text = "CUIT:";
            lblCuit.Location = new Point(25, 80);
            lblCuit.AutoSize = true;
            txtCuit.Location = new Point(25, 105);
            txtCuit.Size = new Size(300, 23);

            // Email
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(25, 140);
            lblEmail.AutoSize = true;
            txtEmail.Location = new Point(25, 165);
            txtEmail.Size = new Size(300, 23);

            // Teléfono
            lblTelefono.Text = "Teléfono:";
            lblTelefono.Location = new Point(25, 200);
            lblTelefono.AutoSize = true;
            txtTelefono.Location = new Point(25, 225);
            txtTelefono.Size = new Size(300, 23);

            // Dirección
            lblDireccion.Text = "Dirección:";
            lblDireccion.Location = new Point(25, 260);
            lblDireccion.AutoSize = true;
            txtDireccion.Location = new Point(25, 285);
            txtDireccion.Size = new Size(300, 23);

            // Provincia
            lblProvincia.Text = "Provincia:";
            lblProvincia.Location = new Point(25, 320);
            lblProvincia.AutoSize = true;
            cmbProvincia.Location = new Point(25, 345);
            cmbProvincia.Size = new Size(300, 23);
            cmbProvincia.SelectedIndexChanged += cmbProvincia_SelectedIndexChanged;

            // Localidad
            lblLocalidad.Text = "Localidad:";
            lblLocalidad.Location = new Point(25, 380);
            lblLocalidad.AutoSize = true;
            cmbLocalidad.Location = new Point(25, 405);
            cmbLocalidad.Size = new Size(300, 23);

            // Botones (a la derecha)
            btnCancelar.Text = "Cancelar";
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.Location = new Point(145, 450);
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.Click += btnCancelar_Click;

            btnGuardar.Text = "Guardar";
            btnGuardar.Size = new Size(100, 30);
            btnGuardar.Location = new Point(255, 450);
            btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardar.Click += btnGuardar_Click;

            // Form
            ClientSize = new Size(360, 500);
            Controls.AddRange(new Control[] {
                lblRazonSocial, txtRazonSocial,
                lblCuit, txtCuit,
                lblEmail, txtEmail,
                lblTelefono, txtTelefono,
                lblDireccion, txtDireccion,
                lblProvincia, cmbProvincia,
                lblLocalidad, cmbLocalidad,
                btnCancelar, btnGuardar
            });
            Text = "Nuevo Proveedor";
            StartPosition = FormStartPosition.CenterParent;
            Load += CrearProveedorForm_Load;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
