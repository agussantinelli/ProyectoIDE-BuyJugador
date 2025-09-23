namespace WinForms
{
    partial class EditarPersonaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.ComboBox cmbProvincia;
        private System.Windows.Forms.Label lblProvincia;
        private System.Windows.Forms.ComboBox cmbLocalidad;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label lblLocalidad;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtEmail = new System.Windows.Forms.TextBox();
            txtTelefono = new System.Windows.Forms.TextBox();
            txtDireccion = new System.Windows.Forms.TextBox();
            cmbProvincia = new System.Windows.Forms.ComboBox();
            lblProvincia = new System.Windows.Forms.Label();
            cmbLocalidad = new System.Windows.Forms.ComboBox();
            btnGuardar = new System.Windows.Forms.Button();
            btnCancelar = new System.Windows.Forms.Button();
            lblEmail = new System.Windows.Forms.Label();
            lblTelefono = new System.Windows.Forms.Label();
            lblDireccion = new System.Windows.Forms.Label();
            lblLocalidad = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // txtEmail
            // 
            txtEmail.Location = new System.Drawing.Point(30, 40);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new System.Drawing.Size(240, 23);
            txtEmail.TabIndex = 0;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new System.Drawing.Point(30, 20);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new System.Drawing.Size(39, 15);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new System.Drawing.Point(30, 95);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new System.Drawing.Size(240, 23);
            txtTelefono.TabIndex = 2;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new System.Drawing.Point(30, 75);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new System.Drawing.Size(55, 15);
            lblTelefono.TabIndex = 3;
            lblTelefono.Text = "Teléfono:";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new System.Drawing.Point(30, 150);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new System.Drawing.Size(240, 23);
            txtDireccion.TabIndex = 4;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new System.Drawing.Point(30, 130);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new System.Drawing.Size(60, 15);
            lblDireccion.TabIndex = 5;
            lblDireccion.Text = "Dirección:";
            // 
            // cmbProvincia
            // 
            cmbProvincia.Location = new System.Drawing.Point(30, 205);
            cmbProvincia.Name = "cmbProvincia";
            cmbProvincia.Size = new System.Drawing.Size(240, 23);
            cmbProvincia.TabIndex = 6;
            cmbProvincia.SelectedIndexChanged += cmbProvincia_SelectedIndexChanged;
            // 
            // lblProvincia
            // 
            lblProvincia.AutoSize = true;
            lblProvincia.Location = new System.Drawing.Point(30, 185);
            lblProvincia.Name = "lblProvincia";
            lblProvincia.Size = new System.Drawing.Size(59, 15);
            lblProvincia.TabIndex = 7;
            lblProvincia.Text = "Provincia:";
            // 
            // cmbLocalidad
            // 
            cmbLocalidad.Location = new System.Drawing.Point(30, 260);
            cmbLocalidad.Name = "cmbLocalidad";
            cmbLocalidad.Size = new System.Drawing.Size(240, 23);
            cmbLocalidad.TabIndex = 8;
            // 
            // lblLocalidad
            // 
            lblLocalidad.AutoSize = true;
            lblLocalidad.Location = new System.Drawing.Point(30, 240);
            lblLocalidad.Name = "lblLocalidad";
            lblLocalidad.Size = new System.Drawing.Size(61, 15);
            lblLocalidad.TabIndex = 9;
            lblLocalidad.Text = "Localidad:";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new System.Drawing.Point(30, 310);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(100, 30);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new System.Drawing.Point(170, 310);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new System.Drawing.Size(100, 30);
            btnCancelar.TabIndex = 11;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // EditarPersonaForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(310, 370);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(lblDireccion);
            Controls.Add(txtDireccion);
            Controls.Add(lblProvincia);
            Controls.Add(cmbProvincia);
            Controls.Add(lblLocalidad);
            Controls.Add(cmbLocalidad);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            Name = "EditarPersonaForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Editar Persona";
            Load += EditarPersonaForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
