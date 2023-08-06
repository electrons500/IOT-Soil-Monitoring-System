using Frontend.Models.Data.ViewModel;
using Frontend.Models.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Models.Data.Service
{
    public class FarmerService
    {
        private IConfiguration _configuration;
        private RegionService _regionService;
        private SoilCategoryService _SoilCategoryService;
        private GenderService _GenderService;
        private readonly IHttpClientFactory _httpClientFactory;
        public FarmerService(SoilCategoryService soilCategoryService, RegionService regionService, IConfiguration configuration, GenderService genderService, IHttpClientFactory httpClientFactory)
        {
            _SoilCategoryService = soilCategoryService;
            _regionService = regionService;
            _configuration = configuration;
            _GenderService = genderService;
            _httpClientFactory = httpClientFactory;
        }

        //Get soil category and regions and populate 
        public FarmerViewModel CreateFarmers()
        {
            FarmerViewModel model = new FarmerViewModel()
            {
                RegionList = new SelectList(_regionService.GetRegions(), "RegionId", "RegionName"),

                GenderList = new SelectList(_GenderService.GetGenders(), "GenderId", "GenderName"),

            };

            return model;
        }


        public bool AddFarmer(FarmerViewModel model, string userId)
        {
            byte[] imageByte;
            string middleName = model.MiddleName;
            if (string.IsNullOrEmpty(middleName))
                middleName = "";

            using (var stream = new MemoryStream())
            {
                model.ImageFile.CopyTo(stream);
                imageByte = stream.ToArray();
            }

            var farmer = new FarmerSecondModel()
            {
                FarmerId = Guid.NewGuid().ToString(),
                Firstname = model.Firstname,
                MiddleName = middleName,
                LastName = model.LastName,
                FullName = "",
                Address = model.Address,
                Location = model.Location,
                Contact = model.Contact,
                GenderId = model.GenderId,
                GenderName = "",
                RegionId = model.RegionId,
                RegionName = "",
                FarmerPhoto = imageByte,
                FarmerImageId = 1,
                UserId = userId,
                Username = "",
                DateCreated = DateTime.Now.ToShortDateString(),


            };


            var httpClient = _httpClientFactory.CreateClient("backendapi");
            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(farmer);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PostAsync(httpClient.BaseAddress + "/Farmer/AddFarmer", content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var messageFromServer = responseMessage.ReasonPhrase;
                return true;
            }
            return false;

        }

        //List of farmers
        public List<FarmerViewModel> GetFarmers()
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<FarmerViewModel> model = new List<FarmerViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Farmer/GetFarmers").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<FarmerViewModel>>(ApiData);
            }

            List<FarmerViewModel> farmers = model.Select(x => new FarmerViewModel
            {
                FarmerId = x.FarmerId,
                Firstname = x.Firstname,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                FullName = x.FullName,
                Address = x.Address,
                Location = x.Location,
                Contact = x.Contact,
                GenderId = x.GenderId,
                GenderName = x.GenderName,
                RegionId = x.RegionId,
                RegionName = x.RegionName,
                FarmerImageId = x.FarmerImageId,
                FarmerPhoto = x.FarmerPhoto,
                Base64StringPic = Convert.ToBase64String(x.FarmerPhoto),
                UserId = x.UserId,
                Username = x.Username,
                DateCreated = x.DateCreated
            }).ToList();

            return farmers;
        }
         //List of farmers registered officers
        public List<FarmerViewModel> GetFarmersByOfficerRegistration(string userId) 
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<FarmerViewModel> model = new List<FarmerViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Farmer/GetFarmersByOfficerRegistration/" + userId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<FarmerViewModel>>(ApiData);
            }

            List<FarmerViewModel> farmers = model.Select(x => new FarmerViewModel
            {
                FarmerId = x.FarmerId,
                Firstname = x.Firstname,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                FullName = x.FullName,
                Address = x.Address,
                Location = x.Location,
                Contact = x.Contact,
                GenderId = x.GenderId,
                GenderName = x.GenderName,
                RegionId = x.RegionId,
                RegionName = x.RegionName,
                FarmerImageId = x.FarmerImageId,
                FarmerPhoto = x.FarmerPhoto,
                Base64StringPic = Convert.ToBase64String(x.FarmerPhoto),
                UserId = x.UserId,
                Username = x.Username,
                DateCreated = x.DateCreated
            }).ToList();

            return farmers;
        }


        //Get a farmer details
        public FarmerViewModel GetFarmerDetails(string farmerId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            FarmerViewModel model = new FarmerViewModel();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Farmer/GetFarmerDetails/" + farmerId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<FarmerViewModel>(ApiData);

            }

            var farmer = new FarmerViewModel()
            {
                FarmerId = model.FarmerId,
                Firstname = model.Firstname,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                FullName = model.FullName,
                Address = model.Address,
                Location = model.Location,
                Contact = model.Contact,
                GenderId = model.GenderId,
                GenderName = model.GenderName,
                RegionId = model.RegionId,
                RegionName = model.RegionName,
                FarmerImageId = model.FarmerImageId,
                FarmerPhoto = model.FarmerPhoto,
                Base64StringPic = Convert.ToBase64String(model.FarmerPhoto),
                UserId = model.UserId,
                Username = model.Username,
                DateCreated = model.DateCreated,
                RegionList = new SelectList(_regionService.GetRegions(), "RegionId", "RegionName"),
                GenderList = new SelectList(_GenderService.GetGenders(), "GenderId", "GenderName")
            };

            return farmer;
        }

        //update farmer
        public bool UpdateFarmerDetails(FarmerViewModel model)
        {
            byte[] imageByte;
            string middleName = model.MiddleName;
            if (string.IsNullOrEmpty(middleName))
                middleName = "";

           if(model.ImageFile != null)
            {
                using var stream = new MemoryStream();
                model.ImageFile.CopyTo(stream);
                imageByte = stream.ToArray();
            }
            else
            {
                imageByte = new byte[0];
            }

            var farmer = new FarmerSecondModel()
            {
                FarmerId = model.FarmerId,
                Firstname = model.Firstname,
                MiddleName = middleName,
                LastName = model.LastName,
                FullName = "",
                Address = model.Address,
                Location = model.Location,
                Contact = model.Contact,
                GenderId = model.GenderId,
                GenderName = "",
                RegionId = model.RegionId,
                RegionName = "",
                FarmerPhoto = imageByte,
                FarmerImageId = 1,
                UserId = "",
                Username = "",
                DateCreated = DateTime.Now.ToShortDateString(),


            };

            var httpClient = _httpClientFactory.CreateClient("backendapi");
            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(farmer);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PutAsync(httpClient.BaseAddress + "/Farmer/UpdateFarmerDetails/" + farmer.FarmerId, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }


        //Store the list of registered farmers in to a json file for generating of report
        
        public bool SaveFarmersDataIntoJsonFile(string userId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Farmer/GetFarmersByOfficerRegistration/" + userId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Save json data as file
                string filePath = @"C:\SMS\Json\RegisteredFarmersReport.json";
                string directory = @"C:\SMS\Json";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                else
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                bool result = WriteJsonDataToFile(filePath, ApiData);
                if (result)
                {
                    return true;
                }


            }

            return false;
        }

        public bool WriteJsonDataToFile(string filePath, string jsonData)
        {
            try
            {
                using (var JsonWriter = new StreamWriter(filePath, true))
                {
                    JsonWriter.WriteLine(jsonData.ToString());
                    JsonWriter.Close();
                }
            }
            catch (UnauthorizedAccessException)
            {
                FileAttributes attr = (new FileInfo(filePath)).Attributes;
                if ((attr & FileAttributes.ReadOnly) > 0)
                {
                    return false;
                }

            }


            return true;
        }


    }
}
