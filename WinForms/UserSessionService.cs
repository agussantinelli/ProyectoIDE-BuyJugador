using DTOs;

namespace WinForms
{
    /// <summary>
    /// Servicio para mantener la información del usuario logueado durante la sesión.
    /// </summary>
    public class UserSessionService
    {
        public PersonaDTO? CurrentUser { get; set; }
    }
}
