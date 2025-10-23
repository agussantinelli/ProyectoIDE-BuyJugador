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
            panelBotones.SuspendLayout();
            SuspendLayout();
            dgvDisponibles.AllowUserToAddRows = false;
            dgvDisponibles.AllowUserToDeleteRows = false;
            dgvDisponibles.BackgroundColor = Color.FromArgb(45, 55, 70);
            dgvDisponibles.BorderStyle = BorderStyle.None;
            dgvDisponibles.Location = new Point(53, 55);
            dgvDisponibles.MultiSelect = false;
            dgvDisponibles.Name = "dgvDisponibles";
            dgvDisponibles.ReadOnly = true;
            dgvDisponibles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDisponibles.Size = new Size(460, 520);
            dgvDisponibles.TabIndex = 2;
            dgvAsignados.AllowUserToAddRows = false;
            dgvAsignados.AllowUserToDeleteRows = false;
            dgvAsignados.BackgroundColor = Color.FromArgb(45, 55, 70);
            dgvAsignados.BorderStyle = BorderStyle.None;
            dgvAsignados.Location = new Point(663, 55);
            dgvAsignados.MultiSelect = false;
            dgvAsignados.Name = "dgvAsignados";
            dgvAsignados.ReadOnly = true;
            dgvAsignados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAsignados.Size = new Size(525, 520);
            dgvAsignados.TabIndex = 3;
            btnAsignar.BackColor = Color.FromArgb(0, 80, 200);
            btnAsignar.Cursor = Cursors.Hand;
            btnAsignar.FlatAppearance.BorderSize = 0;
            btnAsignar.FlatStyle = FlatStyle.Flat;
            btnAsignar.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnAsignar.ForeColor = Color.White;
            btnAsignar.Location = new Point(15, 20);
            btnAsignar.Name = "btnAsignar";
            btnAsignar.Size = new Size(70, 36);
            btnAsignar.TabIndex = 0;
            btnAsignar.Text = ">";
            btnAsignar.UseVisualStyleBackColor = false;
            btnAsignar.Click += btnAsignar_Click;
            btnQuitar.BackColor = Color.FromArgb(220, 53, 69);
            btnQuitar.Cursor = Cursors.Hand;
            btnQuitar.FlatAppearance.BorderSize = 0;
            btnQuitar.FlatStyle = FlatStyle.Flat;
            btnQuitar.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            btnQuitar.ForeColor = Color.White;
            btnQuitar.Location = new Point(15, 90);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(70, 36);
            btnQuitar.TabIndex = 1;
            btnQuitar.Text = "<";
            btnQuitar.UseVisualStyleBackColor = false;
            btnQuitar.Click += btnQuitar_Click;
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(1003, 600);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 40);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            lblDisponibles.AutoSize = true;
            lblDisponibles.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
            lblDisponibles.Location = new Point(53, 25);
            lblDisponibles.Name = "lblDisponibles";
            lblDisponibles.Size = new Size(168, 18);
            lblDisponibles.TabIndex = 0;
            lblDisponibles.Text = "Productos Disponibles";
            lblAsignados.AutoSize = true;
            lblAsignados.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
            lblAsignados.Location = new Point(663, 25);
            lblAsignados.Name = "lblAsignados";
            lblAsignados.Size = new Size(160, 18);
            lblAsignados.TabIndex = 1;
            lblAsignados.Text = "Productos Asignados";
            btnGuardar.BackColor = Color.FromArgb(40, 167, 69);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(873, 600);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 40);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            panelBotones.Controls.Add(btnAsignar);
            panelBotones.Controls.Add(btnQuitar);
            panelBotones.Location = new Point(533, 260);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(100, 150);
            panelBotones.TabIndex = 4;
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 66, 82);
            ClientSize = new Size(1200, 700);
            Controls.Add(lblDisponibles);
            Controls.Add(lblAsignados);
            Controls.Add(dgvDisponibles);
            Controls.Add(dgvAsignados);
            Controls.Add(panelBotones);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            Font = new Font("Century Gothic", 10F);
            Name = "AsignarProductosProveedorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Asignar Productos a Proveedor";
            Load += AsignarProductosProveedorForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDisponibles).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAsignados).EndInit();
            panelBotones.ResumeLayout(false);
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
