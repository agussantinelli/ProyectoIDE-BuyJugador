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
            this.SuspendLayout();
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new System.Drawing.Point(30, 20);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new System.Drawing.Size(39, 15);
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new System.Drawing.Point(30, 40);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new System.Drawing.Size(240, 23);
            txtEmail.TabIndex = 1;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new System.Drawing.Point(30, 80);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new System.Drawing.Size(55, 15);
            lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new System.Drawing.Point(30, 100);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new System.Drawing.Size(240, 23);
            txtTelefono.TabIndex = 3;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new System.Drawing.Point(30, 140);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new System.Drawing.Size(60, 15);
            lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new System.Drawing.Point(30, 160);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new System.Drawing.Size(240, 23);
            txtDireccion.TabIndex = 5;
            // 
            // lblProvincia
            // 
            lblProvincia.AutoSize = true;
            lblProvincia.Location = new System.Drawing.Point(30, 200);
            lblProvincia.Name = "lblProvincia";
            lblProvincia.Size = new System.Drawing.Size(59, 15);
            lblProvincia.Text = "Provincia:";
            // 
            // cmbProvincia
            // 
            cmbProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbProvincia.FormattingEnabled = true;
            cmbProvincia.Location = new System.Drawing.Point(30, 220);
            cmbProvincia.Name = "cmbProvincia";
            cmbProvincia.Size = new System.Drawing.Size(240, 23);
            cmbProvincia.TabIndex = 7;
            cmbProvincia.SelectedIndexChanged += cmbProvincia_SelectedIndexChanged;
            // 
            // lblLocalidad
            // 
            lblLocalidad.AutoSize = true;
            lblLocalidad.Location = new System.Drawing.Point(30, 260);
            lblLocalidad.Name = "lblLocalidad";
            lblLocalidad.Size = new System.Drawing.Size(61, 15);
            lblLocalidad.Text = "Localidad:";
            // 
            // cmbLocalidad
            // 
            cmbLocalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbLocalidad.FormattingEnabled = true;
            cmbLocalidad.Location = new System.Drawing.Point(30, 280);
            cmbLocalidad.Name = "cmbLocalidad";
            cmbLocalidad.Size = new System.Drawing.Size(240, 23);
            cmbLocalidad.TabIndex = 9;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new System.Drawing.Point(30, 320);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new System.Drawing.Size(100, 30);
            btnCancelar.TabIndex = 11;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new System.Drawing.Point(170, 320);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(100, 30);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // EditarPersonaForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(304, 371);
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
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

