using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SISTEMA_DEFENSA_API.EL.Models;

namespace SISTEMA_DEFENSA_API.SQ
{
    public class ProfessorSQ
    {
        private readonly DefenseDbContext _context;

        public ProfessorSQ(DefenseDbContext context)
        {
            _context = context;
        }

        public List<ProfessorResponse> SearchProfessors(string? province, string? municipality, bool? wentAbroad, string? academicRank, string? area)
        {
            var paramProvince = new SqlParameter("@Province", province ?? (object)DBNull.Value);
            var paramMunicipality = new SqlParameter("@Municipality", municipality ?? (object)DBNull.Value);
            var paramWentAbroad = new SqlParameter("@WentAbroad", wentAbroad.HasValue ? (object)wentAbroad.Value : DBNull.Value);
            var paramAcademicRank = new SqlParameter("@AcademicRank", academicRank ?? (object)DBNull.Value);
            var paramArea = new SqlParameter("@Area", area ?? (object)DBNull.Value);

            var professors = _context.ProfessorSearchResults
                .FromSqlRaw("EXEC UC_SP_SEARCH_PROFESSORS @Province, @Municipality, @WentAbroad, @AcademicRank, @Area", paramProvince, paramMunicipality, paramWentAbroad, paramAcademicRank, paramArea)
                .ToList();

            var result = professors.Select(p => new ProfessorResponse
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender,
                BirthDate = p.BirthDate,
                Age = p.Age,
                Area = p.Area,
                WentAbroad = p.WentAbroad,
                AcademicRank = p.AcademicRank,
                ScientificCategory = p.ScientificCategory,
                Address = new AddressResponse
                {
                    Id = p.AddressId,
                    Province = p.Province,
                    Municipality = p.Municipality,
                    Street = p.Street,
                    Number = p.Number
                }
            }).ToList();

            return result;
        }

        public decimal GetAverageAgeProfessors(string area, string province, bool wentAbroad)
        {
            var paramProvince = new SqlParameter("@Province", province ?? (object)DBNull.Value);
            var paramMunicipality = new SqlParameter("@Municipality", DBNull.Value);
            var paramWentAbroad = new SqlParameter("@WentAbroad", wentAbroad);
            var paramAcademicRank = new SqlParameter("@AcademicRank", DBNull.Value);
            var paramArea = new SqlParameter("@Area", area ?? (object)DBNull.Value);

            var professors = _context.ProfessorSearchResults
                .FromSqlRaw("EXEC UC_SP_SEARCH_PROFESSORS @Province, @WentAbroad", paramProvince, paramMunicipality, paramWentAbroad, paramAcademicRank, paramArea)
                .ToList();

            if (professors.Count == 0)
                throw new Exception("No se encontraron profesores para los filtros proporcionados.");

            var today = DateTime.Today;

            var averageAge = professors
                .Select(p => today.Year - p.BirthDate.Year - (today.DayOfYear < p.BirthDate.DayOfYear ? 1 : 0))
                .Average();

            return Math.Round((decimal)averageAge, 2);
        }

        public List<ProfessorResponse> GetOldestProfessorAddress(string excludeMunicipality)
        {
            var paramExcludeMunicipality = new SqlParameter("@ExcludeMunicipality", excludeMunicipality ?? (object)DBNull.Value);

            var professors = _context.ProfessorSearchResults
                .FromSqlRaw("EXEC UC_SP_GET_OLDEST_PROFESSOR_ADDRESS @ExcludeMunicipality", paramExcludeMunicipality)
                .ToList();

            if (professors.Count == 0)
                throw new Exception("No se encontraron profesores para los filtros proporcionados.");

            var result = professors.Select(p => new ProfessorResponse
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender,
                BirthDate = p.BirthDate,
                Area = p.Area,
                WentAbroad = p.WentAbroad,
                AcademicRank = p.AcademicRank,
                ScientificCategory = p.ScientificCategory,
                Address = new AddressResponse
                {
                    Id = p.AddressId,
                    Province = p.Province,
                    Municipality = p.Municipality,
                    Street = p.Street,
                    Number = p.Number
                }
            }).ToList();

            return result;
        }
    }
}
