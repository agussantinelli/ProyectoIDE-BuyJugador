namespace WinForms
{
    partial class TipoProducto
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
            dgvTiposProducto = new DataGridView();
            txtDescripcion = new TextBox();
            btnNuevo = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            lblDescripcion = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTiposProducto).BeginInit();
            SuspendLayout();
            // 
            // dgvTiposProducto
            // 
            dgvTiposProducto.AllowUserToAddRows = false;
            dgvTiposProducto.AllowUserToDeleteRows = false;
            dgvTiposProducto.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTiposProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTiposProducto.Location = new Point(14, 92);
            dgvTiposProducto.MultiSelect = false;
            dgvTiposProducto.Name = "dgvTiposProducto";
            dgvTiposProducto.ReadOnly = true;
            dgvTiposProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTiposProducto.Size = new Size(500, 300);
            dgvTiposProducto.TabIndex = 0;
            dgvTiposProducto.SelectionChanged += dgvTiposProducto_SelectionChanged;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(133, 25);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(222, 23);
            txtDescripcion.TabIndex = 1;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(380, 12);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(100, 30);
            btnNuevo.TabIndex = 2;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(380, 48);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(100, 30);
            btnActualizar.TabIndex = 3;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(380, 84);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(100, 30);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(14, 28);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(72, 15);
            lblDescripcion.TabIndex = 5;
            lblDescripcion.Text = "Descripción:";
            // 
            // TipoProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(540, 420);
            Controls.Add(lblDescripcion);
            Controls.Add(btnEliminar);
            Controls.Add(btnActualizar);
            Controls.Add(btnNuevo);
            Controls.Add(txtDescripcion);
            Controls.Add(dgvTiposProducto);
            Name = "TipoProducto";
            Text = "Gestión de Tipos de Producto";
            Load += TipoProducto_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTiposProducto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvTiposProducto;
        private TextBox txtDescripcion;
        private Button btnNuevo;
        private Button btnActualizar;
        private Button btnEliminar;
        private Label lblDescripcion;
    }
}
