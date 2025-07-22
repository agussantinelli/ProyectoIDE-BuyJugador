namespace DTO
{
    public abstract class Persona
    {
        public int Legajo { get; private set; }
        public string Nombre { get; private set; }
        public string Mail { get; private set; }
        public string Contrasenia { get; private set; }
    }
}