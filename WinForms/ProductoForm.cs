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
    public partial class ProductoForm : BaseForm
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly IServiceProvider _serviceProvider;

        private List<ProductoDTO> _productosActivos = new();
        private List<ProductoDTO> _productosInactivos = new();

        private string _filtroActual = string.Empty;

        public ProductoForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _productoApiClient = serviceProvider.GetRequiredService<ProductoApiClient>();
            _serviceProvider = serviceProvider;

            StyleManager.ApplyDataGridViewStyle(dgvActivos);
            StyleManager.ApplyDataGridViewStyle(dgvInactivos);
            StyleManager.ApplyButtonStyle(btnNuevo);
            StyleManager.ApplyButtonStyle(btnEditar);
            StyleManager.ApplyButtonStyle(btnDarBaja);
            StyleManager.ApplyButtonStyle(btnReactivar);
            StyleManager.ApplyButtonStyle(btnVerHistorialPrecios);
            StyleManager.ApplyButtonStyle(btnEditarPrecio);
            StyleManager.ApplyButtonStyle(btnVolver);
        }

        private async void ProductoForm_Load(object sender, EventArgs e)
        {
            await CargarTodosProductos();
            AplicarFiltro();
        }

        private async Task CargarTodosProductos()
        {
            try
            {
                var activos = await _productoApiClient.GetAllAsync() ?? new List<ProductoDTO>();
                var inactivos = await _productoApiClient.GetAllInactivosAsync() ?? new List<ProductoDTO>();

                _productosActivos = activos;
                _productosInactivos = inactivos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _filtroActual = txtBuscar.Text?.Trim() ?? string.Empty;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string f = _filtroActual.ToLowerInvariant();

            if (tabControl.SelectedTab == tabActivos)
            {
                var lista = _productosActivos
                    .Where(p => string.IsNullOrWhiteSpace(f)
                                || (p.Nombre != null && p.Nombre.ToLower().Contains(f))
                                || (p.Descripcion != null && p.Descripcion.ToLower().Contains(f)))
                    .ToList();

                ConfigurarColumnas(dgvActivos);
                dgvActivos.DataSource = lista;
            }
            else
            {
                var lista = _productosInactivos
                    .Where(p => string.IsNullOrWhiteSpace(f)
                                || (p.Nombre != null && p.Nombre.ToLower().Contains(f))
                                || (p.Descripcion != null && p.Descripcion.ToLower().Contains(f)))
                    .ToList();

                ConfigurarColumnas(dgvInactivos);
                dgvInactivos.DataSource = lista;
            }
        }

        private void ConfigurarColumnas(DataGridView dgv)
        {
            dgv.Columns.Clear();
            dgv.AutoGenerateColumns = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IdProducto", HeaderText = "ID", Width = 50 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Nombre", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Descripcion", HeaderText = "Descripción", Width = 250 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Stock", HeaderText = "Stock", Width = 70 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TipoProductoDescripcion", HeaderText = "Tipo", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioActual", HeaderText = "Precio", Width = 100, DefaultCellStyle = { Format = "C2" } });
        }

        private ProductoDTO? ObtenerSeleccionado(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
                return dgv.SelectedRows[0].DataBoundItem as ProductoDTO;
            return null;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            // Activar/desactivar botones según la selección
            bool seleccionadoActivos = dgvActivos.SelectedRows.Count > 0;
            bool seleccionadoInactivos = dgvInactivos.SelectedRows.Count > 0;

            btnEditar.Enabled = (tabControl.SelectedTab == tabActivos) && seleccionadoActivos;
            btnDarBaja.Enabled = (tabControl.SelectedTab == tabActivos) && seleccionadoActivos;
            btnReactivar.Enabled = (tabControl.SelectedTab == tabInactivos) && seleccionadoInactivos;

            btnVerHistorialPrecios.Enabled = (tabControl.SelectedTab == tabActivos && seleccionadoActivos)
                                             || (tabControl.SelectedTab == tabInactivos && seleccionadoInactivos);
            btnEditarPrecio.Enabled = (tabControl.SelectedTab == tabActivos && seleccionadoActivos)
                                       || (tabControl.SelectedTab == tabInactivos && seleccionadoInactivos);
        }

        private void dgvProductos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                DataGridView dgv = (tabControl.SelectedTab == tabActivos) ? dgvActivos : dgvInactivos;
                dgv.ClearSelection();
                dgv.Rows[e.RowIndex].Selected = true;
                cmOpciones.Show(dgv, e.Location);
            }
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using var form = _serviceProvider.GetRequiredService<CrearProductoForm>();
            form.ShowDialog(this);
            await CargarTodosProductos();
            AplicarFiltro();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab != tabActivos) return;

            var producto = ObtenerSeleccionado(dgvActivos);
            if (producto == null) return;

            using var form = new EditarProductoForm(producto.IdProducto, _productoApiClient, _serviceProvider.GetRequiredService<TipoProductoApiClient>());
            form.ShowDialog(this);
            await CargarTodosProductos();
            AplicarFiltro();
        }

        private async void btnDarBaja_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab != tabActivos) return;

            var producto = ObtenerSeleccionado(dgvActivos);
            if (producto == null) return;

            if (MessageBox.Show($"¿Dar de baja \"{producto.Nombre}\"?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            await _productoApiClient.DeleteAsync(producto.IdProducto);
            await CargarTodosProductos();
            AplicarFiltro();
        }

        private async void btnReactivar_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab != tabInactivos) return;

            var producto = ObtenerSeleccionado(dgvInactivos);
            if (producto == null) return;

            if (MessageBox.Show($"¿Reactivar \"{producto.Nombre}\"?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            await _productoApiClient.ReactivarAsync(producto.IdProducto);
            await CargarTodosProductos();
            AplicarFiltro();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnVerHistorialPrecios_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (tabControl.SelectedTab == tabActivos) ? dgvActivos : dgvInactivos;
            var producto = ObtenerSeleccionado(dgv);
            if (producto == null) return;

            using var hist = new HistorialPreciosForm(
                _serviceProvider.GetRequiredService<PrecioApiClient>(),
                producto.IdProducto,
                producto.Nombre);
            hist.ShowDialog(this);
        }

        private async void btnEditarPrecio_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (tabControl.SelectedTab == tabActivos) ? dgvActivos : dgvInactivos;
            var producto = ObtenerSeleccionado(dgv);
            if (producto == null) return;

            using var edit = new EditarPrecioForm(
                _serviceProvider.GetRequiredService<PrecioApiClient>(),
                producto.IdProducto,
                producto.Nombre,
                producto.PrecioActual);

            if (edit.ShowDialog(this) == DialogResult.OK)
            {
                await CargarTodosProductos();
                AplicarFiltro();
            }
        }

        private void mnuVerHistorialPrecios_Click(object sender, EventArgs e)
        {
            btnVerHistorialPrecios_Click(sender, e);
        }

        private async void mnuEditarPrecio_Click(object sender, EventArgs e)
        {
            btnEditarPrecio_Click(sender, e);
        }
    }
}
