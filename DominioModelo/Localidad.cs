namespace DominioModelo
{
    public class Localidad
    {
        public int IdLocalidad { get; set; }
        public string Nombre { get; set; }
        public int IdProvincia { get; set; }

        public Localidad(int idLocalidad, string nombre, int idProvincia)
        {
            IdLocalidad = idLocalidad;
            Nombre = nombre;
            IdProvincia = idProvincia;
        }
    }
}
