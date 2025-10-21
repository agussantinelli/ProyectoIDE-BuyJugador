using Data;
using DominioModelo;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class TipoProductoService
    {
        private readonly UnitOfWork _unitOfWork;

        public TipoProductoService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TipoProductoDTO>> GetAllAsync()
        {
            var tipos = await _unitOfWork.TipoProductoRepository.GetAllAsync();
            return tipos.Select(tp => new TipoProductoDTO { IdTipoProducto = tp.IdTipoProducto, Descripcion = tp.Descripcion }).ToList();
        }

        public async Task<TipoProductoDTO?> GetByIdAsync(int id)
        {
            var tipoProducto = await _unitOfWork.TipoProductoRepository.GetByIdAsync(id);
            return tipoProducto != null ? new TipoProductoDTO { IdTipoProducto = tipoProducto.IdTipoProducto, Descripcion = tipoProducto.Descripcion } : null;
        }

        public async Task<TipoProductoDTO> CreateAsync(TipoProductoDTO tipoProductoDto)
        {
            var tipoProducto = new TipoProducto { Descripcion = tipoProductoDto.Descripcion };
            await _unitOfWork.TipoProductoRepository.AddAsync(tipoProducto);
            await _unitOfWork.SaveChangesAsync();
            tipoProductoDto.IdTipoProducto = tipoProducto.IdTipoProducto;
            return tipoProductoDto;
        }

        public async Task<bool> UpdateAsync(int id, TipoProductoDTO tipoProductoDto)
        {
            var tipoProducto = await _unitOfWork.TipoProductoRepository.GetByIdAsync(id);
            if (tipoProducto == null) return false;

            tipoProducto.Descripcion = tipoProductoDto.Descripcion;
            _unitOfWork.TipoProductoRepository.Update(tipoProducto);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tipoProducto = await _unitOfWork.TipoProductoRepository.GetByIdConProductosAsync(id);
            if (tipoProducto == null) return false;

            if (tipoProducto.Productos != null && tipoProducto.Productos.Any())
                throw new InvalidOperationException("No se puede eliminar el tipo de producto porque tiene productos asociados.");

            _unitOfWork.TipoProductoRepository.Remove(tipoProducto);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
