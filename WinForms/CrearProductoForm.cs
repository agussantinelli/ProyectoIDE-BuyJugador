using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
    public partial class CrearProductoForm : BaseForm
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;

        public CrearProductoForm(ProductoApiClient productoApiClient, TipoProductoApiClient tipoProductoApiClient)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _tipoProductoApiClient = tipoProductoApiClient;

            StyleManager.ApplyButtonStyle(btnCrear);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void CrearProductoForm_Load(object sender, EventArgs e)
        {
            var tiposProducto = await _tipoProductoApiClient.GetAllAsync();
            cmbTipoProducto.DataSource = tiposProducto;
            cmbTipoProducto.DisplayMember = "Descripcion";
            cmbTipoProducto.ValueMember = "IdTipoProducto";
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (cmbTipoProducto.SelectedValue != null && numPrecio.Value > 0)
            {
                var productoDto = new ProductoDTO
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Stock = (int)numStock.Value,
                    IdTipoProducto = (int)cmbTipoProducto.SelectedValue,
                    Precios = new List<PrecioVentaDTO>()
                };

                var precioDto = new PrecioVentaDTO
                {
                    Monto = (decimal)numPrecio.Value,
                    FechaDesde = DateTime.Now
                };
                productoDto.Precios.Add(precioDto);

                await _productoApiClient.CreateAsync(productoDto);
                this.DialogResult = DialogResult.OK; // # REFACTORIZADO para MDI
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un tipo de producto y asegúrese de que el precio sea mayor a cero.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // # REFACTORIZADO para MDI
            this.Close();
        }
    }
}
