using DTOs;
using System;

namespace WinForms
{
    public class UserSessionService
    {
        public PersonaDTO? CurrentUser { get; private set; }
        private string? _token;

        public bool EsAdmin => CurrentUser != null && "Admin".Equals(CurrentUser.Rol, StringComparison.OrdinalIgnoreCase);

        public void SetSession(PersonaDTO user, string token)
        {
            CurrentUser = user;
            _token = token;
        }
        public string? GetToken() => _token;

        public void Logout()
        {
            CurrentUser = null;
            _token = null;
        }
    }
}
