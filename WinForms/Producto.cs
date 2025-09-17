using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Producto : Form
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;
        private int _selectedProductoId = 0;
        private bool _isClearingSelection = false;

        public Producto(ProductoApiClient productoApiClient, TipoProductoApiClient tipoProductoApiClient)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _tipoProductoApiClient = tipoProductoApiClient;
        }

        private async void Producto_Load(object sender, EventArgs e)
        {
            await CargarProductos();
            await CargarComboTiposProducto();
            LimpiarFormulario();
        }

        private async Task CargarComboTiposProducto()
        {
            try
            {
                var tiposProducto = await _tipoProductoApiClient.GetAllAsync();
                if (tiposProducto != null)
                {
                    cmbTipoProducto.DataSource = tiposProducto;
                    cmbTipoProducto.DisplayMember = "Descripcion";
                    cmbTipoProducto.ValueMember = "IdTipoProducto";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarProductos()
        {
            try
            {
                var productos = await _productoApiClient.GetAllAsync();
                var tiposProducto = await _tipoProductoApiClient.GetAllAsync();

                if (productos != null && tiposProducto != null)
                {
                    // Join manual para mostrar descripción del tipo
                    foreach (var p in productos)
                    {
                        var tipo = tiposProducto.FirstOrDefault(t => t.IdTipoProducto == p.IdTipoProducto);
                        p.TipoProductoDescripcion = tipo?.Descripcion ?? "";
                    }

                    dgvProductos.DataSource = productos;

                    // Configuración de columnas
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

                    // Ocultamos el ID del tipo de producto
                    if (dgvProductos.Columns.Contains("IdTipoProducto"))
                    {
                        dgvProductos.Columns["IdTipoProducto"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (_isClearingSelection) return;

            if (dgvProductos.SelectedRows.Count > 0)
            {
                var selectedRow = dgvProductos.SelectedRows[0];
                var producto = selectedRow.DataBoundItem as ProductoDTO;

                if (producto != null)
                {
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    numStock.Value = producto.Stock;
                    cmbTipoProducto.SelectedValue = producto.IdTipoProducto;
                    _selectedProductoId = producto.IdProducto;
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedProductoId == 0)
                {
                    MessageBox.Show("Seleccione un producto para actualizar.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new ProductoDTO
                {
                    IdProducto = _selectedProductoId,
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Stock = (int)numStock.Value,
                    IdTipoProducto = (int)cmbTipoProducto.SelectedValue
                };

                await _productoApiClient.UpdateAsync(_selectedProductoId, dto);

                MessageBox.Show("Producto actualizado exitosamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await CargarProductos();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar producto: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedProductoId == 0)
                {
                    MessageBox.Show("Seleccione un producto para eliminar.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show(
                    "¿Estás seguro que deseas eliminar este producto?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmResult == DialogResult.Yes)
                {
                    await _productoApiClient.DeleteAsync(_selectedProductoId);
                    MessageBox.Show("Producto eliminado exitosamente.",
                                    "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await CargarProductos();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            _isClearingSelection = true;

            txtNombre.Clear();
            txtDescripcion.Clear();
            numStock.Value = 0;
            if (cmbTipoProducto.Items.Count > 0)
                cmbTipoProducto.SelectedIndex = 0;

            _selectedProductoId = 0;
            dgvProductos.ClearSelection();

            _isClearingSelection = false;

            txtNombre.Focus();
        }
    }
}
