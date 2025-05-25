using Microsoft.EntityFrameworkCore;
using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Request;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;
using SISTEMA_DEFENSA_API.EL.Models;
using SISTEMA_DEFENSA_API.SQ;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public class ComplementService
    {
        private readonly DefenseDbContext _context;

        public ComplementService(DefenseDbContext context)
        {
            _context = context;
        }

        public List<ComplementResponse> GetMajors()
        {
            var majors = _context.Majors
                .Select(m => new ComplementResponse
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToList();

            return majors;
        }

        public List<ComplementResponse> GetAreas()
        {
            var areas = _context.Areas
                .Select(a => new ComplementResponse
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToList();

            return areas;
        }

        public List<ComplementResponse> GetScientificCategories()
        {
            var scientificCategories = _context.ScientificCategories
                .Select(sc => new ComplementResponse
                {
                    Id = sc.Id,
                    Name = sc.Name
                })
                .ToList();

            return scientificCategories;
        }

        public List<ComplementResponse> GetAcademicRanks()
        {
            var academicRanks = _context.AcademicRanks
                .Select(ar => new ComplementResponse
                {
                    Id = ar.Id,
                    Name = ar.Name
                })
                .ToList();
            return academicRanks;
        }
    }
}
