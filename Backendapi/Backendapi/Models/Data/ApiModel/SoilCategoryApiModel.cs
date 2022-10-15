using System.ComponentModel.DataAnnotations;

namespace Backendapi.Models.Data.ApiModel
{
    public class SoilCategoryApiModel
    {
        [Key]
        public int SoilCategoryId { get; set; }
        public string SoilName { get; set; } = null!;
    }
}
