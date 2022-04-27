using System.ComponentModel.DataAnnotations;

namespace SchoolAppASPv2.IdentityJWT.Model.Users
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
