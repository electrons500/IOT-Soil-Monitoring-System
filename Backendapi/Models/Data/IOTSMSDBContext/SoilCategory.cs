using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Backendapi.Models.Data.IOTSMSDBContext
{
    public partial class SoilCategory
    {
        public SoilCategory()
        {
            Farm = new HashSet<Farm>();
        }

        public int SoilCategoryId { get; set; }
        public string SoilName { get; set; }

        public virtual ICollection<Farm> Farm { get; set; }
    }
}
