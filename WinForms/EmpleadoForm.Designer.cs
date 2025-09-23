namespace WinForms
{
    partial class EmpleadoForm
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
            this.btnMenuProductos = new System.Windows.Forms.Button();
            this.btnMenuPersonas = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(0, 20); // Será centrado en el .cs
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(184, 30);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Panel Empleado";
            // 
            // btnMenuProductos
            // 
            this.btnMenuProductos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMenuProductos.Location = new System.Drawing.Point(110, 70);
            this.btnMenuProductos.Name = "btnMenuProductos";
            this.btnMenuProductos.Size = new System.Drawing.Size(280, 50);
            this.btnMenuProductos.TabIndex = 0;
            this.btnMenuProductos.Text = "Gestionar Productos";
            this.btnMenuProductos.UseVisualStyleBackColor = true;
            this.btnMenuProductos.Click += new System.EventHandler(this.btnMenuProductos_Click);
            // 
            // btnMenuPersonas
            // 
            this.btnMenuPersonas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMenuPersonas.Location = new System.Drawing.Point(110, 130);
            this.btnMenuPersonas.Name = "btnMenuPersonas";
            this.btnMenuPersonas.Size = new System.Drawing.Size(280, 50);
            this.btnMenuPersonas.TabIndex = 1;
            this.btnMenuPersonas.Text = "Gestionar Personas";
            this.btnMenuPersonas.UseVisualStyleBackColor = true;
            this.btnMenuPersonas.Click += new System.EventHandler(this.btnMenuPersonas_Click);
            // 
            // EmpleadoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 220);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnMenuPersonas);
            this.Controls.Add(this.btnMenuProductos);
            this.Name = "EmpleadoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel de Empleado";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnMenuProductos;
        private System.Windows.Forms.Button btnMenuPersonas;
        private System.Windows.Forms.Label lblTitle;
    }
}
