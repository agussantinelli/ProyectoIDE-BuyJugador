using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class VentaService
    {
        private readonly UnitOfWork _unitOfWork;

        public VentaService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private DateTime GetCurrentArgentinaTime()
        {
            try
            {
                string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? "Argentina Standard Time"
                    : "America/Argentina/Buenos_Aires";
                TimeZoneInfo argentinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, argentinaTimeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                return DateTime.UtcNow.AddHours(-3);
            }
        }

        public async Task<Venta> CrearVentaCompletaAsync(CrearVentaCompletaDTO dto)
        {
            var nuevaVenta = new Venta
            {
                IdPersona = dto.IdPersona,
                Fecha = GetCurrentArgentinaTime(),
                Estado = dto.Finalizada ? "Finalizada" : "Pendiente",
            };
            await _unitOfWork.VentaRepository.AddAsync(nuevaVenta);
            await _unitOfWork.SaveChangesAsync();

            var idsProductos = dto.Lineas.Where(l => l.IdProducto.HasValue).Select(l => l.IdProducto!.Value).Distinct().ToList();
            var productosAfectados = (await _unitOfWork.ProductoRepository.GetByIdsAsync(idsProductos))
                                        .ToDictionary(p => p.IdProducto);

            int nroLineaCounter = 1;
            foreach (var lineaDto in dto.Lineas)
            {
                if (!lineaDto.IdProducto.HasValue || !productosAfectados.TryGetValue(lineaDto.IdProducto.Value, out var producto))
                {
                    throw new InvalidOperationException($"El producto ID {lineaDto.IdProducto} no es válido o no está activo.");
                }

                if (producto.Stock < lineaDto.Cantidad)
                {
                    throw new InvalidOperationException($"Stock insuficiente para '{producto.Nombre}'. Disponible: {producto.Stock}, Solicitado: {lineaDto.Cantidad}.");
                }

                var precioVigente = producto.PreciosVenta
                    .OrderByDescending(p => p.FechaDesde)
                    .FirstOrDefault()?.Monto;

                if (!precioVigente.HasValue)
                {
                    throw new InvalidOperationException($"El producto '{producto.Nombre}' no tiene un precio de venta vigente.");
                }

                nuevaVenta.LineaVenta.Add(new LineaVenta
                {
                    IdVenta = nuevaVenta.IdVenta,
                    NroLineaVenta = nroLineaCounter++,
                    IdProducto = lineaDto.IdProducto,
                    Cantidad = lineaDto.Cantidad,
                    PrecioUnitario = precioVigente.Value
                });

                producto.Stock -= lineaDto.Cantidad;
            }

            await _unitOfWork.SaveChangesAsync();
            return nuevaVenta;
        }

        public async Task UpdateVentaCompletaAsync(CrearVentaCompletaDTO dto)
        {
            var ventaExistente = await _unitOfWork.VentaRepository.GetByIdParaUpdateAsync(dto.IdVenta);
            if (ventaExistente == null)
                throw new KeyNotFoundException("La venta que intenta modificar no existe.");

            if (ventaExistente.Estado.Equals("Finalizada", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede modificar una venta que ya ha sido finalizada.");

            var cantidadesOriginales = ventaExistente.LineaVenta.ToDictionary(l => l.IdProducto.Value, l => l.Cantidad);
            var cantidadesNuevas = dto.Lineas.ToDictionary(l => l.IdProducto.Value, l => l.Cantidad);
            var idsProductosAfectados = cantidadesOriginales.Keys.Union(cantidadesNuevas.Keys).ToList();

            var productosAfectados = (await _unitOfWork.ProductoRepository.GetByIdsAsync(idsProductosAfectados))
                                        .ToDictionary(p => p.IdProducto);

            foreach (var idProducto in idsProductosAfectados)
            {
                if (!productosAfectados.TryGetValue(idProducto, out var producto)) continue;

                var cantidadOriginal = cantidadesOriginales.GetValueOrDefault(idProducto, 0);
                var cantidadNueva = cantidadesNuevas.GetValueOrDefault(idProducto, 0);
                var diferencia = cantidadNueva - cantidadOriginal;

                var stockDisponible = producto.Stock + cantidadOriginal;
                if (stockDisponible < cantidadNueva)
                {
                    throw new InvalidOperationException($"Stock insuficiente para '{producto.Nombre}'. Disponible: {stockDisponible}, Solicitado: {cantidadNueva}.");
                }
                producto.Stock -= diferencia;
            }

            _unitOfWork.VentaRepository.RemoveLineas(ventaExistente.LineaVenta);

            int nroLineaCounter = 1;
            foreach (var lineaDto in dto.Lineas)
            {
                ventaExistente.LineaVenta.Add(new LineaVenta
                {
                    IdVenta = ventaExistente.IdVenta,
                    NroLineaVenta = nroLineaCounter++,
                    IdProducto = lineaDto.IdProducto,
                    Cantidad = lineaDto.Cantidad,
                    PrecioUnitario = lineaDto.PrecioUnitario
                });
            }

            if (dto.Finalizada)
            {
                ventaExistente.Estado = "Finalizada";
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public IQueryable<Venta> GetVentas()
        {
            return _unitOfWork.VentaRepository.GetVentas();
        }

        public async Task<Venta?> GetVentaByIdAsync(int id)
        {
            return await _unitOfWork.VentaRepository.GetVentaByIdAsync(id);
        }

        public async Task<bool> DeleteVentaAsync(int id)
        {
            var venta = await _unitOfWork.VentaRepository.FindAsync(id);
            if (venta == null) return false;

            _unitOfWork.VentaRepository.Remove(venta);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetTotalVentasHoyAsync()
        {
            var ahora = GetCurrentArgentinaTime();
            var hoy = ahora.Date;
            var mañana = hoy.AddDays(1);
            return await _unitOfWork.VentaRepository.GetTotalVentasEnRangoAsync(hoy, mañana);
        }
    }
}
