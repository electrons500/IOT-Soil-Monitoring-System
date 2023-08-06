using Frontend.Models.Service;
using Frontend.Models.Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Identity;
using Frontend.Models.Data;
using static Frontend.Models.Enum;
using Frontend.Models.Data.IOTSMSDBContext;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace Frontend.Controllers
{
    
    public class ArduinoController : BaseController
    {
        private ArduinoService _ArduinoService;
        private IOTSMSDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
      
        string SessionPath = "";
        string SessionLoginUser = "";

        public ArduinoController(ArduinoService arduinoService, UserManager<ApplicationUser> userManager, IOTSMSDBContext db)
        {
            _ArduinoService = arduinoService;
            _userManager = userManager;
            _db = db;
        }


        // GET: ArduinoController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeployedArduinoList()
        {
            var model = _ArduinoService.GetArduinos();
            return Json(new { data = model });
        }
        

        // GET: ArduinoController/Details/5
        public ActionResult ArduinoDetails(string id)
        {
            var model = _ArduinoService.GetArduinoDetails(id);
            var user = _db.Users.Where(x => x.Id == model.UserId).FirstOrDefault();

            if (model.IsActive == false)
            {
                ViewData["IsActive"] = "No";
            }
            else
            {
                ViewData["IsActive"] = "Yes";
            }

            if (model.IsActivated == false)
            {
                ViewData["IsActivated"] = "No";
            }
            else
            {
                ViewData["IsActivated"] = "Yes";
            }
            if (model.IsOnsite == false)
            {
                ViewData["IsOnsite"] = "No";
            }
            else
            {
                ViewData["IsOnsite"] = "Yes";
            }

            if(model.DateOfActivation == "1/1/0001")
            {
                ViewData["DateOfActivation"] = "No set date";
            }
            else
            {
                ViewData["DateOfActivation"] = model.DateOfActivation;
            }
            ViewData["username"] = user.FullName;
            
            
            ViewData["path"] = GetActionPath();



            return View(model);
        }

        // GET: ArduinoController/Create
       
        public ActionResult AddArduino()
        {
            
            return View();
        }

        // POST: ArduinoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArduino(ArduinoViewModel model)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                
                bool result = _ArduinoService.AddArduino(model, userId);
                if (result)
                {
                    Notify("Arduino device successfully added!", "Congratulations", notificationType: NotificationType.success);

                    return RedirectToAction(nameof(Index));
                }

                throw new Exception();
            }
            catch
            {
                return View();
            }
        }


        // GET: ArduinoController/Edit/5
        public ActionResult UpdateArduino(string id)
        {
            ViewData["hide"] = false;
            var model = _ArduinoService.GetArduinoDetails(id);
            return View(model);
        }

        // POST: ArduinoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateArduino(ArduinoViewModel model)
        {
            try
            {
                bool result = _ArduinoService.UpdateArduino(model);
                if (result)
                {
                    Notify("Arduino device successfully updated!", "Congratulations", notificationType: NotificationType.success);

                    return RedirectToAction(nameof(Index));
                }
                throw new Exception();
            }
            catch
            {
                return View();
            }
        }



         // GET: ArduinoController/Edit/5
        public ActionResult UpdateArduinoByOfficer(string id)
        {
            ViewData["hide"] = false;
            var model = _ArduinoService.GetArduinoDetails(id);
            return View(model);
        }

        // POST: ArduinoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateArduinoByOfficer(ArduinoViewModel model)
        {
            try
            { 
                bool result = _ArduinoService.UpdateArduino(model);
                if (result)
                {
                    Notify("Arduino device successfully updated!", "Congratulations", notificationType: NotificationType.success);

                    return RedirectToAction(nameof(Index));
                }
                throw new Exception();
            }
            catch
            {
                return View();
            }
        }






       
        // DELETE: ArduinoController/Delete/5
        [HttpDelete]
        public ActionResult DeleteArduino(string id)
        {
            try
            {
                bool result = _ArduinoService.DeleteArduino(id);
                if (result)
                {
                    return Json(new { success = true, message = "Data successfully deleted!" });
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    
        //Get all arduino device registerd by Agric extension officer
        public ActionResult GetArduinoDevicesRegisteredByAgricExtOfficer()
        {
            return View();

        }
         [HttpGet]
        public ActionResult GetArduinoDevicesRegisteredByAgricExtOfficerList()
        {

            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetArduinoDevicesRegisteredByAgricExtOfficer");
            }


            var userId = _userManager.GetUserId(User);
            var model = _ArduinoService.GetArduinoDevicesRegisteredByAgricExtOfficer(userId);
            return Json(new { data = model });

        }


        //Get list of unactivated device
        public ActionResult GetUnactivatedDevices()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetUnactivatedDevicesList()
        {
            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetUnactivatedDevices");
            }

            var model = _ArduinoService.GetUnActivateArduino();
            return Json(new { data = model });
        }

         //Get list of activated device
        public ActionResult GetActivatedDevices()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetActivatedDevicesList()
        {
            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetActivatedDevices");
            }

            var model = _ArduinoService.GetActivatedArduino();
            return Json(new { data = model });
        }

         //Get list of activated device that are not onsite
        public ActionResult GetActivatedDevicesAndNotOnsite()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetActivatedDevicesAndNotOnsiteList()
        {
            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetActivatedDevicesAndNotOnsite");
            }


            var model = _ArduinoService.GetActivatedDevicesAndNotOnsite();
            return Json(new { data = model });
        }


        //Get list of activated device that are onsite
        public ActionResult GetActivatedDevicesButAreOnsite()
        { 
            return View();
        }

        [HttpGet]
        public ActionResult GetActivatedDevicesButAreOnsiteList()
        {
            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetActivatedDevicesButAreOnsite");
            }

            var model = _ArduinoService.GetActivatedDevicesButAreOnsite();
            return Json(new { data = model });
        }

         //Get list of activated device that are onsite and active
        public ActionResult GetActivatedDevicesThatAreOnsiteAndActive()
        { 
            return View(); 
        }

        [HttpGet]
        public ActionResult GetActivatedDevicesThatAreOnsiteAndActiveList()
        {
            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetActivatedDevicesThatAreOnsiteAndActive");
            }
            var model = _ArduinoService.GetActivatedDevicesThatAreOnsiteAndActive();
            return Json(new { data = model });
        }

        //Get list of activated device that are onsite and not active
        public ActionResult GetActivatedDevicesThatAreOnsiteAndUnactive()
        { 
            return View(); 
        }

        [HttpGet]
        public ActionResult GetActivatedDevicesThatAreOnsiteAndUnactiveList()
        {
            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetActivatedDevicesThatAreOnsiteAndUnactive");
            }

            var model = _ArduinoService.GetActivatedDevicesThatAreOnsiteAndUnactive();
            return Json(new { data = model });
        }

        //Get list of activated device bought by officer
        public ActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficer()
        { 
            return View(); 
        }

        [HttpGet]
        public ActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficerList()
        {
            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetActivatedDevicesRegisterdByAgricExtensionOfficer");
            }


            var userId = _userManager.GetUserId(User);
            var model = _ArduinoService.GetActivatedDevicesRegisterdByAgricExtensionOfficer(userId);
            return Json(new { data = model });
        }

        //Get list of unactivated device bought by officer
        public ActionResult GetUnActivatedDevicesRegisterdByAgricExtensionOfficer()
        { 
            return View(); 
        }
        
        [HttpGet]
        public ActionResult GetUnActivatedDevicesRegisterdByAgricExtensionOfficerList()
        {
            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetUnActivatedDevicesRegisterdByAgricExtensionOfficer");
            }


            var userId = _userManager.GetUserId(User);
            var model = _ArduinoService.GetUnActivatedDevicesRegisterdByAgricExtensionOfficer(userId);
            return Json(new { data = model });
        }

        //Get list of activated device registered by officer that are not onsite
        public ActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficerAndNotOnsite()
        {
            return View();
        } 

        [HttpGet]
        public async Task<ActionResult> GetActivatedDevicesRegisterdByAgricExtensionOfficerAndNotOnsiteList()
        {

            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath))) 
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetActivatedDevicesRegisterdByAgricExtensionOfficerAndNotOnsite");
            }

            var user = await _userManager.GetUserAsync(User);
            var model = _ArduinoService.GetActivatedDevicesRegisterdByAgricExtensionOfficerAndNotOnsite(user.Id);
            return Json(new { data = model });
        }

         //Get list of activated device registered by officer and onsite
        public ActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsite()
        {
            return View();
        } 
         
        [HttpGet]
        public async Task<ActionResult> GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsiteList()
        {
            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsite");
            }

            var user = await _userManager.GetUserAsync(User);
            var model = _ArduinoService.GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsite(user.Id);
            return Json(new { data = model });
        }

         //Get list of activated device registered by officer and onsite
        public ActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndActive()
        {
            return View();
        } 
         
        [HttpGet]
        public ActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndActiveList() 
        {
            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndActive");
            }

            var userId = _userManager.GetUserId(User);
            var model = _ArduinoService.GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndActive(userId);
            return Json(new { data = model });
        }


        //Get list of activated device registered by officer and onsite and active
        public ActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndUnactive()
        {
            return View();
        } 
         
        [HttpGet] 
        public ActionResult GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndUnactiveList() 
        {
            HttpContext.Session.SetString(SessionPath, "");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionPath)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionPath, "GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndUnactive");
            }

            var userId = _userManager.GetUserId(User);
            var model = _ArduinoService.GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndUnactive(userId);
            return Json(new { data = model });
        }


        private string GetActionPath()
        {
            if (HttpContext.Session.GetString(SessionPath) != null)
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                var actionpath = HttpContext.Session.GetString(SessionPath);
                return actionpath;
            }

            return null;
        }


        //Verify device
        public ActionResult VerifyDevice()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult VerifyDevice(VerifyArduinoViewModel model)
        {
            bool result = _ArduinoService.VerifyDevice(model);
            if (result)
            {
                Notify("Device has been successfully verified!", "Congratulations", notificationType: NotificationType.success);

            }
            else
            {
                Notify("This device is not recoganized!", "Error", notificationType: NotificationType.error);

            }
            return View();
         }

        //Verify device
        public ActionResult ActivateDevice()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<ActionResult> ActivateDevice(ArduinoViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            ArduinoViewModel arduino = new ArduinoViewModel()
            {
                SerialNumber = model.SerialNumber,
                UserId = user.Id
            };
            bool result = _ArduinoService.ActivateDevice(arduino);
            if (result)
            {
                Notify("Device has been successfully activated!", "Congratulations", notificationType: NotificationType.success);
                

            }
            else
            {
                Notify("This device isn't recoganized.Verify the device", "Error", notificationType: NotificationType.error);

            }
            return View();
         }

        public ActionResult GenerateReport()
        {
            return View();
        }




        //Generate report of list of all devices registered by officer
        public async Task<ActionResult> RegisteredArduinoReport()   
        {
            
            var user = await _userManager.GetUserAsync(User);
            bool result = _ArduinoService.SaveRegisteredDevicesByOfficerAsJsonFile(user.Id);
            if (result)
            {
                return View();
            }

            throw new Exception();
        }

        [HttpDelete]
        public ActionResult SetArduinoToActive(string id)
        {
            try
            {
                bool result = _ArduinoService.SetArduinoToActive(id);
                if (result)
                {
                    return Json(new { success = true, message = "Device successfully set to active!" });
                }
                return RedirectToAction("GetArduinoDevicesRegisteredByAgricExtOfficer");
            }
            catch(Exception)
            {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult SetArduinoToInactive(string id)
        {
            try
            {
                bool result = _ArduinoService.SetArduinoToInactive(id);
                if (result)
                {
                    return Json(new { success = true, message = "Device successfully set to inactive!" });
                }
                return RedirectToAction("GetArduinoDevicesRegisteredByAgricExtOfficer");
            }
            catch(Exception)
            {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult SetArduinoIfOnSite(string id)
        {
            try
            {
                bool result = _ArduinoService.SetArduinoIfOnSite(id);
                if (result)
                {
                    return Json(new { success = true, message = "Device successfully set to Onsite!" });
                }
                return RedirectToAction("GetArduinoDevicesRegisteredByAgricExtOfficer");
            }
            catch(Exception)
            {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult ChangeArduinoToOffsite(string id)
        {
            try
            {
                bool result = _ArduinoService.ChangeArduinoToOffsite(id);
                if (result)
                {
                    return Json(new { success = true, message = "Device successfully set to Onsite!" });
                }
                return RedirectToAction("GetArduinoDevicesRegisteredByAgricExtOfficer");
            }
            catch(Exception)
            {
                return View();
            }
        }




    }
}
