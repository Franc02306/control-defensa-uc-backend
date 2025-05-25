using Microsoft.EntityFrameworkCore;
using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Request;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;
using SISTEMA_DEFENSA_API.EL.Models;
using SISTEMA_DEFENSA_API.SQ;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public class ProfessorService
    {
        private readonly DefenseDbContext _context;
        private readonly ProfessorSQ _professorSQ;

        public ProfessorService(DefenseDbContext context, ProfessorSQ professorSQ)
        {
            _context = context;
            _professorSQ = professorSQ;
        }

        public ProfessorResponse CreateProfessor(ProfessorNewRequest request)
        {
            // Validar fecha de nacimiento no mayor a la actual
            if (request.BirthDate.Date > DateTime.Now.Date)
                throw new Exception("La Fecha de Nacimiento no puede ser mayor a la fecha actual");

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

            var professor = new Professor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                Area = request.Area,
                WentAbroad = request.WentAbroad,
                AcademicRank = request.AcademicRank,
                ScientificCategory = request.ScientificCategory,
                IdAddress = address.Id
            };

            _context.Professors.Add(professor);
            _context.SaveChanges();

            return MapToResponse(professor, address);
        }

        public Professor UpdateProfessor(int id, ProfessorUpdateRequest request)
        {
            var existingProfessor = _context.Professors.FirstOrDefault(p => p.Id == id);

            // VALIDACIONES PARA ACTUALIZAR AL PROFESOR
            if (existingProfessor == null)
                throw new Exception("El profesor no existe");

            if (!string.IsNullOrWhiteSpace(request.FirstName))
                existingProfessor.FirstName = request.FirstName;

            if (!string.IsNullOrWhiteSpace(request.LastName))
                existingProfessor.LastName = request.LastName;

            if (!string.IsNullOrWhiteSpace(request.Area))
                existingProfessor.Area = request.Area;

            if (request.WentAbroad.HasValue)
                existingProfessor.WentAbroad = request.WentAbroad.Value;

            if (!string.IsNullOrWhiteSpace(request.AcademicRank))
                existingProfessor.AcademicRank = request.AcademicRank;

            if (!string.IsNullOrWhiteSpace(request.ScientificCategory))
                existingProfessor.ScientificCategory = request.ScientificCategory;

            if (request.BirthDate.HasValue)
            {
                if (request.BirthDate.Value.Date > DateTime.Now.Date)
                    throw new Exception("La Fecha de Nacimiento no puede ser mayor a la fecha actual");

                existingProfessor.BirthDate = request.BirthDate.Value;
            }

            // VALIDACIONES PARA ACTUALIZAR LA DIRECCION DEL PROFESOR
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

                existingProfessor.IdAddress = address.Id;
            }

            // Traer al profesor actualizado con su Dirección incluida para evitar errores
            var reloadedProfessor = _context.Professors
                .Where(p => p.Id == id)
                .Include(p => p.Address)
                    .ThenInclude(a => a.Province)
                .Include(p => p.Address)
                    .ThenInclude(a => a.Municipality)
                .FirstOrDefault();

            if (reloadedProfessor == null)
                throw new Exception("Error al recargar el profesor actualizado");

            return reloadedProfessor;
        }

        public void DeleteProfessor(int id)
        {
            var professor = _context.Professors.FirstOrDefault(p => p.Id == id);
            if (professor == null)
                throw new Exception("El profesor no existe");

            _context.Professors.Remove(professor);
            _context.SaveChanges();
        }

        public ProfessorResponse GetProfessorById(int id)
        {
            var professor = _context.Professors
                .Where(p => p.Id == id)
                .Include(p => p.Address)
                    .ThenInclude(a => a.Province)
                .Include(p => p.Address)
                    .ThenInclude(a => a.Municipality)
                .FirstOrDefault();

            if (professor == null)
                throw new Exception("El profesor no existe");

            return new ProfessorResponse
            {
                Id = professor.Id,
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                Gender = professor.Gender,
                BirthDate = professor.BirthDate,
                Area = professor.Area,
                WentAbroad = professor.WentAbroad,
                AcademicRank = professor.AcademicRank,
                ScientificCategory = professor.ScientificCategory,
                Address = new AddressResponse
                {
                    Id = professor.Address.Id,
                    Province = professor.Address.Province?.Name ?? string.Empty,
                    Municipality = professor.Address.Municipality?.Name ?? string.Empty,
                    Street = professor.Address.Street,
                    Number = professor.Address.Number
                }
            };
        }

        public List<ProfessorResponse> SearchProfessors(string? province, string? municipality, bool? wentAbroad, string? academicRank)
        {
            return _professorSQ.SearchProfessors(province, municipality, wentAbroad, academicRank);
        }

        public decimal GetAverageAge(string area,string province, bool wentAbroad)
        {
            return _professorSQ.GetAverageAgeProfessors(area, province, wentAbroad);
        }

        public List<ProfessorResponse> GetOldestProfessorAddress(string excludeMunicipality)
        {
            return _professorSQ.GetOldestProfessorAddress(excludeMunicipality);
        }

        private ProfessorResponse MapToResponse(Professor professor, Address address)
        {
            var provinceName = _context.Provinces.First(p => p.Id == address.IdProvince).Name;
            var municipalityName = _context.Municipalities.First(m => m.Id == address.IdMunicipality).Name;

            return new ProfessorResponse
            {
                Id = professor.Id,
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                Gender = professor.Gender,
                BirthDate = professor.BirthDate,
                Area = professor.Area,
                WentAbroad = professor.WentAbroad,
                AcademicRank = professor.AcademicRank,
                ScientificCategory = professor.ScientificCategory,
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
