namespace WinForms
{
    partial class ProveedorForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TabControl tabControlProveedores;
        private System.Windows.Forms.TabPage tabPageActivos;
        private System.Windows.Forms.TabPage tabPageInactivos;
        private System.Windows.Forms.DataGridView dgvActivos;
        private System.Windows.Forms.DataGridView dgvInactivos;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnReactivar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnAsignarProductos;
        private System.Windows.Forms.Button btnVerProductos;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.tabControlProveedores = new System.Windows.Forms.TabControl();
            this.tabPageActivos = new System.Windows.Forms.TabPage();
            this.dgvActivos = new System.Windows.Forms.DataGridView();
            this.tabPageInactivos = new System.Windows.Forms.TabPage();
            this.dgvInactivos = new System.Windows.Forms.DataGridView();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAsignarProductos = new System.Windows.Forms.Button();
            this.btnVerProductos = new System.Windows.Forms.Button();
            this.btnReactivar = new System.Windows.Forms.Button();
            this.tabControlProveedores.SuspendLayout();
            this.tabPageActivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivos)).BeginInit();
            this.tabPageInactivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInactivos)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(249, 12);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(300, 30);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(12, 15);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(231, 22);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar por Razón Social:";
            // 
            // tabControlProveedores
            // 
            this.tabControlProveedores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProveedores.Controls.Add(this.tabPageActivos);
            this.tabControlProveedores.Controls.Add(this.tabPageInactivos);
            this.tabControlProveedores.Location = new System.Drawing.Point(12, 50);
            this.tabControlProveedores.Name = "tabControlProveedores";
            this.tabControlProveedores.SelectedIndex = 0;
            this.tabControlProveedores.Size = new System.Drawing.Size(1176, 420);
            this.tabControlProveedores.TabIndex = 2;
            this.tabControlProveedores.SelectedIndexChanged += new System.EventHandler(this.tabControlProveedores_SelectedIndexChanged);
            // 
            // tabPageActivos
            // 
            this.tabPageActivos.Controls.Add(this.dgvActivos);
            this.tabPageActivos.Location = new System.Drawing.Point(4, 30);
            this.tabPageActivos.Name = "tabPageActivos";
            this.tabPageActivos.Size = new System.Drawing.Size(1168, 386);
            this.tabPageActivos.TabIndex = 0;
            this.tabPageActivos.Text = "Activos";
            this.tabPageActivos.UseVisualStyleBackColor = true;
            // 
            // dgvActivos
            // 
            this.dgvActivos.AutoGenerateColumns = false;   
            this.dgvActivos.ColumnHeadersHeight = 29;
            this.dgvActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActivos.Location = new System.Drawing.Point(0, 0);
            this.dgvActivos.MultiSelect = false;
            this.dgvActivos.Name = "dgvActivos";
            this.dgvActivos.RowHeadersWidth = 51;
            this.dgvActivos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActivos.Size = new System.Drawing.Size(1168, 386);
            this.dgvActivos.TabIndex = 0;
            this.dgvActivos.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            // 
            // tabPageInactivos
            // 
            this.tabPageInactivos.Controls.Add(this.dgvInactivos);
            this.tabPageInactivos.Location = new System.Drawing.Point(4, 30);
            this.tabPageInactivos.Name = "tabPageInactivos";
            this.tabPageInactivos.Size = new System.Drawing.Size(1168, 386);
            this.tabPageInactivos.TabIndex = 1;
            this.tabPageInactivos.Text = "Inactivos";
            this.tabPageInactivos.UseVisualStyleBackColor = true;
            // 
            // dgvInactivos
            // 
            this.dgvInactivos.AutoGenerateColumns = false; 
            this.dgvInactivos.ColumnHeadersHeight = 29;
            this.dgvInactivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInactivos.Location = new System.Drawing.Point(0, 0);
            this.dgvInactivos.MultiSelect = false;
            this.dgvInactivos.Name = "dgvInactivos";
            this.dgvInactivos.RowHeadersWidth = 51;
            this.dgvInactivos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInactivos.Size = new System.Drawing.Size(1168, 386);
            this.dgvInactivos.TabIndex = 0;
            this.dgvInactivos.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnVolver);
            this.panelBotones.Controls.Add(this.btnNuevo);
            this.panelBotones.Controls.Add(this.btnEliminar);
            this.panelBotones.Controls.Add(this.btnEditar);
            this.panelBotones.Controls.Add(this.btnAsignarProductos);
            this.panelBotones.Controls.Add(this.btnVerProductos);
            this.panelBotones.Controls.Add(this.btnReactivar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 465);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1200, 65);
            this.panelBotones.TabIndex = 3;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(12, 17);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(115, 30);
            this.btnVolver.TabIndex = 0;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(148, 17);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(115, 30);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(419, 17);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(139, 30);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Dar de Baja";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(284, 17);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(115, 30);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnAsignarProductos
            // 
            this.btnAsignarProductos.Location = new System.Drawing.Point(715, 17);
            this.btnAsignarProductos.Name = "btnAsignarProductos";
            this.btnAsignarProductos.Size = new System.Drawing.Size(207, 30);
            this.btnAsignarProductos.TabIndex = 4;
            this.btnAsignarProductos.Text = "Asignar Productos";
            this.btnAsignarProductos.UseVisualStyleBackColor = true;
            this.btnAsignarProductos.Click += new System.EventHandler(this.btnAsignarProductos_Click);
            // 
            // btnVerProductos
            // 
            this.btnVerProductos.Location = new System.Drawing.Point(1034, 17);
            this.btnVerProductos.Name = "btnVerProductos";
            this.btnVerProductos.Size = new System.Drawing.Size(150, 30);
            this.btnVerProductos.TabIndex = 6;
            this.btnVerProductos.Text = "Ver Productos";
            this.btnVerProductos.UseVisualStyleBackColor = true;
            this.btnVerProductos.Click += new System.EventHandler(this.btnVerProductos_Click);
            // 
            // btnReactivar
            // 
            this.btnReactivar.Location = new System.Drawing.Point(578, 17);
            this.btnReactivar.Name = "btnReactivar";
            this.btnReactivar.Size = new System.Drawing.Size(115, 30);
            this.btnReactivar.TabIndex = 5;
            this.btnReactivar.Text = "Reactivar";
            this.btnReactivar.UseVisualStyleBackColor = true;
            this.btnReactivar.Click += new System.EventHandler(this.btnReactivar_Click);
            // 
            // ProveedorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.ClientSize = new System.Drawing.Size(1200, 530);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.tabControlProveedores);
            this.Controls.Add(this.panelBotones);
            this.Name = "ProveedorForm";
            this.Text = "Gestión de Proveedores";
            this.Load += new System.EventHandler(this.ProveedorForm_Load);
            this.tabControlProveedores.ResumeLayout(false);
            this.tabPageActivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivos)).EndInit();
            this.tabPageInactivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInactivos)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
