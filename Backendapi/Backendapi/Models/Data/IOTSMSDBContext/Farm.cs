using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Backendapi.Models.Data.IOTSMSDBContext
{
    public partial class Farm
    {
        public Farm()
        {
            FarmMapLocation = new HashSet<FarmMapLocation>();
        }

        public string FarmId { get; set; }
        public string FarmName { get; set; }
        public string Location { get; set; }
        public string DateCreated { get; set; }
        public string FarmerId { get; set; }
        public int RegionId { get; set; }
        public int SoilCategoryId { get; set; }
        public string ArduinoId { get; set; }
        public string SerialNumber { get; set; }

        public virtual Arduino Arduino { get; set; }
        public virtual Farmer Farmer { get; set; }
        public virtual Region Region { get; set; }
        public virtual SoilCategory SoilCategory { get; set; }
        public virtual ICollection<FarmMapLocation> FarmMapLocation { get; set; }
    }
}
