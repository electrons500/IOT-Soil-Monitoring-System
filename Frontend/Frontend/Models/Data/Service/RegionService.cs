using Frontend.Models.Data.ViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Frontend.Models.Service
{
    public class RegionService
    {
        private IConfiguration _configuration;
        public RegionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get list of regions from region api
        public List<RegionViewModel> GetRegions()
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            List<RegionViewModel> model = new List<RegionViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Regions/GetRegions").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<RegionViewModel>>(ApiData);

            }

            return model;

        }


        public RegionViewModel GetRegionDetails(int RegionId) 
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            RegionViewModel model = new RegionViewModel();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Regions/GetRegionsDetails/" + RegionId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<RegionViewModel>(ApiData);

            }

            return model;
        }


        public bool AddRegion(RegionViewModel model)
        {

            RegionViewModel region = new RegionViewModel()
            {
                RegionId = 1,
                RegionName = model.RegionName

            };


            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(region);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PostAsync(httpClient.BaseAddress + "/Regions/AddRegions", content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

         public bool EditRegion(RegionViewModel model)
        {
             
            RegionViewModel region = new RegionViewModel()
            {
                RegionId =model.RegionId,
                RegionName = model.RegionName

            };


            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(region);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PutAsync(httpClient.BaseAddress + "/Regions/UpdateRegions/" + model.RegionId, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

         public bool DeleteRegion(int RegionId) 
        {
             
            

            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

          
            HttpResponseMessage responseMessage = httpClient.DeleteAsync(httpClient.BaseAddress + "/Regions/DeleteRegions/" + RegionId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


        


    }
}
