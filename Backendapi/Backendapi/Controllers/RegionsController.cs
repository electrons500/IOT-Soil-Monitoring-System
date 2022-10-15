using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backendapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private RegionService _RegionService;
        public RegionsController(RegionService regionService)
        {
            _RegionService = regionService;
        }

        //Get Regions

        [HttpGet("GetRegions")]
        public ActionResult GetRegions()
        {
            var model = _RegionService.GetRegions();

            return Ok(model);
        }

        [HttpGet("GetRegionsDetails/{RegionId}")]
        public ActionResult GetRegionsDetails(int RegionId)
        {
            var model = _RegionService.GetRegionsDetails(RegionId); 
            return Ok(model);
        }

        [HttpPost("AddRegions")]
        public ActionResult AddRegions([FromBody] RegionApiModel model)
        {
            bool result = _RegionService.AddNewRegion(model);
            if (result)
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Fail to add new region");
        }


        [HttpPut("UpdateRegions/{RegionId}")]
        public ActionResult UpdateRegions([FromBody] RegionApiModel model)
        {
            bool result = _RegionService.EditRegion(model);
            if (result)
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Fail to update region");
        }

        [HttpDelete("DeleteRegions/{RegionId}")]
        public ActionResult DeleteRegions(int RegionId)
        {
            bool result = _RegionService.DeleteRegion(RegionId);
            if (result)
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Fail to update region");
        }





    }
}
