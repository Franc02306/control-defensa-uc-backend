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

        public User CreateUser(UserNewRequest request)
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

        public User UpdateUser(int id, UserUpdateRequest request)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
                throw new Exception("El usuario no existe");

            if (!string.IsNullOrWhiteSpace(request.Username) && request.Username != existingUser.Username)
            {
                if (_context.Users.Any(u => u.Username == request.Username && u.Id != id))
                    throw new Exception("El nombre de usuario ya está en uso");

                existingUser.Username = request.Username;
            }

            if (!string.IsNullOrWhiteSpace(request.Email) && request.Email != existingUser.Email)
            {
                if (_context.Users.Any(u => u.Email == request.Email && u.Id != id))
                    throw new Exception("El correo ya está en uso");

                existingUser.Email = request.Email;
            }

            if (!string.IsNullOrWhiteSpace(request.FirstName))
                existingUser.FirstName = request.FirstName;

            if (!string.IsNullOrWhiteSpace(request.LastName))
                existingUser.LastName = request.LastName;

            if (!string.IsNullOrWhiteSpace(request.Password))
                existingUser.Password = request.Password;

            _context.SaveChanges();

            return existingUser;
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                throw new Exception("El usuario no existe");

            if (!user.Status)
                throw new Exception("El usuario ya está inactivo");

            user.Status = false;
            _context.SaveChanges();
        }
    }
}
