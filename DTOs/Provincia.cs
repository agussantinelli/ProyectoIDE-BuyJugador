namespace DTOs
{
    public class Provincia
    {
        public int CodigoProvincia { get; private set; }
        public string NombreProvincia { get; private set; }

        public Provincia(int codigoProvincia, string nombreProvincia)
        {
            CodigoProvincia = codigoProvincia;
            NombreProvincia = nombreProvincia;
        }
    }
}