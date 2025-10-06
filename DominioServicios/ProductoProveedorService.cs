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

        public async Task<List<ProductoAsignadoDTO>> GetProductosByProveedorIdAsync(int idProveedor)
        {
            var productosAsignados = await _context.ProductoProveedores
                .Where(pp => pp.IdProveedor == idProveedor)
                .Select(pp => new ProductoAsignadoDTO
                {
                    IdProducto = pp.Producto.IdProducto,
                    Nombre = pp.Producto.Nombre,
                    Descripcion = pp.Producto.Descripcion,
                    PrecioCompra = pp.Producto.PreciosCompra
                                      .Where(pc => pc.IdProveedor == idProveedor)
                                      .Select(pc => pc.Monto)
                                      .FirstOrDefault()
                })
                .ToListAsync();

            return productosAsignados;
        }

        public async Task CreateAsync(ProductoProveedorDTO dto)
        {
            var newRelation = new DominioModelo.ProductoProveedor
            {
                IdProducto = dto.IdProducto,
                IdProveedor = dto.IdProveedor
            };
            await _context.ProductoProveedores.AddAsync(newRelation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int idProducto, int idProveedor)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var precioCompra = await _context.PreciosCompra
                    .FirstOrDefaultAsync(pc => pc.IdProducto == idProducto && pc.IdProveedor == idProveedor);

                if (precioCompra != null)
                {
                    _context.PreciosCompra.Remove(precioCompra);
                }

                var pp = await _context.ProductoProveedores
                    .FirstOrDefaultAsync(x => x.IdProducto == idProducto && x.IdProveedor == idProveedor);

                if (pp == null)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                _context.ProductoProveedores.Remove(pp);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

