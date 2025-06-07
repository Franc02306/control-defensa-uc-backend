using Microsoft.EntityFrameworkCore;
using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Request;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;
using SISTEMA_DEFENSA_API.EL.Models;
using SISTEMA_DEFENSA_API.SQ;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public class StudentService
    {
        private readonly DefenseDbContext _context;
        private readonly StudentSQ _studentSQ;

        public StudentService(DefenseDbContext context, StudentSQ studentSQ)
        {
            _context = context;
            _studentSQ = studentSQ;
        }

        public StudentResponse CreateStudent(StudentNewRequest request)
        {
            // Validar fecha de nacimiento no mayor a la actual
            if (request.BirthDate.Date > DateTime.Now.Date)
                throw new Exception("La Fecha de Nacimiento no puede ser mayor a la fecha actual");

            // Validar que el estudiante tenga al menos 18 años
            var today = DateTime.Today;
            var age = today.Year - request.BirthDate.Year;
            if (request.BirthDate.Date > today.AddYears(-age)) age--;
            if (age < 18)
                throw new Exception("El estudiante debe ser mayor a 18 años");

            // Validar valores entre 0 a 20 en promedio de docente para el estudiante
            if (request.TeacherAverage < 0 || request.TeacherAverage > 20)
                throw new Exception("El promedio del profesor debe estar entre 0 y 20");

            var existingAddress = _context.Addresses.FirstOrDefault(a =>
                a.IdProvince == request.Address.IdProvince &&
                a.IdMunicipality == request.Address.IdMunicipality &&
                a.Street == request.Address.Street &&
                a.Number == request.Address.Number
            );

            Address address;

            if (existingAddress != null)
            {
                address = existingAddress;
            }
            else
            {
                address = new Address
                {
                    IdProvince = request.Address.IdProvince,
                    IdMunicipality = request.Address.IdMunicipality,
                    Street = request.Address.Street,
                    Number = request.Address.Number
                };

                _context.Addresses.Add(address);
                _context.SaveChanges();
            }

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

        public Student UpdateStudent(int id, StudentUpdateRequest request)
        {
            var existingStudent = _context.Students.FirstOrDefault(s => s.Id == id);

            // VALIDACIONES PARA ACTUALIZAR AL ESTUDIANTE
            if (existingStudent == null) 
                throw new Exception("El estudiante no existe");

            if (!string.IsNullOrWhiteSpace(request.FirstName))
                existingStudent.FirstName = request.FirstName;

            if (!string.IsNullOrWhiteSpace(request.LastName))
                existingStudent.LastName = request.LastName;

            if (!string.IsNullOrWhiteSpace(request.Major))
                existingStudent.Major = request.Major;

            if (request.Year.HasValue)
                existingStudent.Year = request.Year.Value;

            if (request.BirthDate.HasValue)
            {
                var newBirthDate = request.BirthDate.Value;

                if (newBirthDate.Date > DateTime.Now.Date)
                    throw new Exception("La Fecha de Nacimiento no puede ser mayor a la fecha actual");

                // Validar que tenga al menos 18 años
                var today = DateTime.Today;
                var age = today.Year - newBirthDate.Year;
                if (newBirthDate.Date > today.AddYears(-age)) age--;
                if (age < 18)
                    throw new Exception("El estudiante debe ser mayor de 18 años");

                existingStudent.BirthDate = newBirthDate;
            }

            if (request.TeacherAverage.HasValue)
            {
                if (request.TeacherAverage.Value < 0 || request.TeacherAverage.Value > 20)
                    throw new Exception("El promedio del profesor debe estar entre 0 y 20");

                existingStudent.TeacherAverage = request.TeacherAverage.Value;
            }

            // VALIDACIONES PARA ACTUALIZAR LA DIRECCION DEL ESTUDIANTE
            if (request.Address != null)
            {
                var existingAddress = _context.Addresses.FirstOrDefault(a =>
                    a.IdProvince == request.Address.IdProvince &&
                    a.IdMunicipality == request.Address.IdMunicipality &&
                    a.Street == request.Address.Street &&
                    a.Number == request.Address.Number
                );

                Address address;

                if (existingAddress != null)
                {
                    address = existingAddress;
                }
                else
                {
                    address = new Address
                    {
                        IdProvince = request.Address.IdProvince ?? 0,
                        IdMunicipality = request.Address.IdMunicipality ?? 0,
                        Street = request.Address.Street,
                        Number = request.Address.Number
                    };

                    _context.Addresses.Add(address);
                    _context.SaveChanges();
                }

                existingStudent.IdAddress = address.Id;
            }

            _context.SaveChanges();

            // Traer al estudiante actualizado con su Dirección incluida para evitar errores
            var reloadedStudent = _context.Students
                .Where(s => s.Id == id)
                .Include(s => s.Address)
                    .ThenInclude(a => a.Province)
                .Include(s => s.Address)
                    .ThenInclude(a => a.Municipality)
                .FirstOrDefault();

            if (reloadedStudent == null)
                throw new Exception("Error al recargar el estudiante actualizado");

            return reloadedStudent;
        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null) 
                throw new Exception("El estudiante no existe");

            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public StudentResponse GetStudentById(int id)
        {
            var student = _context.Students
                .Where(s => s.Id == id)
                .Include(s => s.Address)
                    .ThenInclude(a => a.Province)
                .Include(s => s.Address)
                    .ThenInclude(a => a.Municipality)
                .FirstOrDefault();

            if (student == null)
                throw new Exception("El estudiante no existe");

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
                    Id = student.Address.Id,
                    Province = student.Address.Province?.Name ?? string.Empty,
                    Municipality = student.Address.Municipality?.Name ?? string.Empty,
                    Street = student.Address.Street,
                    Number = student.Address.Number
                }
            };
        }

        public List<StudentResponse> SearchStudents(string? name, int? year, string? province)
        {
            return _studentSQ.SearchStudents(name, year, province);
        }

        public decimal GetAverageAge(int year, string province)
        {
            return _studentSQ.GetAverageAgeStudents(year, province);
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