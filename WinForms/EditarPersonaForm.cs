using ApiClient;
using DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class EditarPersonaForm : BaseForm
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

            // Aplicar estilos
            StyleManager.ApplyButtonStyle(btnGuardar);
            StyleManager.ApplyButtonStyle(btnCancelar);
        }

        private async void EditarPersonaForm_Load(object sender, EventArgs e)
        {
            txtEmail.Text = _persona.Email;
            txtTelefono.Text = _persona.Telefono;
            txtDireccion.Text = _persona.Direccion;

            var provincias = await _provinciaApiClient.GetAllAsync();
            cmbProvincia.DataSource = provincias;
            cmbProvincia.DisplayMember = "Nombre";
            cmbProvincia.ValueMember = "IdProvincia";

            if (_persona.IdLocalidad.HasValue)
            {
                var localidadActual = await _localidadApiClient.GetByIdAsync(_persona.IdLocalidad.Value);
                if (localidadActual != null && localidadActual.IdProvincia.HasValue)
                {
                    cmbProvincia.SelectedValue = localidadActual.IdProvincia.Value;

                    await CargarLocalidadesAsync(localidadActual.IdProvincia.Value);

                    cmbLocalidad.SelectedValue = localidadActual.IdLocalidad;
                }
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Se modificarán los datos de la persona. " +
                "¿Querés continuar?",
                "Confirmar edición",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

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
                await CargarLocalidadesAsync(idProvincia);
            }
        }

        private async Task CargarLocalidadesAsync(int idProvincia)
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

