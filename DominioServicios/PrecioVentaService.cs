using Data;
using DominioModelo;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class PrecioVentaService
    {
        private readonly UnitOfWork _unitOfWork;

        public PrecioVentaService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<HistorialPrecioProductoDTO>> GetHistorialPreciosAsync()
        {
            var historial = await _unitOfWork.PrecioVentaRepository.GetHistorialPreciosAsync();

            return historial
                .GroupBy(h => new { h.IdProducto, h.Producto.Nombre })
                .Select(g => new HistorialPrecioProductoDTO
                {
                    IdProducto = g.Key.IdProducto,
                    NombreProducto = g.Key.Nombre,
                    Puntos = g.Select(p => new PrecioPuntoDTO
                    {
                        Fecha = p.FechaDesde,
                        Monto = p.Monto
                    }).ToList()
                })
                .ToList();
        }

        public async Task<PrecioVenta?> GetPrecioVigenteAsync(int idProducto)
        {
            return await _unitOfWork.PrecioVentaRepository.GetPrecioVigenteAsync(idProducto);
        }

        public async Task<List<PrecioVentaDTO>> GetAllAsync()
        {
            var precios = await _unitOfWork.PrecioVentaRepository.GetAllDetalladoAsync();
            return precios.Select(pv => new PrecioVentaDTO
            {
                IdProducto = pv.IdProducto,
                FechaDesde = pv.FechaDesde,
                Monto = pv.Monto,
                NombreProducto = pv.Producto.Nombre
            }).ToList();
        }

        public async Task<PrecioVentaDTO?> GetByIdAsync(int idProducto, DateTime fechaDesde)
        {
            var pv = await _unitOfWork.PrecioVentaRepository.GetByIdAsync(idProducto, fechaDesde);
            if (pv == null) return null;

            return new PrecioVentaDTO
            {
                IdProducto = pv.IdProducto,
                FechaDesde = pv.FechaDesde,
                Monto = pv.Monto,
                NombreProducto = pv.Producto.Nombre
            };
        }

        public async Task<PrecioVentaDTO> CreateAsync(PrecioVentaDTO dto)
        {
            var entity = new PrecioVenta
            {
                IdProducto = dto.IdProducto,
                FechaDesde = dto.FechaDesde.ToUniversalTime(),
                Monto = dto.Monto
            };
            await _unitOfWork.PrecioVentaRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return dto;
        }

        public async Task UpdateAsync(int idProducto, DateTime fechaDesde, PrecioVentaDTO dto)
        {
            var entity = await _unitOfWork.PrecioVentaRepository.GetByIdAsync(idProducto, fechaDesde);
            if (entity != null)
            {
                entity.Monto = dto.Monto;
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int idProducto, DateTime fechaDesde)
        {
            var entity = await _unitOfWork.PrecioVentaRepository.GetByIdAsync(idProducto, fechaDesde);
            if (entity != null)
            {
                _unitOfWork.PrecioVentaRepository.Remove(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
