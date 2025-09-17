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

        public EditarPersonaForm(PersonaApiClient personaApiClient, PersonaDTO persona)
        {
            InitializeComponent();  
            _personaApiClient = personaApiClient;
            _persona = persona;
        }

        private void EditarPersonaForm_Load(object sender, EventArgs e)
        {
            txtNombreCompleto.Text = _persona.NombreCompleto;
            txtDni.Text = _persona.Dni.ToString();
            txtEmail.Text = _persona.Email;
            txtPassword.Text = System.Text.Encoding.UTF8.GetString(_persona.Password);
            txtTelefono.Text = _persona.Telefono;
            txtDireccion.Text = _persona.Direccion;
            cmbLocalidad.SelectedValue = _persona.IdLocalidad;

            if (_persona.FechaIngreso.HasValue)
            {
                cmbRol.SelectedItem = "Empleado";
                dtpFechaIngreso.Value = _persona.FechaIngreso.Value.ToDateTime(TimeOnly.MinValue);
                dtpFechaIngreso.Enabled = true;
            }
            else
            {
                cmbRol.SelectedItem = "Dueño";
                dtpFechaIngreso.Enabled = false;
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "⚠️ Estás a punto de modificar esta persona.\n\n" +
                "Este cambio impactará en todos los lugares donde se use este registro.\n\n" +
                "¿Querés continuar?",
                "Confirmar edición",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            _persona.NombreCompleto = txtNombreCompleto.Text.Trim();
            _persona.Dni = int.Parse(txtDni.Text.Trim());
            _persona.Email = txtEmail.Text.Trim();
            _persona.Password = System.Text.Encoding.UTF8.GetBytes(txtPassword.Text.Trim());
            _persona.Telefono = txtTelefono.Text.Trim();
            _persona.Direccion = txtDireccion.Text.Trim();
            _persona.IdLocalidad = ((LocalidadDTO)cmbLocalidad.SelectedItem)?.IdLocalidad;
            _persona.FechaIngreso = cmbRol.SelectedItem?.ToString() == "Empleado"
                ? DateOnly.FromDateTime(dtpFechaIngreso.Value)
                : null;

            await _personaApiClient.UpdateAsync(_persona.IdPersona, _persona);

            MessageBox.Show("Persona actualizada exitosamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpFechaIngreso.Enabled = cmbRol.SelectedItem?.ToString() == "Empleado";
        }
    }
}
