using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models.Data.ViewModel
{
    public class SoilCategoryViewModel
    {
        [Key]
        [DisplayName("S/No")]
        public int SoilCategoryId { get; set; }
        [DisplayName("Soil Category")]
        public string SoilName { get; set; } = null!;
    }
}
