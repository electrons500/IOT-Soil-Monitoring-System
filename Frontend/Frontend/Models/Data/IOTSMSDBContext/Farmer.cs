using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Frontend.Models.Data.IOTSMSDBContext
{
    public partial class Farmer
    {
        public Farmer()
        {
            Farm = new HashSet<Farm>();
        }

        public string FarmerId { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string DateCreated { get; set; }
        public int RegionId { get; set; }
        public int GenderId { get; set; }
        public int FarmerImageId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual FarmerImage FarmerImage { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Farm> Farm { get; set; }
    }
}
