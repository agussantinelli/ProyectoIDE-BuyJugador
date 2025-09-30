using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    partial class EditarProveedorForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtRazonSocial;
        private TextBox txtEmail;
        private TextBox txtTelefono;
        private TextBox txtDireccion;
        private ComboBox cmbProvincia;
        private ComboBox cmbLocalidad;
        private Label lblRazonSocial;
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
            txtEmail = new TextBox();
            txtTelefono = new TextBox();
            txtDireccion = new TextBox();
            cmbProvincia = new ComboBox();
            cmbLocalidad = new ComboBox();
            lblRazonSocial = new Label();
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

            // Email
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(25, 80);
            lblEmail.AutoSize = true;
            txtEmail.Location = new Point(25, 105);
            txtEmail.Size = new Size(300, 23);

            // Teléfono
            lblTelefono.Text = "Teléfono:";
            lblTelefono.Location = new Point(25, 140);
            lblTelefono.AutoSize = true;
            txtTelefono.Location = new Point(25, 165);
            txtTelefono.Size = new Size(300, 23);

            // Dirección
            lblDireccion.Text = "Dirección:";
            lblDireccion.Location = new Point(25, 200);
            lblDireccion.AutoSize = true;
            txtDireccion.Location = new Point(25, 225);
            txtDireccion.Size = new Size(300, 23);

            // Provincia
            lblProvincia.Text = "Provincia:";
            lblProvincia.Location = new Point(25, 260);
            lblProvincia.AutoSize = true;
            cmbProvincia.Location = new Point(25, 285);
            cmbProvincia.Size = new Size(300, 23);
            cmbProvincia.SelectedIndexChanged += cmbProvincia_SelectedIndexChanged;

            // Localidad
            lblLocalidad.Text = "Localidad:";
            lblLocalidad.Location = new Point(25, 320);
            lblLocalidad.AutoSize = true;
            cmbLocalidad.Location = new Point(25, 345);
            cmbLocalidad.Size = new Size(300, 23);

            // Botones
            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new Point(25, 390);
            btnGuardar.Size = new Size(100, 30);
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(145, 390);
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.Click += btnCancelar_Click;

            // Form
            ClientSize = new Size(360, 450);
            Controls.AddRange(new Control[] {
                lblRazonSocial, txtRazonSocial,
                lblEmail, txtEmail,
                lblTelefono, txtTelefono,
                lblDireccion, txtDireccion,
                lblProvincia, cmbProvincia,
                lblLocalidad, cmbLocalidad,
                btnGuardar, btnCancelar
            });
            Text = "Editar Proveedor";
            StartPosition = FormStartPosition.CenterParent;
            Load += EditarProveedorForm_Load;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
