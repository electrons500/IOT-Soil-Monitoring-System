using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models.Data.ViewModel
{
    public class VerifyArduinoViewModel
    {
        [Key]
        [DisplayName("Serial number")]
        public string SerialNumber { get; set; } 
    }
}
