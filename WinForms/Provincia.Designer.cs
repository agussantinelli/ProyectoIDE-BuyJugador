namespace WinForms
{
    partial class Provincia
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvProvincias;
        private System.Windows.Forms.Button btnVolver;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            dgvProvincias = new DataGridView();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProvincias).BeginInit();
            SuspendLayout();
            // 
            // dgvProvincias
            // 
            dgvProvincias.AllowUserToAddRows = false;
            dgvProvincias.AllowUserToDeleteRows = false;
            dgvProvincias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProvincias.Location = new Point(20, 20);
            dgvProvincias.MultiSelect = false;
            dgvProvincias.Name = "dgvProvincias";
            dgvProvincias.ReadOnly = true;
            dgvProvincias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProvincias.Size = new Size(740, 360);
            dgvProvincias.TabIndex = 0;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(20, 398);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 30);
            btnVolver.TabIndex = 1;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // Provincia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnVolver);
            Controls.Add(dgvProvincias);
            Name = "Provincia";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestión de Provincias";
            Load += Provincia_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProvincias).EndInit();
            ResumeLayout(false);

        }
        #endregion
    }
}
