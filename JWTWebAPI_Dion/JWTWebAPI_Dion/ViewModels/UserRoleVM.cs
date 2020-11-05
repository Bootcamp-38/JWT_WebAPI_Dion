using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebAPI_Dion.ViewModels
{
    public class UserRoleVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
