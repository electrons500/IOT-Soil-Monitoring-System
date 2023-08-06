using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backendapi.Models.Data.ApiModel
{
    public class UserRoleApiModel
    {
        [Key]
        public string RoleId { get; set; }
        [NotMapped]
        public string RoleName { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        public string FullName { get; set; }
        [NotMapped]
        [Phone]
        public string Contact { get; set; } 
    }
}
