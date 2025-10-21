using Data;
using DominioModelo;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class LineaVentaService
    {
        private readonly UnitOfWork _unitOfWork;

        public LineaVentaService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LineaVentaDTO>> GetLineasByVentaIdAsync(int idVenta)
        {
            var lineas = await _unitOfWork.LineaVentaRepository.GetLineasByVentaIdAsync(idVenta);
            return lineas.Select(LineaVentaDTO.FromDominio).ToList();
        }

        public async Task<List<LineaVentaDTO>> GetAllAsync()
        {
            var lineas = await _unitOfWork.LineaVentaRepository.GetAllAsync();
            return lineas.Select(LineaVentaDTO.FromDominio).ToList();
        }

        public async Task<LineaVentaDTO?> GetByIdAsync(int idVenta, int nroLineaVenta)
        {
            var entidad = await _unitOfWork.LineaVentaRepository.GetByIdAsync(idVenta, nroLineaVenta);
            return entidad != null ? LineaVentaDTO.FromDominio(entidad) : null;
        }

        public async Task<LineaVentaDTO> CreateAsync(LineaVentaDTO dto)
        {
            var entidad = dto.ToDominio();
            await _unitOfWork.LineaVentaRepository.AddAsync(entidad);
            await _unitOfWork.SaveChangesAsync();
            return LineaVentaDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int idVenta, int nroLineaVenta, LineaVentaDTO dto)
        {
            var lineaExistente = await _unitOfWork.LineaVentaRepository.GetByIdAsync(idVenta, nroLineaVenta);
            if (lineaExistente == null)
            {
                throw new Exception("La línea de venta no existe.");
            }

            var producto = await _unitOfWork.ProductoRepository.GetByIdAsync(lineaExistente.IdProducto.Value);
            if (producto == null)
            {
                throw new Exception("El producto asociado no existe.");
            }

            int diferenciaCantidad = dto.Cantidad - lineaExistente.Cantidad;

            if (producto.Stock < diferenciaCantidad)
            {
                throw new Exception("Stock insuficiente para actualizar la cantidad.");
            }

            producto.Stock -= diferenciaCantidad;
            lineaExistente.Cantidad = dto.Cantidad;

            // #CAMBIO: No se actualiza la línea directamente, se confía en que el DbContext
            // #rastrea los cambios en la entidad 'lineaExistente'.
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int idVenta, int nroLineaVenta)
        {
            var linea = await _unitOfWork.LineaVentaRepository.GetByIdAsync(idVenta, nroLineaVenta);
            if (linea != null)
            {
                if (linea.IdProducto.HasValue)
                {
                    var producto = await _unitOfWork.ProductoRepository.GetByIdAsync(linea.IdProducto.Value);
                    if (producto != null)
                    {
                        producto.Stock += linea.Cantidad;
                    }
                }
                _unitOfWork.LineaVentaRepository.Remove(linea);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
