namespace DTOs
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }

        public string RazonSocial { get; set; } = string.Empty;

        public string Cuit { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        public int? IdLocalidad { get; set; }

        public bool Activo { get; set; } = true;

        public string? LocalidadNombre { get; set; }

        public string? ProvinciaNombre { get; set; }

        public static ProveedorDTO? FromDominio(DominioModelo.Proveedor entidad)
        {
            if (entidad == null) return null;

            return new ProveedorDTO
            {
                IdProveedor = entidad.IdProveedor,
                RazonSocial = entidad.RazonSocial ?? string.Empty,
                Cuit = entidad.Cuit ?? string.Empty,
                Email = entidad.Email ?? string.Empty,
                Telefono = entidad.Telefono ?? string.Empty,
                Direccion = entidad.Direccion ?? string.Empty,
                IdLocalidad = entidad.IdLocalidad,
                Activo = entidad.Activo,
                LocalidadNombre = entidad.IdLocalidadNavigation?.Nombre,
                ProvinciaNombre = entidad.IdLocalidadNavigation?.IdProvinciaNavigation?.Nombre
            };
        }

        public DominioModelo.Proveedor ToDominio()
        {
            return new DominioModelo.Proveedor
            {
                IdProveedor = IdProveedor,
                RazonSocial = RazonSocial,
                Cuit = Cuit,
                Email = Email,
                Telefono = Telefono,
                Direccion = Direccion,
                IdLocalidad = IdLocalidad,
                Activo = Activo
            };
        }
    }
}
