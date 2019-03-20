using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerAspNetIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppIdentityRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppIdentityRole>(b =>
            {
                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.AppUserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });
        }
    }
}
