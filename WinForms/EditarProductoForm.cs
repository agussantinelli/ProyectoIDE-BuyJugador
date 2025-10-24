using ApiClient;
using DTOs;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic; 
using System.Linq; 

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

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void EditarProductoForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                _producto = await _productoApiClient.GetByIdAsync(_productoId);
                if (_producto == null)
                {
                    MessageBox.Show("No se pudo cargar la información del producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                var tiposProducto = await _tipoProductoApiClient.GetAllAsync() ?? new List<TipoProductoDTO>();

                cmbTipoProducto.DataSource = tiposProducto;
                cmbTipoProducto.DisplayMember = "Descripcion";
                cmbTipoProducto.ValueMember = "IdTipoProducto";

                txtId.Text = _producto.IdProducto.ToString();
                txtNombre.Text = _producto.Nombre;
                txtPrecio.Text = _producto.PrecioActual?.ToString("C2") ?? "N/A";

                txtId.ReadOnly = true;
                txtId.BackColor = Color.LightGray;
                txtNombre.ReadOnly = true;
                txtNombre.BackColor = Color.LightGray;
                txtPrecio.ReadOnly = true;
                txtPrecio.BackColor = Color.LightGray;

                txtDescripcion.Text = _producto.Descripcion;
                numStock.Value = Math.Max(numStock.Minimum, Math.Min(numStock.Maximum, _producto.Stock)); 

                if (_producto.IdTipoProducto.HasValue)
                {
                    cmbTipoProducto.SelectedValue = _producto.IdTipoProducto.Value;
                }
                else if (tiposProducto.Any())
                {
                    cmbTipoProducto.SelectedIndex = 0; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbTipoProducto.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un tipo de producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_producto == null)
            {
                MessageBox.Show("No se ha cargado la información del producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _producto.Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) ? null : txtDescripcion.Text.Trim();
            _producto.Stock = (int)numStock.Value;
            _producto.IdTipoProducto = (int)cmbTipoProducto.SelectedValue;

            this.Cursor = Cursors.WaitCursor;
            try
            {
                var response = await _productoApiClient.UpdateAsync(_producto.IdProducto, _producto);
                if (response.IsSuccessStatusCode)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"No se pudo actualizar el producto: {error}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
