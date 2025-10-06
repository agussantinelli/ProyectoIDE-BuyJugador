using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task UpdateProductosProveedorAsync(int idProveedor, List<int> idProductos)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _context.ProductoProveedores
                    .Where(pp => pp.IdProveedor == idProveedor)
                    .ToListAsync();

                _context.ProductoProveedores.RemoveRange(existing);
                await _context.SaveChangesAsync(); 

                if (idProductos != null && idProductos.Any())
                {
                    var newRelations = idProductos.Select(idProducto => new DominioModelo.ProductoProveedor
                    {
                        IdProveedor = idProveedor,
                        IdProducto = idProducto
                    });
                    await _context.ProductoProveedores.AddRangeAsync(newRelations);
                }

                await _context.SaveChangesAsync(); 
                await transaction.CommitAsync(); 
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(); 
                throw;
            }
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

