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
        private readonly IHttpClientFactory _httpClientFactory;
        public FarmMapLocationService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        //Get list of Farms registered by Officer and their map location
        public List<FarmMapLocationViewModel> GetListOfRegisteredFarmAndTheirMapLocation(string OfficerId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");


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
            var httpClient = _httpClientFactory.CreateClient("backendapi");


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

            var httpClient = _httpClientFactory.CreateClient("backendapi");


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
