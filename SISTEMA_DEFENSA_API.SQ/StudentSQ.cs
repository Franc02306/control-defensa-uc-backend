using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SISTEMA_DEFENSA_API.SQ
{
    public class StudentSQ
    {
        private readonly DefenseDbContext _context;

        public StudentSQ(DefenseDbContext context)
        {
            _context = context;
        }

        public List<StudentResponse> SearchStudents(string? name)
        {
            var param = new SqlParameter("@Name", name ?? (object)DBNull.Value);

            var students = _context.StudentSearchResults
                .FromSqlRaw("EXEC UC_SP_SEARCH_STUDENTS @Name", param)
                .ToList();

            var result = students.Select(s => new StudentResponse
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Gender = s.Gender,
                BirthDate = s.BirthDate,
                Major = s.Major,
                Year = s.Year,
                TeacherAverage = s.TeacherAverage,
                Address = new AddressResponse
                {
                    Id = s.AddressId,
                    Province = s.Province,
                    Municipality = s.Municipality,
                    Street = s.Street,
                    Number = s.Number
                }
            }).ToList();

            return result;
        }

        public decimal GetAverageAgeStudents(int year, string province)
        {
            var paramName = new SqlParameter("@Name", DBNull.Value);
            var paramYear = new SqlParameter("@Year", year);
            var paramProvince = new SqlParameter("@Province", province ?? (object)DBNull.Value);

            var students = _context.StudentSearchResults
                .FromSqlRaw("EXEC UC_SP_SEARCH_STUDENTS @Name, @Year, @Province", paramName, paramYear, paramProvince)
                .ToList();

            if (students.Count == 0)
                throw new Exception("No se encontraron estudiantes para los filtros proporcionados.");

            var today = DateTime.Today;

            var averageAge = students
                .Select(s => today.Year - s.BirthDate.Year - (today.DayOfYear < s.BirthDate.DayOfYear ? 1 : 0))
                .Average();

            return Math.Round((decimal)averageAge, 2);
        }
    }
}
