using DominioModelo;

namespace DTOs
{
    public class LocalidadDTO
    {
        public int IdLocalidad { get; set; }
        public string Nombre { get; set; }
        public int? IdProvincia { get; set; }

        public static LocalidadDTO FromDominio(Localidad entidad)
        {
            return new LocalidadDTO
            {
                IdLocalidad = entidad.IdLocalidad,
                Nombre = entidad.Nombre,
                IdProvincia = entidad.IdProvincia
            };
        }

        public Localidad ToDominio()
        {
            return new Localidad
            {
                IdLocalidad = this.IdLocalidad,
                Nombre = this.Nombre,
                IdProvincia = this.IdProvincia
            };
        }
    }
}


