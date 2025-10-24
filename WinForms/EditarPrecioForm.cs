using ApiClient;
using DTOs;
using System;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;

namespace WinForms
{
    public partial class EditarPrecioForm : BaseForm
    {
        private readonly PrecioVentaApiClient _precioApiClient;
        private readonly int _idProducto;
        private readonly string _nombreProducto;
        private readonly decimal? _precioActual;

        public EditarPrecioForm(PrecioVentaApiClient precioApiClient, int idProducto, string nombreProducto, decimal? precioActual)
        {
            InitializeComponent();
            _precioApiClient = precioApiClient;
            _idProducto = idProducto;
            _nombreProducto = nombreProducto ?? "";
            _precioActual = precioActual;

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private void EditarPrecioForm_Load(object sender, EventArgs e)
        {
            txtProductoId.Text = _idProducto.ToString();
            txtProductoNombre.Text = _nombreProducto;
            lblPrecioActualValor.Text = _precioActual.HasValue ? _precioActual.Value.ToString("C2") : "-";

            txtProductoId.ReadOnly = true;
            txtProductoId.BackColor = Color.LightGray;
            txtProductoNombre.ReadOnly = true;
            txtProductoNombre.BackColor = Color.LightGray;

            if (_precioActual.HasValue) nudNuevoPrecio.Value = _precioActual.Value > nudNuevoPrecio.Maximum ? nudNuevoPrecio.Maximum : _precioActual.Value;
            nudNuevoPrecio.Select(0, nudNuevoPrecio.Text.Length);
            nudNuevoPrecio.Focus();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(nudNuevoPrecio.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("El monto debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new PrecioVentaDTO
            {
                IdProducto = _idProducto,
                FechaDesde = DateTime.Now,
                Monto = monto
            };

            try
            {
                var resp = await _precioApiClient.CreateAsync(dto);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Precio guardado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show($"No se pudo guardar el precio. Código: {(int)resp.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
