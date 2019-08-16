using DemoApp.Web.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DemoApp.Web
{
    public class Program
    {
        //Reference: https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-2.2
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider; /* creamos el alcance del servicio*/
                try
                {
                    var seeder = services.GetRequiredService<Seeder>(); /*instanciamos nuestro servicio, en este caso al Seeder*/

                    seeder.SeederAsync().Wait(); /*Mandamos a llamar el Seeder*/
                }
                catch (Exception ex) /*Capturamos el error en caso que exista*/
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run(); /*Ejecutamos el host*/
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}