using Backendapi.Models.Data.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backendapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private GenderService _GenderService;
        public GenderController(GenderService genderService)
        {
            _GenderService = genderService;
        }

        // GET: api/<GenderController>
        [HttpGet("GetUserGender")]
        public ActionResult GetGender()
        {
            var model = _GenderService.GetGenders();
            return Ok(model);
        }
    }
}
