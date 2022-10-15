using Frontend.Models.Data.ViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Models.Data.Service
{
    public class GenderService
    {
        private IConfiguration _configuration;
        public GenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get list of gender from region api
        public List<GenderViewModel> GetGenders() 
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            List<GenderViewModel> model = new List<GenderViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Gender/GetUserGender").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<GenderViewModel>>(ApiData);

            }

            return model;

        }
    }
}
