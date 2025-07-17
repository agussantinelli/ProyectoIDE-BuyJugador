using System;

namespace Dominio_Modelo
{
    public class Producto
    {
        public int Id { get; private set; }
        public string Caracteristicas { get; private set; }
        public int Stock { get; private set; }
        public int IdTipoProducto { get; private set; }

        public Producto(int id, string caracteristicas, int stock, int idTipoProducto)
        {
            SetId(id);
            SetCaracteristicas(caracteristicas);
            SetStock(stock);
            SetIdTipoProducto(idTipoProducto);
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del producto debe ser positivo.", nameof(id));
            Id = id;
        }

        public void SetCaracteristicas(string caracteristicas)
        {
            if (string.IsNullOrWhiteSpace(caracteristicas))
                throw new ArgumentException("Las características no pueden ser nulas o vacías.", nameof(caracteristicas));
            Caracteristicas = caracteristicas;
        }

        public void SetStock(int stock)
        {
            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.", nameof(stock));
            Stock = stock;
        }

        public void SetIdTipoProducto(int idTipoProducto)
        {
            if (idTipoProducto <= 0)
                throw new ArgumentException("El ID de tipo de producto debe ser positivo.", nameof(idTipoProducto));
            IdTipoProducto = idTipoProducto;
        }
    }
}