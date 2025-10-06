using DTOs;
using System;
using System.Windows.Forms;

namespace WinForms
{
    public partial class AñanirProductoPedidoForm : Form
    {
        public LineaPedidoDTO LineaPedido { get; private set; }
        private readonly ProductoDTO _producto;

        public AñanirProductoPedidoForm(ProductoDTO producto)
        {
            InitializeComponent();
            _producto = producto;
            lblProductoNombre.Text = producto.Nombre;
            numCantidad.Value = 1;

            StyleManager.ApplyButtonStyle(btnConfirmar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            int cantidad = (int)numCantidad.Value;
            if (cantidad > 0)
            {
                LineaPedido = new LineaPedidoDTO
                {
                    IdProducto = _producto.IdProducto,
                    NombreProducto = _producto.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = (decimal)_producto.PrecioActual
                };
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

