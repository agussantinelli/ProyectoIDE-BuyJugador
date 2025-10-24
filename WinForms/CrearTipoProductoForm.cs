using System;
using System.Windows.Forms;
using DTOs;
using ApiClient;
using System.Threading.Tasks;
using System.Net.Http;


namespace WinForms
{
    public partial class CrearTipoProductoForm : BaseForm
    {
        private readonly TipoProductoApiClient _tipoProductoApiClient;

        public CrearTipoProductoForm(TipoProductoApiClient tipoProductoApiClient)
        {
            InitializeComponent();
            _tipoProductoApiClient = tipoProductoApiClient;

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private void CrearTipoProductoForm_Load(object sender, EventArgs e)
        {
            txtDescripcion.Clear();
            txtDescripcion.Focus();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar una descripción.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new TipoProductoDTO
            {
                Descripcion = txtDescripcion.Text.Trim()
            };

            this.Cursor = Cursors.WaitCursor;
            TipoProductoDTO? tipoCreado = null;
            try
            {
                tipoCreado = await _tipoProductoApiClient.CreateAsync(dto);
                if (tipoCreado != null)
                {
                    MessageBox.Show($"Tipo de producto '{tipoCreado.Descripcion}' creado exitosamente (ID: {tipoCreado.IdTipoProducto}).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo crear el tipo de producto. La API no devolvió el objeto creado.", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error de red o API al crear el tipo de producto: {httpEx.Message}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

