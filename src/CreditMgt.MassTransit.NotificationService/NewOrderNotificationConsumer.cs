using CreditMgt.MassTransit.Infrastructure.Messaging;
using CreditMgt.MassTransit.Infrastructure.Messaging.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditMgt.MassTransit.NotificationService
{
    public class NewOrderNotificationConsumer : IConsumer<INewOrderNotification>
    {
      
        public async Task Consume(ConsumeContext<INewOrderNotification> context)
        {
            await Console.Out.WriteLineAsync($"NewOrderNotification Received: {context.Message.ID}");
        }

    }
}
