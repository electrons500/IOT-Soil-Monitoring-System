using Frontend.Models.Data.ViewModel;
using Frontend.Models.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static Frontend.Models.Enum;

namespace Frontend.Controllers
{
    public class SoilCategoryController : BaseController
    {
        private readonly SoilCategoryService _SoilCategoryService;
        public SoilCategoryController(SoilCategoryService soilCategoryService)
        {
            _SoilCategoryService = soilCategoryService;
        }

       
        // GET: SoilCategoryController
        public ActionResult Index()
        {
          
            return View();
        }


        [HttpGet]
        public ActionResult SoilCategoryList()
        {
            var model = _SoilCategoryService.GetSoilCategories();
            return Json(new { data = model });
        }

        // GET: SoilCategoryController/Details/5
        public ActionResult SoilCategoryDetails(int id) 
        {
            var model = _SoilCategoryService.GetSoilCategoryDetails(id);
            return View(model);
        }

        // GET: SoilCategoryController/Create
        public ActionResult AddSoilCategory()
        {
            return View();
        }

        // POST: SoilCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSoilCategory(SoilCategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _SoilCategoryService.AddSoilCategory(model);
                    if (result)
                    {
                        Notify("New soil has been successfully added!", "Congratulations", notificationType: NotificationType.success);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Notify("New soil failed to be added!", "Congratulations", notificationType: NotificationType.error);
                    }


                }

                throw new Exception();
            }
            catch
            {
                return View();
            }
        }

        // GET: SoilCategoryController/Edit/5
        public ActionResult UpdateSoilCategory(int id)
        {
            var model = _SoilCategoryService.GetSoilCategoryDetails(id);
            return View(model);
        }

        // POST: SoilCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSoilCategory(SoilCategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _SoilCategoryService.UpdateSoilCategory(model);
                    if (result)
                    {
                        Notify("Soil name was successfully updated!", "Congratulations", notificationType: NotificationType.success);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Notify("Soil name failed to be updated!", "Congratulations", notificationType: NotificationType.error);
                    }


                }

                throw new Exception();
            }
            catch
            {
                return View();
            }
        }

       
        // POST: SoilCategoryController/Delete/5
        [HttpDelete]
        public ActionResult DeleteSoilCategory(int id)
        {
            try
            {
                bool result = _SoilCategoryService.RemoveSoilCategory(id);
                if (result)
                    return Json(new { success = true, message = "Soil category successfully deleted!" });

                throw new Exception();
            }
            catch
            {
                return View();
            }
        }
    }
}
