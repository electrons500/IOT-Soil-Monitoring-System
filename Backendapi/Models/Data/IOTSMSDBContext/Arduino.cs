using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Backendapi.Models.Data.IOTSMSDBContext
{
    public partial class Arduino
    {
        public Arduino()
        {
            Farm = new HashSet<Farm>();
            FarmMapLocation = new HashSet<FarmMapLocation>();
            SoilData = new HashSet<SoilData>();
        }

        public string ArduinoId { get; set; }
        public string SerialNumber { get; set; }
        public string Vid { get; set; }
        public string Pid { get; set; }
        public string Bn { get; set; }
        public string DeploymentDate { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActivated { get; set; }
        public string DateOfActivation { get; set; }
        public string LastPowerOnDate { get; set; }
        public string LastPowerOnTime { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOnsite { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Farm> Farm { get; set; }
        public virtual ICollection<FarmMapLocation> FarmMapLocation { get; set; }
        public virtual ICollection<SoilData> SoilData { get; set; }
    }
}
