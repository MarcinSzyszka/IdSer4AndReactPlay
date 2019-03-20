using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAspNetIdentity.Data.Seed
{
    public class UsersDbInitializer : IUsersDbInitializer
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersDbInitializer(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await _applicationDbContext.Database.MigrateAsync().ConfigureAwait(false);

            var adminRoleApi1 = await _applicationDbContext.Roles.FirstOrDefaultAsync(r => r.AppName == "api1" && r.Name == "Admin");
            if (adminRoleApi1 == null)
            {
                adminRoleApi1 = new AppIdentityRole
                {
                    Name = "Admin",
                    AppName = "ap1",
                    NormalizedName = "API1"
                };

                await _applicationDbContext.Roles.AddAsync(adminRoleApi1);
            }

            var alice = _userManager.FindByNameAsync("alice").Result;



            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "alice"
                };
                var result = _userManager.CreateAsync(alice, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                await _userManager.AddClaimsAsync(alice, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address,
                        @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }",
                        IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                });

                await _applicationDbContext.UserRoles.AddAsync(new AppUserRole
                {
                    RoleId = adminRoleApi1.Id,
                    UserId = alice.Id
                });

                await _applicationDbContext.SaveChangesAsync();
            }

            var bob = _userManager.FindByNameAsync("bob").Result;
            if (bob == null)
            {
                bob = new ApplicationUser
                {
                    UserName = "bob"
                };

                await _userManager.CreateAsync(bob, "Pass123$");


                await _userManager.AddClaimsAsync(bob, new Claim[]
                {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address,
                            @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }",
                            IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere")
                });
            }
        }
    }
}
