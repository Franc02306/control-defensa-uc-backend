using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;
using SISTEMA_DEFENSA_API.BL.Services;
using SISTEMA_DEFENSA_API.EL.DbContexts;
using SISTEMA_DEFENSA_API.EL.DTOs.Request;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;
using System.Linq;
using System.Net;

namespace SISTEMA_DEFENSA_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _authService.ValidateCredentials(request.Username, request.Password);

            if (user == null)
            {
                return Unauthorized(ApiResponse<string>.ErrorResponse("Credenciales inválidas"));
            }

            var userResponse = new
            {
                user.Id,
                user.Username,
                user.Email
            };

            return Ok(ApiResponse<object>.SuccessResponse(userResponse, "Inicio de sesión exitoso"));
        }
    }
}
