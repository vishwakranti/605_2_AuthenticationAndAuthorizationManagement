using BikeSparePartsShop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace APIIntegrationTest
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType ==
                                                            typeof(DbContextOptions<ApplicationDbContext>));
                // we remove the ApplicationDbContext registration from the Program class
                if (descriptor != null)
                    services.Remove(descriptor);

                //we add the database context to the service container and instruct it to use the in-memory database instead of the real database
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryStockApiTest");
                });

                //Finally, we ensure that we seed the data from the ApplicationDbContext class (The same data you inserted into a real SQL Server database).
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())

                using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();

                        //context.Database.EnsureCreated() ensures that the database for the context exists. If it exists, no action is taken. If it does not exist then the database and all its schema are created and also it ensures it is compatible with the model for this context.

                        //Seed test data
                        AddTestData.AddStockData(appContext);
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            });
        }

    }
}
