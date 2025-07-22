namespace DTO
{
    public class Cliente : Persona
    {
        public DateTime FechaNacimiento { get; private set; }
        public string Telefono { get; private set; }
        public int CodLocalidad { get; private set; }
        public int Edad { get; private set; }

    }
}