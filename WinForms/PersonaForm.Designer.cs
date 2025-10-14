using System.Windows.Forms;
using System.Drawing;

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
            tabControlPersonas.SuspendLayout();
            tabPageActivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvActivos).BeginInit();
            tabPageInactivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).BeginInit();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(80, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(600, 25);
            txtBuscar.TabIndex = 1;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(140, 480);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(100, 30);
            btnNuevo.TabIndex = 4;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(260, 480);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(100, 30);
            btnEditar.TabIndex = 5;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(380, 480);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(100, 30);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Dar de Baja";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(20, 480);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 30);
            btnVolver.TabIndex = 3;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(20, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(62, 20);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Buscar:";
            // 
            // tabControlPersonas
            // 
            tabControlPersonas.Controls.Add(tabPageActivos);
            tabControlPersonas.Controls.Add(tabPageInactivos);
            tabControlPersonas.Location = new Point(20, 50);
            tabControlPersonas.Name = "tabControlPersonas";
            tabControlPersonas.SelectedIndex = 0;
            tabControlPersonas.Size = new Size(1150, 420);
            tabControlPersonas.TabIndex = 2;
            tabControlPersonas.SelectedIndexChanged += tabControlPersonas_SelectedIndexChanged;
            // 
            // tabPageActivos
            // 
            tabPageActivos.Controls.Add(dgvActivos);
            tabPageActivos.Location = new Point(4, 29);
            tabPageActivos.Name = "tabPageActivos";
            tabPageActivos.Padding = new Padding(3);
            tabPageActivos.Size = new Size(1142, 387);
            tabPageActivos.TabIndex = 0;
            tabPageActivos.Text = "Personal Activo";
            tabPageActivos.UseVisualStyleBackColor = true;
            // 
            // dgvActivos
            // 
            dgvActivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvActivos.Dock = DockStyle.Fill;
            dgvActivos.Location = new Point(3, 3);
            dgvActivos.Name = "dgvActivos";
            dgvActivos.Size = new Size(1136, 381);
            dgvActivos.TabIndex = 0;
            dgvActivos.SelectionChanged += dgvActivos_SelectionChanged;
            // 
            // tabPageInactivos
            // 
            tabPageInactivos.Controls.Add(dgvInactivos);
            tabPageInactivos.Location = new Point(4, 29);
            tabPageInactivos.Name = "tabPageInactivos";
            tabPageInactivos.Padding = new Padding(3);
            tabPageInactivos.Size = new Size(1142, 387);
            tabPageInactivos.TabIndex = 1;
            tabPageInactivos.Text = "Ex-Personal";
            tabPageInactivos.UseVisualStyleBackColor = true;
            // 
            // dgvInactivos
            // 
            dgvInactivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInactivos.Dock = DockStyle.Fill;
            dgvInactivos.Location = new Point(3, 3);
            dgvInactivos.Name = "dgvInactivos";
            dgvInactivos.Size = new Size(1136, 381);
            dgvInactivos.TabIndex = 0;
            dgvInactivos.SelectionChanged += dgvInactivos_SelectionChanged;
            // 
            // btnReactivar
            // 
            btnReactivar.Location = new Point(500, 480);
            btnReactivar.Name = "btnReactivar";
            btnReactivar.Size = new Size(100, 30);
            btnReactivar.TabIndex = 7;
            btnReactivar.Text = "Reactivar";
            btnReactivar.UseVisualStyleBackColor = true;
            btnReactivar.Click += btnReactivar_Click;
            // 
            // PersonaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            ClientSize = new Size(1200, 530);
            Controls.Add(btnReactivar);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(btnNuevo);
            Controls.Add(btnVolver);
            Controls.Add(tabControlPersonas);
            Controls.Add(txtBuscar);
            Controls.Add(lblBuscar);
            Name = "PersonaForm";
            Text = "Gestión de Personal";
            Load += PersonaForm_Load;
            tabControlPersonas.ResumeLayout(false);
            tabPageActivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvActivos).EndInit();
            tabPageInactivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInactivos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
