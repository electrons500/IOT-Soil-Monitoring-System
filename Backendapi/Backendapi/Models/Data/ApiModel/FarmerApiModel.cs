using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backendapi.Models.Data.ApiModel
{
    public class FarmerApiModel
    {
        [Key]
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
        public int FarmerImageId { get; set; }
        
        public byte[]? FarmerPhoto { get; set; }
  
        public string? UserId { get; set; }
        public string? Username { get; set; }

       
    }
}
