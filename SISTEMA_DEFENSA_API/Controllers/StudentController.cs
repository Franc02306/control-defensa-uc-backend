using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SISTEMA_DEFENSA_API.BL.Services;
using SISTEMA_DEFENSA_API.EL.DTOs.Request;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;

namespace SISTEMA_DEFENSA_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] StudentNewRequest request)
        {
            try
            {
                var student = _studentService.CreateStudent(request);

                return CreatedAtAction(nameof(CreateStudent), new { id = student.Id },
                    ApiResponse<StudentResponse>.SuccessResponse(student, "Estudiante creado exitosamente"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] StudentUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("No se recibieron datos para actualizar"));
            }

            try
            {
                var updatedStudent = _studentService.UpdateStudent(id, request);

                var response = new StudentResponse
                {
                    Id = updatedStudent.Id,
                    FirstName = updatedStudent.FirstName,
                    LastName = updatedStudent.LastName,
                    Gender = updatedStudent.Gender,
                    BirthDate = updatedStudent.BirthDate,
                    Major = updatedStudent.Major,
                    Year = updatedStudent.Year,
                    TeacherAverage = updatedStudent.TeacherAverage,
                    Address = new AddressResponse
                    {
                        Id = updatedStudent.Address.Id,
                        Province = updatedStudent.Address.Province?.Name ?? string.Empty,
                        Municipality = updatedStudent.Address.Municipality?.Name ?? string.Empty,
                        Street = updatedStudent.Address.Street,
                        Number = updatedStudent.Address.Number
                    }
                };

                return Ok(ApiResponse<StudentResponse>.SuccessResponse(response, "Estudiante actualizado exitosamente"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _studentService.DeleteStudent(id);
                return Ok(ApiResponse<string>.SuccessResponse(null, "Estudiante eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }
    }
}
