using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frontend.Models.Data.ViewModel
{
    public class ArduinoViewModel
    {
        [Key] 
        public string ArduinoId { get; set; } 

        [DisplayName("Serial number")]
        public string SerialNumber { get; set; }
        [DisplayName("Vid No.")]
        public string Vid { get; set; }
        [DisplayName("PID No.")]
        public string Pid { get; set; }
        [DisplayName("Brand name")]
        public string Bn { get; set; }
        [DisplayName("Deployment date")]
        public string DeploymentDate { get; set; }
        [DisplayName("Verified")]
        public bool IsVerified { get; set; }
        [DisplayName("Activated")]
        public bool IsActivated { get; set; }
        [DisplayName("Activation date")]
        public string DateOfActivation { get; set; }
        [DisplayName("Last power on date")]
        public string LastPowerOnDate { get; set; }
        [DisplayName("Last power on time")]
        public string LastPowerOnTime { get; set; }
        [DisplayName("Active")]
        public bool IsActive { get; set; }
        [DisplayName("On site")]
        public bool IsOnsite { get; set; }
        [DisplayName("Owner name")]
        public string UserId { get; set; }
        [NotMapped]
        [DisplayName("Owner name")]
        public string UserName { get; set; }
    }
}
