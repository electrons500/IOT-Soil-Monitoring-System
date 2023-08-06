using System.ComponentModel.DataAnnotations;

namespace Backendapi.Models.Data.ApiModel
{
    public class FarmApiModel
    {
        [Key]
        public string FarmId { get; set; } = null!;
        public string FarmName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string DateCreated { get; set; } = null!;
        public string FarmerId { get; set; } = null!;
        public string? FarmerName { get; set; }
        public string? FarmerContact { get; set; }
        public int RegionId { get; set; }
        public string? RegionName { get; set; }
        public int SoilCategoryId { get; set; }
        public string? SoilCategoryName { get; set; }
    
        public string SerialNumber { get; set; } 

    }
}
