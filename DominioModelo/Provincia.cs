namespace DominioModelo
{
    public class Provincia
    {
        public int IdProvincia { get; set; }
        public string Nombre { get; set; }

        public Provincia(int idProvincia, string nombre)
        {
            IdProvincia = idProvincia;
            Nombre = nombre;
        }
    }
}
