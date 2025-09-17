using System;
using System.Windows.Forms;
using DTOs;
using ApiClient;

namespace WinForms
{
    public partial class EditarProductoForm : Form
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;
        private readonly ProductoDTO _producto;

        public EditarProductoForm(ProductoApiClient productoApiClient, TipoProductoApiClient tipoProductoApiClient, ProductoDTO producto)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _tipoProductoApiClient = tipoProductoApiClient;
            _producto = producto;
        }

        private async void EditarProductoForm_Load(object sender, EventArgs e)
        {
            var tipos = await _tipoProductoApiClient.GetAllAsync();
            cmbTipoProducto.DataSource = tipos;
            cmbTipoProducto.DisplayMember = "Descripcion";
            cmbTipoProducto.ValueMember = "IdTipoProducto";

            // Cargar datos
            txtNombre.Text = _producto.Nombre;
            txtDescripcion.Text = _producto.Descripcion;
            numStock.Value = _producto.Stock;
            cmbTipoProducto.SelectedValue = _producto.IdTipoProducto;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            _producto.Nombre = txtNombre.Text.Trim();
            _producto.Descripcion = txtDescripcion.Text.Trim();
            _producto.Stock = (int)numStock.Value;
            _producto.IdTipoProducto = (int)cmbTipoProducto.SelectedValue;

            await _productoApiClient.UpdateAsync(_producto.IdProducto, _producto);

            MessageBox.Show("Producto actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
