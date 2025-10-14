using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class PedidoForm : BaseForm
    {
        private readonly PedidoApiClient _pedidoApiClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserSessionService _userSessionService;
        private List<PedidoDTO> _todosLosPedidos = new();

        public PedidoForm(PedidoApiClient pedidoApiClient, IServiceProvider serviceProvider, UserSessionService userSessionService)
        {
            InitializeComponent();
            _pedidoApiClient = pedidoApiClient;
            _serviceProvider = serviceProvider;
            _userSessionService = userSessionService;

            StyleManager.ApplyDataGridViewStyle(dataGridPedidos);
            StyleManager.ApplyButtonStyle(btnVolver);
            StyleManager.ApplyButtonStyle(btnNuevoPedido);
            StyleManager.ApplyButtonStyle(btnEliminar);
            StyleManager.ApplyButtonStyle(btnFinalizarPedido);
            StyleManager.ApplyButtonStyle(btnVerDetalle);

            ConfigurarColumnas();
        }

        private async void PedidoForm_Load(object sender, EventArgs e)
        {
            if (cmbFiltroGasto.Items.Count == 0)
            {
                cmbFiltroGasto.Items.AddRange(new object[]
                {
                    "Todos",
                    "Hasta $100.000",
                    "Entre $100.000 y $500.000",
                    "Más de $500.000"
                });
            }
            cmbFiltroGasto.SelectedIndex = 0;

            await CargarPedidos();
            ConfigurarVisibilidadControles();
            ActualizarEstadoBotones();
        }

        private void ConfigurarVisibilidadControles()
        {
            bool esAdmin = _userSessionService.EsAdmin;
            btnNuevoPedido.Visible = esAdmin;
            btnEliminar.Visible = esAdmin;
            btnFinalizarPedido.Visible = esAdmin;
        }

        private async Task CargarPedidos()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _todosLosPedidos = await _pedidoApiClient.GetAllAsync() ?? new List<PedidoDTO>();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los pedidos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarColumnas()
        {
            dataGridPedidos.AutoGenerateColumns = false;
            dataGridPedidos.Columns.Clear();

            dataGridPedidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdPedido", DataPropertyName = "IdPedido", HeaderText = "Nro. Pedido", Width = 100 });
            dataGridPedidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", DataPropertyName = "Fecha", HeaderText = "Fecha", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }, Width = 120 });
            dataGridPedidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Proveedor", DataPropertyName = "ProveedorRazonSocial", HeaderText = "Proveedor", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridPedidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", DataPropertyName = "Estado", HeaderText = "Estado", Width = 120 });
            dataGridPedidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", DataPropertyName = "Total", HeaderText = "Total", DefaultCellStyle = new DataGridViewCellStyle { Format = "C" }, Width = 150 });
        }

        private void AplicarFiltros()
        {
            var textoBusqueda = txtBuscarProveedor.Text.Trim().ToLower();
            var filtroGasto = cmbFiltroGasto.SelectedIndex;

            var pedidosFiltrados = _todosLosPedidos
                .Where(p => p.ProveedorRazonSocial != null && p.ProveedorRazonSocial.ToLower().Contains(textoBusqueda))
                .Where(p =>
                {
                    return filtroGasto switch
                    {
                        1 => p.Total <= 100000,
                        2 => p.Total > 100000 && p.Total <= 500000,
                        3 => p.Total > 500000,
                        _ => true
                    };
                })
                .ToList();

            dataGridPedidos.DataSource = null;
            if (pedidosFiltrados.Any())
            {
                dataGridPedidos.DataSource = pedidosFiltrados;
            }

            ActualizarEstadoBotones();
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dataGridPedidos.CurrentRow?.DataBoundItem is not PedidoDTO pedidoSeleccionado)
            {
                MessageBox.Show("Por favor, seleccione un pedido para ver su detalle.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existingForm = this.MdiParent?.MdiChildren
                .OfType<DetallePedidoForm>()
                .FirstOrDefault(f => f.Tag is int pedidoId && pedidoId == pedidoSeleccionado.IdPedido);

            if (existingForm != null)
            {
                existingForm.BringToFront();
            }
            else
            {
                var pedidoApiClient = _serviceProvider.GetRequiredService<PedidoApiClient>();
                var productoApiClient = _serviceProvider.GetRequiredService<ProductoApiClient>();

                var detalleForm = new DetallePedidoForm(
                    pedidoSeleccionado.IdPedido,
                    _userSessionService.EsAdmin,
                    pedidoApiClient,
                    productoApiClient,
                    _serviceProvider
                );

                detalleForm.Tag = pedidoSeleccionado.IdPedido;
                detalleForm.MdiParent = this.MdiParent;

                detalleForm.FormClosed += async (s, args) => {
                    if (detalleForm.DialogResult == DialogResult.OK)
                    {
                        await CargarPedidos();
                    }
                };

                detalleForm.Show();
            }
        }

        private void btnNuevoPedido_Click(object sender, EventArgs e)
        {
            var existingForm = this.MdiParent?.MdiChildren.OfType<CrearPedidoForm>().FirstOrDefault();
            if (existingForm != null)
            {
                existingForm.BringToFront();
                return;
            }

            var form = _serviceProvider.GetRequiredService<CrearPedidoForm>();
            form.MdiParent = this.MdiParent;

            form.FormClosed += async (s, args) => {
                if (form.DialogResult == DialogResult.OK)
                {
                    await CargarPedidos();
                }
            };
            form.Show();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridPedidos.CurrentRow?.DataBoundItem is not PedidoDTO pedido) return;

            var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar el pedido #{pedido.IdPedido}?\nEsta acción no se puede deshacer.",
                                              "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var response = await _pedidoApiClient.DeleteAsync(pedido.IdPedido);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Pedido eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarPedidos();
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al eliminar: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            if (dataGridPedidos.CurrentRow?.DataBoundItem is not PedidoDTO pedido) return;

            if (pedido.Estado == "Recibido")
            {
                MessageBox.Show("Este pedido ya ha sido marcado como recibido.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmResult = MessageBox.Show($"¿Desea marcar el pedido #{pedido.IdPedido} como 'Recibido'?",
                                              "Confirmar Recepción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var response = await _pedidoApiClient.MarcarComoRecibidoAsync(pedido.IdPedido);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Pedido marcado como recibido.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarPedidos();
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al finalizar: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DataGridPedidos_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void ActualizarEstadoBotones()
        {
            bool hayFilaSeleccionada = dataGridPedidos.CurrentRow != null;
            btnVerDetalle.Enabled = hayFilaSeleccionada;
            btnEliminar.Enabled = hayFilaSeleccionada && _userSessionService.EsAdmin;

            if (hayFilaSeleccionada && dataGridPedidos.CurrentRow.DataBoundItem is PedidoDTO pedido)
            {
                btnFinalizarPedido.Enabled = "Pendiente".Equals(pedido.Estado, StringComparison.OrdinalIgnoreCase) && _userSessionService.EsAdmin;
            }
            else
            {
                btnFinalizarPedido.Enabled = false;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e) => this.Close();

        private void FiltrosChanged(object sender, EventArgs e) => AplicarFiltros();
    }
}