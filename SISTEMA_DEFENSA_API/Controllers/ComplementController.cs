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
    public class ComplementController : ControllerBase
    {
        private readonly ComplementService _complementService;

        public ComplementController(ComplementService complementService)
        {
            _complementService = complementService;
        }

        [HttpGet("majors")]
        public IActionResult GetMajors()
        {
            try
            {
                var majors = _complementService.GetMajors();

                return Ok(ApiResponse<List<ComplementResponse>>.SuccessResponse(majors, "Lista de carreras obtenida"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("areas")]
        public IActionResult GetAreas()
        {
            try
            {
                var areas = _complementService.GetAreas();

                return Ok(ApiResponse<List<ComplementResponse>>.SuccessResponse(areas, "Lista de áreas obtenida"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("scientific-categories")]
        public IActionResult GetScientificCategories()
        {
            try
            {
                var scientificCategories = _complementService.GetScientificCategories();

                return Ok(ApiResponse<List<ComplementResponse>>.SuccessResponse(scientificCategories, "Lista de categorías científicas obtenida"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("academic-ranks")]
        public IActionResult GetAcademicRanks()
        {
            try
            {
                var categories = _complementService.GetAcademicRanks();

                return Ok(ApiResponse<List<ComplementResponse>>.SuccessResponse(categories, "Lista de categorías obtenida"));
            }
            catch (Exception ex)
            {
                return Conflict(ApiResponse<string>.ErrorResponse(ex.Message));
            }
        }
    }
}
