namespace WinForms
{
    partial class ProvinciasForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreProvincia = new System.Windows.Forms.TextBox();
            this.txtCodigoProvincia = new System.Windows.Forms.TextBox();
            this.btnEliminarProvincia = new System.Windows.Forms.Button();
            this.btnActualizarProvincia = new System.Windows.Forms.Button();
            this.btnAgregarProvincia = new System.Windows.Forms.Button();
            this.dgvProvincias = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProvincias)).BeginInit();
            this.SuspendLayout();
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Nombre";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Código";
            //
            // txtNombreProvincia
            //
            this.txtNombreProvincia.Location = new System.Drawing.Point(82, 206);
            this.txtNombreProvincia.Name = "txtNombreProvincia";
            this.txtNombreProvincia.Size = new System.Drawing.Size(200, 23);
            this.txtNombreProvincia.TabIndex = 6;
            //
            // txtCodigoProvincia
            //
            this.txtCodigoProvincia.Location = new System.Drawing.Point(82, 169);
            this.txtCodigoProvincia.Name = "txtCodigoProvincia";
            this.txtCodigoProvincia.Size = new System.Drawing.Size(200, 23);
            this.txtCodigoProvincia.TabIndex = 5;
            //
            // btnEliminarProvincia
            //
            this.btnEliminarProvincia.Location = new System.Drawing.Point(162, 316);
            this.btnEliminarProvincia.Name = "btnEliminarProvincia";
            this.btnEliminarProvincia.Size = new System.Drawing.Size(120, 23);
            this.btnEliminarProvincia.TabIndex = 4;
            this.btnEliminarProvincia.Text = "Eliminar";
            this.btnEliminarProvincia.UseVisualStyleBackColor = true;
            this.btnEliminarProvincia.Click += new System.EventHandler(this.btnEliminarProvincia_Click);
            //
            // btnActualizarProvincia
            //
            this.btnActualizarProvincia.Location = new System.Drawing.Point(19, 316);
            this.btnActualizarProvincia.Name = "btnActualizarProvincia";
            this.btnActualizarProvincia.Size = new System.Drawing.Size(120, 23);
            this.btnActualizarProvincia.TabIndex = 3;
            this.btnActualizarProvincia.Text = "Actualizar";
            this.btnActualizarProvincia.UseVisualStyleBackColor = true;
            this.btnActualizarProvincia.Click += new System.EventHandler(this.btnActualizarProvincia_Click);
            //
            // btnAgregarProvincia
            //
            this.btnAgregarProvincia.Location = new System.Drawing.Point(91, 266);
            this.btnAgregarProvincia.Name = "btnAgregarProvincia";
            this.btnAgregarProvincia.Size = new System.Drawing.Size(120, 23);
            this.btnAgregarProvincia.TabIndex = 2;
            this.btnAgregarProvincia.Text = "Agregar";
            this.btnAgregarProvincia.UseVisualStyleBackColor = true;
            this.btnAgregarProvincia.Click += new System.EventHandler(this.btnAgregarProvincia_Click);
            //
            // dgvProvincias
            //
            this.dgvProvincias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProvincias.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvProvincias.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvProvincias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProvincias.Location = new System.Drawing.Point(300, 6);
            this.dgvProvincias.Name = "dgvProvincias";
            this.dgvProvincias.Size = new System.Drawing.Size(630, 560);
            this.dgvProvincias.TabIndex = 0;
            this.dgvProvincias.SelectionChanged += new System.EventHandler(this.dgvProvincias_SelectionChanged);
            //
            // ProvinciasForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNombreProvincia);
            this.Controls.Add(this.txtCodigoProvincia);
            this.Controls.Add(this.btnEliminarProvincia);
            this.Controls.Add(this.btnActualizarProvincia);
            this.Controls.Add(this.btnAgregarProvincia);
            this.Controls.Add(this.dgvProvincias);
            this.Name = "ProvinciasForm";
            this.Text = "Gestión de Provincias";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProvincias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreProvincia;
        private System.Windows.Forms.TextBox txtCodigoProvincia;
        private System.Windows.Forms.Button btnEliminarProvincia;
        private System.Windows.Forms.Button btnActualizarProvincia;
        private System.Windows.Forms.Button btnAgregarProvincia;
        private System.Windows.Forms.DataGridView dgvProvincias;
    }
}