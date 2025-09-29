using ApiClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows.Forms;
using DTOs;

namespace WinForms
{
    public partial class ProductoForm : Form
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly IServiceProvider _serviceProvider;

        public ProductoForm(ProductoApiClient productoApiClient, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _serviceProvider = serviceProvider;
            dgvProductos.AutoGenerateColumns = false;
        }

        private async void ProductoForm_Load(object sender, EventArgs e)
        {
            await LoadProductos();
        }

        private async Task LoadProductos()
        {
            var productos = await _productoApiClient.GetAllAsync();
            dgvProductos.DataSource = productos;
            dgvProductos.Refresh();
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            var crearForm = _serviceProvider.GetRequiredService<CrearProductoForm>();
            crearForm.ShowDialog();
            await LoadProductos();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                var productoSeleccionado = (ProductoDTO)dgvProductos.SelectedRows[0].DataBoundItem;
                var editarForm = new EditarProductoForm(productoSeleccionado.IdProducto, _productoApiClient, _serviceProvider.GetRequiredService<TipoProductoApiClient>());
                editarForm.ShowDialog();
                await LoadProductos();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                var productoSeleccionado = (ProductoDTO)dgvProductos.SelectedRows[0].DataBoundItem;
                var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar el producto '{productoSeleccionado.Nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    await _productoApiClient.DeleteAsync(productoSeleccionado.IdProducto);
                    await LoadProductos();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
