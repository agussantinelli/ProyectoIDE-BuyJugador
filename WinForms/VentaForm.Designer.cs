using System.Windows.Forms;
using System.Drawing;

namespace WinForms
{
    partial class VentaForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtBuscar;
        private Button btnVolver;
        private Label lblBuscar;
        private Label lblVentas;
        private Label lblDetalleVenta;
        private DataGridView dgvVentas;
        private DataGridView dgvLineasVenta;
        private SplitContainer splitContainer1;

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
            btnVolver = new Button();
            lblBuscar = new Label();
            lblVentas = new Label();
            lblDetalleVenta = new Label();
            dgvVentas = new DataGridView();
            dgvLineasVenta = new DataGridView();
            splitContainer1 = new SplitContainer();

            ((System.ComponentModel.ISupportInitialize)(dgvVentas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dgvLineasVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();

            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(12, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new System.Drawing.Size(112, 15);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Buscar por Vendedor:";
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            txtBuscar.Location = new Point(140, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new System.Drawing.Size(1048, 23);
            txtBuscar.TabIndex = 1;
            txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            splitContainer1.Location = new Point(12, 41);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lblVentas);
            splitContainer1.Panel1.Controls.Add(dgvVentas);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lblDetalleVenta);
            splitContainer1.Panel2.Controls.Add(dgvLineasVenta);
            splitContainer1.Size = new System.Drawing.Size(1176, 437);
            splitContainer1.SplitterDistance = 218;
            splitContainer1.TabIndex = 2;
            // 
            // lblVentas
            // 
            lblVentas.AutoSize = true;
            lblVentas.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblVentas.Location = new Point(0, 0);
            lblVentas.Name = "lblVentas";
            lblVentas.Size = new System.Drawing.Size(114, 17);
            lblVentas.TabIndex = 0;
            lblVentas.Text = "Listado de Ventas";
            // 
            // dgvVentas
            // 
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Location = new Point(0, 20);
            dgvVentas.Name = "dgvVentas";
            dgvVentas.RowTemplate.Height = 25;
            dgvVentas.Size = new System.Drawing.Size(1176, 195);
            dgvVentas.TabIndex = 1;
            dgvVentas.SelectionChanged += new System.EventHandler(this.dgvVentas_SelectionChanged);
            // 
            // lblDetalleVenta
            // 
            lblDetalleVenta.AutoSize = true;
            lblDetalleVenta.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblDetalleVenta.Location = new Point(0, 0);
            lblDetalleVenta.Name = "lblDetalleVenta";
            lblDetalleVenta.Size = new System.Drawing.Size(115, 17);
            lblDetalleVenta.TabIndex = 0;
            lblDetalleVenta.Text = "Detalle de la Venta";
            // 
            // dgvLineasVenta
            // 
            dgvLineasVenta.AllowUserToAddRows = false;
            dgvLineasVenta.AllowUserToDeleteRows = false;
            dgvLineasVenta.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            dgvLineasVenta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLineasVenta.Location = new Point(0, 20);
            dgvLineasVenta.Name = "dgvLineasVenta";
            dgvLineasVenta.RowTemplate.Height = 25;
            dgvLineasVenta.Size = new System.Drawing.Size(1176, 192);
            dgvLineasVenta.TabIndex = 1;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            btnVolver.Location = new Point(1088, 484);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new System.Drawing.Size(100, 30);
            btnVolver.TabIndex = 3;
            btnVolver.Text = "Volver";
            btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // VentaForm
            // 
            ClientSize = new Size(1200, 530);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(splitContainer1);
            Controls.Add(btnVolver);
            Name = "VentaForm";
            Text = "Gestión de Ventas";
            Load += new System.EventHandler(this.VentaForm_Load);

            ((System.ComponentModel.ISupportInitialize)(dgvVentas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dgvLineasVenta)).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

