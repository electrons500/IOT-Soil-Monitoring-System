using Frontend.Models.Data;
using Frontend.Models.Data.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Frontend.Models.Service
{
    public class FarmService
    {
        private IConfiguration _configuration;
        private RegionService _regionService;
        private SoilCategoryService _SoilCategoryService;

        public FarmService(IConfiguration configuration, RegionService regionService, SoilCategoryService soilCategoryService)
        {
            _configuration = configuration;
            _regionService = regionService;
            _SoilCategoryService = soilCategoryService;
        }

        //Get list of farms from farm api
        public List<FarmViewModel> GetFarms()
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            List<FarmViewModel> model = new List<FarmViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Farm/GetFarms").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<FarmViewModel>>(ApiData);

            }

            return model;

        }
         //Get list of farms belonging to a farmer
        public List<FarmViewModel> GetFarmsbyFarmerId(string farmerId)
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            List<FarmViewModel> model = new List<FarmViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Farm/GetFarmsbyFarmerId/" + farmerId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<FarmViewModel>>(ApiData);

            }

            return model;

        }

       //Regions
        public FarmViewModel CreateFarms()
        {
            FarmViewModel model = new FarmViewModel()
            {
                RegionList = new SelectList(_regionService.GetRegions(), "RegionId", "RegionName"),
                SoilCategoryList =new SelectList(_SoilCategoryService.GetSoilCategories(), "SoilCategoryId", "SoilName")
            };

            return model;
        }

        //Get farm details
        public FarmViewModel GetFarmDetails(string farmId)
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            FarmViewModel model = new FarmViewModel();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Farm/GetFarmDetails/" + farmId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<FarmViewModel>(ApiData);
                

            }

            return model;
        }
         //Get farm details
        public FarmViewModel GetFarmDetailsByFarmerId(string farmerId) 
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            FarmViewModel model = new FarmViewModel();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Farm/GetFarmDetailsByFarmerId/" + farmerId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<FarmViewModel>(ApiData);
            }

            return model;
        }

        //Add new farm details
        public bool AddFarm(FarmViewModel model)
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
            HttpResponseMessage responseMessage = httpClient.PostAsync(httpClient.BaseAddress + "/Farm/AddFarm", content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                bool sendFarmCoordinates = SendFarmGPSCoordinates(model);
                if ( responseMessage.IsSuccessStatusCode && sendFarmCoordinates)
                {
                    return true;
                }
                
               
            }
            return false;
        }

        public bool SendFarmGPSCoordinates(FarmViewModel model) 
        {
            FarmMapLocationModel GPScoordinates = new FarmMapLocationModel()
            {
                SerialNumber = model.SerialNumber,
                Latitude = model.Latitude.ToString(),
                Longitude = model.Longitude.ToString()
            };
            
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(GPScoordinates);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PostAsync(httpClient.BaseAddress + "/FarmMapLocation/AddMapLocation", content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }





        //Update Farm details
        public bool UpdateFarm(FarmViewModel model)
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
            HttpResponseMessage responseMessage = httpClient.PutAsync(httpClient.BaseAddress + "/Farm/UpdateFarmDetails/" + model.FarmId, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                
                    return true;
                
            }
            return false;
        }

        //Delete Farm details
        public bool RemoveFarm(string farmId)
        {

            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };
            //Convert data into json by serializing it with NewtonSoft json package         
            HttpResponseMessage responseMessage = httpClient.DeleteAsync(httpClient.BaseAddress + "/Farm/DeleteFarmDetails/" + farmId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }


        //Get all farms registered by officer
        public List<FarmViewModel> GetFarmsRegisteredByOfficer(string OfficerId) 
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            List<FarmViewModel> model = new List<FarmViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Farm/GetFarmsRegisteredByOfficer/" + OfficerId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<FarmViewModel>>(ApiData);

            }

            return model;

        }


    }
}
