using System.ComponentModel.DataAnnotations;

namespace Backendapi.Models.Data.ApiModel
{
    public class RegionApiModel
    {
        [Key]
        public int RegionId { get; set; }
        public string RegionName { get; set; } = null!;

    }
}
