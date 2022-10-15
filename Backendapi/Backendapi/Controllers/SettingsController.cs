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
    public class SettingsController : ControllerBase
    {
        private SettingsService _SettingsService;
        public SettingsController(SettingsService settingsService)
        {
            _SettingsService = settingsService;
        }

        // GET: api/<SettingsController>
        [HttpGet("GetNumberOfExtensionOfficers")]
        public ActionResult GetNumberOfExtensionOfficers()
        {
            string OfficerCount = _SettingsService.GetNumberOfExtensionOfficers();
            return  Ok(OfficerCount);
        }

       // GET: api/<SettingsController>
        [HttpGet("GetNumberOfRegisteredDevices")]
        public ActionResult GetNumberOfRegisteredDevices()
        {
            return StatusCode(StatusCodes.Status200OK, _SettingsService.GetNumberOfRegisteredDevices());
        }
        // GET: api/<SettingsController>
        [HttpGet("GetNumberOfFarmers")]
        public ActionResult GetNumberOfFarmers()
        {
            return StatusCode(StatusCodes.Status200OK, _SettingsService.GetNumberOfFarmers()); 
        }

        [HttpGet("GetNumberOfFarms")]
        public ActionResult GetNumberOfFarms()
        {
            return StatusCode(StatusCodes.Status200OK, _SettingsService.GetNumberOfFarms()); 
        }

       

        
    }
}
