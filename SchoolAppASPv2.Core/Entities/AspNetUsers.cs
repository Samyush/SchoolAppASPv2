
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
