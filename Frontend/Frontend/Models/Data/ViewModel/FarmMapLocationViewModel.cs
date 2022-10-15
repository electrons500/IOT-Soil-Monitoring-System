using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models.Data.ViewModel
{
    public class FarmMapLocationViewModel
    {
        [Key]
        public string MaplocationId { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FarmId { get; set; }
        public string FarmName { get; set; }
        public string FarmLocation { get; set; }
        public string ArduinoId { get; set; }
        public string SerialNumber { get; set; }
    }
}
