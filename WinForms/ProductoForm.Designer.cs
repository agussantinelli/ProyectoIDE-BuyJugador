using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    partial class ProductoForm
    {
        private System.ComponentModel.IContainer components = null;

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
        private Button btnVerHistorialPrecios;
        private Button btnEditarPrecio;

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
            components = new System.ComponentModel.Container();

            txtBuscar = new TextBox();
            lblBuscar = new Label();
            cmbFiltroStock = new ComboBox();
            tabControl = new TabControl();
            tabActivos = new TabPage();
            dgvActivos = new DataGridView();
            tabInactivos = new TabPage();
            dgvInactivos = new DataGridView();

            btnNuevo = new Button();
            btnEditar = new Button();
            btnDarBaja = new Button();
            btnReactivar = new Button();
            btnVolver = new Button();
            btnVerHistorialPrecios = new Button();
            btnEditarPrecio = new Button();

            cmOpciones = new ContextMenuStrip(components);
            mnuVerHistorialPrecios = new ToolStripMenuItem();
            mnuEditarPrecio = new ToolStripMenuItem();

            ((System.ComponentModel.ISupportInitialize)dgvActivos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).BeginInit();
            tabControl.SuspendLayout();
            tabActivos.SuspendLayout();
            tabInactivos.SuspendLayout();
            SuspendLayout();

            // --- Buscar ---
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(20, 15);
            lblBuscar.Text = "Buscar:";

            txtBuscar.Location = new Point(80, 12);
            txtBuscar.Size = new Size(400, 23);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // --- Filtro Stock ---
            cmbFiltroStock.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroStock.Items.AddRange(new object[]
            {
                "Todos",
                "Con stock",
                "Sin stock",
                "Stock < 10"
            });
            cmbFiltroStock.SelectedIndex = 0;
            cmbFiltroStock.Location = new Point(500, 12);
            cmbFiltroStock.Size = new Size(160, 23);
            cmbFiltroStock.SelectedIndexChanged += cmbFiltroStock_SelectedIndexChanged;

            // --- Tabs ---
            tabControl.Controls.Add(tabActivos);
            tabControl.Controls.Add(tabInactivos);
            tabControl.Location = new Point(20, 50);
            tabControl.Size = new Size(1150, 420);
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;

            // --- Activos ---
            tabActivos.Text = "Activos";
            tabActivos.Controls.Add(dgvActivos);

            dgvActivos.Dock = DockStyle.Fill;
            dgvActivos.ReadOnly = true;
            dgvActivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActivos.MultiSelect = false;
            dgvActivos.AllowUserToAddRows = false;
            dgvActivos.AllowUserToDeleteRows = false;
            dgvActivos.SelectionChanged += dgvProductos_SelectionChanged;
            dgvActivos.CellMouseDown += dgvProductos_CellMouseDown;

            // --- Inactivos ---
            tabInactivos.Text = "Inactivos";
            tabInactivos.Controls.Add(dgvInactivos);

            dgvInactivos.Dock = DockStyle.Fill;
            dgvInactivos.ReadOnly = true;
            dgvInactivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInactivos.MultiSelect = false;
            dgvInactivos.AllowUserToAddRows = false;
            dgvInactivos.AllowUserToDeleteRows = false;
            dgvInactivos.SelectionChanged += dgvProductos_SelectionChanged;
            dgvInactivos.CellMouseDown += dgvProductos_CellMouseDown;

            // --- Context Menu ---
            cmOpciones.Items.AddRange(new ToolStripItem[]
            {
                mnuVerHistorialPrecios,
                mnuEditarPrecio
            });

            mnuVerHistorialPrecios.Text = "Ver historial de precios";
            mnuVerHistorialPrecios.Click += mnuVerHistorialPrecios_Click;

            mnuEditarPrecio.Text = "Editar precio";
            mnuEditarPrecio.Click += mnuEditarPrecio_Click;

            dgvActivos.ContextMenuStrip = cmOpciones;
            dgvInactivos.ContextMenuStrip = cmOpciones;

            // --- Botones inferiores ---
            btnVolver.Location = new Point(20, 480);
            btnVolver.Size = new Size(100, 30);
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;

            btnNuevo.Location = new Point(140, 480);
            btnNuevo.Size = new Size(100, 30);
            btnNuevo.Text = "Nuevo";
            btnNuevo.Click += btnNuevo_Click;

            btnEditar.Location = new Point(260, 480);
            btnEditar.Size = new Size(100, 30);
            btnEditar.Text = "Editar";
            btnEditar.Click += btnEditar_Click;

            btnDarBaja.Location = new Point(380, 480);
            btnDarBaja.Size = new Size(100, 30);
            btnDarBaja.Text = "Dar de Baja";
            btnDarBaja.Click += btnDarBaja_Click;

            btnReactivar.Location = new Point(500, 480);
            btnReactivar.Size = new Size(100, 30);
            btnReactivar.Text = "Reactivar";
            btnReactivar.Click += btnReactivar_Click;

            btnVerHistorialPrecios.Location = new Point(620, 480);
            btnVerHistorialPrecios.Size = new Size(150, 30);
            btnVerHistorialPrecios.Text = "Ver Historial Precios";
            btnVerHistorialPrecios.Click += btnVerHistorialPrecios_Click;

            btnEditarPrecio.Location = new Point(780, 480);
            btnEditarPrecio.Size = new Size(130, 30);
            btnEditarPrecio.Text = "Editar Precio";
            btnEditarPrecio.Click += btnEditarPrecio_Click;

            // --- Form ---
            ClientSize = new Size(1200, 530);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(cmbFiltroStock);
            Controls.Add(tabControl);
            Controls.Add(btnNuevo);
            Controls.Add(btnEditar);
            Controls.Add(btnDarBaja);
            Controls.Add(btnReactivar);
            Controls.Add(btnVerHistorialPrecios);
            Controls.Add(btnEditarPrecio);
            Controls.Add(btnVolver);
            Name = "ProductoForm";
            Text = "Gestión de Productos";
            Load += ProductoForm_Load;

            ((System.ComponentModel.ISupportInitialize)dgvActivos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).EndInit();
            tabControl.ResumeLayout(false);
            tabActivos.ResumeLayout(false);
            tabInactivos.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
