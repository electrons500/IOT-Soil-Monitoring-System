using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Backendapi.Models.Data.IOTSMSDBContext
{
    public partial class Region
    {
        public Region()
        {
            Farm = new HashSet<Farm>();
            Farmer = new HashSet<Farmer>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<Farm> Farm { get; set; }
        public virtual ICollection<Farmer> Farmer { get; set; }
    }
}
