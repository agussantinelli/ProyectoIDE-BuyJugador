using DTOs;
using System;

namespace WinForms
{
    /// <summary>
    /// Servicio para mantener la información del usuario logueado durante la sesión.
    /// </summary>
    public class UserSessionService
    {
        public PersonaDTO? CurrentUser { get; set; }

        /// <summary>
        /// Propiedad que determina si el usuario actual es un Administrador.
        /// </summary>
        public bool EsAdmin => CurrentUser != null && "Admin".Equals(CurrentUser.Rol, StringComparison.OrdinalIgnoreCase);
    }
}

