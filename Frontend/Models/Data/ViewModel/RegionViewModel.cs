using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frontend.Models.Data.ViewModel
{
    public class RegionViewModel 
    {
        [Key]
        public int RegionId { get; set; }
        [DisplayName("Region")]
        public string? RegionName { get; set; }
    }
}
