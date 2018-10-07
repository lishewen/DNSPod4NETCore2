using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DNSPod4NETCore2.Cli
{
    class Program
    {
        static IConfigurationRoot Configuration { get; set; }
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }

                    Configuration = config.Build();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    DnsPodConfiguration configuration = Configuration.Get<DnsPodConfiguration>();
                    services.AddSingleton(configuration);
                    services.AddHttpClient<IDnsPodClient, DnsPodClient>();
                })
                .ConfigureLogging(a =>
                {
                    a.SetMinimumLevel(LogLevel.None);
                });

            await builder.RunConsoleAsync();
        }
    }
}
