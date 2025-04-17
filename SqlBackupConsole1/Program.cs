using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SqlBackupConsole1;
using System;

using System.Threading.Tasks;

namespace SqlBackupConsole1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                IHost host = Host.CreateDefaultBuilder(args)
                    .UseWindowsService()
                    .UseSerilog((context, services, configuration) =>
                    {
                        configuration
                            .WriteTo.File("D:\\Backup\\logs\\startup_log.txt", rollingInterval: RollingInterval.Day)
                            .WriteTo.Console();
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddHostedService<SqlBackupWorker>();
                    })
                    .Build();

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Application failed to start.");
                Console.WriteLine($"Error: {ex}");
                throw; // Re-throw to ensure the service fails visibly

            }
        }

    }
}
