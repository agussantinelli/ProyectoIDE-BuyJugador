using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinForms
{
    partial class ProductoForm
    {
        private IContainer components = null;

        private TextBox txtBuscar;
        private Label lblBuscar;
        private ComboBox cmbFiltroStock;
        private TabControl tabControl;
        private TabPage tabActivos;
        private TabPage tabInactivos;
        private DataGridView dgvActivos;
        private DataGridView dgvInactivos;

        private Button btnNuevo;
        private Button btnEditar;
        private Button btnDarBaja;
        private Button btnReactivar;
        private Button btnVolver;
        private Button btnReportePrecios;
        private Button btnEditarPrecio;
        private Button btnVerProveedores;

        private ContextMenuStrip cmOpciones;
        private ToolStripMenuItem mnuVerHistorialPrecios;
        private ToolStripMenuItem mnuEditarPrecio;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            cmbFiltroStock = new ComboBox();
            tabControl = new TabControl();
            tabActivos = new TabPage();
            dgvActivos = new DataGridView();
            cmOpciones = new ContextMenuStrip(components);
            mnuVerHistorialPrecios = new ToolStripMenuItem();
            mnuEditarPrecio = new ToolStripMenuItem();
            tabInactivos = new TabPage();
            dgvInactivos = new DataGridView();
            btnNuevo = new Button();
            btnEditar = new Button();
            btnDarBaja = new Button();
            btnReactivar = new Button();
            btnVolver = new Button();
            btnReportePrecios = new Button();
            btnEditarPrecio = new Button();
            btnVerProveedores = new Button();
            tabControl.SuspendLayout();
            tabActivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvActivos).BeginInit();
            cmOpciones.SuspendLayout();
            tabInactivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).BeginInit();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new System.Drawing.Point(109, 15);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new System.Drawing.Size(400, 25);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new System.Drawing.Point(20, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new System.Drawing.Size(62, 20);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // cmbFiltroStock
            // 
            cmbFiltroStock.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroStock.Items.AddRange(new object[] { "Todos", "Con stock", "Sin stock", "Stock < 10" });
            cmbFiltroStock.Location = new System.Drawing.Point(538, 12);
            cmbFiltroStock.Name = "cmbFiltroStock";
            cmbFiltroStock.Size = new System.Drawing.Size(160, 28);
            cmbFiltroStock.TabIndex = 3;
            cmbFiltroStock.SelectedIndexChanged += cmbFiltroStock_SelectedIndexChanged;
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(tabActivos);
            tabControl.Controls.Add(tabInactivos);
            tabControl.Location = new System.Drawing.Point(20, 50);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(1150, 420);
            tabControl.TabIndex = 4;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabActivos
            // 
            tabActivos.Controls.Add(dgvActivos);
            tabActivos.Location = new System.Drawing.Point(4, 29);
            tabActivos.Name = "tabActivos";
            tabActivos.Padding = new Padding(3);
            tabActivos.Size = new System.Drawing.Size(1142, 387);
            tabActivos.TabIndex = 0;
            tabActivos.Text = "Activos";
            tabActivos.UseVisualStyleBackColor = true;
            // 
            // dgvActivos
            // 
            dgvActivos.AllowUserToAddRows = false;
            dgvActivos.AllowUserToDeleteRows = false;
            dgvActivos.ContextMenuStrip = cmOpciones;
            dgvActivos.Dock = DockStyle.Fill;
            dgvActivos.Location = new System.Drawing.Point(3, 3);
            dgvActivos.MultiSelect = false;
            dgvActivos.Name = "dgvActivos";
            dgvActivos.ReadOnly = true;
            dgvActivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActivos.Size = new System.Drawing.Size(1136, 381);
            dgvActivos.TabIndex = 0;
            dgvActivos.CellMouseDown += dgvProductos_CellMouseDown;
            dgvActivos.SelectionChanged += dgvProductos_SelectionChanged;
            dgvActivos.CellDoubleClick += dgvProductos_CellDoubleClick;
            // 
            // cmOpciones
            // 
            cmOpciones.Items.AddRange(new ToolStripItem[] { mnuVerHistorialPrecios, mnuEditarPrecio });
            cmOpciones.Name = "cmOpciones";
            cmOpciones.Size = new System.Drawing.Size(286, 48);
            cmOpciones.Opening += cmOpciones_Opening;
            // 
            // mnuVerHistorialPrecios
            // 
            mnuVerHistorialPrecios.Name = "mnuVerHistorialPrecios";
            mnuVerHistorialPrecios.Size = new System.Drawing.Size(285, 22);
            mnuVerHistorialPrecios.Text = "Ver historial de precios del producto";
            mnuVerHistorialPrecios.Click += mnuVerHistorialPrecios_Click;
            // 
            // mnuEditarPrecio
            // 
            mnuEditarPrecio.Name = "mnuEditarPrecio";
            mnuEditarPrecio.Size = new System.Drawing.Size(285, 22);
            mnuEditarPrecio.Text = "Editar precio de producto";
            mnuEditarPrecio.Click += mnuEditarPrecio_Click;
            // 
            // tabInactivos
            // 
            tabInactivos.Controls.Add(dgvInactivos);
            tabInactivos.Location = new System.Drawing.Point(4, 29);
            tabInactivos.Name = "tabInactivos";
            tabInactivos.Padding = new Padding(3);
            tabInactivos.Size = new System.Drawing.Size(1142, 387);
            tabInactivos.TabIndex = 1;
            tabInactivos.Text = "Inactivos";
            tabInactivos.UseVisualStyleBackColor = true;
            // 
            // dgvInactivos
            // 
            dgvInactivos.AllowUserToAddRows = false;
            dgvInactivos.AllowUserToDeleteRows = false;
            dgvInactivos.ContextMenuStrip = cmOpciones;
            dgvInactivos.Dock = DockStyle.Fill;
            dgvInactivos.Location = new System.Drawing.Point(3, 3);
            dgvInactivos.MultiSelect = false;
            dgvInactivos.Name = "dgvInactivos";
            dgvInactivos.ReadOnly = true;
            dgvInactivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInactivos.Size = new System.Drawing.Size(1136, 381);
            dgvInactivos.TabIndex = 0;
            dgvInactivos.CellMouseDown += dgvProductos_CellMouseDown;
            dgvInactivos.SelectionChanged += dgvProductos_SelectionChanged;
            dgvInactivos.CellDoubleClick += dgvProductos_CellDoubleClick;
            // 
            // btnNuevo
            // 
            btnNuevo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNuevo.Location = new System.Drawing.Point(140, 480);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new System.Drawing.Size(100, 30);
            btnNuevo.TabIndex = 5;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEditar
            // 
            btnEditar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditar.Location = new System.Drawing.Point(260, 480);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new System.Drawing.Size(100, 30);
            btnEditar.TabIndex = 6;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnDarBaja
            // 
            btnDarBaja.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDarBaja.Location = new System.Drawing.Point(380, 480);
            btnDarBaja.Name = "btnDarBaja";
            btnDarBaja.Size = new System.Drawing.Size(100, 30);
            btnDarBaja.TabIndex = 7;
            btnDarBaja.Text = "Dar de Baja";
            btnDarBaja.UseVisualStyleBackColor = true;
            btnDarBaja.Click += btnDarBaja_Click;
            // 
            // btnReactivar
            // 
            btnReactivar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReactivar.Location = new System.Drawing.Point(500, 480);
            btnReactivar.Name = "btnReactivar";
            btnReactivar.Size = new System.Drawing.Size(100, 30);
            btnReactivar.TabIndex = 8;
            btnReactivar.Text = "Reactivar";
            btnReactivar.UseVisualStyleBackColor = true;
            btnReactivar.Click += btnReactivar_Click;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnVolver.Location = new System.Drawing.Point(20, 480);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new System.Drawing.Size(100, 30);
            btnVolver.TabIndex = 11;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnReportePrecios
            // 
            btnReportePrecios.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReportePrecios.Location = new System.Drawing.Point(620, 480);
            btnReportePrecios.Name = "btnReportePrecios";
            btnReportePrecios.Size = new System.Drawing.Size(150, 30);
            btnReportePrecios.TabIndex = 9;
            btnReportePrecios.Text = "Reporte de Precios";
            btnReportePrecios.UseVisualStyleBackColor = true;
            btnReportePrecios.Click += btnReportePrecios_Click;
            // 
            // btnEditarPrecio
            // 
            btnEditarPrecio.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditarPrecio.Location = new System.Drawing.Point(780, 480);
            btnEditarPrecio.Name = "btnEditarPrecio";
            btnEditarPrecio.Size = new System.Drawing.Size(130, 30);
            btnEditarPrecio.TabIndex = 10;
            btnEditarPrecio.Text = "Editar Precio";
            btnEditarPrecio.UseVisualStyleBackColor = true;
            btnEditarPrecio.Click += btnEditarPrecio_Click;
            // 
            // btnVerProveedores
            // 
            btnVerProveedores.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnVerProveedores.Location = new System.Drawing.Point(920, 480);
            btnVerProveedores.Name = "btnVerProveedores";
            btnVerProveedores.Size = new System.Drawing.Size(150, 30);
            btnVerProveedores.TabIndex = 12;
            btnVerProveedores.Text = "Ver Proveedores";
            btnVerProveedores.UseVisualStyleBackColor = true;
            btnVerProveedores.Click += btnVerProveedores_Click;
            // 
            // ProductoForm
            // 
            ClientSize = new System.Drawing.Size(1200, 530);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(cmbFiltroStock);
            Controls.Add(tabControl);
            Controls.Add(btnNuevo);
            Controls.Add(btnEditar);
            Controls.Add(btnDarBaja);
            Controls.Add(btnReactivar);
            Controls.Add(btnReportePrecios);
            Controls.Add(btnEditarPrecio);
            Controls.Add(btnVerProveedores);
            Controls.Add(btnVolver);
            Name = "ProductoForm";
            Text = "Gestión de Productos";
            Load += ProductoForm_Load;
            tabControl.ResumeLayout(false);
            tabActivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvActivos).EndInit();
            cmOpciones.ResumeLayout(false);
            tabInactivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
