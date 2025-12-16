using LinkDev.Talabat.Domain.Contracts.Persistence.DbInitializers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Common
{
    internal abstract class BaseDbInitializer(DbContext dbContext) : IDbInitializer
    {
        public async Task InitializeOrUpdateAsync()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
                await dbContext.Database.MigrateAsync(); //Update-Database
        }

        public abstract Task SeedDataAsync();
    }
}
