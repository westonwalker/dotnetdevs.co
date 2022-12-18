using Microsoft.AspNetCore.Identity;

namespace dotnetdevs.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Company Company { get; set; }
        public Developer Developer { get; set; }
    }
}
