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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtNombreCompleto = new System.Windows.Forms.TextBox();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.cmbProvincia = new System.Windows.Forms.ComboBox();
            this.lblProvincia = new System.Windows.Forms.Label();
            this.cmbLocalidad = new System.Windows.Forms.ComboBox();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblNombreCompleto = new System.Windows.Forms.Label();
            this.lblDni = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.lblRol = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // lblNombreCompleto
            // 
            this.lblNombreCompleto.AutoSize = true;
            this.lblNombreCompleto.Location = new System.Drawing.Point(30, 20);
            this.lblNombreCompleto.Name = "lblNombreCompleto";
            this.lblNombreCompleto.Size = new System.Drawing.Size(107, 15);
            this.lblNombreCompleto.Text = "Nombre Completo:";
            // 
            // txtNombreCompleto
            // 
            this.txtNombreCompleto.Location = new System.Drawing.Point(30, 40);
            this.txtNombreCompleto.Size = new System.Drawing.Size(240, 23);
            this.txtNombreCompleto.TabIndex = 0;
            // 
            // lblDni
            // 
            this.lblDni.AutoSize = true;
            this.lblDni.Location = new System.Drawing.Point(30, 80);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(30, 15);
            this.lblDni.Text = "DNI:";
            // 
            // txtDni
            // 
            this.txtDni.Location = new System.Drawing.Point(30, 100);
            this.txtDni.Size = new System.Drawing.Size(240, 23);
            this.txtDni.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(30, 140);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(30, 160);
            this.txtEmail.Size = new System.Drawing.Size(240, 23);
            this.txtEmail.TabIndex = 2;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(30, 200);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(68, 15);
            this.lblPassword.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(30, 220);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(240, 23);
            this.txtPassword.TabIndex = 3;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(30, 260);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(55, 15);
            this.lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(30, 280);
            this.txtTelefono.Size = new System.Drawing.Size(240, 23);
            this.txtTelefono.TabIndex = 4;
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Location = new System.Drawing.Point(30, 320);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(60, 15);
            this.lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(30, 340);
            this.txtDireccion.Size = new System.Drawing.Size(240, 23);
            this.txtDireccion.TabIndex = 5;
            // 
            // lblProvincia
            // 
            this.lblProvincia.AutoSize = true;
            this.lblProvincia.Location = new System.Drawing.Point(30, 380);
            this.lblProvincia.Name = "lblProvincia";
            this.lblProvincia.Size = new System.Drawing.Size(59, 15);
            this.lblProvincia.Text = "Provincia:";
            // 
            // cmbProvincia
            // 
            this.cmbProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvincia.FormattingEnabled = true;
            this.cmbProvincia.Location = new System.Drawing.Point(30, 400);
            this.cmbProvincia.Size = new System.Drawing.Size(240, 23);
            this.cmbProvincia.TabIndex = 6;
            this.cmbProvincia.SelectedIndexChanged += new System.EventHandler(this.cmbProvincia_SelectedIndexChanged);
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.AutoSize = true;
            this.lblLocalidad.Location = new System.Drawing.Point(30, 440);
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.Size = new System.Drawing.Size(61, 15);
            this.lblLocalidad.Text = "Localidad:";
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(30, 460);
            this.cmbLocalidad.Size = new System.Drawing.Size(240, 23);
            this.cmbLocalidad.TabIndex = 7;
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.Location = new System.Drawing.Point(30, 500);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(27, 15);
            this.lblRol.Text = "Rol:";
            // 
            // cmbRol
            // 
            this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new System.Drawing.Point(30, 520);
            this.cmbRol.Size = new System.Drawing.Size(240, 23);
            this.cmbRol.TabIndex = 8;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(30, 570);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(170, 570);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 30);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // CrearPersonaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 621);
            this.Controls.Add(this.lblNombreCompleto);
            this.Controls.Add(this.txtNombreCompleto);
            this.Controls.Add(this.lblDni);
            this.Controls.Add(this.txtDni);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.lblDireccion);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.lblProvincia);
            this.Controls.Add(this.cmbProvincia);
            this.Controls.Add(this.lblLocalidad);
            this.Controls.Add(this.cmbLocalidad);
            this.Controls.Add(this.lblRol);
            this.Controls.Add(this.cmbRol);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Name = "CrearPersonaForm";
            this.Text = "Crear Persona";
            this.Load += new System.EventHandler(this.CrearPersonaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

