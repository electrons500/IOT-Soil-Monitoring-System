using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backendapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoilCategoryController : ControllerBase
    {
        private SoilCategoryService _SoilCategoryService;
        public SoilCategoryController(SoilCategoryService soilCategoryService)
        {
            _SoilCategoryService = soilCategoryService;
        }

        // GET: api/<SoilCategoryController>
        [HttpGet("GetSoilCategories")]
        public ActionResult GetSoilCategory()  
        {
            var model = _SoilCategoryService.GetSoilCategories();
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Records not found");
            }
            else
            {
                return Ok(model);
            }
               
            
        }

        // GET api/<SoilCategoryController>/5
        [HttpGet("GetSoilCategoryDetails/{id}")]
        public ActionResult GetSoilCategoryDetails(int id)
        {
            var model = _SoilCategoryService.GetSoilCategoryDetails(id);

            return Ok(model);
        }

        // POST api/<SoilCategoryController>
        [HttpPost("AddSoilCategory")]
        public ActionResult AddSoilCategory([FromBody] SoilCategoryApiModel model)
        {
            bool result = _SoilCategoryService.AddNewSoil(model);
            if(result)
                return Ok(result);
            return StatusCode(StatusCodes.Status400BadRequest, "Error occured whiles add new soil category");
        }

        // PUT api/<SoilCategoryController>/5
        [HttpPut("UpdateSoilCategory/{id}")]
        public ActionResult UpdateSoilCategory(int id,[FromBody] SoilCategoryApiModel model)
        {
            bool result = _SoilCategoryService.updateSoilCategory(id,model);
            if (result)
                return Ok(result);

            return StatusCode(StatusCodes.Status400BadRequest, "Error occured whiles try to update record");
        }

        // DELETE api/<SoilCategoryController>/5
        [HttpDelete("RemoveSoilCategory/{id}")]
        public ActionResult RemoveSoilCategory(int id)
        {
            bool result = _SoilCategoryService.DeleteSoilCategory(id);  
            if (result)
                return Ok(result);

            return StatusCode(StatusCodes.Status400BadRequest, "Error occured whiles try to update record");
        }
    }
}
