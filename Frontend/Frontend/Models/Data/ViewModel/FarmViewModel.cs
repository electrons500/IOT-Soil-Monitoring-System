using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frontend.Models.Data.ViewModel
{
    public class FarmViewModel
    {
        [Key]
        [DisplayName("S/No")]
        public string? FarmId { get; set; }
        [DisplayName("Farm name")]
        public string? FarmName { get; set; } 
        public string? Location { get; set; }
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        [NotMapped]
        public string? DateCreated { get; set; }
        [DisplayName("Farmer name")]
        public string? FarmerId { get; set; }
        [NotMapped]
        [DisplayName("Farmer name")]
        public string? FarmerName { get; set; }
        [DisplayName("Region")]
        public int RegionId { get; set; }
        [DisplayName("Region")]
        [NotMapped]
        public SelectList? RegionList { get; set; }

        [DisplayName("Region")]
        [NotMapped]
        public string? RegionName { get; set; }
        [DisplayName("Soil Category")]
        public int SoilCategoryId { get; set; }
        [DisplayName("Soil Category")]
        [NotMapped]
        public SelectList? SoilCategoryList { get; set; }
        [DisplayName("Soil Category")]
        [NotMapped]
        public string? SoilCategoryName { get; set; }
        [DisplayName("Farmer contact")]
        [NotMapped]
        public string? FarmerContact { get; set; }
        [DisplayName("S/No of device on the farm")]
        [NotMapped]
        public string SerialNumber { get; set; }
        [NotMapped]
        public string ArduinoId { get; set; }
        [DisplayName("Latitude")]
        [NotMapped]
        public decimal Latitude { get; set; }
        [DisplayName("Longitude")]
        [NotMapped]
        public decimal Longitude { get; set; }

    }
}
