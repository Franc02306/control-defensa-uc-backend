using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public class AuthService
    {
        private readonly DefenseDbContext _context;

        public AuthService(DefenseDbContext context)
        {
            _context = context;
        }

        public User? ValidateCredentials(string username, string password)
        {
            return _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
