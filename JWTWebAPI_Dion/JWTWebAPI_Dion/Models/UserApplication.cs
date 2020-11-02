using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebAPI_Dion.Models
{
    [Table("TB_T_UserApplication")]
    public class UserApplication
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
