namespace WinForms
{
    partial class TipoProducto
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
            this.dgvTiposProducto = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiposProducto)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTiposProducto
            // 
            this.dgvTiposProducto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTiposProducto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTiposProducto.Location = new System.Drawing.Point(0, 0);
            this.dgvTiposProducto.Name = "dgvTiposProducto";
            this.dgvTiposProducto.RowTemplate.Height = 25;
            this.dgvTiposProducto.Size = new System.Drawing.Size(800, 450);
            this.dgvTiposProducto.TabIndex = 0;
            // 
            // TipoProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvTiposProducto);
            this.Name = "TipoProducto";
            this.Text = "Tipos de Producto";
            this.Load += new System.EventHandler(this.TipoProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTiposProducto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTiposProducto;
    }
}
