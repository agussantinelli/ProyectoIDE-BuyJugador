namespace DominioModelo
{
    public class Duenio : Persona
    {
        public int IdDuenio { get; set; }

        public Duenio(int idDuenio, string nombre, int cuit, string email, int telefono, string direccion, int idLocalidad) : base(nombre, cuit, email, telefono, direccion, idLocalidad)
        {
            IdDuenio = idDuenio;
        }
    }
}
