using ApiClient;
using System;
using System.Windows.Forms;
using DTOs;

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
                // # MODIFICACIÓN: Se consume el método de login actualizado.
                var loginResponse = await _personaApiClient.LoginAsync(dni, txtPassword.Text);

                // # La validación ahora usa el objeto de respuesta completo.
                if (loginResponse?.Token != null && loginResponse.UserInfo != null)
                {
                    // # Se establece la sesión con ambos, el DTO del usuario y el token.
                    _userSessionService.SetSession(loginResponse.UserInfo, loginResponse.Token);

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

