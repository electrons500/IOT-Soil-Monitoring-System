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
        private readonly IHttpClientFactory _httpClientFactory;
        public SoilCategoryService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        //Get all soil category from soil category api
        public List<SoilCategoryViewModel> GetSoilCategories()
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

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


            var httpClient = _httpClientFactory.CreateClient("backendapi");
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
            var httpClient = _httpClientFactory.CreateClient("backendapi");
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

            var httpClient = _httpClientFactory.CreateClient("backendapi");

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

            var httpClient = _httpClientFactory.CreateClient("backendapi");
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
