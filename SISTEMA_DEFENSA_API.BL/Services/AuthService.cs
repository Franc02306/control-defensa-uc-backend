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
        private readonly DefensaDbContext _context;

        public AuthService(DefensaDbContext context)
        {
            _context = context;
        }

        public User? ValidarCredenciales(string username, string password)
        {
            return _context.Usuarios
                .FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
