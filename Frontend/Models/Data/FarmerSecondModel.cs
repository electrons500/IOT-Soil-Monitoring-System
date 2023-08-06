using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models.Data
{
    public class FarmerSecondModel
    {
        public string? FarmerId { get; set; }
        public string Firstname { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? FullName { get; set; }
        public string Address { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Contact { get; set; } = null!;

        public string? DateCreated { get; set; }
        public int RegionId { get; set; }

        public string? RegionName { get; set; }
        public int GenderId { get; set; }

        public string? GenderName { get; set; }
        public int? FarmerImageId { get; set; }

        public byte[]? FarmerPhoto { get; set; }

        public string? UserId { get; set; }
        public string? Username { get; set; }

       

    }
}
