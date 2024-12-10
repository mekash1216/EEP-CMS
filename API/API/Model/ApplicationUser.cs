using Microsoft.AspNetCore.Identity;

namespace API.Model
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; } = true;
    
    }
}
