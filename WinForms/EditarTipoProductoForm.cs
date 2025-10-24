using System;
using System.Drawing;
using System.Windows.Forms;
using DTOs;
using ApiClient;
using System.Threading.Tasks;
using System.Net.Http;


namespace WinForms
{
    public partial class EditarTipoProductoForm : BaseForm
    {
        private readonly TipoProductoApiClient _tipoProductoApiClient;
        private readonly TipoProductoDTO _tipo;

        public EditarTipoProductoForm(TipoProductoApiClient tipoProductoApiClient, TipoProductoDTO tipo)
        {
            InitializeComponent();
            _tipoProductoApiClient = tipoProductoApiClient;
            _tipo = tipo ?? throw new ArgumentNullException(nameof(tipo));

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private void EditarTipoProductoForm_Load(object sender, EventArgs e)
        {
            txtId.Text = _tipo.IdTipoProducto.ToString();
            txtDescripcion.Text = _tipo.Descripcion;

            txtId.ReadOnly = true;
            txtId.BackColor = Color.LightGray;
            txtDescripcion.Focus();
            txtDescripcion.SelectAll();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar una descripción.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "⚠️ Estás a punto de modificar este tipo de producto.\n\n" +
                "Este cambio se aplicará automáticamente en todos los productos que lo usen.\n\n" +
                "¿Estás seguro de que querés continuar?",
                "Confirmar edición",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            _tipo.Descripcion = txtDescripcion.Text.Trim();

            this.Cursor = Cursors.WaitCursor;
            try
            {
                await _tipoProductoApiClient.UpdateAsync(_tipo.IdTipoProducto, _tipo);

                MessageBox.Show("Tipo de producto actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error de red o API al actualizar: {httpEx.Message}", "Error de API", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException opEx) when (opEx.Message.Contains("No se puede eliminar"))
            {
                MessageBox.Show(opEx.Message, "Conflicto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

