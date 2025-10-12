using ApiClient;
using DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class CrearPersonaForm : BaseForm
    {
        private readonly PersonaApiClient _personaApiClient;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;

        public CrearPersonaForm(
            PersonaApiClient personaApiClient,
            ProvinciaApiClient provinciaApiClient,
            LocalidadApiClient localidadApiClient)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
            _provinciaApiClient = provinciaApiClient;
            _localidadApiClient = localidadApiClient;

            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void CrearPersonaForm_Load(object sender, EventArgs e)
        {
            cmbRol.Items.Add("Dueño");
            cmbRol.Items.Add("Empleado");
            cmbRol.SelectedIndex = 1;

            var provincias = await _provinciaApiClient.GetAllAsync();
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
                var localidades = await _localidadApiClient.GetAllOrderedAsync();
                var filtradas = localidades?
                    .Where(l => l.IdProvincia == idProvincia)
                    .ToList();
                filtradas.Insert(0, new LocalidadDTO { IdLocalidad = 0, Nombre = "-- Seleccione una localidad --" });
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
            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Debe ingresar nombre completo y email.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var persona = new PersonaDTO
            {
                NombreCompleto = txtNombreCompleto.Text.Trim(),
                Dni = int.Parse(txtDni.Text.Trim()),
                Email = txtEmail.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                IdLocalidad = ((LocalidadDTO)cmbLocalidad.SelectedItem)?.IdLocalidad,
                FechaIngreso = cmbRol.SelectedItem?.ToString() == "Empleado" ? DateOnly.FromDateTime(DateTime.Today) : null
            };

            await _personaApiClient.CreateAsync(persona);

            MessageBox.Show("Persona creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
