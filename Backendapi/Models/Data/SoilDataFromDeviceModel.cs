using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backendapi.Models.Data
{
    public class SoilDataFromDeviceModel
    {
        public string SoilMoisture { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string SoilTemperature { get; set; }
        public string Nitrogen { get; set; }
        public string Potassium { get; set; }
        public string Phosphorus { get; set; }
        public string SerialNumber { get; set; }
      
    }
}
