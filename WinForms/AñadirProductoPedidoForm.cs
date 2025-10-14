using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    public partial class AñadirProductoPedidoForm : BaseForm
    {
        public ProductoDTO ProductoSeleccionado { get; private set; }
        public int CantidadSeleccionada { get; private set; }

        public AñadirProductoPedidoForm()
        {
            InitializeComponent();
            StyleManager.ApplyButtonStyle(btnAceptar);
            StyleManager.ApplyButtonStyle(btnCancelar);
            StyleManager.ApplyDataGridViewStyle(dgvProductos);
        }

        // # Carga la lista de productos en el DataGridView.
        public void CargarProductosDisponibles(List<ProductoDTO> productos)
        {
            dgvProductos.DataSource = productos;
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            if (dgvProductos.Columns.Count > 0)
            {
                dgvProductos.Columns["Nombre"].HeaderText = "Producto";
                dgvProductos.Columns["Descripcion"].HeaderText = "Descripción";
                dgvProductos.Columns["Stock"].HeaderText = "Stock Actual";

                dgvProductos.Columns["Precio"].HeaderText = "Precio de Compra";
                dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "C2";

                // # Ocultar columnas no relevantes.
                if (dgvProductos.Columns["IdProducto"] != null) dgvProductos.Columns["IdProducto"].Visible = false;
                if (dgvProductos.Columns["IdTipoProducto"] != null) dgvProductos.Columns["IdTipoProducto"].Visible = false;
                if (dgvProductos.Columns["Activo"] != null) dgvProductos.Columns["Activo"].Visible = false;
                if (dgvProductos.Columns["Precios"] != null) dgvProductos.Columns["Precios"].Visible = false;

                dgvProductos.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvProductos.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow?.DataBoundItem is ProductoDTO selectedProduct && int.TryParse(txtCantidad.Text, out int cantidad))
            {
                if (cantidad <= 0)
                {
                    MessageBox.Show("La cantidad debe ser mayor a cero.", "Cantidad inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ProductoSeleccionado = selectedProduct;
                CantidadSeleccionada = cantidad;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto e ingrese una cantidad numérica válida.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
