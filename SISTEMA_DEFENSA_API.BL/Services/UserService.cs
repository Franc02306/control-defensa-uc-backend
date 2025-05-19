using Microsoft.EntityFrameworkCore.Storage.Json;
using SISTEMA_DEFENSA_API.BL.Utils;
using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Request;
using SISTEMA_DEFENSA_API.EL.Models;
using System.Net.Mail;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
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

                    // Se envia el correo a los administradores
                    _emailService.SendEmailToAdminsUsingTemplate(_context, newUser.FirstName, newUser.LastName, newUser.Email);

                    // Confirmamos transacción en caso el correo se ha enviado correctamente
                    transaction.Commit();

                    return newUser;
                }
                catch (SmtpException smtpEx)
                {
                    // Revertimos transacción por error SMTP (Correo)
                    transaction.Rollback();
                    throw new Exception($"Error al crear el usuario: No se pudo enviar el correo de notificación. Verifique la configuración del servidor de correo. Detalles: {smtpEx.Message}");
                }
                catch (Exception ex)
                {
                    // Revertimos transacción por si cae una excepción
                    transaction.Rollback();
                    throw new Exception($"Error al crear el usuario: {ex.Message}");
                }
            }
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

        public string ApproveUser(string email)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(u => u.Email == email);
                    if (user == null)
                        throw new Exception("Usuario no encontrado");

                    if (user.Status)
                        throw new Exception("El usuario ya está aprobado");

                    _emailService.SendActionEmail(
                        user.Email,
                        "aprobado",
                        user.FirstName,
                        user.LastName
                    );

                    user.Status = true;
                    _context.SaveChanges();

                    transaction.Commit();
                    return "Usuario aprobado exitosamente";
                }
                catch (SmtpException smtpEx)
                {
                    // Revertimos transacción por error SMTP (Correo)
                    transaction.Rollback();
                    throw new Exception($"Error al aprobar el usuario: No se pudo enviar el correo de notificación. Detalles: {smtpEx.Message}");
                }
                catch (Exception ex)
                {
                    // Revertimos transacción por si cae una excepción
                    transaction.Rollback();
                    throw new Exception($"Error al aprobar el usuario: {ex.Message}");
                }
            }
        }

        public string RejectUser(string email)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(u => u.Email == email);
                    if (user == null)
                        throw new Exception("Usuario no encontrado");

                    if (user.Status)
                        throw new Exception("El usuario ya está aprobado. No se puede rechazar");

                    _emailService.SendActionEmail(
                        user.Email,
                        "rechazado",
                        user.FirstName,
                        user.LastName
                    );

                    _context.Users.Remove(user);
                    _context.SaveChanges();

                    transaction.Commit();
                    return "Usuario rechazado correctamente.";
                }
                catch (SmtpException smtpEx)
                {
                    // Revertimos transacción por error SMTP (Correo)
                    transaction.Rollback();
                    throw new Exception($"Error al rechazar el usuario: No se pudo enviar el correo de notificación. Detalles: {smtpEx.Message}");
                }
                catch (Exception ex)
                {
                    // Revertimos transacción por si cae una excepción
                    transaction.Rollback();
                    throw new Exception($"Error al rechazar el usuario: {ex.Message}");
                }
            }
        }
    }
}
