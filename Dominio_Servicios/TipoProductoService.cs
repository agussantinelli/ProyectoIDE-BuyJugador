using System;
using System.Collections.Generic;
using System.Linq;
using DominioModelo;
using Data;

namespace DominioServicios
{
    public class TipoProductoService
    {
        public bool Add(TipoProducto tipoProducto)
        {
            if (TipoProductoInMemory.TiposProducto.Any(tp => tp.NombreTipoProducto.Equals(tipoProducto.NombreTipoProducto, StringComparison.OrdinalIgnoreCase)))
                return false;

            tipoProducto.SetIdTipoProducto(GetNextId());
            TipoProductoInMemory.TiposProducto.Add(tipoProducto);
            return true;
        }

        public bool Delete(int id)
        {
            var tipoProductoToDelete = TipoProductoInMemory.TiposProducto.FirstOrDefault(x => x.IdTipoProducto == id);
            if (tipoProductoToDelete != null)
            {
                TipoProductoInMemory.TiposProducto.Remove(tipoProductoToDelete);
                return true;
            }
            return false;
        }

        public TipoProducto? Get(int id)
        {
            return TipoProductoInMemory.TiposProducto.FirstOrDefault(x => x.IdTipoProducto == id);
        }

        public IReadOnlyList<TipoProducto> GetAll()
        {
            return TipoProductoInMemory.TiposProducto.AsReadOnly();
        }

        public bool Update(TipoProducto tipoProducto)
        {
            var tipoProductoToUpdate = TipoProductoInMemory.TiposProducto.FirstOrDefault(x => x.IdTipoProducto == tipoProducto.IdTipoProducto);
            if (tipoProductoToUpdate != null)
            {
                tipoProductoToUpdate.SetNombreTipoProducto(tipoProducto.NombreTipoProducto);
                return true;
            }
            return false;
        }

        private static int GetNextId()
        {
            return TipoProductoInMemory.TiposProducto.Any() ? TipoProductoInMemory.TiposProducto.Max(x => x.IdTipoProducto) + 1 : 1;
        }
    }
}
