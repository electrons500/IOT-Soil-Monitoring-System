using Frontend.Models.Service;
using Frontend.Models.Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using static Frontend.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Frontend.Models.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Frontend.Models.Data.Service;

namespace Frontend.Controllers
{
   
    public class FarmController : BaseController
    {
        private FarmService _FarmService;
        private RegionService _regionService;
        private SoilCategoryService _SoilCategoryService;
        private FarmMapLocationService _FarmMapLocationService;
        private readonly UserManager<ApplicationUser> _userManager;
        string SessionFarmerId = "";
        public FarmController(FarmService farmService, SoilCategoryService soilCategoryService, RegionService regionService, UserManager<ApplicationUser> userManager,
            FarmMapLocationService farmMapLocationService
            )
        {
            _FarmService = farmService;
            _SoilCategoryService = soilCategoryService;
            _regionService = regionService;
            _userManager = userManager;
            _FarmMapLocationService = farmMapLocationService;
        }


        // GET: FarmController
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetFarmList()
        {
            var model = _FarmService.GetFarms();
            return Json(new { data = model });
        }

        //Get all farms registered by officer

        public ActionResult FarmsRegisteredByOfficer()
        { 

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> FarmsRegisteredByOfficerList()  
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _FarmService.GetFarmsRegisteredByOfficer(user.Id);
            return Json(new { data = model });
        }

        //Get all farms registered by officer and their map coordinates
        public ActionResult GetRegisteredFarmAndTheirMapLocation()  
        {

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetRegisteredFarmAndTheirMapLocationList() 
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _FarmMapLocationService.GetListOfRegisteredFarmAndTheirMapLocation(user.Id);
            return Json(new { data = model });
        }

        public ActionResult GetAllFarmersfarms(string id)
        {
            HttpContext.Session.Clear();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionFarmerId)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionFarmerId, id);
            }

            return View();
        }

        [HttpGet]
        public ActionResult GetAllFarmersfarmsList()
        {
            string GetfarmerId = HttpContext.Session.GetString(SessionFarmerId);
            var model = _FarmService.GetFarmsbyFarmerId(GetfarmerId);
            return Json(new { data = model });
        }


         public ActionResult GetAllFarmsByFarmerId(string id) 
        {
            HttpContext.Session.Clear();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionFarmerId)))
            {
                //store farmer id in a cookie for only 10 seconds and expire.
                HttpContext.Session.SetString(SessionFarmerId, id);
            }

            return View();
        }

        [HttpGet]
        public ActionResult GetAllFarmsByFarmerIdList() 
        {
            string GetfarmerId = HttpContext.Session.GetString(SessionFarmerId);
            var model = _FarmService.GetFarmsbyFarmerId(GetfarmerId);
            return Json(new { data = model });
        }

        // GET: FarmController/Details/5
        public ActionResult GetFarmDetails(string id)
        {
            var model = _FarmService.GetFarmDetails(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult GetFarmerfarmDetails(string id)
        {
            var model = _FarmService.GetFarmDetails(id);
            return View(model);
        }

        // GET: FarmController/Create
        public ActionResult AddFarm()
        {
            var model = _FarmService.CreateFarms();
            return View(model);
        }

        // POST: FarmController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFarm(FarmViewModel model, string id)
        {

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    id = HttpContext.Session.GetString(SessionFarmerId);
                }

                FarmViewModel farm = new FarmViewModel()
                {
                    FarmId = Guid.NewGuid().ToString(),
                    FarmName = model.FarmName,
                    Location = model.Location,
                    DateCreated = DateTime.Now.ToShortDateString(),
                    FarmerId = id,
                    FarmerName = "",
                    RegionId = model.RegionId,
                    SoilCategoryId = model.SoilCategoryId,
                    SoilCategoryName = "",
                    FarmerContact = "",
                    RegionName = "",
                    SerialNumber = model.SerialNumber,
                    ArduinoId = "",
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                    

                };


                bool result = _FarmService.AddFarm(farm);
                if (result)
                {
                    Notify("Farm information successfully saved!", "Congratulations", notificationType: NotificationType.success);
                    return RedirectToAction("GetFarmersByOfficers","Farmer");

                    //next coding edit farms
                }

                throw new Exception();
            }
            catch
            {
                return View();
            }
        }

        // GET: FarmController/Edit/5
        public ActionResult UpdateFarm(string id)
        {
            var farm = _FarmService.GetFarmDetails(id);
            FarmViewModel model = new FarmViewModel()
            {
                FarmId = farm.FarmId,
                FarmName = farm.FarmName,
                Location = farm.Location,
                DateCreated = farm.DateCreated,
                FarmerId = farm.FarmerId,
                FarmerName = farm.FarmerName,
                RegionId = farm.RegionId,
                SoilCategoryId = farm.SoilCategoryId,
                RegionList = new SelectList(_regionService.GetRegions(), "RegionId", "RegionName"),
                SoilCategoryList = new SelectList(_SoilCategoryService.GetSoilCategories(), "SoilCategoryId", "SoilName"),
                SerialNumber = farm.SerialNumber


            };
            return View(model);
        }

        // POST: FarmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFarm(FarmViewModel model)
        {
            try
            {
                FarmViewModel farm = new FarmViewModel()
                {
                    FarmId = model.FarmId,
                    FarmName = model.FarmName,
                    Location = model.Location,
                    DateCreated = "",
                    FarmerId = model.FarmerId,
                    FarmerName = "",
                    RegionId = model.RegionId,
                    SoilCategoryId = model.SoilCategoryId,
                    SoilCategoryName = "",
                    FarmerContact = "",
                    RegionName = "",
                    SerialNumber = model.SerialNumber,
                    ArduinoId = "",

                };

                bool result = _FarmService.UpdateFarm(farm);
                if (result)
                {
                    Notify("Farm information successfully updated!", "Congratulations", notificationType: NotificationType.success);
                    return RedirectToAction("GetFarmersByOfficers", "Farmer");
                }

                throw new Exception();
            }
            catch
            {
                return View();
            }
        }

         // GET: FarmController/Edit/5
        public ActionResult UpdateFarmerFarm(string id)
        {
            var farm = _FarmService.GetFarmDetails(id);
            FarmViewModel model = new FarmViewModel()
            {
                FarmId = farm.FarmId,
                FarmName = farm.FarmName,
                Location = farm.Location,
                DateCreated = farm.DateCreated,
                FarmerId = farm.FarmerId,
                FarmerName = farm.FarmerName,
                RegionId = farm.RegionId,
                SoilCategoryId = farm.SoilCategoryId,
                RegionList = new SelectList(_regionService.GetRegions(), "RegionId", "RegionName"),
                SoilCategoryList = new SelectList(_SoilCategoryService.GetSoilCategories(), "SoilCategoryId", "SoilName"),
                SerialNumber = farm.SerialNumber


            };
            return View(model);
        }

        // POST: FarmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFarmerFarm(FarmViewModel model) 
        {
            try
            {
                FarmViewModel farm = new FarmViewModel()
                {
                    FarmId = model.FarmId,
                    FarmName = model.FarmName,
                    Location = model.Location,
                    DateCreated = "",
                    FarmerId = model.FarmerId,
                    FarmerName = "",
                    RegionId = model.RegionId,
                    SoilCategoryId = model.SoilCategoryId,
                    SoilCategoryName = "",
                    FarmerContact = "",
                    RegionName = "",
                    SerialNumber = model.SerialNumber,
                    ArduinoId = "",

                };

                bool result = _FarmService.UpdateFarm(farm);
                if (result)
                {
                    Notify("Farm information successfully updated!", "Congratulations", notificationType: NotificationType.success);
                    return RedirectToAction("GetAllFarmersfarms");
                }

                throw new Exception();
            }
            catch
            {
                return View();
            }
        }


        // POST: FarmController/Delete/5
        [HttpDelete]
        public ActionResult DeleteFarm(string id)
        {
            try
            {
                bool result = _FarmService.RemoveFarm(id);
                if (result)
                {
                    return Json(new { success = true, message = "Message successfully deleted!" });
                }

                throw new Exception();
            }
            catch
            {
                return View();
            }
        }


        //Generate of list of all devices and owner
        public ActionResult GenerateReport()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateReport(IFormCollection collection)
        {
            return View();
        }

        //Get Farm map location details
        public ActionResult FarmMaplocationDetails(string id) 
        {
            var model = _FarmMapLocationService.GetFarmMapDetails(id);
            ViewData["googlemapURL"] = "https://maps.google.com/?q=" + model.Latitude + "," + model.Longitude + "";
            return View(model);
        }
        
        //Edit farm map details
        public ActionResult EditFarmMapLocationDetails(string id)
        {
            var model = _FarmMapLocationService.GetFarmMapDetails(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFarmMapLocationDetails(FarmMapLocationViewModel model,string id)
        {
            try
            {
                FarmMapLocationModel farmMapLocation = new FarmMapLocationModel()
                {
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    SerialNumber = id
                    
                };


                bool result = _FarmMapLocationService.UpdateFarmMapLocation(farmMapLocation);
                if (result)
                {
                    Notify("Farm map location information successfully saved!", "Congratulations", notificationType: NotificationType.success);
                    return RedirectToAction("GetRegisteredFarmAndTheirMapLocation");
                }

                throw new Exception();
            }
            catch (Exception)
            {

                throw;
            }
           
        }



    }
}
