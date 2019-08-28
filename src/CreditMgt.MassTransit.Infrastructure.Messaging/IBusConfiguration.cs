using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditMgt.MassTransit.Infrastructure.Messaging
{
   public interface IBusConfiguration
    {
        IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null);
        string Host { get; }
    }
}
