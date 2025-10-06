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
        private Panel panelBotones;
        private Button btnNuevo;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnReactivar;
        private Button btnVolver;
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
            panelBotones = new Panel();
            btnVolver = new Button();
            btnNuevo = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnAsignarProductos = new Button();
            btnReactivar = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvActivos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).BeginInit();
            tabControlProveedores.SuspendLayout();
            tabPageActivos.SuspendLayout();
            tabPageInactivos.SuspendLayout();
            panelBotones.SuspendLayout();
            SuspendLayout();

            // === Buscar ===
            lblBuscar.Text = "Buscar por Razón Social:";
            lblBuscar.Location = new Point(12, 15);
            lblBuscar.AutoSize = true;

            txtBuscar.Location = new Point(160, 12);
            txtBuscar.Size = new Size(300, 23);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // === TabControl ===
            tabControlProveedores.Controls.Add(tabPageActivos);
            tabControlProveedores.Controls.Add(tabPageInactivos);
            tabControlProveedores.Location = new Point(12, 50);
            tabControlProveedores.Size = new Size(1176, 420);
            tabControlProveedores.SelectedIndexChanged += tabControlProveedores_SelectedIndexChanged;

            // === Pestañas ===
            tabPageActivos.Text = "Activos";
            tabPageActivos.Controls.Add(dgvActivos);

            tabPageInactivos.Text = "Inactivos";
            tabPageInactivos.Controls.Add(dgvInactivos);

            // === DataGrids ===
            dgvActivos.Dock = DockStyle.Fill;
            dgvActivos.SelectionChanged += dgv_SelectionChanged;

            dgvInactivos.Dock = DockStyle.Fill;
            dgvInactivos.SelectionChanged += dgv_SelectionChanged;

            // === Panel de botones ===
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Height = 65;
            panelBotones.Controls.Add(btnVolver);
            panelBotones.Controls.Add(btnNuevo);
            panelBotones.Controls.Add(btnEliminar);
            panelBotones.Controls.Add(btnEditar);
            panelBotones.Controls.Add(btnAsignarProductos);
            panelBotones.Controls.Add(btnReactivar);

            // === Botones ===
            int baseY = 17;

            btnVolver.Text = "Volver";
            btnVolver.Location = new Point(12, baseY);
            btnVolver.Size = new Size(115, 30);
            btnVolver.Click += btnVolver_Click;

            btnNuevo.Text = "Nuevo";
            btnNuevo.Location = new Point(133, baseY);
            btnNuevo.Size = new Size(115, 30);
            btnNuevo.Click += btnNuevo_Click;

            btnEliminar.Text = "Dar de Baja";
            btnEliminar.Location = new Point(254, baseY);
            btnEliminar.Size = new Size(115, 30);
            btnEliminar.Click += btnEliminar_Click;

            btnEditar.Text = "Editar";
            btnEditar.Location = new Point(375, baseY);
            btnEditar.Size = new Size(115, 30);
            btnEditar.Click += btnEditar_Click;

            btnAsignarProductos.Text = "Asignar Productos";
            btnAsignarProductos.Location = new Point(496, baseY);
            btnAsignarProductos.Size = new Size(150, 30);
            btnAsignarProductos.Click += btnAsignarProductos_Click;

            btnReactivar.Text = "Reactivar";
            btnReactivar.Location = new Point(656, baseY);
            btnReactivar.Size = new Size(115, 30);
            btnReactivar.Click += btnReactivar_Click;
            btnReactivar.Visible = false;

            // === Form ===
            ClientSize = new Size(1200, 530);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(tabControlProveedores);
            Controls.Add(panelBotones);
            Name = "ProveedorForm";
            Text = "Gestión de Proveedores";
            StartPosition = FormStartPosition.CenterScreen;
            Load += ProveedorForm_Load;

            ((System.ComponentModel.ISupportInitialize)dgvActivos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).EndInit();
            tabControlProveedores.ResumeLayout(false);
            tabPageActivos.ResumeLayout(false);
            tabPageInactivos.ResumeLayout(false);
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}


