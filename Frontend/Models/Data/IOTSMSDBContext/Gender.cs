using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Frontend.Models.Data.IOTSMSDBContext
{
    public partial class Gender
    {
        public Gender()
        {
            Farmer = new HashSet<Farmer>();
        }

        public int GenderId { get; set; }
        public string GenderName { get; set; }

        public virtual ICollection<Farmer> Farmer { get; set; }
    }
}
