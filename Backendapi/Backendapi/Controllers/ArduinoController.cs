using Backendapi.Models.Data;
using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.IOTSMSDBContext;
using Backendapi.Models.Data.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Net;


namespace Backendapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArduinoController : ControllerBase
    {
        private ArduinoService _arduinoService;
        private UserManager<ApplicationUser> _userManager;
        private SMSService _SMSService;
        private IOTSMSDBContext _Context;
        public ArduinoController(ArduinoService arduinoService, UserManager<ApplicationUser> userManager, SMSService sMSService, IOTSMSDBContext context)
        {
            _arduinoService = arduinoService;
            _userManager = userManager;
            _SMSService = sMSService;
            _Context = context;
        }


        // GET: api/<ArduinoController>
        [HttpGet("GetArduinoList")]
        public IActionResult GetArduinoList()
        {

            var model = _arduinoService.GetArduinos();
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }

        // GET: api/<ArduinoController>
        [HttpGet("GetArduinoDevicesByAgricExtensionOfficer/{id}")]
        public IActionResult GetArduinoDevicesByAgricExtensionOfficer(string id)
        {

            var model = _arduinoService.GetArduinoDevicesByAgricOfficer(id);
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }
         // GET: api/<ArduinoController>
        [HttpGet("GetDevicesRegisteredByOfficer/{id}")]
        public IActionResult GetDevicesRegisteredByOfficer(string id)
        {
             
            var model = _arduinoService.GetDevicesRegisteredByOfficer(id);
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }


        // GET api/<ArduinoController>/5
        [HttpGet("GetArduinoDetails/{id}")]
        public ActionResult GetArduinoDetails(string id)
        {
            var model = _arduinoService.GetArduinoDetail(id);
            if (model == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }

        
        [HttpGet("VerifyArduino/{serialnumber}")]
        public ActionResult VerifyArduino(string serialnumber)
        {
            try
            {
                bool result = _arduinoService.VerifyDevice(serialnumber);
                if (result)
                {

                    Arduino arduino = _Context.Arduino.Where(x => x.SerialNumber == serialnumber).FirstOrDefault();
                    ApplicationUser user = _Context.Users.Where(x => x.Id == arduino.UserId).FirstOrDefault();
                    string officerNumber = user.PhoneNumber;
                    string message = "The device with the serial number " + serialnumber + " is successfully verified";
                   // bool msgResult = _SMSService.SendSMS(message, officerNumber); //send message to officer

                    return Ok(result);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "This device is not recognized.");

            }
            catch (WebException)
            {

                throw;
            }

        }

        [HttpGet("IsDeviceVerified/{serialnumber}")] 
        public ActionResult IsDeviceVerified(string serialnumber)
        {
            
                bool result = _arduinoService.CheckDeviceIsNotVerified(serialnumber);
                if (result)
                {

                    return Ok(result);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "This device is not recognized.");


        }


        // PUT api/<ArduinoController>/5
        [HttpGet("GetActivateArduino")]
        public ActionResult GetActivateArduino()
        {
            var model = _arduinoService.GetActivatedDevices();
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);

        }
        // GET api/<ArduinoController>
        [HttpGet("GetUnActivateArduino")]
        public ActionResult GetUnActivateArduino()
        {
            var model = _arduinoService.GetUnActivatedDevices();
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }

        // GET: api/<ArduinoController>
        [HttpGet("GetActivatedDevicesAndNotOnsite")]
        public IActionResult GetActivatedDevicesAndNotOnsite()
        {

            var model = _arduinoService.GetActivatedDevicesAndNotOnsite();
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }
        // GET: api/<ArduinoController>
        [HttpGet("GetActivatedDevicesButAreOnsite")]
        public IActionResult GetActivatedDevicesButAreOnsite()
        {

            var model = _arduinoService.GetActivatedDevicesButAreOnsite();
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }

        // GET: api/<ArduinoController>
        [HttpGet("GetActivatedDevicesThatAreOnsiteAndActive")]
        public IActionResult GetActivatedDevicesThatAreOnsiteAndActive()
        {

            var model = _arduinoService.GetActivatedDevicesThatAreOnsiteAndActive();
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }

        // GET: api/<ArduinoController>
        [HttpGet("GetActivatedDevicesThatAreOnsiteAndUnactive")]
        public IActionResult GetActivatedDevicesThatAreOnsiteAndUnactive()
        {

            var model = _arduinoService.GetActivatedDevicesThatAreOnsiteAndUnactive();
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }

        // GET: api/<ArduinoController>
        [HttpGet("GetActivatedDevicesRegisterdByAgricExtensionOfficer/{id}")]
        public IActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficer(string id)
        {

            var model = _arduinoService.GetActivatedDevicesRegisterdByAgricExtensionOfficer(id);
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }

        // GET: api/<ArduinoController>
        [HttpGet("GetUnActivatedDevicesRegisterdByAgricExtensionOfficer/{id}")]
        public IActionResult GetUnActivatedDevicesRegisterdByAgricExtensionOfficer(string id)
        {

            var model = _arduinoService.GetUnActivatedDevicesRegisterdByAgricExtensionOfficer(id);
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }

        // GET: api/<ArduinoController>
        [HttpGet("GetActivatedDevicesRegisterdByAgricExtensionOfficerAndNotOnsite/{id}")]
        public IActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficerAndNotOnsite(string id)
        {

            var model = _arduinoService.GetActivatedDevicesRegisterdByAgricExtensionOfficerAndNotOnsite(id);
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }

        [HttpGet("GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsite/{id}")]
        public IActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsite(string id)
        {

            var model = _arduinoService.GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsite(id);
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }

        [HttpGet("GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndActive/{id}")]
        public IActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndActive(string id)
        {

            var model = _arduinoService.GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsite(id);
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }

        [HttpGet("GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndUnactive/{id}")]
        public IActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndUnactive(string id)
        {

            var model = _arduinoService.GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndUnactive(id);
            if (model.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "List of arduino not found");
            }
            return Ok(model);
        }



        // POST api/<ArduinoController>
        [HttpPost("AddNewArduino")]
        public IActionResult AddNewArduino([FromBody] ArduinoApiModel model)
        {

            bool result = _arduinoService.AddNewArduino(model);
            if (result)
                return Ok(result);
            return BadRequest("Sorry something went wrong");

        }


        // PUT api/<ArduinoController>/5
        [HttpPut("UpdateArduino/{id}")]
        public IActionResult UpdateArduino([FromBody] ArduinoApiModel model)
        {

            bool result = _arduinoService.UpdateArduino(model);
            if (result)
                return Ok(result);

            return StatusCode(StatusCodes.Status400BadRequest, "Sorry something went wrong");
        }
        
        //Activate device by officer
       
        [HttpPut("ActivateArduino/{serialnumber}")]
        public ActionResult ActivateArduino([FromBody] ArduinoApiModel model)
        {

            bool result = _arduinoService.ActivateArduino(model.SerialNumber,model.UserId);
            if (result)
            {
                ApplicationUser user = _Context.Users.Where(x => x.Id == model.UserId).FirstOrDefault();
                string officerNumber = user.PhoneNumber;
                string message = "The device with the serial number " + model.SerialNumber + " is successfully activated.";
                //bool msgResult = _SMSService.SendSMS(message, officerNumber); //send message to officer
                return Ok(result); 
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Sorry something went wrong");
        }

        // PUT api/<ArduinoController>/5
        [HttpPut("ActivateArduinoByDeviceOnsite/{id}")]
        public IActionResult ActivateArduinoByDeviceOnsite(string id)
        {

            bool result = _arduinoService.ActivateArduinoByDeviceOnsite(id);
            if (result)
                return Ok(result);

            return StatusCode(StatusCodes.Status400BadRequest, "Sorry something went wrong");
        }



      


        // PUT api/<ArduinoController>/5
        [HttpPut("SetArduinoToActive/{deviceId}")]
        public ActionResult SetArduinoToActive(string deviceId)
        {

            bool result = _arduinoService.SetArduinoToActive(deviceId);
            if (result)
                return Ok(result);

            return StatusCode(StatusCodes.Status400BadRequest, "Sorry something went wrong");
        }

        // PUT api/<ArduinoController>/5
        [HttpPut("SetArduinoToInactive/{deviceId}")]
        public ActionResult SetArduinoToInactive(string deviceId)
        {

            bool result = _arduinoService.SetArduinoToInactive(deviceId);
            if (result)
                return Ok(result);

            return StatusCode(StatusCodes.Status400BadRequest, "Sorry something went wrong");
        }

        // PUT api/<ArduinoController>/5
        [HttpPut("SetArduinoIfOnSite/{deviceId}")]
        public ActionResult SetArduinoIfOnSite(string deviceId)
        {

            bool result = _arduinoService.SetArduinoIfOnSite(deviceId);
            if (result)
                return Ok(result);

            return StatusCode(StatusCodes.Status400BadRequest, "Sorry something went wrong");
        }
        // PUT api/<ArduinoController>/5
        [HttpPut("ChangeArduinoToOffsite/{deviceId}")]
        public ActionResult ChangeArduinoToOffsite(string deviceId)
        {

            bool result = _arduinoService.ChangeArduinoToOffsite(deviceId);
            if (result)
                return Ok(result);

            return StatusCode(StatusCodes.Status400BadRequest, "Sorry something went wrong");
        }


        // DELETE api/<ArduinoController>/5
        [HttpDelete("DeleteArduino/{id}")]
        public ActionResult DeleteArduino(string id)
        {
            bool result = _arduinoService.DeleteArduino(id);
            if (result)
                return Ok(result);

            return StatusCode(StatusCodes.Status400BadRequest, "Sorry something went wrong");
        }
    }
}
