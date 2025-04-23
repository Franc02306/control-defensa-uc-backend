using Microsoft.EntityFrameworkCore;
using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public class LocationService
    {
        private readonly DefenseDbContext _context;

        public LocationService(DefenseDbContext context)
        {
            _context = context;
        }

        public List<ProvinceResponse> GetAllProvinces()
        {
            return _context.Provinces
                .Select(p => new ProvinceResponse
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();
        }

        public List<MunicipalityResponse> GetMunicipalitiesByProvince(int provinceId)
        {
            return _context.Municipalities
                .Where(m => m.IdProvince == provinceId)
                .Include(m => m.Province)
                .Select(m => new MunicipalityResponse
                {
                    Id = m.Id,
                    Name = m.Name,
                    IdProvince = m.IdProvince
                }).ToList();
        }
    }
}
