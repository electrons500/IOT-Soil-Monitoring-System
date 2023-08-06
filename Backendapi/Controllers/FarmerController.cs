using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backendapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmerController : ControllerBase
    {
        private FarmerService _FarmerService;
        public FarmerController(FarmerService farmerService)
        {
            _FarmerService = farmerService;
        }


       // GET: api/<FarmerController>
        [HttpGet("GetFarmers")]
        public ActionResult GetFarmers()
        {
            var model = _FarmerService.GetFarmers();
            if (model == null)
                return StatusCode(StatusCodes.Status204NoContent, "No records of farmers found");
            return Ok(model);
        }

        // GET: api/<FarmerController>
        [HttpGet("GetFarmersByOfficerRegistration/{officerId}")]
        public ActionResult GetFarmersByOfficerRegistration(string officerId)
        {
            var model = _FarmerService.GetFarmersByOfficerRegistration(officerId);
            if (model == null)
                return StatusCode(StatusCodes.Status204NoContent, "No records of farmers found");
            return Ok(model);
        }


        //GET api/<FarmerController>/5
        [HttpGet("GetFarmerDetails/{farmerId}")]
        public ActionResult GetFarmerDetails(string farmerId)
        {
            var model = _FarmerService.GetFarmerDetails(farmerId);
            if (model == null)
                return StatusCode(StatusCodes.Status204NoContent, "No record of farmer found");
            return Ok(model);
        }

        // POST api/<FarmerController>
        [HttpPost("AddFarmer")]
        public ActionResult AddFarmer([FromBody] FarmerApiModel model)
        {
            bool result = _FarmerService.AddFarmer(model);
            if (result)
                return Ok(result);
            
            return BadRequest("Sorry something went wrong");
        }

        // PUT api/<FarmerController>/5
        [HttpPut("UpdateFarmerDetails/{farmerId}")]
        public ActionResult UpdateFarmerDetails(string farmerId, [FromBody] FarmerApiModel model) 
        {
            bool result = _FarmerService.UpdateFarmerDetails(model, farmerId);
            if (result)
                return Ok(result);

            return BadRequest("Sorry something went wrong");
        }

        //// DELETE api/<FarmerController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}



    }
}
