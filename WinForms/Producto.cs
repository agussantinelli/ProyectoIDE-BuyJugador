using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Producto : Form
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;

        private List<ProductoDTO> _productosCache = new();
        private string _filtroActual = string.Empty;

        public Producto(ProductoApiClient productoApiClient, TipoProductoApiClient tipoProductoApiClient)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _tipoProductoApiClient = tipoProductoApiClient;
        }

        // LOAD
        private async void ProductoForm_Load(object? sender, EventArgs e)
        {
            await CargarProductos();
            AplicarFiltro(); // por si hay texto en el buscador al abrir
        }

        // CARGA DATOS
        private async Task CargarProductos()
        {
            try
            {
                var productos = await _productoApiClient.GetAllAsync() ?? new List<ProductoDTO>();
                var tipos = await _tipoProductoApiClient.GetAllAsync() ?? new List<TipoProductoDTO>();

                // join para mostrar descripción del tipo
                var mapTipos = tipos.ToDictionary(t => t.IdTipoProducto, t => t.Descripcion);
                foreach (var p in productos)
                    p.TipoProductoDescripcion = mapTipos.TryGetValue((int)p.IdTipoProducto, out var d) ? d : string.Empty;

                _productosCache = productos.ToList();

                // bind base (sin filtro); el filtro lo aplica AplicarFiltro()
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
                dgvProductos.Columns["Nombre"].Width = 150;
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
                dgvProductos.Columns["TipoProductoDescripcion"].Width = 150;
            }

            if (dgvProductos.Columns.Contains("IdTipoProducto"))
                dgvProductos.Columns["IdTipoProducto"].Visible = false;
        }

        // BUSCADOR
        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            var f = (_filtroActual ?? string.Empty).ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(f))
            {
                dgvProductos.DataSource = _productosCache.ToList();
            }
            else
            {
                dgvProductos.DataSource = _productosCache
                    .Where(p =>
                        (!string.IsNullOrEmpty(p.Nombre) && p.Nombre.ToLower().Contains(f)) ||
                        (!string.IsNullOrEmpty(p.Descripcion) && p.Descripcion.ToLower().Contains(f)) ||
                        (!string.IsNullOrEmpty(p.TipoProductoDescripcion) && p.TipoProductoDescripcion.ToLower().Contains(f))
                    )
                    .ToList();
            }

            ConfigurarColumnas();
        }

        private ProductoDTO? ObtenerProductoSeleccionado()
        {
            // Prioridad a SelectedRows; si no, CurrentRow
            var row = dgvProductos.SelectedRows.Count > 0
                ? dgvProductos.SelectedRows[0]
                : dgvProductos.CurrentRow;

            return row?.DataBoundItem as ProductoDTO;
        }

        // NUEVO
        private async void btnNuevo_Click(object? sender, EventArgs e)
        {
            using var form = new CrearProductoForm(_productoApiClient, _tipoProductoApiClient);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await CargarProductos();
                AplicarFiltro();
            }
        }

        // EDITAR
        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            var producto = ObtenerProductoSeleccionado();
            if (producto == null)
            {
                MessageBox.Show("Seleccione un producto para editar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = new EditarProductoForm(_productoApiClient, _tipoProductoApiClient, producto);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await CargarProductos();
                AplicarFiltro();
            }
        }

        // ELIMINAR (corregido y robusto)
        private async void btnEliminar_Click(object? sender, EventArgs e)
        {
            var producto = ObtenerProductoSeleccionado();
            if (producto == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (producto.IdProducto <= 0)
            {
                MessageBox.Show("El producto seleccionado no es válido.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Está seguro que desea eliminar el producto \"{producto.Nombre}\" (Código {producto.IdProducto})?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                await _productoApiClient.DeleteAsync(producto.IdProducto);

                // Actualizamos cache local sin recargar todo para mejor UX
                _productosCache.RemoveAll(p => p.IdProducto == producto.IdProducto);

                // Reaplicamos filtro y columnas
                AplicarFiltro();

                MessageBox.Show("Producto eliminado exitosamente.",
                    "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"No se pudo contactar al servidor: {httpEx.Message}",
                    "Error de red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException invEx)
            {
                // Por si el backend lanza esto cuando hay relaciones/constraints
                MessageBox.Show(invEx.Message, "No se puede eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // VOLVER
        private void btnVolver_Click(object? sender, EventArgs e)
        {
            Close(); // vuelve al formulario que lo abrió (MainForm)
        }
    }
}
