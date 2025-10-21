using Data;
using DominioModelo;
using DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class ProvinciaService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProvinciaService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProvinciaDTO>> GetAllAsync()
        {
            var provincias = await _unitOfWork.ProvinciaRepository.GetAllAsync();
            return provincias.Select(p => new ProvinciaDTO { IdProvincia = p.IdProvincia, Nombre = p.Nombre }).ToList();
        }

        public async Task<ProvinciaDTO?> GetByIdAsync(int id)
        {
            var provincia = await _unitOfWork.ProvinciaRepository.GetByIdAsync(id);
            return provincia != null ? new ProvinciaDTO { IdProvincia = provincia.IdProvincia, Nombre = provincia.Nombre } : null;
        }

        public async Task<ProvinciaDTO> CreateAsync(ProvinciaDTO provinciaDto)
        {
            var provincia = new Provincia { Nombre = provinciaDto.Nombre };
            await _unitOfWork.ProvinciaRepository.AddAsync(provincia);
            await _unitOfWork.SaveChangesAsync();
            provinciaDto.IdProvincia = provincia.IdProvincia;
            return provinciaDto;
        }

        public async Task<bool> UpdateAsync(int id, ProvinciaDTO provinciaDto)
        {
            var provincia = await _unitOfWork.ProvinciaRepository.GetByIdAsync(id);
            if (provincia == null) return false;

            provincia.Nombre = provinciaDto.Nombre;
            _unitOfWork.ProvinciaRepository.Update(provincia);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var provincia = await _unitOfWork.ProvinciaRepository.GetByIdAsync(id);
            if (provincia == null) return false;

            _unitOfWork.ProvinciaRepository.Remove(provincia);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
