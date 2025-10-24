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
        private Button btnEditarPrecio;
        private Button btnVerProveedores;
        private Button btnVerHistorial;

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
            btnEditarPrecio = new Button();
            btnVerProveedores = new Button();
            btnVerHistorial = new Button();
            tabControl.SuspendLayout();
            tabActivos.SuspendLayout();
            ((ISupportInitialize)dgvActivos).BeginInit();
            cmOpciones.SuspendLayout();
            tabInactivos.SuspendLayout();
            ((ISupportInitialize)dgvInactivos).BeginInit();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(109, 15);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(400, 30);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(20, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(76, 22);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // cmbFiltroStock
            // 
            cmbFiltroStock.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroStock.Items.AddRange(new object[] { "Todos", "Con stock", "Sin stock", "Stock < 10" });
            cmbFiltroStock.Location = new Point(538, 12);
            cmbFiltroStock.Name = "cmbFiltroStock";
            cmbFiltroStock.Size = new Size(160, 29);
            cmbFiltroStock.TabIndex = 3;
            cmbFiltroStock.SelectedIndexChanged += cmbFiltroStock_SelectedIndexChanged;
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(tabActivos);
            tabControl.Controls.Add(tabInactivos);
            tabControl.Location = new Point(20, 50);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1150, 420);
            tabControl.TabIndex = 4;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabActivos
            // 
            tabActivos.Controls.Add(dgvActivos);
            tabActivos.Location = new Point(4, 30);
            tabActivos.Name = "tabActivos";
            tabActivos.Padding = new Padding(3);
            tabActivos.Size = new Size(1142, 386);
            tabActivos.TabIndex = 0;
            tabActivos.Text = "Activos";
            tabActivos.UseVisualStyleBackColor = true;
            // 
            // dgvActivos
            // 
            dgvActivos.AllowUserToAddRows = false;
            dgvActivos.AllowUserToDeleteRows = false;
            dgvActivos.ColumnHeadersHeight = 29;
            dgvActivos.ContextMenuStrip = cmOpciones;
            dgvActivos.Dock = DockStyle.Fill;
            dgvActivos.Location = new Point(3, 3);
            dgvActivos.MultiSelect = false;
            dgvActivos.Name = "dgvActivos";
            dgvActivos.ReadOnly = true;
            dgvActivos.RowHeadersWidth = 51;
            dgvActivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActivos.Size = new Size(1136, 380);
            dgvActivos.TabIndex = 0;
            dgvActivos.CellDoubleClick += dgvProductos_CellDoubleClick;
            dgvActivos.CellMouseDown += dgvProductos_CellMouseDown;
            dgvActivos.SelectionChanged += dgvProductos_SelectionChanged;
            // 
            // cmOpciones
            // 
            cmOpciones.ImageScalingSize = new Size(20, 20);
            cmOpciones.Items.AddRange(new ToolStripItem[] { mnuVerHistorialPrecios, mnuEditarPrecio });
            cmOpciones.Name = "cmOpciones";
            cmOpciones.Size = new Size(320, 52);
            cmOpciones.Opening += cmOpciones_Opening;
            // 
            // mnuVerHistorialPrecios
            // 
            mnuVerHistorialPrecios.Name = "mnuVerHistorialPrecios";
            mnuVerHistorialPrecios.Size = new Size(319, 24);
            mnuVerHistorialPrecios.Text = "Ver historial de precios del producto";
            mnuVerHistorialPrecios.Click += mnuVerHistorialPrecios_Click;
            // 
            // mnuEditarPrecio
            // 
            mnuEditarPrecio.Name = "mnuEditarPrecio";
            mnuEditarPrecio.Size = new Size(319, 24);
            mnuEditarPrecio.Text = "Editar precio de producto";
            mnuEditarPrecio.Click += mnuEditarPrecio_Click;
            // 
            // tabInactivos
            // 
            tabInactivos.Controls.Add(dgvInactivos);
            tabInactivos.Location = new Point(4, 30);
            tabInactivos.Name = "tabInactivos";
            tabInactivos.Padding = new Padding(3);
            tabInactivos.Size = new Size(1142, 386);
            tabInactivos.TabIndex = 1;
            tabInactivos.Text = "Inactivos";
            tabInactivos.UseVisualStyleBackColor = true;
            // 
            // dgvInactivos
            // 
            dgvInactivos.AllowUserToAddRows = false;
            dgvInactivos.AllowUserToDeleteRows = false;
            dgvInactivos.ColumnHeadersHeight = 29;
            dgvInactivos.ContextMenuStrip = cmOpciones;
            dgvInactivos.Dock = DockStyle.Fill;
            dgvInactivos.Location = new Point(3, 3);
            dgvInactivos.MultiSelect = false;
            dgvInactivos.Name = "dgvInactivos";
            dgvInactivos.ReadOnly = true;
            dgvInactivos.RowHeadersWidth = 51;
            dgvInactivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInactivos.Size = new Size(1136, 380);
            dgvInactivos.TabIndex = 0;
            dgvInactivos.CellDoubleClick += dgvProductos_CellDoubleClick;
            dgvInactivos.CellMouseDown += dgvProductos_CellMouseDown;
            dgvInactivos.SelectionChanged += dgvProductos_SelectionChanged;
            // 
            // btnNuevo
            // 
            btnNuevo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNuevo.Location = new Point(140, 480);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(100, 30);
            btnNuevo.TabIndex = 5;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEditar
            // 
            btnEditar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditar.Location = new Point(260, 480);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(100, 30);
            btnEditar.TabIndex = 6;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnDarBaja
            // 
            btnDarBaja.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDarBaja.Location = new Point(380, 480);
            btnDarBaja.Name = "btnDarBaja";
            btnDarBaja.Size = new Size(139, 30);
            btnDarBaja.TabIndex = 7;
            btnDarBaja.Text = "Dar de Baja";
            btnDarBaja.UseVisualStyleBackColor = true;
            btnDarBaja.Click += btnDarBaja_Click;
            // 
            // btnReactivar
            // 
            btnReactivar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReactivar.Location = new Point(538, 480);
            btnReactivar.Name = "btnReactivar";
            btnReactivar.Size = new Size(100, 30);
            btnReactivar.TabIndex = 8;
            btnReactivar.Text = "Reactivar";
            btnReactivar.UseVisualStyleBackColor = true;
            btnReactivar.Click += btnReactivar_Click;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnVolver.Location = new Point(20, 480);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 30);
            btnVolver.TabIndex = 11;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnEditarPrecio
            // 
            btnEditarPrecio.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditarPrecio.Location = new Point(835, 480);
            btnEditarPrecio.Name = "btnEditarPrecio";
            btnEditarPrecio.Size = new Size(130, 30);
            btnEditarPrecio.TabIndex = 10;
            btnEditarPrecio.Text = "Editar Precio";
            btnEditarPrecio.UseVisualStyleBackColor = true;
            btnEditarPrecio.Click += btnEditarPrecio_Click;
            // 
            // btnVerProveedores
            // 
            btnVerProveedores.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnVerProveedores.Location = new Point(986, 480);
            btnVerProveedores.Name = "btnVerProveedores";
            btnVerProveedores.Size = new Size(184, 30);
            btnVerProveedores.TabIndex = 12;
            btnVerProveedores.Text = "Ver Proveedores";
            btnVerProveedores.UseVisualStyleBackColor = true;
            btnVerProveedores.Click += btnVerProveedores_Click;
            // 
            // btnVerHistorial
            // 
            btnVerHistorial.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnVerHistorial.Location = new Point(663, 480);
            btnVerHistorial.Name = "btnVerHistorial";
            btnVerHistorial.Size = new Size(150, 30);
            btnVerHistorial.TabIndex = 9;
            btnVerHistorial.Text = "Ver Historial Precios";
            btnVerHistorial.UseVisualStyleBackColor = true;
            btnVerHistorial.Click += btnVerHistorial_Click;
            // 
            // ProductoForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            ClientSize = new Size(1200, 530);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(cmbFiltroStock);
            Controls.Add(tabControl);
            Controls.Add(btnNuevo);
            Controls.Add(btnEditar);
            Controls.Add(btnDarBaja);
            Controls.Add(btnReactivar);
            Controls.Add(btnVerHistorial);
            Controls.Add(btnEditarPrecio);
            Controls.Add(btnVerProveedores);
            Controls.Add(btnVolver);
            Name = "ProductoForm";
            Text = "Gestión de Productos";
            Load += ProductoForm_Load;
            tabControl.ResumeLayout(false);
            tabActivos.ResumeLayout(false);
            ((ISupportInitialize)dgvActivos).EndInit();
            cmOpciones.ResumeLayout(false);
            tabInactivos.ResumeLayout(false);
            ((ISupportInitialize)dgvInactivos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
