using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Producto : Form
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;
        private int? _selectedProductoId = null;

        public Producto(ProductoApiClient productoApiClient, TipoProductoApiClient tipoProductoApiClient)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _tipoProductoApiClient = tipoProductoApiClient;
        }

        private async void ProductoForm_Load(object sender, EventArgs e)
        {
            await CargarTiposProductoComboBox();
            await CargarGrillaProductos();
        }

        private async Task CargarGrillaProductos()
        {
            try
            {
                List<ProductoDTO>? listaProductos = await _productoApiClient.GetAllAsync();
                dgvProductos.DataSource = listaProductos;
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarTiposProductoComboBox()
        {
            try
            {
                List<TipoProductoDTO>? tiposProducto = await _tipoProductoApiClient.GetAllAsync();
                if (tiposProducto != null)
                {
                    cmbTipoProducto.DataSource = tiposProducto;
                    cmbTipoProducto.DisplayMember = "Descripcion"; 
                    cmbTipoProducto.ValueMember = "IdTipoProducto"; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productoDto = new ProductoDTO
            {
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Stock = (int)numStock.Value,
                IdTipoProducto = (int?)cmbTipoProducto.SelectedValue
            };

            try
            {
                if (_selectedProductoId.HasValue)
                {
                    productoDto.IdProducto = _selectedProductoId.Value;
                    bool success = await _productoApiClient.UpdateAsync(_selectedProductoId.Value, productoDto);
                    if (success)
                    {
                        MessageBox.Show("Producto actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    var nuevoProducto = await _productoApiClient.CreateAsync(productoDto);
                    if (nuevoProducto != null)
                    {
                        MessageBox.Show($"Producto '{nuevoProducto.Nombre}' creado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                await CargarGrillaProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!_selectedProductoId.HasValue)
            {
                MessageBox.Show("Por favor, seleccione un producto de la lista para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este producto?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    bool success = await _productoApiClient.DeleteAsync(_selectedProductoId.Value);
                    if (success)
                    {
                        MessageBox.Show("Producto eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarGrillaProductos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                var selectedRow = dgvProductos.SelectedRows[0];
                var producto = selectedRow.DataBoundItem as ProductoDTO;

                if (producto != null)
                {
                    _selectedProductoId = producto.IdProducto;
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    cmbTipoProducto.SelectedValue = producto.IdTipoProducto ?? -1; 
                    numStock.Value = producto.Stock;
                }
            }
        }

        private void LimpiarFormulario()
        {
            _selectedProductoId = null;
            txtNombre.Clear();
            txtDescripcion.Clear();
            numStock.Value = 0;
            cmbTipoProducto.SelectedIndex = -1; 
            dgvProductos.ClearSelection();
        }
    }
}
