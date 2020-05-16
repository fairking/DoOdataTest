using DoOdataTest.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xtensive.Orm;

namespace DoOdataTest
{
    public static class DatabaseSetup
    {
        public static IHost EnsureDatabaseCreated(this IHost host)
        {
            Exception error = null;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var session = services.GetRequiredService<Session>();
                var logger = services.GetRequiredService<ILogger<Program>>();

                logger.LogInformation($"EnsureDatabaseCreated ASPNETCORE_ENVIRONMENT: {System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}");

                // Seed database
                try
                {
                    if (!session.Query.All<WeatherForecast>().Any())
                    {
                        using (var tran = session.OpenTransaction())
                        {
                            try
                            {
                                var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

                                var rng = new Random();

                                var town1 = new Town("London");
                                var town2 = new Town("New York");
                                var town3 = new Town("Sidney");
                                var allTowns = new[] { town1, town2, town3 };

                                var entities = Enumerable.Range(1, 25).Select(index => new WeatherForecast(allTowns[rng.Next(allTowns.Length)])
                                {
                                    Date = DateTime.Now.AddDays(index),
                                    TemperatureC = rng.Next(-20, 55),
                                    Summary = summaries[rng.Next(summaries.Length)]
                                }).ToArray();

                                session.SaveChanges();

                                tran.Complete();
                            }
                            catch
                            {
                                throw;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "Error whilst seeding database");
                    error = new Exception("Error whilst seeding database", ex);
                }
            }

            if (error != null)
                throw error;

            return host;
        }
    }
}
