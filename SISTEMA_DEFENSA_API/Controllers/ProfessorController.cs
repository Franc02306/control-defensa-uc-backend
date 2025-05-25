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
    public class ProfessorController : ControllerBase
    {
        private readonly ProfessorService _professorService;

        public ProfessorController(ProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpPost]
        public IActionResult CreateProfessor([FromBody] ProfessorNewRequest request)
        {
            try
            {
                var professor = _professorService.CreateProfessor(request);

                return CreatedAtAction(nameof(CreateProfessor), new { id = professor.Id },
                    ApiResponse<ProfessorResponse>.SuccessResponse(professor, "Profesor creado exitosamente"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProfessor(int id, [FromBody] ProfessorUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("No se recibieron datos para actualizar"));
            }

            try
            {
                var updatedProfessor = _professorService.UpdateProfessor(id, request);

                var response = new ProfessorResponse
                {
                    Id = updatedProfessor.Id,
                    FirstName = updatedProfessor.FirstName,
                    LastName = updatedProfessor.LastName,
                    Gender = updatedProfessor.Gender,
                    BirthDate = updatedProfessor.BirthDate,
                    Area = updatedProfessor.Area,
                    WentAbroad = updatedProfessor.WentAbroad,
                    AcademicRank = updatedProfessor.AcademicRank,
                    ScientificCategory = updatedProfessor.ScientificCategory,
                    Address = new AddressResponse
                    {
                        Id = updatedProfessor.Address.Id,
                        Province = updatedProfessor.Address.Province?.Name ?? string.Empty,
                        Municipality = updatedProfessor.Address.Municipality?.Name ?? string.Empty,
                        Street = updatedProfessor.Address.Street,
                        Number = updatedProfessor.Address.Number
                    }
                };

                return Ok(ApiResponse<ProfessorResponse>.SuccessResponse(response, "Profesor actualizado exitosamente"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProfessorById(int id)
        {
            try
            {
                var professor = _professorService.GetProfessorById(id);
                return Ok(ApiResponse<ProfessorResponse>.SuccessResponse(professor, "Profesor encontrado exitosamente"));
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("search")]
        public IActionResult SearchProfessors([FromQuery] string? province, [FromQuery] string? municipality, [FromQuery] bool? wentAbroad, [FromQuery] string? academicRank)
        {
            var professors = _professorService.SearchProfessors(province, municipality, wentAbroad, academicRank);

            return Ok(ApiResponse<List<ProfessorResponse>>.SuccessResponse(professors, "Consulta realizada exitosamente"));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProfessor(int id)
        {
            try
            {
                _professorService.DeleteProfessor(id);
                return Ok(ApiResponse<string>.SuccessResponse(null, "Profesor eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("average-age")]
        public IActionResult GetAverageAge([FromQuery] string area, [FromQuery] string province, [FromQuery] bool wentAbroad)
        {
            try
            {
                var averageAge = _professorService.GetAverageAge(area, province, wentAbroad);
                return Ok(ApiResponse<decimal>.SuccessResponse(averageAge, "Promedio de edad calculado exitosamente"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("oldest-professor-address")]
        public IActionResult GetOldestProfessorAddress([FromQuery] string excludeMunicipality)
        {
            try
            {
                var professors = _professorService.GetOldestProfessorAddress(excludeMunicipality);
                return Ok(ApiResponse<List<ProfessorResponse>>.SuccessResponse(professors, "Consulta realizada exitosamente"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }
    }
}
