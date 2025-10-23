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
            dgvLocalidades = new DataGridView();
            btnVolver = new Button();
            cboProvincias = new ComboBox();
            lblFiltrar = new Label();
            btnOrdenarAZ = new Button();
            btnOrdenarZA = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLocalidades).BeginInit();
            SuspendLayout();
            dgvLocalidades.AllowUserToAddRows = false;
            dgvLocalidades.AllowUserToDeleteRows = false;
            dgvLocalidades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLocalidades.Location = new Point(14, 69);
            dgvLocalidades.Margin = new Padding(4, 4, 4, 4);
            dgvLocalidades.MultiSelect = false;
            dgvLocalidades.Name = "dgvLocalidades";
            dgvLocalidades.ReadOnly = true;
            dgvLocalidades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocalidades.Size = new Size(905, 441);
            dgvLocalidades.TabIndex = 0;
            btnVolver.Location = new Point(14, 534);
            btnVolver.Margin = new Padding(4, 4, 4, 4);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(117, 39);
            btnVolver.TabIndex = 1;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            cboProvincias.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProvincias.FormattingEnabled = true;
            cboProvincias.Location = new Point(194, 24);
            cboProvincias.Margin = new Padding(4, 4, 4, 4);
            cboProvincias.Name = "cboProvincias";
            cboProvincias.Size = new Size(291, 28);
            cboProvincias.TabIndex = 2;
            cboProvincias.SelectedIndexChanged += cboProvincias_SelectedIndexChanged;
            lblFiltrar.AutoSize = true;
            lblFiltrar.Location = new Point(14, 29);
            lblFiltrar.Margin = new Padding(4, 0, 4, 0);
            lblFiltrar.Name = "lblFiltrar";
            lblFiltrar.Size = new Size(153, 20);
            lblFiltrar.TabIndex = 3;
            lblFiltrar.Text = "Filtrar por Provincia:";
            btnOrdenarAZ.Location = new Point(632, 22);
            btnOrdenarAZ.Margin = new Padding(4, 4, 4, 4);
            btnOrdenarAZ.Name = "btnOrdenarAZ";
            btnOrdenarAZ.Size = new Size(113, 30);
            btnOrdenarAZ.TabIndex = 4;
            btnOrdenarAZ.Text = "Ordenar A-Z";
            btnOrdenarAZ.UseVisualStyleBackColor = true;
            btnOrdenarAZ.Click += btnOrdenar_Click;
            btnOrdenarZA.Location = new Point(752, 22);
            btnOrdenarZA.Margin = new Padding(4, 4, 4, 4);
            btnOrdenarZA.Name = "btnOrdenarZA";
            btnOrdenarZA.Size = new Size(113, 30);
            btnOrdenarZA.TabIndex = 5;
            btnOrdenarZA.Text = "Ordenar Z-A";
            btnOrdenarZA.UseVisualStyleBackColor = true;
            btnOrdenarZA.Click += btnOrdenar_Click;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 588);
            Controls.Add(btnOrdenarZA);
            Controls.Add(btnOrdenarAZ);
            Controls.Add(lblFiltrar);
            Controls.Add(cboProvincias);
            Controls.Add(btnVolver);
            Controls.Add(dgvLocalidades);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            Name = "LocalidadForm";
            Text = "Gestión de Localidades";
            Load += LocalidadForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLocalidades).EndInit();
            ResumeLayout(false);
            PerformLayout();

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
