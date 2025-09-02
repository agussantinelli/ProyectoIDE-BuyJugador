namespace DTOs
{
    public class LocalidadDTO
    {
        public int IdLocalidad { get; set; }
        public string Nombre { get; set; }
        public int? IdProvincia { get; set; }

        public static LocalidadDTO FromDominio(DominioModelo.Localidad entidad)
        {
            if (entidad == null) return null;

            return new LocalidadDTO
            {
                IdLocalidad = entidad.IdLocalidad,
                Nombre = entidad.Nombre,
                IdProvincia = entidad.IdProvincia
            };
        }

        public DominioModelo.Localidad ToDominio()
        {
            return new DominioModelo.Localidad
            {
                IdLocalidad = this.IdLocalidad,
                Nombre = this.Nombre,
                IdProvincia = this.IdProvincia
            };
        }
    }
}
