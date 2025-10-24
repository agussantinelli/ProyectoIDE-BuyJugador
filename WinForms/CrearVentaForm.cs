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
    public partial class CrearVentaForm : BaseForm
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly VentaApiClient _ventaApiClient;
        private readonly UserSessionService _userSessionService;
        private readonly PrecioVentaApiClient _precioVentaApiClient;

        private List<ProductoDTO> _productosDisponibles;
        private BindingList<LineaVentaDTO> _lineasVentaActual;
        private int _nroLineaCounter = 1;

        public CrearVentaForm(ProductoApiClient productoApiClient, VentaApiClient ventaApiClient, UserSessionService userSessionService, PrecioVentaApiClient precioVentaApiClient)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _ventaApiClient = ventaApiClient;
            _userSessionService = userSessionService;
            _precioVentaApiClient = precioVentaApiClient;

            _productosDisponibles = new List<ProductoDTO>();
            _lineasVentaActual = new BindingList<LineaVentaDTO>();

            StyleManager.ApplyDataGridViewStyle(dataGridLineasVenta);
            StyleManager.ApplyButtonStyle(btnAgregarProducto);
            StyleManager.ApplyButtonStyle(btnEliminarLinea);
            StyleManager.ApplyButtonStyle(btnFinalizarVenta);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void CrearVentaForm_Load(object sender, EventArgs e)
        {
            await CargarProductos();
            ConfigurarGrilla();
            dataGridLineasVenta.DataSource = _lineasVentaActual;
            ActualizarTotal();
        }

        private async Task CargarProductos()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                _productosDisponibles = await _productoApiClient.GetAllAsync() ?? new List<ProductoDTO>();
                var productosConStockYPrecio = _productosDisponibles
                    .Where(p => p.Stock > 0 && p.PrecioActual.HasValue && p.PrecioActual > 0)
                    .ToList();

                cmbProductos.DataSource = productosConStockYPrecio;
                cmbProductos.DisplayMember = "Nombre";
                cmbProductos.ValueMember = "IdProducto";
                cmbProductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProductos.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbProductos.SelectedIndex = -1;
                cmbProductos.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbProductos.DataSource = null;
                cmbProductos.Enabled = false;
                btnAgregarProducto.Enabled = false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarGrilla()
        {
            dataGridLineasVenta.AutoGenerateColumns = false;
            dataGridLineasVenta.Columns.Clear();
            dataGridLineasVenta.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            dataGridLineasVenta.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad", Width = 80, ReadOnly = false });
            dataGridLineasVenta.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioUnitario", HeaderText = "Precio Unit.", Width = 120, DefaultCellStyle = { Format = "C2" }, ReadOnly = true });
            dataGridLineasVenta.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Subtotal", HeaderText = "Subtotal", Width = 120, DefaultCellStyle = { Format = "C2" }, ReadOnly = true });
            dataGridLineasVenta.CellEndEdit += DataGridLineasVenta_CellEndEdit;
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem is not ProductoDTO productoSeleccionado)
            {
                MessageBox.Show("Seleccione un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cantidadDeseada = (int)numCantidad.Value;
            if (cantidadDeseada <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!productoSeleccionado.PrecioActual.HasValue || productoSeleccionado.PrecioActual <= 0)
            {
                MessageBox.Show($"El producto '{productoSeleccionado.Nombre}' no tiene un precio de venta válido asignado.", "Precio no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var lineaExistente = _lineasVentaActual.FirstOrDefault(l => l.IdProducto == productoSeleccionado.IdProducto);
            var cantidadYaEnCarro = lineaExistente?.Cantidad ?? 0;

            if (cantidadYaEnCarro + cantidadDeseada > productoSeleccionado.Stock)
            {
                MessageBox.Show($"Stock insuficiente para '{productoSeleccionado.Nombre}'. Stock disponible: {productoSeleccionado.Stock - cantidadYaEnCarro}", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lineaExistente != null)
            {
                lineaExistente.Cantidad += cantidadDeseada;
            }
            else
            {
                _lineasVentaActual.Add(new LineaVentaDTO
                {
                    IdProducto = productoSeleccionado.IdProducto,
                    NombreProducto = productoSeleccionado.Nombre,
                    Cantidad = cantidadDeseada,
                    PrecioUnitario = productoSeleccionado.PrecioActual.Value,
                    NroLineaVenta = _nroLineaCounter++
                });
            }

            _lineasVentaActual.ResetBindings();
            ActualizarTotal();
            cmbProductos.SelectedIndex = -1;
            cmbProductos.Text = "";
            numCantidad.Value = 1;
            cmbProductos.Focus();

        }

        private void DataGridLineasVenta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dataGridLineasVenta.Columns["Cantidad"].Index) return;

            if (dataGridLineasVenta.Rows[e.RowIndex].DataBoundItem is LineaVentaDTO linea)
            {
                var producto = _productosDisponibles.FirstOrDefault(p => p.IdProducto == linea.IdProducto);
                if (producto == null) return;

                var celda = dataGridLineasVenta.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (int.TryParse(celda.Value?.ToString(), out int nuevaCantidad) && nuevaCantidad > 0)
                {
                    if (nuevaCantidad > producto.Stock)
                    {
                        MessageBox.Show($"Stock insuficiente. Máximo disponible: {producto.Stock}", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        celda.Value = linea.Cantidad;
                        dataGridLineasVenta.CancelEdit();
                        return;
                    }
                    if (linea.Cantidad != nuevaCantidad)
                    {
                        linea.Cantidad = nuevaCantidad;
                        _lineasVentaActual.ResetItem(e.RowIndex);
                        ActualizarTotal();
                    }
                }
                else
                {
                    MessageBox.Show("Cantidad inválida. Debe ser un número mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    celda.Value = linea.Cantidad;
                    dataGridLineasVenta.CancelEdit();
                }
            }
        }


        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (dataGridLineasVenta.CurrentRow?.DataBoundItem is LineaVentaDTO lineaSeleccionada)
            {
                _lineasVentaActual.Remove(lineaSeleccionada);
                ActualizarTotal();
            }
            else
            {
                MessageBox.Show("Seleccione una línea para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ActualizarTotal()
        {
            decimal total = _lineasVentaActual.Sum(l => l.Subtotal);
            lblTotalVenta.Text = $"Total: {total:C}";
        }

        private async void btnFinalizarVenta_Click(object sender, EventArgs e)
        {
            if (!_lineasVentaActual.Any())
            {
                MessageBox.Show("Debe agregar al menos un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_userSessionService.CurrentUser == null)
            {
                MessageBox.Show("Error de sesión. No se pudo identificar al vendedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var ventaCompletaDto = new CrearVentaCompletaDTO
            {
                IdPersona = _userSessionService.CurrentUser.IdPersona,
                Lineas = _lineasVentaActual.ToList(),
                Finalizada = chkMarcarFinalizada.Checked
            };

            this.Cursor = Cursors.WaitCursor;
            try
            {
                var response = await _ventaApiClient.CreateCompletaAsync(ventaCompletaDto);
                if (response.IsSuccessStatusCode)
                {
                    var ventaCreada = await response.Content.ReadFromJsonAsync<VentaDTO>();
                    MessageBox.Show($"Venta #{ventaCreada?.IdVenta} creada exitosamente.", "Venta Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al crear la venta: {error}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado al crear la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
