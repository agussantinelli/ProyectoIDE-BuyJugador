namespace DTOs
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public string Cuit { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int? IdLocalidad { get; set; }

        public static ProveedorDTO FromDominio(DominioModelo.Proveedor entidad)
        {
            if (entidad == null) return null;

            return new ProveedorDTO
            {
                IdProveedor = entidad.IdProveedor,
                RazonSocial = entidad.RazonSocial,
                Cuit = entidad.Cuit,
                Email = entidad.Email,
                Telefono = entidad.Telefono,
                Direccion = entidad.Direccion,
                IdLocalidad = entidad.IdLocalidad
            };
        }

        public DominioModelo.Proveedor ToDominio()
        {
            return new DominioModelo.Proveedor
            {
                IdProveedor = this.IdProveedor,
                RazonSocial = this.RazonSocial,
                Cuit = this.Cuit,
                Email = this.Email,
                Telefono = this.Telefono,
                Direccion = this.Direccion,
                IdLocalidad = this.IdLocalidad
            };
        }
    }
}


