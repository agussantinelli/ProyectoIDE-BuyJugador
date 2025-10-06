using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    public partial class EditarProductoForm : BaseForm
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

            this.StartPosition = FormStartPosition.CenterScreen;

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
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
            numPrecio.Value = (decimal)(_producto.PrecioActual ?? 0);
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

            if (numPrecio.Value != _producto.PrecioActual)
            {
                var nuevoPrecio = new PrecioVentaDTO
                {
                    Monto = (decimal)numPrecio.Value,
                    FechaDesde = DateTime.Now
                };

                if (_producto.Precios == null)
                {
                    _producto.Precios = new List<PrecioVentaDTO>();
                }
                _producto.Precios.Add(nuevoPrecio);
            }

            await _productoApiClient.UpdateAsync(_producto.IdProducto, _producto);
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

