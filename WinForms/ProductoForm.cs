using ApiClient;
using DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class ProductoForm : Form
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly IServiceProvider _serviceProvider;
        private List<ProductoDTO> _productosCache = new();
        private string _filtroActual = string.Empty;

        public ProductoForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _productoApiClient = serviceProvider.GetRequiredService<ProductoApiClient>();
            _serviceProvider = serviceProvider;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private async void ProductoForm_Load(object sender, EventArgs e)
        {
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            dgvProductos.ClearSelection();
            await CargarProductos();
            AplicarFiltro();
        }

        private async Task CargarProductos()
        {
            try
            {
                var productos = await _productoApiClient.GetAllAsync() ?? new List<ProductoDTO>();
                _productosCache = productos.ToList();
                dgvProductos.DataSource = _productosCache.ToList();
                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvProductos.Columns.Contains("IdProducto"))
            {
                dgvProductos.Columns["IdProducto"].HeaderText = "Código";
                dgvProductos.Columns["IdProducto"].Width = 80;
            }
            if (dgvProductos.Columns.Contains("Nombre"))
            {
                dgvProductos.Columns["Nombre"].HeaderText = "Nombre";
                dgvProductos.Columns["Nombre"].Width = 200;
            }
            if (dgvProductos.Columns.Contains("Descripcion"))
            {
                dgvProductos.Columns["Descripcion"].HeaderText = "Descripción";
                dgvProductos.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvProductos.Columns.Contains("Stock"))
            {
                dgvProductos.Columns["Stock"].HeaderText = "Stock";
                dgvProductos.Columns["Stock"].Width = 80;
            }
            if (dgvProductos.Columns.Contains("TipoProductoDescripcion"))
            {
                dgvProductos.Columns["TipoProductoDescripcion"].HeaderText = "Tipo de Producto";
                dgvProductos.Columns["TipoProductoDescripcion"].Width = 200;
            }
            if (dgvProductos.Columns.Contains("PrecioActual"))
            {
                dgvProductos.Columns["PrecioActual"].HeaderText = "Precio";
                dgvProductos.Columns["PrecioActual"].DefaultCellStyle.Format = "C2";
                dgvProductos.Columns["PrecioActual"].Width = 100;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            var f = _filtroActual.ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(f))
                dgvProductos.DataSource = _productosCache.ToList();
            else
                dgvProductos.DataSource = _productosCache
                    .Where(p =>
                        (p.Nombre != null && p.Nombre.ToLower().Contains(f)) ||
                        (p.Descripcion != null && p.Descripcion.ToLower().Contains(f)) ||
                        (p.TipoProductoDescripcion != null && p.TipoProductoDescripcion.ToLower().Contains(f)))
                    .ToList();

            ConfigurarColumnas();
        }

        private ProductoDTO? ObtenerSeleccionado()
        {
            var row = dgvProductos.SelectedRows.Count > 0
                ? dgvProductos.SelectedRows[0]
                : dgvProductos.CurrentRow;

            return row?.DataBoundItem as ProductoDTO;
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            var crearForm = _serviceProvider.GetRequiredService<CrearProductoForm>();
            if (crearForm.ShowDialog(this) == DialogResult.OK)
            {
                await CargarProductos();
                AplicarFiltro();
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            var producto = ObtenerSeleccionado();
            if (producto == null) return;

            using var form = new EditarProductoForm(
                producto.IdProducto,
                _productoApiClient,
                _serviceProvider.GetRequiredService<TipoProductoApiClient>()
            );

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await CargarProductos();
                AplicarFiltro();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var producto = ObtenerSeleccionado();
            if (producto == null) return;

            var confirm = MessageBox.Show(
                $"¿Está seguro que desea eliminar el producto \"{producto.Nombre}\"?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                await _productoApiClient.DeleteAsync(producto.IdProducto);
                _productosCache.RemoveAll(p => p.IdProducto == producto.IdProducto);
                AplicarFiltro();

                MessageBox.Show("Producto eliminado exitosamente.",
                    "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error de red: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e) => Close();

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            bool seleccionado = dgvProductos.SelectedRows.Count > 0;
            btnEditar.Visible = seleccionado;
            btnEliminar.Visible = seleccionado;
        }
    }
}
