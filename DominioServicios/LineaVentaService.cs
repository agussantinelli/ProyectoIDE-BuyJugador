using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class LineaVentaService
    {
        private readonly BuyJugadorContext _context;

        public LineaVentaService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<LineaVentaDTO>> GetAllAsync()
        {
            var entidades = await _context.LineaVentas.ToListAsync();
            return entidades.Select(e => LineaVentaDTO.FromDominio(e)).ToList();
        }

        public async Task<LineaVentaDTO?> GetByIdAsync(int idVenta, int nroLineaVenta)
        {
            var entidad = await _context.LineaVentas.FindAsync(idVenta, nroLineaVenta);
            return LineaVentaDTO.FromDominio(entidad);
        }

        public async Task<LineaVentaDTO> CreateAsync(LineaVentaDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.LineaVentas.Add(entidad);
            await _context.SaveChangesAsync();
            return LineaVentaDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int idVenta, int nroLineaVenta, LineaVentaDTO dto)
        {
            var entidad = await _context.LineaVentas.FindAsync(idVenta, nroLineaVenta);
            if (entidad != null)
            {
                entidad.Cantidad = dto.Cantidad;
                entidad.IdProducto = dto.IdProducto;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int idVenta, int nroLineaVenta)
        {
            var entidad = await _context.LineaVentas.FindAsync(idVenta, nroLineaVenta);
            if (entidad != null)
            {
                _context.LineaVentas.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
