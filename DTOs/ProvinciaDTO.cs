namespace DTOs
{
    public class ProvinciaDTO
    {
        public int IdProvincia { get; set; }

        public string Nombre { get; set; }

        public static ProvinciaDTO FromDominio(DominioModelo.Provincia entidad)
        {
            if (entidad == null) return null;

            return new ProvinciaDTO
            {
                IdProvincia = entidad.IdProvincia,
                Nombre = entidad.Nombre
            };
        }

        public DominioModelo.Provincia ToDominio()
        {
            return new DominioModelo.Provincia
            {
                IdProvincia = this.IdProvincia,
                Nombre = this.Nombre
            };
        }
    }
}


