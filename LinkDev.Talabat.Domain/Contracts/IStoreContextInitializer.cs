using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Contracts
{
    public interface IStoreContextInitializer
    {
        Task InitializeOrUpdateAsync();
        Task SeedDataAsync();
    }
}
