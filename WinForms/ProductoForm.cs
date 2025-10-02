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
    public partial class ProductoForm : BaseForm
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

            // Aplicar estilos
            StyleManager.ApplyDataGridViewStyle(dgvProductos);
            StyleManager.ApplyButtonStyle(btnNuevo);
            StyleManager.ApplyButtonStyle(btnEditar);
            StyleManager.ApplyButtonStyle(btnEliminar);
            StyleManager.ApplyButtonStyle(btnVolver);
            StyleManager.ApplyButtonStyle(btnVerHistorialPrecios);
            StyleManager.ApplyButtonStyle(btnEditarPrecio);
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
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error de red: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            dgvProductos.Columns.Clear();
            dgvProductos.AutoGenerateColumns = false;

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IdProducto", HeaderText = "ID", Width = 50 });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Nombre", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Descripcion", HeaderText = "Descripción", Width = 250 });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Stock", HeaderText = "Stock", Width = 70 });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TipoProductoNombre", HeaderText = "Tipo", Width = 150 });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioActual", HeaderText = "Precio", Width = 100, DefaultCellStyle = { Format = "C2" } });
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            ConfigurarColumnas();

            var f = _filtroActual.ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(f))
            {
                dgvProductos.DataSource = _productosCache.ToList();
            }
            else
            {
                dgvProductos.DataSource = _productosCache
                    .Where(p => (p.Nombre != null && p.Nombre.ToLower().Contains(f)) ||
                                (p.Descripcion != null && p.Descripcion.ToLower().Contains(f)))
                    .ToList();
            }
        }

        private ProductoDTO? ObtenerSeleccionado()
        {
            return dgvProductos.SelectedRows.Count > 0
                ? dgvProductos.SelectedRows[0].DataBoundItem as ProductoDTO
                : null;
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<CrearProductoForm>();
            form.ShowDialog(this);
            await CargarProductos();
            AplicarFiltro();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            var producto = ObtenerSeleccionado();
            if (producto == null) return;

            using var form = new EditarProductoForm(producto.IdProducto, _productoApiClient, _serviceProvider.GetRequiredService<TipoProductoApiClient>());
            form.ShowDialog(this);
            await CargarProductos();
            AplicarFiltro();
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
                await CargarProductos();
                AplicarFiltro();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                MessageBox.Show("No se puede eliminar el producto porque está siendo utilizado en ventas.", "Conflicto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e) => Close();

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            bool seleccionado = dgvProductos.SelectedRows.Count > 0;
            btnEditar.Visible = seleccionado;
            btnEliminar.Visible = seleccionado;
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                AbrirHistorialPrecios();
        }

        private void dgvProductos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvProductos.ClearSelection();
                dgvProductos.Rows[e.RowIndex].Selected = true;
                cmOpciones.Show(dgvProductos, e.Location);
            }
        }

        private void mnuVerHistorialPrecios_Click(object sender, EventArgs e) => AbrirHistorialPrecios();

        private async void mnuEditarPrecio_Click(object sender, EventArgs e) => await AbrirEditarPrecioAsync();

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

