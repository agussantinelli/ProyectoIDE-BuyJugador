using Data;
using DominioModelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DominioServicio
{
    public class TipoProductoService
    {
        public void Add(TipoProducto tipoProducto)
        {
            // Validar que el ID no esté duplicado
            if (TipoProductoInMemory.TiposProducto.Any(t => t.IdTipoProducto == tipoProducto.IdTipoProducto))
            {
                throw new ArgumentException($"El tipo de producto con ID {tipoProducto.IdTipoProducto} ya existe.");
            }

            TipoProductoInMemory.TiposProducto.Add(tipoProducto);
        }

        public bool Delete(int id)
        {
            var tipoProductoToDelete = TipoProductoInMemory.TiposProducto.Find(t => t.IdTipoProducto == id);
            if (tipoProductoToDelete != null)
            {
                TipoProductoInMemory.TiposProducto.Remove(tipoProductoToDelete);
                return true;
            }
            return false;
        }

        public TipoProducto Get(int id)
        {
            return TipoProductoInMemory.TiposProducto.FirstOrDefault(t => t.IdTipoProducto == id);
        }

        public IEnumerable<TipoProducto> GetAll()
        {
            return TipoProductoInMemory.TiposProducto.ToList();
        }

        public bool Update(TipoProducto tipoProducto)
        {
            var tipoProductoToUpdate = TipoProductoInMemory.TiposProducto.Find(t => t.IdTipoProducto == tipoProducto.IdTipoProducto);
            if (tipoProductoToUpdate != null)
            {
                tipoProductoToUpdate.SetNombreTipoProducto(tipoProducto.NombreTipoProducto);
                return true;
            }
            return false;
        }
    }
}
