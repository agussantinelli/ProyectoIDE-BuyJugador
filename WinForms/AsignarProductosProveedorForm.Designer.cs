namespace WinForms
{
    partial class AsignarProductosProveedorForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvDisponibles = new DataGridView();
            dgvAsignados = new DataGridView();
            btnAsignar = new Button();
            btnQuitar = new Button();
            btnCancelar = new Button();
            lblDisponibles = new Label();
            lblAsignados = new Label();
            btnGuardar = new Button();
            panelBotones = new Panel();

            ((System.ComponentModel.ISupportInitialize)dgvDisponibles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAsignados).BeginInit();
            SuspendLayout();

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 66, 82);
            ClientSize = new Size(1100, 650);
            Font = new Font("Century Gothic", 10F, FontStyle.Regular, GraphicsUnit.Point);
            StartPosition = FormStartPosition.CenterScreen;
            ForeColor = Color.White;
            Text = "Asignar Productos a Proveedor";

            lblDisponibles.AutoSize = true;
            lblDisponibles.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
            lblDisponibles.Location = new Point(60, 25);
            lblDisponibles.Text = "Productos Disponibles";

            lblAsignados.AutoSize = true;
            lblAsignados.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
            lblAsignados.Location = new Point(620, 25);
            lblAsignados.Text = "Productos Asignados";

            dgvDisponibles.AllowUserToAddRows = false;
            dgvDisponibles.AllowUserToDeleteRows = false;
            dgvDisponibles.ReadOnly = true;
            dgvDisponibles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDisponibles.MultiSelect = false;
            dgvDisponibles.Location = new Point(60, 55);
            dgvDisponibles.Size = new Size(420, 480);
            dgvDisponibles.BackgroundColor = Color.FromArgb(45, 55, 70);
            dgvDisponibles.BorderStyle = BorderStyle.None;

            dgvAsignados.AllowUserToAddRows = false;
            dgvAsignados.AllowUserToDeleteRows = false;
            dgvAsignados.ReadOnly = true;
            dgvAsignados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAsignados.MultiSelect = false;
            dgvAsignados.Location = new Point(620, 55);
            dgvAsignados.Size = new Size(420, 480);
            dgvAsignados.BackgroundColor = Color.FromArgb(45, 55, 70);
            dgvAsignados.BorderStyle = BorderStyle.None;

            panelBotones.Location = new Point(500, 230);
            panelBotones.Size = new Size(100, 140);

            btnAsignar.Text = ">";
            btnAsignar.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnAsignar.Size = new Size(70, 34);
            btnAsignar.Location = new Point(15, 20);
            btnAsignar.BackColor = Color.FromArgb(0, 80, 200);
            btnAsignar.ForeColor = Color.White;
            btnAsignar.FlatStyle = FlatStyle.Flat;
            btnAsignar.FlatAppearance.BorderSize = 0;
            btnAsignar.Cursor = Cursors.Hand;
            btnAsignar.Click += btnAsignar_Click;

            btnQuitar.Text = "<";
            btnQuitar.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnQuitar.Size = new Size(70, 34);
            btnQuitar.Location = new Point(15, 80);
            btnQuitar.BackColor = Color.FromArgb(220, 53, 69);
            btnQuitar.ForeColor = Color.White;
            btnQuitar.FlatStyle = FlatStyle.Flat;
            btnQuitar.FlatAppearance.BorderSize = 0;
            btnQuitar.Cursor = Cursors.Hand;
            btnQuitar.Click += btnQuitar_Click;

            panelBotones.Controls.Add(btnAsignar);
            panelBotones.Controls.Add(btnQuitar);

            btnGuardar.Text = "Guardar";
            btnGuardar.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnGuardar.Size = new Size(110, 38);
            btnGuardar.Location = new Point(820, 560);
            btnGuardar.BackColor = Color.FromArgb(40, 167, 69);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnCancelar.Size = new Size(110, 38);
            btnCancelar.Location = new Point(940, 560);
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Click += btnCancelar_Click;

            Controls.Add(lblDisponibles);
            Controls.Add(lblAsignados);
            Controls.Add(dgvDisponibles);
            Controls.Add(dgvAsignados);
            Controls.Add(panelBotones);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);

            Load += AsignarProductosProveedorForm_Load;

            ((System.ComponentModel.ISupportInitialize)dgvDisponibles).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAsignados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDisponibles;
        private DataGridView dgvAsignados;
        private Button btnAsignar;
        private Button btnQuitar;
        private Button btnCancelar;
        private Button btnGuardar;
        private Label lblDisponibles;
        private Label lblAsignados;
        private Panel panelBotones;
    }
}
