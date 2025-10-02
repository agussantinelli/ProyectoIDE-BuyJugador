using ApiClient;
using System;
using System.Windows.Forms;

namespace WinForms
{
    public partial class LoginForm : BaseForm
    {
        private readonly PersonaApiClient _personaApiClient;
        private readonly UserSessionService _userSessionService;

        public LoginForm(PersonaApiClient personaApiClient, UserSessionService userSessionService)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
            _userSessionService = userSessionService;

            // Aplicar estilos
            StyleManager.ApplyButtonStyle(btnLogin);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDni.Text, out int dni))
            {
                MessageBox.Show("Por favor, ingrese un DNI válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var loggedInUser = await _personaApiClient.LoginAsync(dni, txtPassword.Text);
                if (loggedInUser != null)
                {
                    _userSessionService.CurrentUser = loggedInUser;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("DNI o contraseña incorrectos.", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo conectar con el servidor: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
