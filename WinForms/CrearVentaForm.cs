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


            this.StartPosition = FormStartPosition.CenterScreen;


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
        }


        private async Task CargarProductos()
        {
            try
            {
                _productosDisponibles = await _productoApiClient.GetAllAsync() ?? new List<ProductoDTO>();
                cmbProductos.DataSource = _productosDisponibles;
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


        private async void btnAgregarProducto_Click(object sender, EventArgs e)
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


            try
            {
                var precioVenta = await _precioVentaApiClient.GetPrecioVigenteAsync(productoSeleccionado.IdProducto);
                if (precioVenta == null)
                {
                    MessageBox.Show($"El producto '{productoSeleccionado.Nombre}' no tiene un precio de venta asignado.", "Precio no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                var lineaExistente = _lineasVentaActual.FirstOrDefault(l => l.IdProducto == productoSeleccionado.IdProducto);
                var cantidadYaEnCarro = lineaExistente?.Cantidad ?? 0;


                if (cantidadYaEnCarro + cantidadDeseada > productoSeleccionado.Stock)
                {
                    MessageBox.Show($"Stock insuficiente para '{productoSeleccionado.Nombre}'.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        PrecioUnitario = precioVenta.Monto,
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar el precio: {ex.Message}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGridLineasVenta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != dataGridLineasVenta.Columns["Cantidad"].Index) return;


            if (dataGridLineasVenta.Rows[e.RowIndex].DataBoundItem is LineaVentaDTO linea)
            {
                var producto = _productosDisponibles.FirstOrDefault(p => p.IdProducto == linea.IdProducto);
                if (producto == null) return;

                var celda = dataGridLineasVenta.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (int.TryParse(celda.Value?.ToString(), out int nuevaCantidad) && nuevaCantidad > 0)
                {
                    if (nuevaCantidad > producto.Stock)
                    {
                        MessageBox.Show($"Stock insuficiente. Máximo: {producto.Stock}", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        celda.Value = linea.Cantidad;
                        return;
                    }
                    linea.Cantidad = nuevaCantidad;
                }
                else
                {
                    MessageBox.Show("Cantidad inválida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    celda.Value = linea.Cantidad;
                }
                _lineasVentaActual.ResetBindings();
                ActualizarTotal();
            }
        }


        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (dataGridLineasVenta.CurrentRow?.DataBoundItem is LineaVentaDTO lineaSeleccionada)
            {
                _lineasVentaActual.Remove(lineaSeleccionada);
                ActualizarTotal();
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
                MessageBox.Show("Error de sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            var ventaCompletaDto = new CrearVentaCompletaDTO
            {
                IdPersona = _userSessionService.CurrentUser.IdPersona,
                Lineas = _lineasVentaActual.ToList(),
                Finalizada = chkMarcarFinalizada.Checked
            };


            try
            {
                var response = await _ventaApiClient.CreateCompletaAsync(ventaCompletaDto);
                var ventaCreada = await response.Content.ReadFromJsonAsync<VentaDTO>();
                MessageBox.Show($"Venta #{ventaCreada?.IdVenta} creada.", "Venta Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la venta: {ex.Message}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();
    }
}

