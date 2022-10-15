using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backendapi.Models.Data.ApiModel
{
    public class UsersApiModel
    {
        [Key]
        public string UserId { get; set; } 
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; } 
        public string HomeTown { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; } 
        public string Residence { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte[] ProfilePic { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
