using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProvinciaRepository
    {
        private readonly BuyJugadorContext _context;

        public ProvinciaRepository(BuyJugadorContext context)
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

        public async Task AddAsync(Provincia entity)
        {
            await _context.Provincias.AddAsync(entity);
        }

        public void Update(Provincia entity)
        {
            _context.Provincias.Update(entity);
        }

        public void Remove(Provincia entity)
        {
            _context.Provincias.Remove(entity);
        }
    }
}
