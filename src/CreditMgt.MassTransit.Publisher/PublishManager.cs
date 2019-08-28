

using CreditMgt.MassTransit.Infrastructure.Messaging;
using CreditMgt.MassTransit.Infrastructure.Messaging.Messages;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CreditMgt.MassTransit.Publisher
{
    public class PublishManager :BaseService, IHostedService
    {
        CancellationTokenSource _cancellationTokenSource;
        Task _task;
        IBusControl _busControl;
        OrderServiceClient _orderServiceClient;
        public PublishManager(IBusControl busControl)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _busControl = busControl;
            _orderServiceClient = new OrderServiceClient(_busControl, rabbitMQHost);

        }
      
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _busControl.StartAsync();
            _task = Task.Run(() => Worker(), _cancellationTokenSource.Token);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _busControl.StopAsync();
            return Task.CompletedTask;
        }

        private async void Worker()
        {

            while (true)
            {



                NewOrder reqCommand = new NewOrder();
                reqCommand.ID = Guid.NewGuid();
                Console.WriteLine("Sending ID:{0}", reqCommand.ID);

                //  var result = await _orderServiceClient.NewOrder(reqCommand);
                // Console.WriteLine("Return notificationID ID:{0}", result.ID);

                //    await _orderServiceClient.NewOrderPublishSample(reqCommand); //publish
                // await _orderServiceClient.NewOrderSendSpecificService(reqCommand); //publish to specific service
                Thread.Sleep(5000);
            }


        }
    }
}
