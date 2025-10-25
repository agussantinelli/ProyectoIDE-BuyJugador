namespace DTOs
{
    public class LoginRequestDTO
    {
        public int Dni { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
