using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.Runtime;
using Topshelf.ServiceConfigurators;

namespace HangfireServerNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostBuilder = new HostBuilder().ConfigureAppConfiguration((hostContext, config) =>
            {
                // configure application configuration
            }).ConfigureServices((hostContext, service) =>
            {
                // configure application service
                service.AddTransient<HangfireService>();
            });

            var host = hostBuilder.Build();
            
            // Begin topshelf configuration
            int result = (int)HostFactory.Run((Action<HostConfigurator>)(x =>
            {
                x.Service<HangfireService>();
                x.RunAsLocalSystem();
                x.SetDescription("PPO Background job console/service");
                x.SetDisplayName("PPO Background Job");
                x.SetServiceName("PPO Background Job");
            }));
        }
    }
}
