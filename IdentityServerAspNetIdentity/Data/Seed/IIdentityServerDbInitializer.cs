using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Data.Seed
{
    public interface IIdentityServerDbInitializer
    {
        Task SeedAsync();
    }
}