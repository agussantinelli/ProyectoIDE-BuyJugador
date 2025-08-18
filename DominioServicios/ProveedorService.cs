using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class ProveedorService
    {
        private readonly BuyJugadorContext _context;

        public ProveedorService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Proveedor>> GetAllAsync()
        {
            return await _context.Proveedores.ToListAsync();
        }

        public async Task<Proveedor?> GetByIdAsync(int id)
        {
            return await _context.Proveedores.FindAsync(id);
        }

        public async Task<Proveedor> CreateAsync(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task UpdateAsync(int id, Proveedor proveedor)
        {
            var existing = await _context.Proveedores.FindAsync(id);
            if (existing != null)
            {
                existing.Nombre = proveedor.Nombre;
                existing.Cuit = proveedor.Cuit;
                existing.Email = proveedor.Email;
                // Actualiza las demás propiedades...
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await _context.Proveedores.FindAsync(id);
            if (toDelete != null)
            {
                _context.Proveedores.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
