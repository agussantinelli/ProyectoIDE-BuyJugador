using System;

namespace Dominio_Modelo
{
    public class Provincia
    {
        public int Id { get; private set; }
        public string Descripcion { get; private set; }

        public Provincia(int id, string descripcion)
        {
            SetId(id);
            SetDescripcion(descripcion);
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de provincia debe ser positivo.", nameof(id));
            Id = id;
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción no puede ser nula o vacía.", nameof(descripcion));
            Descripcion = descripcion;
        }
    }
}