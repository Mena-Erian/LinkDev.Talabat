using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Identity
{
    internal class StoreIdentityDbInitializer(StoreIdentityDbContext dbContext, UserManager<ApplicationUser> userManager) : BaseDbInitializer(dbContext), IStoreIdentityDbInitializer
    {

        public override async Task SeedDataAsync()
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Mena Erian",
                    UserName = "Mena.Erian",
                    Email = "codewithmena@gmail.com",
                    PhoneNumber = "01000000000"
                };
                var result = await userManager.CreateAsync(user, "P@ss0rd");

            }
        }
    }
}
