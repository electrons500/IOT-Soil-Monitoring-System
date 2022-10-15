using Frontend.Models.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class SoilDataController : BaseController
    {
        private SoilDataService _SoilDataService;
        private string SessionSoilData = "";
        string farmId;
        public SoilDataController(SoilDataService soilDataService)
        {
            _SoilDataService = soilDataService;
        }
        // GET: SoilDataController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SoilDataFromAllDevicesList()
        {
            var model = _SoilDataService.GetSoilData();
            return Json(new { data = model });
        }

        public ActionResult SoilDataFromDevice(string id) 
        {
            HttpContext.Session.Clear();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionSoilData)))
            {
                HttpContext.Session.SetString(SessionSoilData, id);
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult SoilDataFromDeviceList()
        {         
            if (!(string.IsNullOrEmpty(HttpContext.Session.GetString(SessionSoilData))))
            {
                farmId = HttpContext.Session.GetString(SessionSoilData);
            }

            var model = _SoilDataService.GetSoilDataByDeviceId(farmId);
            return Json(new { data = model });
        }

         public ActionResult ViewSoilDataFromDeviceByAdmin(string id) 
        {
            HttpContext.Session.Clear();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionSoilData)))
            {
                HttpContext.Session.SetString(SessionSoilData, id);
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult ViewSoilDataFromDeviceByAdminList()  
        {          
            if (!(string.IsNullOrEmpty(HttpContext.Session.GetString(SessionSoilData))))
            {
                farmId = HttpContext.Session.GetString(SessionSoilData);
            }

            var model = _SoilDataService.GetSoilDataByDeviceId(farmId);
            return Json(new { data = model });
        }


        [HttpGet]
        public ActionResult ViewSoilData(int id) 
        {
            var model = _SoilDataService.GetSoilDataDetails(id);
            return View(model);
        }


         [HttpGet] 
        public ActionResult ViewSoilDataByAdmin(int id) 
        {
            var model = _SoilDataService.GetSoilDataDetails(id);
            return View(model);
        }




        [HttpDelete]
        public ActionResult DeleteSoilData(int id)
        {
            try
            {
                bool result = _SoilDataService.RemoveSoilData(id);
                if (result)
                {
                    return Json(new { success = true, message = "Data successfully deleted!" });
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


    }
}
