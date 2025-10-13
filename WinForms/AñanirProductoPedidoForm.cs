using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    public partial class AñanirProductoPedidoForm : BaseForm
    {
        public LineaPedidoDTO? LineaPedido { get; private set; }
        public ProductoDTO? ProductoSeleccionado => cmbProductos.SelectedItem as ProductoDTO;
        public int CantidadSeleccionada => (int)numCantidad.Value;

        // Constructor vacío para permitir la inyección de dependencias.
        public AñanirProductoPedidoForm()
        {
            InitializeComponent();
            StyleManager.ApplyButtonStyle(btnConfirmar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        // # NUEVO: Método para cargar la lista de productos disponibles en el ComboBox.
        public void CargarProductosDisponibles(List<ProductoDTO> productos)
        {
            cmbProductos.DataSource = productos;
            cmbProductos.DisplayMember = "Nombre";
            cmbProductos.ValueMember = "IdProducto";
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ProductoSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CantidadSeleccionada <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // # CORRECCIÓN: Usamos la propiedad `PrecioCompra` que ahora existe en `ProductoDTO`.
            LineaPedido = new LineaPedidoDTO
            {
                IdProducto = ProductoSeleccionado.IdProducto,
                NombreProducto = ProductoSeleccionado.Nombre,
                Cantidad = CantidadSeleccionada,
                PrecioUnitario = ProductoSeleccionado.PrecioCompra
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

