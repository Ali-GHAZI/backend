using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Auth
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool IsActive { get; set; } = true;
    }
}
