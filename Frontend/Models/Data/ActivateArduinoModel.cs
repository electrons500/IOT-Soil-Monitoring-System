using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models.Data
{
    public class ActivateArduinoModel
    {
        public string ArduinoId { get; set; }
        public string SerialNumber { get; set; }
        public string Vid { get; set; }
        public string Pid { get; set; }
        public string Bn { get; set; }
        public string DeploymentDate { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActivated { get; set; }
        public string? DateOfActivation { get; set; }
        public string? LastPowerOnDate { get; set; }
        public string? LastPowerOnTime { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOnsite { get; set; }
        public string UserId { get; set; }
    }
}
