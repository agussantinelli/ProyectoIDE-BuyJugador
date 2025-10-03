using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    partial class ProveedorForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtBuscar;
        private Label lblBuscar;
        private TabControl tabControlProveedores;
        private TabPage tabPageActivos;
        private TabPage tabPageInactivos;
        private DataGridView dgvActivos;
        private DataGridView dgvInactivos;
        private Button btnNuevo;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnReactivar;
        private Button btnVolver;
        // --- BOTÓN AÑADIDO ---
        private Button btnAsignarProductos;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            tabControlProveedores = new TabControl();
            tabPageActivos = new TabPage();
            tabPageInactivos = new TabPage();
            dgvActivos = new DataGridView();
            dgvInactivos = new DataGridView();
            btnNuevo = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnReactivar = new Button();
            btnVolver = new Button();
            // --- BOTÓN AÑADIDO ---
            btnAsignarProductos = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvActivos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).BeginInit();
            tabControlProveedores.SuspendLayout();
            tabPageActivos.SuspendLayout();
            tabPageInactivos.SuspendLayout();
            SuspendLayout();

            // Controles de Búsqueda
            lblBuscar.Text = "Buscar por Razón Social:";
            lblBuscar.Location = new Point(12, 15);
            lblBuscar.AutoSize = true;
            txtBuscar.Location = new Point(160, 12);
            txtBuscar.Size = new Size(300, 23);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // TabControl
            tabControlProveedores.Controls.Add(tabPageActivos);
            tabControlProveedores.Controls.Add(tabPageInactivos);
            tabControlProveedores.Location = new Point(12, 50);
            tabControlProveedores.Size = new Size(1176, 420);
            tabControlProveedores.SelectedIndexChanged += tabControlProveedores_SelectedIndexChanged;

            // Pestañas
            tabPageActivos.Text = "Activos";
            tabPageActivos.Controls.Add(dgvActivos);
            tabPageInactivos.Text = "Inactivos";
            tabPageInactivos.Controls.Add(dgvInactivos);

            // DataGridViews
            dgvActivos.Dock = DockStyle.Fill;
            dgvActivos.SelectionChanged += dgv_SelectionChanged;
            dgvInactivos.Dock = DockStyle.Fill;
            dgvInactivos.SelectionChanged += dgv_SelectionChanged;

            // Botones
            btnVolver.Text = "Volver";
            btnVolver.Location = new Point(12, 480);
            btnVolver.Size = new Size(100, 30);
            btnVolver.Click += btnVolver_Click;

            btnNuevo.Text = "Nuevo";
            btnNuevo.Location = new Point(1088, 480);
            btnNuevo.Size = new Size(100, 30);
            btnNuevo.Click += btnNuevo_Click;

            btnEditar.Text = "Editar";
            btnEditar.Location = new Point(982, 480);
            btnEditar.Size = new Size(100, 30);
            btnEditar.Click += btnEditar_Click;

            btnEliminar.Text = "Dar de Baja";
            btnEliminar.Location = new Point(876, 480);
            btnEliminar.Size = new Size(100, 30);
            btnEliminar.Click += btnEliminar_Click;

            // --- BOTÓN AÑADIDO ---
            btnAsignarProductos.Text = "Asignar Productos";
            btnAsignarProductos.Location = new Point(720, 480);
            btnAsignarProductos.Size = new Size(150, 30);
            btnAsignarProductos.Click += btnAsignarProductos_Click;

            btnReactivar.Text = "Reactivar";
            btnReactivar.Location = new Point(982, 480);
            btnReactivar.Size = new Size(100, 30);
            btnReactivar.Visible = false;
            btnReactivar.Click += btnReactivar_Click;

            // Form
            ClientSize = new Size(1200, 530);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(tabControlProveedores);
            Controls.Add(btnNuevo);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(btnReactivar);
            Controls.Add(btnAsignarProductos);
            Controls.Add(btnVolver);
            Name = "ProveedorForm";
            Text = "Gestión de Proveedores";
            Load += ProveedorForm_Load;
            StartPosition = FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)dgvActivos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).EndInit();
            tabControlProveedores.ResumeLayout(false);
            tabPageActivos.ResumeLayout(false);
            tabPageInactivos.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
