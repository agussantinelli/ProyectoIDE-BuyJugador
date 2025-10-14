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
            // # Los controles de producto se deshabilitan hasta que se elija un proveedor.
            ToggleProductControls(false);
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
            // # Limpia el pedido actual y la selección de productos.
            _lineasPedido.Clear();
            cmbProductos.DataSource = null;
            ActualizarTotal();

            if (cmbProveedores.SelectedItem is ProveedorDTO proveedor)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    // # Carga los productos asociados al proveedor seleccionado.
                    var productosDelProveedor = await _productoApiClient.GetProductosByProveedorIdAsync(proveedor.IdProveedor) ?? new List<ProductoDTO>();

                    cmbProductos.DataSource = productosDelProveedor;
                    cmbProductos.DisplayMember = "Nombre";
                    cmbProductos.ValueMember = "IdProducto";
                    cmbProductos.SelectedIndex = -1;
                    cmbProductos.Text = "Seleccione un producto...";

                    ToggleProductControls(true); // # Habilita los controles de producto.
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar productos del proveedor: {ex.Message}", "Error");
                    ToggleProductControls(false);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                ToggleProductControls(false); // # Deshabilita si no hay proveedor.
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem is not ProductoDTO productoSeleccionado)
            {
                MessageBox.Show("Seleccione un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cantidad = (int)numCantidad.Value;
            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var lineaExistente = _lineasPedido.FirstOrDefault(l => l.IdProducto == productoSeleccionado.IdProducto);

            if (lineaExistente != null)
            {
                // # Si el producto ya está en el pedido, suma la cantidad.
                lineaExistente.Cantidad += cantidad;
            }
            else
            {
                // # Si es un producto nuevo, crea la línea.
                _lineasPedido.Add(new LineaPedidoDTO
                {
                    IdProducto = productoSeleccionado.IdProducto,
                    NombreProducto = productoSeleccionado.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = productoSeleccionado.PrecioCompra
                });
            }

            _lineasPedido.ResetBindings(); // # Refresca la grilla.
            ActualizarTotal();

            // # Resetea los controles para la siguiente entrada.
            cmbProductos.SelectedIndex = -1;
            cmbProductos.Text = "";
            numCantidad.Value = 1;
            cmbProductos.Focus();
        }

        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (dataGridLineasPedido.CurrentRow?.DataBoundItem is LineaPedidoDTO lineaSeleccionada)
            {
                _lineasPedido.Remove(lineaSeleccionada);
                ActualizarTotal();
            }
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

        private void ConfigurarGridLineas()
        {
            dataGridLineasPedido.AutoGenerateColumns = false;
            dataGridLineasPedido.Columns.Clear();
            dataGridLineasPedido.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            dataGridLineasPedido.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad", Width = 80, ReadOnly = true });
            dataGridLineasPedido.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioUnitario", HeaderText = "Precio Unit.", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "C" }, ReadOnly = true });
            dataGridLineasPedido.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Subtotal", HeaderText = "Subtotal", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "C" }, ReadOnly = true });
        }

        private void ActualizarTotal()
        {
            lblTotalPedido.Text = $"Total: {_lineasPedido.Sum(l => l.Subtotal):C}";
        }

        private void ToggleProductControls(bool enabled)
        {
            cmbProductos.Enabled = enabled;
            numCantidad.Enabled = enabled;
            btnAgregarProducto.Enabled = enabled;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

