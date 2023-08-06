using Backendapi.Models.Data;
using Backendapi.Models.Data.ApiModel;
using Backendapi.Models.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Backendapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private SMSService _SMSService;
        public AccountController(AccountService accountService, UserManager<ApplicationUser> userManager, SMSService sMSService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _SMSService = sMSService;

        }



        [HttpPost("AdminAccountRegistration")]
        public async Task<IActionResult> AdminAccountRegistration([FromBody] AccountRegisterApiModel model)
        {
            string adminPhoneNumber = model.ContactNumber;

            //generate user roles
           await _accountService.GenerateRolesAsync();
            
            int id = 3;
            var result = await _accountService.RegisterUserAsync(model, id);
            if (result.Succeeded)
            {
                
                string message = "Your account has been successfully created.";
                bool msgResult = _SMSService.SendSMS(message, adminPhoneNumber); //send message to admin
                return Ok(result.Succeeded);

            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error Occured while server was processing");
        }

        
        [HttpPost("AgricOfficerAccountRegistration")]
        public async Task<IActionResult> AgricOfficerAccountRegistration([FromBody] AccountRegisterApiModel model)
        {
            _accountService.InsertGenderValuesIntoDB();
            _accountService.InsertRegionsIntoDB();
            await _accountService.GenerateRolesAsync();

            int id = 1;
            string officerNumaber = model.ContactNumber;
            var result = await _accountService.RegisterUserAsync(model, id);
            if (result.Succeeded)
            {
                string message = "Your account has been successfully created.";
                bool msgResult = _SMSService.SendSMS(message, officerNumaber); //send message to officer
                return Ok(result.Succeeded);
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error Occured while server was processing");
        }

        [HttpPost("NonAgricOfficerAccountRegistration")]
        public async Task<IActionResult> NonAgricOfficerAccountRegistration([FromBody] AccountRegisterApiModel model)
        {
            _accountService.InsertGenderValuesIntoDB();
            _accountService.InsertRegionsIntoDB();
            await _accountService.GenerateRolesAsync();

            int id = 2;
            var result = await _accountService.RegisterUserAsync(model, id);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Error Occured while server was processing");
        }

       [HttpGet("GetUsersByRole/{rolename}")]
        public ActionResult GetUsersByRole(string rolename)
        {
            var userroles = _accountService.GetUserRoles(rolename);
            return Ok(userroles);
        }

        [HttpGet("GetUsersDetails/{UserId}")]
        public ActionResult GetUsersDetails(string UserId)
        {
            var users = _accountService.GetUsersDetails(UserId);
            if (users == null)
                return NotFound();
            return Ok(users);
        }

        [HttpPut("UpdateUserAccount/{userId}")]
        public ActionResult UpdateUserAccount([FromBody] UserAccountDetailsApiModel model,string userId)
        {
            bool result = _accountService.UpdateUserAccountAsync(model,userId);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest("Something happened while server was processing.");
        }



    }
}
