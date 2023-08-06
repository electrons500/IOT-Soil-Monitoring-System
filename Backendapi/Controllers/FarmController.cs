using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backendapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private FarmService _FarmService;

        public FarmController(FarmService farmService)
        {
            _FarmService = farmService;
        }

        // GET: api/<FarmController>
        [HttpGet("GetFarms")]
        public ActionResult GetFarms()
        {
            var model = _FarmService.GetFarms();
            return Ok(model);
        }
        // GET: api/<FarmController>
        [HttpGet("GetFarmsbyFarmerId/{farmerId}")]
        public ActionResult GetFarmsbyFarmerId(string farmerId)
        {
            var model = _FarmService.GetFarmsbyFarmerId(farmerId);
            return Ok(model);
        }

        // GET api/<FarmController>/5
        [HttpGet("GetFarmDetails/{farmId}")]
        public ActionResult GetFarmDetails(string farmId)
        {
            var model = _FarmService.GetFarmDetails(farmId);
            return Ok(model);
        }
        
        // GET api/<FarmController>/5
        [HttpGet("GetFarmDetailsByFarmerId/{farmerId}")]
        public ActionResult GetFarmDetailsByFarmerId(string farmerId)
        {
            var model = _FarmService.GetFarmDetailsByFarmerId(farmerId);
            return Ok(model);
        }
        // GET api/<FarmController>/5
        [HttpGet("GetFarmsRegisteredByOfficer/{OfficerId}")]
        public ActionResult GetFarmsRegisteredByOfficer(string OfficerId)
        {
            var model = _FarmService.GetFarmsRegisteredByOfficer(OfficerId);
            return Ok(model);
        }

        // POST api/<FarmController>
        [HttpPost("AddFarm")]
        public ActionResult AddFarm([FromBody] FarmApiModel model)
        {
            bool result = _FarmService.AddFarm(model);
            if (result)
                return Ok(result);

            return BadRequest("Sorry something went wrong");

        }

        // PUT api/<FarmController>/5
        [HttpPut("UpdateFarmDetails/{farmId}")]
        public ActionResult UpdateFarmDetails(string farmId, [FromBody] FarmApiModel model)
        {
            bool result = _FarmService.UpdateFarmDetails(model, farmId);
            if (result)
                return Ok(result);

            return BadRequest("Sorry something went wrong");
        }

        // DELETE api/<FarmController>/5
        [HttpDelete("DeleteFarmDetails/{farmId}")]
        public ActionResult DeleteFarmDetails(string farmId)
        {
            bool result = _FarmService.DeleteFarmDetails(farmId);
            if (result)
                return Ok(result);

            return BadRequest("Sorry something went wrong"); 
        }
    }
}
