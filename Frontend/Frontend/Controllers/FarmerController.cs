using Frontend.Models.Data;
using Frontend.Models.Data.Service;
using Frontend.Models.Data.ViewModel;
using Frontend.Models.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Frontend.Models.Enum;

namespace Frontend.Controllers
{
    public class FarmerController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private FarmerService _FarmerService;
        public FarmerController(UserManager<ApplicationUser> userManager, FarmerService farmerService)
        {
            _userManager = userManager;
            _FarmerService = farmerService;
        }
        // GET: FarmerController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetFarmersList()  
        {
            var model = _FarmerService.GetFarmers();         
          return Json(new { data = model });

        }
        public ActionResult GetFarmersByOfficers()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetFarmersByOfficersList()   
        {
            string userId = _userManager.GetUserId(User);
            var model = _FarmerService.GetFarmersByOfficerRegistration(userId);       
          return Json(new { data = model });

        }

        // GET: FarmerController/Details/5
        public ActionResult FarmerDetails(string id)
        {
            var model = _FarmerService.GetFarmerDetails(id);
            return View(model); 
        }

         // GET: FarmerController/Details/5
        public ActionResult FarmerDetailsbyOfficer(string id) 
        {
            var model = _FarmerService.GetFarmerDetails(id);
            return View(model); 
        }

        // GET: FarmerController/RegisterFarmer
        public ActionResult RegisterFarmer()
        {
            var model = _FarmerService.CreateFarmers();
            return View(model);
        }

        // POST: FarmerController/RegisterFarmer
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult RegisterFarmer(FarmerViewModel model)
        {
            try
            {
                string userId = _userManager.GetUserId(User);
                bool result = _FarmerService.AddFarmer(model, userId);
                if (result)
                {
                    Notify("Farmer information successfully saved!", "Congratulations", notificationType: NotificationType.success);
                    return RedirectToAction(nameof(GetFarmersByOfficers));
                }

                throw new Exception();
               
            }
            catch
            {
                return View();
            }
        }

        // GET: FarmerController/Edit/5
        public ActionResult EditFarmerDetails(string id)
        {
            var model = _FarmerService.GetFarmerDetails(id);
            return View(model);
        }

        // POST: FarmerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult EditFarmerDetails(int id, FarmerViewModel model)
        {
            try
            {
                bool result = _FarmerService.UpdateFarmerDetails(model);
                if (result)
                {
                    Notify("Farmer information successfully updated!", "Congratulations", notificationType: NotificationType.success);
                    return RedirectToAction(nameof(Index)); 
                }

                throw new Exception();
            }
            catch
            {
                return View();
            }
        }
         // GET: FarmerController/Edit/5
        public ActionResult EditFarmerDetailsByOfficer(string id)
        {
            var model = _FarmerService.GetFarmerDetails(id);
            return View(model);
        }

        // POST: FarmerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult EditFarmerDetailsByOfficer(FarmerViewModel model) 
        {
            try
            {
                bool result = _FarmerService.UpdateFarmerDetails(model);
                if (result)
                {
                    Notify("Farmer information successfully updated!", "Congratulations", notificationType: NotificationType.success);
                    return RedirectToAction("GetFarmersByOfficers");
                }

                throw new Exception();
            }
            catch
            {
                return View();
            }
        }

        // GET: FarmerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FarmerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
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

        //Generate report of list of all devices registered by officer
        public async Task<ActionResult> RegisteredFarmersByOfficerReport() 
        {

            var user = await _userManager.GetUserAsync(User);
            bool result = _FarmerService.SaveFarmersDataIntoJsonFile(user.Id);
            if (result)
            {
                return View();
            }

            throw new Exception();
        }


    }
}
