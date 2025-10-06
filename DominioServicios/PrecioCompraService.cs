using Data;
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
                .Include(pc => pc.Producto)
                .Include(pc => pc.Proveedor)
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
                .Include(pc => pc.Producto)
                .Include(pc => pc.Proveedor)
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

        public async Task<List<PrecioCompraDTO>> GetByProveedorAsync(int idProveedor)
        {
            return await _context.PreciosCompra
                .Include(pc => pc.Producto)
                .Include(pc => pc.Proveedor)
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

        public async Task<PrecioCompraDTO?> GetByIdAsync(int idProducto, int idProveedor)
        {
            var e = await _context.PreciosCompra
                .Include(pc => pc.Producto)
                .Include(pc => pc.Proveedor)
                .FirstOrDefaultAsync(pc => pc.IdProducto == idProducto && pc.IdProveedor == idProveedor);

            return PrecioCompraDTO.FromDominio(e);
        }

        // upsert (sin historial)
        public async Task<PrecioCompraDTO> CreateOrUpdateAsync(PrecioCompraDTO dto)
        {
            var e = await _context.PreciosCompra.FindAsync(dto.IdProducto, dto.IdProveedor);
            if (e == null)
            {
                e = dto.ToDominio();
                _context.PreciosCompra.Add(e);
            }
            else
            {
                e.Monto = dto.Monto;
            }

            await _context.SaveChangesAsync();

            var full = await _context.PreciosCompra
                .Include(pc => pc.Producto)
                .Include(pc => pc.Proveedor)
                .FirstAsync(pc => pc.IdProducto == dto.IdProducto && pc.IdProveedor == dto.IdProveedor);

            return PrecioCompraDTO.FromDominio(full)!;
        }

        public async Task<bool> DeleteAsync(int idProducto, int idProveedor)
        {
            var e = await _context.PreciosCompra.FindAsync(idProducto, idProveedor);
            if (e == null) return false;

            _context.PreciosCompra.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
