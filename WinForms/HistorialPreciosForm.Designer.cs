using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    partial class HistorialPreciosForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private TextBox txtBuscar;
        private Label lblBuscar;
        private DataGridView dgvHistorial;
        private Button btnCerrar;
        private Button btnRefrescar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            dgvHistorial = new DataGridView();
            btnCerrar = new Button();
            btnRefrescar = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            SuspendLayout();

            // lblTitulo
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 10);
            lblTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitulo.Text = "Historial de precios";

            // lblBuscar
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(20, 40);
            lblBuscar.Text = "Buscar:";

            // txtBuscar
            txtBuscar.Location = new Point(80, 37);
            txtBuscar.Size = new Size(600, 23);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // dgvHistorial
            dgvHistorial.Location = new Point(20, 70);
            dgvHistorial.Size = new Size(760, 360);
            dgvHistorial.ReadOnly = true;
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.MultiSelect = false;
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.AllowUserToDeleteRows = false;
            dgvHistorial.AutoGenerateColumns = true;

            // btnCerrar
            btnCerrar.Location = new Point(20, 445);
            btnCerrar.Size = new Size(100, 30);
            btnCerrar.Text = "Cerrar";
            btnCerrar.Click += btnCerrar_Click;

            // btnRefrescar
            btnRefrescar.Location = new Point(140, 445);
            btnRefrescar.Size = new Size(100, 30);
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Click += btnRefrescar_Click;

            // HistorialPreciosForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 490);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(dgvHistorial);
            Controls.Add(btnCerrar);
            Controls.Add(btnRefrescar);
            Name = "HistorialPreciosForm";
            Text = "Historial de precios";
            StartPosition = FormStartPosition.CenterParent;
            Load += HistorialPreciosForm_Load;

            ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
