using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models.Data.ViewModel
{
    public class GenderViewModel
    {
        [Key]
        public int GenderId { get; set; }
        public string GenderName { get; set; }

    }
}
