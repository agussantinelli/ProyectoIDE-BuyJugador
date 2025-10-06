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
            dgvActivos = new DataGridView();
            tabPageInactivos = new TabPage();
            dgvInactivos = new DataGridView();
            panelBotones = new Panel();
            btnVolver = new Button();
            btnNuevo = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnAsignarProductos = new Button();
            btnReactivar = new Button();
            tabControlProveedores.SuspendLayout();
            tabPageActivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvActivos).BeginInit();
            tabPageInactivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).BeginInit();
            panelBotones.SuspendLayout();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(169, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(300, 22);
            txtBuscar.TabIndex = 1;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(12, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(151, 17);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Buscar por Razón Social:";
            // 
            // tabControlProveedores
            // 
            tabControlProveedores.Controls.Add(tabPageActivos);
            tabControlProveedores.Controls.Add(tabPageInactivos);
            tabControlProveedores.Location = new Point(12, 50);
            tabControlProveedores.Name = "tabControlProveedores";
            tabControlProveedores.SelectedIndex = 0;
            tabControlProveedores.Size = new Size(1176, 420);
            tabControlProveedores.TabIndex = 2;
            tabControlProveedores.SelectedIndexChanged += tabControlProveedores_SelectedIndexChanged;
            // 
            // tabPageActivos
            // 
            tabPageActivos.Controls.Add(dgvActivos);
            tabPageActivos.Location = new Point(4, 26);
            tabPageActivos.Name = "tabPageActivos";
            tabPageActivos.Size = new Size(1168, 390);
            tabPageActivos.TabIndex = 0;
            tabPageActivos.Text = "Activos";
            // 
            // dgvActivos
            // 
            dgvActivos.Dock = DockStyle.Fill;
            dgvActivos.Location = new Point(0, 0);
            dgvActivos.Name = "dgvActivos";
            dgvActivos.Size = new Size(1168, 390);
            dgvActivos.TabIndex = 0;
            dgvActivos.SelectionChanged += dgv_SelectionChanged;
            // 
            // tabPageInactivos
            // 
            tabPageInactivos.Controls.Add(dgvInactivos);
            tabPageInactivos.Location = new Point(4, 26);
            tabPageInactivos.Name = "tabPageInactivos";
            tabPageInactivos.Size = new Size(1168, 390);
            tabPageInactivos.TabIndex = 1;
            tabPageInactivos.Text = "Inactivos";
            // 
            // dgvInactivos
            // 
            dgvInactivos.Dock = DockStyle.Fill;
            dgvInactivos.Location = new Point(0, 0);
            dgvInactivos.Name = "dgvInactivos";
            dgvInactivos.Size = new Size(1168, 390);
            dgvInactivos.TabIndex = 0;
            dgvInactivos.SelectionChanged += dgv_SelectionChanged;
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(btnVolver);
            panelBotones.Controls.Add(btnNuevo);
            panelBotones.Controls.Add(btnEliminar);
            panelBotones.Controls.Add(btnEditar);
            panelBotones.Controls.Add(btnAsignarProductos);
            panelBotones.Controls.Add(btnReactivar);
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Location = new Point(0, 465);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(1200, 65);
            panelBotones.TabIndex = 3;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(12, 17);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(115, 30);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(133, 17);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(115, 30);
            btnNuevo.TabIndex = 1;
            btnNuevo.Text = "Nuevo";
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(375, 17);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(115, 30);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Dar de Baja";
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(254, 17);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(115, 30);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar";
            btnEditar.Click += btnEditar_Click;
            // 
            // btnAsignarProductos
            // 
            btnAsignarProductos.Location = new Point(496, 17);
            btnAsignarProductos.Name = "btnAsignarProductos";
            btnAsignarProductos.Size = new Size(150, 30);
            btnAsignarProductos.TabIndex = 4;
            btnAsignarProductos.Text = "Asignar Productos";
            btnAsignarProductos.Click += btnAsignarProductos_Click;
            // 
            // btnReactivar
            // 
            btnReactivar.Location = new Point(656, 17);
            btnReactivar.Name = "btnReactivar";
            btnReactivar.Size = new Size(115, 30);
            btnReactivar.TabIndex = 5;
            btnReactivar.Text = "Reactivar";
            btnReactivar.Visible = false;
            btnReactivar.Click += btnReactivar_Click;
            // 
            // ProveedorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            ClientSize = new Size(1200, 530);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(tabControlProveedores);
            Controls.Add(panelBotones);
            Name = "ProveedorForm";
            Text = "Gestión de Proveedores";
            Load += ProveedorForm_Load;
            tabControlProveedores.ResumeLayout(false);
            tabPageActivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvActivos).EndInit();
            tabPageInactivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).EndInit();
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}


