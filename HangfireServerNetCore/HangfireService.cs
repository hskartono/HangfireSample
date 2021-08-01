using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace HangfireServerNetCore
{
    public class HangfireService : ServiceControl
    {
        private BackgroundJobServer _server;

        public HangfireService()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();

            string cn = config.GetConnectionString("HangfireConnection");
            GlobalConfiguration.Configuration.UseSqlServerStorage(cn, new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            });
        }

        public bool Start(HostControl hostControl)
        {
            _server = new BackgroundJobServer();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _server.Dispose();
            return true;
        }
    }
}
