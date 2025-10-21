using Data;
using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class ReporteService
    {
        private readonly UnitOfWork _unitOfWork;

        public ReporteService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ReporteVentasDTO>> GetVentasPorPersonaUltimos7DiasAsync(int idPersona)
        {
            return await _unitOfWork.ReporteRepository.GetVentasPorPersonaUltimos7DiasAsync(idPersona);
        }
    }
}
