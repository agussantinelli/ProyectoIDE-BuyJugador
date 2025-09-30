using ApiClient;
using DTOs;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class EditarPrecioForm : Form
    {
        private readonly PrecioApiClient _precioApiClient;
        private readonly int _idProducto;
        private readonly string _nombreProducto;
        private readonly decimal? _precioActual;

        public EditarPrecioForm(PrecioApiClient precioApiClient, int idProducto, string nombreProducto, decimal? precioActual)
        {
            InitializeComponent();
            _precioApiClient = precioApiClient;
            _idProducto = idProducto;
            _nombreProducto = nombreProducto ?? "";
            _precioActual = precioActual;
        }

        private void EditarPrecioForm_Load(object sender, EventArgs e)
        {
            lblProductoValor.Text = $"{_nombreProducto} (ID: {_idProducto})";
            lblPrecioActualValor.Text = _precioActual.HasValue ? _precioActual.Value.ToString("C2") : "-";
            if (_precioActual.HasValue) nudNuevoPrecio.Value = _precioActual.Value > nudNuevoPrecio.Maximum ? nudNuevoPrecio.Maximum : _precioActual.Value;
            nudNuevoPrecio.Select(0, nudNuevoPrecio.Text.Length);
            nudNuevoPrecio.Focus();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var monto = nudNuevoPrecio.Value;
            if (monto <= 0)
            {
                MessageBox.Show("El monto debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new PrecioDTO
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
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error de red: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
