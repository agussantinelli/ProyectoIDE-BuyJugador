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

        private bool isLoading = false;

        public LoginForm(PersonaApiClient personaApiClient, UserSessionService userSessionService)
        {
            InitializeComponent();
            _personaApiClient = personaApiClient;
            _userSessionService = userSessionService;

            // # REFACTOR: Ya no se necesita StyleManager.ApplyButtonStyle(btnLogin);
            // # Todos los estilos, incluidos los de hover y deshabilitado,
            // # se han definido en LoginForm.Designer.cs para total consistencia.
        }

        // # REFACTOR: Método de carga simplificado
        private void SetLoading(bool loading)
        {
            this.isLoading = loading;

            // # Muestra u oculta la barra de progreso
            loadingBar.Visible = loading;

            // # Deshabilita los controles
            txtDni.Enabled = !loading;
            txtPassword.Enabled = !loading;

            // # REFACTOR: Al deshabilitar el botón, el Designer aplicará
            // # automáticamente el color 'DisabledBackColor' que definimos.
            btnLogin.Enabled = !loading;

            // # Feedback visual en el botón
            btnLogin.Text = loading ? "Ingresando..." : "Ingresar";
        }

        private void ClearValidationErrors()
        {
            errorProvider.SetError(txtDni, string.Empty);
            errorProvider.SetError(txtPassword, string.Empty);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (isLoading) return;

            ClearValidationErrors();

            // # Validación con ErrorProvider
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                errorProvider.SetError(txtDni, "El DNI es obligatorio.");
                return;
            }

            if (!int.TryParse(txtDni.Text, out int dni))
            {
                errorProvider.SetError(txtDni, "Por favor, ingrese un DNI válido (solo números).");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "La contraseña es obligatoria.");
                return;
            }

            try
            {
                // # Activa el estado de carga
                SetLoading(true);

                var loginResponse = await _personaApiClient.LoginAsync(dni, txtPassword.Text);

                if (loginResponse?.Token != null && loginResponse.UserInfo != null)
                {
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
            finally
            {
                // # Asegura que la UI se reactive siempre
                SetLoading(false);
            }
        }
    }
}