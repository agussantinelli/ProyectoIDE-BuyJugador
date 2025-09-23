using System.Windows.Forms;

namespace WinForms
{
    partial class PersonaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvPersonas;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblBuscar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtBuscar = new TextBox();
            dgvPersonas = new DataGridView();
            btnNuevo = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnVolver = new Button();
            lblBuscar = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvPersonas).BeginInit();
            SuspendLayout();

            // lblBuscar
            lblBuscar.Location = new Point(20, 15);
            lblBuscar.Text = "Buscar:";
            lblBuscar.AutoSize = true;

            // txtBuscar
            txtBuscar.Location = new Point(80, 12);
            txtBuscar.Size = new Size(600, 23);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // dgvPersonas
            dgvPersonas.Location = new Point(20, 50);
            dgvPersonas.Size = new Size(1040, 400);
            dgvPersonas.ReadOnly = true;
            dgvPersonas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPersonas.MultiSelect = false;
            dgvPersonas.SelectionChanged += dgvPersonas_SelectionChanged;

            // Botones
            btnVolver.Location = new Point(20, 470);
            btnVolver.Size = new Size(100, 30);
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;

            btnNuevo.Location = new Point(140, 470);
            btnNuevo.Size = new Size(100, 30);
            btnNuevo.Text = "Nuevo";
            btnNuevo.Click += btnNuevo_Click;

            btnEditar.Location = new Point(260, 470);
            btnEditar.Size = new Size(100, 30);
            btnEditar.Text = "Editar";
            btnEditar.Click += btnEditar_Click;

            btnEliminar.Location = new Point(380, 470);
            btnEliminar.Size = new Size(100, 30);
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;

            // Form
            ClientSize = new Size(1100, 520);
            StartPosition = FormStartPosition.CenterScreen; 
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(dgvPersonas);
            Controls.Add(btnNuevo);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(btnVolver);
            Name = "Persona";
            Text = "Gestión de Personas";
            Load += PersonaForm_Load;

            ((System.ComponentModel.ISupportInitialize)dgvPersonas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
