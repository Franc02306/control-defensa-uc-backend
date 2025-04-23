using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Request;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;
using SISTEMA_DEFENSA_API.EL.Models;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public class StudentService
    {
        private readonly DefenseDbContext _context;

        public StudentService(DefenseDbContext context)
        {
            _context = context;
        }

        public StudentResponse CreateStudent(StudentRequest request)
        {
            var address = new Address
            {
                IdProvince = request.Address.IdProvince,
                IdMunicipality = request.Address.IdMunicipality,
                Street = request.Address.Street,
                Number = request.Address.Number
            };

            _context.Addresses.Add(address);
            _context.SaveChanges();

            var student = new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                Major = request.Major,
                Year = request.Year,
                TeacherAverage = request.TeacherAverage,
                IdAddress = address.Id
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            return MapToResponse(student, address);
        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null) throw new Exception("El estudiante no existe");

            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        private StudentResponse MapToResponse(Student student, Address address)
        {
            var provinceName = _context.Provinces.First(p => p.Id == address.IdProvince).Name;
            var municipalityName = _context.Municipalities.First(m => m.Id == address.IdMunicipality).Name;

            return new StudentResponse
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Gender = student.Gender,
                BirthDate = student.BirthDate,
                Major = student.Major,
                Year = student.Year,
                TeacherAverage = student.TeacherAverage,
                Address = new AddressResponse
                {
                    Id = address.Id,
                    Province = provinceName,
                    Municipality = municipalityName,
                    Street = address.Street,
                    Number = address.Number
                }
            };
        }
    }
}