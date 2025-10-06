using DTOs;
using System;

namespace WinForms
{
    public class UserSessionService
    {
        public PersonaDTO? CurrentUser { get; set; }
        public bool EsAdmin => CurrentUser != null && "Admin".Equals(CurrentUser.Rol, StringComparison.OrdinalIgnoreCase);
        public void Logout()
        {
            CurrentUser = null;
        }
    }
}

