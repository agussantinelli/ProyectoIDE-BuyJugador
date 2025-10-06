using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
                .Select(pc => new PrecioCompraDTO
                {
                    IdProducto = pc.IdProducto,
                    IdProveedor = pc.IdProveedor,
                    Monto = pc.Monto,
                    NombreProducto = pc.Producto.Nombre,
                    RazonSocialProveedor = pc.Proveedor.RazonSocial
                }).ToListAsync();
        }

        public async Task<List<PrecioCompraDTO>> GetByProductoAsync(int idProducto)
        {
            return await _context.PreciosCompra
                .Where(pc => pc.IdProducto == idProducto)
                .Select(pc => new PrecioCompraDTO
                {
                    IdProducto = pc.IdProducto,
                    IdProveedor = pc.IdProveedor,
                    Monto = pc.Monto,
                    NombreProducto = pc.Producto.Nombre,
                    RazonSocialProveedor = pc.Proveedor.RazonSocial
                }).ToListAsync();
        }

        // Obtener precios por proveedor
        public async Task<List<PrecioCompraDTO>> GetByProveedorAsync(int idProveedor)
        {
            return await _context.PreciosCompra
                .Where(pc => pc.IdProveedor == idProveedor)
                .Select(pc => new PrecioCompraDTO
                {
                    IdProducto = pc.IdProducto,
                    IdProveedor = pc.IdProveedor,
                    Monto = pc.Monto,
                    NombreProducto = pc.Producto.Nombre,
                    RazonSocialProveedor = pc.Proveedor.RazonSocial
                }).ToListAsync();
        }

        public async Task<PrecioCompraDTO> CreateOrUpdateAsync(PrecioCompraDTO dto)
        {
            var entity = await _context.PreciosCompra
                .FirstOrDefaultAsync(pc => pc.IdProducto == dto.IdProducto && pc.IdProveedor == dto.IdProveedor);

            if (entity == null)
            {
                entity = new PrecioCompra
                {
                    IdProducto = dto.IdProducto,
                    IdProveedor = dto.IdProveedor,
                    Monto = dto.Monto
                };
                _context.PreciosCompra.Add(entity);
            }
            else
            {
                entity.Monto = dto.Monto;
            }

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int idProducto, int idProveedor)
        {
            var entity = await _context.PreciosCompra
                .FirstOrDefaultAsync(pc => pc.IdProducto == idProducto && pc.IdProveedor == idProveedor);

            if (entity == null)
                return false;

            _context.PreciosCompra.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
