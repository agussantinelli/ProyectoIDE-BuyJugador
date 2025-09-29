using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            var tiposProducto = await _tipoProductoApiClient.GetAllAsync();
            cmbTipoProducto.DataSource = tiposProducto;
            // Corrección: Usar "Descripcion" para mostrar el nombre
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
                    Precios = new List<PrecioDTO>()
                };

                var precioDto = new PrecioDTO
                {
                    Monto = numPrecio.Value,
                    FechaDesde = DateTime.Now
                };
                productoDto.Precios.Add(precioDto);

                await _productoApiClient.CreateAsync(productoDto);
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un tipo de producto y asegúrese de que el precio sea mayor a cero.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

