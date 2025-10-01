using ApiClient;
using DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace WinForms
{
    public partial class VentaForm : Form
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
                MessageBox.Show($"Error al cargar las ventas: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarColumnas()
        {
            if (dataGridVentas.Columns.Count > 0)
            {
                dataGridVentas.Columns["IdVenta"].HeaderText = "N° Venta";
                dataGridVentas.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dataGridVentas.Columns["NombreVendedor"].HeaderText = "Vendedor";
                dataGridVentas.Columns["Total"].DefaultCellStyle.Format = "C2";
                dataGridVentas.Columns["IdPersona"].Visible = false;
            }
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
                MessageBox.Show("Por favor, seleccione una venta para eliminar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar la venta N° {selectedVenta.IdVenta}? Esta acción no se puede deshacer y el stock de los productos será restaurado.",
                                                 "Confirmar Eliminación",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var response = await _ventaApiClient.DeleteAsync(selectedVenta.IdVenta);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("La venta ha sido eliminada exitosamente.", "Eliminación Completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarVentas();
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar la venta: {response.ReasonPhrase}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dataGridVentas.CurrentRow?.DataBoundItem is not VentaDTO selectedVenta)
            {
                MessageBox.Show("Por favor, seleccione una venta para ver el detalle.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}

