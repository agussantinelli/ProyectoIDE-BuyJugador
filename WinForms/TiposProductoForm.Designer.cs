namespace WinForms
{
    partial class TiposProductoForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNombreTipoProducto = new System.Windows.Forms.TextBox();
            this.txtIdTipoProducto = new System.Windows.Forms.TextBox();
            this.btnEliminarTipoProducto = new System.Windows.Forms.Button();
            this.btnActualizarTipoProducto = new System.Windows.Forms.Button();
            this.btnAgregarTipoProducto = new System.Windows.Forms.Button();
            this.dgvTiposProducto = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiposProducto)).BeginInit();
            this.SuspendLayout();
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Nombre";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "ID";
            //
            // txtNombreTipoProducto
            //
            this.txtNombreTipoProducto.Location = new System.Drawing.Point(82, 206);
            this.txtNombreTipoProducto.Name = "txtNombreTipoProducto";
            this.txtNombreTipoProducto.Size = new System.Drawing.Size(200, 23);
            this.txtNombreTipoProducto.TabIndex = 15;
            //
            // txtIdTipoProducto
            //
            this.txtIdTipoProducto.Location = new System.Drawing.Point(82, 169);
            this.txtIdTipoProducto.Name = "txtIdTipoProducto";
            this.txtIdTipoProducto.Size = new System.Drawing.Size(200, 23);
            this.txtIdTipoProducto.TabIndex = 14;
            //
            // btnEliminarTipoProducto
            //
            this.btnEliminarTipoProducto.Location = new System.Drawing.Point(162, 316);
            this.btnEliminarTipoProducto.Name = "btnEliminarTipoProducto";
            this.btnEliminarTipoProducto.Size = new System.Drawing.Size(120, 23);
            this.btnEliminarTipoProducto.TabIndex = 13;
            this.btnEliminarTipoProducto.Text = "Eliminar";
            this.btnEliminarTipoProducto.UseVisualStyleBackColor = true;
            this.btnEliminarTipoProducto.Click += new System.EventHandler(this.btnEliminarTipoProducto_Click);
            //
            // btnActualizarTipoProducto
            //
            this.btnActualizarTipoProducto.Location = new System.Drawing.Point(19, 316);
            this.btnActualizarTipoProducto.Name = "btnActualizarTipoProducto";
            this.btnActualizarTipoProducto.Size = new System.Drawing.Size(120, 23);
            this.btnActualizarTipoProducto.TabIndex = 12;
            this.btnActualizarTipoProducto.Text = "Actualizar";
            this.btnActualizarTipoProducto.UseVisualStyleBackColor = true;
            this.btnActualizarTipoProducto.Click += new System.EventHandler(this.btnActualizarTipoProducto_Click);
            //
            // btnAgregarTipoProducto
            //
            this.btnAgregarTipoProducto.Location = new System.Drawing.Point(91, 266);
            this.btnAgregarTipoProducto.Name = "btnAgregarTipoProducto";
            this.btnAgregarTipoProducto.Size = new System.Drawing.Size(120, 23);
            this.btnAgregarTipoProducto.TabIndex = 11;
            this.btnAgregarTipoProducto.Text = "Agregar";
            this.btnAgregarTipoProducto.UseVisualStyleBackColor = true;
            this.btnAgregarTipoProducto.Click += new System.EventHandler(this.btnAgregarTipoProducto_Click);
            //
            // dgvTiposProducto
            //
            this.dgvTiposProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTiposProducto.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvTiposProducto.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvTiposProducto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTiposProducto.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvTiposProducto.Location = new System.Drawing.Point(300, 6);
            this.dgvTiposProducto.Name = "dgvTiposProducto";
            this.dgvTiposProducto.Size = new System.Drawing.Size(630, 560);
            this.dgvTiposProducto.TabIndex = 9;
            this.dgvTiposProducto.SelectionChanged += new System.EventHandler(this.dgvTiposProducto_SelectionChanged);
            //
            // TiposProductoForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNombreTipoProducto);
            this.Controls.Add(this.txtIdTipoProducto);
            this.Controls.Add(this.btnEliminarTipoProducto);
            this.Controls.Add(this.btnActualizarTipoProducto);
            this.Controls.Add(this.btnAgregarTipoProducto);
            this.Controls.Add(this.dgvTiposProducto);
            this.Name = "TiposProductoForm";
            this.Text = "Gestión de Tipos de Producto";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiposProducto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNombreTipoProducto;
        private System.Windows.Forms.TextBox txtIdTipoProducto;
        private System.Windows.Forms.Button btnEliminarTipoProducto;
        private System.Windows.Forms.Button btnActualizarTipoProducto;
        private System.Windows.Forms.Button btnAgregarTipoProducto;
        private System.Windows.Forms.DataGridView dgvTiposProducto;
    }
}