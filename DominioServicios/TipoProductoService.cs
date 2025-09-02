using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;

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
            var entidades = await _context.TiposProductos.ToListAsync();
            return entidades.Select(e => TipoProductoDTO.FromDominio(e)).ToList();
        }

        public async Task<TipoProductoDTO?> GetByIdAsync(int id)
        {
            var entidad = await _context.TiposProductos.FindAsync(id);
            return TipoProductoDTO.FromDominio(entidad);
        }

        public async Task<TipoProductoDTO> CreateAsync(TipoProductoDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.TiposProductos.Add(entidad);
            await _context.SaveChangesAsync();
            return TipoProductoDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int id, TipoProductoDTO dto)
        {
            var entidad = await _context.TiposProductos.FindAsync(id);
            if (entidad != null)
            {
                entidad.Descripcion = dto.Descripcion;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.TiposProductos.FindAsync(id);
            if (entidad != null)
            {
                _context.TiposProductos.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
