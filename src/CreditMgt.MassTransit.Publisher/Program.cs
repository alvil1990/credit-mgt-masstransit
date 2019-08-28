
using CreditMgt.MassTransit.Infrastructure.Messaging;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
namespace CreditMgt.MassTransit.Publisher
{
    class Program : BaseService
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            Task.Run(async () => { await host.RunAsync(); }).Wait();
            Console.ReadKey();
        }


        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = new HostBuilder()
                 .ConfigureHostConfiguration(configHost =>
                 {
                     configHost.SetBasePath(Directory.GetCurrentDirectory());
                     configHost.AddJsonFile("hostsettings.json", optional: true);
                     configHost.AddJsonFile($"appsettings.json", optional: false);
                     configHost.AddEnvironmentVariables();
                     configHost.AddEnvironmentVariables("CreditMgt_MassTransit_Order.Service");
                     configHost.AddCommandLine(args);
                 })
                 .ConfigureAppConfiguration((hostContext, config) =>
                 {
                     config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false);
                 })
                 .ConfigureServices((hostContext, services) =>
                 {
                     services.AddTransient<IBusControl>((svc) =>
                     {

                         var rabbitMQConfigSection = hostContext.Configuration.GetSection("RabbitMQ");
                         rabbitMQHost = rabbitMQConfigSection["Host"];
                         string rabbitMQUserName = rabbitMQConfigSection["UserName"];
                         string rabbitMQPassword = rabbitMQConfigSection["Password"];
                         return new BusConfiguration(rabbitMQHost, rabbitMQUserName, rabbitMQPassword).ConfigureBus();
                     });
                     services.AddHostedService<PublishManager>();

                 })
                 .UseConsoleLifetime();

            return hostBuilder;
        }
    }

}
