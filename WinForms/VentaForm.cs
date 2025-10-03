using ApiClient;
using DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace WinForms
{
    public partial class VentaForm : BaseForm
    {
        private readonly VentaApiClient _ventaApiClient;
        private readonly LineaVentaApiClient _lineaVentaApiClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserSessionService _userSessionService;

        public VentaForm(
            VentaApiClient ventaApiClient,
            LineaVentaApiClient lineaVentaApiClient,
            IServiceProvider serviceProvider,
            UserSessionService userSessionService)
        {
            InitializeComponent();
            _ventaApiClient = ventaApiClient;
            _lineaVentaApiClient = lineaVentaApiClient;
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
        }

        private async Task CargarVentas()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var ventas = await _ventaApiClient.GetAllAsync();
                if (ventas != null)
                {
                    dataGridVentas.DataSource = ventas.OrderByDescending(v => v.Fecha).ToList();
                    ConfigurarColumnas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarColumnas()
        {
            dataGridVentas.Columns["IdVenta"].HeaderText = "ID Venta";
            dataGridVentas.Columns["Fecha"].HeaderText = "Fecha";
            dataGridVentas.Columns["Total"].HeaderText = "Total";
            dataGridVentas.Columns["NombreVendedor"].HeaderText = "Vendedor";
            dataGridVentas.Columns["IdPersona"].Visible = false;

            dataGridVentas.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dataGridVentas.Columns["Total"].DefaultCellStyle.Format = "C2";

            dataGridVentas.Columns["NombreVendedor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private async void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<CrearVentaForm>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                await CargarVentas();
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

        private async void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dataGridVentas.CurrentRow?.DataBoundItem is not VentaDTO selectedVenta)
            {
                MessageBox.Show("Por favor, seleccione una venta para ver su detalle.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            try
            {
                var lineasDeLaVenta = await _lineaVentaApiClient.GetLineasByVentaIdAsync(selectedVenta.IdVenta);

                if (lineasDeLaVenta == null)
                {
                    MessageBox.Show("No se pudieron cargar las líneas para esta venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool esAdmin = "Dueño".Equals(_userSessionService.CurrentUser?.Rol, StringComparison.OrdinalIgnoreCase);

                using var detalleForm = _serviceProvider.GetRequiredService<DetalleVentaForm>();

                detalleForm.Venta = selectedVenta;
                detalleForm.Lineas = lineasDeLaVenta;
                detalleForm.EsAdmin = esAdmin;

                if (detalleForm.ShowDialog() == DialogResult.OK)
                {
                    await CargarVentas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar el detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void btnFinalizarVenta_Click(object sender, EventArgs e)
        {
            if (dataGridVentas.CurrentRow?.DataBoundItem is not VentaDTO selectedVenta)
            {
                MessageBox.Show("Por favor, seleccione una venta para finalizar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!"Pendiente".Equals(selectedVenta.Estado, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("La venta ya está finalizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmar = MessageBox.Show($"¿Desea marcar la venta #{selectedVenta.IdVenta} como FINALIZADA?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar != DialogResult.Yes) return;

            try
            {
                var response = await _ventaApiClient.MarcarComoFinalizadaAsync(selectedVenta.IdVenta);
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

