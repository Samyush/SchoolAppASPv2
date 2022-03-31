using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppASPv2.Core.Entities
{
    [Table("user", Schema = "dbo")]

    public class Users
    {
        [Key]
        public int Id { get; set; }
        
        public int UserTypeId { get; set; }
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public  bool IsActive { get; set; }
        
        [ForeignKey("UserTypeId")] 
        public UserType UserType { get; set; }
    }
}
