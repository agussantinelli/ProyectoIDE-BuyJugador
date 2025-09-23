using ApiClient;
using DTOs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinForms
{
    public partial class CrearPersonaForm : Form
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
        }

        private async void CrearPersonaForm_Load(object sender, EventArgs e)
        {
            cmbRol.Items.Add("Dueño");
            cmbRol.Items.Add("Empleado");
            cmbRol.SelectedIndex = 1; // default empleado
            dtpFechaIngreso.Enabled = true;

            // cargar provincias
            var provincias = await _provinciaApiClient.GetAllAsync();

            // Insertar ítem vacío al inicio
            provincias.Insert(0, new ProvinciaDTO { IdProvincia = 0, Nombre = "-- Seleccionar provincia --" });

            cmbProvincia.DataSource = provincias;
            cmbProvincia.DisplayMember = "Nombre";
            cmbProvincia.ValueMember = "IdProvincia";
            cmbProvincia.SelectedIndex = 0; // Selecciona el ítem vacío
        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpFechaIngreso.Enabled = cmbRol.SelectedItem?.ToString() != "Dueño";
        }

        private async void cmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvincia.SelectedValue is int idProvincia)
            {
                var localidades = await _localidadApiClient.GetAllAsync();
                var filtradas = localidades?
                    .Where(l => l.IdProvincia == idProvincia)
                    .ToList();

                cmbLocalidad.DataSource = filtradas;
                cmbLocalidad.DisplayMember = "Nombre";
                cmbLocalidad.ValueMember = "IdLocalidad";
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Debe ingresar nombre completo y email.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                FechaIngreso = cmbRol.SelectedItem?.ToString() == "Empleado"
                    ? DateOnly.FromDateTime(dtpFechaIngreso.Value)
                    : null
            };

            await _personaApiClient.CreateAsync(persona);

            MessageBox.Show("Persona creada exitosamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e) =>
            DialogResult = DialogResult.Cancel;
    }
}
