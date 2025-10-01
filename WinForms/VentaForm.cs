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
    public partial class VentaForm : Form
    {
        private readonly VentaApiClient _ventaApiClient;
        private readonly LineaVentaApiClient _lineaVentaApiClient;
        private readonly PersonaApiClient _personaApiClient;
        private readonly ProductoApiClient _productoApiClient;

        private List<VentaDTO> _ventasCache = new();
        private Dictionary<int, string> _vendedoresLookup = new();
        private Dictionary<int, ProductoDTO> _productosLookup = new();

        public VentaForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _ventaApiClient = serviceProvider.GetRequiredService<VentaApiClient>();
            _lineaVentaApiClient = serviceProvider.GetRequiredService<LineaVentaApiClient>();
            _personaApiClient = serviceProvider.GetRequiredService<PersonaApiClient>();
            _productoApiClient = serviceProvider.GetRequiredService<ProductoApiClient>();

            PrepararGridVentas(dgvVentas);
            PrepararGridLineasVenta(dgvLineasVenta);
        }

        private void PrepararGridVentas(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IdVenta", HeaderText = "ID Venta", Name = "IdVenta" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Fecha", HeaderText = "Fecha", Name = "Fecha", DefaultCellStyle = new DataGridViewCellStyle { Format = "g" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreVendedor", HeaderText = "Vendedor", Name = "NombreVendedor", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Estado", HeaderText = "Estado", Name = "Estado" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Total", HeaderText = "Total", Name = "Total", DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
        }

        private void PrepararGridLineasVenta(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto", Name = "NombreProducto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad", Name = "Cantidad" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Subtotal", HeaderText = "Subtotal", Name = "Subtotal", DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
        }

        private async void VentaForm_Load(object sender, EventArgs e)
        {
            await CargarDatos();
        }

        private async Task CargarDatos()
        {
            try
            {
                // 1. Cargar datos de referencia en paralelo para eficiencia.
                var personalTask = _personaApiClient.GetAllAsync();
                var productosTask = _productoApiClient.GetAllAsync();
                await Task.WhenAll(personalTask, productosTask);

                // 2. Poblar los diccionarios de búsqueda (lookups).
                var personal = await personalTask ?? new List<PersonaDTO>();
                // CORRECCIÓN: Usar la propiedad correcta 'NombreCompleto' del DTO.
                _vendedoresLookup = personal.ToDictionary(p => p.IdPersona, p => p.NombreCompleto);

                var productos = await productosTask ?? new List<ProductoDTO>();
                _productosLookup = productos.ToDictionary(p => p.IdProducto);

                // 3. Cargar ventas y enriquecerlas.
                var ventas = await _ventaApiClient.GetAllAsync() ?? new List<VentaDTO>();

                foreach (var venta in ventas)
                {
                    venta.NombreVendedor = (venta.IdPersona.HasValue && _vendedoresLookup.TryGetValue(venta.IdPersona.Value, out var nombreVendedor))
                        ? nombreVendedor
                        : "Vendedor Desconocido";

                    // CORRECCIÓN: Llamar al método que devuelve una LISTA de líneas.
                    var lineas = await _lineaVentaApiClient.GetByIdAsync(venta.IdVenta);
                    decimal totalVenta = 0;
                    if (lineas != null)
                    {
                        foreach (var linea in lineas)
                        {
                            // CORRECCIÓN: Usar 'PrecioActual' del ProductoDTO.
                            if (linea.IdProducto.HasValue && _productosLookup.TryGetValue(linea.IdProducto.Value, out var producto))
                            {
                                totalVenta += linea.Cantidad * producto.PrecioActual;
                            }
                        }
                    }
                    venta.Total = totalVenta;
                }

                _ventasCache = ventas;
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltro()
        {
            var filtro = txtBuscar.Text.Trim().ToLower();
            var datosFiltrados = _ventasCache
                .Where(v => v.NombreVendedor.ToLower().Contains(filtro))
                .OrderByDescending(v => v.Fecha)
                .ToList();

            dgvVentas.DataSource = datosFiltrados;
            dgvVentas.ClearSelection();
            dgvLineasVenta.DataSource = null;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro();

        private VentaDTO ObtenerVentaSeleccionada()
        {
            return dgvVentas.SelectedRows.Count > 0 && dgvVentas.SelectedRows[0].DataBoundItem is VentaDTO venta ? venta : null;
        }

        private async void dgvVentas_SelectionChanged(object sender, EventArgs e)
        {
            var ventaSeleccionada = ObtenerVentaSeleccionada();
            if (ventaSeleccionada == null)
            {
                dgvLineasVenta.DataSource = null;
                return;
            }

            try
            {
                // CORRECCIÓN: Llamar al método que devuelve una LISTA de líneas.
                var lineas = await _lineaVentaApiClient.GetByIdAsync(ventaSeleccionada.IdVenta);
                if (lineas != null)
                {
                    foreach (var linea in lineas)
                    {
                        if (linea.IdProducto.HasValue && _productosLookup.TryGetValue(linea.IdProducto.Value, out var producto))
                        {
                            // CORRECCIÓN: Usar las propiedades correctas 'Nombre' y 'PrecioActual'.
                            linea.NombreProducto = producto.Nombre;
                            linea.Subtotal = linea.Cantidad * producto.PrecioActual;
                        }
                    }
                }
                dgvLineasVenta.DataSource = lineas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el detalle de la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvLineasVenta.DataSource = null;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e) => this.Close();
    }
}

