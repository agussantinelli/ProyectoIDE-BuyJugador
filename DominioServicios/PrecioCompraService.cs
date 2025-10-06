using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class PrecioCompraService
    {
        private readonly BuyJugadorContext _context;

        public PrecioCompraService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<PrecioCompraDTO>> GetAllAsync()
        {
            return await _context.PreciosCompra
                .Include(pc => pc.Producto)
                .Include(pc => pc.Proveedor)
                .Select(pc => new PrecioCompraDTO
                {
                    IdProducto = pc.IdProducto,
                    IdProveedor = pc.IdProveedor,
                    Monto = pc.Monto,
                    NombreProducto = pc.Producto.Nombre,
                    RazonSocialProveedor = pc.Proveedor.RazonSocial
                })
                .ToListAsync();
        }

        public async Task<PrecioCompraDTO?> GetByIdAsync(int idProducto, int idProveedor)
        {
            var pc = await _context.PreciosCompra
                .Include(p => p.Producto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(p => p.IdProducto == idProducto && p.IdProveedor == idProveedor);

            if (pc == null) return null;

            return new PrecioCompraDTO
            {
                IdProducto = pc.IdProducto,
                IdProveedor = pc.IdProveedor,
                Monto = pc.Monto,
                NombreProducto = pc.Producto.Nombre,
                RazonSocialProveedor = pc.Proveedor.RazonSocial
            };
        }

        public async Task<PrecioCompraDTO> CreateAsync(PrecioCompraDTO dto)
        {
            var entity = new PrecioCompra
            {
                IdProducto = dto.IdProducto,
                IdProveedor = dto.IdProveedor,
                Monto = dto.Monto
            };

            _context.PreciosCompra.Add(entity);
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task UpdateAsync(int idProducto, int idProveedor, PrecioCompraDTO dto)
        {
            var entity = await _context.PreciosCompra
                .FirstOrDefaultAsync(p => p.IdProducto == idProducto && p.IdProveedor == idProveedor);

            if (entity != null)
            {
                entity.Monto = dto.Monto;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int idProducto, int idProveedor)
        {
            var entity = await _context.PreciosCompra
                .FirstOrDefaultAsync(p => p.IdProducto == idProducto && p.IdProveedor == idProveedor);

            if (entity != null)
            {
                _context.PreciosCompra.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
