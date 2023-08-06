using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backendapi.Models.Data.ApiModel
{
    public class UserAccountDetailsApiModel 
    {
       public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int GenderId { get; set; }

        public string HomeTown { get; set; }

        public int RegionId { get; set; }

        public string Residence { get; set; }

        public string Address { get; set; }

        public byte[]? UserPhoto { get; set; }

        public string ContactNumber { get; set; }

    }
}
