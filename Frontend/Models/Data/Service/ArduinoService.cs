using Frontend.Models.Data;
using Frontend.Models.Data.ViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Frontend.Models.Service
{
    public class ArduinoService
    {
        private IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public ArduinoService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }


        #region Admins sections
        //Get all Devices deployed by admins
        public List<ArduinoViewModel> GetArduinos()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("backendapi");

                List<ArduinoViewModel> model = new List<ArduinoViewModel>();

                HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetArduinoList").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                    //Deserialize the json data into the view Model List by using Newtonsoft json package
                    model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);


                }
                return model;
            }
            catch (JsonSerializationException)
            {

                return null;
            }
        }

        //Register new devices for deployment
        public bool AddArduino(ArduinoViewModel arduino, string userId)
        {

            ArduinoViewModel model = new ArduinoViewModel()
            {
                ArduinoId = " ",
                SerialNumber = arduino.SerialNumber,
                Vid = arduino.Vid,
                Pid = arduino.Pid,
                Bn = arduino.Bn,
                DeploymentDate = DateTime.Now.ToShortDateString(),
                IsVerified = false,
                IsActivated = false,
                DateOfActivation = "1/1/0001",
                LastPowerOnDate = DateTime.Now.ToShortDateString(),
                LastPowerOnTime = DateTime.Now.ToShortTimeString(),
                IsActive = false,
                IsOnsite = false,
                UserId = userId
            };


            var httpClient = _httpClientFactory.CreateClient("backendapi");

            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PostAsync(httpClient.BaseAddress + "/Arduino/AddNewArduino", content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //update device information
        public bool UpdateArduino(ArduinoViewModel arduino)
        {
            ArduinoViewModel model = new ArduinoViewModel()
            {
                ArduinoId = arduino.ArduinoId,
                SerialNumber = arduino.SerialNumber,
                Vid = arduino.Vid,
                Pid = arduino.Pid,
                Bn = arduino.Bn,
                DeploymentDate = arduino.DeploymentDate,
                IsVerified = arduino.IsVerified,
                IsActivated = false,
                DateOfActivation = arduino.DateOfActivation,
                LastPowerOnDate = arduino.LastPowerOnDate,
                LastPowerOnTime = arduino.LastPowerOnTime,
                IsActive = false,
                IsOnsite = false,
                UserId = arduino.UserId,
                UserName = ""
            };


            var httpClient = _httpClientFactory.CreateClient("backendapi");

            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PutAsync(httpClient.BaseAddress + "/Arduino/UpdateArduino/" + model.ArduinoId, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        // view device information
        public ArduinoViewModel GetArduinoDetails(string arduinoId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            ArduinoViewModel model = new ArduinoViewModel();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetArduinoDetails/" + arduinoId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<ArduinoViewModel>(ApiData);

            }

            return model;
        }

        //delete devices by Id
        public bool DeleteArduino(string arduinoId)
        {

            var httpClient = _httpClientFactory.CreateClient("backendapi");

            HttpResponseMessage responseMessage = httpClient.DeleteAsync(httpClient.BaseAddress + "/Arduino/DeleteArduino/" + arduinoId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


        //Get list of unactivated device
        public List<ArduinoViewModel> GetUnActivateArduino()
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            List<ArduinoViewModel> model = new List<ArduinoViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetUnActivateArduino").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;
        }

        //Get list of activated device
        public List<ArduinoViewModel> GetActivatedArduino()
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            List<ArduinoViewModel> model = new List<ArduinoViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetActivateArduino").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;
        }

        //Get list of activated device and not onsite
        public List<ArduinoViewModel> GetActivatedDevicesAndNotOnsite()
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<ArduinoViewModel> model = new List<ArduinoViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetActivatedDevicesAndNotOnsite").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;
        }

        //Get list of activated device and are onsite
        public List<ArduinoViewModel> GetActivatedDevicesButAreOnsite()
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            List<ArduinoViewModel> model = new List<ArduinoViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetActivatedDevicesButAreOnsite").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;
        }

        //Get list of activated device and are onsite and active
        public List<ArduinoViewModel> GetActivatedDevicesThatAreOnsiteAndActive()
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            List<ArduinoViewModel> model = new List<ArduinoViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetActivatedDevicesThatAreOnsiteAndActive").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;
        }

        //Get list of activated device and are onsite and not active
        public List<ArduinoViewModel> GetActivatedDevicesThatAreOnsiteAndUnactive()
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            List<ArduinoViewModel> model = new List<ArduinoViewModel>();

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetActivatedDevicesThatAreOnsiteAndUnactive").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;
        }



        #endregion


        #region Agric Extension Officer
        public List<ArduinoViewModel> GetArduinoDevicesRegisteredByAgricExtOfficer(string Id)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<ArduinoViewModel> model = new List<ArduinoViewModel>();


            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetArduinoDevicesByAgricExtensionOfficer/" + Id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;


        }


        public bool ActivateDevice(ArduinoViewModel arduino)
        {
            ActivateArduinoModel model = new ActivateArduinoModel()
            {
                ArduinoId = "",
                SerialNumber = arduino.SerialNumber,
                Vid = "",
                Pid = "",
                Bn = "",
                DeploymentDate = DateTime.Now.ToShortDateString(),
                IsVerified = false,
                IsActivated = true,
                DateOfActivation = DateTime.Now.ToShortDateString(),
                LastPowerOnDate = DateTime.Now.ToShortDateString(),
                LastPowerOnTime = DateTime.Now.ToShortTimeString(),
                IsActive = false,
                IsOnsite = false,
                UserId = arduino.UserId
                
            };

            //Check if device is verified or not
            //if false then is not verified
            bool verifydevice = CheckDeviceVerified(arduino.SerialNumber);
            if (verifydevice == false)
            {
                return false;
            }

            var httpClient = _httpClientFactory.CreateClient("backendapi");

            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PutAsync(httpClient.BaseAddress + "/Arduino/ActivateArduino/" + model.SerialNumber, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public List<ArduinoViewModel> GetActivatedDevicesRegisterdByAgricExtensionOfficer(string Id)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<ArduinoViewModel> model = new List<ArduinoViewModel>();


            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetActivatedDevicesRegisterdByAgricExtensionOfficer/" + Id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;


        }

        public List<ArduinoViewModel> GetUnActivatedDevicesRegisterdByAgricExtensionOfficer(string Id)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<ArduinoViewModel> model = new List<ArduinoViewModel>();


            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetUnActivatedDevicesRegisterdByAgricExtensionOfficer/" + Id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;


        }

        public List<ArduinoViewModel> GetActivatedDevicesRegisterdByAgricExtensionOfficerAndNotOnsite(string Id)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            List<ArduinoViewModel> model = new List<ArduinoViewModel>();


            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetActivatedDevicesRegisterdByAgricExtensionOfficerAndNotOnsite/" + Id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;


        }

        public List<ArduinoViewModel> GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsite(string Id)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<ArduinoViewModel> model = new List<ArduinoViewModel>();


            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetActivatedDevicesRegisterdByAgricExtensionOfficerButAreOnsite/" + Id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;


        }

        public List<ArduinoViewModel> GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndActive(string Id)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<ArduinoViewModel> model = new List<ArduinoViewModel>();


            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndActive/" + Id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;


        }

         public List<ArduinoViewModel> GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndUnactive(string Id)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            List<ArduinoViewModel> model = new List<ArduinoViewModel>();


            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndUnactive/" + Id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ResponseMsg = responseMessage.ReasonPhrase; 

                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Deserialize the json data into the view Model List by using Newtonsoft json package
                model = JsonConvert.DeserializeObject<List<ArduinoViewModel>>(ApiData);

            }
            return model;


        }


        public bool VerifyDevice(VerifyArduinoViewModel model)
        {

            var httpClient = _httpClientFactory.CreateClient("backendapi");

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/VerifyArduino/" + model.SerialNumber).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

         public bool CheckDeviceVerified(string serialnumber) 
        {

            var httpClient = _httpClientFactory.CreateClient("backendapi");

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/IsDeviceVerified/" + serialnumber).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }




        //Get all registered device by Officer and store it as json file in drive C:
        public bool SaveRegisteredDevicesByOfficerAsJsonFile(string userId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            HttpResponseMessage responseMessage = httpClient.GetAsync(httpClient.BaseAddress + "/Arduino/GetDevicesRegisteredByOfficer/" + userId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string ApiData = responseMessage.Content.ReadAsStringAsync().Result;
                //Save json data as file
                string filePath = @"C:\SMS\Json\DeviceReport.json";
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



        #endregion

        public bool WriteJsonDataToFile(string filePath,string jsonData)
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
                if((attr & FileAttributes.ReadOnly) > 0)
                {
                    return false;
                }

            }
            

            return true;
        }


     public bool SetArduinoToActive(string arduinoId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            ArduinoViewModel model = new ArduinoViewModel();
            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PutAsync(httpClient.BaseAddress + "/Arduino/SetArduinoToActive/" + arduinoId, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool SetArduinoToInactive(string arduinoId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            ArduinoViewModel model = new ArduinoViewModel();
            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PutAsync(httpClient.BaseAddress + "/Arduino/SetArduinoToInactive/" + arduinoId, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

         public bool SetArduinoIfOnSite(string arduinoId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");

            ArduinoViewModel model = new ArduinoViewModel();
            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PutAsync(httpClient.BaseAddress + "/Arduino/SetArduinoIfOnSite/" + arduinoId, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
         
         public bool ChangeArduinoToOffsite(string arduinoId)
        {
            var httpClient = _httpClientFactory.CreateClient("backendapi");
            ArduinoViewModel model = new ArduinoViewModel();
            //Convert data into json by serializing it with NewtonSoft json package
            string SendingData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(SendingData, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = httpClient.PutAsync(httpClient.BaseAddress + "/Arduino/ChangeArduinoToOffsite/" + arduinoId, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }





    }
}
