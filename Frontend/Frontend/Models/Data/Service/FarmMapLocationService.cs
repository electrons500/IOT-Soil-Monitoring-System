using Frontend.Models.Data.ViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Models.Data.Service
{
    public class FarmMapLocationService
    {
        private IConfiguration _configuration;
        public FarmMapLocationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get list of Farms registered by Officer and their map location
        public List<FarmMapLocationViewModel> GetListOfRegisteredFarmAndTheirMapLocation(string OfficerId)
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            List<FarmMapLocationViewModel> model = new List<FarmMapLocationViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/FarmMapLocation/GetRegisteredFarmsMapLocation/" + OfficerId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<FarmMapLocationViewModel>>(ApiData);

            }

            return model;

        }

        //Get farm location details
        public FarmMapLocationViewModel GetFarmMapDetails(string mapId)
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            FarmMapLocationViewModel model = new  FarmMapLocationViewModel();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/FarmMapLocation/GetRegisteredFarmsMapLocationDetails/" + mapId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<FarmMapLocationViewModel>(ApiData);

            }

            return model;
        }

        public bool UpdateFarmMapLocation(FarmMapLocationModel model) 
        {

            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PostAsync(httpClient.BaseAddress + "/FarmMapLocation/UpdateFarmMapLocation", content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }



    }
}
