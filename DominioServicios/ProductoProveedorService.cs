using Data;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class ProductoProveedorService
    {
        private readonly BuyJugadorContext _context;

        public ProductoProveedorService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoDTO>> GetProductosByProveedorIdAsync(int idProveedor)
        {
            var productos = await _context.ProductoProveedores
                .Where(pp => pp.IdProveedor == idProveedor)
                .Select(pp => pp.Producto)
                .Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Stock = p.Stock,
                    IdTipoProducto = p.IdTipoProducto,
                    TipoProductoDescripcion = p.IdTipoProductoNavigation.Descripcion,
                    PrecioActual = p.PreciosVenta
                        .OrderByDescending(pr => pr.FechaDesde)
                        .FirstOrDefault().Monto
                })
                .ToListAsync();

            return productos;
        }

        public async Task UpdateProductosProveedorAsync(ProductoProveedorDTO dto)
        {
            var existing = await _context.ProductoProveedores
                .IgnoreQueryFilters()
                .Where(pp => pp.IdProveedor == dto.IdProveedor)
                .ToListAsync();

            _context.ProductoProveedores.RemoveRange(existing);

            if (dto.IdsProducto.Any())
            {
                var newRelations = dto.IdsProducto.Select(idProducto => new DominioModelo.ProductoProveedor
                {
                    IdProveedor = dto.IdProveedor,
                    IdProducto = idProducto
                });
                await _context.ProductoProveedores.AddRangeAsync(newRelations);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int idProducto, int idProveedor)
        {
            var pp = await _context.ProductoProveedores
                .FirstOrDefaultAsync(x => x.IdProducto == idProducto && x.IdProveedor == idProveedor);

            if (pp == null) return false;

            _context.ProductoProveedores.Remove(pp);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
