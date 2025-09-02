using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class VentaService
    {
        private readonly BuyJugadorContext _context;

        public VentaService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<VentaDTO>> GetAllAsync()
        {
            var entidades = await _context.Ventas.ToListAsync();
            return entidades.Select(e => VentaDTO.FromDominio(e)).ToList();
        }

        public async Task<VentaDTO?> GetByIdAsync(int id)
        {
            var entidad = await _context.Ventas.FindAsync(id);
            return VentaDTO.FromDominio(entidad);
        }

        public async Task<VentaDTO> CreateAsync(VentaDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.Ventas.Add(entidad);
            await _context.SaveChangesAsync();
            return VentaDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int id, VentaDTO dto)
        {
            var entidad = await _context.Ventas.FindAsync(id);
            if (entidad != null)
            {
                entidad.Fecha = dto.Fecha;
                entidad.Estado = dto.Estado;
                entidad.IdPersona = dto.IdPersona;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.Ventas.FindAsync(id);
            if (entidad != null)
            {
                _context.Ventas.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
