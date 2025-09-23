using ApiClient;
using DTOs;
using System;
using System.Windows.Forms;

namespace WinForms
{
    public partial class EditarPersonaForm : Form
    {
        private readonly PersonaApiClient _personaApiClient;
        private readonly PersonaDTO _persona;
        private readonly ProvinciaApiClient _provinciaApiClient;
        private readonly LocalidadApiClient _localidadApiClient;

        public EditarPersonaForm(
            PersonaApiClient personaApiClient,
            ProvinciaApiClient provinciaApiClient,
            LocalidadApiClient localidadApiClient,
            PersonaDTO persona)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
            _provinciaApiClient = provinciaApiClient;
            _localidadApiClient = localidadApiClient;
            _persona = persona;
        }

        private async void EditarPersonaForm_Load(object sender, EventArgs e)
        {
            txtNombreCompleto.Text = _persona.NombreCompleto;
            txtEmail.Text = _persona.Email;
            txtTelefono.Text = _persona.Telefono;
            txtDireccion.Text = _persona.Direccion;

            // cargar provincias
            var provincias = await _provinciaApiClient.GetAllAsync();
            cmbProvincia.DataSource = provincias;
            cmbProvincia.DisplayMember = "Nombre";
            cmbProvincia.ValueMember = "IdProvincia";

            // buscar la provincia de la localidad actual
            var localidadActual = await _localidadApiClient.GetByIdAsync(_persona.IdLocalidad ?? 0);
            if (localidadActual != null)
            {
                cmbProvincia.SelectedValue = localidadActual.IdProvincia;

                // cargar localidades de esa provincia
                var localidades = await _localidadApiClient.GetAllAsync();
                var filtradas = localidades?.Where(l => l.IdProvincia == localidadActual.IdProvincia).ToList();
                cmbLocalidad.DataSource = filtradas;
                cmbLocalidad.DisplayMember = "Nombre";
                cmbLocalidad.ValueMember = "IdLocalidad";

                cmbLocalidad.SelectedValue = _persona.IdLocalidad;
            }
        }


        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "⚠ Estás a punto de modificar esta persona.\n\n" +
                "Este cambio impactará en todos los lugares donde se use este registro.\n\n" +
                "¿Querés continuar?",
                "Confirmar edición",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            _persona.NombreCompleto = txtNombreCompleto.Text.Trim();
            _persona.Email = txtEmail.Text.Trim();
            _persona.Telefono = txtTelefono.Text.Trim();
            _persona.Direccion = txtDireccion.Text.Trim();
            _persona.IdLocalidad = ((LocalidadDTO)cmbLocalidad.SelectedItem)?.IdLocalidad;

            await _personaApiClient.UpdateAsync(_persona.IdPersona, _persona);

            MessageBox.Show("Persona actualizada exitosamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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

    }
}