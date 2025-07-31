namespace WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPageProvincias = new TabPage();
            label2 = new Label();
            label1 = new Label();
            txtNombreProvincia = new TextBox();
            txtCodigoProvincia = new TextBox();
            btnEliminarProvincia = new Button();
            btnActualizarProvincia = new Button();
            btnAgregarProvincia = new Button();
            btnCargarProvincias = new Button();
            dgvProvincias = new DataGridView();
            tabPageTiposProducto = new TabPage();
            label3 = new Label();
            label4 = new Label();
            txtNombreTipoProducto = new TextBox();
            txtIdTipoProducto = new TextBox();
            btnEliminarTipoProducto = new Button();
            btnActualizarTipoProducto = new Button();
            btnAgregarTipoProducto = new Button();
            btnCargarTiposProducto = new Button();
            dgvTiposProducto = new DataGridView();
            tabControl1.SuspendLayout();
            tabPageProvincias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProvincias).BeginInit();
            tabPageTiposProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTiposProducto).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageProvincias);
            tabControl1.Controls.Add(tabPageTiposProducto);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 450);
            tabControl1.TabIndex = 0;
            // 
            // tabPageProvincias
            // 
            tabPageProvincias.Controls.Add(label2);
            tabPageProvincias.Controls.Add(label1);
            tabPageProvincias.Controls.Add(txtNombreProvincia);
            tabPageProvincias.Controls.Add(txtCodigoProvincia);
            tabPageProvincias.Controls.Add(btnEliminarProvincia);
            tabPageProvincias.Controls.Add(btnActualizarProvincia);
            tabPageProvincias.Controls.Add(btnAgregarProvincia);
            tabPageProvincias.Controls.Add(btnCargarProvincias);
            tabPageProvincias.Controls.Add(dgvProvincias);
            tabPageProvincias.Location = new Point(4, 24);
            tabPageProvincias.Name = "tabPageProvincias";
            tabPageProvincias.Padding = new Padding(3);
            tabPageProvincias.Size = new Size(792, 422);
            tabPageProvincias.TabIndex = 0;
            tabPageProvincias.Text = "Provincias";
            tabPageProvincias.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 219);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 8;
            label2.Text = "Nombre";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 182);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 7;
            label1.Text = "Código";
            // 
            // txtNombreProvincia
            // 
            txtNombreProvincia.Location = new Point(82, 216);
            txtNombreProvincia.Name = "txtNombreProvincia";
            txtNombreProvincia.Size = new Size(200, 23);
            txtNombreProvincia.TabIndex = 6;
            // 
            // txtCodigoProvincia
            // 
            txtCodigoProvincia.Location = new Point(82, 179);
            txtCodigoProvincia.Name = "txtCodigoProvincia";
            txtCodigoProvincia.Size = new Size(200, 23);
            txtCodigoProvincia.TabIndex = 5;
            // 
            // btnEliminarProvincia
            // 
            btnEliminarProvincia.Location = new Point(21, 350);
            btnEliminarProvincia.Name = "btnEliminarProvincia";
            btnEliminarProvincia.Size = new Size(120, 23);
            btnEliminarProvincia.TabIndex = 4;
            btnEliminarProvincia.Text = "Eliminar";
            btnEliminarProvincia.UseVisualStyleBackColor = true;
            btnEliminarProvincia.Click += btnEliminarProvincia_Click;
            // 
            // btnActualizarProvincia
            // 
            btnActualizarProvincia.Location = new Point(162, 307);
            btnActualizarProvincia.Name = "btnActualizarProvincia";
            btnActualizarProvincia.Size = new Size(120, 23);
            btnActualizarProvincia.TabIndex = 3;
            btnActualizarProvincia.Text = "Actualizar";
            btnActualizarProvincia.UseVisualStyleBackColor = true;
            btnActualizarProvincia.Click += btnActualizarProvincia_Click;
            // 
            // btnAgregarProvincia
            // 
            btnAgregarProvincia.Location = new Point(21, 307);
            btnAgregarProvincia.Name = "btnAgregarProvincia";
            btnAgregarProvincia.Size = new Size(120, 23);
            btnAgregarProvincia.TabIndex = 2;
            btnAgregarProvincia.Text = "Agregar";
            btnAgregarProvincia.UseVisualStyleBackColor = true;
            btnAgregarProvincia.Click += btnAgregarProvincia_Click;
            // 
            // btnCargarProvincias
            // 
            btnCargarProvincias.Location = new Point(21, 264);
            btnCargarProvincias.Name = "btnCargarProvincias";
            btnCargarProvincias.Size = new Size(261, 23);
            btnCargarProvincias.TabIndex = 1;
            btnCargarProvincias.Text = "Cargar Provincias";
            btnCargarProvincias.UseVisualStyleBackColor = true;
            btnCargarProvincias.Click += btnCargarProvincias_Click;
            // 
            // dgvProvincias
            // 
            dgvProvincias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProvincias.Location = new Point(300, 6);
            dgvProvincias.Name = "dgvProvincias";
            dgvProvincias.Size = new Size(486, 410);
            dgvProvincias.TabIndex = 0;
            dgvProvincias.CellContentClick += dgvProvincias_CellContentClick;
            dgvProvincias.SelectionChanged += dgvProvincias_SelectionChanged;
            // 
            // tabPageTiposProducto
            // 
            tabPageTiposProducto.Controls.Add(label3);
            tabPageTiposProducto.Controls.Add(label4);
            tabPageTiposProducto.Controls.Add(txtNombreTipoProducto);
            tabPageTiposProducto.Controls.Add(txtIdTipoProducto);
            tabPageTiposProducto.Controls.Add(btnEliminarTipoProducto);
            tabPageTiposProducto.Controls.Add(btnActualizarTipoProducto);
            tabPageTiposProducto.Controls.Add(btnAgregarTipoProducto);
            tabPageTiposProducto.Controls.Add(btnCargarTiposProducto);
            tabPageTiposProducto.Controls.Add(dgvTiposProducto);
            tabPageTiposProducto.Location = new Point(4, 24);
            tabPageTiposProducto.Name = "tabPageTiposProducto";
            tabPageTiposProducto.Padding = new Padding(3);
            tabPageTiposProducto.Size = new Size(792, 422);
            tabPageTiposProducto.TabIndex = 1;
            tabPageTiposProducto.Text = "Tipos de Producto";
            tabPageTiposProducto.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 219);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 17;
            label3.Text = "Nombre";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 182);
            label4.Name = "label4";
            label4.Size = new Size(18, 15);
            label4.TabIndex = 16;
            label4.Text = "ID";
            // 
            // txtNombreTipoProducto
            // 
            txtNombreTipoProducto.Location = new Point(82, 216);
            txtNombreTipoProducto.Name = "txtNombreTipoProducto";
            txtNombreTipoProducto.Size = new Size(200, 23);
            txtNombreTipoProducto.TabIndex = 15;
            // 
            // txtIdTipoProducto
            // 
            txtIdTipoProducto.Location = new Point(82, 179);
            txtIdTipoProducto.Name = "txtIdTipoProducto";
            txtIdTipoProducto.Size = new Size(200, 23);
            txtIdTipoProducto.TabIndex = 14;
            // 
            // btnEliminarTipoProducto
            // 
            btnEliminarTipoProducto.Location = new Point(21, 350);
            btnEliminarTipoProducto.Name = "btnEliminarTipoProducto";
            btnEliminarTipoProducto.Size = new Size(120, 23);
            btnEliminarTipoProducto.TabIndex = 13;
            btnEliminarTipoProducto.Text = "Eliminar";
            btnEliminarTipoProducto.UseVisualStyleBackColor = true;
            btnEliminarTipoProducto.Click += btnEliminarTipoProducto_Click;
            // 
            // btnActualizarTipoProducto
            // 
            btnActualizarTipoProducto.Location = new Point(162, 307);
            btnActualizarTipoProducto.Name = "btnActualizarTipoProducto";
            btnActualizarTipoProducto.Size = new Size(120, 23);
            btnActualizarTipoProducto.TabIndex = 12;
            btnActualizarTipoProducto.Text = "Actualizar";
            btnActualizarTipoProducto.UseVisualStyleBackColor = true;
            btnActualizarTipoProducto.Click += btnActualizarTipoProducto_Click;
            // 
            // btnAgregarTipoProducto
            // 
            btnAgregarTipoProducto.Location = new Point(21, 307);
            btnAgregarTipoProducto.Name = "btnAgregarTipoProducto";
            btnAgregarTipoProducto.Size = new Size(120, 23);
            btnAgregarTipoProducto.TabIndex = 11;
            btnAgregarTipoProducto.Text = "Agregar";
            btnAgregarTipoProducto.UseVisualStyleBackColor = true;
            btnAgregarTipoProducto.Click += btnAgregarTipoProducto_Click;
            // 
            // btnCargarTiposProducto
            // 
            btnCargarTiposProducto.Location = new Point(21, 264);
            btnCargarTiposProducto.Name = "btnCargarTiposProducto";
            btnCargarTiposProducto.Size = new Size(261, 23);
            btnCargarTiposProducto.TabIndex = 10;
            btnCargarTiposProducto.Text = "Cargar Tipos de Producto";
            btnCargarTiposProducto.UseVisualStyleBackColor = true;
            btnCargarTiposProducto.Click += btnCargarTiposProducto_Click;
            // 
            // dgvTiposProducto
            // 
            dgvTiposProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTiposProducto.Location = new Point(300, 6);
            dgvTiposProducto.Name = "dgvTiposProducto";
            dgvTiposProducto.Size = new Size(486, 410);
            dgvTiposProducto.TabIndex = 9;
            dgvTiposProducto.SelectionChanged += dgvTiposProducto_SelectionChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "CRUD WinForms API";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPageProvincias.ResumeLayout(false);
            tabPageProvincias.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProvincias).EndInit();
            tabPageTiposProducto.ResumeLayout(false);
            tabPageTiposProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTiposProducto).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageProvincias;
        private System.Windows.Forms.TabPage tabPageTiposProducto;
        private System.Windows.Forms.DataGridView dgvProvincias;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreProvincia;
        private System.Windows.Forms.TextBox txtCodigoProvincia;
        private System.Windows.Forms.Button btnEliminarProvincia;
        private System.Windows.Forms.Button btnActualizarProvincia;
        private System.Windows.Forms.Button btnAgregarProvincia;
        private System.Windows.Forms.Button btnCargarProvincias;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNombreTipoProducto;
        private System.Windows.Forms.TextBox txtIdTipoProducto;
        private System.Windows.Forms.Button btnEliminarTipoProducto;
        private System.Windows.Forms.Button btnActualizarTipoProducto;
        private System.Windows.Forms.Button btnAgregarTipoProducto;
        private System.Windows.Forms.Button btnCargarTiposProducto;
        private System.Windows.Forms.DataGridView dgvTiposProducto;
    }
}
}