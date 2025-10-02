namespace WinForms
{
    partial class ProvinciaForm
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
            this.dgvProvincias = new System.Windows.Forms.DataGridView();
            this.btnVolver = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvProvincias)).BeginInit();
            this.SuspendLayout();

            // dgvProvincias
            this.dgvProvincias.AllowUserToAddRows = false;
            this.dgvProvincias.AllowUserToDeleteRows = false;
            this.dgvProvincias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProvincias.Location = new System.Drawing.Point(20, 20);
            this.dgvProvincias.MultiSelect = false;
            this.dgvProvincias.Name = "dgvProvincias";
            this.dgvProvincias.ReadOnly = true;
            this.dgvProvincias.RowTemplate.Height = 25;
            this.dgvProvincias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProvincias.Size = new System.Drawing.Size(1040, 400);

            // btnVolver
            this.btnVolver.Location = new System.Drawing.Point(20, 470);
            this.btnVolver.Size = new System.Drawing.Size(100, 30);
            this.btnVolver.Text = "Volver";
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);

            // ProvinciaForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 520);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.dgvProvincias);
            this.Name = "ProvinciaForm";
            this.Text = "Provincias";
            this.Load += new System.EventHandler(this.Provincia_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvProvincias)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
    }
}
