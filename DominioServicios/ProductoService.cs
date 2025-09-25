using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class ProductoService
    {
        private readonly BuyJugadorContext _context;

        public ProductoService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoDTO>> GetAllAsync()
        {
            var entidades = await _context.Productos.ToListAsync();
            return entidades.Select(e => ProductoDTO.FromDominio(e)).ToList();
        }

        public async Task<ProductoDTO?> GetByIdAsync(int id)
        {
            var entidad = await _context.Productos.FindAsync(id);
            return ProductoDTO.FromDominio(entidad);
        }

        public async Task<ProductoDTO> CreateAsync(ProductoDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.Productos.Add(entidad);
            await _context.SaveChangesAsync();
            return ProductoDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int id, ProductoDTO dto)
        {
            var entidad = await _context.Productos.FindAsync(id);
            if (entidad != null)
            {
                entidad.Nombre = dto.Nombre;
                entidad.Descripcion = dto.Descripcion;
                entidad.Stock = dto.Stock;
                entidad.IdTipoProducto = dto.IdTipoProducto;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.Productos.FindAsync(id);
            if (entidad != null)
            {
                entidad.Activo = false;
                await _context.SaveChangesAsync();
            }
        }

    }
}
