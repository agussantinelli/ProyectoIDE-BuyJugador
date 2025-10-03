using Data;
using DominioModelo;
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
            return await _context.ProductoProveedores
                .Where(pp => pp.IdProveedor == idProveedor)
                .Select(pp => pp.Producto)
                .Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Stock = p.Stock,
                    PrecioActual = p.Precios.OrderByDescending(pr => pr.FechaDesde).FirstOrDefault().Monto
                }).ToListAsync();
        }

        public async Task UpdateProductosProveedorAsync(ProductoProveedorDTO dto)
        {
            var existentes = await _context.ProductoProveedores
                .Where(pp => pp.IdProveedor == dto.IdProveedor)
                .ToListAsync();

            _context.ProductoProveedores.RemoveRange(existentes);

            var nuevasRelaciones = dto.IdsProducto
                .Select(idProd => new ProductoProveedor { IdProveedor = dto.IdProveedor, IdProducto = idProd });

            await _context.ProductoProveedores.AddRangeAsync(nuevasRelaciones);
            await _context.SaveChangesAsync();
        }
    }
}
