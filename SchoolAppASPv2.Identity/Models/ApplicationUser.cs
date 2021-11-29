using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SchoolAppASPv2.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }

        public string? Country { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? LastName { get; set; }
    }
}
