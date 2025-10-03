using ApiClient;
using DTOs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class CrearProveedorForm : BaseForm
    {
        private readonly ProveedorApiClient _proveedorApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;

        public CrearProveedorForm(
            ProveedorApiClient proveedorApiClient,
            ProvinciaApiClient provinciaApiClient,
            LocalidadApiClient localidadApiClient)
        {
            InitializeComponent();
            _proveedorApiClient = proveedorApiClient;
            _provinciaApiClient = provinciaApiClient;
            _localidadApiClient = localidadApiClient;

            // Aplicar estilos
            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void CrearProveedorForm_Load(object sender, EventArgs e)
        {
            // provincias
            var provincias = await _provinciaApiClient.GetAllAsync() ?? new();
            provincias.Insert(0, new ProvinciaDTO { IdProvincia = 0, Nombre = "-- Seleccionar provincia --" });

            cmbProvincia.DataSource = provincias;
            cmbProvincia.DisplayMember = "Nombre";
            cmbProvincia.ValueMember = "IdProvincia";
            cmbProvincia.SelectedIndex = 0;
        }

        private async void cmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvincia.SelectedValue is int idProvincia && idProvincia > 0)
            {
                var localidades = await _localidadApiClient.GetAllOrderedAsync() ?? new();
                var filtradas = localidades.Where(l => l.IdProvincia == idProvincia).ToList();
                filtradas.Insert(0, new LocalidadDTO
                {
                    IdLocalidad = 0,
                    Nombre = "-- Seleccione una localidad --"
                });

                cmbLocalidad.DataSource = filtradas;
                cmbLocalidad.DisplayMember = "Nombre";
                cmbLocalidad.ValueMember = "IdLocalidad";
                cmbLocalidad.SelectedIndex = 0;

            }
            else
            {
                cmbLocalidad.DataSource = null;
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text)
                || string.IsNullOrWhiteSpace(txtCuit.Text))
            {
                MessageBox.Show("Ingrese Razón Social y CUIT.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new ProveedorDTO
            {
                RazonSocial = txtRazonSocial.Text.Trim(),
                Cuit = txtCuit.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                IdLocalidad = (cmbLocalidad.SelectedItem as LocalidadDTO)?.IdLocalidad
            };

            var resp = await _proveedorApiClient.CreateAsync(dto);
            if (resp.IsSuccessStatusCode)
            {
                MessageBox.Show("Proveedor creado.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("No se pudo crear el proveedor.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
            => DialogResult = DialogResult.Cancel;
    }
}

