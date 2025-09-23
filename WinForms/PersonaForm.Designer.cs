using System.Windows.Forms;

namespace WinForms
{
    partial class PersonaForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtBuscar;
        private Button btnNuevo;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnVolver;
        private Label lblBuscar;
        private TabControl tabControlPersonas;
        private TabPage tabPageActivos;
        private TabPage tabPageInactivos;
        private DataGridView dgvActivos;
        private DataGridView dgvInactivos;
        private Button btnReactivar;

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
            btnNuevo = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnVolver = new Button();
            lblBuscar = new Label();
            tabControlPersonas = new TabControl();
            tabPageActivos = new TabPage();
            dgvActivos = new DataGridView();
            tabPageInactivos = new TabPage();
            dgvInactivos = new DataGridView();
            btnReactivar = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvActivos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).BeginInit();
            tabControlPersonas.SuspendLayout();
            tabPageActivos.SuspendLayout();
            tabPageInactivos.SuspendLayout();
            SuspendLayout();

            // lblBuscar
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(20, 15);
            lblBuscar.Text = "Buscar:";

            // txtBuscar
            txtBuscar.Location = new Point(80, 12);
            txtBuscar.Size = new Size(600, 23);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // tabControlPersonas
            tabControlPersonas.Controls.Add(tabPageActivos);
            tabControlPersonas.Controls.Add(tabPageInactivos);
            tabControlPersonas.Location = new Point(20, 50);
            tabControlPersonas.Size = new Size(1150, 420);
            tabControlPersonas.SelectedIndexChanged += tabControlPersonas_SelectedIndexChanged;

            // tabPageActivos
            tabPageActivos.Controls.Add(dgvActivos);
            tabPageActivos.Location = new Point(4, 24);
            tabPageActivos.Padding = new Padding(3);
            tabPageActivos.Size = new Size(1142, 392);
            tabPageActivos.Text = "Personal Activo";
            tabPageActivos.UseVisualStyleBackColor = true;

            // dgvActivos
            dgvActivos.Dock = DockStyle.Fill;
            dgvActivos.ReadOnly = true;
            dgvActivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActivos.MultiSelect = false;
            dgvActivos.SelectionChanged += dgvActivos_SelectionChanged;

            // tabPageInactivos
            tabPageInactivos.Controls.Add(dgvInactivos);
            tabPageInactivos.Location = new Point(4, 24);
            tabPageInactivos.Padding = new Padding(3);
            tabPageInactivos.Size = new Size(1142, 392);
            tabPageInactivos.Text = "Ex-Personal";
            tabPageInactivos.UseVisualStyleBackColor = true;

            // dgvInactivos
            dgvInactivos.Dock = DockStyle.Fill;
            dgvInactivos.ReadOnly = true;
            dgvInactivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInactivos.MultiSelect = false;
            dgvInactivos.SelectionChanged += dgvInactivos_SelectionChanged;

            // Botones
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

            btnEliminar.Location = new Point(380, 480);
            btnEliminar.Size = new Size(100, 30);
            btnEliminar.Text = "Dar de Baja";
            btnEliminar.Click += btnEliminar_Click;

            btnReactivar.Location = new Point(500, 480);
            btnReactivar.Size = new Size(100, 30);
            btnReactivar.Text = "Reactivar";
            btnReactivar.Click += btnReactivar_Click;

            // Form
            ClientSize = new Size(1200, 530);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(tabControlPersonas);
            Controls.Add(btnNuevo);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(btnReactivar);
            Controls.Add(btnVolver);
            Name = "PersonaForm";
            Text = "Gestión de Personal";
            Load += PersonaForm_Load;

            ((System.ComponentModel.ISupportInitialize)dgvActivos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).EndInit();
            tabControlPersonas.ResumeLayout(false);
            tabPageActivos.ResumeLayout(false);
            tabPageInactivos.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

