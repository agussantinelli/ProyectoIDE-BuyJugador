using DTOs;
using System;
using System.Windows.Forms;

namespace WinForms
{
    public partial class AñadirProductoVentaForm : Form
    {
        public LineaVentaDTO LineaVenta { get; private set; }
        private readonly ProductoDTO _producto;

        public AñadirProductoVentaForm(ProductoDTO producto)
        {
            InitializeComponent();
            _producto = producto;
            lblProductoNombre.Text = producto.Nombre;

            numCantidad.Maximum = producto.Stock > 0 ? producto.Stock : 1;
            if (producto.Stock > 0)
            {
                numCantidad.Value = 1;
            }

            StyleManager.ApplyButtonStyle(btnConfirmar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            int cantidad = (int)numCantidad.Value;
            if (cantidad > 0)
            {
                LineaVenta = new LineaVentaDTO
                {
                    IdProducto = _producto.IdProducto,
                    NombreProducto = _producto.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = _producto.PrecioActual,
                    Subtotal = cantidad * _producto.PrecioActual
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

