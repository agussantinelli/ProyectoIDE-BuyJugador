using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class VentaForm : BaseForm
    {
        private readonly VentaApiClient _ventaApiClient;
        private readonly LineaVentaApiClient _lineaVentaApiClient;
        private readonly ProductoApiClient _productoApiClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserSessionService _userSessionService;
        private List<VentaDTO> _todasLasVentas = new();

        public VentaForm(
            VentaApiClient ventaApiClient,
            LineaVentaApiClient lineaVentaApiClient,
            ProductoApiClient productoApiClient,
            IServiceProvider serviceProvider,
            UserSessionService userSessionService)
        {
            InitializeComponent();
            _ventaApiClient = ventaApiClient;
            _lineaVentaApiClient = lineaVentaApiClient;
            _productoApiClient = productoApiClient;
            _serviceProvider = serviceProvider;
            _userSessionService = userSessionService;

            this.StartPosition = FormStartPosition.CenterScreen;

            StyleManager.ApplyDataGridViewStyle(dataGridVentas);
            StyleManager.ApplyButtonStyle(btnNuevaVenta);
            StyleManager.ApplyButtonStyle(btnVerDetalle);
            StyleManager.ApplyButtonStyle(btnEliminar);
            StyleManager.ApplyButtonStyle(btnVolver);
            StyleManager.ApplyButtonStyle(btnFinalizarVenta);
        }

        private async void VentaForm_Load(object sender, EventArgs e)
        {
            await CargarVentas();
            ConfigurarVisibilidadControles();
            ActualizarEstadoBotones();
        }

        private void ConfigurarVisibilidadControles()
        {
            bool esAdmin = _userSessionService.EsAdmin;
            btnNuevaVenta.Visible = esAdmin;
            btnEliminar.Visible = esAdmin;
            btnFinalizarVenta.Visible = esAdmin;
        }

        private async System.Threading.Tasks.Task CargarVentas()
        {
            try
            {
                _todasLasVentas = await _ventaApiClient.GetAllAsync();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dataGridVentas.Columns.Count > 0)
            {
                dataGridVentas.Columns["IdVenta"].HeaderText = "ID Venta";
                dataGridVentas.Columns["NombreVendedor"].HeaderText = "Vendedor";
                dataGridVentas.Columns["Total"].DefaultCellStyle.Format = "C2";
                dataGridVentas.Columns["IdPersona"].Visible = false;
                if (dataGridVentas.Columns.Contains("Lineas"))
                {
                    dataGridVentas.Columns["Lineas"].Visible = false;
                }
            }
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            var crearVentaForm = _serviceProvider.GetRequiredService<CrearVentaForm>();
            if (crearVentaForm.ShowDialog() == DialogResult.OK)
            {
                CargarVentas();
            }
        }

        private async void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dataGridVentas.CurrentRow != null)
            {
                var ventaSeleccionada = (VentaDTO)dataGridVentas.CurrentRow.DataBoundItem;
                try
                {
                    var ventaCompleta = await _ventaApiClient.GetByIdAsync(ventaSeleccionada.IdVenta);
                    if (ventaCompleta != null)
                    {
                        var detalleForm = new DetalleVentaForm(_lineaVentaApiClient, _productoApiClient)
                        {
                            Venta = ventaCompleta,
                            EsAdmin = _userSessionService.EsAdmin
                        };

                        if (detalleForm.ShowDialog() == DialogResult.OK)
                        {
                            await CargarVentas();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo obtener el detalle de la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar el detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridVentas.CurrentRow != null)
            {
                var ventaSeleccionada = (VentaDTO)dataGridVentas.CurrentRow.DataBoundItem;
                var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar la venta ID {ventaSeleccionada.IdVenta}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        var response = await _ventaApiClient.DeleteAsync(ventaSeleccionada.IdVenta);
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Venta eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await CargarVentas();
                        }
                        else
                        {
                            var error = await response.Content.ReadAsStringAsync();
                            MessageBox.Show($"Error al eliminar la venta: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnFinalizarVenta_Click(object sender, EventArgs e)
        {
            if (dataGridVentas.CurrentRow == null) return;

            var ventaSeleccionada = (VentaDTO)dataGridVentas.CurrentRow.DataBoundItem;
            if (ventaSeleccionada.Estado.Equals("Finalizada", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Esta venta ya ha sido finalizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var response = await _ventaApiClient.FinalizarVentaAsync(ventaSeleccionada.IdVenta);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Venta finalizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarVentas();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al finalizar la venta: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltros()
        {
            var textoBusqueda = txtBuscarCliente.Text.Trim().ToLower();
            var filtroGasto = cmbFiltroGasto.SelectedIndex;

            var ventasFiltradas = _todasLasVentas
                .Where(v => v.NombreVendedor.ToLower().Contains(textoBusqueda))
                .Where(v =>
                {
                    return filtroGasto switch
                    {
                        1 => v.Total <= 10000,
                        2 => v.Total > 10000 && v.Total <= 50000,
                        3 => v.Total > 50000,
                        _ => true
                    };
                })
                .ToList();

            dataGridVentas.DataSource = null;
            dataGridVentas.DataSource = ventasFiltradas;
            ConfigurarColumnas();
        }

        private void FiltrosChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridVentas_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void ActualizarEstadoBotones()
        {
            bool hayFilaSeleccionada = dataGridVentas.CurrentRow != null;
            btnVerDetalle.Enabled = hayFilaSeleccionada;
            btnEliminar.Enabled = hayFilaSeleccionada && _userSessionService.EsAdmin;
            btnFinalizarVenta.Enabled = hayFilaSeleccionada && _userSessionService.EsAdmin;
        }
    }
}

