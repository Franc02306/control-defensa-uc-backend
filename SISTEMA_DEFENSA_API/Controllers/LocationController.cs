using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SISTEMA_DEFENSA_API.BL.Services;
using SISTEMA_DEFENSA_API.EL.DTOs.Response;

namespace SISTEMA_DEFENSA_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _locationService;

        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("provinces")]
        public ActionResult<List<ProvinceResponse>> GetProvinces()
        {
            return Ok(_locationService.GetAllProvinces());
        }

        [HttpGet("municipality/by-province{provinceId}")]
        public ActionResult<List<MunicipalityResponse>> GetMunicipalitiesByProvince(int provinceId)
        {
            return Ok(_locationService.GetMunicipalitiesByProvince(provinceId));
        }
    }
}
