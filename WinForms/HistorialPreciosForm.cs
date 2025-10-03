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
    public partial class HistorialPreciosForm : BaseForm
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

            this.StartPosition = FormStartPosition.CenterScreen;

            StyleManager.ApplyDataGridViewStyle(dgvHistorial);
            StyleManager.ApplyButtonStyle(btnCerrar);
            StyleManager.ApplyButtonStyle(btnRefrescar);
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
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            dgvHistorial.Columns.Clear();
            dgvHistorial.AutoGenerateColumns = false;

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FechaDesde",
                HeaderText = "Fecha desde",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" },
                Width = 160
            });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Monto",
                HeaderText = "Monto",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
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
        }
    }
}
