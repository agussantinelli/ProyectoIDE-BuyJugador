using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class VentaRepository
    {
        private readonly BuyJugadorContext _context;

        public VentaRepository(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Venta venta)
        {
            await _context.Ventas.AddAsync(venta);
        }

        public async Task<Venta?> GetByIdParaUpdateAsync(int id)
        {
            return await _context.Ventas
               .Include(v => v.LineaVenta)
               .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

        public IQueryable<Venta> GetVentas()
        {
            return _context.Ventas.Include(v => v.IdPersonaNavigation);
        }

        public async Task<Venta?> GetVentaByIdAsync(int id)
        {
            return await _context.Ventas
                .Include(v => v.IdPersonaNavigation)
                .Include(v => v.LineaVenta)
                    .ThenInclude(lv => lv.IdProductoNavigation)
                        .ThenInclude(p => p.PreciosVenta)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

        public void Remove(Venta venta)
        {
            _context.Ventas.Remove(venta);
        }

        public async Task<Venta?> FindAsync(int id)
        {
            return await _context.Ventas.FindAsync(id);
        }

        public async Task<decimal> GetTotalVentasEnRangoAsync(DateTime desde, DateTime hasta)
        {
            return await _context.Ventas
                .Where(v => v.Fecha >= desde && v.Fecha < hasta && v.Estado == "Finalizada")
                .SelectMany(v => v.LineaVenta)
                .SumAsync(lv => (decimal?)lv.Cantidad * lv.PrecioUnitario) ?? 0m;
        }

        public void RemoveLineas(IEnumerable<LineaVenta> lineas)
        {
            _context.LineaVentas.RemoveRange(lineas);
        }
    }
}
