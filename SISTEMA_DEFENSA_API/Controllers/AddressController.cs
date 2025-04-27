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
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public ActionResult<List<AddressResponse>> GetAll()
        {
            return Ok(_addressService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<AddressResponse> GetById(int id)
        {
            var address = _addressService.GetById(id);
            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public ActionResult<int> Create([FromBody] AddressNewRequest request)
        {
            var id = _addressService.Create(request);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AddressNewRequest request)
        {
            _addressService.Update(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _addressService.Delete(id);
            return NoContent();
        }
    }
}
