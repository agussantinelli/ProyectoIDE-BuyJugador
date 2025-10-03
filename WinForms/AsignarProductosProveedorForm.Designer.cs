namespace WinForms
{
    partial class AsignarProductosProveedorForm
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
            this.dgvDisponibles = new System.Windows.Forms.DataGridView();
            this.dgvAsignados = new System.Windows.Forms.DataGridView();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblDisponibles = new System.Windows.Forms.Label();
            this.lblAsignados = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisponibles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsignados)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDisponibles
            // 
            this.dgvDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisponibles.Location = new System.Drawing.Point(12, 41);
            this.dgvDisponibles.Name = "dgvDisponibles";
            this.dgvDisponibles.RowTemplate.Height = 25;
            this.dgvDisponibles.Size = new System.Drawing.Size(320, 350);
            this.dgvDisponibles.TabIndex = 0;
            // 
            // dgvAsignados
            // 
            this.dgvAsignados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAsignados.Location = new System.Drawing.Point(452, 41);
            this.dgvAsignados.Name = "dgvAsignados";
            this.dgvAsignados.RowTemplate.Height = 25;
            this.dgvAsignados.Size = new System.Drawing.Size(320, 350);
            this.dgvAsignados.TabIndex = 1;
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(350, 150);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(84, 33);
            this.btnAsignar.TabIndex = 2;
            this.btnAsignar.Text = ">>";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.Location = new System.Drawing.Point(350, 220);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(84, 33);
            this.btnQuitar.TabIndex = 3;
            this.btnQuitar.Text = "<<";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(672, 408);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 30);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Cerrar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblDisponibles
            // 
            this.lblDisponibles.AutoSize = true;
            this.lblDisponibles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDisponibles.Location = new System.Drawing.Point(12, 18);
            this.lblDisponibles.Name = "lblDisponibles";
            this.lblDisponibles.Size = new System.Drawing.Size(130, 15);
            this.lblDisponibles.TabIndex = 5;
            this.lblDisponibles.Text = "Productos Disponibles";
            // 
            // lblAsignados
            // 
            this.lblAsignados.AutoSize = true;
            this.lblAsignados.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAsignados.Location = new System.Drawing.Point(452, 18);
            this.lblAsignados.Name = "lblAsignados";
            this.lblAsignados.Size = new System.Drawing.Size(122, 15);
            this.lblAsignados.TabIndex = 6;
            this.lblAsignados.Text = "Productos Asignados";
            // 
            // AsignarProductosProveedorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 450);
            this.Controls.Add(this.lblAsignados);
            this.Controls.Add(this.lblDisponibles);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.dgvAsignados);
            this.Controls.Add(this.dgvDisponibles);
            this.Name = "AsignarProductosProveedorForm";
            this.Text = "Asignar Productos a Proveedor";
            this.Load += new System.EventHandler(this.AsignarProductosProveedorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisponibles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsignados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDisponibles;
        private System.Windows.Forms.DataGridView dgvAsignados;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblDisponibles;
        private System.Windows.Forms.Label lblAsignados;
    }
}
