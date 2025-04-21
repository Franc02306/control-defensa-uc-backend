using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Request;
using SISTEMA_DEFENSA_API.EL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public class UserService
    {
        private readonly DefenseDbContext _context;

        public UserService(DefenseDbContext context)
        {
            _context = context;
        }

        public User CreateUser(UserRequest request)
        {
            if (_context.Users.Any(u => u.Username == request.Username))
                throw new Exception("El nombre de usuario ya existe");

            if (_context.Users.Any(u => u.Email == request.Email))
                throw new Exception("El correo electrónico ya existe");

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Status = request.Status,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }
    }
}
