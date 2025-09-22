namespace WinForms
{
    partial class Provincia
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvProvincias;
        private System.Windows.Forms.Button btnVolver;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            dgvProvincias = new DataGridView();
            btnVolver = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvProvincias).BeginInit();
            SuspendLayout();

            // dgvProvincias
            dgvProvincias.AllowUserToAddRows = false;
            dgvProvincias.AllowUserToDeleteRows = false;
            dgvProvincias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProvincias.Location = new Point(20, 20);
            dgvProvincias.MultiSelect = false;
            dgvProvincias.Name = "dgvProvincias";
            dgvProvincias.ReadOnly = true;
            dgvProvincias.RowTemplate.Height = 25;
            dgvProvincias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProvincias.Size = new Size(1040, 400); 

            // btnVolver
            btnVolver.Location = new Point(20, 470);
            btnVolver.Size = new Size(100, 30);
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;

            // Provincia
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 520);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(btnVolver);
            Controls.Add(dgvProvincias);
            Name = "Provincia";
            Text = "Gestión de Provincias";
            Load += Provincia_Load;

            ((System.ComponentModel.ISupportInitialize)dgvProvincias).EndInit();
            ResumeLayout(false);
        }
        #endregion
    }
}
