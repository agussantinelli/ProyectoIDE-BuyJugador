using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    partial class VentasPersonaForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitulo;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Label lblEstado;
        private ComboBox cmbEstado;
        private DataGridView dgvVentas;
        private Panel panelBotones;
        private Button btnVerDetalle;
        private Button btnCerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            dgvVentas = new DataGridView();
            panelBotones = new Panel();
            btnVerDetalle = new Button();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            panelBotones.SuspendLayout();
            SuspendLayout();
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Location = new Point(12, 10);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(171, 21);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Ventas de <Persona>";
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(12, 50);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(153, 20);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar (ID / Fecha):";
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Location = new Point(171, 47);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Ej: 1024 / 12-09-2025";
            txtBuscar.Size = new Size(524, 25);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += FiltrosChanged;
            lblEstado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(701, 50);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(62, 20);
            lblEstado.TabIndex = 3;
            lblEstado.Text = "Estado:";
            cmbEstado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Items.AddRange(new object[] { "Todos", "Pendiente", "Finalizada" });
            cmbEstado.Location = new Point(766, 45);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(151, 28);
            cmbEstado.TabIndex = 4;
            cmbEstado.SelectedIndexChanged += FiltrosChanged;
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Location = new Point(12, 87);
            dgvVentas.MultiSelect = false;
            dgvVentas.Name = "dgvVentas";
            dgvVentas.ReadOnly = true;
            dgvVentas.RowHeadersVisible = false;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.Size = new Size(905, 415);
            dgvVentas.TabIndex = 5;
            dgvVentas.CellDoubleClick += DgvVentas_CellDoubleClick;
            dgvVentas.SelectionChanged += DgvVentas_SelectionChanged;
            panelBotones.Controls.Add(btnVerDetalle);
            panelBotones.Controls.Add(btnCerrar);
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Location = new Point(0, 515);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(929, 65);
            panelBotones.TabIndex = 6;
            btnVerDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerDetalle.Location = new Point(688, 14);
            btnVerDetalle.Name = "btnVerDetalle";
            btnVerDetalle.Size = new Size(115, 37);
            btnVerDetalle.TabIndex = 0;
            btnVerDetalle.Text = "Ver Detalle";
            btnVerDetalle.UseVisualStyleBackColor = true;
            btnVerDetalle.Click += BtnVerDetalle_Click;
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.Location = new Point(809, 14);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(108, 37);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += BtnCerrar_Click;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(929, 580);
            Controls.Add(dgvVentas);
            Controls.Add(panelBotones);
            Controls.Add(cmbEstado);
            Controls.Add(lblEstado);
            Controls.Add(txtBuscar);
            Controls.Add(lblBuscar);
            Controls.Add(lblTitulo);
            MinimumSize = new Size(740, 493);
            Name = "VentasPersonaForm";
            Text = "Ventas por Persona";
            Load += VentasPersonaForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

