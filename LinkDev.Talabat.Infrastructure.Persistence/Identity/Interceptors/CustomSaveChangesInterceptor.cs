using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Identity.Interceptors
{
    internal class CustomSaveChangesInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            //UpdateEntities(eventData, result);

            //loggedInUserService.
            return base.SavingChanges(eventData, result);
        }

    }
}
