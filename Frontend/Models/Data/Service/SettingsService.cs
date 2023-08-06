using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Models.Data.Service
{
    public class SettingsService
    {
        private IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public SettingsService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        //Get number of officer from api

        public string GetNumberOfExtensionOfficers()
        {
            string OfficerCount = "";
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Settings/GetNumberOfExtensionOfficers").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                OfficerCount = responseMessage.Content.ReadAsStringAsync().Result;

            }

            return OfficerCount;

        }

        public string GetNumberOfRegisteredDevices()
        {
            string deviceCount = "";
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Settings/GetNumberOfRegisteredDevices").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                deviceCount = responseMessage.Content.ReadAsStringAsync().Result;

            }

            return deviceCount;

        }

        public string GetNumberOfFarmers()
        {
            string deviceCount = "";
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Settings/GetNumberOfFarmers").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                deviceCount = responseMessage.Content.ReadAsStringAsync().Result;

            }

            return deviceCount;

        }

        public string GetNumberOfFarms()
        {
            string deviceCount = "";
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Settings/GetNumberOfFarms").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                deviceCount = responseMessage.Content.ReadAsStringAsync().Result;

            }

            return deviceCount;

        }




    }
}
