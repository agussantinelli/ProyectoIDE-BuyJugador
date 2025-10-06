using ApiClient;
using DTOs;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    public partial class EditarProductoForm : BaseForm
    {
        private readonly int _productoId;
        private readonly ProductoApiClient _productoApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;
        private ProductoDTO _producto;

        public EditarProductoForm(int productoId, ProductoApiClient productoApiClient, TipoProductoApiClient tipoProductoApiClient)
        {
            InitializeComponent();
            _productoId = productoId;
            _productoApiClient = productoApiClient;
            _tipoProductoApiClient = tipoProductoApiClient;

            this.StartPosition = FormStartPosition.CenterScreen;

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void EditarProductoForm_Load(object sender, EventArgs e)
        {
            _producto = await _productoApiClient.GetByIdAsync(_productoId);
            var tiposProducto = await _tipoProductoApiClient.GetAllAsync();

            cmbTipoProducto.DataSource = tiposProducto;
            cmbTipoProducto.DisplayMember = "Descripcion";
            cmbTipoProducto.ValueMember = "IdTipoProducto";

            // Cargar datos no editables
            txtId.Text = _producto.IdProducto.ToString();
            txtNombre.Text = _producto.Nombre;
            txtPrecio.Text = _producto.PrecioActual?.ToString("C2") ?? "N/A";

            // Configurar campos como solo lectura
            txtId.ReadOnly = true;
            txtId.BackColor = Color.LightGray;
            txtNombre.ReadOnly = true;
            txtNombre.BackColor = Color.LightGray;
            txtPrecio.ReadOnly = true;
            txtPrecio.BackColor = Color.LightGray;

            // Cargar datos editables
            txtDescripcion.Text = _producto.Descripcion;
            numStock.Value = _producto.Stock;
            if (_producto.IdTipoProducto.HasValue)
            {
                cmbTipoProducto.SelectedValue = _producto.IdTipoProducto.Value;
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            // Actualizar solo los datos pertinentes del producto
            _producto.Descripcion = txtDescripcion.Text;
            _producto.Stock = (int)numStock.Value;
            _producto.IdTipoProducto = (int)cmbTipoProducto.SelectedValue;

            await _productoApiClient.UpdateAsync(_producto.IdProducto, _producto);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

