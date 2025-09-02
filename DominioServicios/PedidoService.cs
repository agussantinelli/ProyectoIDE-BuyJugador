using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class PedidoService
    {
        private readonly BuyJugadorContext _context;

        public PedidoService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<PedidoDTO>> GetAllAsync()
        {
            var entidades = await _context.Pedidos.ToListAsync();
            return entidades.Select(e => PedidoDTO.FromDominio(e)).ToList();
        }

        public async Task<PedidoDTO?> GetByIdAsync(int id)
        {
            var entidad = await _context.Pedidos.FindAsync(id);
            return PedidoDTO.FromDominio(entidad);
        }

        public async Task<PedidoDTO> CreateAsync(PedidoDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.Pedidos.Add(entidad);
            await _context.SaveChangesAsync();
            return PedidoDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int id, PedidoDTO dto)
        {
            var entidad = await _context.Pedidos.FindAsync(id);
            if (entidad != null)
            {
                entidad.Fecha = dto.Fecha;
                entidad.Estado = dto.Estado;
                entidad.IdProveedor = dto.IdProveedor;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.Pedidos.FindAsync(id);
            if (entidad != null)
            {
                _context.Pedidos.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
