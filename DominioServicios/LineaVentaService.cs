using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class LineaVentaService
    {
        private readonly BuyJugadorContext _context;

        public LineaVentaService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<LineaVentaDTO>> GetLineasByVentaIdAsync(int idVenta)
        {
            var lineas = await _context.LineaVentas
                .Where(l => l.IdVenta == idVenta)
                .Include(l => l.IdProductoNavigation)
                .AsNoTracking()
                .ToListAsync();

            return lineas.Select(LineaVentaDTO.FromDominio).ToList();
        }


        public async Task<List<LineaVentaDTO>> GetAllAsync()
        {
            return await _context.LineaVentas
                .Select(l => LineaVentaDTO.FromDominio(l))
                .ToListAsync();
        }

        public async Task<LineaVentaDTO?> GetByIdAsync(int idVenta, int nroLineaVenta)
        {
            var entidad = await _context.LineaVentas.FindAsync(idVenta, nroLineaVenta);
            return LineaVentaDTO.FromDominio(entidad);
        }

        public async Task<LineaVentaDTO> CreateAsync(LineaVentaDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.LineaVentas.Add(entidad);
            await _context.SaveChangesAsync();
            return LineaVentaDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int idVenta, int nroLineaVenta, LineaVentaDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var lineaExistente = await _context.LineaVentas.FindAsync(idVenta, nroLineaVenta);
                if (lineaExistente == null)
                {
                    throw new System.Exception("La línea de venta no existe.");
                }

                var producto = await _context.Productos.FindAsync(lineaExistente.IdProducto);
                if (producto == null)
                {
                    throw new System.Exception("El producto asociado no existe.");
                }

                int diferenciaCantidad = dto.Cantidad - lineaExistente.Cantidad;

                if (producto.Stock < diferenciaCantidad)
                {
                    throw new System.Exception("Stock insuficiente para actualizar la cantidad.");
                }

                producto.Stock -= diferenciaCantidad;
                lineaExistente.Cantidad = dto.Cantidad;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task DeleteAsync(int idVenta, int nroLineaVenta)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var linea = await _context.LineaVentas.FindAsync(idVenta, nroLineaVenta);
                if (linea != null)
                {
                    if (linea.IdProducto.HasValue)
                    {
                        var producto = await _context.Productos.FindAsync(linea.IdProducto.Value);
                        if (producto != null)
                        {
                            producto.Stock += linea.Cantidad;
                        }
                    }
                    _context.LineaVentas.Remove(linea);
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
