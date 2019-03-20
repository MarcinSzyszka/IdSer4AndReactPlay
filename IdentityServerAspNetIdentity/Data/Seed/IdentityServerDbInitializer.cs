using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityServerAspNetIdentity.Data.Seed
{
    public class IdentityServerDbInitializer : IIdentityServerDbInitializer
    {
        private readonly PersistedGrantDbContext _persistedGrantContext;
        private readonly ConfigurationDbContext _configurationContext;
        public IdentityServerDbInitializer(PersistedGrantDbContext persistedGrantContext, ConfigurationDbContext configurationContext)
        {
            _persistedGrantContext = persistedGrantContext;
            _configurationContext = configurationContext;
        }

        public async Task SeedAsync()
        {
            await _persistedGrantContext.Database.MigrateAsync().ConfigureAwait(false);
            await _configurationContext.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _configurationContext.Clients.AnyAsync())
            {
                foreach (var client in Config.GetClients())
                {
                    _configurationContext.Clients.Add(client.ToEntity());
                }
                _configurationContext.SaveChanges();
            }
            if (!await _configurationContext.IdentityResources.AnyAsync())
            {
                foreach (var resource in Config.GetIdentityResources())
                {
                    _configurationContext.IdentityResources.Add(resource.ToEntity());
                }
                _configurationContext.SaveChanges();
            }
            if (!await _configurationContext.ApiResources.AnyAsync())
            {
                foreach (var resource in Config.GetApis())
                {
                    _configurationContext.ApiResources.Add(resource.ToEntity());
                }
                _configurationContext.SaveChanges();
            }
        }
    }
}
