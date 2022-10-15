using Frontend.Models.Data.ViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Security;
using System.Text;

namespace Frontend.Models.Service
{
    public class SoilCategoryService
    {
        private IConfiguration _configuration;
        HttpClientHandler _clientHandler = new HttpClientHandler();

        public SoilCategoryService(IConfiguration configuration)
        {
            _configuration = configuration;
            _clientHandler.ServerCertificateCustomValidationCallback = (sender,cert,chain,slPolicyErrors) =>{ return true; };
            
        }

        //Get all soil category from soil category api
        public List<SoilCategoryViewModel> GetSoilCategories()
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;

            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            List<SoilCategoryViewModel> model = new List<SoilCategoryViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/SoilCategory/GetSoilCategories").Result;
            if (responseMessage.IsSuccessStatusCode)
            {             
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<SoilCategoryViewModel>>(ApiData);
                
            }

            return model;

        }

        //Add soil category
        public bool AddSoilCategory(SoilCategoryViewModel model)
        {

            SoilCategoryViewModel soilCategory = new SoilCategoryViewModel()
            {
                SoilCategoryId = 1,
                SoilName = model.SoilName

            };
           
            
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(soilCategory);
            StringContent content = new StringContent(SendingData,Encoding.UTF8,"application/json");
            HttpResponseMessage responseMessage = httpClient.PostAsync(httpClient.BaseAddress + "/SoilCategory/AddSoilCategory",content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //Soil Category Details
        public SoilCategoryViewModel GetSoilCategoryDetails(int soilcategoryId)
        {
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            SoilCategoryViewModel model = new SoilCategoryViewModel();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/SoilCategory/GetSoilCategoryDetails/" + soilcategoryId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<SoilCategoryViewModel>(ApiData);

            }

            return model;
        }


        //Edit Soil Category
        public bool UpdateSoilCategory(SoilCategoryViewModel model)
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
            HttpResponseMessage responseMessage = httpClient.PutAsync(httpClient.BaseAddress + "/SoilCategory/UpdateSoilCategory/"+ model.SoilCategoryId, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //Delete soil category
        public bool RemoveSoilCategory(int soilcategoryId)
         {

            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };
            //Convert data into json by serializing it with NewtonSoft json package         
            HttpResponseMessage responseMessage = httpClient.DeleteAsync(httpClient.BaseAddress + "/SoilCategory/RemoveSoilCategory/" + soilcategoryId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }



    }
}
