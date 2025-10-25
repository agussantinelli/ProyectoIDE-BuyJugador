using DTOs;

namespace DTOs
{
    public class LoginResponseDTO
    {
        public string? Token { get; set; }

        public PersonaDTO? UserInfo { get; set; }
    }
}

