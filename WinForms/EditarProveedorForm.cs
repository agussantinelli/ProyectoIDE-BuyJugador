using ApiClient;
using DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class EditarProveedorForm : BaseForm
    {
        private readonly ProveedorApiClient _proveedorApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;
        private readonly ProveedorDTO _proveedor;

        public EditarProveedorForm(
            ProveedorApiClient proveedorApiClient,
            ProvinciaApiClient provinciaApiClient,
            LocalidadApiClient localidadApiClient,
            ProveedorDTO proveedor)
        {
            InitializeComponent();
            _proveedorApiClient = proveedorApiClient;
            _provinciaApiClient = provinciaApiClient;
            _localidadApiClient = localidadApiClient;
            _proveedor = proveedor;

            this.StartPosition = FormStartPosition.CenterScreen;

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void EditarProveedorForm_Load(object sender, EventArgs e)
        {
            // Cargar datos del proveedor en los campos del formulario
            txtRazonSocial.Text = _proveedor.RazonSocial;
            txtCuit.Text = _proveedor.Cuit; // CUIT se muestra pero no se edita
            txtEmail.Text = _proveedor.Email;
            txtTelefono.Text = _proveedor.Telefono;
            txtDireccion.Text = _proveedor.Direccion;

            // Cargar y seleccionar provincia y localidad
            var provincias = await _provinciaApiClient.GetAllAsync();
            cmbProvincia.DataSource = provincias;
            cmbProvincia.DisplayMember = "Nombre";
            cmbProvincia.ValueMember = "IdProvincia";

            if (_proveedor.IdLocalidad.HasValue)
            {
                var localidadActual = await _localidadApiClient.GetByIdAsync(_proveedor.IdLocalidad.Value);
                if (localidadActual != null && localidadActual.IdProvincia.HasValue)
                {
                    cmbProvincia.SelectedValue = localidadActual.IdProvincia.Value;

                    await CargarLocalidadesAsync(localidadActual.IdProvincia.Value);

                    cmbLocalidad.SelectedValue = localidadActual.IdLocalidad;
                }
            }
        }

        private async void cmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvincia.SelectedValue is int idProvincia && idProvincia > 0)
            {
                await CargarLocalidadesAsync(idProvincia);
            }
            else
            {
                cmbLocalidad.DataSource = null;
            }
        }

        private async Task CargarLocalidadesAsync(int idProvincia)
        {
            var localidades = await _localidadApiClient.GetAllOrderedAsync() ?? new();
            var filtradas = localidades.Where(l => l.IdProvincia == idProvincia).ToList();
            cmbLocalidad.DataSource = filtradas;
            cmbLocalidad.DisplayMember = "Nombre";
            cmbLocalidad.ValueMember = "IdLocalidad";
        }


        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "⚠ Se modificarán los datos del proveedor.\n¿Desea continuar?",
                "Confirmar edición",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            // Actualizar el DTO con los nuevos datos
            _proveedor.RazonSocial = txtRazonSocial.Text.Trim();
            _proveedor.Email = txtEmail.Text.Trim();
            _proveedor.Telefono = txtTelefono.Text.Trim();
            _proveedor.Direccion = txtDireccion.Text.Trim();
            _proveedor.IdLocalidad = (cmbLocalidad.SelectedItem as LocalidadDTO)?.IdLocalidad;

            var resp = await _proveedorApiClient.UpdateAsync(_proveedor.IdProveedor, _proveedor);
            if (resp.IsSuccessStatusCode)
            {
                MessageBox.Show("Proveedor actualizado.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el proveedor.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
            => DialogResult = DialogResult.Cancel;
    }
}

