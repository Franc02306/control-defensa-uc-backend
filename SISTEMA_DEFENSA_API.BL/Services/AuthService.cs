using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SISTEMA_DEFENSA_API.BL.Utils;
using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SISTEMA_DEFENSA_API.BL.Services
{
    public class AuthService
    {
        private readonly DefenseDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DefenseDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public User? ValidateCredentials(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == username &&
                u.Status);

            if (user == null) return null;

            // Compara password plano contra hash
            bool valid = PasswordHasher.Verify(password, user.Password);
            return valid ? user : null;
        }

        public string GenerateJwtToken(User user)
        {
            var jwtConfig = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var role = _context.Roles.FirstOrDefault(r => r.Id == user.IdRole)?.Name ?? "Regular";

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtConfig["Issuer"],
                audience: jwtConfig["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtConfig["ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
