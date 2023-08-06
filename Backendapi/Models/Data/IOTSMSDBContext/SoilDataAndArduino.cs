using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Backendapi.Models.Data.IOTSMSDBContext
{
    public partial class SoilDataAndArduino
    {
        public int SoilDataId { get; set; }
        public string SoilMoisture { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string Nitrogen { get; set; }
        public string Potassium { get; set; }
        public string Phosphorus { get; set; }
        public string SerialNumber { get; set; }
        public string SentDateAndTime { get; set; }
    }
}
