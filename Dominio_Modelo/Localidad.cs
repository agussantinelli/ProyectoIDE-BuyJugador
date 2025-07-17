using System;

namespace Dominio_Modelo
{
    public class Localidad
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public int IdProvincia { get; private set; }

        public Localidad(int id, string nombre, int idProvincia)
        {
            SetId(id);
            SetNombre(nombre);
            SetIdProvincia(idProvincia);
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de localidad debe ser positivo.", nameof(id));
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void SetIdProvincia(int idProvincia)
        {
            if (idProvincia <= 0)
                throw new ArgumentException("El ID de provincia debe ser positivo.", nameof(idProvincia));
            IdProvincia = idProvincia;
        }
    }
}