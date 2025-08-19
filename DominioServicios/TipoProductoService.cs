using Data;
using DominioModelo;
using Dominio_Modelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class TipoProductoService
    {
        private readonly BuyJugadorContext _context;

        public TipoProductoService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<TipoProducto>> GetAllAsync()
        {
            return await _context.TiposProducto.ToListAsync();
        }

        public async Task<TipoProducto?> GetByIdAsync(int id)
        {
            return await _context.TiposProducto.FindAsync(id);
        }
    }
}
