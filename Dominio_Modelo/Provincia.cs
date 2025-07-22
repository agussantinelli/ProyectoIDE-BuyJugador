namespace Dominio_Modelo
{
    public class Provincia
    {
        public int CodigoProvincia { get; private set; }
        public string NombreProvincia { get; private set; }

        public Provincia(int codigoProvincia, string nombreProvincia)
        {
            SetCodigoProvincia(codigoProvincia);
            SetNombreProvincia(nombreProvincia);
        }

        public void SetCodigoProvincia(int codigoProvincia)
        {
            if (codigoProvincia <= 0)
                throw new ArgumentException("El código de provincia debe ser positivo.", nameof(codigoProvincia));
            CodigoProvincia = codigoProvincia;
        }

        public void SetNombreProvincia(string nombreProvincia)
        {
            if (string.IsNullOrWhiteSpace(nombreProvincia))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombreProvincia));
            NombreProvincia = nombreProvincia;
        }

    }
}
