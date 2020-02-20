using FluentScheduler;
using HolidayOptimizations.BackgroundWorker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ninject;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HoildayOptimizations.Integrations;
using static HolidayOptimizations.BackgroundWorker.Factory;
using HolidayOptimizations.Common.Helpers.Api;
using HolidayOptimizations.StorageRepository.DataRepository.Features.Holidays;
using HolidayOptimizations.StorageRepository.DataRepositoryInterface.Features.Holidays;

namespace App.Client.Background.Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<BackgroundServiceService>();
                });

            if (isService)
            {
                builder.RunAsServiceAsync().GetAwaiter().GetResult();
            }
            else
            {
                builder.RunConsoleAsync().GetAwaiter().GetResult();
            }
        }
    }

    /// <summary>
    /// Background service hosting handle
    /// </summary>
    public class BackgroundServiceService : IHostedService, IDisposable
    {
        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Called when process is starting
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            HolidayOptimizations.StorageRepository.DataRepository.ConnectionString.Value =
                "Server = tcp:digitalmenudb.database.windows.net,1433; Initial Catalog = DigitalMenu; Persist Security Info = False; User ID = Ivona; Password = P@ssw0rd123; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30; ";
            JobManager.JobFactory = new JobFactory(new StandardKernel(new MyNinjectModule()));
            JobManager.Initialize(new JobRegistry());

            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when process is ending
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}