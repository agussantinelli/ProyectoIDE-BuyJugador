using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    partial class ProductoForm
    {
        private System.ComponentModel.IContainer components = null;

        private TabControl tabControl;
        private TabPage tabActivos;
        private TabPage tabInactivos;
        private DataGridView dgvActivos;
        private DataGridView dgvInactivos;

        private TextBox txtBuscar;
        private Label lblBuscar;
        private ComboBox cmbFiltroStock;

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
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            tabControl = new TabControl();
            tabActivos = new TabPage("Activos");
            tabInactivos = new TabPage("Inactivos");
            dgvActivos = new DataGridView();
            dgvInactivos = new DataGridView();

            txtBuscar = new TextBox();
            lblBuscar = new Label();
            cmbFiltroStock = new ComboBox();

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

            SuspendLayout();

            // lblBuscar
            lblBuscar.Text = "Buscar:";
            lblBuscar.Location = new Point(20, 10);
            lblBuscar.AutoSize = true;

            // txtBuscar
            txtBuscar.Location = new Point(80, 7);
            txtBuscar.Size = new Size(450, 23);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // cmbFiltroStock
            cmbFiltroStock.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroStock.Items.AddRange(new object[] {
                "Todos",
                "Con stock",
                "Sin stock",
                "Stock < 10"
            });
            cmbFiltroStock.SelectedIndex = 0;
            cmbFiltroStock.Location = new Point(550, 7);
            cmbFiltroStock.Size = new Size(160, 23);
            cmbFiltroStock.SelectedIndexChanged += cmbFiltroStock_SelectedIndexChanged;

            // tabControl
            tabControl.Location = new Point(20, 40);
            tabControl.Size = new Size(1060, 380);
            tabControl.TabPages.Add(tabActivos);
            tabControl.TabPages.Add(tabInactivos);
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;

            // dgvActivos
            dgvActivos.Dock = DockStyle.Fill;
            dgvActivos.ReadOnly = true;
            dgvActivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActivos.MultiSelect = false;
            dgvActivos.AllowUserToAddRows = false;
            dgvActivos.AllowUserToDeleteRows = false;
            dgvActivos.SelectionChanged += dgvProductos_SelectionChanged;
            dgvActivos.CellMouseDown += dgvProductos_CellMouseDown;
            tabActivos.Controls.Add(dgvActivos);

            // dgvInactivos
            dgvInactivos.Dock = DockStyle.Fill;
            dgvInactivos.ReadOnly = true;
            dgvInactivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInactivos.MultiSelect = false;
            dgvInactivos.AllowUserToAddRows = false;
            dgvInactivos.AllowUserToDeleteRows = false;
            dgvInactivos.SelectionChanged += dgvProductos_SelectionChanged;
            dgvInactivos.CellMouseDown += dgvProductos_CellMouseDown;
            tabInactivos.Controls.Add(dgvInactivos);

            // Context menu
            cmOpciones.Items.AddRange(new ToolStripItem[] {
                mnuVerHistorialPrecios,
                mnuEditarPrecio
            });
            mnuVerHistorialPrecios.Text = "Ver historial de precios";
            mnuVerHistorialPrecios.Click += mnuVerHistorialPrecios_Click;

            mnuEditarPrecio.Text = "Editar precio";
            mnuEditarPrecio.Click += mnuEditarPrecio_Click;

            dgvActivos.ContextMenuStrip = cmOpciones;
            dgvInactivos.ContextMenuStrip = cmOpciones;

            // Botones
            btnNuevo.Text = "Nuevo";
            btnNuevo.Location = new Point(20, 430);
            btnNuevo.Size = new Size(100, 30);
            btnNuevo.Click += btnNuevo_Click;

            btnEditar.Text = "Editar";
            btnEditar.Location = new Point(130, 430);
            btnEditar.Size = new Size(100, 30);
            btnEditar.Click += btnEditar_Click;

            btnDarBaja.Text = "Dar de baja";
            btnDarBaja.Location = new Point(240, 430);
            btnDarBaja.Size = new Size(100, 30);
            btnDarBaja.Click += btnDarBaja_Click;

            btnReactivar.Text = "Reactivar";
            btnReactivar.Location = new Point(350, 430);
            btnReactivar.Size = new Size(100, 30);
            btnReactivar.Click += btnReactivar_Click;

            btnVerHistorialPrecios.Text = "Ver Historial Precios";
            btnVerHistorialPrecios.Location = new Point(470, 430);
            btnVerHistorialPrecios.Size = new Size(170, 30);
            btnVerHistorialPrecios.Click += btnVerHistorialPrecios_Click;

            btnEditarPrecio.Text = "Editar precio";
            btnEditarPrecio.Location = new Point(650, 430);
            btnEditarPrecio.Size = new Size(150, 30);
            btnEditarPrecio.Click += btnEditarPrecio_Click;

            btnVolver.Text = "Volver";
            btnVolver.Location = new Point(810, 430);
            btnVolver.Size = new Size(100, 30);
            btnVolver.Click += btnVolver_Click;

            // Form
            ClientSize = new Size(1100, 480);
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

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
