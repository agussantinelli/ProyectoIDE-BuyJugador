using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class LineaPedidoService
    {
        private readonly BuyJugadorContext _context;

        public LineaPedidoService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<LineaPedidoDTO>> GetAllAsync()
        {
            var entidades = await _context.LineaPedidos.ToListAsync();
            return entidades.Select(e => LineaPedidoDTO.FromDominio(e)).ToList();
        }

        public async Task<LineaPedidoDTO?> GetByIdAsync(int idPedido, int nroLineaPedido)
        {
            var entidad = await _context.LineaPedidos.FindAsync(idPedido, nroLineaPedido);
            return LineaPedidoDTO.FromDominio(entidad);
        }

        public async Task<LineaPedidoDTO> CreateAsync(LineaPedidoDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.LineaPedidos.Add(entidad);
            await _context.SaveChangesAsync();
            return LineaPedidoDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int idPedido, int nroLineaPedido, LineaPedidoDTO dto)
        {
            var entidad = await _context.LineaPedidos.FindAsync(idPedido, nroLineaPedido);
            if (entidad != null)
            {
                entidad.Cantidad = dto.Cantidad;
                entidad.IdProducto = dto.IdProducto;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int idPedido, int nroLineaPedido)
        {
            var entidad = await _context.LineaPedidos.FindAsync(idPedido, nroLineaPedido);
            if (entidad != null)
            {
                _context.LineaPedidos.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
