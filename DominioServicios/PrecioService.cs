using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class PrecioService
    {
        private readonly BuyJugadorContext _context;

        public PrecioService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<PrecioDTO>> GetAllAsync()
        {
            var entidades = await _context.Precios.ToListAsync();
            return entidades.Select(e => PrecioDTO.FromDominio(e)).ToList();
        }

        public async Task<PrecioDTO?> GetByIdAsync(int idProducto, DateTime fechaDesde)
        {
            var entidad = await _context.Precios.FindAsync(idProducto, fechaDesde);
            return PrecioDTO.FromDominio(entidad);
        }

        public async Task<PrecioDTO> CreateAsync(PrecioDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.Precios.Add(entidad);
            await _context.SaveChangesAsync();
            return PrecioDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int idProducto, DateTime fechaDesde, PrecioDTO dto)
        {
            var entidad = await _context.Precios.FindAsync(idProducto, fechaDesde);
            if (entidad != null)
            {
                entidad.Monto = dto.Monto;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int idProducto, DateTime fechaDesde)
        {
            var entidad = await _context.Precios.FindAsync(idProducto, fechaDesde);
            if (entidad != null)
            {
                _context.Precios.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
