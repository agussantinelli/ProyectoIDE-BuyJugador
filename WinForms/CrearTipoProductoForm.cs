using System;
using System.Windows.Forms;
using DTOs;
using ApiClient;

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

            await _tipoProductoApiClient.CreateAsync(dto);

            MessageBox.Show("Tipo de producto creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            this.Close(); // # REFACTORIZADO para MDI
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close(); // # REFACTORIZADO para MDI
        }
    }
}
