using CreditMgt.MassTransit.Infrastructure.Messaging;
using CreditMgt.MassTransit.Infrastructure.Messaging.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditMgt.MassTransit.OrderService
{
    public class NewOrderConsumer : IConsumer<INewOrder>
    {

        public async Task Consume(ConsumeContext<INewOrder> context)
        {
            await Console.Out.WriteLineAsync($"Received: {context.Message.ID}");
            //publish a NewOrderNotification
            NewOrderNotification orderNote = new NewOrderNotification
            {
                ID = Guid.NewGuid()
            };
            if (context.RequestId.HasValue)
                await context.RespondAsync<INewOrderNotification>(orderNote);
          await context.Publish(orderNote);
            await Console.Out.WriteLineAsync($"Send a NewOrderNotification: {orderNote.ID}");
        }

    }
}
