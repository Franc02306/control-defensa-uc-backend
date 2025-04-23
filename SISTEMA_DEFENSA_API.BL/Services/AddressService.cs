using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Request;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;
using SISTEMA_DEFENSA_API.EL.Models;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public class AddressService
    {
        private readonly DefenseDbContext _context;

        public AddressService(DefenseDbContext context)
        {
            _context = context;
        }

        public List<AddressResponse> GetAll()
        {
            var addresses = _context.Addresses
                .Select(a => new AddressResponse
                {
                    Id = a.Id,
                    Province = a.Province.Name,
                    Municipality = a.Municipality.Name,
                    Street = a.Street,
                    Number = a.Number
                }).ToList();

            return addresses;
        }

        public AddressResponse? GetById(int id)
        {
            var address = _context.Addresses
                .Where(a => a.Id == id)
                .Select(a => new AddressResponse
                {
                    Id = a.Id,
                    Province = a.Province.Name,
                    Municipality = a.Municipality.Name,
                    Street = a.Street,
                    Number = a.Number
                }).FirstOrDefault();

            return address;
        }

        public int Create(AddressRequest request)
        {
            var newAddress = new Address
            {
                IdProvince = request.IdProvince,
                IdMunicipality = request.IdMunicipality,
                Street = request.Street,
                Number = request.Number
            };

            _context.Addresses.Add(newAddress);
            _context.SaveChanges();

            return newAddress.Id;
        }

        public void Update(int id, AddressRequest request)
        {
            var address = _context.Addresses.FirstOrDefault(a => a.Id == id);
            if (address == null)
                throw new Exception("La dirección no existe");

            address.IdProvince = request.IdProvince;
            address.IdMunicipality = request.IdMunicipality;
            address.Street = request.Street;
            address.Number = request.Number;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var address = _context.Addresses.FirstOrDefault(a => a.Id == id);
            if (address == null)
                throw new Exception("La dirección no existe");

            _context.Addresses.Remove(address);
            _context.SaveChanges();
        }
    }
}
