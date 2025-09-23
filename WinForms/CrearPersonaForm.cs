using ApiClient;
using DTOs;
using System;
using System.Windows.Forms;

namespace WinForms
{
    public partial class CrearPersonaForm : Form
    {
        private readonly PersonaApiClient _personaApiClient;

        public CrearPersonaForm(PersonaApiClient personaApiClient)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
        }

        private void CrearPersonaForm_Load(object sender, EventArgs e)
        {
            cmbRol.Items.Add("Dueño");
            cmbRol.Items.Add("Empleado");
            cmbRol.SelectedIndex = 1; //default empleado
            dtpFechaIngreso.Enabled = true;
        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRol.SelectedItem?.ToString() == "Dueño")
            {
                dtpFechaIngreso.Enabled = false;
            }
            else 
            {
                dtpFechaIngreso.Enabled = true;
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

        private void btnCancelar_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
