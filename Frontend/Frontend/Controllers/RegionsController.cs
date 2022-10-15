using Frontend.Models.Data.ViewModel;
using Frontend.Models.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Frontend.Models.Enum;

namespace Frontend.Controllers
{
    public class RegionsController : BaseController
    {
        private RegionService _RegionService;
        public RegionsController(RegionService regionService)
        {
            _RegionService = regionService;
        }

        // GET: RegionsController
        public ActionResult Index()
        {
            return View();
        }
         public ActionResult RegionsList()
        {
            var model = _RegionService.GetRegions();
            return Json(new {data = model });
        }

        // GET: RegionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegionsController/Create
        public ActionResult AddRegions() 
        {
            return View();
        }

        // POST: RegionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRegions(RegionViewModel model)
        {
            try
            {

                bool result = _RegionService.AddRegion(model);
                if (result)
                {
                    Notify("New region has been successfully added!", "Congratulations", notificationType: NotificationType.success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Notify("New region failed to be added!", "Congratulations", notificationType: NotificationType.error);
                }
                throw new Exception();
            }
            catch
            {
                return View();
            }
        }

        // GET: RegionsController/Edit/5
        public ActionResult EditRegion(int id)
        {
            var model = _RegionService.GetRegionDetails(id);
            return View(model);
        }

        // POST: RegionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRegion(int id, RegionViewModel model)
        {
            try
            {

                bool result = _RegionService.EditRegion(model);
                if (result)
                {
                    Notify("New region has been successfully updated!", "Congratulations", notificationType: NotificationType.success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Notify("New region failed to be updated!", "Congratulations", notificationType: NotificationType.error);
                }
                throw new Exception();
            }
            catch
            {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult DeleteRegion(int id)
        {
            try
            {
                bool result = _RegionService.DeleteRegion(id);
                if (result)
                    return Json(new { success = true, message = "Region successfully deleted!" });

                throw new Exception();
            }
            catch
            {
                return View();
            }
        }
    }
}
