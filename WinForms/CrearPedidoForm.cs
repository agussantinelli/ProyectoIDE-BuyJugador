using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
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

        private BindingList<LineaPedidoDTO> _lineasPedido = new();
        private List<ProductoDTO> _productosDelProveedor = new();

        public CrearPedidoForm(
            ProductoApiClient productoApiClient,
            PedidoApiClient pedidoApiClient,
            ProveedorApiClient proveedorApiClient)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _pedidoApiClient = pedidoApiClient;
            _proveedorApiClient = proveedorApiClient;

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
                    cmbProductos.DataSource = _productosDelProveedor;
                    cmbProductos.DisplayMember = "Nombre";
                    cmbProductos.ValueMember = "IdProducto";
                    cmbProductos.SelectedIndex = _productosDelProveedor.Any() ? 0 : -1;
                    btnAgregarProducto.Enabled = _productosDelProveedor.Any();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar productos del proveedor: {ex.Message}", "Error");
                    _productosDelProveedor.Clear();
                    cmbProductos.DataSource = null;
                    btnAgregarProducto.Enabled = false;
                }
            }
            else
            {
                btnAgregarProducto.Enabled = false;
                cmbProductos.DataSource = null;
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
            if (cmbProductos.SelectedItem is not ProductoDTO productoSeleccionado)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_lineasPedido.Any(l => l.IdProducto == productoSeleccionado.IdProducto))
            {
                MessageBox.Show("Este producto ya ha sido agregado al pedido.", "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var nuevaLinea = new LineaPedidoDTO
            {
                IdProducto = productoSeleccionado.IdProducto,
                NombreProducto = productoSeleccionado.Nombre,
                Cantidad = (int)numCantidad.Value,
                PrecioUnitario = productoSeleccionado.PrecioCompra
            };

            _lineasPedido.Add(nuevaLinea);
            ActualizarTotal();
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
                MarcarComoRecibido = chkMarcarRecibido.Checked // # NUEVO
            };

            try
            {
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

