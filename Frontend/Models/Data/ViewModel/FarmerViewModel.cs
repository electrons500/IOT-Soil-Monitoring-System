using Frontend.Models.Data.Service;
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
   
    public class FarmerViewModel
    {
        [Key]
        public string FarmerId { get; set; }
        [Display(Name ="First name")]
        public string Firstname { get; set; }
        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Name")]
        [NotMapped]
        public string FullName { get; set; } 
        public string Address { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        [Display(Name = "Date")]
        [NotMapped]
        public string DateCreated { get; set; }
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        [Display(Name = "Region")]
        [NotMapped]
        public SelectList RegionList { get; set; }
        [Display(Name = "Region")]
        [NotMapped]
        public string RegionName { get; set; }
        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        [Display(Name = "Gender")]
        [NotMapped]
        public SelectList GenderList { get; set; }
        [Display(Name = "Gender")]
        [NotMapped]
        public string GenderName { get; set; }
        [Display(Name = "photo")]
        public int? FarmerImageId { get; set; }
        [Display(Name = "photo")]
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [Display(Name = "photo")]
        [NotMapped]
        public byte[] FarmerPhoto { get; set; } 
        [NotMapped]
        public string Base64StringPic { get; set; }
        [Display(Name = "Farmer")]
        [NotMapped]
        public string UserId { get; set; }
        [NotMapped]
        public string Username { get; set; }

    }
}
