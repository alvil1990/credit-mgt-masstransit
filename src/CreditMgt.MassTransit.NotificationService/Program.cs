
using CreditMgt.MassTransit.Infrastructure.Messaging;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CreditMgt.MassTransit.NotificationService
{
    class Program
    {
        public static void Main(string[] args)
        {
         
           var host = CreateHostBuilder(args).Build();
             host.RunAsync();
            Console.ReadLine();


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
                                   string rabbitMQHost = rabbitMQConfigSection["Host"];
                                   string rabbitMQUserName = rabbitMQConfigSection["UserName"];
                                   string rabbitMQPassword = rabbitMQConfigSection["Password"];
                                   return new BusConfiguration(rabbitMQHost, rabbitMQUserName, rabbitMQPassword).ConfigureBus((cfg, host) =>
                                   {
                                       cfg.ReceiveEndpoint("notification.service", e =>
                                       {
                                           e.Consumer<NewOrderConsumer>();
                                           e.Consumer<NewOrderNotificationConsumer>();
                                       });
                                   });
                               });
                               services.AddHostedService<NotificationHostedService>();

                           })
                           .UseConsoleLifetime();

            return hostBuilder;
        }
    }

}
