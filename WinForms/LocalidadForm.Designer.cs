namespace WinForms
{
    partial class LocalidadForm
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
            this.dgvLocalidades = new System.Windows.Forms.DataGridView();
            this.btnVolver = new System.Windows.Forms.Button();
            this.cboProvincias = new System.Windows.Forms.ComboBox();
            this.lblFiltrar = new System.Windows.Forms.Label();
            this.btnOrdenarAZ = new System.Windows.Forms.Button();
            this.btnOrdenarZA = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalidades)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLocalidades
            // 
            this.dgvLocalidades.AllowUserToAddRows = false;
            this.dgvLocalidades.AllowUserToDeleteRows = false;
            this.dgvLocalidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalidades.Location = new System.Drawing.Point(12, 53);
            this.dgvLocalidades.MultiSelect = false;
            this.dgvLocalidades.Name = "dgvLocalidades";
            this.dgvLocalidades.ReadOnly = true;
            this.dgvLocalidades.RowTemplate.Height = 25;
            this.dgvLocalidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalidades.Size = new System.Drawing.Size(776, 356);
            this.dgvLocalidades.TabIndex = 0;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(713, 415);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // cboProvincias
            // 
            this.cboProvincias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProvincias.FormattingEnabled = true;
            this.cboProvincias.Location = new System.Drawing.Point(145, 12);
            this.cboProvincias.Name = "cboProvincias";
            this.cboProvincias.Size = new System.Drawing.Size(217, 21);
            this.cboProvincias.TabIndex = 2;
            this.cboProvincias.SelectedIndexChanged += new System.EventHandler(this.cboProvincias_SelectedIndexChanged);
            // 
            // lblFiltrar
            // 
            this.lblFiltrar.AutoSize = true;
            this.lblFiltrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltrar.Location = new System.Drawing.Point(12, 15);
            this.lblFiltrar.Name = "lblFiltrar";
            this.lblFiltrar.Size = new System.Drawing.Size(127, 15);
            this.lblFiltrar.TabIndex = 3;
            this.lblFiltrar.Text = "Filtrar por Provincia:";
            // 
            // btnOrdenarAZ
            // 
            this.btnOrdenarAZ.Location = new System.Drawing.Point(632, 12);
            this.btnOrdenarAZ.Name = "btnOrdenarAZ";
            this.btnOrdenarAZ.Size = new System.Drawing.Size(75, 23);
            this.btnOrdenarAZ.TabIndex = 4;
            this.btnOrdenarAZ.Text = "A-Z";
            this.btnOrdenarAZ.UseVisualStyleBackColor = true;
            this.btnOrdenarAZ.Click += new System.EventHandler(this.btnOrdenar_Click);
            // 
            // btnOrdenarZA
            // 
            this.btnOrdenarZA.Location = new System.Drawing.Point(713, 12);
            this.btnOrdenarZA.Name = "btnOrdenarZA";
            this.btnOrdenarZA.Size = new System.Drawing.Size(75, 23);
            this.btnOrdenarZA.TabIndex = 5;
            this.btnOrdenarZA.Text = "Z-A";
            this.btnOrdenarZA.UseVisualStyleBackColor = true;
            this.btnOrdenarZA.Click += new System.EventHandler(this.btnOrdenar_Click);
            // 
            // LocalidadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOrdenarZA);
            this.Controls.Add(this.btnOrdenarAZ);
            this.Controls.Add(this.lblFiltrar);
            this.Controls.Add(this.cboProvincias);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.dgvLocalidades);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LocalidadForm";
            this.Text = "Gestión de Localidades";
            this.Load += new System.EventHandler(this.LocalidadForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLocalidades;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.ComboBox cboProvincias;
        private System.Windows.Forms.Label lblFiltrar;
        private System.Windows.Forms.Button btnOrdenarAZ;
        private System.Windows.Forms.Button btnOrdenarZA;
    }
}

