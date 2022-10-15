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
        public SettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get number of officer from api

        public string GetNumberOfExtensionOfficers()
        {
            string OfficerCount = "";
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

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
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

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
            string baseUrl = _configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

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
            Uri baseadress = new Uri(baseUrl);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = baseadress
            };

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Settings/GetNumberOfFarms").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                deviceCount = responseMessage.Content.ReadAsStringAsync().Result;

            }

            return deviceCount;

        }




    }
}
