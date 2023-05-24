using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudioTemplate.DAL;

namespace StudioTemplate.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    
    }
}
