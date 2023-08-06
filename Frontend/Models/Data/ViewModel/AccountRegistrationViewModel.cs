using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models.Data.ViewModel
{
    public class AccountRegistrationViewModel
    {

        public string Id { get; set; } 
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public int GenderId { get; set; }
        [NotMapped]
        public SelectList GenderList { get; set; }
        [NotMapped]
        public string GenderName { get; set; } 

        public string? HomeTown { get; set; }

        public int RegionId { get; set; }
        [NotMapped]
        public SelectList RegionList { get; set; }
        [NotMapped]
        public string RegionName { get; set; }

        public string? Residence { get; set; }

        public string? Address { get; set; }

        public byte[]? ProfilePic { get; set; } 
        [NotMapped]
        public IFormFile ImageFormFile { get; set; }
        [Key]
        public string? ContactNumber { get; set; }

        public string? EmailAdress { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; } 
        [Compare("Password",ErrorMessage ="Password doesnnot match")]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        public DateTime? RegistrationDate { get; set; }
    }
}
