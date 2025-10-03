using DTOs;

namespace WinForms
{
    /// <summary>
    /// Servicio para mantener la información del usuario logueado durante la sesión.
    /// </summary>
    public class UserSessionService
    {
        public PersonaDTO? CurrentUser { get; set; }

        // Propiedad agregada para verificar fácilmente si el usuario es Administrador.
        public bool EsAdmin => CurrentUser?.Rol?.Equals("Admin", System.StringComparison.OrdinalIgnoreCase) ?? false;
    }
}

