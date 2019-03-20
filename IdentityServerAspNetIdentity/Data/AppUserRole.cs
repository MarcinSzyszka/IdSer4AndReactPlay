using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerAspNetIdentity.Data
{
    public class AppUserRole : IdentityUserRole<string>
    {
        public virtual AppIdentityRole Role { get; set; }
    }
}
