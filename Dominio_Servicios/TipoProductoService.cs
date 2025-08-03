using Data;
using DominioModelo;
namespace DominioServicios
{
    public class TipoProductoService
    {
        public IEnumerable<TipoProducto> GetAll() => TipoProductoInMemory.TiposProducto;

        public TipoProducto? GetById(int id) => TipoProductoInMemory.TiposProducto.FirstOrDefault(t => t.IdTipoProducto == id);

        public TipoProducto Add(TipoProducto tipoProducto)
        {
            var nuevoId = TipoProductoInMemory.NextTipoProductoId;

            var nuevoTipo = new TipoProducto(nuevoId, tipoProducto.NombreTipoProducto);
            TipoProductoInMemory.TiposProducto.Add(nuevoTipo);
            return nuevoTipo;
        }

        public bool Update(int id, TipoProducto tipoProductoActualizado)
        {
            var tipoProductoExistente = GetById(id);
            if (tipoProductoExistente == null)
            {
                return false;
            }
            TipoProductoInMemory.TiposProducto.Remove(tipoProductoExistente);
            TipoProductoInMemory.TiposProducto.Add(tipoProductoActualizado);
            return true;
        }

        public bool Delete(int id)
        {
            var tipoProducto = GetById(id);
            if (tipoProducto == null)
            {
                return false;
            }
            TipoProductoInMemory.TiposProducto.Remove(tipoProducto);
            return true;
        }
    }
}
