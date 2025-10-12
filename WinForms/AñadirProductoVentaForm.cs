using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    // # Este formulario se mantiene como modal (usa ShowDialog).
    // # Se le hace heredar de BaseForm para consistencia de estilo.
    public partial class AñadirProductoVentaForm : BaseForm
    {
        public ProductoDTO ProductoSeleccionado { get; private set; }
        public int CantidadSeleccionada { get; private set; }

        public AñadirProductoVentaForm()
        {
            InitializeComponent();
            StyleManager.ApplyButtonStyle(btnAceptar);
            StyleManager.ApplyButtonStyle(btnCancelar);
            StyleManager.ApplyDataGridViewStyle(dgvProductos);
        }

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
                dgvProductos.Columns["Stock"].HeaderText = "Stock";
                dgvProductos.Columns["PrecioActual"].HeaderText = "Precio";
                dgvProductos.Columns["PrecioActual"].DefaultCellStyle.Format = "C2";

                dgvProductos.Columns["IdProducto"].Visible = false;
                dgvProductos.Columns["IdTipoProducto"].Visible = false;
                dgvProductos.Columns["Activo"].Visible = false;
                dgvProductos.Columns["Precios"].Visible = false;

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

                if (cantidad > selectedProduct.Stock)
                {
                    MessageBox.Show($"La cantidad solicitada ({cantidad}) supera el stock disponible ({selectedProduct.Stock}).", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
