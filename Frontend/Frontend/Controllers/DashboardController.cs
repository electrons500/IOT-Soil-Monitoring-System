using Frontend.Models.Data;
using Frontend.Models.Data.Service;
using Frontend.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    
    public class DashboardController : BaseController
    {
        private ArduinoService _ArduinoService;
        private SettingsService _SettingsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private FarmerService _FarmerService;
        private FarmService _FarmService;
        public DashboardController(ArduinoService arduinoService, SettingsService settingsService, UserManager<ApplicationUser> userManager, FarmerService farmerService, FarmService farmService)
        {
            _ArduinoService = arduinoService;
            _SettingsService = settingsService;
            _userManager = userManager;
            _FarmerService = farmerService;
            _FarmService = farmService;
        }

        // GET: DashboardController
        public ActionResult Index()
        {
            return View();
        }



        // GET: DashboardController/AgricOfficers
        public ActionResult AgricOfficers()
        {
            var userId = _userManager.GetUserId(User);
            ViewData["GetArduinoDevicesRegisteredByAgricExtOfficer"] = _ArduinoService.GetArduinoDevicesRegisteredByAgricExtOfficer(userId).Count();

            ViewData["GetArduinoDevicesRegisteredByAgricExtOfficerAndOnsite"] = _ArduinoService.GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsite(userId).Count();
            ViewData["GetFarmersByOfficerRegistration"] = _FarmerService.GetFarmersByOfficerRegistration(userId).Count();
            ViewData["GetfarmsByOfficerRegistration"] = _FarmService.GetFarmsRegisteredByOfficer(userId).Count();

            return View();
        }

       
        [HttpGet]
        public ActionResult AgricOfficerDevicesList() 
        {
            var userId = _userManager.GetUserId(User);
            var model = _ArduinoService.GetArduinoDevicesRegisteredByAgricExtOfficer(userId);
            return Json(new { data = model });
        }

      

        public ActionResult Admins() 
        {

            ViewData["NumberOfOfficers"] = _SettingsService.GetNumberOfExtensionOfficers();
            ViewData["NumberOfFarmers"] = _SettingsService.GetNumberOfFarmers();
            ViewData["NumberOfFarms"] = _SettingsService.GetNumberOfFarms();
            ViewData["NumberOfRegisteredDevices"] = _SettingsService.GetNumberOfRegisteredDevices();
            
            var model = _ArduinoService.GetArduinos();
            return View(model);
        }

        [HttpGet]
        public ActionResult AdminArduinoList() 
        {
            var model = _ArduinoService.GetArduinos();
            return Json(new { data = model });
        }




    }
}
