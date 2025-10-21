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
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            dgvHistorial = new DataGridView();
            btnCerrar = new Button();
            btnRefrescar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 11);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(140, 19);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Historial de precios";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(20, 45);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(62, 20);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(88, 42);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(600, 25);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // dgvHistorial
            // 
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.AllowUserToDeleteRows = false;
            dgvHistorial.Location = new Point(20, 79);
            dgvHistorial.MultiSelect = false;
            dgvHistorial.Name = "dgvHistorial";
            dgvHistorial.ReadOnly = true;
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.Size = new Size(760, 408);
            dgvHistorial.TabIndex = 3;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(20, 504);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(100, 34);
            btnCerrar.TabIndex = 4;
            btnCerrar.Text = "Cerrar";
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Location = new Point(140, 504);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(100, 34);
            btnRefrescar.TabIndex = 5;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // HistorialPreciosForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 555);
            Controls.Add(lblTitulo);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(dgvHistorial);
            Controls.Add(btnCerrar);
            Controls.Add(btnRefrescar);
            Name = "HistorialPreciosForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Historial de precios";
            Load += HistorialPreciosForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}