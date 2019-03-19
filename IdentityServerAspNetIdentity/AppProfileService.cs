using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityServerAspNetIdentity
{
    public class AppProfileService : ProfileService<ApplicationUser>
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AppProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, ApplicationDbContext context) : base(userManager, claimsFactory)
        {
            _userManager = userManager;
            _context = context;
        }

        public AppProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, ILogger<ProfileService<ApplicationUser>> logger, ApplicationDbContext context) : base(userManager, claimsFactory, logger)
        {
            _userManager = userManager;
            _context = context;
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            await base.GetProfileDataAsync(context);

            var roles = _context.UserRoles.Include(r => r.Role).Where(r => r.UserId == context.Subject.GetSubjectId() && context.RequestedResources.ApiResources.Select(res => res.Name).Contains(r.Role.AppName));

            context.IssuedClaims.AddRange(roles.Select(r => new Claim(JwtClaimTypes.Role, r.Role.Name)));
        }
    }
}
