using DTOs;
using System;
using System.Windows.Forms;
using ApiClient;

namespace WinForms
{
    // # Este formulario se mantiene como modal (usa ShowDialog).
    public partial class AsignarPrecioProductoProveedorForm : BaseForm
    {
        private readonly PrecioCompraApiClient _precioCompraApiClient;
        private readonly ProductoProveedorApiClient _productoProveedorApiClient;
        private readonly int _idProducto;
        private readonly int _idProveedor;

        public decimal Precio { get; private set; }

        public AsignarPrecioProductoProveedorForm(
            PrecioCompraApiClient precioCompraApiClient,
            ProductoProveedorApiClient productoProveedorApiClient,
            int idProducto,
            int idProveedor,
            string nombreProducto,
            string razonSocial)
        {
            InitializeComponent();
            _precioCompraApiClient = precioCompraApiClient;
            _productoProveedorApiClient = productoProveedorApiClient;
            _idProducto = idProducto;
            _idProveedor = idProveedor;

            this.Text = $"Asignar Precio a {nombreProducto}";
            lblProductoInfo.Text = $"Producto: '{nombreProducto}'";
            lblProveedorInfo.Text = $"Proveedor: '{razonSocial}'";

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (numPrecio.Value <= 0)
            {
                MessageBox.Show("El precio debe ser mayor a cero.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Precio = numPrecio.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
