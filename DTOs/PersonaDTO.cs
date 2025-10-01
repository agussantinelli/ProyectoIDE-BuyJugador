namespace DTOs
{
    public class PersonaDTO
    {
        public int IdPersona { get; set; }
        public string? NombreCompleto { get; set; }
        public int Dni { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public int? IdLocalidad { get; set; }
        public DateOnly? FechaIngreso { get; set; }
        public string? LocalidadNombre { get; set; }
        public string? ProvinciaNombre { get; set; }

        // --- LÓGICA DE ROL ---
        public string Rol => !FechaIngreso.HasValue ? "Dueño" : "Empleado";

        public string FechaIngresoFormateada => FechaIngreso.HasValue ? FechaIngreso.Value.ToString("dd/MM/yy") : "-";

        public bool Estado { get; set; }
        public string EstadoDescripcion => Estado ? "Activo" : "Inactivo";


        public static PersonaDTO FromDominio(DominioModelo.Persona entidad)
        {
            if (entidad == null) return null;

            return new PersonaDTO
            {
                IdPersona = entidad.IdPersona,
                NombreCompleto = entidad.NombreCompleto,
                Dni = entidad.Dni,
                Email = entidad.Email,
                Password = entidad.Password,
                Telefono = entidad.Telefono,
                Direccion = entidad.Direccion,
                IdLocalidad = entidad.IdLocalidad,
                FechaIngreso = entidad.FechaIngreso,
                LocalidadNombre = entidad.IdLocalidadNavigation?.Nombre,
                ProvinciaNombre = entidad.IdLocalidadNavigation?.IdProvinciaNavigation?.Nombre,
                Estado = entidad.Estado
            };
        }

        public DominioModelo.Persona ToDominio()
        {
            return new DominioModelo.Persona
            {
                IdPersona = this.IdPersona,
                NombreCompleto = this.NombreCompleto,
                Dni = this.Dni,
                Email = this.Email,
                Password = this.Password,
                Telefono = this.Telefono,
                Direccion = this.Direccion,
                IdLocalidad = this.IdLocalidad,
                FechaIngreso = this.FechaIngreso,
                Estado = this.Estado
            };
        }
    }
}

