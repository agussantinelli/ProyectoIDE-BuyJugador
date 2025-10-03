using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace WinForms
{
    public partial class PedidoForm : BaseForm
    {
        private readonly PedidoApiClient _pedidoApiClient;
        private readonly IServiceProvider _serviceProvider;
        private List<PedidoDTO> _todosLosPedidos = new();

        public PedidoForm(PedidoApiClient pedidoApiClient, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _pedidoApiClient = pedidoApiClient;
            _serviceProvider = serviceProvider;

            // Aplicar Estilos
            StyleManager.ApplyDataGridViewStyle(dataGridPedidos);
            StyleManager.ApplyButtonStyle(btnNuevoPedido);
            StyleManager.ApplyButtonStyle(btnVerDetalle);
            StyleManager.ApplyButtonStyle(btnEliminar);
            StyleManager.ApplyButtonStyle(btnFinalizarPedido);
            StyleManager.ApplyButtonStyle(btnVolver);

            ConfigurarColumnas();
        }

        private async void PedidoForm_Load(object sender, EventArgs e)
        {
            cmbFiltroGasto.Items.AddRange(new object[] { "Todos", "Hasta $100.000", "Entre $100.000 y $500.000", "Más de $500.000" });
            cmbFiltroGasto.SelectedIndex = 0;
            await CargarPedidos();
        }

        private async Task CargarPedidos()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _todosLosPedidos = await _pedidoApiClient.GetPedidosAsync() ?? new List<PedidoDTO>();
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

        private void btnNuevoPedido_Click(object sender, EventArgs e)
        {
            var crearPedidoForm = _serviceProvider.GetRequiredService<CrearPedidoForm>();
            if (crearPedidoForm.ShowDialog() == DialogResult.OK)
            {
                _ = CargarPedidos();
            }
        }

        private async void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dataGridPedidos.CurrentRow?.DataBoundItem is PedidoDTO pedidoSeleccionado)
            {
                var detalleForm = _serviceProvider.GetRequiredService<DetallePedidoForm>();
                detalleForm.Pedido = pedidoSeleccionado;
                detalleForm.EsAdmin = true;

                if (detalleForm.ShowDialog() == DialogResult.OK)
                {
                    await CargarPedidos();
                }
            }
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
            dataGridPedidos.DataSource = pedidosFiltrados;
        }

        private void btnVolver_Click(object sender, EventArgs e) => this.Close();
        private void FiltrosChanged(object sender, EventArgs e) => AplicarFiltros();
    }
}
