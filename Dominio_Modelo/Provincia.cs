namespace Dominio_Modelo
{
    public class Provincia
    {
        public int CodProvincia { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }

        public Provincia(int codProvincia, string nombre, string descripcion)
        {
            SetCodProvincia(codProvincia);
            SetNombre(nombre);
            SetDescripcion(descripcion);
        }

        public void SetCodProvincia(int codProvincia)
        {
            if (codProvincia <= 0)
                throw new ArgumentException("El código de provincia debe ser positivo.", nameof(codProvincia));
            CodProvincia = codProvincia;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción no puede ser nula o vacía.", nameof(descripcion));
            Descripcion = descripcion;
        }
    }
}
