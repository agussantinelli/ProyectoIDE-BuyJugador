namespace WinForms
{
    partial class EditarPersonaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtNombreCompleto;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtDireccion;
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
            lblNombreCompleto.Location = new System.Drawing.Point(30, 20);
            txtNombreCompleto.Location = new System.Drawing.Point(30, 40);
            txtNombreCompleto.Size = new System.Drawing.Size(240, 23);

            // DNI
            lblDni.Text = "DNI:";
            lblDni.Location = new System.Drawing.Point(30, 70);
            txtDni.Location = new System.Drawing.Point(30, 90);
            txtDni.Size = new System.Drawing.Size(240, 23);

            // Email
            lblEmail.Text = "Email:";
            lblEmail.Location = new System.Drawing.Point(30, 120);
            txtEmail.Location = new System.Drawing.Point(30, 140);
            txtEmail.Size = new System.Drawing.Size(240, 23);

            // Password
            lblPassword.Text = "Password:";
            lblPassword.Location = new System.Drawing.Point(30, 170);
            txtPassword.Location = new System.Drawing.Point(30, 190);
            txtPassword.Size = new System.Drawing.Size(240, 23);
            txtPassword.PasswordChar = '*';

            // Teléfono
            lblTelefono.Text = "Teléfono:";
            lblTelefono.Location = new System.Drawing.Point(30, 220);
            txtTelefono.Location = new System.Drawing.Point(30, 240);
            txtTelefono.Size = new System.Drawing.Size(240, 23);

            // Dirección
            lblDireccion.Text = "Dirección:";
            lblDireccion.Location = new System.Drawing.Point(30, 270);
            txtDireccion.Location = new System.Drawing.Point(30, 290);
            txtDireccion.Size = new System.Drawing.Size(240, 23);

            // Localidad
            lblLocalidad.Text = "Localidad:";
            lblLocalidad.Location = new System.Drawing.Point(30, 320);
            cmbLocalidad.Location = new System.Drawing.Point(30, 340);
            cmbLocalidad.Size = new System.Drawing.Size(240, 23);

            // Rol
            lblRol.Text = "Rol:";
            lblRol.Location = new System.Drawing.Point(30, 370);
            cmbRol.Location = new System.Drawing.Point(30, 390);
            cmbRol.Size = new System.Drawing.Size(240, 23);
            cmbRol.SelectedIndexChanged += cmbRol_SelectedIndexChanged;

            // Fecha Ingreso
            lblFechaIngreso.Text = "Fecha Ingreso:";
            lblFechaIngreso.Location = new System.Drawing.Point(30, 420);
            dtpFechaIngreso.Location = new System.Drawing.Point(30, 440);
            dtpFechaIngreso.Size = new System.Drawing.Size(240, 23);

            // Botones
            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new System.Drawing.Point(30, 480);
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new System.Drawing.Point(150, 480);
            btnCancelar.Click += btnCancelar_Click;

            // Form
            ClientSize = new System.Drawing.Size(320, 540);
            Controls.AddRange(new Control[] {
                lblNombreCompleto, txtNombreCompleto,
                lblDni, txtDni,
                lblEmail, txtEmail,
                lblPassword, txtPassword,
                lblTelefono, txtTelefono,
                lblDireccion, txtDireccion,
                lblLocalidad, cmbLocalidad,
                lblRol, cmbRol,
                lblFechaIngreso, dtpFechaIngreso,
                btnGuardar, btnCancelar
            });
            Text = "Editar Persona";
            StartPosition = FormStartPosition.CenterParent;
            Load += EditarPersonaForm_Load;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
