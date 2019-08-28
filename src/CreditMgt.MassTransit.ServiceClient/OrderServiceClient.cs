using CreditMgt.MassTransit.Infrastructure.Messaging.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditMgt.MassTransit.Publisher
{
    public class OrderServiceClient
    {
        private readonly Uri serviceAddress;
        private readonly IBusControl _busControl;
        public OrderServiceClient(IBusControl busControl,string host)
        {
            _busControl = busControl;
            serviceAddress = new Uri($"{host}order.service");
        }
        public Task<INewOrderNotification> NewOrder(INewOrder requestCmd)
        {
            IRequestClient<INewOrder, INewOrderNotification> client =
                _busControl.CreateRequestClient<INewOrder, INewOrderNotification>(serviceAddress, TimeSpan.FromSeconds(10));

            return client.Request(requestCmd);
        }
        public Task NewOrderPublishSample(INewOrder msg)
        {
            return _busControl.Publish(msg);
        }
        public Task NewOrderSendSample(INewOrder msg)
        {
            return _busControl.Send(msg);
        }
        public async Task NewOrderSendSpecificService(INewOrder msg)
        {
            var endPoint = await _busControl.GetSendEndpoint(serviceAddress);
            await endPoint.Send(msg);
        }
    }
}
