using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class CrearVentaForm : Form
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly VentaApiClient _ventaApiClient;
        private readonly LineaVentaApiClient _lineaVentaApiClient;
        private readonly UserSessionService _userSessionService;

        private List<ProductoDTO> _productosDisponibles;
        private BindingList<LineaVentaDTO> _lineasVentaActual;
        private int _nroLineaCounter = 1;

        public CrearVentaForm(ProductoApiClient productoApiClient, VentaApiClient ventaApiClient, LineaVentaApiClient lineaVentaApiClient, UserSessionService userSessionService)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _ventaApiClient = ventaApiClient;
            _lineaVentaApiClient = lineaVentaApiClient;
            _userSessionService = userSessionService;

            _productosDisponibles = new List<ProductoDTO>();
            _lineasVentaActual = new BindingList<LineaVentaDTO>();
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
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrilla()
        {
            dataGridLineasVenta.AutoGenerateColumns = false;
            dataGridLineasVenta.Columns.Clear();
            dataGridLineasVenta.Columns.Add(new DataGridViewTextBoxColumn { Name = "Producto", DataPropertyName = "NombreProducto", HeaderText = "Producto", ReadOnly = true });
            dataGridLineasVenta.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", DataPropertyName = "Cantidad", HeaderText = "Cantidad", ReadOnly = true });
            dataGridLineasVenta.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subtotal", DataPropertyName = "Subtotal", HeaderText = "Subtotal", ReadOnly = true, DefaultCellStyle = { Format = "C" } });
            dataGridLineasVenta.Columns["Producto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem is not ProductoDTO productoSeleccionado)
            {
                MessageBox.Show("Por favor, seleccione un producto válido de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cantidadDeseada = (int)numCantidad.Value;
            var lineaExistente = _lineasVentaActual.FirstOrDefault(l => l.IdProducto == productoSeleccionado.IdProducto);
            var cantidadYaEnCarro = lineaExistente?.Cantidad ?? 0;

            // --- MEJORA: Validar contra el stock disponible ---
            if (cantidadYaEnCarro + cantidadDeseada > productoSeleccionado.Stock)
            {
                MessageBox.Show($"Stock insuficiente para '{productoSeleccionado.Nombre}'.\nStock disponible: {productoSeleccionado.Stock}\nCantidad en venta actual: {cantidadYaEnCarro}", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lineaExistente != null)
            {
                lineaExistente.Cantidad += cantidadDeseada;
                lineaExistente.Subtotal = lineaExistente.Cantidad * productoSeleccionado.PrecioActual;
                _lineasVentaActual.ResetBindings();
            }
            else
            {
                var nuevaLinea = new LineaVentaDTO
                {
                    IdProducto = productoSeleccionado.IdProducto,
                    NombreProducto = productoSeleccionado.Nombre,
                    Cantidad = cantidadDeseada,
                    Subtotal = productoSeleccionado.PrecioActual * (decimal)cantidadDeseada,
                    NroLineaVenta = _nroLineaCounter++
                };
                _lineasVentaActual.Add(nuevaLinea);
            }

            ActualizarTotal();
            cmbProductos.SelectedIndex = -1;
            cmbProductos.Text = "";
            numCantidad.Value = 1;
            cmbProductos.Focus();
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
                MessageBox.Show("Seleccione una línea de la venta para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ActualizarTotal()
        {
            decimal total = _lineasVentaActual.Sum(l => l.Subtotal);
            lblTotalVenta.Text = $"Total: {total:C}";
        }

        private async void btnFinalizarVenta_Click(object sender, EventArgs e)
        {
            if (_lineasVentaActual.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto a la venta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_userSessionService.CurrentUser == null)
            {
                MessageBox.Show("Error fatal: No se pudo identificar al vendedor.", "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- MEJORA: Usar el nuevo endpoint transaccional ---
            var ventaCompletaDto = new CrearVentaCompletaDTO
            {
                IdPersona = _userSessionService.CurrentUser.IdPersona,
                Lineas = _lineasVentaActual.ToList()
            };

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
                    var errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al crear la venta: {errorContent}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al finalizar la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

