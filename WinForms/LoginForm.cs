using ApiClient;
using System;
using System.Windows.Forms;

namespace WinForms
{
    public partial class LoginForm : Form
    {
        private readonly PersonaApiClient _personaApiClient;
        public string? RolUsuario { get; private set; }

        public LoginForm(PersonaApiClient personaApiClient)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
            RolUsuario = null;
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
                var personaLogueada = await _personaApiClient.LoginAsync(dni, txtPassword.Text);

                if (personaLogueada != null)
                {
                    RolUsuario = personaLogueada.Rol;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("DNI o contraseña incorrectos, o el usuario está inactivo.", "Error de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

