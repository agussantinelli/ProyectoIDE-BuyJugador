using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class CrearPedidoForm : BaseForm
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly PedidoApiClient _pedidoApiClient;
        private readonly ProveedorApiClient _proveedorApiClient;
        private readonly IServiceProvider _serviceProvider;

        private BindingList<LineaPedidoDTO> _lineasPedido = new();
        private List<ProductoDTO> _productosDelProveedor = new();

        public CrearPedidoForm(
            ProductoApiClient productoApiClient,
            PedidoApiClient pedidoApiClient,
            ProveedorApiClient proveedorApiClient,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _pedidoApiClient = pedidoApiClient;
            _proveedorApiClient = proveedorApiClient;
            _serviceProvider = serviceProvider;

            dataGridLineasPedido.DataSource = _lineasPedido;

            StyleManager.ApplyDataGridViewStyle(dataGridLineasPedido);
            StyleManager.ApplyButtonStyle(btnConfirmarPedido);
            StyleManager.ApplyButtonStyle(btnAgregarProducto);
            StyleManager.ApplyButtonStyle(btnEliminarLinea);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void CrearPedidoForm_Load(object sender, EventArgs e)
        {
            await CargarProveedores();
            ConfigurarGridLineas();
            btnAgregarProducto.Enabled = false;
        }

        private async Task CargarProveedores()
        {
            try
            {
                var proveedores = await _proveedorApiClient.GetAllAsync();
                cmbProveedores.DataSource = proveedores;
                cmbProveedores.DisplayMember = "RazonSocial";
                cmbProveedores.ValueMember = "IdProveedor";
                cmbProveedores.SelectedIndex = -1;
                cmbProveedores.Text = "Seleccione un proveedor...";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proveedores: {ex.Message}", "Error");
            }
        }

        private async void cmbProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lineasPedido.Clear();
            ActualizarTotal();

            if (cmbProveedores.SelectedItem is ProveedorDTO proveedor)
            {
                try
                {
                    _productosDelProveedor = await _productoApiClient.GetProductosByProveedorIdAsync(proveedor.IdProveedor) ?? new List<ProductoDTO>();
                    btnAgregarProducto.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar productos del proveedor: {ex.Message}", "Error");
                    _productosDelProveedor.Clear();
                    btnAgregarProducto.Enabled = false;
                }
            }
            else
            {
                btnAgregarProducto.Enabled = false;
            }
        }

        private void ConfigurarGridLineas()
        {
            dataGridLineasPedido.AutoGenerateColumns = false;
            dataGridLineasPedido.Columns.Clear();
            dataGridLineasPedido.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            dataGridLineasPedido.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad", Width = 80, ReadOnly = true });
            dataGridLineasPedido.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioUnitario", HeaderText = "Precio Unit.", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "C" }, ReadOnly = true });
            dataGridLineasPedido.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Subtotal", HeaderText = "Subtotal", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "C" }, ReadOnly = true });
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            var idsProductosEnPedido = _lineasPedido.Select(l => l.IdProducto).ToList();
            var productosDisponibles = _productosDelProveedor
                .Where(p => !idsProductosEnPedido.Contains(p.IdProducto))
                .ToList();

            if (!productosDisponibles.Any())
            {
                MessageBox.Show("No hay más productos de este proveedor para agregar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var form = _serviceProvider.GetRequiredService<AñanirProductoPedidoForm>();
            form.CargarProductosDisponibles(productosDisponibles);

            if (form.ShowDialog() == DialogResult.OK && form.LineaPedido != null)
            {
                _lineasPedido.Add(form.LineaPedido);
                ActualizarTotal();
            }
        }

        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (dataGridLineasPedido.CurrentRow != null)
            {
                _lineasPedido.Remove((LineaPedidoDTO)dataGridLineasPedido.CurrentRow.DataBoundItem);
                ActualizarTotal();
            }
        }

        private void ActualizarTotal()
        {
            lblTotalPedido.Text = $"Total: {_lineasPedido.Sum(l => l.Subtotal):C}";
        }

        private async void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            if (cmbProveedores.SelectedValue == null || !_lineasPedido.Any())
            {
                MessageBox.Show("Debe seleccionar un proveedor y agregar al menos un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pedidoCompletoDto = new CrearPedidoCompletoDTO
            {
                IdProveedor = (int)cmbProveedores.SelectedValue,
                LineasPedido = _lineasPedido.ToList(),
            };

            try
            {
                // # CORRECCIÓN: Se utiliza el método 'CreateAsync' estandarizado en el ApiClient.
                var response = await _pedidoApiClient.CreateAsync(pedidoCompletoDto);
                if (response.IsSuccessStatusCode)
                {
                    var pedidoCreado = await response.Content.ReadFromJsonAsync<PedidoDTO>();
                    MessageBox.Show($"Pedido #{pedidoCreado?.IdPedido} creado exitosamente.", "Pedido Creado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al crear el pedido: {error}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear el pedido: {ex.Message}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

