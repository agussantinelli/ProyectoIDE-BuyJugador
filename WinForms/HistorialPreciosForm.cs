using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class HistorialPreciosForm : Form
    {
        private readonly PrecioApiClient _precioApiClient;
        private readonly int _idProducto;
        private readonly string _nombreProducto;

        private List<PrecioDTO> _cache = new();

        public HistorialPreciosForm(PrecioApiClient precioApiClient, int idProducto, string nombreProducto)
        {
            InitializeComponent();
            _precioApiClient = precioApiClient;
            _idProducto = idProducto;
            _nombreProducto = nombreProducto ?? "";
        }

        private async void HistorialPreciosForm_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = $"Historial de precios - {_nombreProducto} (ID: {_idProducto})";
            await CargarHistorial();
        }

        private async Task CargarHistorial()
        {
            try
            {
                var todos = await _precioApiClient.GetAllAsync() ?? new List<PrecioDTO>();
                _cache = todos
                    .Where(p => p.IdProducto == _idProducto)
                    .OrderByDescending(p => p.FechaDesde)
                    .ToList();

                dgvHistorial.DataSource = _cache;
                ConfigurarColumnas();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error de red: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvHistorial.Columns.Contains("IdProducto"))
            {
                dgvHistorial.Columns["IdProducto"].HeaderText = "Producto";
                dgvHistorial.Columns["IdProducto"].Width = 90;
            }
            if (dgvHistorial.Columns.Contains("FechaDesde"))
            {
                dgvHistorial.Columns["FechaDesde"].HeaderText = "Fecha desde";
                dgvHistorial.Columns["FechaDesde"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                dgvHistorial.Columns["FechaDesde"].Width = 160;
            }
            if (dgvHistorial.Columns.Contains("Monto"))
            {
                dgvHistorial.Columns["Monto"].HeaderText = "Monto";
                dgvHistorial.Columns["Monto"].DefaultCellStyle.Format = "C2";
                dgvHistorial.Columns["Monto"].Width = 110;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e) => Close();

        private async void btnRefrescar_Click(object sender, EventArgs e) => await CargarHistorial();

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            var filtro = txtBuscar.Text?.Trim().ToLowerInvariant() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(filtro))
            {
                dgvHistorial.DataSource = _cache.ToList();
            }
            else
            {
                dgvHistorial.DataSource = _cache
                    .Where(p =>
                        p.FechaDesde.ToString("yyyy-MM-dd HH:mm").ToLower().Contains(filtro) ||
                        p.Monto.ToString("0.##").ToLower().Contains(filtro))
                    .ToList();
            }

            ConfigurarColumnas();
        }
    }
}
