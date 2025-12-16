using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Domain.Contracts.Persistence.DbInitializers;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();

            //var StoreContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
            // Ask Runtime Env For an Object from "Store Context" Service Explicitly.

            var storeContextInitializer = scope.ServiceProvider.GetRequiredService<IStoreContextInitializer>();
            var identityContextInitializer = scope.ServiceProvider.GetRequiredService<IStoreIdentityDbInitializer>();

            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            //var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                await storeContextInitializer.InitializeOrUpdateAsync();
                await storeContextInitializer.SeedDataAsync();

                await identityContextInitializer.InitializeOrUpdateAsync();
                await identityContextInitializer.SeedDataAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex.Message, "An Error has been occurred during applying the migrations Or The Data Seeding.");
            }
            return app;
        }
    }
}
