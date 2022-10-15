using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backendapi.Models.Data
{
    public class RegisteredDevicesModel
    {
        [Key]
        public string ArduinoId { get; set; } 
        public string SerialNumber { get; set; }
        public string Vid { get; set; } 
        public string Pid { get; set; } 
        public string Bn { get; set; } 
        public string UserId { get; set; }
        public string OfficerName { get; set; } 
    }
}
