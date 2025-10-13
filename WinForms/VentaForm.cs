using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class VentaForm : BaseForm
    {
        private readonly VentaApiClient _ventaApiClient;
        private readonly UserSessionService _userSessionService;
        private readonly IServiceProvider _serviceProvider;
        private List<VentaDTO> _todasLasVentas;

        public VentaForm(
            VentaApiClient ventaApiClient,
            IServiceProvider serviceProvider,
            UserSessionService userSessionService)
        {
            InitializeComponent();
            _ventaApiClient = ventaApiClient;
            _serviceProvider = serviceProvider;
            _userSessionService = userSessionService;
            _todasLasVentas = new List<VentaDTO>();

            StyleManager.ApplyDataGridViewStyle(dataGridVentas);
            StyleManager.ApplyButtonStyle(btnNuevaVenta);
            StyleManager.ApplyButtonStyle(btnVerDetalle);
            StyleManager.ApplyButtonStyle(btnEliminar);
            StyleManager.ApplyButtonStyle(btnVolver);
            StyleManager.ApplyButtonStyle(btnFinalizarVenta);
        }

        private async void VentaForm_Load(object sender, EventArgs e)
        {
            if (cmbFiltroGasto.Items.Count == 0)
            {
                cmbFiltroGasto.Items.AddRange(new object[]
                {
                    "Todos",
                    "Hasta $10.000",
                    "$10.001 a $50.000",
                    "Más de $50.000"
                });
            }
            cmbFiltroGasto.SelectedIndex = 0;

            await CargarVentas();
            ConfigurarVisibilidadControles();
            ActualizarEstadoBotones();
        }

        private void ConfigurarVisibilidadControles()
        {
            bool esAdmin = _userSessionService.EsAdmin;
            btnNuevaVenta.Visible = esAdmin;
            btnEliminar.Visible = esAdmin;
        }

        private async Task CargarVentas()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var ventas = await _ventaApiClient.GetAllAsync();
                _todasLasVentas = ventas?.OrderByDescending(v => v.Fecha).ToList() ?? new List<VentaDTO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _todasLasVentas = new List<VentaDTO>();
            }
            finally
            {
                AplicarFiltros();
                this.Cursor = Cursors.Default;
            }
        }

        private void AplicarFiltros()
        {
            try
            {
                var textoBusqueda = txtBuscarCliente.Text.Trim().ToLower();
                var filtroGasto = cmbFiltroGasto.SelectedIndex;

                var ventasFiltradas = (_todasLasVentas ?? new List<VentaDTO>())
                    .Where(v => v.NombreVendedor != null && v.NombreVendedor.ToLower().Contains(textoBusqueda))
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
                if (ventasFiltradas.Any())
                {
                    dataGridVentas.DataSource = ventasFiltradas;
                    ConfigurarColumnas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al aplicar los filtros: {ex.Message}", "Error de Filtrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dataGridVentas.Columns.Contains("IdVenta"))
            {
                dataGridVentas.Columns["IdVenta"].HeaderText = "ID Venta";
                dataGridVentas.Columns["IdVenta"].Width = 80;
            }
            if (dataGridVentas.Columns.Contains("Fecha"))
            {
                dataGridVentas.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dataGridVentas.Columns["Fecha"].Width = 120;
            }
            if (dataGridVentas.Columns.Contains("NombreVendedor"))
            {
                dataGridVentas.Columns["NombreVendedor"].HeaderText = "Vendedor";
                dataGridVentas.Columns["NombreVendedor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dataGridVentas.Columns.Contains("Total"))
            {
                dataGridVentas.Columns["Total"].DefaultCellStyle.Format = "C2";
                dataGridVentas.Columns["Total"].Width = 100;
            }
            if (dataGridVentas.Columns.Contains("Estado"))
            {
                dataGridVentas.Columns["Estado"].Width = 100;
            }
            if (dataGridVentas.Columns.Contains("IdPersona"))
                dataGridVentas.Columns["IdPersona"].Visible = false;
            if (dataGridVentas.Columns.Contains("Lineas"))
                dataGridVentas.Columns["Lineas"].Visible = false;
        }

        private void FiltrosChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            var existingForm = this.MdiParent?.MdiChildren.OfType<CrearVentaForm>().FirstOrDefault();
            if (existingForm != null)
            {
                existingForm.BringToFront();
                return;
            }

            var form = _serviceProvider.GetRequiredService<CrearVentaForm>();
            form.MdiParent = this.MdiParent;
            form.FormClosed += async (s, args) => {
                if (form.DialogResult == DialogResult.OK)
                {
                    await CargarVentas();
                }
            };
            form.Show();
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dataGridVentas.CurrentRow?.DataBoundItem is not VentaDTO selectedVenta)
            {
                MessageBox.Show("Por favor, seleccione una venta para ver su detalle.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existingForm = this.MdiParent?.MdiChildren
                .OfType<DetalleVentaForm>()
                .FirstOrDefault(f => f.Tag is int ventaId && ventaId == selectedVenta.IdVenta);

            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                // # CORRECCIÓN: Se crea la instancia de DetalleVentaForm con los 5 argumentos correctos.
                var detalleForm = new DetalleVentaForm(
                    selectedVenta.IdVenta,
                    _userSessionService.EsAdmin,
                    _serviceProvider.GetRequiredService<VentaApiClient>(),
                    _serviceProvider.GetRequiredService<ProductoApiClient>(),
                    _serviceProvider
                );

                detalleForm.Tag = selectedVenta.IdVenta;
                detalleForm.MdiParent = this.MdiParent;

                detalleForm.FormClosed += async (s, args) => {
                    if (detalleForm.DialogResult == DialogResult.OK)
                    {
                        await CargarVentas();
                    }
                };
                detalleForm.Show();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridVentas.CurrentRow?.DataBoundItem is not VentaDTO selectedVenta)
            {
                MessageBox.Show("Por favor, seleccione una venta para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"¿Está seguro que desea eliminar la venta #{selectedVenta.IdVenta}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            try
            {
                var response = await _ventaApiClient.DeleteAsync(selectedVenta.IdVenta);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Venta eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void btnFinalizarVenta_Click(object sender, EventArgs e)
        {
            if (dataGridVentas.CurrentRow?.DataBoundItem is not VentaDTO selectedVenta) return;

            var confirmar = MessageBox.Show($"¿Desea marcar la venta #{selectedVenta.IdVenta} como FINALIZADA?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar != DialogResult.Yes) return;

            try
            {
                var ventaCompleta = await _ventaApiClient.GetByIdAsync(selectedVenta.IdVenta);
                if (ventaCompleta?.IdPersona == null)
                {
                    MessageBox.Show("No se encontró la venta para finalizar o no tiene un vendedor asignado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var dto = new CrearVentaCompletaDTO
                {
                    IdVenta = ventaCompleta.IdVenta,
                    IdPersona = ventaCompleta.IdPersona.Value,
                    Lineas = ventaCompleta.Lineas ?? new List<LineaVentaDTO>(),
                    Finalizada = true
                };

                var response = await _ventaApiClient.UpdateCompletaAsync(dto);

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

        private void DataGridVentas_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void ActualizarEstadoBotones()
        {
            bool hayFilaSeleccionada = dataGridVentas.CurrentRow != null;
            btnVerDetalle.Enabled = hayFilaSeleccionada;
            btnEliminar.Enabled = hayFilaSeleccionada && _userSessionService.EsAdmin;

            if (hayFilaSeleccionada && dataGridVentas.CurrentRow.DataBoundItem is VentaDTO selectedVenta)
            {
                btnFinalizarVenta.Enabled = "Pendiente".Equals(selectedVenta.Estado, StringComparison.OrdinalIgnoreCase) && _userSessionService.EsAdmin;
                btnFinalizarVenta.Visible = _userSessionService.EsAdmin;
            }
            else
            {
                btnFinalizarVenta.Enabled = false;
                btnFinalizarVenta.Visible = _userSessionService.EsAdmin;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

