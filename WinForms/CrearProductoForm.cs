using ApiClient;
using DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http; 
using System.Net.Http.Json; 
using System.Threading.Tasks;


namespace WinForms
{
    public partial class CrearProductoForm : BaseForm
    {
        private readonly ProductoApiClient _productoApiClient;
        private readonly TipoProductoApiClient _tipoProductoApiClient;
        private readonly PrecioVentaApiClient _precioVentaApiClient; 

        public CrearProductoForm(ProductoApiClient productoApiClient,
                               TipoProductoApiClient tipoProductoApiClient,
                               PrecioVentaApiClient precioVentaApiClient)
        {
            InitializeComponent();
            _productoApiClient = productoApiClient;
            _tipoProductoApiClient = tipoProductoApiClient;
            _precioVentaApiClient = precioVentaApiClient; 

            StyleManager.ApplyButtonStyle(btnCrear);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void CrearProductoForm_Load(object sender, EventArgs e)
        {
            var tiposProducto = await _tipoProductoApiClient.GetAllAsync();
            cmbTipoProducto.DataSource = tiposProducto;
            cmbTipoProducto.DisplayMember = "Descripcion";
            cmbTipoProducto.ValueMember = "IdTipoProducto";
            cmbTipoProducto.SelectedIndex = -1; 
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (cmbTipoProducto.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un tipo de producto.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (numPrecio.Value <= 0)
            {
                MessageBox.Show("Por favor, ingrese un precio mayor a cero.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el producto.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var productoDto = new ProductoDTO
            {
                Nombre = txtNombre.Text.Trim(), 
                Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) ? null : txtDescripcion.Text.Trim(), 
                Stock = (int)numStock.Value,
                IdTipoProducto = (int)cmbTipoProducto.SelectedValue
            };

            this.Cursor = Cursors.WaitCursor;
            HttpResponseMessage? responseProducto = null;
            ProductoDTO? nuevoProducto = null;

            try
            {
                responseProducto = await _productoApiClient.CreateAsync(productoDto);

                if (responseProducto.IsSuccessStatusCode)
                {
                    try
                    {
                        nuevoProducto = await responseProducto.Content.ReadFromJsonAsync<ProductoDTO>();
                    }
                    catch { } 

                    if (nuevoProducto == null && responseProducto.Headers.Location != null)
                    {
                        var locationUrl = responseProducto.Headers.Location;
                        nuevoProducto = await _productoApiClient.GetByUrlAsync(locationUrl.ToString()); 
                    }

                    if (nuevoProducto == null)
                    {
                        await Task.Delay(200); 
                        var todos = await _productoApiClient.GetAllAsync();
                        nuevoProducto = todos?.FirstOrDefault(p => p.Nombre == productoDto.Nombre && p.IdTipoProducto == productoDto.IdTipoProducto);
                    }


                    if (nuevoProducto != null)
                    {
                        var precioDto = new PrecioVentaDTO
                        {
                            IdProducto = nuevoProducto.IdProducto, 
                            Monto = (decimal)numPrecio.Value,
                            FechaDesde = DateTime.Now
                        };

                        var responsePrecio = await _precioVentaApiClient.CreateAsync(precioDto);

                        if (responsePrecio.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Producto y precio inicial creados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            var errorPrecio = await responsePrecio.Content.ReadAsStringAsync();
                            MessageBox.Show($"Producto creado (ID: {nuevoProducto.IdProducto}), pero falló al crear el precio inicial: {errorPrecio}. Puede agregarlo editando el producto.", "Error Parcial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.DialogResult = DialogResult.OK; 
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Producto creado, pero no se pudo obtener su ID para asignar el precio inicial. Edite el producto para agregar el precio.", "Error Parcial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.OK; 
                        this.Close();
                    }
                }
                else
                {
                    var errorProducto = await responseProducto.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al crear el producto: {errorProducto}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error");
                if (responseProducto?.IsSuccessStatusCode == true && nuevoProducto == null)
                {
                    MessageBox.Show("Se creó el producto, pero ocurrió un error antes de poder guardar el precio inicial. Edite el producto para agregarlo.", "Error Post-Creación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                }
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

    public static class ProductoApiClientExtensions
    {
        public static async Task<ProductoDTO?> GetByUrlAsync(this ProductoApiClient client, string url)
        {

            var httpClient = (HttpClient)typeof(ProductoApiClient)
                                .GetField("_httpClient", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                ?.GetValue(client);

            if (httpClient == null) return null; 

            try
            {
                return await httpClient.GetFromJsonAsync<ProductoDTO?>(url);
            }
            catch
            {
                return null; 
            }
        }
    }

}
