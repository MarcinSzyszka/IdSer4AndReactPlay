using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerAspNetIdentity.Data
{
    public class AppIdentityRole : IdentityRole<string>
    {
        public string AppName { get; set; }
        public ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}
