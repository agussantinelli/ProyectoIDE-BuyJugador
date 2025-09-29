using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class ProductoService
    {
        private readonly BuyJugadorContext _context;

        public ProductoService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            // SOLUCIÓN: Se reemplaza el Include por un Select explícito para asegurar la carga de datos relacionados.
            // Esto evita problemas con los filtros globales y la reconstrucción de objetos de EF.
            return await _context.Productos
                .Select(p => new Producto
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Stock = p.Stock,
                    Activo = p.Activo,
                    IdTipoProducto = p.IdTipoProducto,
                    IdTipoProductoNavigation = new TipoProducto
                    {
                        IdTipoProducto = p.IdTipoProductoNavigation.IdTipoProducto,
                        Descripcion = p.IdTipoProductoNavigation.Descripcion
                    },
                    Precios = p.Precios.Select(pr => new Precio
                    {
                        IdProducto = pr.IdProducto,
                        FechaDesde = pr.FechaDesde,
                        Monto = pr.Monto
                    }).ToList()
                })
                .ToListAsync();
        }


        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.IdTipoProductoNavigation)
                .Include(p => p.Precios)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<Producto> CreateAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task UpdateAsync(Producto producto)
        {
            var existingProducto = await _context.Productos.FindAsync(producto.IdProducto);
            if (existingProducto != null)
            {
                _context.Entry(existingProducto).CurrentValues.SetValues(producto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                producto.Activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}

