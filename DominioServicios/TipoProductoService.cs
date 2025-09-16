using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class TipoProductoService
    {
        private readonly BuyJugadorContext _context;

        public TipoProductoService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<TipoProductoDTO>> GetAllAsync()
        {
            return await _context.TiposProductos
                .Select(tp => new TipoProductoDTO { IdTipoProducto = tp.IdTipoProducto, Descripcion = tp.Descripcion })
                .ToListAsync();
        }

        public async Task<TipoProductoDTO?> GetByIdAsync(int id)
        {
            var tipoProducto = await _context.TiposProductos.FindAsync(id);
            return tipoProducto != null ? new TipoProductoDTO { IdTipoProducto = tipoProducto.IdTipoProducto, Descripcion = tipoProducto.Descripcion } : null;
        }

        public async Task<TipoProductoDTO> CreateAsync(TipoProductoDTO tipoProductoDto)
        {
            var tipoProducto = new TipoProducto { Descripcion = tipoProductoDto.Descripcion };
            _context.TiposProductos.Add(tipoProducto);
            await _context.SaveChangesAsync();
            tipoProductoDto.IdTipoProducto = tipoProducto.IdTipoProducto;
            return tipoProductoDto;
        }

        public async Task<bool> UpdateAsync(int id, TipoProductoDTO tipoProductoDto)
        {
            var tipoProducto = await _context.TiposProductos.FindAsync(id);
            if (tipoProducto == null) return false;
            tipoProducto.Descripcion = tipoProductoDto.Descripcion;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tipoProducto = await _context.TiposProductos
                .Include(tp => tp.Productos)
                .FirstOrDefaultAsync(tp => tp.IdTipoProducto == id);

            if (tipoProducto == null)
                return false;

            if (tipoProducto.Productos != null && tipoProducto.Productos.Any())
                throw new InvalidOperationException("No se puede eliminar el tipo de producto porque tiene productos asociados.");

            _context.TiposProductos.Remove(tipoProducto);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

