namespace WinForms
{
    partial class VerProveedoresProductoForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvProveedores;
        private System.Windows.Forms.Button btnCerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvProveedores = new DataGridView();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).BeginInit();
            SuspendLayout();
            // 
            // dgvProveedores
            // 
            dgvProveedores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProveedores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProveedores.Location = new Point(10, 10);
            dgvProveedores.Name = "dgvProveedores";
            dgvProveedores.Size = new Size(805, 367);
            dgvProveedores.TabIndex = 0;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCerrar.Location = new Point(716, 383);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(100, 29);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // VerProveedoresProductoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(826, 422);
            Controls.Add(btnCerrar);
            Controls.Add(dgvProveedores);
            Name = "VerProveedoresProductoForm";
            Text = "Proveedores del Producto";
            Load += VerProveedoresProductoForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).EndInit();
            ResumeLayout(false);
        }
    }
}
