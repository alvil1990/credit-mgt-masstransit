using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditMgt.MassTransit.Infrastructure.Messaging
{
    public class BusConfiguration : IBusConfiguration
    {
        private readonly string _host;
        private readonly string _username;
        private readonly string _password;
        public BusConfiguration(string host, string username, string password)
        {
            _host = host;
            _username = username;
            _password = password;
        }
        public string Host { get { return _host; } }
        public IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
           return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(_host), hst =>
                {
                    hst.Username(_username);
                    hst.Password(_password);
                });
                registrationAction?.Invoke(cfg, host);

            });
        }

    }
}
