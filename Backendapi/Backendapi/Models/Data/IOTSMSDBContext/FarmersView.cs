using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Backendapi.Models.Data.IOTSMSDBContext
{
    public partial class FarmersView
    {
        public string FarmerId { get; set; }
        public string FarmerName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string GenderName { get; set; }
        public string RegionName { get; set; }
        public byte[] FarmerPhoto { get; set; }
    }
}
