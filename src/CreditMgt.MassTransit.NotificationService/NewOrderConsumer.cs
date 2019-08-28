using CreditMgt.MassTransit.Infrastructure.Messaging;
using CreditMgt.MassTransit.Infrastructure.Messaging.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditMgt.MassTransit.NotificationService
{
    public class NewOrderConsumer : IConsumer<INewOrder>
    {
      
        public async Task Consume(ConsumeContext<INewOrder> context)
        {
            await Console.Out.WriteLineAsync($"New Order Received: {context.Message.ID}");
        }

    }
}
