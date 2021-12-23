using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Core.Entities
{
    [Table("AspNetUsers", Schema = "dbo")]
    public class AspNetUsers
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        //public string Email { get; set; }

        public string PasswordHash { get; set; }

        //public bool IsActive { get; set; }
    }
}
