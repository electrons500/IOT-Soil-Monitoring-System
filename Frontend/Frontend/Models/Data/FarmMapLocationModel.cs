using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models.Data
{
    public class FarmMapLocationModel
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        [Key]
        public string SerialNumber { get; set; }
    }
}
