using System;

namespace Dominio_Modelo
{
    public class TipoProducto
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public TipoProducto(int id, string nombre)
        {
            SetId(id);
            SetNombre(nombre);
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del tipo de producto debe ser positivo.", nameof(id));
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del tipo de producto no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }
    }
}