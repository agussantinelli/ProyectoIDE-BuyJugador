using System;
using System.Windows.Forms;
using DTOs;
using ApiClient;

namespace WinForms
{
    public partial class CrearProductoForm : Form
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;

        public CrearProductoForm(ProductoApiClient productoApiClient, TipoProductoApiClient tipoProductoApiClient)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _tipoProductoApiClient = tipoProductoApiClient;
        }

        private async void CrearProductoForm_Load(object sender, EventArgs e)
        {
            var tipos = await _tipoProductoApiClient.GetAllAsync();
            cmbTipoProducto.DataSource = tipos;
            cmbTipoProducto.DisplayMember = "Descripcion";
            cmbTipoProducto.ValueMember = "IdTipoProducto";
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var dto = new ProductoDTO
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Stock = (int)numStock.Value,
                IdTipoProducto = (int)cmbTipoProducto.SelectedValue
            };

            await _productoApiClient.CreateAsync(dto);

            MessageBox.Show("Producto creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void lblTipoProducto_Click(object sender, EventArgs e)
        {

        }
    }
}
