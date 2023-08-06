using System.ComponentModel.DataAnnotations;

namespace Backendapi.Models.Data.ApiModel
{
    public class GenderApiModel
    {
        [Key]
        public int GenderId { get; set; }
        public string GenderName { get; set; } = null!;
    }
}
