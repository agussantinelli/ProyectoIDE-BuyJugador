using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    public partial class AñadirProductoPedidoForm : BaseForm
    {
        private readonly List<ProductoDTO> _productos;

        public ProductoDTO? ProductoSeleccionado { get; private set; }
        public int CantidadSeleccionada => (int)numCantidad.Value;

        public AñadirProductoPedidoForm(List<ProductoDTO> productosDisponibles)
        {
            InitializeComponent();
            _productos = productosDisponibles;

            // Estilos
            StyleManager.ApplyPrimaryButtonStyle(btnSeleccionar);
            StyleManager.ApplySecondaryButtonStyle(btnCancelar);
        }

        private void AñadirProductoPedidoForm_Load(object sender, EventArgs e)
        {
            cmbProductos.DataSource = _productos;
            cmbProductos.DisplayMember = "Nombre";
            cmbProductos.ValueMember = "IdProducto";
            cmbProductos.SelectedIndex = -1;
            lblPrecio.Text = "Precio: $0.00";
        }

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem is ProductoDTO producto)
            {
                lblPrecio.Text = $"Precio: {producto.PrecioActual:C}";
                // A diferencia de la venta, no limitamos la cantidad por el stock.
                numCantidad.Maximum = 9999;
                numCantidad.Value = 1;
            }
            else
            {
                lblPrecio.Text = "Precio: $0.00";
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem is ProductoDTO seleccionado)
            {
                ProductoSeleccionado = seleccionado;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Seleccione un producto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
