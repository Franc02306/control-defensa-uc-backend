using SISTEMA_DEFENSA_API.BL.Utils;
using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Request;
using SISTEMA_DEFENSA_API.EL.Models;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public class UserService
    {
        private readonly DefenseDbContext _context;
        private readonly EmailService _emailService;

        public UserService(DefenseDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public User CreateUser(UserNewRequest request)
        {
            if (_context.Users.Any(u => u.Username == request.Username))
                throw new Exception("El nombre de usuario ya existe");

            if (_context.Users.Any(u => u.Email == request.Email))
                throw new Exception("El correo electrónico ya existe");

            if (!PasswordValidator.IsStrong(request.Password))
                throw new Exception("La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un símbolo");

            // Verificar que el rol sea válido
            var role = _context.Roles.FirstOrDefault(r => r.Id == request.IdRole);

            if (role == null)
                throw new Exception("El rol especificado no existe");

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Password = PasswordHasher.Hash(request.Password),
                Status = request.Status,
                CreatedAt = DateTime.Now,
                IdRole = request.IdRole
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            // Enviar correo a los administradores
            try
            {
                _emailService.SendEmailToAdminsUsingTemplate(
                    _context,
                    newUser.FirstName,
                    newUser.LastName,
                    newUser.Email
                );
            }
            catch (Exception ex)
            {
                // Manejo de errores de correo, pero sin afectar la creación del usuario
                Console.WriteLine($"Error al enviar correo a administradores: {ex.Message}");
            }

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
            {
                if (!PasswordValidator.IsStrong(request.Password))
                    throw new Exception("La nueva contraseña no es segura. Debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un símbolo");

                existingUser.Password = PasswordHasher.Hash(request.Password);
            }

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
