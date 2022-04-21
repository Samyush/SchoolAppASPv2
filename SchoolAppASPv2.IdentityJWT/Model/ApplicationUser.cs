using Microsoft.AspNetCore.Identity;
using SchoolAppASPv2.IdentityJWT.Entities;
using System.Text.Json.Serialization;

namespace SchoolAppASPv2.IdentityJWT.Model
{
    public class ApplicationUser : IdentityUser
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [JsonIgnore]
        public string? PasswordHash { get; set; }
        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
