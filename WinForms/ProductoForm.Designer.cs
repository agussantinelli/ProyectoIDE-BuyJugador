using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    partial class ProductoForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtBuscar;
        private DataGridView dgvProductos;
        private Button btnNuevo;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnVolver;
        private Button btnVerHistorialPrecios; 
        private Button btnEditarPrecio;       
        private Label lblBuscar;

        // Menú contextual (opcional)
        private ContextMenuStrip cmOpciones;
        private ToolStripMenuItem mnuVerHistorialPrecios;
        private ToolStripMenuItem mnuEditarPrecio;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            txtBuscar = new TextBox();
            dgvProductos = new DataGridView();
            btnNuevo = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnVolver = new Button();
            btnVerHistorialPrecios = new Button(); 
            btnEditarPrecio = new Button();      
            lblBuscar = new Label();

            cmOpciones = new ContextMenuStrip(components);
            mnuVerHistorialPrecios = new ToolStripMenuItem();
            mnuEditarPrecio = new ToolStripMenuItem();

            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();

            // lblBuscar
            lblBuscar.Location = new Point(20, 10);
            lblBuscar.Text = "Buscar:";
            lblBuscar.AutoSize = true;

            // txtBuscar
            txtBuscar.Location = new Point(80, 7);
            txtBuscar.Size = new Size(600, 23);
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // dgvProductos
            dgvProductos.Location = new Point(20, 40);
            dgvProductos.Size = new Size(1040, 400);
            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AutoGenerateColumns = true;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            dgvProductos.CellMouseDown += dgvProductos_CellMouseDown;

            // ContextMenuStrip (opcional)
            cmOpciones.Items.AddRange(new ToolStripItem[] {
                mnuVerHistorialPrecios,
                mnuEditarPrecio
            });
            mnuVerHistorialPrecios.Text = "Ver historial de precios";
            mnuVerHistorialPrecios.Click += mnuVerHistorialPrecios_Click;

            mnuEditarPrecio.Text = "Editar precio (fecha hoy)";
            mnuEditarPrecio.Click += mnuEditarPrecio_Click;

            dgvProductos.ContextMenuStrip = cmOpciones;

            // Botones base
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

            // NUEVOS botones a la derecha
            btnVerHistorialPrecios.Location = new Point(500, 470);
            btnVerHistorialPrecios.Size = new Size(150, 30);
            btnVerHistorialPrecios.Text = "Ver historial precios";
            btnVerHistorialPrecios.Click += btnVerHistorialPrecios_Click;

            btnEditarPrecio.Location = new Point(660, 470);
            btnEditarPrecio.Size = new Size(150, 30);
            btnEditarPrecio.Text = "Editar precio";
            btnEditarPrecio.Click += btnEditarPrecio_Click;

            // ProductoForm
            ClientSize = new Size(1100, 520);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(dgvProductos);
            Controls.Add(btnNuevo);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(btnVolver);
            Controls.Add(btnVerHistorialPrecios); 
            Controls.Add(btnEditarPrecio);        
            Name = "ProductoForm";
            Text = "Gestión de Productos";
            Load += ProductoForm_Load;

            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
