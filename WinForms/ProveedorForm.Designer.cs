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
            btnVolver = new Button(); // Estandarizado

            ((System.ComponentModel.ISupportInitialize)dgvActivos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).BeginInit();
            tabControlProveedores.SuspendLayout();
            tabPageActivos.SuspendLayout();
            tabPageInactivos.SuspendLayout();
            SuspendLayout();

            // lblBuscar
            lblBuscar.Text = "Buscar por Razón Social o CUIT:";
            lblBuscar.Location = new Point(20, 15);
            lblBuscar.AutoSize = true;

            // txtBuscar
            txtBuscar.Location = new Point(220, 12);
            txtBuscar.Size = new Size(400, 23);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // tabControl
            tabControlProveedores.Location = new Point(20, 50);
            tabControlProveedores.Size = new Size(1160, 420);
            tabControlProveedores.Controls.Add(tabPageActivos);
            tabControlProveedores.Controls.Add(tabPageInactivos);
            tabControlProveedores.SelectedIndexChanged += tabControlProveedores_SelectedIndexChanged;

            // tabActivos
            tabPageActivos.Text = "Activos";
            tabPageActivos.Padding = new Padding(3);
            tabPageActivos.Controls.Add(dgvActivos);

            // tabInactivos
            tabPageInactivos.Text = "Inactivos";
            tabPageInactivos.Padding = new Padding(3);
            tabPageInactivos.Controls.Add(dgvInactivos);

            // dgv (ambos)
            dgvActivos.Dock = DockStyle.Fill;
            dgvActivos.SelectionChanged += dgvActivos_SelectionChanged;

            dgvInactivos.Dock = DockStyle.Fill;
            dgvInactivos.SelectionChanged += dgvInactivos_SelectionChanged;

            // Botones
            btnVolver.Text = "Volver"; // Estandarizado
            btnVolver.Location = new Point(20, 480);
            btnVolver.Size = new Size(100, 30);
            btnVolver.Click += btnVolver_Click;

            btnNuevo.Text = "Nuevo";
            btnNuevo.Location = new Point(140, 480);
            btnNuevo.Size = new Size(100, 30);
            btnNuevo.Click += btnNuevo_Click;

            btnEditar.Text = "Editar";
            btnEditar.Location = new Point(260, 480);
            btnEditar.Size = new Size(100, 30);
            btnEditar.Click += btnEditar_Click;

            btnEliminar.Text = "Dar de Baja";
            btnEliminar.Location = new Point(380, 480);
            btnEliminar.Size = new Size(100, 30);
            btnEliminar.Click += btnEliminar_Click;

            btnReactivar.Text = "Reactivar";
            btnReactivar.Location = new Point(500, 480);
            btnReactivar.Size = new Size(100, 30);
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
