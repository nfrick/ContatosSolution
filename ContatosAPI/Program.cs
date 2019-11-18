using ContatosAPI.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ContatosAPI {
    public class Program {
        public static void Main(string[] args) {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try {
                    var context = services.GetRequiredService<ContatosContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Erro ao inicializar o banco de dados.");
                }
            }

            host.Run();
        }

        //Logging: https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/logging/?view=aspnetcore-3.0
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.ConfigureLogging(logging => {
                //    logging.ClearProviders();
                //    logging.AddConsole();
                //})
                .UseStartup<Startup>()
                .Build();


        /*
            public static void Main(string[] args) {
                CreateWebHostBuilder(args).Build().Run();
            }

            public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>();
        */
    }
}

