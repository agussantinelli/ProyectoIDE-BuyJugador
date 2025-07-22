using System;
using System.Collections.Generic;
using System.Linq;
using Dominio_Modelo;
using Data;

namespace Dominio_Servicios
{
    public class ProductoService
    {
        public bool Add(Producto producto)
        {
            if (ProductoInMemory.Productos.Any(p => p.NombreProducto.Equals(producto.NombreProducto, StringComparison.OrdinalIgnoreCase)))
                return false; 

            producto.SetIdProducto(GetNextId());
            ProductoInMemory.Productos.Add(producto);
            return true;
        }

        public bool Delete(int id)
        {
            var productoToDelete = ProductoInMemory.Productos.FirstOrDefault(x => x.IdProducto == id);
            if (productoToDelete != null)
            {
                ProductoInMemory.Productos.Remove(productoToDelete);
                return true;
            }
            return false;
        }

        public Producto? Get(int id)
        {
            return ProductoInMemory.Productos.FirstOrDefault(x => x.IdProducto == id);
        }

        public IEnumerable<Producto> GetAll()
        {
            return ProductoInMemory.Productos.AsReadOnly();
        }

        public bool Update(Producto producto)
        {
            var productoToUpdate = ProductoInMemory.Productos.FirstOrDefault(x => x.IdProducto == producto.IdProducto);
            if (productoToUpdate != null)
            {
                productoToUpdate.SetNombreProducto(producto.NombreProducto);
                productoToUpdate.SetDescripcionProducto(producto.DescripcionProducto);
                productoToUpdate.SetStock(producto.Stock);
                productoToUpdate.SetIdTipoProducto(producto.IdTipoProducto);
                return true;
            }
            return false;
        }

        private static int GetNextId()
        {
            return ProductoInMemory.Productos.Any() ? ProductoInMemory.Productos.Max(x => x.IdProducto) + 1 : 1;
        }
    }
}
