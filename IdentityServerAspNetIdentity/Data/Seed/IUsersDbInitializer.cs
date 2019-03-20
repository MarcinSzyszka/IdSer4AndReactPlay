using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Data.Seed
{
    public interface IUsersDbInitializer
    {
        Task SeedAsync();
    }
}