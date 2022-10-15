using Frontend.Models.Data;
using Frontend.Models.Data.IOTSMSDBContext;
using Frontend.Models.Data.Service;
using Frontend.Models.Data.ViewModel;
using Frontend.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using static Frontend.Models.Enum;

namespace Frontend.Controllers
{
    public class AccountsController : BaseController
    {
        private UserAccountService _userAccountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOTSMSDBContext _db;
    
        public AccountsController(UserAccountService userAccountService, UserManager<ApplicationUser> userManager, IOTSMSDBContext db)
        {
            _userAccountService = userAccountService;
            _userManager = userManager;
            _db = db;
           
        }
       
        // GET: AccountsController
        
        public ActionResult Index()
        {

            return View();
        }

        // GET: AccountsController/Create
        public ActionResult Login()
        {
            return View();
        }

        // POST: AccountsController/Login
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model,string ReturnUrl = null)
        {
            try
            {
                //ReturnUrl = ReturnUrl ?? Url.Content("~/" + ReturnUrl);
                
                if (ModelState.IsValid)
                {
                    var result = await _userAccountService.UserLoginAsync(model);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        {
                            return LocalRedirect(ReturnUrl);
                        }
                        else
                        {
                            MailAddress address = new MailAddress(model.EmailAdress);
                            var userName = address.User;
                            var GetUserId = await _userManager.FindByNameAsync(userName);

                            var UserHasRole = WhatRoleIsUser(GetUserId.Id);

                            if (UserHasRole == "Agric Extension Officer")
                            {
                                //go to student page
                                return RedirectToAction("AgricOfficers", "Dashboard");
                            }
                            else if (UserHasRole == "Non-Agric Extension Officer")
                            {
                                //go to Non-Agric Extension Officer page
                                return RedirectToAction("NonAgricOfficers", "Dashboard");
                            }
                            else
                            {
                                //go to admin page
                                return RedirectToAction("Admins", "Dashboard");
                            }
                        }

                        

                    }

                   
                }
                return View();

            }
            catch
            {
                return View();
            }

        }
         // GET: AccountsController/Create
        public ActionResult AdminLogin()
        {
            return View();
        }

        // POST: AccountsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdminLogin(LoginViewModel model)
        {
            try
            {

                var result = await _userAccountService.UserLoginAsync(model);
                if (result.Succeeded)
                {
                    MailAddress address = new MailAddress(model.EmailAdress);
                    var userName = address.User;
                    var GetUserId = await _userManager.FindByNameAsync(userName);

                    var UserHasRole = WhatRoleIsUser(GetUserId.Id);

                    if (UserHasRole == "Agric Extension Officer")
                    {
                        //go to student page
                        return RedirectToAction("AgricOfficers", "Dashboard");
                    }
                    else if (UserHasRole == "Non-Agric Extension Officer")
                    {
                        //go to Non-Agric Extension Officer page
                        return RedirectToAction("NonAgricOfficers", "Dashboard");
                    }
                    else
                    {
                        //go to admin page
                        return RedirectToAction("Admins", "Dashboard");
                    }

                }

                return View();

            }
            catch
            {
                return View();
            }
        }

        //Get logged in user's role
        public string WhatRoleIsUser(string userId)
        {
            var GetUserRoleId = _db.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
            var GetUserRole = _db.Roles.Where(x => x.Id == GetUserRoleId.RoleId).FirstOrDefault();


            return GetUserRole.Name;

        }
        public async Task<IActionResult> Logout()
        {
            await _userAccountService.LogOutAsync();
            return RedirectToAction("Index","Home");
        }

        public ActionResult AdminAccountRegistration()
        {
            var model = _userAccountService.CreateUsers();
            return View(model);
        }

        // POST: AccountsController/AdminAccountRegistration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminAccountRegistration(AccountRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = _userAccountService.AdminAccountRegistration(model);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            return View();
        }

        public ActionResult AgricExtAccountRegistration()
        {
            var model = _userAccountService.CreateUsers();
            return View(model);
        }

        // POST: AccountsController/AdminAccountRegistration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgricExtAccountRegistration(AccountRegistrationViewModel model)
        {
            bool result = _userAccountService.AgricExtOfficerAccountRegistration(model);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Administrators()
        { 

            return View();
        }

        [HttpGet]
        public ActionResult GetAdministratorsList()
        {
            var model = _userAccountService.GetUserRoles("Administrator");
            return Json(new { data = model });
        }
         public ActionResult Officers() 
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetOfficersList() 
        {
            var model = _userAccountService.GetUserRoles("Agric Extension Officer");
            return Json(new { data = model });
        }

        
        //User details
        public IActionResult GetUserDetails(string id)
        {
            var model = _userAccountService.GetUserDetails(id);
            return View(model);
        }

        public async Task<ActionResult> UpdateAdminDetails()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _userAccountService.LoadUserAccountDetailsAsync(user);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAdminDetails(AccountRegistrationViewModel model) 
        {
            var user = await _userManager.GetUserAsync(User);
            model.ProfilePic = user.ProfilePic; 
            bool result = _userAccountService.UpdateUserAccountAsync(model);
            if (result)
            {
                Notify("Account information successfully updated!", "Congratulations", notificationType: NotificationType.success);
            }
            else
            {
                Notify("Account information failed to update!", "Congratulations", notificationType: NotificationType.error); 
            }

            return RedirectToAction("UpdateAdminDetails");

        }



         public async Task<ActionResult> UpdateOfficerDetails()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _userAccountService.LoadUserAccountDetailsAsync(user);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<ActionResult> UpdateOfficerDetails(AccountRegistrationViewModel model) 
        {
            var user = await _userManager.GetUserAsync(User);
            model.ProfilePic = user.ProfilePic; 
            bool result = _userAccountService.UpdateUserAccountAsync(model);
            if (result)
            {
                Notify("Account information successfully updated!", "Congratulations", notificationType: NotificationType.success);
            }
            else
            {
                Notify("Account information failed to update!", "Congratulations", notificationType: NotificationType.error); 
            }

            return RedirectToAction("UpdateOfficerDetails");

        }


    }
}
