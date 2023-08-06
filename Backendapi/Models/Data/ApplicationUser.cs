using Backendapi.Models.Data.IOTSMSDBContext;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backendapi.Models.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; } 
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public string HomeTown { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public string Residence { get; set; }
        public string Address { get; set; }
        public byte[] ProfilePic { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
