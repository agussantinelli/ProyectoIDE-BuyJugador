namespace WinForms
{
    partial class ReporteVentasForm
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
            this.lblVendedor = new System.Windows.Forms.Label();
            this.cmbVendedores = new System.Windows.Forms.ComboBox();
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.lblVendedor.Location = new System.Drawing.Point(12, 20);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(84, 19);
            this.lblVendedor.TabIndex = 0;
            this.lblVendedor.Text = "Vendedor:";
            // 
            // cmbVendedores
            // 
            this.cmbVendedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVendedores.FormattingEnabled = true;
            this.cmbVendedores.Location = new System.Drawing.Point(102, 19);
            this.cmbVendedores.Name = "cmbVendedores";
            this.cmbVendedores.Size = new System.Drawing.Size(300, 25);
            this.cmbVendedores.TabIndex = 1;
            this.cmbVendedores.SelectedIndexChanged += new System.EventHandler(this.cmbVendedores_SelectedIndexChanged);
            // 
            // dgvReporte
            // 
            this.dgvReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.Location = new System.Drawing.Point(16, 60);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.ReadOnly = true;
            this.dgvReporte.RowTemplate.Height = 25;
            this.dgvReporte.Size = new System.Drawing.Size(977, 435);
            this.dgvReporte.TabIndex = 2;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Italic);
            this.lblInfo.Location = new System.Drawing.Point(13, 505);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 18);
            this.lblInfo.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(691, 502);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(300, 19);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ReporteVentasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 526);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgvReporte);
            this.Controls.Add(this.cmbVendedores);
            this.Controls.Add(this.lblVendedor);
            this.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "ReporteVentasForm";
            this.Text = "Reporte de Ventas";
            this.Load += new System.EventHandler(this.ReporteVentasForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVendedor;
        private System.Windows.Forms.ComboBox cmbVendedores;
        private System.Windows.Forms.DataGridView dgvReporte;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblTotal;
    }
}

