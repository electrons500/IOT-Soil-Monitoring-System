using Backendapi.Models.Data;
using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using Backendapi.Models.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Backendapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmMapLocationController : ControllerBase
    {
        private MapLocationService _MapLocationService;
        private IOTSMSDBContext _Context;
        public FarmMapLocationController(MapLocationService mapLocationService, IOTSMSDBContext context)
        {
            _MapLocationService = mapLocationService;
            _Context = context;
        }


        [HttpPost("AddMapLocation")]
        public ActionResult AddMapLocation([FromBody] DeviceMapLocationModel model)
        {
            bool result = _MapLocationService.AddorEditFarmMapLocation(model);
            if (result)
                return Ok(result);
             
            return StatusCode(StatusCodes.Status400BadRequest, "Data was not successfully submitted");
        }

        //Get list farms registered by officer and their map location
        
        [HttpGet("GetRegisteredFarmsMapLocation/{OfficerId}")]
        public ActionResult GetRegisteredFarmsMapLocation(string OfficerId)
        {
            var model = _MapLocationService.GetListOfRegisteredFarmAndTheirMapLocation(OfficerId);
            return Ok(model);
        }

        //Get location details
        
        [HttpGet("GetRegisteredFarmsMapLocationDetails/{mapId}")]
        public ActionResult GetRegisteredFarmsMapLocationDetails(string mapId) 
        {
            var model = _MapLocationService.GetFarmMapLocationDetails(mapId); 
            return Ok(model);
        }


        //Edit farm map location
        [HttpPost("UpdateFarmMapLocation")]
        public ActionResult UpdateFarmMapLocation(DeviceMapLocationModel model)
        {
            FarmMapLocation mapLocation = _Context.FarmMapLocation.Where(x => x.MaplocationId == model.SerialNumber).FirstOrDefault();
            DeviceMapLocationModel deviceMapLocation = new DeviceMapLocationModel()
            {
                SerialNumber = mapLocation.SerialNumber,
                Latitude = model.Latitude,
                Longitude = model.Longitude
               
            };

            bool result = _MapLocationService.AddorEditFarmMapLocation(deviceMapLocation);
            if (result)
                return Ok(result);

            return StatusCode(StatusCodes.Status400BadRequest, "Data was not successfully updated");
        }



    }
}
