namespace WinForms
{
    partial class EditarProveedorForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            txtRazonSocial = new TextBox();
            label2 = new Label();
            txtCuit = new TextBox();
            label3 = new Label();
            txtEmail = new TextBox();
            label4 = new Label();
            txtTelefono = new TextBox();
            label5 = new Label();
            txtDireccion = new TextBox();
            label6 = new Label();
            cmbProvincia = new ComboBox();
            label7 = new Label();
            cmbLocalidad = new ComboBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            lblId = new Label();
            txtId = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 66);
            label1.Name = "label1";
            label1.Size = new Size(104, 20);
            label1.TabIndex = 2;
            label1.Text = "Razón Social:";
            // 
            // txtRazonSocial
            // 
            txtRazonSocial.Location = new Point(143, 66);
            txtRazonSocial.Name = "txtRazonSocial";
            txtRazonSocial.Size = new Size(219, 25);
            txtRazonSocial.TabIndex = 3;
            txtRazonSocial.TextChanged += txtRazonSocial_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 103);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 4;
            label2.Text = "CUIT:";
            // 
            // txtCuit
            // 
            txtCuit.Location = new Point(143, 103);
            txtCuit.Name = "txtCuit";
            txtCuit.Size = new Size(219, 25);
            txtCuit.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 141);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 6;
            label3.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(143, 141);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(219, 25);
            txtEmail.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 178);
            label4.Name = "label4";
            label4.Size = new Size(75, 20);
            label4.TabIndex = 8;
            label4.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(143, 178);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(219, 25);
            txtTelefono.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(26, 215);
            label5.Name = "label5";
            label5.Size = new Size(84, 20);
            label5.TabIndex = 10;
            label5.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(143, 215);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(219, 25);
            txtDireccion.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(26, 253);
            label6.Name = "label6";
            label6.Size = new Size(82, 20);
            label6.TabIndex = 12;
            label6.Text = "Provincia:";
            // 
            // cmbProvincia
            // 
            cmbProvincia.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProvincia.FormattingEnabled = true;
            cmbProvincia.Location = new Point(143, 253);
            cmbProvincia.Name = "cmbProvincia";
            cmbProvincia.Size = new Size(219, 28);
            cmbProvincia.TabIndex = 13;
            cmbProvincia.SelectedIndexChanged += cmbProvincia_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(26, 290);
            label7.Name = "label7";
            label7.Size = new Size(86, 20);
            label7.TabIndex = 14;
            label7.Text = "Localidad:";
            // 
            // cmbLocalidad
            // 
            cmbLocalidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLocalidad.FormattingEnabled = true;
            cmbLocalidad.Location = new Point(143, 290);
            cmbLocalidad.Name = "cmbLocalidad";
            cmbLocalidad.Size = new Size(219, 28);
            cmbLocalidad.TabIndex = 15;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(262, 338);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 34);
            btnGuardar.TabIndex = 16;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(102, 338);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 34);
            btnCancelar.TabIndex = 17;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(26, 28);
            lblId.Name = "lblId";
            lblId.Size = new Size(29, 20);
            lblId.TabIndex = 0;
            lblId.Text = "ID:";
            // 
            // txtId
            // 
            txtId.Location = new Point(143, 28);
            txtId.Name = "txtId";
            txtId.Size = new Size(219, 25);
            txtId.TabIndex = 1;
            // 
            // EditarProveedorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(390, 399);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(cmbLocalidad);
            Controls.Add(label7);
            Controls.Add(cmbProvincia);
            Controls.Add(label6);
            Controls.Add(txtDireccion);
            Controls.Add(label5);
            Controls.Add(txtTelefono);
            Controls.Add(label4);
            Controls.Add(txtEmail);
            Controls.Add(label3);
            Controls.Add(txtCuit);
            Controls.Add(label2);
            Controls.Add(txtRazonSocial);
            Controls.Add(label1);
            Controls.Add(txtId);
            Controls.Add(lblId);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditarProveedorForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Editar Proveedor";
            Load += EditarProveedorForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbProvincia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbLocalidad;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
    }
}
