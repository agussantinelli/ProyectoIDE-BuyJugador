namespace WinForms
{
    partial class AsignarProductosProveedorForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lstNoAsignados = new System.Windows.Forms.ListBox();
            this.lstAsignados = new System.Windows.Forms.ListBox();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnDesasignar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblNoAsignados = new System.Windows.Forms.Label();
            this.lblAsignados = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstNoAsignados
            // 
            this.lstNoAsignados.FormattingEnabled = true;
            this.lstNoAsignados.ItemHeight = 15;
            this.lstNoAsignados.Location = new System.Drawing.Point(12, 35);
            this.lstNoAsignados.Name = "lstNoAsignados";
            this.lstNoAsignados.Size = new System.Drawing.Size(200, 304);
            this.lstNoAsignados.TabIndex = 0;
            // 
            // lstAsignados
            // 
            this.lstAsignados.FormattingEnabled = true;
            this.lstAsignados.ItemHeight = 15;
            this.lstAsignados.Location = new System.Drawing.Point(272, 35);
            this.lstAsignados.Name = "lstAsignados";
            this.lstAsignados.Size = new System.Drawing.Size(200, 304);
            this.lstAsignados.TabIndex = 1;
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(218, 145);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(48, 23);
            this.btnAsignar.TabIndex = 2;
            this.btnAsignar.Text = ">>";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // btnDesasignar
            // 
            this.btnDesasignar.Location = new System.Drawing.Point(218, 174);
            this.btnDesasignar.Name = "btnDesasignar";
            this.btnDesasignar.Size = new System.Drawing.Size(48, 23);
            this.btnDesasignar.TabIndex = 3;
            this.btnDesasignar.Text = "<<";
            this.btnDesasignar.UseVisualStyleBackColor = true;
            this.btnDesasignar.Click += new System.EventHandler(this.btnDesasignar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(372, 345);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 30);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(12, 345);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblNoAsignados
            // 
            this.lblNoAsignados.AutoSize = true;
            this.lblNoAsignados.Location = new System.Drawing.Point(12, 17);
            this.lblNoAsignados.Name = "lblNoAsignados";
            this.lblNoAsignados.Size = new System.Drawing.Size(130, 15);
            this.lblNoAsignados.TabIndex = 6;
            this.lblNoAsignados.Text = "Productos Disponibles:";
            // 
            // lblAsignados
            // 
            this.lblAsignados.AutoSize = true;
            this.lblAsignados.Location = new System.Drawing.Point(272, 17);
            this.lblAsignados.Name = "lblAsignados";
            this.lblAsignados.Size = new System.Drawing.Size(126, 15);
            this.lblAsignados.TabIndex = 7;
            this.lblAsignados.Text = "Productos Asignados:";
            // 
            // AsignarProductosProveedorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 386);
            this.Controls.Add(this.lblAsignados);
            this.Controls.Add(this.lblNoAsignados);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnDesasignar);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.lstAsignados);
            this.Controls.Add(this.lstNoAsignados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsignarProductosProveedorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Asignar Productos";
            this.Load += new System.EventHandler(this.AsignarProductosProveedorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.ListBox lstNoAsignados;
        private System.Windows.Forms.ListBox lstAsignados;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnDesasignar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblNoAsignados;
        private System.Windows.Forms.Label lblAsignados;
    }
}
