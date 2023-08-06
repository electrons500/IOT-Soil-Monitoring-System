using System;
using System.ComponentModel.DataAnnotations;

namespace Backendapi.Models.Data.ApiModel
{
    public class AccountRegisterApiModel
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
      
        public string MiddleName { get; set; }
       
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
       
        public int GenderId { get; set; }
      
        public string HomeTown { get; set; }
       
        public int RegionId { get; set; }
       
        public string Residence { get; set; }
       
        public string Address { get; set; }
       
        public byte[] ProfilePic { get; set; } 
      
        public string ContactNumber { get; set; }
       
        public string EmailAdress { get; set; } 
       
        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
