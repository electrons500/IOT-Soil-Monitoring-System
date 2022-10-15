using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Backendapi.Models.Data.IOTSMSDBContext
{
    public partial class FarmMapLocation
    {
        public int Id { get; set; }
        public string MaplocationId { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FarmId { get; set; }
        public string ArduinoId { get; set; }
        public string SerialNumber { get; set; }

        public virtual Arduino Arduino { get; set; }
        public virtual Farm Farm { get; set; }
    }
}
