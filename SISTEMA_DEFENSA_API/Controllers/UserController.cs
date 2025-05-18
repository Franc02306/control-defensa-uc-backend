using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SISTEMA_DEFENSA_API.BL.Services;
using SISTEMA_DEFENSA_API.EL.DTOs.Request;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;
using SISTEMA_DEFENSA_API.EL.Models;

namespace SISTEMA_DEFENSA_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserNewRequest request)
        {
            try
            {
                var user = _userService.CreateUser(request);

                var response = new UserResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    Status = user.Status,
                    CreatedAt = user.CreatedAt,
                    IdRole = user.IdRole
                };

                return CreatedAtAction(nameof(CreateUser), new { id = response.Id }, 
                    ApiResponse<UserResponse>.SuccessResponse(response, "Usuario creado exitosamente"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserUpdateRequest? request)
        {
            if (request == null)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("No se recibieron datos para actualizar"));
            }

            try
            {
                var updatedUser = _userService.UpdateUser(id, request);

                var response = new UserResponse
                {
                    Id = updatedUser.Id,
                    FirstName = updatedUser.FirstName,
                    LastName = updatedUser.LastName,
                    Username = updatedUser.Username,
                    Email = updatedUser.Email,
                    Status = updatedUser.Status,
                    CreatedAt = updatedUser.CreatedAt
                };

                return Ok(ApiResponse<UserResponse>.SuccessResponse(response, "Usuario actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok(ApiResponse<string>.SuccessResponse(null, "Usuario eliminado exitosamente"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }
    }
}
