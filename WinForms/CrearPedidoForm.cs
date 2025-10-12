using ApiClient;
using DTOs;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class CrearPedidoForm : BaseForm
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly PedidoApiClient _pedidoApiClient;
        private readonly ProveedorApiClient _proveedorApiClient;
        private readonly PrecioCompraApiClient _precioCompraApiClient;

        private BindingList<LineaPedidoDTO> _lineasPedidoActual = new();
        private int _nroLineaCounter = 1;

        public CrearPedidoForm(ProductoApiClient productoApiClient, PedidoApiClient pedidoApiClient, ProveedorApiClient proveedorApiClient, PrecioCompraApiClient precioCompraApiClient)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _pedidoApiClient = pedidoApiClient;
            _proveedorApiClient = proveedorApiClient;
            _precioCompraApiClient = precioCompraApiClient;

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
            dataGridLineasPedido.DataSource = _lineasPedidoActual;
        }

        private async Task CargarProveedores()
        {
            try
            {
                var proveedores = await _proveedorApiClient.GetProveedoresAsync();
                cmbProveedores.DataSource = proveedores;
                cmbProveedores.DisplayMember = "RazonSocial";
                cmbProveedores.ValueMember = "IdProveedor";
                cmbProveedores.SelectedIndex = -1;
                cmbProductos.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proveedores: {ex.Message}", "Error");
            }
        }

        private async void cmbProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedores.SelectedItem is ProveedorDTO proveedor)
            {
                try
                {
                    var productos = await _productoApiClient.GetProductosByProveedorIdAsync(proveedor.IdProveedor);
                    cmbProductos.DataSource = productos;
                    cmbProductos.DisplayMember = "Nombre";
                    cmbProductos.ValueMember = "IdProducto";
                    cmbProductos.Enabled = true;
                    cmbProductos.SelectedIndex = -1;
                    cmbProductos.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar productos del proveedor: {ex.Message}", "Error");
                    cmbProductos.DataSource = null;
                    cmbProductos.Enabled = false;
                }
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

        private async void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cmbProveedores.SelectedItem is not ProveedorDTO proveedorSeleccionado || cmbProductos.SelectedItem is not ProductoDTO productoSeleccionado)
            {
                MessageBox.Show("Seleccione un proveedor y un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var cantidad = (int)numCantidad.Value;
            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var precioCompra = await _precioCompraApiClient.GetByIdAsync(productoSeleccionado.IdProducto, proveedorSeleccionado.IdProveedor);

                if (precioCompra == null)
                {
                    MessageBox.Show("Este producto no tiene un precio de compra asignado por el proveedor seleccionado. No se puede agregar al pedido.", "Precio no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var lineaExistente = _lineasPedidoActual.FirstOrDefault(l => l.IdProducto == productoSeleccionado.IdProducto);
                if (lineaExistente != null)
                {
                    lineaExistente.Cantidad += cantidad;
                }
                else
                {
                    _lineasPedidoActual.Add(new LineaPedidoDTO
                    {
                        IdProducto = productoSeleccionado.IdProducto,
                        Cantidad = cantidad,
                        NombreProducto = productoSeleccionado.Nombre,
                        PrecioUnitario = precioCompra.Monto,
                        NroLineaPedido = _nroLineaCounter++
                    });
                }
                _lineasPedidoActual.ResetBindings();
                ActualizarTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al verificar el precio: {ex.Message}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (dataGridLineasPedido.CurrentRow != null)
            {
                _lineasPedidoActual.Remove((LineaPedidoDTO)dataGridLineasPedido.CurrentRow.DataBoundItem);
                ActualizarTotal();
            }
        }

        private void ActualizarTotal()
        {
            lblTotalPedido.Text = $"Total: {_lineasPedidoActual.Sum(l => l.Subtotal):C}";
        }

        private async void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            if (cmbProveedores.SelectedValue == null || !_lineasPedidoActual.Any())
            {
                MessageBox.Show("Debe seleccionar un proveedor y agregar al menos un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pedidoCompletoDto = new CrearPedidoCompletoDTO
            {
                IdProveedor = (int)cmbProveedores.SelectedValue,
                LineasPedido = _lineasPedidoActual.ToList(),
            };

            try
            {
                var pedidoCreado = await _pedidoApiClient.CreatePedidoCompletoAsync(pedidoCompletoDto);
                MessageBox.Show($"Pedido #{pedidoCreado?.IdPedido} creado exitosamente.", "Pedido Creado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
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
