﻿using JWTWebAPI_Dion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebAPI_Dion.Models
{
    [Table("TB_M_Role")]
    public class Role : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
