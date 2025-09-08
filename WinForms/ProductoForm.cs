using ApiClient;
using DTOs;
using DominioModelo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class ProductoForm : Form
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;
        private int _selectedProductoId = 0;

        public ProductoForm(ProductoApiClient productoApiClient, TipoProductoApiClient tipoProductoApiClient)
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
                List<Producto>? listaProductos = await _productoApiClient.GetAllAsync();
                if (listaProductos != null)
                {
                    dgvProductos.DataSource = listaProductos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}");
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
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}");
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e) { 
        
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
        
        }

        private void btnEliminar_Click(object sender, EventArgs e) {
        
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                var selectedRow = dgvProductos.SelectedRows[0];
                var producto = selectedRow.DataBoundItem as Producto;

                if (producto != null)
                {
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    cmbTipoProducto.SelectedValue = producto.IdTipoProducto;
                    numStock.Value = producto.Stock;
                }
            }
        }

    }
}
