namespace Dominio_Modelo
{
    public class Provincia
    {
        public int CodProvincia { get; private set; }
        public string NombreProvincia { get; private set; }

        public Provincia(int codProvincia, string nombreProvincia)
        {
            SetCodProvincia(codProvincia);
            SetNombreProvincia(nombreProvincia);
        }

        public void SetCodProvincia(int codProvincia)
        {
            if (codProvincia <= 0)
                throw new ArgumentException("El código de provincia debe ser positivo.", nameof(codProvincia));
            CodProvincia = codProvincia;
        }

        public void SetNombreProvincia(string nombreProvincia)
        {
            if (string.IsNullOrWhiteSpace(nombreProvincia))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombreProvincia));
            NombreProvincia = nombreProvincia;
        }

    }
}
