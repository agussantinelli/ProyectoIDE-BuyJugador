using Data;
using DominioModelo;
using Dominio_Modelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class ProvinciaService
    {
        private readonly BuyJugadorContext _context;

        public ProvinciaService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Provincia>> GetAllAsync()
        {
            return await _context.Provincias.ToListAsync();
        }

        public async Task<Provincia?> GetByIdAsync(int id)
        {
            return await _context.Provincias.FindAsync(id);
        }
    }
}
