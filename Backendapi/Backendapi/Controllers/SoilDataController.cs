using Backendapi.Models.Data;
using Backendapi.Models.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Backendapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoilDataController : ControllerBase
    {
        private SoilDataService _SoilDataService;
        public SoilDataController(SoilDataService soilDataService)
        {
            _SoilDataService = soilDataService;
        }


        // GET: api/<SoilDataController>
        [HttpGet("GetSoilDatasFromAllDevices")]
        public ActionResult GetSoilDatasFromAllDevices()
        {
            var model = _SoilDataService.GetSoilDatasFromAllDevices();
            return Ok(model);
        }

        // GET api/<SoilDataController>/5
        [HttpGet("GetSoilDatasbyDeviceId/{farmId}")]
        public ActionResult GetSoilDatasbyDeviceId(string farmId) 
        {
            var model = _SoilDataService.GetSoilDatasbyDeviceId(farmId);
            return Ok(model);
        }
        // GET api/<SoilDataController>/5
        [HttpGet("GetSoilDataDetails/{soilId}")]
        public ActionResult GetSoilDataDetails(int soilId) 
        {
            var model = _SoilDataService.GetSoilDataDetails(soilId);
            return Ok(model);
        }

        // POST api/<SoilDataController>
        [HttpPost("AddSoilData")]
        public ActionResult AddSoilData([FromBody] SoilDataFromDeviceModel model)
        {
            bool result = _SoilDataService.AddSoilData(model);
            if (result)
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Sorry something went wrong");
        }

        // DELETE api/<SoilDataController>/5
        [HttpDelete("DeleteSoilData/{id}")]
        public ActionResult DeleteSoilData(int id)
        {
            bool result = _SoilDataService.DeleteSoilData(id);
            if (result)
                return Ok(result);

            return BadRequest("Sorry something went wrong");
        }
    }
}
