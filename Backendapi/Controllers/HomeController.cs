using Backendapi.Models.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backendapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private AccountService _accountService;
        public HomeController(AccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: api/<HomeController>
        [HttpGet("InsertDefaultData")]
        public ActionResult InsertDefaultData()
        {
            _accountService.InsertGenderValuesIntoDB();
            _accountService.InsertRegionsIntoDB();
            _accountService.InsertSoilCategoryIntoDB();

            return StatusCode(StatusCodes.Status200OK, "Data inserted sucessfully");
        }

       
    }
}
