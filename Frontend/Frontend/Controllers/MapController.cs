using Frontend.Models.Data;
using Frontend.Models.Data.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class MapController : Controller
    {
        private FarmMapLocationService _FarmMapLocationService;
        private readonly UserManager<ApplicationUser> _userManager;
        public MapController(FarmMapLocationService farmMapLocationService, UserManager<ApplicationUser> userManager)
        {
            _FarmMapLocationService = farmMapLocationService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        //View farms on map
        public async Task<ActionResult> RegisteredFarmsbyOfficer()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _FarmMapLocationService.GetListOfRegisteredFarmAndTheirMapLocation(user.Id);
            return View(model);
        }


    }
}
