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
    public class SoilDataService
    {
        private IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public SoilDataService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        //Get list of Soil data from api
        public List<SoilDataViewModel> GetSoilData()
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<SoilDataViewModel> model = new List<SoilDataViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/SoilData/GetSoilDatasFromAllDevices").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<SoilDataViewModel>>(ApiData);

            }

            return model;

        }
        //Get list of Soil data by device number from api
        public List<SoilDataViewModel> GetSoilDataByDeviceId(string farmId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<SoilDataViewModel> model = new List<SoilDataViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/SoilData/GetSoilDatasbyDeviceId/" + farmId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<SoilDataViewModel>>(ApiData);

            }

            return model;

        }

        public SoilDataViewModel GetSoilDataDetails(int soilId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            SoilDataViewModel model = new SoilDataViewModel();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/SoilData/GetSoilDataDetails/" + soilId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<SoilDataViewModel>(ApiData);

            }

            double humdity = Convert.ToDouble(model.Humidity);
            double temperature = Convert.ToDouble(model.Temperature);
            double soiltemperature = Convert.ToDouble(model.SoilTemperature);

            SoilDataViewModel soilData = new SoilDataViewModel()
            {
                SoilDataId = model.SoilDataId,
                SoilMoisture = model.SoilMoisture,
                Temperature = (Convert.ToInt32(temperature)).ToString(),
                Humidity = humdity.ToString(),
                SoilTemperature = (Convert.ToInt32(soiltemperature)).ToString(),
                Nitrogen = model.Nitrogen,
                Potassium = model.Potassium,
                Phosphorus = model.Phosphorus,
                Date = model.Date,
                Time = model.Time

            };

            return soilData;
        }


        //Delete Farm details
        public bool RemoveSoilData(int soilId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            //Convert data into json by serializing it with NewtonSoft json package         
            HttpResponseMessage responseMessage = httpClient.DeleteAsync(httpClient.BaseAddress + "/SoilData/DeleteSoilData/" + soilId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true; 
            }
            return false;

        }

    }
}
