using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using ApiClient; // Para ProductoApiClient y TipoProductoApiClient
using DTOs;     // Para las clases Producto y TipoProducto
using DominioModelo;

namespace WinForms
{
    public partial class ProductoForm : Form
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;
        private int _selectedProductoId = 0;

        public ProductoForm()
        {
            InitializeComponent();
            _productoApiClient = new ProductoApiClient();
            _tipoProductoApiClient = new TipoProductoApiClient(new HttpClient()); // Pasa una instancia de HttpClient
        }

        private async void ProductoForm_Load(object sender, EventArgs e)
        {
            await CargarTiposProductoComboBox();
            await CargarGrillaProductos();
        }

        #region Carga de Datos

        private async Task CargarGrillaProductos()
        {
            try
            {
                List<Producto> listaProductos = await _productoApiClient.GetProductosAsync();
                dgvProductos.DataSource = listaProductos;
                ConfigurarColumnasGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarTiposProductoComboBox()
        {
            try
            {
                // Obtener la lista de DTOs desde la API
                var tiposDto = await _tipoProductoApiClient.GetAllAsync();

                // Manejar el posible valor nulo (CS8600)
                if (tiposDto == null)
                {
                    cmbTipoProducto.DataSource = null;
                    return;
                }

                // Mapear los DTOs a un tipo anónimo para el ComboBox (o crea una clase si lo prefieres)
                var tipos = tiposDto.Select(dto => new
                {
                    IdTipoProducto = dto.IdTipoProducto,
                    Descripcion = dto.Descripcion
                }).ToList();

                cmbTipoProducto.DataSource = tipos;
                cmbTipoProducto.DisplayMember = "Descripcion";
                cmbTipoProducto.ValueMember = "IdTipoProducto";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ConfigurarColumnasGrilla()
        {
            // Ocultar columnas innecesarias o configurar títulos
            if (dgvProductos.Columns["IdProducto"] != null)
                dgvProductos.Columns["IdProducto"].HeaderText = "ID";

            if (dgvProductos.Columns["IdTipoProducto"] != null)
                dgvProductos.Columns["IdTipoProducto"].Visible = false;

            // Ocultar propiedades de navegación si existen en el DTO
            if (dgvProductos.Columns["TipoProducto"] != null)
                dgvProductos.Columns["TipoProducto"].Visible = false;
        }

        #endregion

        #region Lógica de Controles y CRUD

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Validación de entradas
            if (!ValidarEntradas()) return;

            // 2. Crear objeto Producto desde el formulario
            Producto producto = new Producto
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                //Precio = numPrecio.Value,
                Stock = (int)numStock.Value,
                IdTipoProducto = (int)cmbTipoProducto.SelectedValue
            };

            try
            {
                bool resultado;
                if (_selectedProductoId == 0)
                {
                    // Crear nuevo producto (CREATE)
                    resultado = await _productoApiClient.CreateProductoAsync(producto);
                    if (resultado)
                        MessageBox.Show("Producto creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Actualizar producto existente (UPDATE)
                    producto.IdProducto = _selectedProductoId;
                    resultado = await _productoApiClient.UpdateProductoAsync(_selectedProductoId, producto);
                    if (resultado)
                        MessageBox.Show("Producto actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (resultado)
                {
                    await CargarGrillaProductos();
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("No se pudo completar la operación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el producto: {ex.Message}", "Error Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_selectedProductoId == 0)
            {
                MessageBox.Show("Debe seleccionar un producto de la grilla.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este producto?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    bool resultado = await _productoApiClient.DeleteProductoAsync(_selectedProductoId);
                    if (resultado)
                    {
                        MessageBox.Show("Producto eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarGrillaProductos();
                        LimpiarFormulario();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el producto: {ex.Message}", "Error Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                try
                {
                    // Obtener el objeto Producto directamente del DataBoundItem
                    Producto productoSeleccionado = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
                    PoblarFormulario(productoSeleccionado);
                }
                catch (Exception ex)
                {
                     MessageBox.Show($"Error al seleccionar producto: {ex.Message}");
                }
            }
        }

        #endregion

        #region Métodos Auxiliares

        private void LimpiarFormulario()
        {
            _selectedProductoId = 0;
            txtNombre.Clear();
            txtDescripcion.Clear();
            //numPrecio.Value = 0;
            numStock.Value = 0;
            cmbTipoProducto.SelectedIndex = -1;
            txtNombre.Focus();
        }

        private void PoblarFormulario(Producto producto)
        {
            _selectedProductoId = producto.IdProducto;
            txtNombre.Text = producto.Nombre;
            txtDescripcion.Text = producto.Descripcion;
            //numPrecio.Value = producto.Precio;
            numStock.Value = producto.Stock;
            cmbTipoProducto.SelectedValue = producto.IdTipoProducto;
        }

        private bool ValidarEntradas()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }
            if (cmbTipoProducto.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un Tipo de Producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoProducto.Focus();
                return false;
            }
            //if (numPrecio.Value <= 0)
            //{
            //     MessageBox.Show("El precio debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     numPrecio.Focus();
            //     return false;
            //}
            return true;
        }

        #endregion
    }
}