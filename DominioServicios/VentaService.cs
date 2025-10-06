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
    public class VentaService
    {
        private readonly BuyJugadorContext _context;

        public VentaService(BuyJugadorContext context)
        {
            _context = context;
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
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

        public async Task<Venta> CrearVentaCompletaAsync(CrearVentaCompletaDTO dto)
        {
            var nuevaVenta = new Venta
            {
                IdPersona = dto.IdPersona,
                Fecha = DateTime.UtcNow,
                Estado = dto.Finalizada ? "Finalizada" : "Pendiente",
                LineaVenta = dto.Lineas.Select((l, index) => new LineaVenta
                {
                    NroLineaVenta = index + 1,
                    IdProducto = l.IdProducto,
                    Cantidad = l.Cantidad
                }).ToList()
            };

            _context.Ventas.Add(nuevaVenta);
            await _context.SaveChangesAsync();
            return nuevaVenta;
        }

        public async Task UpdateVentaCompletaAsync(CrearVentaCompletaDTO dto)
        {
            var ventaExistente = await _context.Ventas.Include(v => v.LineaVenta).FirstOrDefaultAsync(v => v.IdVenta == dto.IdVenta);
            if (ventaExistente == null)
            {
                throw new KeyNotFoundException("Venta no encontrada.");
            }

            _context.LineaVentas.RemoveRange(ventaExistente.LineaVenta);

            ventaExistente.LineaVenta = dto.Lineas.Select((l, index) => new LineaVenta
            {
                NroLineaVenta = index + 1,
                IdProducto = l.IdProducto,
                Cantidad = l.Cantidad
            }).ToList();

            if (dto.Finalizada)
            {
                ventaExistente.Estado = "Finalizada";
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteVentaAsync(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return false;
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
