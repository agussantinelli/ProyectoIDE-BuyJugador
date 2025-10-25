using Data;
using DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class LocalidadService
    {
        private readonly UnitOfWork _unitOfWork;

        public LocalidadService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LocalidadDTO>> GetAllAsync()
        {
            var entidades = await _unitOfWork.LocalidadRepository.GetAllAsync();
            return entidades.Select(LocalidadDTO.FromDominio).ToList();
        }

        public async Task<LocalidadDTO?> GetByIdAsync(int id)
        {
            var entidad = await _unitOfWork.LocalidadRepository.GetByIdAsync(id);
            return entidad == null ? null : LocalidadDTO.FromDominio(entidad);
        }

        public async Task<LocalidadDTO> CreateAsync(LocalidadDTO dto)
        {
            var entidad = dto.ToDominio();
            await _unitOfWork.LocalidadRepository.AddAsync(entidad);
            await _unitOfWork.SaveChangesAsync();
            return LocalidadDTO.FromDominio(entidad);
        }

        public async Task<List<LocalidadDTO>> GetAllOrderedAsync()
        {
            var entidades = await _unitOfWork.LocalidadRepository.GetAllOrderedByNameAsync();
            return entidades.Select(LocalidadDTO.FromDominio).ToList();
        }
    }
}
