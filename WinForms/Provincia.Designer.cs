namespace WinForms
{
    partial class Provincia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvProvincias = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProvincias)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProvincias
            // 
            this.dgvProvincias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProvincias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProvincias.Location = new System.Drawing.Point(0, 0);
            this.dgvProvincias.Name = "dgvProvincias";
            this.dgvProvincias.RowTemplate.Height = 25;
            this.dgvProvincias.Size = new System.Drawing.Size(800, 450);
            this.dgvProvincias.TabIndex = 0;
            // 
            // Provincia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvProvincias);
            this.Name = "Provincia";
            this.Text = "Provincias";
            this.Load += new System.EventHandler(this.Provincia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProvincias)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProvincias;
    }
}
