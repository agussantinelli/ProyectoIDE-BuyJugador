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
            this.components = new System.ComponentModel.Container();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.tabControlPersonas = new System.Windows.Forms.TabControl();
            this.tabPageActivos = new System.Windows.Forms.TabPage();
            this.dgvActivos = new System.Windows.Forms.DataGridView();
            this.tabPageInactivos = new System.Windows.Forms.TabPage();
            this.dgvInactivos = new System.Windows.Forms.DataGridView();
            this.btnReactivar = new System.Windows.Forms.Button();
            this.tabControlPersonas.SuspendLayout();
            this.tabPageActivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivos)).BeginInit();
            this.tabPageInactivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInactivos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(20, 15);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(45, 15);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(80, 12);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(600, 23);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // tabControlPersonas
            // 
            this.tabControlPersonas.Controls.Add(this.tabPageActivos);
            this.tabControlPersonas.Controls.Add(this.tabPageInactivos);
            this.tabControlPersonas.Location = new System.Drawing.Point(20, 50);
            this.tabControlPersonas.Name = "tabControlPersonas";
            this.tabControlPersonas.SelectedIndex = 0;
            this.tabControlPersonas.Size = new System.Drawing.Size(1150, 420);
            this.tabControlPersonas.TabIndex = 2;
            this.tabControlPersonas.SelectedIndexChanged += new System.EventHandler(this.tabControlPersonas_SelectedIndexChanged);
            // 
            // tabPageActivos
            // 
            this.tabPageActivos.Controls.Add(this.dgvActivos);
            this.tabPageActivos.Location = new System.Drawing.Point(4, 24);
            this.tabPageActivos.Name = "tabPageActivos";
            this.tabPageActivos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageActivos.Size = new System.Drawing.Size(1142, 392);
            this.tabPageActivos.TabIndex = 0;
            this.tabPageActivos.Text = "Personal Activo";
            this.tabPageActivos.UseVisualStyleBackColor = true;
            // 
            // dgvActivos
            // 
            this.dgvActivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActivos.Location = new System.Drawing.Point(3, 3);
            this.dgvActivos.Name = "dgvActivos";
            this.dgvActivos.Size = new System.Drawing.Size(1136, 386);
            this.dgvActivos.TabIndex = 0;
            this.dgvActivos.SelectionChanged += new System.EventHandler(this.dgvActivos_SelectionChanged);
            // 
            // tabPageInactivos
            // 
            this.tabPageInactivos.Controls.Add(this.dgvInactivos);
            this.tabPageInactivos.Location = new System.Drawing.Point(4, 24);
            this.tabPageInactivos.Name = "tabPageInactivos";
            this.tabPageInactivos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInactivos.Size = new System.Drawing.Size(1142, 392);
            this.tabPageInactivos.TabIndex = 1;
            this.tabPageInactivos.Text = "Ex-Personal";
            this.tabPageInactivos.UseVisualStyleBackColor = true;
            // 
            // dgvInactivos
            // 
            this.dgvInactivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInactivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInactivos.Location = new System.Drawing.Point(3, 3);
            this.dgvInactivos.Name = "dgvInactivos";
            this.dgvInactivos.Size = new System.Drawing.Size(1136, 386);
            this.dgvInactivos.TabIndex = 0;
            this.dgvInactivos.SelectionChanged += new System.EventHandler(this.dgvInactivos_SelectionChanged);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(20, 480);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(100, 30);
            this.btnVolver.TabIndex = 3;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(140, 480);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(100, 30);
            this.btnNuevo.TabIndex = 4;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(260, 480);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 30);
            this.btnEditar.TabIndex = 5;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(380, 480);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 30);
            this.btnEliminar.TabIndex = 6;
            this.btnEliminar.Text = "Dar de Baja";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnReactivar
            // 
            this.btnReactivar.Location = new System.Drawing.Point(500, 480);
            this.btnReactivar.Name = "btnReactivar";
            this.btnReactivar.Size = new System.Drawing.Size(100, 30);
            this.btnReactivar.TabIndex = 7;
            this.btnReactivar.Text = "Reactivar";
            this.btnReactivar.UseVisualStyleBackColor = true;
            this.btnReactivar.Click += new System.EventHandler(this.btnReactivar_Click);
            // 
            // PersonaForm
            // 
            this.ClientSize = new System.Drawing.Size(1200, 530);
            this.Controls.Add(this.btnReactivar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.tabControlPersonas);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblBuscar);
            this.Name = "PersonaForm";
            this.Text = "Gestión de Personal";
            this.Load += new System.EventHandler(this.PersonaForm_Load);
            this.tabControlPersonas.ResumeLayout(false);
            this.tabPageActivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivos)).EndInit();
            this.tabPageInactivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInactivos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
