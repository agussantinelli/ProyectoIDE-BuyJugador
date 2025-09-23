using ApiClient;
using System;
using System.Windows.Forms;
using System.Text.Json;

namespace WinForms
{
    public partial class LoginForm : Form
    {
        private readonly PersonaApiClient _personaApiClient;
        public string RolUsuario { get; private set; }

        public LoginForm(PersonaApiClient personaApiClient)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDni.Text, out int dni))
            {
                MessageBox.Show("Por favor, ingrese un DNI válido.", "Entrada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor, ingrese la contraseña.", "Entrada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var loginRequest = new { Dni = dni, Password = txtPassword.Text };
                var personaJson = await _personaApiClient.LoginAsync(loginRequest);

                if (personaJson != null)
                {
                    using (JsonDocument doc = JsonDocument.Parse(personaJson.ToString()))
                    {
                        JsonElement root = doc.RootElement;
                        RolUsuario = root.GetProperty("rol").GetString();
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("DNI o contraseña incorrectos.", "Error de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
