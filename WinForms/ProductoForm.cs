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

            if (dgvProductos.Columns.Contains("Precios"))
                dgvProductos.Columns["Precios"].Visible = false;
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
            if (producto == null)
            {
                MessageBox.Show("Seleccioná un producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var form = new EditarProductoForm(
                producto.IdProducto,
                _serviceProvider.GetRequiredService<ProductoApiClient>(),
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
            if (producto == null)
            {
                MessageBox.Show("Seleccioná un producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Está seguro que desea eliminar el producto \"{producto.Nombre}\"?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                var ok = await _serviceProvider.GetRequiredService<ProductoApiClient>()
                                               .DeleteAsync(producto.IdProducto);
                if (!ok)
                {
                    MessageBox.Show("No se pudo eliminar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
            bool seleccionado = dgvProductos.SelectedRows.Count > 0 || dgvProductos.CurrentRow != null;
            btnEditar.Visible = seleccionado;
            btnEliminar.Visible = seleccionado;
        }

        private void dgvProductos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvProductos.ClearSelection();
                dgvProductos.Rows[e.RowIndex].Selected = true;
                dgvProductos.CurrentCell = dgvProductos.Rows[e.RowIndex].Cells[Math.Max(0, e.ColumnIndex)];
            }
        }

        private void mnuVerHistorialPrecios_Click(object? sender, EventArgs e) => AbrirHistorialPrecios();
        private void mnuEditarPrecio_Click(object? sender, EventArgs e) => _ = AbrirEditarPrecioAsync();

        private void btnVerHistorialPrecios_Click(object? sender, EventArgs e) => AbrirHistorialPrecios();
        private async void btnEditarPrecio_Click(object? sender, EventArgs e) => await AbrirEditarPrecioAsync();

        private void AbrirHistorialPrecios()
        {
            var producto = ObtenerSeleccionado();
            if (producto == null)
            {
                MessageBox.Show("Seleccioná un producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var hist = new HistorialPreciosForm(
                _serviceProvider.GetRequiredService<PrecioApiClient>(),
                producto.IdProducto,
                producto.Nombre
            );

            hist.ShowDialog(this);
        }

        private async Task AbrirEditarPrecioAsync()
        {
            var producto = ObtenerSeleccionado();
            if (producto == null)
            {
                MessageBox.Show("Seleccioná un producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var edit = new EditarPrecioForm(
                _serviceProvider.GetRequiredService<PrecioApiClient>(),
                producto.IdProducto,
                producto.Nombre,
                producto.PrecioActual
            );

            if (edit.ShowDialog(this) == DialogResult.OK)
            {
                await CargarProductos();
                AplicarFiltro();
            }
        }
    }
}
