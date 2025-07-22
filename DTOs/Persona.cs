namespace DTOs
{
    public abstract class Persona
    {
        public int Dni { get; private set; }
        public string NombrePersona { get; private set; }
        public string MailPersona { get; private set; }
        public string Contrasenia { get; private set; }
        public string TelefonoPersona { get; private set; }
    }
}