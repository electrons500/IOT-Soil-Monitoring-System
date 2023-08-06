using System.ComponentModel.DataAnnotations;

namespace Backendapi.Models.Data.ApiModel
{
    public class SoilDataApiModel
    {
        [Key]
        public int SoilDataId { get; set; }
        public string SoilMoisture { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string SoilTemperature { get; set; }
        public string Nitrogen { get; set; }
        public string Potassium { get; set; }
        public string Phosphorus { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SerialNumber { get; set; }
        public string ArduinoId { get; set; }


    }


}
