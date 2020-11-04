using JWTWebAPI_Dion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebAPI_Dion.Models
{
    [Table("TB_M_Application")]
    public class Application : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserApplication> UserApplications { get; set; }
    }
}
