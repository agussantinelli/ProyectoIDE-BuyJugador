using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    public partial class EditarProductoForm : Form
    {
        private readonly int _productoId;
        private readonly ProductoApiClient _productoApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;
        private ProductoDTO _producto;

        public EditarProductoForm(int productoId, ProductoApiClient productoApiClient, TipoProductoApiClient tipoProductoApiClient)
        {
            InitializeComponent();
            _productoId = productoId;
            _productoApiClient = productoApiClient;
            _tipoProductoApiClient = tipoProductoApiClient;
        }

        private async void EditarProductoForm_Load(object sender, EventArgs e)
        {
            _producto = await _productoApiClient.GetByIdAsync(_productoId);
            var tiposProducto = await _tipoProductoApiClient.GetAllAsync();

            cmbTipoProducto.DataSource = tiposProducto;
            cmbTipoProducto.DisplayMember = "Descripcion";
            cmbTipoProducto.ValueMember = "IdTipoProducto";

            txtNombre.Text = _producto.Nombre;
            txtDescripcion.Text = _producto.Descripcion;
            numStock.Value = _producto.Stock;
            numPrecio.Value = _producto.PrecioActual;
            if (_producto.IdTipoProducto.HasValue)
            {
                cmbTipoProducto.SelectedValue = _producto.IdTipoProducto.Value;
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            _producto.Nombre = txtNombre.Text;
            _producto.Descripcion = txtDescripcion.Text;
            _producto.Stock = (int)numStock.Value;
            _producto.IdTipoProducto = (int)cmbTipoProducto.SelectedValue;

            // Si el precio cambió, se agrega uno nuevo con la fecha actual.
            if (numPrecio.Value != _producto.PrecioActual)
            {
                var nuevoPrecio = new PrecioDTO
                {
                    Monto = numPrecio.Value,
                    FechaDesde = DateTime.Now
                };
                // Aseguramos que la lista de precios no sea nula
                if (_producto.Precios == null)
                {
                    _producto.Precios = new List<PrecioDTO>();
                }
                _producto.Precios.Add(nuevoPrecio);
            }

            await _productoApiClient.UpdateAsync(_producto.IdProducto, _producto);
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
