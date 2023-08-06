using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Frontend.Models.Enum;

namespace Frontend.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            InsertDefaultData();
           
            return View();
        }


        private void InsertDefaultData()
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Home/InsertDefaultData").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var msg = responseMessage.ReasonPhrase;

            }

        }

        public ActionResult BingMap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BingMap(string id)
        {
            return View();
        }


    }
}
