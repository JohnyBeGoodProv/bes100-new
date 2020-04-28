using LibraryApi;
using LibraryApi.Services;
using LibraryApiIntegrationTest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace LibraryApiIntegrationTests
{
    public class WebTestFixture : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(ISystemTime));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                    services.AddTransient<ISystemTime, TestingSystemTime>();
                }

                var provider = services
                    .BuildServiceProvider();





                var sp = services.BuildServiceProvider();


                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var loggerFactory = scopedServices.GetRequiredService<ILoggerFactory>();

                    var logger = scopedServices
                        .GetRequiredService<ILogger<WebTestFixture>>();




                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the " +
                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}