namespace WinForms
{
    partial class CrearPersonaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtNombreCompleto;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.ComboBox cmbProvincia;
        private System.Windows.Forms.Label lblProvincia;
        private System.Windows.Forms.ComboBox cmbLocalidad;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.DateTimePicker dtpFechaIngreso;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblNombreCompleto;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label lblLocalidad;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.Label lblFechaIngreso;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtNombreCompleto = new TextBox();
            txtDni = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            txtTelefono = new TextBox();
            txtDireccion = new TextBox();
            cmbProvincia = new ComboBox();
            lblProvincia = new Label();
            cmbLocalidad = new ComboBox();
            cmbRol = new ComboBox();
            dtpFechaIngreso = new DateTimePicker();
            btnGuardar = new Button();
            btnCancelar = new Button();
            lblNombreCompleto = new Label();
            lblDni = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            lblTelefono = new Label();
            lblDireccion = new Label();
            lblLocalidad = new Label();
            lblRol = new Label();
            lblFechaIngreso = new Label();

            SuspendLayout();

            // NombreCompleto
            lblNombreCompleto.Text = "Nombre Completo:";
            lblNombreCompleto.Location = new Point(30, 20);
            lblNombreCompleto.AutoSize = true;
            txtNombreCompleto.Location = new Point(30, 45);
            txtNombreCompleto.Size = new Size(240, 23);

            // DNI
            lblDni.Text = "DNI:";
            lblDni.Location = new Point(30, 85);
            lblDni.AutoSize = true;
            txtDni.Location = new Point(30, 110);
            txtDni.Size = new Size(240, 23);

            // Email
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(30, 150);
            lblEmail.AutoSize = true;
            txtEmail.Location = new Point(30, 175);
            txtEmail.Size = new Size(240, 23);

            // Password
            lblPassword.Text = "Password:";
            lblPassword.Location = new Point(30, 215);
            lblPassword.AutoSize = true;
            txtPassword.Location = new Point(30, 240);
            txtPassword.Size = new Size(240, 23);
            txtPassword.PasswordChar = '*';

            // Teléfono
            lblTelefono.Text = "Teléfono:";
            lblTelefono.Location = new Point(30, 280);
            lblTelefono.AutoSize = true;
            txtTelefono.Location = new Point(30, 305);
            txtTelefono.Size = new Size(240, 23);

            // Dirección
            lblDireccion.Text = "Dirección:";
            lblDireccion.Location = new Point(30, 345);
            lblDireccion.AutoSize = true;
            txtDireccion.Location = new Point(30, 370);
            txtDireccion.Size = new Size(240, 23);

            // Provincia
            lblProvincia.Text = "Provincia:";
            lblProvincia.Location = new Point(30, 410);
            lblProvincia.AutoSize = true;
            cmbProvincia.Location = new Point(30, 435);
            cmbProvincia.Size = new Size(240, 23);
            cmbProvincia.SelectedIndexChanged += cmbProvincia_SelectedIndexChanged;

            // Localidad
            lblLocalidad.Text = "Localidad:";
            lblLocalidad.Location = new Point(30, 475);
            lblLocalidad.AutoSize = true;
            cmbLocalidad.Location = new Point(30, 500);
            cmbLocalidad.Size = new Size(240, 23);

            // Rol
            lblRol.Text = "Rol:";
            lblRol.Location = new Point(30, 540);
            lblRol.AutoSize = true;
            cmbRol.Location = new Point(30, 565);
            cmbRol.Size = new Size(240, 23);
            cmbRol.SelectedIndexChanged += cmbRol_SelectedIndexChanged;

            // Fecha Ingreso
            lblFechaIngreso.Text = "Fecha Ingreso:";
            lblFechaIngreso.Location = new Point(30, 605);
            lblFechaIngreso.AutoSize = true;
            dtpFechaIngreso.Location = new Point(30, 630);
            dtpFechaIngreso.Size = new Size(240, 23);

            // Botones
            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new Point(30, 670);
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(150, 670);
            btnCancelar.Click += btnCancelar_Click;

            // Form
            ClientSize = new Size(320, 730);
            Controls.AddRange(new Control[] {
                lblNombreCompleto, txtNombreCompleto,
                lblDni, txtDni,
                lblEmail, txtEmail,
                lblPassword, txtPassword,
                lblTelefono, txtTelefono,
                lblDireccion, txtDireccion,
                lblProvincia, cmbProvincia,
                lblLocalidad, cmbLocalidad,
                lblRol, cmbRol,
                lblFechaIngreso, dtpFechaIngreso,
                btnGuardar, btnCancelar
            });
            Text = "Crear Persona";
            StartPosition = FormStartPosition.CenterParent;
            Load += CrearPersonaForm_Load;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
